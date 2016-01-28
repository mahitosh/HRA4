using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.Clinic;
using HtmlAgilityPack;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using EvoPdf;
using System.Xml;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;

namespace RiskApps3.Utilities
{
    public enum pacsSubTypes : int { PDF = 1, JPG, BMP, inverseBMP };

    public class InterfaceUtils
    {
        /// <summary>
        /// send this document to Powerscribe as an ORU message using lkpInterfaceDefinitions
        /// IFF this is a POWERSCRIBE interface.  
        /// </summary>
        /// <param name="dt">DocumentTemplate</param>
        /// <returns></returns>
        public static bool sendPowerscribe(DocumentTemplate dt)
        {
            // fetch interfaceId from the document template...
            int interfaceId = -1;
            bool returnValue = false;
            string interfaceType = "";
            string ip = "";
            string port = "";
            string segmentList = "";

            ParameterCollection psArgs = new ParameterCollection();
            psArgs.Add("documentTemplateID", dt.documentTemplateID);

            try
            {
                // NOTE: only ONE Powerscribe interface is currently supported.  The PACS code will support multiple interfaces per document template.
                // It seems unlikely that there will be multiple Powerscribe interfaces per document, but if there are, then you will have to add in the
                // code to iterate through multiple rows returned from this SP call and process them individually as shown in the PACS code, below.  jdg 8/17/15
                SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_getInterfaceDefinitionFromTemplateID", psArgs);
                while (reader.Read())
                {
                    if (reader.IsDBNull(0) == false)
                    {
                        interfaceId = reader.GetInt32(0);
                    }
                    if (reader.IsDBNull(1) == false)
                    {
                        interfaceType = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2) == false)
                    {
                        ip = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3) == false)
                    {
                        port = reader.GetString(3);
                    }
                    if (reader.IsDBNull(4) == false)
                    {
                        segmentList = reader.GetString(4);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("SendPowerscribe:  Error running sp_getInterfaceDefinitionsfromTemplateID, error = " + e.Message);
                return false;
            }

            if ((!String.IsNullOrEmpty(interfaceType)) && (interfaceType.ToUpper() == "POWERSCRIBE") && (interfaceId > 0))
            {
                // note:  this method returns a bool success flag... use it to 
                returnValue = sendPowerscribe(dt, interfaceId, interfaceType, ip, port, segmentList);
            }

            return returnValue;
        }

        private static bool sendPowerscribe(DocumentTemplate dt, int interfaceId, string interfaceType, string ip, string port, string hl7SegmentList)
        {
            bool returnValue = false;

            if ((!String.IsNullOrEmpty(ip)) && (!String.IsNullOrEmpty(port)))
            {
                string str = null;
                string hl7Response = "";
                TcpClient client;
                Stream stream = null;

                try
                {
                    client = new TcpClient(ip, Int32.Parse(port));
                    client.SendTimeout = 5000;  // milliseconds
                    client.ReceiveTimeout = 30000;
                    stream = client.GetStream();
                }
                catch (Exception e)
                {
                    Logger.Instance.WriteToLog("Failed to establish HL7 MLLP listener with Powerscribe at location " + ip + ":" + port + ", underlying error message is: " + e.ToString());
                }

                // Extract the outbound message from the html document... 
                str = extractHL7(dt, hl7SegmentList);   // enter the segmentList in lkpInterfaceDefinitions.stringParam1 if you don't want to use the default segment list

                if (stream == null)
                {
                    Logger.Instance.WriteToLog("No HL7 MLLP connection with Powerscribe at location " + ip + ":" + port + ".");
                    return false;
                }
                // Transmit the HL7 message, and await the response.
                try
                {
                    MLLP mllp = new MLLP(stream, false);
                    mllp.Send(str);
                    hl7Response = mllp.Receive();
                    returnValue = true;     // got a response...
                    Logger.Instance.WriteToLog("MLLP Message Received: " + hl7Response);
                }
                catch (Exception e)
                {
                    // TODO:  Do something, you know, retry perhaps?
                    Logger.Instance.WriteToLog("Failed to send ORU to Powerscribe.  HL7 message is: " + str + ", underlying error message is: " + e.ToString());
                }
                finally
                {
                    stream.Close();
                }
                return returnValue;
            }
            else
            {
                Logger.Instance.WriteToLog("Failed to send ORU to Powerscribe. Currently configured is interface id " + interfaceId + ", port " + port + ", IP " + ip + ", and segment list " + hl7SegmentList + ".  Relevant tables are lkpAutomationDocuments, lkpInterfaceDefinitions, and lkpDocumentTemplates. ");
            }

            return false;
        }

        private static string extractHL7(DocumentTemplate dt, string segmentList)
        {
            StringBuilder sb = new StringBuilder();
            string[] segments;

            if (!String.IsNullOrEmpty(segmentList))
            {
                segments = segmentList.Split('|');
            }
            else
            {
                segments = new string[] { "MSH", "PID", "PV1", "OBR", "OBX" };        // stick in some defaults... is this a good idea? 
            }

            foreach (string segment in segments)
            {
                if (!String.IsNullOrEmpty(dt.doc.GetElementbyId(segment).InnerText))
                {
                    sb.Append(dt.doc.GetElementbyId(segment).InnerText);  // required
                    sb.Append((char)0x0D);
                }
                else
                {
                    Logger.Instance.WriteToLog("Powerscribe error: segment " + segment + " was not successfully built.  Check your database instance for stored procedure GetHL7ORU" + segment + ", or whatever stored procedure is referenced in your HTML template. ");
                }

            }
            return sb.ToString();

        }

        /// <summary>
        /// Code added for FMH PACS connectivity.  This is generic enough to call any of the DCMTK executables.
        /// wrapDCMTKmethodAsExternalCommand
        /// creates a command-line style call to the program specified by the programQualifiedName argument, then parses the results into a list.  
        /// This takes a variable list of arguments which will be assembled into an argument list.
        /// </summary>
        /// <param name="ip">IP</param>
        /// <param name="port">Port</param>
        /// <param name="programNameQualified">fully qualified name of program, e.g., C:\\Program Files\\DCMTK\\bin\\findscu.exe</param>
        /// <param name="args">args consists of a list of three strings to allow specification of any parameters to findscu (e.g.) we want</param>
        /// <returns>List of name, value pairs returned from DICOM query
        /// string 1: Name of return value, e.g., AccessionNumber, StudyID
        /// string 2: Value of this returned item, e.g., 123456789, Smith^John
        /// </returns>
        public static List<KeyValuePair<string, string>> wrapDCMTKmethodAsExternalCommand(string ip, string port, string programNameQualified, List<TripleArg> args)
        {
            StringBuilder sb = new StringBuilder();

            if ((!String.IsNullOrEmpty(ip)) && (!String.IsNullOrEmpty(port)))
            {
                sb.Append(" " + ip + " " + port + " ");
            }
            // now process the variable list of parameters, 
            // this allows this routine to be used to call the program with any set of args
            // the nature of the arguments is what DCMTK expects, not all program launches could use these style args (but many could)
            //foreach (Tuple<string, string, string> arg in args)
            foreach (TripleArg arg in args)
            {
                sb.Append(" " + arg.ArgSwitch + " ");
                if (!String.IsNullOrEmpty(arg.Name))
                {
                    sb.Append(arg.Name);
                    if (!String.IsNullOrEmpty(arg.Value))
                    {
                        sb.Append("=" + arg.Value);
                    }
                }
            }

            //Logger.Instance.WriteToLog("Debug: DCMTK COMMAND = " + sb.ToString());    // jdg 1/5/16 DEBUGGING ONLY
            // now the string should look like this, for example:
            // findscu -S 127.0.0.1 5678 -k StudyInstanceUID -k StudyID=1 -k QueryRetrieveLevel=STUDY -k StudyTime -k AccessionNumber
            // Now to send this command to DICOM and retrieve the parameters returned from DICOM into an array of <parameterName, parameterValue>

            List<KeyValuePair<string, string>> returnValue = new List<KeyValuePair<string, string>>();
            try
            {
                returnValue = callOutToUnmanagedCode(programNameQualified, sb.ToString());
            }
            catch (Exception e)
            {
                string strDumpArgs = "";
                foreach (TripleArg ta in args)
                {
                    strDumpArgs = strDumpArgs + "; " + ta.dumpArg();
                }
                Logger.Instance.WriteToLog("Error sending DICOM Message. IP = " + ip + ", port = " + port + ", Program Name = " + programNameQualified + ", arguments = " + strDumpArgs + ".  Underlying error was: " + e.Message + ".");
            }   // all errors are fatal.  do nothing (return the empty list), but do log the message

            return returnValue;
        }

        /// <summary>
        /// Call Out to Unmanaged Code.  You can call out to any DOS program, however, the response parsing is specific to the application you call.  
        /// This method is bound to the DCMTK library (which encapsulates DICOM), you can add parsing for additional commands separately.
        /// However, this method should parse most any real DICOM return values, YMMV.  
        /// </summary>
        /// <param name="prg">fully qualified program name suitable for calling Process.Start</param>
        /// <param name="str">a fully built argument list like what you'd have typed if you ran this program on the command line</param>
        /// <returns>Key/Value pairs containing the returned parameters from the call.  </returns>
        private static List<KeyValuePair<string, string>> callOutToUnmanagedCode(string prg, string str)
        {
            List<KeyValuePair<string, string>> response = new List<KeyValuePair<string, string>>();
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = prg,
                    Arguments = str,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            proc.Start();

            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                KeyValuePair<string, string> parameter = parseDCMTKOutputLine(line);
                if (!String.IsNullOrEmpty(parameter.Key)) response.Add(parameter);
                //Console.WriteLine(line);    // todo:  remove me or #DEBUG me
            }

            return response;
        }

        /// <summary>
        /// takes each line of output from the call DCMTK utility and sticks the pertinent values into a key/value pair
        /// WARNING: DICOM specific parsing.  And tested with DCMTK!  Other command line commands may return other formats, parse those separately.
        /// </summary>
        /// <param name="line">The line of console output from the call out to DOS.</param>
        /// <returns>Key/Value pair with parameter returned IF this was a line containing an actual parameter (many lines do not). </returns>
        private static KeyValuePair<string, string> parseDCMTKOutputLine(string line)
        {
            KeyValuePair<string, string> response = new KeyValuePair<string, string>();
            string key = "", value = "";
            int start = 0, end = 0;
            start = line.IndexOf("[") + 1;
            if (start > 0)
            {
                end = line.IndexOf("]");
                if (end > 0)
                {
                    value = line.Substring(start, (end - start));
                }
            }

            if (value != "")
            {
                start = line.LastIndexOf(" ") + 1;
                end = line.Length;
                if ((start > 0) && (end > 0) && (start < end))
                {
                    key = line.Substring(start, (end - start));
                }

                if (key != "")
                {
                    response = new KeyValuePair<string, string>(key, value);
                }
            }

            return response;
        }

        /// <summary>
        /// Send the PDF document referenced by "dt" to the PACS system defined in the database lkpInterfaceDefinitions for this document
        /// effective 1/22/16 the values required to be present in lkpInterfaceDefinitions are as follows:
        /// interfaceType = "PACS"
        /// ipAddress, port self explanatory
        /// stringParam1: "their" AET
        /// stringParam2: "our" AET
        /// stringParam3: qualified path to DCMTK binaries, e.g., C:\HUGHES_DCMTK_DICOM\bin
        /// intParam1: send file type, see enum.  1=pdf, 2=jpg, 3=bmp
        /// stringParam4: Modality to send to PACS.  Defaults to "OT"
        /// stringParam5: Additional parameters.  see documentation for DCMTK "storescu".  e.g., when sending a jpg, you might have to specify jpg type, e.g., "-xy"
        /// </summary>
        /// <param name="dt">Document Template, as per the HTML document generation</param>
        /// <param name="patient">V3 Patient object</param>
        /// <param name="filename">Name of the PDF file to be converted to a .DCM (DICOM) file and transmitted to PACS</param>
        /// <param name="apptId">Appointment ID</param>
        /// <returns></returns>
        public static bool send2PACS(DocumentTemplate dt, RiskApps3.Model.PatientRecord.Patient patient, string filenameIn, int apptId)
        {
            List<TripleArg> args = new List<TripleArg>();   // for want of a tuple which doesn't exist till .NET 4.0
            List<KeyValuePair<string, string>> dicomOutputArgs = new List<KeyValuePair<string, string>>();
            List<InterfaceInstance> interfaces = new List<InterfaceInstance>();

            string accessionNumber = "";
            string patientname = "";
            string dob = "";
            string gender = "";
            string apptDate = "";
            string apptTime = "";

            ParameterCollection pacsArgs = new ParameterCollection();
            pacsArgs.Add("documentTemplateID", dt.documentTemplateID);
            string filename;
            Image img = null;

            SqlDataReader reader = BCDB2.Instance.ExecuteReaderSPWithParams("sp_getInterfaceDefinitionFromTemplateID", pacsArgs);
            while (reader.Read())
            {
                InterfaceInstance instance = new InterfaceInstance();

                if (reader.IsDBNull(0) == false)
                {
                    instance.InterfaceId = reader.GetInt32(0);
                }
                if (reader.IsDBNull(1) == false)
                {
                    instance.InterfaceType = reader.GetString(1);
                }
                if (reader.IsDBNull(2) == false)
                {
                    instance.IpAddress = reader.GetString(2);
                }
                if (reader.IsDBNull(3) == false)
                {
                    instance.Port = reader.GetString(3);
                }
                if (reader.IsDBNull(4) == false)
                {
                    instance.AeTitleRemote = reader.GetString(4);
                }
                if (reader.IsDBNull(5) == false)
                {
                    instance.AeTitleLocal = reader.GetString(5);
                }
                if (reader.IsDBNull(6) == false)
                {
                    instance.DCMTKpath = reader.GetString(6);
                }
                if (reader.IsDBNull(7) == false)
                {
                    instance.PacsSubType = reader.GetInt32(7);   // for now, 1 == PDF, 2 == JPG, 3 == BMP (see enum pacsSubTypes, above)
                }
                // there is no intParam2 for PACS as yet...skipping ahead to the new-as-of-jan-2016 string param 4
                if (reader.IsDBNull(9) == false)
                {
                    instance.Modality = reader.GetString(9);
                }
                if (reader.IsDBNull(10) == false)
                {
                    instance.AdditionalParams = reader.GetString(10);
                }

                if (instance.InterfaceId > 0)
                {
                    interfaces.Add(instance);   // multiple PACS interfaces per document are now supported.
                }

            }

            // just do this once...
            if (apptId > 0)
            {
                // This is an FMH specific hack, they are sending the Accession Number in the HL7 ADT_A08 message in PID|26...
                // and it is being stored in the appointment table in the unused field "referral".  
                // No more obvious way of accomplishing this has jumped out at me yet, but this will have to be made more generic as new
                // customers are brought on-line. 
                string sqlStr = "SELECT referral,patientname,dob,gender,apptDate,apptTime FROM tblAppointments WHERE apptID=" + apptId.ToString() + ";";
                reader = BCDB2.Instance.ExecuteReader(sqlStr);
                while (reader.Read())
                {
                    if (reader.IsDBNull(0) == false)
                    {
                        accessionNumber = reader.GetString(0);
                    }
                    if (reader.IsDBNull(1) == false)   // taking this from appointment table, alternatively, could use Patient object
                    {
                        patientname = reader.GetString(1);
                    }
                    if (reader.IsDBNull(2) == false)
                    {
                        dob = reader.GetString(2);
                    }
                    if (reader.IsDBNull(3) == false)
                    {
                        gender = reader.GetString(3);
                    }
                    if (reader.IsDBNull(4) == false)
                    {
                        apptDate = reader.GetString(4);
                    }
                    if (reader.IsDBNull(5) == false)
                    {
                        apptTime = reader.GetString(5);
                    }
                }
                reader.Close();
            }

            foreach (InterfaceInstance iface in interfaces)     // send document to each eligible interface, converting it as need be
            {
                if ((iface.InterfaceId > 0) && (iface.InterfaceType == "PACS"))     // no interface?  not a PACS interface?  bye bye!
                {
                    // Ok!  We're sending this thing to PACS... go ahead and convert the PDF file to a DCM file...
                    // This will call into the DCMTK library to do their pdf2dcm, dcmodify (to set metadata), and storescu.  Add appropriate parameters to the DCM.

                    filename = filenameIn;      // reinit

                    // filter out oddball cases of calling this method with conflicting interface parameters versus the type of file passed in.  
                    // however, we WILL reconvert PDFs to an image upon demand, see below.
                    if (((filename.ToUpper().Contains(".JPG")) || (filename.ToUpper().Contains(".BMP"))) && (iface.PacsSubType == (int)pacsSubTypes.PDF))
                    {
                        Logger.Instance.WriteToLog("Send2PACS:  Attempted to send an image file to a PDF interface, interfaceID #" + iface.InterfaceId + ".  Request Aborted" + (apptId > 0 ? " for appt id " + apptId.ToString() + "." : "."));
                        continue;
                    }
                    if (iface.PacsSubType == (int)pacsSubTypes.JPG)    // jpg
                    {
                        // If we were sent a PDF file, yet it's pacsSubType is different, convert the document to an image
                        // this could happen depending on what type of file is generated from the survey, and the settings made in lkpInterfaceDefinitions
                        // as of now, we're not converting between jpgs and bmps... if you send a bmp and interface type is jpg, bail... and vice versa
                        if (filename.ToUpper().Contains(".BMP"))
                        {
                            Logger.Instance.WriteToLog("Send2PACS:  Attempted to send a BMP file to a JPG interface.  Request Aborted" + (apptId > 0 ? " for appt id " + apptId.ToString() + "." : "."));
                            continue;
                        }
                        if (filename.ToUpper().Contains(".PDF"))    // re-convert this html to an image instead
                        {
                            HtmlToImageConverter htmlToJpgConverter = new HtmlToImageConverter();
                            htmlToJpgConverter.LicenseKey = "sjwvPS4uPSskPSgzLT0uLDMsLzMkJCQk";
                            filename = filename.ToUpper().Replace(".PDF", "") + ".jpg";
                            htmlToJpgConverter.ConvertHtmlToFile(dt.htmlText, "", System.Drawing.Imaging.ImageFormat.Jpeg, filename);
                        }
                    }

                    if ((iface.PacsSubType == (int)pacsSubTypes.BMP) ||  (iface.PacsSubType == (int)pacsSubTypes.inverseBMP)) // bitmap
                    {
                        // If we were sent a PDF file, yet it's pacsSubType is different, convert the document to an image
                        // this could happen depending on what type of file is generated from the survey, and the settings made in lkpInterfaceDefinitions
                        // as of now, we're not converting between jpgs and bmps... if you send a bmp and interface type is jpg, bail... and vice versa
                        if (filename.ToUpper().Contains(".JPG"))
                        {
                            Logger.Instance.WriteToLog("Send2PACS:  Attempted to send a JPG file to a BMP interface.  Request Aborted" + (apptId > 0 ? " for appt id " + apptId.ToString() + "." : "."));
                            continue;
                        }
                        if (filename.ToUpper().Contains(".PDF"))     // re-convert this html to an image instead
                        {
                            HtmlToImageConverter htmlToJpgConverter = new HtmlToImageConverter();
                            htmlToJpgConverter.LicenseKey = "sjwvPS4uPSskPSgzLT0uLDMsLzMkJCQk";
                            filename = filename.ToUpper().Replace(".PDF", "") + ".bmp";
                            if (iface.PacsSubType == (int)pacsSubTypes.inverseBMP)
                            {
                                img = htmlToJpgConverter.ConvertHtmlToImageObject(dt.htmlText, "");
                            }
                            else
                            {
                                htmlToJpgConverter.ConvertHtmlToFile(dt.htmlText, "", System.Drawing.Imaging.ImageFormat.Bmp, filename);
                            }
                        }

                        // jdg 1/26/2016 invert bmp
                        if (iface.PacsSubType == (int)pacsSubTypes.inverseBMP)
                        {
                            InvertBitmap(filename, img);
                        }
                    }

                    args.Clear();

                    string dcmFilename = filename + ".dcm";

                    TripleArg arg1 = new TripleArg();
                    //arg1.ArgSwitch = filename;
                    arg1.ArgSwitch = "\"" + filename + "\"";
                    args.Add(arg1);

                    TripleArg arg2 = new TripleArg();
                    //arg2.ArgSwitch = dcmFilename;
                    arg2.ArgSwitch = "\"" + dcmFilename + "\"";
                    args.Add(arg2);

                    if ((patient != null) && (iface.PacsSubType == (int)pacsSubTypes.PDF)) // for some reason, img2dcm (jpg) doesn't have a patient identifier arg.  will have to insert this into the metadata later.
                    {
                        TripleArg arg3 = new TripleArg();
                        arg3.ArgSwitch = "+pi";
                        arg3.Name = patient.unitnum;
                        args.Add(arg3);
                    }

                    if ((iface.PacsSubType == (int)pacsSubTypes.BMP) || (iface.PacsSubType == (int)pacsSubTypes.inverseBMP))       // defaults to jpg
                    {
                        TripleArg arg4 = new TripleArg();
                        arg4.ArgSwitch = "-i";
                        arg4.Name = "BMP";
                        args.Add(arg4);
                    }

                    if ((iface.PacsSubType == (int)pacsSubTypes.JPG) || (iface.PacsSubType == (int)pacsSubTypes.BMP) || (iface.PacsSubType == (int)pacsSubTypes.inverseBMP))    // jpg, bmp
                    {
                        // do the conversion jpg --> dcm;  this is a local command, no ip/port needed for this one.
                        wrapDCMTKmethodAsExternalCommand(null, null, iface.DCMTKpath + "\\img2dcm.exe", args);
                    }
                    else
                    {
                        // do the conversion pdf --> dcm;  this is a local command, no ip/port needed for this one.
                        wrapDCMTKmethodAsExternalCommand(null, null, iface.DCMTKpath + "\\pdf2dcm.exe", args);
                    }
                    if (!File.Exists(dcmFilename))
                    {
                        Logger.Instance.WriteToLog("Send2PACS: Unable to convert JPG or PDF to DCM OR perhaps just unable to write it to disk.  Check lkp_AutomationDocuments, lkpInterfaceDefinitions, document storage location and DICOM binary library (" + iface.DCMTKpath + ") for existence and/or permissions.  Filename: " + dcmFilename + ".");
                        continue;        // something went wrong, but didn't throw an error...
                    }

                    // I'm guessing that we'll be adding a study/series ID later, just add another TripleArg here, if you didn't build the DCM with it in the metadata initially
                    // If you need to fetch the UID, here is a sample of what a findscu would look like... don't forget the -aec and -aet... 
                    // findscu localhost 5678 -S -k StudyInstanceUID -k QueryRetrieveLevel=STUDY -k AccessionNumber=22222893 -aet Hughes -aec Hughes2
                    // and of course, you have to read the resulting array list. 

                    args.Clear();

                    TripleArg modArg0 = new TripleArg();   // dcm file name
                    modArg0.ArgSwitch = "\"" + dcmFilename + "\"";
                    args.Add(modArg0);

                    if (!String.IsNullOrEmpty(accessionNumber))
                    {
                        TripleArg modArg1 = new TripleArg();   // accession number
                        modArg1.ArgSwitch = "-m";
                        modArg1.Name = "AccessionNumber";    // DICOM literal string, do not modify
                        modArg1.Value = accessionNumber;
                        args.Add(modArg1);
                    }
                    else
                    {
                        // make a note of this circumstance, it's especially critical for FMH
                        Logger.Instance.WriteToLog("Send2PACS:  Accession number is not set for Interface #" + iface.InterfaceId + " for appt # : " + apptId.ToString());
                    }

                    TripleArg modArg2 = new TripleArg();   // Modality hard-coded to "OT".  May need to be parameterized later.
                    modArg2.ArgSwitch = "-i";
                    modArg2.Name = "Modality";  // DICOM literal string, do not modify
                    modArg2.Value = iface.Modality;     //  "OT";   no longer hard-coded jdg 1/21/16

                    args.Add(modArg2);

                    TripleArg modArg3 = new TripleArg();   // Patient Name
                    modArg3.ArgSwitch = "-m";
                    modArg3.Name = "PatientName";   // DICOM literal string, do not modify
                    patientname = patientname.Replace(",  ", ",");   // hack for extra space in name... 
                    patientname = patientname.Replace(", ", ",");   // hack for extra space in name... 
                    modArg3.Value = "\"" + patientname.Replace(",", "^") + "\"";
                    args.Add(modArg3);

                    try
                    {
                        if (!String.IsNullOrEmpty(dob))
                        {
                            dob = DateTime.ParseExact(dob, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");    // FMH preferred format... DICOM Standard??
                        }
                    }
                    catch (Exception) { }   // else use dob as found in database

                    TripleArg modArg4 = new TripleArg();   // Date of Birth
                    modArg4.ArgSwitch = "-m";
                    modArg4.Name = "PatientBirthDate";  // DICOM literal string, do not modify
                    modArg4.Value = dob;
                    args.Add(modArg4);

                    TripleArg modArg5 = new TripleArg();   // Gender
                    modArg5.ArgSwitch = "-m";
                    modArg5.Name = "PatientSex";    // DICOM literal string, do not modify
                    modArg5.Value = gender;
                    args.Add(modArg5);

                    try
                    {
                        if (!String.IsNullOrEmpty(apptDate))
                        {
                            apptDate = DateTime.ParseExact(apptDate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("yyyyMMdd");
                        }
                    }
                    catch (Exception) { }   // else use apptDate as found in database

                    TripleArg modArg6 = new TripleArg();   // Study Date
                    modArg6.ArgSwitch = "-m";
                    modArg6.Name = "StudyDate"; // DICOM literal string, do not modify
                    modArg6.Value = apptDate;
                    args.Add(modArg6);

                    if ((iface.PacsSubType == (int)pacsSubTypes.JPG) || (iface.PacsSubType == (int)pacsSubTypes.BMP))  // pdf already has this in the metadata; img2dcm won't take patient id argument like pdf2dcm, for reasons that escape me
                    {
                        TripleArg modArg7 = new TripleArg();   // MRN
                        modArg7.ArgSwitch = "-m";
                        modArg7.Name = "PatientID"; // DICOM literal string, do not modify
                        modArg7.Value = patient.unitnum;
                        args.Add(modArg7);
                    }

                    try
                    {
                        if (!String.IsNullOrEmpty(apptTime))
                        {
                            apptTime = DateTime.ParseExact(apptTime, "hh:mm tt", CultureInfo.InvariantCulture).ToString("hhmm") + "00";
                            TripleArg modArg8 = new TripleArg();   // Date of Birth
                            modArg8.ArgSwitch = "-m";
                            modArg8.Name = "StudyTime"; // DICOM literal string, do not modify
                            modArg8.Value = apptTime;
                            args.Add(modArg8);
                        }
                    }
                    catch (Exception) { }   // no time?  no big deal.

                    TripleArg modArg9 = new TripleArg();   // Study ID
                    modArg9.ArgSwitch = "-m";
                    modArg9.Name = "StudyID"; // DICOM literal string, do not modify
                    //modArg9.Value = "1";
                    modArg9.Value = iface.InterfaceId.ToString();
                    args.Add(modArg9);

                    TripleArg modArg10 = new TripleArg();   // Series ID
                    modArg10.ArgSwitch = "-m";
                    modArg10.Name = "SeriesNumber"; // DICOM literal string, do not modify
                    //modArg10.Value = "1";
                    modArg10.Value = iface.InterfaceId.ToString();
                    args.Add(modArg10);

                    TripleArg modArg11 = new TripleArg();   // Instance
                    modArg11.ArgSwitch = "-m";
                    modArg11.Name = "InstanceNumber"; // DICOM literal string, do not modify
                    //modArg11.Value = "1";
                    modArg11.Value = iface.InterfaceId.ToString();
                    args.Add(modArg11);

                    wrapDCMTKmethodAsExternalCommand(null, null, iface.DCMTKpath + "\\dcmodify.exe", args);

                    args.Clear();

                    TripleArg sendArg1 = new TripleArg();   // file to xmit
                    sendArg1.ArgSwitch = "\"" + dcmFilename + "\"";
                    args.Add(sendArg1);

                    TripleArg sendArg2 = new TripleArg();   // ae title HRA
                    sendArg2.ArgSwitch = "-aet";
                    sendArg2.Name = iface.AeTitleLocal;
                    args.Add(sendArg2);

                    TripleArg sendArg3 = new TripleArg();   // ae title remote
                    sendArg3.ArgSwitch = "-aec";
                    sendArg3.Name = iface.AeTitleRemote;
                    args.Add(sendArg3);

                    if (!String.IsNullOrEmpty(iface.AdditionalParams))
                    {
                        TripleArg sendArg4 = new TripleArg();   // additional params
                        sendArg4.ArgSwitch = iface.AdditionalParams;
                        args.Add(sendArg4);
                    }

                    // Send it out... finally!
                    dicomOutputArgs = wrapDCMTKmethodAsExternalCommand(iface.IpAddress, iface.Port, iface.DCMTKpath + "\\storescu.exe", args);
                    // todo: parse out any useful returned values... in the meantime, unless an error is thrown we'll return success
                    Logger.Instance.WriteToLog("Send2PACS: Successful send to " + iface.AeTitleRemote + " for Interface #" + iface.InterfaceId + " " + (apptId > 0 ? " for appt id " + apptId.ToString() : ""));

                    try
                    {
                        File.Delete(dcmFilename);   // no point in keeping these around.
                        File.Delete(dcmFilename + ".bak");
                        File.Delete(filename);       
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.WriteToLog("Send2PACS: Unable to delete " + dcmFilename + ", underlying error was " + e.Message);
                    }
                }
            }

            // if some interfaces were processed, return true.  
            if (interfaces.Count > 0) return true;
            else return false;
        }

        private static void InvertBitmap(string filename, Image img)
        {
            // jdg 1/26/16
            if(filename.ToUpper().Contains(".BMP"))
            {
                //Bitmap temp = new Bitmap(filename, false);
                Bitmap temp = new Bitmap(img);
                Bitmap bmap = (Bitmap)temp.Clone();

                Color c;
                for (int i = 0; i < bmap.Width; i++)
                {
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        bmap.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                    }
                }

                temp.Dispose();

                bmap.Save(filename, ImageFormat.Bmp);

                bmap.Dispose();
            }
        }

        internal static void send2Penrad(Model.PatientRecord.Patient patient, int templateId, string saveLocation, string strServiceBinding, string toolsPath, int apptid)
        {
            XmlDocument hl7FHData = new XmlDocument();
            try
            {
                //Serialize the FH
                patient.LoadFullObject(); //needed to ensure patient object is complete, including ObGynHx, etc.
                RiskApps3.Model.PatientRecord.FHx.FamilyHistory theFH = patient.owningFHx;
                string fhAsString = TransformUtils.DataContractSerializeObject<RiskApps3.Model.PatientRecord.FHx.FamilyHistory>(theFH);

                //transform it
                XmlDocument inDOM = new XmlDocument();
                inDOM.LoadXml(fhAsString);
                if (String.IsNullOrEmpty(toolsPath))
                {
                    toolsPath = RiskApps3.Utilities.Configurator.getNodeValue("Globals", "ToolsPath"); // @"C:\Program Files\riskappsv2\tools\";
                }
                if (String.IsNullOrEmpty(strServiceBinding))
                {
                    strServiceBinding = "WSHttpBinding_IService1";
                }
                XmlDocument resultXmlDoc = TransformUtils.performTransform(inDOM, toolsPath, @"hra_to_ccd_remove_namespaces.xsl");
                hl7FHData = TransformUtils.performTransformWith2Params(resultXmlDoc, toolsPath, @"hra_serialized_to_hl7.xsl", "dcisAsCancer", "1", "deIdentify", "0");
                XmlDocument riskAndPenrad = TransformUtils.performTransformWith2Params(resultXmlDoc, toolsPath, @"hra_serialized_to_RISK.xsl", "dcisAsCancer", "1", "deIdentify", "0");
                hl7FHData.SelectSingleNode("//FamilyHistory").AppendChild(hl7FHData.ImportNode(riskAndPenrad.SelectSingleNode("FamilyHistory/risk"), true));
                hl7FHData.SelectSingleNode("//FamilyHistory").AppendChild(hl7FHData.ImportNode(riskAndPenrad.SelectSingleNode("FamilyHistory/Penrad"), true));
                hl7FHData.Save(saveLocation + "\\" + apptid.ToString() + ".xml"); // todo:  figure out a naming convention for this file
            }
            catch (Exception eFH)
            {
                Logger.Instance.WriteToLog("AUTOMATION:  Unable to write Family History with Penrad and Risk Data for appt " + apptid.ToString() + "; Underlying error = " + eFH.ToString() + ".  See hra_serialized_to_RISK.xsl. InnerXML = " + hl7FHData.InnerXml);
            }
        }

        /// <summary>
        /// Find an interface specific to fetching appointments from a DICOM Modality Worklist (MWL) and get the appointments into the database.
        /// effective 1/22/16 the values required to be present in lkpInterfaceDefinitions are as follows:
        /// interfaceType = "ApptMWL"
        /// ipAddress, port self explanatory
        /// stringParam1: "their" AET
        /// stringParam2: "our" AET
        /// stringParam3: qualified path to DCMTK binaries, e.g., C:\HUGHES_DCMTK_DICOM\bin
        /// intParam1: (optional) number of minutes BEFORE current time to bound the MWL query
        /// intParam2: (optional) number of minutes AFTER current time to bound the MWL query.  
        ///             Summary: (CurrentTime - intParam1) - Current Time - (Current Time + intParam2).  DEFAULTS to whole day.
        /// stringParam4: Modality to send to MWL.  Defaults to "MG".
        /// stringParam5: Name of DICOM field that contains the ClinicCode (mapping to lkpClinics.ClinicCode).  E.g., ScheduledStationName
        /// </summary>
        /// <param name="clinicID">string representation of clinicID, key to lkpClinics</param>
        /// <returns>true if succeeds, false otherwise</returns>
        public static bool updateApptListFromMWL(string clinicID)
        {
            System.Threading.Mutex appMutex = new System.Threading.Mutex(false, "MWLApptFeed");
            // prevent concurrent MWL fetches.  Just exit if we don't get the mutex, as someone else is updating the same list of appointments.  jdg 1/15/16
            if (appMutex.WaitOne(100, false))
            {
                try
                {
                    bool bSuccessInsertAppt = false;
                    // Probably will just be one appt interface, but why assume?  Let's support multiple ApptMWL's.  
                    // I can imagine updating for selected clinic(s) or all clinics.
                    List<InterfaceInstance> interfaces = new List<InterfaceInstance>();
                    SqlDataReader reader = BCDB2.Instance.ExecuteReader("EXEC sp_getInterfaceDefinitionForApptMWL");
                    while (reader.Read())
                    {
                        InterfaceInstance instance = new InterfaceInstance();

                        if (reader.IsDBNull(0) == false)
                        {
                            instance.InterfaceId = reader.GetInt32(0);
                        }
                        if (reader.IsDBNull(1) == false)
                        {
                            instance.InterfaceType = reader.GetString(1);
                        }
                        if (reader.IsDBNull(2) == false)
                        {
                            instance.IpAddress = reader.GetString(2);
                        }
                        if (reader.IsDBNull(3) == false)
                        {
                            instance.Port = reader.GetString(3);
                        }
                        if (reader.IsDBNull(4) == false)
                        {
                            instance.AeTitleRemote = reader.GetString(4);
                        }
                        if (reader.IsDBNull(5) == false)
                        {
                            instance.AeTitleLocal = reader.GetString(5);
                        }
                        if (reader.IsDBNull(6) == false)
                        {
                            instance.DCMTKpath = reader.GetString(6);
                        }
                        if (reader.IsDBNull(7) == false)
                        {
                            instance.TimeSpanFrom = reader.GetInt32(7);
                        }

                        if (reader.IsDBNull(8) == false)
                        {
                            instance.TimeSpanTo = reader.GetInt32(8);
                        }

                        if (reader.IsDBNull(9) == false)
                        {
                            instance.Modality = reader.GetString(9);
                        }

                        if (reader.IsDBNull(10) == false)
                        {
                            instance.ClinicFieldInMWLQuery = reader.GetString(10);
                        }

                        if (instance.InterfaceId > 0)
                        {
                            interfaces.Add(instance);   // multiple appointment sources per query are supported.  Who knows?  Maybe this will be useful someday.
                        }

                    }

                    foreach (InterfaceInstance iface in interfaces)     // send request to each eligible interface
                    {
                        List<TripleArg> args = new List<TripleArg>();   // for want of a tuple which doesn't exist till .NET 4.0
                        List<KeyValuePair<string, string>> dicomOutputArgs = new List<KeyValuePair<string, string>>();

                        TripleArg sendArg1 = new TripleArg();   // file to xmit
                        sendArg1.ArgSwitch = "-W";
                        args.Add(sendArg1);

                        TripleArg sendArg2 = new TripleArg();   // ae title HRA
                        sendArg2.ArgSwitch = "-aet";
                        sendArg2.Name = iface.AeTitleLocal;
                        args.Add(sendArg2);

                        TripleArg sendArg3 = new TripleArg();   // ae title remote
                        sendArg3.ArgSwitch = "-aec";
                        sendArg3.Name = iface.AeTitleRemote;
                        args.Add(sendArg3);

                        TripleArg sendArg5 = new TripleArg();
                        sendArg5.ArgSwitch = "-k";
                        sendArg5.Name = "(0010,0010)";      // "PatientName";
                        args.Add(sendArg5);

                        TripleArg sendArg6 = new TripleArg();
                        sendArg6.ArgSwitch = "-k";
                        sendArg6.Name = "(0010,0020)";      // "PatientID";
                        args.Add(sendArg6);

                        TripleArg sendArg7 = new TripleArg();
                        sendArg7.ArgSwitch = "-k";
                        sendArg7.Name = "(0008,0092)";      // "ReferringPhysicianAddress", hijacked by NSMC for referring provider id
                        args.Add(sendArg7);

                        TripleArg sendArg8 = new TripleArg();
                        sendArg8.ArgSwitch = "-k";
                        sendArg8.Name = "(0008,0094)";  // "ReferringPhysicianTelephoneNumber", hijacked by NSMC for pcp id
                        args.Add(sendArg8);

                        TripleArg sendArg12 = new TripleArg();
                        sendArg12.ArgSwitch = "-k";
                        sendArg12.Name = "(0040,0100)[0].Modality";
                        sendArg12.Value = iface.Modality;           // no long hardcoded "MG";
                        args.Add(sendArg12);

                        TripleArg sendArg4 = new TripleArg();
                        sendArg4.ArgSwitch = "-k";
                        sendArg4.Name = "(0008,0050)";      // "AccessionNumber"
                        args.Add(sendArg4);

                        TripleArg sendArg9 = new TripleArg();
                        sendArg9.ArgSwitch = "-k";
                        sendArg9.Name = "(0040,0100)[0].ScheduledProcedureStepStartDate";        //"StudyDate";
                        sendArg9.Value = DateTime.Now.ToString("yyyyMMdd");
                        args.Add(sendArg9);

                        TripleArg sendArg10 = new TripleArg();
                        sendArg10.ArgSwitch = "-k";
                        sendArg10.Name = "(0040,0100)[0].ScheduledProcedureStepStartTime";      // "StudyTime";
                        // NOTE NOTE NOTE: jdg 1/14/16 you can't do a date range on a -W query only a -S... according to McKesson.  Maybe other systems will be more DICOM compliant, 
                        // so I'll leave this code here.  In any case, if you don't specify the two intParams it's moot anyway.  

                        // STUDY TIME RANGE jdg 1/6/15 Use lkpInterfaceDefinitions entry to supply in intParam1 and 2 the number of minutes BEFORE the current time to query 
                        // AND the number of minutes AFTER the current time to query.
                        if ((iface.TimeSpanFrom >= 0) && (iface.TimeSpanTo > 0))    // no negative values, suckers.  and no null ranges.
                        {
                            TimeSpan fromSpan = new TimeSpan(0, iface.TimeSpanFrom, 0);
                            TimeSpan toSpan = new TimeSpan(0, iface.TimeSpanTo, 0);
                            DateTime timeFrom = DateTime.Now - fromSpan;
                            DateTime timeTo = DateTime.Now + toSpan;
                            string strTimeFrom = timeFrom.ToString("HHmmss");
                            string strTimeTo = timeTo.ToString("HHmmss");
                            sendArg10.Value = strTimeFrom + "-" + strTimeTo;
                            //Logger.Instance.WriteToLog("MWL: time span from = " + iface.TimeSpanFrom + ", to = " + iface.TimeSpanTo + ", date range = " + strTimeFrom + "-" + strTimeTo);
                        }
                        // end jdg 1/6/15
                        args.Add(sendArg10);

                        TripleArg sendArg11 = new TripleArg();
                        sendArg11.ArgSwitch = "-k";
                        sendArg11.Name = "(0010,0030)";     // "PatientBirthDate"
                        args.Add(sendArg11);

                        TripleArg sendArg13 = new TripleArg();
                        sendArg13.ArgSwitch = "-k";
                        sendArg13.Name = "(0010,0040)";     // "PatientSex"
                        args.Add(sendArg13);

                        try
                        {
                            dicomOutputArgs = wrapDCMTKmethodAsExternalCommand(iface.IpAddress, iface.Port, iface.DCMTKpath + "\\findscu.exe", args);

                            bSuccessInsertAppt = parseAndInsertAppointments(dicomOutputArgs, iface);
                        }
                        catch (Exception e3)
                        {
                            RiskApps3.Utilities.Logger.Instance.WriteToLog("MWL Appt Feed FAILED for interface # " + iface.InterfaceId.ToString() + "; underlying error was: " + e3.ToString());
                        }

                    }
                }
                finally
                {
                    appMutex.ReleaseMutex();
                }


            }   // end mutex

            return true;
        }

        /// <summary>
        /// Takes aggregated list of DICOM generated studies, and imports it like any other appointment feed
        /// </summary>
        /// <param name="kvp">Output from DICOM Modality Worklist query.</param>
        /// <returns></returns>
        public static bool parseAndInsertAppointments(List<KeyValuePair<string, string>> kvp, InterfaceInstance iface)
        {
            string sqlStr;
            SqlDataReader reader;

            if ((kvp == null) || (kvp.Count == 0))
            {
                Logger.Instance.WriteToLog("parseAndInsertAppointments in interfaceUtils... NULL or EMPTY arglist in.");
                return false;
            }

            // now process the output.... StudyDate is the first item in each returned study.  List contains zero to infinity appointments.
            List<ParameterCollection> paramCollection = new List<ParameterCollection>();
            ParameterCollection pc = new ParameterCollection();
            string sDate;

            // these two variables used in mapping clinics, see note below.
            int clinicID = 0;   // default to clinic 0... will be changed below
            string defaultAssessmentType = "";

            // emerge from this loop with a collection of parameterCollections, each representing one appointment.  
            // "StudyDate" is the delimiting field... when that comes up, cleave off a new param collection.
            foreach (KeyValuePair<string, string> item in kvp)
            {
                switch (item.Key.ToUpper())
                {
                    case "ACCESSIONNUMBER":
                        // this is the delimiting field.  start a new param collection now.
                        if (pc.getKeys().Count > 0)     // first time through?  don't add to collection...
                        {
                            paramCollection.Add(pc);
                            pc = new ParameterCollection();
                        }
                        pc.Add("referral", item.Value);

                        break;
                    case "SCHEDULEDPROCEDURESTEPSTARTDATE":
                        // CONVERT to mm/dd/yyyy
                        sDate = item.Value.Substring(4, 2) + "/" + item.Value.Substring(6, 2) + "/" + item.Value.Substring(0, 4);
                        pc.Add("apptdate", sDate);
                        break;
                    case "SCHEDULEDPROCEDURESTEPSTARTTIME":
                        pc.Add("appttime", item.Value.Substring(0, 2) + ":" + item.Value.Substring(2, 2));
                        break;

                    // use these ids to key into lkpProviders for the localId (?) and set displayName in the appt, upsert into tblApptProviders...
                    // that logic is done in sp_createApptFromSchedService once the new appt id is created.
                    case "REFERRINGPHYSICIANADDRESS":   // note:  at NSMC, this contains referring phys ID!
                        pc.Add("refPhysLocalID", item.Value);
                        break;
                    case "REFERRINGPHYSICIANTELEPHONENUMBERS":  // note: at NSMC, this contains pcp ID!
                        pc.Add("pcpLocalID", item.Value);
                        break;
                    case "PATIENTNAME":
                        // Break into @firstName and @lastName
                        string[] nameParts = item.Value.Split(new char[] { ',', '^' });

                        if (nameParts.Length > 0)
                        {
                            pc.Add("lastname", nameParts[0]);
                            pc.Add("firstname", nameParts[1]);
                        }

                        break;
                    case "PATIENTID":
                        pc.Add("unitnum", item.Value);
                        break;
                    case "PATIENTBIRTHDATE":
                        sDate = item.Value.Substring(4, 2) + "/" + item.Value.Substring(6, 2) + "/" + item.Value.Substring(0, 4);
                        pc.Add("dob", sDate);
                        break;
                    case "PATIENTSEX":
                        pc.Add("gender", item.Value);
                        break;

                    case "SCHEDULEDPERFORMINGPHYSICIANNAME":
                        pc.Add("apptphysname", item.Value);
                        break;

                    default:
                        // turns out that different systems put the clinic name in different fields... it has now been
                        // parameterized in lkpInterfaceDefinitions and InterfaceInstance... deal with it here in the default case.  
                        // defaults to SCHEDULEDSTATIONNAME in InterfaceInstance for historical reasons (NSMC, our first MWL customer, put it there.) jdg 1/22/16
                        if (item.Key.ToUpper() == iface.ClinicFieldInMWLQuery.ToUpper())
                        {
                            sqlStr = "select clinicID, defaultAssessmentType from lkpClinics where clinicCode='" + item.Value.Trim() + "'";
                            reader = BCDB2.Instance.ExecuteReader(sqlStr);
                            if (reader != null)
                            {
                                while (reader.Read())
                                {
                                    if (reader.IsDBNull(0) == false)
                                    {
                                        clinicID = reader.GetInt32(0);
                                    }
                                    if (reader.IsDBNull(1) == false)
                                    {
                                        defaultAssessmentType = reader.GetString(1);
                                    }
                                }
                            }
                            if (clinicID == 0)
                            {
                                RiskApps3.Utilities.Logger.Instance.WriteToLog("Failed to read the clinicId... Are you connected to the database??  Is lkpClinics set up correctly with the submitted clinicCode " + item.Value + "??");
                            }
                            else
                            {
                                pc.Add("defaultScreenType", defaultAssessmentType);
                                pc.Add("clinicId", clinicID);
                            }

                        }
                        break;


                }   // end switch

            }   // end foreach

            if (pc.getKeys().Count > 0)     // last time through?  add to collection if anything there...
            {
                paramCollection.Add(pc);
            }

            // do we have some appointments?  Attempt to insert them into the database.

            foreach (ParameterCollection individualAppt in paramCollection)
            {
                object newApptIdObj = BCDB2.Instance.ExecuteSpWithRetValAndParams("sp_createApptFromSchedService", SqlDbType.Int, individualAppt);
                int apptIdInt = (int)newApptIdObj;
                if (apptIdInt > 0)
                {
                    RiskApps3.Utilities.Logger.Instance.WriteToLog("MWL Appt Feed, successfully added appt # " + apptIdInt.ToString());
                }
            }

            return true;
        }
    }
}

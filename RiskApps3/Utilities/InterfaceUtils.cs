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

namespace RiskApps3.Utilities
{
    public enum pacsSubTypes : int { PDF=1,JPG,BMP };   

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
            catch (Exception e) {
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
                } catch (Exception e) {
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
                segments = new string[] {"MSH", "PID", "PV1", "OBR", "OBX"};        // stick in some defaults... is this a good idea? 
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
                foreach(TripleArg ta in args) 
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
            //int interfaceId = -1;
            //string interfaceType = "";
            //string ipAddress = "";
            //string port = "";
            //string aeTitleRemote = "";
            //string aeTitleLocal = "";
            //string DCMTKpath = "";
            string accessionNumber = "";
            string patientname = "";
            string dob = "";
            string gender = "";
            string apptDate = "";
            string apptTime = "";
            //int pacsSubType = (int)pacsSubTypes.PDF;    // NEW jdg 8/7/15... which type of encapsulated DCM to generate?  Default to PDF... NOW 2 ==> JPG
            ParameterCollection pacsArgs = new ParameterCollection();
            pacsArgs.Add("documentTemplateID", dt.documentTemplateID);
            string filename;

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

                if(instance.InterfaceId > 0)
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
                    if ( ((filename.ToUpper().Contains(".JPG")) || (filename.ToUpper().Contains(".BMP"))) &&  (iface.PacsSubType == (int)pacsSubTypes.PDF))
                    {
                        Logger.Instance.WriteToLog("Send2PACS:  Attempted to send an image file to a PDF interface, interfaceID #" + iface.InterfaceId + ".  Request Aborted" + (apptId > 0 ? " for appt id " + apptId.ToString() + "." : "."));
                        continue;
                    }
                    if (iface.PacsSubType == (int)pacsSubTypes.JPG)    // jpg
                    {
                        // If we were sent a PDF file, yet it's pacsSubType is different, convert the document to an image
                        // this could happen depending on what type of file is generated from the survey, and the settings made in lkpInterfaceDefinitions
                        // as of now, we're not converting between jpgs and bmps... if you send a bmp and interface type is jpg, bail... and vice versa
                        if(filename.ToUpper().Contains(".BMP"))
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

                    if (iface.PacsSubType == (int)pacsSubTypes.BMP)    // bitmap
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
                            htmlToJpgConverter.ConvertHtmlToFile(dt.htmlText, "", System.Drawing.Imaging.ImageFormat.Bmp, filename);
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

                    if (iface.PacsSubType == (int)pacsSubTypes.BMP) // defaults to jpg
                    {
                        TripleArg arg4 = new TripleArg();
                        arg4.ArgSwitch = "-i";
                        arg4.Name = "BMP";
                        args.Add(arg4);
                    }

                    if ((iface.PacsSubType == (int)pacsSubTypes.JPG) || (iface.PacsSubType == (int)pacsSubTypes.BMP))    // jpg, bmp
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
                        Logger.Instance.WriteToLog("Send2PACS:  Accession number is not set for Interface #" + iface.InterfaceId + " for appt # : " + apptId.ToString() );
                    }

                    TripleArg modArg2 = new TripleArg();   // Modality hard-coded to "OT".  May need to be parameterized later.
                    modArg2.ArgSwitch = "-i";
                    modArg2.Name = "Modality";  // DICOM literal string, do not modify
                    modArg2.Value = "OT";
                    args.Add(modArg2);

                    TripleArg modArg3 = new TripleArg();   // Patient Name
                    modArg3.ArgSwitch = "-m";
                    modArg3.Name = "PatientName";   // DICOM literal string, do not modify
                    patientname = patientname.Replace(",  ", ",");   // hack for extra space in name... 
                    patientname = patientname.Replace(", ", ",");   // hack for extra space in name... 
                    modArg3.Value = patientname.Replace(",", "^");
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
    }
}

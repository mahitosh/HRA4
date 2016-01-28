using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace RiskApps3.Utilities
{
    public class Logger
    {
        //////////////////////////////////
        //  The singleton instance
        private static Logger instance;
        private static bool write_to_file = true;
        private string err = "";
        private string path = "";

        /**********************************************************************/
        //
        //  Logger()
        //
        //  This is the default constructor
        //
        //
        //
        private Logger()
        {
            NameValueCollection values = Configurator.GetConfig("AppSettings");
            if (values != null)
            {
                path = values["LogFile"];
            }
        }

        public void SetFilePath(string LogPath)
        {
            path = LogPath;
        }
        /**********************************************************************/
        //
        //  Instance()
        //
        //
        //
        //
        public static Logger Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Logger();
                }
                return instance;
            }
        }

        // Declaring the ReaderWriterLock at the class level 
        // makes it visible to all threads. 
        static ReaderWriterLock rwl = new ReaderWriterLock();
        // The shared resource protected by the 
        // ReaderWriterLock is a FileStream. 
        static FileStream fsLockable = null;

        static ReaderWriterLock rwl2 = new ReaderWriterLock();
        static FileStream fsLockable2 = null;

        /**********************************************************************/
        //
        //
        //
        //
        public static void SetWriteMode(bool file_based)
        {
            write_to_file = file_based;
        }

        /**********************************************************************/
        //
        //
        //
        //
        public string GetError()
        {
            return instance.err;
        }

        /**********************************************************************/
        //
        //
        //
        //
        public void ClearError()
        {
            instance.err = "";
        }

        /**********************************************************************/
        //
        //
        //
        //
        public int WriteToLog(string msg)
        {
            if (string.Compare("eventviewer", path, true) == 0)
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "HRA";
                    eventLog.WriteEntry(msg, EventLogEntryType.Information, 101, 1);
                }
                return 0;
            }
            else
            {
                if (write_to_file)
                {
                    //NameValueCollection values = Configurator.GetConfig("AppSettings");
                    //string path = values["LogFile"];
                    //string path = ConfigurationSettings.AppSettings["LogFile"];
                    //FileStream fs = null;

                    try
                    {
                        rwl.AcquireWriterLock(50);  //wait up to 50 milliseconds to attempt to acquire the lock
                        try
                        {
                            // It is safe for this thread to read or write 
                            // from the shared resource.

                            fsLockable = File.Open(path,
                                           FileMode.OpenOrCreate,
                                           FileAccess.Write,
                                           FileShare.None);


                            writeString(DateTime.Now + " : " + msg, fsLockable);

                            fsLockable.Close();
                        }
                        finally
                        {
                            // Ensure that the lock is released.
                            rwl.ReleaseWriterLock();
                        }
                    }
                    catch
                    {
                        return -1;
                    }
                    return 0;
                }
                else
                {
                    lock (instance.err)
                    {
                        instance.err += msg + Environment.NewLine;
                    }
                    return 0;
                }
            }
        }
        public int DebugToLog(string msg)
        {
            if (write_to_file)
            {
                NameValueCollection values = Configurator.GetConfig("AppSettings");
                string path = values["DebugLogFile"];
                //FileStream fs = null;

                try
                {
                    rwl2.AcquireWriterLock(50);  //wait up to 50 milliseconds to attempt to acquire the lock
                    try
                    {
                        // It is safe for this thread to read or write 
                        // from the shared resource.
                        fsLockable2 = File.Open(path,
                                       FileMode.OpenOrCreate,
                                       FileAccess.Write,
                                       FileShare.None);


                        writeString(DateTime.Now + " : " + msg, fsLockable2);

                        fsLockable2.Close();
                    }
                    finally
                    {
                        // Ensure that the lock is released.
                        rwl2.ReleaseWriterLock();
                    }
                }
                catch
                {
                    return -1;
                }
                return 0;
            }
            else
            {
                instance.err = msg;
                return 0;
            }
        }

        /**********************************************************************/
        //
        //  writeString()
        //
        //
        //
        //
        private void writeString(string s, FileStream fs)
        {
            Byte[] info;
            if (s != null)
            {
                fs.Seek(0, SeekOrigin.End);
                info = new UTF8Encoding(true).GetBytes(s);
                fs.Write(info, 0, info.Length);
                fs.WriteByte(13);
                fs.WriteByte(10);
            }
        }
    }
}
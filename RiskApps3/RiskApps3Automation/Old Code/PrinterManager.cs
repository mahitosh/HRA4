using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Management; // ??
using System.Printing;

using System.Drawing.Printing;

using RiskApps3.Utilities;

// Resetting the default printer needs probably elevated permissions.
// Obsolete permissions interface; needs migration to .Net 4.0+.
//
//using System.Security.Permissions;
//[assembly: PermissionSetAttribute(SecurityAction.RequestMinimum, Name = "FullTrust")]
//[assembly: PrintingPermission(SecurityAction.RequestMinimum)]

namespace RiskApps3Automation
{
    class PrinterManager
    {

        PrintDocument pd;
        LocalPrintServer ps;
        string defaultPrintQueue;

        public PrinterManager()
        {
            pd = new PrintDocument();
            ps = new LocalPrintServer();
            defaultPrintQueue = ps.DefaultPrintQueue.Name;
        }
        public string getDefaultPrintQueue()
        {
            return defaultPrintQueue;
        }
        // Need to change (and later restore) the default printer for SHDocVw.InternetExplorer.ExecWB.
        // Setting the default printer probably needs elevated permissions.
        //
        // Obsolete permissions interface; needs migration to .Net 4.0+.
        //
        public bool setDefaultPrintQueue(string pq)
        {
            try
            {
                // Obsolete permissions interface; needs migration to .Net 4.0+.
                //
                //PrintingPermission pp = new PrintingPermission(PrintingPermissionLevel.DefaultPrinting);
                //pp.Demand();

                ps.DefaultPrintQueue = new PrintQueue(ps, pq);
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("setDefaultPrintQueue() " + e);
                return false;
            }
            defaultPrintQueue = pq;
            return true;
        }        
    }
}

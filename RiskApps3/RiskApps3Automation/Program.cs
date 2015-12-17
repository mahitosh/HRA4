using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Timers;

using RiskApps3.Utilities;
using RiskApps3Automation;

namespace RiskApps3Automation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // If no arguments, run the form, but do not make it visible.
            //

            
            try
            {
                Application.Run(new Automation());
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("Automation Error\r\n" + e);
            }
            
            /*
            if (args.Length == 0)
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Automation3Form(true));
                }
                catch (Exception e)
                {
                    Logger.Instance.WriteToLog("RiskApps3Automation Error\r\n" + e);
                }
            }
            else if (args[0].ToLower() == "silent")
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Automation3Form(false));
                }
                catch (Exception e)
                {
                    Logger.Instance.WriteToLog("RiskApps3Automation Error\r\n" + e);
                }
            }
             */
        }        
    }
}

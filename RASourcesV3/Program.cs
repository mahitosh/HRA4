// #define international

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RiskAppCore;
using RiskApps3.Utilities;

#if international
using System.Globalization;
using System.Threading;
#endif


namespace RiskApps3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //TODO attribution: http://www.codeproject.com/Articles/100199/Smart-Hotkey-Handler-NET
            SmartHotKey.HotKey hotKeyManager = new SmartHotKey.HotKey();
            hotKeyManager.AddHotKey("Control+Shift+S");
            hotKeyManager.AddHotKey("Control+Shift+D");

            hotKeyManager.HotKeyPressed += new SmartHotKey.HotKey.HotKeyEventHandler(hotKeyManager_HotKeyPressed);

            RefreshTables.updateTables();
            
            ApplicationUtils.checkForUpdates("RiskApps3.exe");

            ApplicationUtils.checkFirstLogin();

#if international
            string region = RiskApps3.Utilities.Configurator.getNodeValue("globals", "CultureRegion");

            if (string.IsNullOrEmpty(region) == false)
            {
                CultureInfo culture = new CultureInfo(region);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;

            }
#endif
            //then if authenticate, show main form
            Application.Run(new MainForm());
        }

        static void hotKeyManager_HotKeyPressed(object sender, SmartHotKey.HotKeyEventArgs e)
        {
            switch (e.HotKey)
            {
                case "Control+Shift+S":
                    ToggleSaveLayoutOnClose(e);
                    return;
                case "Control+Shift+D":
                    ToggleDockingEnabled(e);
                    return;
                default:
                    return;
            }
        }

        private static void ToggleDockingEnabled(SmartHotKey.HotKeyEventArgs e)
        {
            RiskApps3.Controllers.SessionManager.Instance.AllowDockDragAndDrop = !RiskApps3.Controllers.SessionManager.Instance.AllowDockDragAndDrop;
            ShowToggleMessage(e, "AllowDockDragAndDrop", RiskApps3.Controllers.SessionManager.Instance.AllowDockDragAndDrop);
        }

        private static void ToggleSaveLayoutOnClose(SmartHotKey.HotKeyEventArgs e)
        {
            RiskApps3.Controllers.SessionManager.Instance.SaveLayoutOnClose = !RiskApps3.Controllers.SessionManager.Instance.SaveLayoutOnClose;
            ShowToggleMessage(e, "SaveLayoutOnClose", RiskApps3.Controllers.SessionManager.Instance.SaveLayoutOnClose);
        }

        private static void ShowToggleMessage(SmartHotKey.HotKeyEventArgs e, string variable, bool value)
        {
            MessageBox.Show(variable + " = " + value + ".\n\rToggle using " + e.HotKey + ".");
        }
    }
}

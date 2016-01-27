using System;
using System.Collections.Generic;

using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using RiskApps3.Utilities;

namespace RiskApps3.Controllers.Pedigree
{
    public class AnimationThread
    {
        BackgroundWorker animationThread = new BackgroundWorker();
        bool animate = true;
        public int frameRate = 35;

        public delegate bool PerFrameCallback();
        public void SpawnAnimationThread(PerFrameCallback callback)
        {
            int configFrameRate = frameRate;
            if (int.TryParse(Configurator.getNodeValue("globals", "PedigreeFrameRate"), out configFrameRate))
                frameRate = configFrameRate;

            animationThread.WorkerReportsProgress = true;
            animationThread.WorkerSupportsCancellation = true;
            animationThread.DoWork += new DoWorkEventHandler(delegate(object sender, DoWorkEventArgs e)
            {
                try
                {
                    while (animate && animationThread.CancellationPending==false)
                    {
                        //Thread.Sleep(100);
                        Thread.Sleep(frameRate);
                        animationThread.ReportProgress(0);
                    }
                }
                catch { }
            });
            animationThread.ProgressChanged += new ProgressChangedEventHandler(
                delegate(object sender, ProgressChangedEventArgs e) {
                    animate = callback(); 
                });
            animationThread.RunWorkerAsync();
        }

        internal void Shutdown()
        {
            animate = false;
            animationThread.CancelAsync();
        }
    }
}

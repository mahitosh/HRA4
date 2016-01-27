using System;
using System.Collections.Generic;

using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree
{
    /// <summary>
    /// A LayoutStep is a computation which incrementally enforces some
    /// conceptual layout constraint (mutates the model state incrementally).
    /// </summary>
    public delegate void LayoutStep();

    public class LayoutEngine
    {

        public bool LayoutComplete = false;


        private String mode;
        private Dictionary<String,List<LayoutStep>> layoutSteps = new Dictionary<string,List<LayoutStep>>();

        

        /// <summary>
        /// Increments the layout (mutates the model state) by
        /// executing all layout steps once in sequence.
        /// </summary>
        public void IncrementLayout()
        {
            foreach (LayoutStep layoutStep in layoutSteps[mode])
            {
                try
                {
                    layoutStep();
                }
                catch (Exception e)
                {
                    SetMode("MANUAL");
                }
            }
        }
        /// <summary>
        /// Adds a layout step to this layout engine.
        /// </summary>
        public void AddLayoutStep(String mode,LayoutStep layoutStep)
        {
            if (!layoutSteps.ContainsKey(mode))
                layoutSteps.Add(mode, new List<LayoutStep>());
            layoutSteps[mode].Add(layoutStep);
        }

        /// <summary>
        /// Sets the current mode, determining which layout steps are executed.
        /// </summary>
        public void SetMode(String mode)
        {
            this.mode = mode;
            
        }

        public string GetMode()
        {
            return mode;
        }

        public void Finished()
        {
            LayoutComplete = true;
        }
    }
}

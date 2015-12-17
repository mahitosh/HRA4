using System;
using System.Collections.Generic;
using System.Text;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Controllers.Pedigree.layoutSteps
{
    class AutoSave
    {
        public readonly LayoutStep step;
        public AutoSave(PedigreeModel model, PedigreeController layoutEngine)
        {
            step = delegate()
            {

                layoutEngine.Finished();

            };
        }
    }
}

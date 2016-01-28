using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotnetCHARTING.WinForms.Internal.Designer;
using RiskApps3.Model.Clinic;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    /// <summary>
    /// All providers in system.  Used 
    /// </summary>
    public class AllProviders : HRAList<Provider>
    {
        private readonly ParameterCollection _pc;
        private readonly object[] _constructorArgs;

        public AllProviders()
        {
            this._constructorArgs = new object [] {};
            _pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            LoadListArgs lla = new LoadListArgs("sp_3_LoadAllProviders", _pc, _constructorArgs);

            DoListLoad(lla);
        }
    }
}

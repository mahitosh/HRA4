using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;
using RiskApps3.Controllers;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using RiskApps3.Model.MetaData;
using RiskApps3.Model.PatientRecord.Pedigree;

namespace RiskApps3.Model.MetaData
{
    public class GlobalSettings : HraObject
    {
        [HraAttribute(auditable = false)] public int ID;
        [HraAttribute(auditable = false)] public string appName;
        [HraAttribute(auditable = false)] public string appVer;
        [HraAttribute(auditable = false)] public bool encryptPasswords;
        [HraAttribute(auditable = false)] public int passwordWarnDays;
        [HraAttribute(auditable = false)] public int passwordExpireDays;
        [HraAttribute(auditable = false)] public int passwordNoReuse;
        [HraAttribute(auditable = false)] public string signaturePath;
        [HraAttribute(auditable = false)] public string providerPhotosPath;
        [HraAttribute(auditable = false)] public string templatesPath;
        [HraAttribute(auditable = false)] public bool tabletLoginCaseSensitive;
        [HraAttribute(auditable = false)] public bool NCCN;


        public override void BackgroundLoadWork()
        {
            ParameterCollection p = new ParameterCollection();
            DoLoadWithSpAndParams("sp_3_LoadGlobals", p);
        }
    }
}
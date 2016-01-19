using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
    public class AuditReports
    {
        public AuditReports()
        {
            RSAuditMrnAccessV2Entry = new List<AuditMrnAccessV2Entry>();
            RSAuditMrnAccessV3Entry = new List<AuditMrnAccessV3Entry>();
        }
        public List<AuditMrnAccessV2Entry> RSAuditMrnAccessV2Entry { get; set; }
        public List<AuditMrnAccessV3Entry> RSAuditMrnAccessV3Entry { get; set; }

    }
    public class AuditMrnAccessV2Entry 
    {
        public DateTime? startTime { get; set; }
        public DateTime? endTime { get; set; }
        public string createdBy { get; set; }
        public string machineName { get; set; }
        public string MRN { get; set; }
        public int apptid { get; set; }
        public string message { get; set; }
        public string application { get; set; }
        public string table { get; set; }
        public string field { get; set; }
        public string fieldMeaning { get; set; }
        public string oldValue { get; set; }
        public string newValue { get; set; }
    }
    public class AuditMrnAccessV3Entry 
    {
        public DateTime? timestamp { get; set; }
        public string userName { get; set; }
        public string MRN { get; set; }
        public int apptID { get; set; }
        public string storedProcedure { get; set; }
        public string hraAttributeValueList { get; set; }
        public int rowsAffected { get; set; }
        public int relativeID { get; set; }
        public string hraObject { get; set; }
    }
}

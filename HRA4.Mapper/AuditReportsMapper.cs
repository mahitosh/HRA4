using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.ViewModels;
using RA = RiskApps3.Model.Clinic.Reports;
namespace HRA4.Mapper
{
    public static class AuditReportsMapper
    {
        public static AuditMrnAccessV2Entry ToAuditMrnAccessV2Entry(RA.AuditMrnAccessV2Entry AR) 
        {

            return new AuditMrnAccessV2Entry()
            {
                startTime = AR.startTime,
                endTime = AR.endTime,
                createdBy = AR.createdBy,
                machineName = AR.machineName,
                MRN = AR.unitnum,
                apptid = AR.apptid,
                message = AR.message,
                application = AR.application,
                table = AR.table,
                field = AR.field,
                fieldMeaning = AR.fieldMeaning,
                oldValue = AR.oldValue,
                newValue = AR.newValue

            };
            
        }
        //public static List<AuditMrnAccessV2Entry> ToAuditMrnAccessV2EntryList(this List<RA.AuditMrnAccessV2Entry> AR)
        //{
        //    List<AuditMrnAccessV2Entry> auditMrnAccessV2Entry = new List<AuditMrnAccessV2Entry>();
        //    foreach (var item in AR)
        //    {
        //        AuditMrnAccessV2Entry a = ToAuditMrnAccessV2Entry(item);
        //        auditMrnAccessV2Entry.Add(a);
        //    }
        //    return auditMrnAccessV2Entry;
        //}

        public static AuditMrnAccessV3Entry ToAuditMrnAccessV3Entry(RA.AuditMrnAccessV3Entry AR)
        {
            return new AuditMrnAccessV3Entry()
                {
                    timestamp = AR.timestamp,
                    userName = AR.userName,
                    MRN = AR.unitnum,
                    apptID = AR.apptID,
                    storedProcedure = AR.storedProcedure,
                    hraAttributeValueList = AR.hraAttributeValueList,
                    rowsAffected = AR.rowsAffected,
                    relativeID = AR.relativeID,
                    hraObject = AR.hraObject

                };
        }

    }
}

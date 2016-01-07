using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM = HRA4.ViewModels;
namespace HRA4.Services.Interfaces
{
    public interface IExportImportService
    {
        VM.HraXmlFile ExportAsXml(string mrn, int apptId, bool Identified);

        VM.HraXmlFile ExportAsHL7(string mrn, int apptId, bool Identified);

        void ImportXml(VM.HraXmlFile xmlFIle, string mrn, int apptId);
        void ImportHL7(VM.HraXmlFile xmlFIle, string mrn, int apptId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Entities;
using HRA4.ViewModels;
namespace HRA4.Services.Interfaces
{
    public interface ITemplateService
    {
        List<HtmlTemplate> GetTemplatesByInstitution(int institutionId);
        HtmlTemplate GetTemplate(int id);
        HtmlTemplate GetTemplateByRaTemplateId(int institutionId, int raTemplateId);
        HtmlTemplate InsertTemplate(HtmlTemplate template);
        int UpdateTemplate( int id, string TemplateString);
        TemplateList GetTemplates();
        Template GenerateHtmlFromTemplate(int templateId,string mrn,int apptId);
       
    }
}

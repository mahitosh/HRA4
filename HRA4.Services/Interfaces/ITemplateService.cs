using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRA4.Entities;
namespace HRA4.Services.Interfaces
{
    public interface ITemplateService
    {
        List<HtmlTemplate> GetTemplatesByInstitution(int institutionId);
        HtmlTemplate GetTemplate(int institutionId, int id);
        HtmlTemplate GetTemplateByRaTemplateId(int institutionId, int raTemplateId);
        HtmlTemplate InsertTemplate(HtmlTemplate template);
        HtmlTemplate UpdateTemplate(HtmlTemplate template);
    }
}

using HRA4.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Repositories.Interfaces
{
    public interface IHtmlTemplateRepository
    {
         void Insert(HtmlTemplate template);
         void Save(HtmlTemplate template);
         HtmlTemplate GetTemplateById(int instituionId, int templateId);
         List<HtmlTemplate> GetAllTemplates(int institutionId);
         int UpdateHtmlTemplate(Entities.HtmlTemplate HtmlTemplate);
        
    }
}

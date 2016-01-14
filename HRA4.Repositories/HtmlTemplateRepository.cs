using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Repositories
{
    public class HtmlTemplateRepository : Interfaces.IHtmlTemplateRepository
    {
        dynamic dbContext;

        public HtmlTemplateRepository(dynamic database)
        {
            dbContext = database;
        }

        public void Insert(Entities.HtmlTemplate template)
        {
             
        }

        public void Save(Entities.HtmlTemplate template)
        {
            throw new NotImplementedException();
        }

        public Entities.HtmlTemplate GetTemplateById(int instituionId, int templateId)
        {
            throw new NotImplementedException();
        }

        public Entities.HtmlTemplate GetAllTemplates(int institutionId)
        {
            throw new NotImplementedException();
        }
    }
}

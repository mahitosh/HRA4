using HRA4.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Services
{
    public class TemplateService:Interfaces.ITemplateService
    {
        IRepositoryFactory _repositoryFactory;
        public TemplateService(IRepositoryFactory repositoryFactory)
        {
            this._repositoryFactory = repositoryFactory;
        }
        public List<Entities.HtmlTemplate> GetTemplatesByInstitution(int institutionId)
        {
            throw new NotImplementedException();
        }

        public Entities.HtmlTemplate GetTemplate(int institutionId, int id)
        {
            throw new NotImplementedException();
        }

        public Entities.HtmlTemplate GetTemplateByRaTemplateId(int institutionId, int raTemplateId)
        {
            throw new NotImplementedException();
        }

        public Entities.HtmlTemplate InsertTemplate(Entities.HtmlTemplate template)
        {
            throw new NotImplementedException();
        }

        public Entities.HtmlTemplate UpdateTemplate(Entities.HtmlTemplate template)
        {
            throw new NotImplementedException();
        }
    }
}

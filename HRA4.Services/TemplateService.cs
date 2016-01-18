using HRA4.Repositories.Interfaces;
using HRA4.Services.Interfaces;
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
        string[] supported = new string[] { "surveySummary", "riskClinic", "LMN", "relativeLetter", "relativeKnownMutationLetter", "Screening" };
        string routine = "screening";
        IHraSessionManager _hraSessionManager;
        public TemplateService(IRepositoryFactory repositoryFactory, IHraSessionManager hraSessionManger)
        {
            this._repositoryFactory = repositoryFactory;
            _hraSessionManager = hraSessionManger;
        }

        public List<Entities.HtmlTemplate> GetTemplatesByInstitution(int institutionId)
        {
            return _repositoryFactory.HtmlTemplateRepository.GetAllTemplates(institutionId);

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

        /// <summary>
        /// Create the list of Suggested and other templates 
        /// </summary>
        /// <param name="institutionId">Institution Id</param>
        /// <returns>Returns the list of templates categorised as Suggested and others</returns>
        public ViewModels.TemplateList GetTemplates(int institutionId)
        {
            List<Entities.HtmlTemplate> _templates = _repositoryFactory.HtmlTemplateRepository.GetAllTemplates(institutionId);
            ViewModels.TemplateList templateList = new ViewModels.TemplateList();

            foreach(var template in _templates)
            {
                if (supported.Contains(template.RoutineName))
                {
                    if (string.Compare(routine, template.RoutineName, true) == 0 || string.Compare(template.RoutineName, "surveySummary", true) == 0)
                    {
                        templateList.SuggestedDocument.Add(new ViewModels.Template()
                        {
                            Id=template.Id,
                            TemplateName= template.TemplateName
                        });
                    }
                    else
                    {
                        templateList.OtherDocuments.Add(new ViewModels.Template()
                        {
                            Id = template.Id,
                            TemplateName = template.TemplateName
                        });
                    }
                }
            }

            return templateList;

        }
    }
}

using HRA4.Repositories.Interfaces;
using HRA4.Services.Interfaces;
using RiskApps3.Controllers;
using RiskApps3.Model.Clinic;
using System;
using System.Collections.Generic;
using System.Linq;
using HRA4.Entities;
using System.Text;
using System.Threading.Tasks;
using HRA4.Utilities;
using System.Web;
using System.IO;
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
           return _repositoryFactory.HtmlTemplateRepository.GetTemplateById(institutionId, id);
        }

        public Entities.HtmlTemplate GetTemplateByRaTemplateId(int institutionId, int raTemplateId)
        {
            throw new NotImplementedException();
        }

        public Entities.HtmlTemplate InsertTemplate(Entities.HtmlTemplate template)
        {
            throw new NotImplementedException();
        }

        public int UpdateTemplate(int institutionId, int id ,string TemplateString )
        {
            HtmlTemplate template = _repositoryFactory.HtmlTemplateRepository.GetTemplateById(institutionId, id);
            template.Template = System.Text.Encoding.UTF8.GetBytes(TemplateString);
            return _repositoryFactory.HtmlTemplateRepository.UpdateHtmlTemplate(template);
            
        }

        /// <summary>
        /// Runs template to generate html with required data.
        /// </summary>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public ViewModels.Template GenerateHtmlFromTemplate(int templateId)
        {
            Entities.HtmlTemplate _template = GetTemplate(0, templateId);
            string tempFileName = Guid.NewGuid().ToString();
            string path = HttpContext.Current.Server.MapPath(System.IO.Path.Combine(Constants.RAFilePath, "Temp", tempFileName));
            System.IO.File.WriteAllText(path, _template.TemplateString);
           string finalHtml= CreateHtmlDocument(path);
           try
           {
               File.Delete(path);
           }
           catch (Exception ex)
           {               
               
           }

           return new ViewModels.Template()
           {
               Id=templateId,
               FinalHtml = finalHtml,
               TemplateName = _template.TemplateName
           };

        }

        private string CreateHtmlDocument(string htmlPath)
        {
            //set active patient
            DocumentTemplate dt = new DocumentTemplate();
            dt.SetPatient(SessionManager.Instance.GetActivePatient());
            //string htmlPath = HttpContext.Current.Server.MapPath(Path.Combine(Constants.RAFilePath, "Temp", "SurveySummary.html"));
            dt.htmlPath = htmlPath;
            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(htmlPath))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
            }
            string html = sb.ToString();
            dt.htmlText = html;
            dt.ProcessDocument();
            string newhtml = dt.htmlText;
            return newhtml;
        }

        /// <summary>
        /// Create the list of Suggested and other templates 
        /// </summary>       
        /// <returns>Returns the list of templates categorised as Suggested and others</returns>
        public ViewModels.TemplateList GetTemplates()
        {
            List<Entities.HtmlTemplate> _templates = _repositoryFactory.HtmlTemplateRepository.GetAllTemplates(Convert.ToInt32(_hraSessionManager.InstitutionId));
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
            templateList.SuggestedDocument.OrderBy(o => o.TemplateName);
            templateList.OtherDocuments.OrderBy(o => o.TemplateName);
            return templateList;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.ViewModels
{
    public class Template
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
    }
    
    public class TemplateList
    {
        public TemplateList()
        {
            SuggestedDocument = new List<Template>();
            OtherDocuments = new List<Template>();
        }

        public List<Template> SuggestedDocument { get; set; }

        public List<Template> OtherDocuments { get; set; }
    }
}

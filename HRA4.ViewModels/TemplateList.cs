using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace HRA4.ViewModels
{
    public class Template
    {
        public int Id { get; set; }
        public string FinalHtml { get; set; }
        public string TemplateName { get; set; }
        /*
        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Content { get; set; }
         */ 
    }
    
    public class TemplateList
    {
        public TemplateList()
        {
            SuggestedDocument = new List<Template>();
            OtherDocuments = new List<Template>();

        }

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Content { get; set; }

        public string TemplateName { get; set; }

        public List<Template> SuggestedDocument { get; set; }

        public List<Template> OtherDocuments { get; set; }



    }

   

}

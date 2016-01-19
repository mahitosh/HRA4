using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HRA4.Web.Models {

    public class TinyMCEModelJQuery {

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Content { get; set; }
        public string TemplateName { get; set; }

    }
}
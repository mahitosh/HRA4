using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Entities
{
    public class HtmlTemplate
    {
        public int Id { get; set; }
        public int InstitutionId { get; set; }
        public string TemplateName { get; set; }
        public byte[] Template { get; set; }
        public int RATemplateId { get; set; }
        public string TemplateString
        {
            get
            {
                return System.Text.Encoding.UTF8.GetString(Template);
            }
        }
        public string RoutineName { get; set; }

    }
}

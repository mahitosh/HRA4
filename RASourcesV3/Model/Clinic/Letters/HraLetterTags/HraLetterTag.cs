using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace RiskApps3.Model.Clinic.Letters.HraLetterTags
{
    public interface HraLetterTag
    {
        void ProcessHtml(ref HtmlDocument doc);
    }
}

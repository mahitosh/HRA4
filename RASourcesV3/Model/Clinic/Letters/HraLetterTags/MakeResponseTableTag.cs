using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using System.Data;

namespace RiskApps3.Model.Clinic.Letters.HraLetterTags
{
    public class MakeResponseTableTag : MakeTableTag
    {
        public MakeResponseTableTag()
        {
            base.HraTagText = "[MakeResponseTable(";
            base.HraTagSuffix = ")]";
        }
        public override void ProcessTableCellByIndex(int row, int column, HtmlNode tableCellNode, HtmlTextNode cellTextNode)
        {
            if (column == 0)
            {
                tableCellNode.Attributes.Remove("align");
                tableCellNode.Attributes.Add(tableCellNode.OwnerDocument.CreateAttribute("align", "right"));
            }
            else
            {
                tableCellNode.Attributes.Remove("align");
                tableCellNode.Attributes.Add(tableCellNode.OwnerDocument.CreateAttribute("align", "left"));
                cellTextNode.Text = "<b>" + cellTextNode.Text + "</b>";
            }
        }
    }
}

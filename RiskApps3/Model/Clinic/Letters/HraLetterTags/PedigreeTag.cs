using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using RiskApps3.Utilities;
using System.Drawing;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.Model.Clinic.Letters.HraLetterTags
{
    public class PedigreeTag : HraLetterTag
    {
        private char[] splitChars = {','};

        private int height = 500;
        private int width = 500;

        public readonly string HraTagText = "[Pedigree(";
        public readonly string HraTagSuffix = ")]";

        public Patient proband;
        /**********************************************************************/
        //the interface contract to substitute hra tag in the html text
        public void ProcessHtml(ref HtmlDocument doc)
        {
            try
            {
                //find all pedigree nodes
                List<HtmlTextNode> pedigreeNodes = new List<HtmlTextNode>();
                FindPedigreeNodes(doc.DocumentNode, ref pedigreeNodes);

                foreach (HtmlTextNode pedigreeNode in pedigreeNodes)
                {
                    PerformPedigreeReplacement(pedigreeNode);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        /**********************************************************************/
        private void FindPedigreeNodes(HtmlNode node, ref List<HtmlTextNode> pedigreeNodes)
        {
            if (node.NodeType == HtmlNodeType.Text)
            {
                HtmlTextNode tn = (HtmlTextNode)node;
                if (tn.Text.Contains(HraTagText))
                {
                    pedigreeNodes.Add(tn);
                }
            }

            foreach (HtmlNode child in node.ChildNodes)
            {
                FindPedigreeNodes(child, ref pedigreeNodes);
            }
        }

        /**********************************************************************/
        private void PerformPedigreeReplacement(HtmlTextNode pedigreeNode)
        {
            int pos = pedigreeNode.Text.IndexOf(HraTagText, 0);
            int suffixPos = pedigreeNode.Text.IndexOf(HraTagSuffix);
            if (suffixPos > pos)
            {
                string args = pedigreeNode.Text.Substring(pos + HraTagText.Length, suffixPos - pos - HraTagText.Length);
                string[] parts = args.Split(splitChars);
                if (parts.Length == 2)
                {
                    int.TryParse(parts[0], out width);
                    int.TryParse(parts[1], out height);
                }

                PedigreeGenerator pg = new PedigreeGenerator(width, height,proband); 
                Bitmap bmp;
                if (proband != null)
                {
                    bmp = pg.GeneratePedigreeImage(proband);
                }
                else
                {
                    bmp = pg.GeneratePedigreeImage();
                }
                
               
                System.IO.MemoryStream stream = new System.IO.MemoryStream();
                bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                var base64Data = Convert.ToBase64String(stream.ToArray());

                HtmlNode newPedNode = pedigreeNode.OwnerDocument.CreateElement("img");
                newPedNode.Attributes.Add(pedigreeNode.OwnerDocument.CreateAttribute("src", "data:image/png;base64," + base64Data));
                newPedNode.Attributes.Add(pedigreeNode.OwnerDocument.CreateAttribute("style", "width:" + bmp.Width + "px; height:" + bmp.Height + "px"));

                pedigreeNode.ParentNode.ReplaceChild(newPedNode, pedigreeNode);
            }
        }
    }
}
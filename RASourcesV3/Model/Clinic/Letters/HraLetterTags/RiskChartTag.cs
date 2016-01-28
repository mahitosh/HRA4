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
    public class RiskChartTag : HraLetterTag
    {
        private char[] splitChars = {','};

        private int height = 500;
        private int width = 500;

        public readonly string HraTagText = "[RiskChart(";
        public readonly string HraTagSuffix = ")]";

        public Patient proband;
        /**********************************************************************/
        //the interface contract to substitute hra tag in the html text
        public void ProcessHtml(ref HtmlDocument doc)
        {
            try
            {
                //find all risk chart nodes
                List<HtmlTextNode> riskChartNodes = new List<HtmlTextNode>();
                FindRiskChartNodes(doc.DocumentNode, ref riskChartNodes);

                foreach (HtmlTextNode riskChartNode in riskChartNodes)
                {
                    PerformRiskChartReplacement(riskChartNode);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        /**********************************************************************/
        private void FindRiskChartNodes(HtmlNode node, ref List<HtmlTextNode> riskChartNodes)
        {
            if (node.NodeType == HtmlNodeType.Text)
            {
                HtmlTextNode tn = (HtmlTextNode)node;
                if (tn.Text.Contains(HraTagText))
                {
                    riskChartNodes.Add(tn);
                }
            }

            foreach (HtmlNode child in node.ChildNodes)
            {
                FindRiskChartNodes(child, ref riskChartNodes);
            }
        }

        /**********************************************************************/
        private void PerformRiskChartReplacement(HtmlTextNode riskChartNode)
        {
            int pos = riskChartNode.Text.IndexOf(HraTagText, 0);
            int suffixPos = riskChartNode.Text.IndexOf(HraTagSuffix);
            if (suffixPos > pos)
            {
                if (proband.HasBreastCancer() || proband.gender.ToLower().StartsWith("m"))
                {
                    riskChartNode.ParentNode.RemoveChild(riskChartNode);
                }
                else
                {
                    string args = riskChartNode.Text.Substring(pos + HraTagText.Length, suffixPos - pos - HraTagText.Length);
                    string[] parts = args.Split(splitChars);
                    if (parts.Length == 2)
                    {
                        int.TryParse(parts[0], out width);
                        int.TryParse(parts[1], out height);
                    }

                    RiskChartGenerator rcg = new RiskChartGenerator(width, height, proband);
                    Bitmap bmp = rcg.GenerateRiskChartImage();

                    System.IO.MemoryStream stream = new System.IO.MemoryStream();
                    bmp.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    var base64Data = Convert.ToBase64String(stream.ToArray());

                    HtmlNode newRCNode = riskChartNode.OwnerDocument.CreateElement("img");

                    newRCNode.Attributes.Add(riskChartNode.OwnerDocument.CreateAttribute("src", "data:image/png;base64," + base64Data));
                    newRCNode.Attributes.Add(riskChartNode.OwnerDocument.CreateAttribute("style", "width:" + bmp.Width + "px; height:" + bmp.Height + "px"));


                    riskChartNode.ParentNode.ReplaceChild(newRCNode, riskChartNode);
                }
            }
        }
    }
}
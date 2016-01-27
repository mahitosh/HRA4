using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
using RiskApps3.Utilities;
using System.IO;

namespace RiskApps3.Model.Clinic.Letters.HraLetterTags
{
    public class ImageTag : HraLetterTag
    {
        public string DocumentHtmlPath = "";
        /**********************************************************************/
        //the interface contract to substitute hra tag in the html text
        public void ProcessHtml(ref HtmlDocument doc)
        {
            try
            {
                //find all pedigree nodes
                List<HtmlNode> imageNodes = new List<HtmlNode>();
                FindImageNodes(doc.DocumentNode, ref imageNodes);

                foreach (HtmlNode imageNode in imageNodes)
                {
                    PerformImageReplacement(imageNode);
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(e.ToString());
            }
        }

        /**********************************************************************/
        private void FindImageNodes(HtmlNode node, ref List<HtmlNode> imageNodes)
        {
            if (node.Name == "img")
            {
                imageNodes.Add(node);
            }

            foreach (HtmlNode child in node.ChildNodes)
            {
                FindImageNodes(child, ref imageNodes);
            }
        }

        /**********************************************************************/
        private void PerformImageReplacement(HtmlNode imageNode)
        {
            HtmlAttribute src = null;
            foreach (HtmlAttribute hat in imageNode.Attributes)
            {
                if (hat.Name == "src" && hat.Value.ToLower().Contains(".gif"))
                {
                    src = hat;
                    break;
                }
            }
            if (src != null)
            {
                string filename = Path.Combine(DocumentHtmlPath.Substring(0,DocumentHtmlPath.LastIndexOf("\\")),src.Value);
                MemoryStream ms = new MemoryStream();
                try
                {
                    FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read);
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    ms.Write(bytes, 0, (int)file.Length);
                    file.Close();
                    imageNode.Attributes.Remove(src);
                    imageNode.Attributes.Add(imageNode.OwnerDocument.CreateAttribute("src", "data:image/gif;base64," + Convert.ToBase64String(ms.ToArray())));
                }
                catch (Exception e)
                {
                    Logger.Instance.WriteToLog(e.ToString());
                }

                ms.Close();
                
            }
        }
    }
}
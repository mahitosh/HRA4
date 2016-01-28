using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using Saxon.Api;
using System.Xml;
using System.Xml.Linq;

namespace RiskApps3.Utilities
{
    public class TransformUtils
    {
        //returns the passed object as a string (no landing in file)
        public static string DataContractSerializeObject<T>(T objectToSerialize)
        {
            using (MemoryStream memStm = new MemoryStream())
            {
                var serializer = new DataContractSerializer(typeof(T));
                serializer.WriteObject(memStm, objectToSerialize);

                memStm.Seek(0, SeekOrigin.Begin);

                using (var streamReader = new StreamReader(memStm))
                {
                    string result = streamReader.ReadToEnd();
                    return result;
                }
            }
        }

        //run an XSLT transform without an extra specified paramater.
        //transformFile is the name of the .xsl to run;
        //path is usually tools directory (be sure to include final slash in passed string)
        static public XmlDocument performTransform(XmlDocument inDOM, string path, string transformFile)
        {
            return (performTransformWithParam(inDOM, path, transformFile, "", ""));
        }

        //run an XSLT transform.
        //transformFile is the name of the .xsl to run;
        //path is usually tools directory (be sure to include final slash in passed string)
        //takes a single paramater and value; use empty string for both if not needed
        static public XmlDocument performTransformWithParam(XmlDocument inDOM, string path, string transformFile, string param1Name, string param1Value)
        {
            return performTransformWith2Params(inDOM, path, transformFile, param1Name, param1Value, "", "");
        }

        //run an XSLT transform.
        //transformFile is the name of the .xsl to run;
        //path is usually tools directory (be sure to include final slash in passed string)
        //takes two paramaters and corresponding values; use empty strings if not needed
        static public XmlDocument performTransformWith2Params(XmlDocument inDOM, string path, string transformFile, string param1Name, string param1Value, string param2Name, string param2Value)
        {
            // Create a Processor instance.
            Processor processor = new Processor();

            // Load the source document, building a tree
            XmlNode node = inDOM;
            XdmNode input = processor.NewDocumentBuilder().Build(node);

            // Compile the stylesheet
            XsltExecutable exec = processor.NewXsltCompiler().Compile(new XmlTextReader(path.Replace("Program Files", "PROGRA~1") + transformFile));

            // Create a transformer 
            XsltTransformer transformer = exec.Load();

            string xdmToolsPath = "file:/" + path.Replace("\\", "/").Replace(" ", "%20");

            // Run it once        
            // Set parameters
            transformer.SetParameter(new QName("", "", "include-attributes"), new XdmAtomicValue(false));
            //following may be needed if xslt itself needs to find other files
            transformer.SetParameter(new QName("", "", "localBaseUri"), new XdmAtomicValue(xdmToolsPath.Replace("Program%20Files", "PROGRA~1")));
            //optionally add another parameter
            if (!String.IsNullOrEmpty(param1Name))
            {
                transformer.SetParameter(new QName("", "", param1Name), new XdmAtomicValue(param1Value));
            }
            //and another param
            if (!String.IsNullOrEmpty(param2Name))
            {
                transformer.SetParameter(new QName("", "", param2Name), new XdmAtomicValue(param2Value));
            }

            transformer.InitialContextNode = input;
            XdmDestination results = new XdmDestination();
            transformer.Run(results);

            XmlDocument resultXmlDoc = new XmlDocument();
            resultXmlDoc.LoadXml(results.XdmNode.OuterXml);
            XmlDeclaration declaration = resultXmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            resultXmlDoc.PrependChild(declaration);

            // return the result
            return (resultXmlDoc);
        }

        static public string FormatXml(String Xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(Xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return Xml;
            }
        }
    }
}

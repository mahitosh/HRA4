using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using RiskApps3.View.Common;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.View;
using RiskApps3.View.PatientRecord;
using RiskApps3.View.PatientRecord.FamilyHistory;
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    public partial class FamilyHistoryViewSerializer : Form
    {
        private Patient proband;

        private Model.PatientRecord.FHx.FamilyHistory fhx;

        public FamilyHistoryViewSerializer(Patient p, Model.PatientRecord.FHx.FamilyHistory f)
        {
            InitializeComponent();
            this.fhx = f;
            this.proband = p;
        }

        private void serializeFamilyHistoryButton_Click(object sender, EventArgs e)
        {
            serializeFamilyHistoryView();
        }

        private void transformXml(string inFile, string transform, string outFile)
        {
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            XmlReader reader = new XmlTextReader(inFile);
            XmlTextWriter myWriter = new XmlTextWriter(outFile, null);
            try
            {
                myXslTrans.Load(transform, new XsltSettings(false, true), new XmlUrlResolver());
                myXslTrans.Transform(reader, null, myWriter);
                myWriter.Close();
                reader.Close();
            }
            catch (Exception e)
            {
                if (myWriter != null)
                    myWriter.Close();

                if (reader != null)
                    reader.Close();

                throw (e);
            }

        }

        private void transformXml(Stream input, string transform, Stream output)
        {
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            XmlReader reader = new XmlTextReader(input);
            XmlTextWriter myWriter = new XmlTextWriter(output, null);
            try
            {
                myXslTrans.Load(transform);
                myXslTrans.Transform(reader, null, myWriter);
                myWriter.Close();
                reader.Close();
            }
            catch (Exception e)
            {
                if (myWriter != null)
                    myWriter.Close();

                if (reader != null)
                    reader.Close();

                throw (e);
            }
        }

        private void transformXml(string input, string transform, Stream output)
        {
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            XmlReader reader = new XmlTextReader(new StringReader(input));
            XmlTextWriter myWriter = new XmlTextWriter(output, null);
            XsltArgumentList argList = new XsltArgumentList();
            DateTime dt = new DateTime();
            argList.AddExtensionObject("http://exslt.org/dates-and-times", dt);

            try
            {
                myXslTrans.Load(transform);
                myXslTrans.Transform(reader, null, myWriter);
                myWriter.Close();
                reader.Close();
            }
            catch (Exception e)
            {
                if (myWriter != null)
                    myWriter.Close();

                if (reader != null)
                    reader.Close();

                throw (e);
            }
        }

        private void serializeFamilyHistoryView()
        {
            string  xmlFileName = @"C:\Program Files\riskAppsV2\documents\" + proband.name + DateTime.Now.ToString("_yyyy-MM-dd_HHmm") + ".xml";
            string  resultFile1 = @"C:\Program Files\riskAppsV2\documents\" + proband.name + DateTime.Now.ToString("_yyyy-MM-dd_HHmm") + "-result1.xml";
            string  resultFile2 = @"C:\Program Files\riskAppsV2\documents\" + proband.name + DateTime.Now.ToString("_yyyy-MM-dd_HHmm") + "-result2.xml";
            string  resultFile3 = @"C:\Program Files\riskAppsV2\documents\" + proband.name + " CCD " + DateTime.Now.ToString("yyyy-MM-dd_HHmm") + ".xml";

            try
            {
                proband.Summarize();

                DataContractSerializer ds = new DataContractSerializer(typeof(Model.PatientRecord.FHx.FamilyHistory));

                using (Stream s = File.Create(xmlFileName))
                {
                    ds.WriteObject(s, fhx);
                }
                transformXml(xmlFileName, @"C:\Program Files\riskappsv2\tools\hra_to_ccd_remove_namespaces.xsl", resultFile1);
                transformXml(resultFile1, @"C:\Program Files\riskappsv2\tools\hra_to_ccd.xsl", resultFile2);
                transformXml(resultFile2, @"C:\Program Files\riskappsv2\tools\hra_to_ccd2.xsl", resultFile3);


                fhvSerializerRichTextBox.Hide();

                webBrowser1.Navigate(resultFile3);
                this.WindowState = FormWindowState.Maximized;
                webBrowser1.Show();
            }
            catch (Exception e)
            {
                webBrowser1.Hide();
                fhvSerializerRichTextBox.Show();
                fhvSerializerRichTextBox.Text = e.ToString();
            }
#if HRA_CCD_DONE
            finally
            {
                if (File.Exists(xmlFileName)) File.Delete(xmlFileName);
                if (File.Exists(resultFile1)) File.Delete(resultFile1);
                if (File.Exists(resultFile2)) File.Delete(resultFile2);
            }
#endif
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Controllers;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class ProviderParagraph : HraObject
    {
        public int paragraphID;
        public int providerID;
        [HraAttribute] public string paragraph;
        [HraAttribute] public string patientParagraph;

        /*****************************************************/

        public string ProviderParagraph_paragraph
        {
            get { return paragraph; }
            set
            {
                if (value != paragraph)
                {
                    paragraph = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("paragraph"));
                    SignalModelChanged(args);

                    SessionManager.Instance.MetaData.BrOvCdsRecs.BackgroundLoadWork();
                }
            }
        }

        /*****************************************************/

        public string ProviderParagraph_patientParagraph
        {
            get { return patientParagraph; }
            set
            {
                if (value != patientParagraph)
                {
                    patientParagraph = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("patientParagraph"));
                    SignalModelChanged(args);

                    SessionManager.Instance.MetaData.BrOvCdsRecs.BackgroundLoadWork();
                }
            }
        }


        /*******************************************************************************/

        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("paragraphID", paragraphID);
            pc.Add("providerID", providerID);

            DoPersistWithSpAndParams(e,
                                     "sp_3_Save_ProviderParagraph",
                                     ref pc);
        }
    }
}
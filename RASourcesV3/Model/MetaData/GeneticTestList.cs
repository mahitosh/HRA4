using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Utilities;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace RiskApps3.Model.MetaData
{
    public class GeneticTestList : HRAList<GeneticTestObject>
    {
        private object[] constructor_args;
        private ParameterCollection pc;

        /**************************************************************************************************/

        public GeneticTestList()
        {
            this.constructor_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs(
                "sp_3_LoadGeneticTests",
                this.pc,
                this.constructor_args);

            DoListLoad(lla);
        }

        public List<String> GetGenesInPanel(int panelID)
        {
            return this
                .Where(t => ((GeneticTestObject)t).panelID == panelID)
                .Select(t => ((GeneticTestObject)t).geneName)
                .Distinct()
                .ToList();
        }

        public int GetPanelIDFromName(String panelName)
        {
            int panelId = this
                .Where(t => ((GeneticTestObject)t).panelName.Equals(panelName))
                .Select(gto => ((GeneticTestObject)gto).panelID)
                .Distinct()
                .SingleOrDefault();

            if(panelId==0)
            {
                panelId=-1;
            }

            return panelId;
        }

        public String GetPanelNameFromID(int panelID)
        {
            string panelName = this
                .Where(t => ((GeneticTestObject)t).panelID == panelID)
                .Select(t => ((GeneticTestObject)t).panelName)
                .Distinct()
                .SingleOrDefault();

            if (String.IsNullOrEmpty(panelName))
            {
                panelName = "";
            }

            return panelName;
        }
        public String GetPanelShortNameFromID(int panelID)
        {
            string panelShortName = this
                .Where(t => ((GeneticTestObject)t).panelID == panelID)
                .Select(t => ((GeneticTestObject)t).panelShortName)
                .Distinct()
                .SingleOrDefault();

            if (String.IsNullOrEmpty(panelShortName))
            {
                panelShortName = "";
            }

            return panelShortName;
        }
    }
}
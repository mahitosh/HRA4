using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord.Pedigree;


namespace RiskApps3.View.PatientRecord.Pedigree
{
    class PedigreeContextMenu
    {
        internal static ContextMenuStrip GenerateMenu(PedigreeControl pedigreeControl, PedigreeIndividual individual)
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            ToolStripMenuItem addBrotherToolStripMenuItem = new ToolStripMenuItem();
            
            addBrotherToolStripMenuItem.Click += GenerateAddSiblingListener(pedigreeControl, individual);

            addBrotherToolStripMenuItem.Text = "Add Brother";
            menu.Items.Add(addBrotherToolStripMenuItem);


            ToolStripMenuItem addSonX1ToolStripMenuItem = new ToolStripMenuItem("Add Son");
            addSonX1ToolStripMenuItem.Click += GenerateAddChildListener(pedigreeControl, individual);
            addSonX1ToolStripMenuItem.Text = "Add Son";
            menu.Items.Add(addSonX1ToolStripMenuItem);


            ToolStripMenuItem addParentsToolStripMenuItem = new ToolStripMenuItem("Add Parents");
            addParentsToolStripMenuItem.Click += GenerateAddParentsListener(pedigreeControl, individual);
            addParentsToolStripMenuItem.Text = "Add Parents";
            menu.Items.Add(addParentsToolStripMenuItem);


            menu.Items.Add("-");
            ToolStripMenuItem LinkToParentToolStripMenuItem = new ToolStripMenuItem("Link To Parent");
            LinkToParentToolStripMenuItem.Click += GenerateLinkToParentListener(pedigreeControl, individual);
            LinkToParentToolStripMenuItem.Text = "Link To Parent";
            menu.Items.Add(LinkToParentToolStripMenuItem);


            return menu;


        }

        private static EventHandler GenerateAddParentsListener(PedigreeControl pedigreeControl, PedigreeIndividual individual)
        {
            return new EventHandler(delegate(object sender, EventArgs e)
            {
                //if (pedigreeControl.model.Selected.Count == 0)
                //{
                //    pedigreeControl.SetPedigreeModel(PedigreeModelUtils.AddParents(individual, pedigreeControl.model));
                //}
                //else
                //{
                //    foreach (PedigreeIndividual pi in pedigreeControl.model.Selected)
                //    {
                //        pedigreeControl.SetPedigreeModel(PedigreeModelUtils.AddParents(pi, pedigreeControl.model));
                //    }
                //}
            });
        }

        private static EventHandler GenerateLinkToParentListener(PedigreeControl pedigreeControl, PedigreeIndividual individual)
        {
            return new EventHandler(delegate(object sender, EventArgs e)
            {
                //pedigreeControl.SetPedigreeModel(PedigreeModelUtils.SetIndividualParents(individual, pedigreeControl.model));
                
            });
        }

        private static EventHandler GenerateAddChildListener(PedigreeControl pedigreeControl, PedigreeIndividual individual)
        {
            return new EventHandler(delegate(object sender, EventArgs e)
            {
                if (pedigreeControl.model.Selected.Count == 0)
                {
                    //pedigreeControl.SetPedigreeModel(PedigreeModelUtils.AddSon(individual, pedigreeControl.model));
                }
                else
                {
                    foreach (PedigreeIndividual pi in pedigreeControl.model.Selected)
                    {
                        //pedigreeControl.SetPedigreeModel(PedigreeModelUtils.AddSon(pi, pedigreeControl.model));
                    }
                }
                //pedigreeControl.SetPedigreeModel(PedigreeModelUtils.AddSon(individual, pedigreeControl.model));

            });
        }

        private static EventHandler GenerateAddSiblingListener(PedigreeControl pedigreeControl, PedigreeIndividual individual)
        {
            return new EventHandler(delegate(object sender, EventArgs e)
                {
                    if (pedigreeControl.model.Selected.Count == 0)
                    {
                        //pedigreeControl.SetPedigreeModel(PedigreeModelUtils.AddBrother(individual, pedigreeControl.model));
                    }
                    else
                    {
                        foreach (PedigreeIndividual pi in pedigreeControl.model.Selected)
                        {
                            //pedigreeControl.SetPedigreeModel(PedigreeModelUtils.AddBrother(pi, pedigreeControl.model));
                        }
                    }
                    //ToolStripItem tsi = (ToolStripItem)sender;
                    //pedigreeControl.SetPedigreeModel(PedigreeModelUtils.AddBrother(individual, pedigreeControl.model));
                });
        }
    }
}



/***************************/

//ToolStripMenuItem addBrotherX1ToolStripMenuItem = new ToolStripMenuItem("Add Brother");


//ToolStripMenuItem addBrotherToolStripMenuItem = new ToolStripMenuItem();
//ToolStripMenuItem addBrotherX1ToolStripMenuItem = new ToolStripMenuItem("Add Brother");
//addBrotherX1ToolStripMenuItem.Click += new System.EventHandler(AddSibController_Click);
//addBrotherX1ToolStripMenuItem.Tag = individual;
//addBrotherToolStripMenuItem.Name = "addBrotherToolStripMenuItem";
//addBrotherToolStripMenuItem.Text = "Add Brother(s)";
//menu.Items.Add(addBrotherToolStripMenuItem);


//ToolStripMenuItem addBrotherX2ToolStripMenuItem = new ToolStripMenuItem("Add Brother X2");
//addBrotherX2ToolStripMenuItem.Click += new System.EventHandler(AddSibController_Click);
//addBrotherX2ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addBrotherX3ToolStripMenuItem = new ToolStripMenuItem("Add Brother X3");
//addBrotherX3ToolStripMenuItem.Click += new System.EventHandler(AddSibController_Click);
//addBrotherX3ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addBrotherX4ToolStripMenuItem = new ToolStripMenuItem("Add Brother X4");
//addBrotherX4ToolStripMenuItem.Click += new System.EventHandler(AddSibController_Click);
//addBrotherX4ToolStripMenuItem.Tag = e;
//addBrotherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
//            addBrotherX1ToolStripMenuItem,
//            addBrotherX2ToolStripMenuItem,
//            addBrotherX3ToolStripMenuItem,
//            addBrotherX4ToolStripMenuItem});
//addBrotherToolStripMenuItem.Name = "addBrotherToolStripMenuItem";


//ToolStripMenuItem addSisterToolStripMenuItem = new ToolStripMenuItem();
//ToolStripMenuItem addSisterX1ToolStripMenuItem = new ToolStripMenuItem("Add Sister");
//addSisterX1ToolStripMenuItem.Click += new System.EventHandler(AddSibController_Click);
//addSisterX1ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addSisterX2ToolStripMenuItem = new ToolStripMenuItem("Add Sister X2");
//addSisterX2ToolStripMenuItem.Click += new System.EventHandler(AddSibController_Click);
//addSisterX2ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addSisterX3ToolStripMenuItem = new ToolStripMenuItem("Add Sister X3");
//addSisterX3ToolStripMenuItem.Click += new System.EventHandler(AddSibController_Click);
//addSisterX3ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addSisterX4ToolStripMenuItem = new ToolStripMenuItem("Add Sister X4");
//addSisterX4ToolStripMenuItem.Click += new System.EventHandler(AddSibController_Click);
//addSisterX4ToolStripMenuItem.Tag = e;
//addSisterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
//            addSisterX1ToolStripMenuItem,
//            addSisterX2ToolStripMenuItem,
//            addSisterX3ToolStripMenuItem,
//            addSisterX4ToolStripMenuItem});
//addSisterToolStripMenuItem.Name = "addSisterToolStripMenuItem";
//addSisterToolStripMenuItem.Text = "Add Sister(s)";
//control.ContextMenuStrip.Items.Add(addSisterToolStripMenuItem);


//ToolStripMenuItem addSonToolStripMenuItem = new ToolStripMenuItem();


//addSonX1ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addSonX2ToolStripMenuItem = new ToolStripMenuItem("Add Son X2");
//addSonX2ToolStripMenuItem.Click += new System.EventHandler(AddKidController_Click);
//addSonX2ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addSonX3ToolStripMenuItem = new ToolStripMenuItem("Add Son X3");
//addSonX3ToolStripMenuItem.Click += new System.EventHandler(AddKidController_Click);
//addSonX3ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addSonX4ToolStripMenuItem = new ToolStripMenuItem("Add Son X4");
//addSonX4ToolStripMenuItem.Click += new System.EventHandler(AddKidController_Click);
//addSonX4ToolStripMenuItem.Tag = e;
//addSonToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
//            addSonX1ToolStripMenuItem,
//            addSonX2ToolStripMenuItem,
//            addSonX3ToolStripMenuItem,
//            addSonX4ToolStripMenuItem});
//addSonToolStripMenuItem.Name = "addSonToolStripMenuItem";
//addSonToolStripMenuItem.Text = "Add Son(s)";
//control.ContextMenuStrip.Items.Add(addSonToolStripMenuItem);

//ToolStripMenuItem addDaughterToolStripMenuItem = new ToolStripMenuItem();
//ToolStripMenuItem addDaughterX1ToolStripMenuItem = new ToolStripMenuItem("Add Daughter");
//addDaughterX1ToolStripMenuItem.Click += new System.EventHandler(AddKidController_Click);
//addDaughterX1ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addDaughterX2ToolStripMenuItem = new ToolStripMenuItem("Add Daughter X2");
//addDaughterX2ToolStripMenuItem.Click += new System.EventHandler(AddKidController_Click);
//addDaughterX2ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addDaughterX3ToolStripMenuItem = new ToolStripMenuItem("Add Daughter X3");
//addDaughterX3ToolStripMenuItem.Click += new System.EventHandler(AddKidController_Click);
//addDaughterX3ToolStripMenuItem.Tag = e;
//ToolStripMenuItem addDaughterX4ToolStripMenuItem = new ToolStripMenuItem("Add Daughter X4");
//addDaughterX4ToolStripMenuItem.Click += new System.EventHandler(AddKidController_Click);
//addDaughterX4ToolStripMenuItem.Tag = e;
//addDaughterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
//            addDaughterX1ToolStripMenuItem,
//            addDaughterX2ToolStripMenuItem,
//            addDaughterX3ToolStripMenuItem,
//            addDaughterX4ToolStripMenuItem});
//addDaughterToolStripMenuItem.Name = "addDaughterToolStripMenuItem";
//addDaughterToolStripMenuItem.Text = "Add Daughter(s)";
//control.ContextMenuStrip.Items.Add(addDaughterToolStripMenuItem);



//control.ContextMenuStrip.Items.Add("-");
//ToolStripItem tsi = control.ContextMenuStrip.Items.Add("Add Parents");
//tsi.Tag = e;
//tsi.Click += new EventHandler(AddParentController_Click);

//
//tsi = control.ContextMenuStrip.Items.Add("Unhide Spouse");
//tsi.Tag = e;
//tsi.Click += new EventHandler(UnhideSpouseController_Click);

//tsi = control.ContextMenuStrip.Items.Add("Hide Spouse");
//tsi.Tag = e;
//tsi.Click += new EventHandler(HideSpouseController_Click);

//if (model.Selected.Count > 0)
//{
//    control.ContextMenuStrip.Items.Add("-");
//    tsi = control.ContextMenuStrip.Items.Add("Make Twins");
//    tsi.Tag = e;
//    tsi.Click += new EventHandler(MakeTwinsController_Click);
//}

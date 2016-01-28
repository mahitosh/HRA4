using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Controllers;
using RiskApps3.Model.MetaData;
using RiskApps3.Model;
using Transitions;
using RiskApps3.View.Common.AutoSearchTextBox;
using RiskApps3.Model.PatientRecord.Labs;

namespace RiskApps3.View.PatientRecord
{
    public partial class OrderRow : UserControl
    {
        protected Order order;
        protected HraView owningView;
        OrderTypesList otl;
        Transition t;
        String group = "All Groups";

        public OrderRow(Order passedOrder)
        {
            InitializeComponent();
            order = passedOrder;

            //FillControls();
        }

        public OrderRow(Order passedOrder, HraView passedOwningView)
            : this(passedOrder)
        {
            owningView = passedOwningView;
            // Initial group setting must be obtained here.
            if (((OrdersView)owningView).Controls["groupComboBox"] != null)
            {
                group = ((OrdersView)owningView).Controls["groupComboBox"].Text;
            }
        }

        private void FillControls()
        {
            orderDateLabel.Text = ((DateTime)order.Order_orderDate).Date.ToString("MM/dd/yyyy");
            orderComboBox.Text = order.Order_orderDesc;
            group = ((OrdersView)owningView).Controls["groupComboBox"].Text;

            if (otl != null)
            {
                //set the correct list of available orders showing based on the OrdersView group
                String existingOrderValue = orderComboBox.Text;
                orderComboBox.Clear();

                if (group.Equals("All Groups"))
                {
                    //orderComboBox.Items.AddRange(otl.Select(g => ((OrderTypeObject)g).orderDescription).ToArray());
                    AutoCompleteUtils.fillComboBoxFromArray(orderComboBox, otl.Select(g => ((OrderTypeObject)g).orderDescription).ToArray());
                }
                else
                {
                    AutoCompleteUtils.fillComboBoxFromArray(orderComboBox,
                                                otl.Where(g => ((OrderTypeObject)g).grouping.Equals(group))
                                                    .Select(t => ((OrderTypeObject)t).orderDescription).ToArray());
                }


                if (!String.IsNullOrEmpty(existingOrderValue))
                    orderComboBox.setText(existingOrderValue);
            }
        }

        public Order getOrder()
        {
            return order;
        }

        public void SetOrder(Order a)
        {
            order = a;
            FillControls();
        }

        private void OrderRow_Load(object sender, EventArgs e)
        {
            ((OrdersView)owningView).Finalized += new EventHandler<OrdersView.FinalizedEventArgs>(OrderRow_Finalized);

            SessionManager.Instance.MetaData.OrderTypes.AddHandlersWithLoad(null, OrderTypesLoaded, null);

            orderComboBox.setSelectedIndexChangedEventHandler(orderComboBox_SelectionChangeCommitted);
            orderComboBox.Focus();
            orderComboBox.Select();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            ((OrdersView)owningView).Finalized += new EventHandler<OrdersView.FinalizedEventArgs>(OrderRow_Finalized);
            base.Dispose(disposing);
        }

        private void OrderTypesLoaded(HraListLoadedEventArgs e)
        {
            otl = SessionManager.Instance.MetaData.OrderTypes;

            FillControls();

            SessionManager.Instance.MetaData.OrderTypes.ReleaseListeners(this);
        }

        private void orderComboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            order.Order_orderDesc = (String)orderComboBox.Text;
            order.Order_orderGrouping = group;
        }

        //this property needed for animation w/Transitions
        public int MarginTop
        {
            get
            {
                return Margin.Top;
            }
            set
            {
                if (value != Margin.Top)
                {
                    Padding marginThingy = new Padding(Margin.Left, value, Margin.Right, Margin.Bottom);
                    Margin = marginThingy;
                }
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            ((OrdersView)owningView).Finalized -= new EventHandler<OrdersView.FinalizedEventArgs>(OrderRow_Finalized);
            SessionManager.Instance.MetaData.OrderTypes.ReleaseListeners(this);

            //Transition.run(this, "BackColor", Color.Red, new TransitionType_Flash(1, 100));
            t = new Transition(new TransitionType_Flash(1, 50));
            t.add(this, "BackColor", Color.Gray);
            t.TransitionCompletedEvent += new EventHandler<Transition.Args>(t_TransitionCompletedEvent);
            
            t.run();

        }

        void t_TransitionCompletedEvent(object sender, Transition.Args e)
        {
            PtOrderList pol = ((OrdersView)owningView).getPtOrderList();
            pol.RemoveFromList(order, SessionManager.Instance.securityContext);

            t.TransitionCompletedEvent -= new EventHandler<Transition.Args>(t_TransitionCompletedEvent);
        }

        private void orderComboBox_Enter(object sender, EventArgs e)
        {
            //set the correct list of available orders showing based on the OrdersView group
            String existingOrderValue = orderComboBox.Text;
            orderComboBox.Clear();

            group = ((OrdersView)owningView).Controls["groupComboBox"].Text;

            if (otl != null)
            {
                if (group.Equals("All Groups"))
                {
                    //orderComboBox.Items.AddRange(otl.Select(g => ((OrderTypeObject)g).orderDescription).ToArray());
                    AutoCompleteUtils.fillComboBoxFromArray(orderComboBox, otl.Select(g => ((OrderTypeObject)g).orderDescription).ToArray());
                }
                else
                {
                    AutoCompleteUtils.fillComboBoxFromArray(orderComboBox,
                                                otl.Where(g => ((OrderTypeObject)g).grouping.Equals(group))
                                                    .Select(t => ((OrderTypeObject)t).orderDescription).ToArray());
                }


                if (!String.IsNullOrEmpty(existingOrderValue))
                    orderComboBox.setText(existingOrderValue);
            }

        }

        void OrderRow_Finalized(object sender, OrdersView.FinalizedEventArgs e)
        {
            //Finalize has been clicked
            if (order.Order_finalized == 0)
            {
                order.Order_finalized = 1;
                Patient proband = SessionManager.Instance.GetActivePatient();
                HraModelChangedEventArgs args = new HraModelChangedEventArgs(this.owningView);

                int panelID = SessionManager.Instance.MetaData.GeneticTests.GetPanelIDFromName(order.Order_orderDesc);
                if (panelID > 0)  // a genetic test has been ordered
                {
                    //add the pending gen test to the model
                    PastMedicalHistory pmh = proband.PMH;

                    
                    //GeneticTest geneticTest = (GeneticTest)(pmh.GeneticTests.SingleOrDefault(v => ((GeneticTest)v).panelID == panelID && ((GeneticTest)v).status == "Pending"
                    //    && ((GeneticTest)v).GeneticTest_testYear == (order.Order_orderDate.Year).ToString()
                    //    && ((GeneticTest)v).GeneticTest_testMonth == (order.Order_orderDate.Month).ToString()
                    //    && ((GeneticTest)v).GeneticTest_testDay == (order.Order_orderDate.Day).ToString()));
                    //if (geneticTest == null)
                    //{

                    //duplicate pending tests are allowed, so ignore commented check above
                    //also, no difference between all "Familial Known Genetic Test"s, regardless of group
                        GeneticTest geneticTest = new GeneticTest(pmh);
                        geneticTest.GeneticTest_status = "Pending";
                        geneticTest.GeneticTest_panelID = panelID;
                        geneticTest.GeneticTest_testYear = (order.Order_orderDate.Year).ToString();
                        geneticTest.GeneticTest_testMonth = (order.Order_orderDate.Month).ToString();
                        geneticTest.GeneticTest_testDay = (order.Order_orderDate.Day).ToString();

                        pmh.GeneticTests.AddToList(geneticTest, args);
                    //}
                    //else
                    //{
                    //    geneticTest.SignalModelChanged(args);
                    //}
                }

                else if (order.Order_orderDesc.Contains("mammo"))
                {
                    BreastImagingStudy bis = new BreastImagingStudy();
                    bis.unitnum = proband.unitnum;
                    bis.type = "MammographyHxView";
                    bis.date = DateTime.Today;
                    bis.imagingType = "MammographyHxView";
                    bis.status = "Ordered";
                    proband.breastImagingHx.AddToList(bis, args);
                }
                else if (order.Order_orderDesc.Contains("MRI"))
                {
                    BreastImagingStudy bis = new BreastImagingStudy();
                    bis.unitnum = proband.unitnum;
                    bis.type = "MRI";
                    bis.date = DateTime.Today;
                    bis.imagingType = "MRI";
                    bis.status = "Ordered";
                    bis.side = "Bilateral";
                    proband.breastImagingHx.AddToList(bis, args);
                }
                else if (order.Order_orderDesc.Contains("Transvaginal Sonography"))
                {
                    TransvaginalImagingStudy tvs = new TransvaginalImagingStudy();
                    tvs.unitnum = proband.unitnum;
                    tvs.type = "TVS";
                    tvs.date = DateTime.Today;
                    tvs.imagingType = "TVS";
                    tvs.status = "Ordered";
                    proband.transvaginalImagingHx.AddToList(tvs, args);

                }
                else if (order.Order_orderDesc.Contains("CA-125"))
                {
                    LabResult lr = new LabResult();
                    lr.unitnum = proband.unitnum;
                    lr.date = DateTime.Today;
                    lr.TestDesc = "CA125";
                    lr.status = "Ordered";
                    proband.labsHx.AddToList(lr, args);
                }
            }
        }

        public void hideDeleteButton()
        {
            deleteButton.Visible = false;
            orderComboBox.Enabled = false;
        }

        private void orderComboBox_Leave(object sender, EventArgs e)
        {
            //String value = RiskAppCore.DBUtils.makeSQLSafe();
            order.Order_orderDesc = orderComboBox.Text; // value;
            order.Order_orderGrouping = group;
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using RiskApps3.Controllers;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model;
using RiskApps3.Model.MetaData;
using Transitions;
using System.Runtime.InteropServices;
using System.Diagnostics;
using RiskApps3.Utilities;
using RiskApps3.View.Common.AutoSearchTextBox;
using RiskApps3.Model.SurgicalClinic;
using RiskClinicApp;


namespace RiskApps3.View.PatientRecord
{
    public partial class OrdersView : HraView
    {
        private PtOrderList orders;
        delegate void fillControlsCallback();
        public event EventHandler<FinalizedEventArgs> Finalized;
        OrderTypesList otl;

        private String selectedGroup = "All Groups";

        //private CreatedSurgery createdSurgery;

        public OrdersView()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            //this.createdSurgery = new CreatedSurgery();
        }

        private void OrdersView_Load(object sender, EventArgs e)
        {
            if (!ViewClosing)
            {
                SessionManager.Instance.MetaData.OrderTypes.AddHandlersWithLoad(null, OrderTypesLoaded, null);

                getNewOrderList();

                FillControls();
            }
        }


        public PtOrderList getPtOrderList()
        {
            return orders;
        }

        
        private void OrdersView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SessionManager.Instance.MetaData.OrderTypes.ReleaseListeners(this);
            if (orders != null)
                orders.ReleaseListeners(this);
            SessionManager.Instance.RemoveHraView(this);
        }

        private void OrderTypesLoaded(HraListLoadedEventArgs e)
        {
            otl = SessionManager.Instance.MetaData.OrderTypes;

            groupComboBox.Clear();
            AutoCompleteUtils.fillComboBoxFromArray(groupComboBox, otl.Select(g => ((OrderTypeObject)g).grouping).Distinct().ToArray());
            groupComboBox.addItem("All Groups");

            groupComboBox.setText("All Groups");
        }

        /**************************************************************************************************/
        public void FillControls()
        {
            if (loadingCircle1.InvokeRequired)
            {
                fillControlsCallback fcc = new fillControlsCallback(FillControls);
                this.Invoke(fcc, null);
            }
            else
            {
                loadingCircle1.Active = false;
                loadingCircle1.Visible = false;

                flowLayoutPanel1.Controls.Clear();
                flowLayoutPanel2.Controls.Clear();

                foreach (Order order in orders.ToList())
                {
                    OrderRow newRow = new OrderRow(order, this);
                    if (order.Order_finalized == 1)
                    {
                        newRow.hideDeleteButton();
                        flowLayoutPanel2.Controls.Add(newRow);
                    }
                    else
                    {
                        flowLayoutPanel1.Controls.Add(newRow);
                    }

                    //Application.DoEvents();
                }
            }
        }

        private void getNewOrderList()
        {
            loadingCircle1.Enabled = true;
            loadingCircle1.Visible = true;
            loadingCircle1.Active = true;

            if (orders != null)
                orders.ReleaseListeners(this);

            orders = new PtOrderList();

            orders.userLogin = SessionManager.Instance.ActiveUser.userLogin;
            orders.groupName = SessionManager.Instance.UserGroup;
            orders.orderDate = null; //get all existing orders

            orders.AddHandlersWithLoad(OrdersListChanged,
                                         OrdersListLoaded,
                                         OrderChanged);
        }

        private void OrdersListChanged(HraListChangedEventArgs e)
        {
            if (e.hraOperand != null)
            {
                Order order = (Order)e.hraOperand;

                switch (e.hraListChangeType)
                {
                    case HraListChangedEventArgs.HraListChangeType.ADD:
                            OrderRow newRow = new OrderRow(order, this);

                            if (order.Order_finalized == 0)
                            {
                                ////if there aren't too many existing orders, use animation to add a new row
                                //noExistingOrdersLabel.Visible = false;
                                //if (((countOfOrderRows() + 1) * (newRow.Margin.Top + newRow.Margin.Bottom + newRow.Height)) < flowLayoutPanel1.Height)
                                //{
                                //    int origNewRowMarginTop = newRow.Margin.Top;
                                //    newRow.MarginTop = flowLayoutPanel1.Height - ((countOfOrderRows() + 1) * (newRow.Margin.Top + newRow.Margin.Bottom + newRow.Height));
                                //    flowLayoutPanel1.Controls.Add(newRow);

                                //    Transition t = new Transition(new TransitionType_Acceleration(100));
                                //    t.add(newRow, "MarginTop", origNewRowMarginTop);
                                //    t.run();
                                //}
                                //else
                                //{
                                    flowLayoutPanel1.Controls.Add(newRow);
                                    setNoOrdersLabels();
                                //}

                            }
                            else
                            {
                                newRow.hideDeleteButton();
                                flowLayoutPanel2.Controls.Add(newRow);
                            }
                        break;
                    case HraListChangedEventArgs.HraListChangeType.DELETE:
                        Control doomed = null;
                        foreach (Control c in flowLayoutPanel1.Controls)
                        {
                            if (c is OrderRow)
                            {
                                OrderRow targetRow = (OrderRow)c;
                                if (targetRow.getOrder() == order)
                                {
                                    doomed = c;
                                    break;  //presumes only one deleted
                                }
                            }
                        }
                        if (doomed != null)
                        {
                            flowLayoutPanel1.Controls.Remove(doomed);
                            break;
                        }

                        foreach (Control c in flowLayoutPanel2.Controls)
                        {
                            if (c is OrderRow)
                            {
                                OrderRow targetRow = (OrderRow)c;
                                if (targetRow.getOrder() == order)
                                {
                                    doomed = c;
                                    break;  //presumes only one deleted
                                }
                            }
                        }
                        if (doomed != null)
                            flowLayoutPanel2.Controls.Remove(doomed);
                        break;
                }

                setNoOrdersLabels();
            }
        }

        /**************************************************************************************************/
        private void OrdersListLoaded(HraListLoadedEventArgs e)
        {
            FillControls();
            setNoOrdersLabels();
        }

        /**************************************************************************************************/

        private void OrderChanged(object sender, HraModelChangedEventArgs e)
        {
            Boolean foundOne = false;
            do
            {
                foundOne = false;
                foreach (Control c in flowLayoutPanel1.Controls)
                {
                    if (c is OrderRow)
                    {
                        Order prevExistingOrder = ((OrderRow)c).getOrder();
                        if (prevExistingOrder == sender)
                        {
                            if (((Order)sender).Order_finalized == 1)
                            {
                                flowLayoutPanel1.Controls.Remove(c);
                                ((OrderRow)c).hideDeleteButton();
                                flowLayoutPanel2.Controls.Add(c);
                                ((OrderRow)c).SetOrder((Order)sender);
                                foundOne = true;
                                break;
                            }
                        }
                    }
                }
            } while (foundOne);

            setNoOrdersLabels();

            //foreach (Control c in flowLayoutPanel2.Controls)
            //{
            //    if (c is OrderRow)
            //    {
            //        if (((OrderRow)c).getOrder() == sender)
            //        {
            //            ((OrderRow)c).SetOrder((Order)sender);
            //        }
            //    }
            //}
        }

        private void setNoOrdersLabels()
        {
            //not sure why this is needed, but doesn't work w/o it
            if (noExistingOrdersLabel.Parent == null)
                noExistingOrdersLabel.Parent = flowLayoutPanel1;
            //

            bool noOrdersToday = true;
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is OrderRow)
                {
                    noOrdersToday = false;
                    break;
                }
            }
            noExistingOrdersLabel.Visible = noOrdersToday;
            finalizeButton.Enabled = !noOrdersToday;

            bool noOrdersPast = true;
            foreach (Control c in flowLayoutPanel2.Controls)
            {
                if (c is OrderRow)
                {
                    noOrdersPast = false;
                    break;
                }
            }
            noExistingPastOrdersLabel.Visible = noOrdersPast;
        }

        private void addNewOrderButton_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = todaysOrdersTabPage;
            Order order = new Order();

            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Persist = false;
            orders.AddToList(order, args);
        }

        private int countOfOrderRows()
        {
            int count = 0;
            foreach (Control c in flowLayoutPanel1.Controls)
            {
                if (c is OrderRow)
                    count++;
            }
            return count;
        }

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = "RiskAppUtils.exe";
            process.StartInfo.Arguments = "v_3_GenerateOrders " + SessionManager.Instance.GetActivePatient().apptid.ToString();
            process.Start();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            SetForegroundWindow(process.MainWindowHandle);
            process.WaitForExit();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            getNewOrderList();
            FillControls();
            loadingCircle1.Active = false;
            loadingCircle1.Visible = false;

            generateOrdersButton.Enabled = true;
        }

        private void generateOrdersButton_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogResult.Yes;

            //determine if orders have already been generated previously            
            ValueHolder valHolder = new ValueHolder("");
            ParameterCollection pc = new ParameterCollection("unitnum", SessionManager.Instance.GetActivePatient().unitnum);
            pc.Add("table", "tblSurgicalClinic");
            pc.Add("column", "createdOrders");
            //pull one value from the database (fast)
            valHolder.DoLoadWithSpAndParams("sp_3_LoadRelativeValue", pc);
            Boolean alreadyGeneratedOrders = ((valHolder.relativeValue).ToLower() == "yes");

            if (alreadyGeneratedOrders)
            {
                //optionally set the DialogBox result to not "Yes"
                result = MessageBox.Show("Overwrite orders generated previously?", "RiskApps3", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            }

            if (result == DialogResult.Yes)
            {
                generateOrdersButton.Enabled = false;
                loadingCircle1.Active = true;
                loadingCircle1.Visible = true;

                backgroundWorker1.RunWorkerAsync();
            }
        }

        private class ValueHolder : HraObject
        {
            public string relativeValue;

            public ValueHolder(string s)
            {
                relativeValue = s;
            }
        }

        private void groupComboBox_Load(object sender, EventArgs e)
        {
            groupComboBox.setSelectedIndexChangedEventHandler(groupComboBox_SelectedIndexChanged);
        }

        private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedGroup = groupComboBox.Text;

        }

        private void finalizeButton_Click(object sender, EventArgs e)
        {
            //fire the event; all OrderRows s/b listening
            FinalizedEventArgs args = new FinalizedEventArgs();
            OnFinalized(args);
        }

        protected virtual void OnFinalized(FinalizedEventArgs e)
        {
            EventHandler<FinalizedEventArgs> handler = Finalized;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public class FinalizedEventArgs : EventArgs
        {
            //doesn't need to do anything
        }

        private void generateDocumentsButton_Click(object sender, EventArgs e)
        {
            Patient proband = SessionManager.Instance.GetActivePatient();
            if (proband != null)
            {
                RiskAppCore.Globals.setApptID(proband.apptid);
                RiskAppCore.Globals.setUnitNum(proband.unitnum);
                //RiskAppCore.DecisionSupport.fillInComputerRecommendations();
                PrintRiskClinicDocumentsForm pdf = new PrintRiskClinicDocumentsForm();
                pdf.Setup();
                pdf.ShowDialog();
                RiskAppCore.Globals.setApptID(-1);
                RiskAppCore.Globals.setUnitNum("");
            }
        }
    }
}

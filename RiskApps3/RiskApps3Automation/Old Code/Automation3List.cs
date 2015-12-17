using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;

using RiskApps3.Utilities;
using RiskApps3.Controllers;

namespace RiskApps3Automation
{
    public partial class Automation3List : UserControl
    {
        public Automation3List()
        {
            InitializeComponent();

            if (Process.GetCurrentProcess().ProcessName == "devenv")
            {
                return;
            }
            refreshGrid();
        }

        public void refreshGrid()
        {
            try
            {
                String CustomAutomationString = Configurator.getNodeValue("DatabaseInfo", "AUTOMATION_SQL");
                if (string.IsNullOrEmpty(CustomAutomationString) == false)
                    Automation3.AUTOMATION_SQL = CustomAutomationString;

                dataGridView.DataSource = BCDB2.Instance.getDataTable(Automation3.AUTOMATION_SQL);

                numberLabel.Text = "" + dataGridView.RowCount;
                if (dataGridView.ColumnCount == 0)
                {
                    return;
                }

                int columnIndex = 0;

                dataGridView.Columns[columnIndex].Visible = true;
                dataGridView.Columns[columnIndex].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9,
                                                                                   FontStyle.Bold);
                dataGridView.Columns[columnIndex].HeaderText = "ApptID";
                dataGridView.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView.Columns[columnIndex].Width = 70;

                columnIndex++;

                dataGridView.Columns[columnIndex].Visible = true;
                dataGridView.Columns[columnIndex].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9,
                                                                                   FontStyle.Bold);
                dataGridView.Columns[columnIndex].HeaderText = "Patient Name";
                dataGridView.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView.Columns[columnIndex].Width = 250;

                columnIndex++;
                dataGridView.Columns[columnIndex].Visible = true;
                dataGridView.Columns[columnIndex].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9,
                                                                                   FontStyle.Bold);
                dataGridView.Columns[columnIndex].HeaderText = "MRN";
                dataGridView.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView.Columns[columnIndex].Width = 120;

                columnIndex++;
                dataGridView.Columns[columnIndex].Visible = true;
                dataGridView.Columns[columnIndex].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9,
                                                                                   FontStyle.Bold);
                dataGridView.Columns[columnIndex].HeaderText = "DOB";
                dataGridView.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView.Columns[columnIndex].Width = 80;

                columnIndex++;
                dataGridView.Columns[columnIndex].Visible = true;
                dataGridView.Columns[columnIndex].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9,
                                                                                   FontStyle.Bold);
                dataGridView.Columns[columnIndex].HeaderText = "Appt Date";
                dataGridView.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView.Columns[columnIndex].Width = 120;

                columnIndex++;
                dataGridView.Columns[columnIndex].Visible = true;
                dataGridView.Columns[columnIndex].HeaderCell.Style.Font = new Font("Microsoft Sans Serif", 9,
                                                                                   FontStyle.Bold);
                dataGridView.Columns[columnIndex].HeaderText = "Completed";
                dataGridView.Columns[columnIndex].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridView.Columns[columnIndex].Width = 120;
            }
            catch (Exception e)
            {
                logAutomation3Exception("", e);
            }
        }

        // TBD: Create a static utilities class with a logException() method. Put one in RiskApps3.Utilities.Logger???
        //
        private void logAutomation3Exception(String message, Exception e)
        {
            // get call stack
            StackTrace stackTrace = new StackTrace();

            // get calling method name
            String callingRoutine = stackTrace.GetFrame(1).GetMethod().Name;
            Logger.Instance.WriteToLog("[RiskAppsAutomation3] from [" + callingRoutine + "] " + message + "'\n\t" + e);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RiskApps3.Utilities;
using RiskApps3.Model.MetaData;

namespace RiskApps3.View.Admin
{
    public partial class QueueParameterRow : UserControl
    {
        QueueParameter queueParameter;
        bool initializing = false;

        public QueueParameterRow()
        {
            InitializeComponent();
        }
        public QueueParameterRow(QueueParameter queueParameter)
        {
            initializing = true;

            InitializeComponent();

            this.queueParameter = queueParameter;
            IdLabel.Text = queueParameter.ID.ToString();
            rulesLabel.Text = queueParameter.rulesName;
            parameterLabel.Text = queueParameter.parameterName;
            valueIntegerTextBox.Text = ((int)queueParameter.parameterValue).ToString();

            initializing = false;
        }

        private void valueIntegerTextBox_TextChanged(object sender, EventArgs e)
        {
            double value = 0;
            if (double.TryParse(valueIntegerTextBox.Text, out value) == true)
            {
                queueParameter.parameterValue = value;
                RiskApps3.Model.HraModelChangedEventArgs args = new RiskApps3.Model.HraModelChangedEventArgs(null);
                queueParameter.BackgroundPersistWork(args);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RiskApps3.Utilities
{
    public class toolsStripDateTimePicker: ToolStripControlHost
    {
        private FlowLayoutPanel controlPanel;
        private DateTimePicker picker = new DateTimePicker();

        public toolsStripDateTimePicker()
            : base(new FlowLayoutPanel())
        {
            // Set up the FlowLayouPanel.
            controlPanel = (FlowLayoutPanel)base.Control;
            controlPanel.BackColor = Color.Transparent;
 
            // Add two child controls.
            controlPanel.Controls.Add(picker);
        }
 
        public DateTime Value
        {
            get { return this.picker.Value; }
            set { this.picker.Value = value; }
        }
 
        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
 
            //Add your code here to subsribe Control Events
        }
 
        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnUnsubscribeControlEvents(control);
 
            //Add your code here to unsubscribe control events.
        }
 
    }
}
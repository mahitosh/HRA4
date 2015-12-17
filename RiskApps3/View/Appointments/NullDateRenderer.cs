using BrightIdeasSoftware;
using RiskApps3.Model.PatientRecord;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace RiskApps3.View.Appointments
{
    public class NullDateRenderer : BaseRenderer
    {
        public override void Render(Graphics g, Rectangle r)
        {


            Appointment a = (Appointment)this.RowObject;
            if (a.riskdatacompleted > DateTime.MinValue)
                base.Render(g, r);
            else
                g.FillRectangle(Brushes.White, r);

        }
    }
}

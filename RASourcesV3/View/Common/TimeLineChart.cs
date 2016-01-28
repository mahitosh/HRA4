using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotnetCHARTING.WinForms;
using System.Drawing;

namespace RiskApps3.View.Common
{
    public class TimeLineChart : Chart
    {
        //constructor
        public TimeLineChart()
        {
            this.Application = "SZ7Xdm7cflDtBo4+WqVIPuWTiHM5kccphrHW2GjvTk4bKM4ygUUoF+CkZOtrPEEtVQERA/1RWBUebtVBt4PLmQCFIDsJrzpm1pCPKOH1Ad8=";
            this.TempDirectory = "temp";
            this.Debug = true;
            this.Palette = new Color[] { Color.Gray, Color.FromArgb(255, 255, 0), Color.FromArgb(255, 99, 49), Color.FromArgb(0, 156, 255) };
            this.Type = dotnetCHARTING.WinForms.ChartType.Combo;
            this.XAxis.Scale = dotnetCHARTING.WinForms.Scale.Time;

            this.LegendBox.Visible = false;
            this.YAxis.Clear();
            this.ChartArea.ClearColors();
            this.ChartArea.Background.Color = Color.White;
            this.ChartArea.Line.Color = Color.Black;
            this.ChartArea.Label.Text = "";
            this.XAxis.Line.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.XAxis.Line.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            this.XAxis.Line.AnchorCapScale = 4;
            this.XAxis.StaticColumnWidth = 1;
            this.XAxis.TimeScaleLabels.Mode = dotnetCHARTING.WinForms.TimeScaleLabelMode.Smart;

            this.DefaultElement.Annotation = new dotnetCHARTING.WinForms.Annotation("%Name");
            this.DefaultElement.Annotation.HeaderLabel.Text = " <%XValue,d>";
            this.DefaultElement.Annotation.HeaderBackground.Color = Color.DarkOliveGreen;
            this.DefaultElement.Annotation.HeaderLabel.Font = new Font("Arial", 10);
            this.DefaultElement.Annotation.HeaderBackground.ShadingEffectMode = dotnetCHARTING.WinForms.ShadingEffectMode.Four;
            this.DefaultElement.Annotation.Padding = 4;
            this.DefaultElement.Annotation.CornerTopLeft = dotnetCHARTING.WinForms.BoxCorner.Round;
            this.DefaultElement.Annotation.Background.Color = Color.White;
        }

       
    }
}

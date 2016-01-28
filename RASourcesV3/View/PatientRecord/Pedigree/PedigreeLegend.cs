using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;
using RiskApps3.Model.PatientRecord.PMH;
using RiskApps3.Utilities;

namespace RiskApps3.View.PatientRecord.Pedigree
{
    public partial class PedigreeLegend : PedigreeComponent
    {
        private const int EdgeMargin = 40;
        private int radius = 8;
        private int legendPadding = 12;

        Font legendFont = new Font("Tahoma", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));

        private PedigreeTitleBlock _titleBlockReference;

        public PedigreeLegend(PedigreeTitleBlock titleBlockReference)
        {
            _titleBlockReference = titleBlockReference;
            InitializeComponent();
        }

        public int LegendRadius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
                foreach (Control c in flowLayoutPanel.Controls)
                {
                    PedigreeLegendRow plr = (PedigreeLegendRow)c;
                    plr.Radius = value;
                }
            }
        }
        public Font LegendFont
        {
            get
            {
                return legendFont;
            }
            set
            {
                legendFont = value;
                foreach (Control c in flowLayoutPanel.Controls)
                {
                    PedigreeLegendRow plr = (PedigreeLegendRow)c;
                    plr.LegendFont = value;
                }
            }
        }
        public Color Background
        {
            get
            {
                return BackColor;
            }
            set
            {
                flowLayoutPanel.BackColor = value;
                RefreshMe();
            }
        }
        /**************************************************************************************************/
        delegate void RefreshMeCallback();
        private void RefreshMe()
        {
            if (this.InvokeRequired)
            {
                RefreshMeCallback rmc = new RefreshMeCallback(RefreshMe);
                this.Invoke(rmc, null);
            }
            else
            {
                Refresh();
            }
        }
        
        delegate void AddObservationsCallback(ClinicalObservationList dxList);
        public void AddObservations(ClinicalObservationList dxList)
        {
            if (flowLayoutPanel.InvokeRequired)
            {
                AddObservationsCallback aoc = AddObservations;
                object[] args = new object[1];
                args[0] = dxList;
                this.Invoke(aoc, args);
            }
            else
            {
                lock (dxList)
                {
                    foreach (ClincalObservation co in dxList)
                    {
                        AddClinicalObservation(co);
                    }
                    CheckForEmpty();
                }
                CalculateOptimalDimensions();
            }
        }

        delegate void AddObservationsGenericCallback(IEnumerable<ClincalObservation> dxList);
        internal void AddObservations(IEnumerable<ClincalObservation> enumerable)
        {
            if (this.flowLayoutPanel.InvokeRequired)
            {
                AddObservationsGenericCallback callback = AddObservations;
                object[] args = new object[1];
                args[0] = enumerable;
                this.Invoke(callback, args);
            }
            else
            {
                foreach (ClincalObservation co in enumerable)
                {
                    AddClinicalObservation(co);
                }
                CheckForEmpty();
            }
            CalculateOptimalDimensions();
        }

        delegate void AddSingleObservationCallback(ClincalObservation dx, bool isNew);
        public void AddSingleObservation(ClincalObservation dx, bool isNew)
        {
            if (flowLayoutPanel.InvokeRequired)
            {
                AddSingleObservationCallback aoc = AddSingleObservation;
                object[] args = new object[1];
                args[0] = dx;
                args[1] = isNew;
                this.Invoke(aoc, args);
            }
            else
            {
                AddClinicalObservation(dx);
                CalculateOptimalDimensions();
                CheckForEmpty();
            }
        }

        private void AddClinicalObservation(ClincalObservation co)
        {
            var add = ClinicalObservationDoesNotExist(co);
            if (add)
            {
                if (string.IsNullOrEmpty(co.disease) == false)
                {
                    PedigreeLegendRow plr = new PedigreeLegendRow();
                    plr.SetObservation(co);
                    flowLayoutPanel.Controls.Add(plr);
                    this.Visible = true;
                }
            }
        }

        private bool ClinicalObservationDoesNotExist(ClincalObservation co)
        {
            bool add = true;
            foreach (Control c in flowLayoutPanel.Controls)
            {
                PedigreeLegendRow plr = (PedigreeLegendRow) c;
                string plrDisplayName = plr.GetObservationisplayName();
                string plrShortName = plr.GetObservationisplayShortName();
                if (ClinicalObservationIsMatch(co, plrDisplayName, plrShortName))
                {
                    add = false;
                    break;
                }
            }
            return add;
        }

        private static bool ClinicalObservationIsMatch(ClincalObservation co, string plrDisplayName, string plrShortName)
        {
            //TODO consider adding to ClinicalObservation.Equals
            return string.Compare(co.ClinicalObservation_diseaseDisplayName, plrDisplayName, true) == 0 &&
                   string.Compare(co.ClinicalObservation_diseaseShortName, plrShortName) == 0;
        }

        /**************************************************************************************************/
        delegate void CheckForEmptyCallback();
        public void CheckForEmpty()
        {
            if (flowLayoutPanel.InvokeRequired)
            {
                CheckForEmptyCallback aoc = CheckForEmpty;
                this.Invoke(aoc);
            }
            else
            {
                if (flowLayoutPanel.Controls.Count == 0)
                    this.Visible = false;
                else
                    this.Visible = true;
            }
        }
        
        public void ClearObservations()
        {
            flowLayoutPanel.Controls.Clear();
            this.Visible = false;
        }
        internal delegate void RemoveObservationCallback(ClincalObservation co);
        internal void RemoveObservation(ClincalObservation co)
        {
            if (flowLayoutPanel.InvokeRequired)
            {
                RemoveObservationCallback aoc = new RemoveObservationCallback(RemoveObservation);
                object[] args = new object[1];
                args[0] = co;
                this.Invoke(aoc, args);
            }
            else
            {
                Control toBeRemoved = null;
                int maxWidth = 0;
                int totalHeight = 0;
                foreach (Control c in flowLayoutPanel.Controls)
                {
                    PedigreeLegendRow plr = (PedigreeLegendRow)c;
                    string plrDisplayName = plr.GetObservationisplayName();
                    string plrShortName = plr.GetObservationisplayShortName();
                    if (ClinicalObservationIsMatch(co, plrDisplayName, plrShortName))
                    {
                        toBeRemoved = c;
                    }
                }
                if (toBeRemoved != null)
                {
                    flowLayoutPanel.Controls.Remove(toBeRemoved);
                }

                CalculateOptimalDimensions();
                CheckForEmpty();
            }
        }

        internal void DrawToBitmapWithBorder(Bitmap image, Rectangle rectangle, float scalingFactor = 1)
        {
            if (rectangle.X < 0)
                rectangle.X = 0;
            if (rectangle.Y < 0)
                rectangle.Y = 0;

            //rectangle.Size = new Size(image.Width - (EdgeMargin * 2), rectangle.Size.Height / 2);
            this.Size = rectangle.Size;
            this.flowLayoutPanel.Size = rectangle.Size;
            this.legendTitleLabel.Size = this.legendTitleLabel.Size.Scale(scalingFactor);
            this.legendTitleLabel.Font = this.legendTitleLabel.Font.Scale(scalingFactor);
            this.flowLayoutPanel.Controls.OfType<PedigreeLegendRow>().ToList().ForEach(row =>row.ScaleByFactor(scalingFactor));
            this.Refresh();

            Graphics g = Graphics.FromImage(image);
            DrawToBitmap(image, rectangle);
            g.DrawRectangle(Pens.Black, rectangle);
        }

        private bool _isHorizontal = true;

        void PedigreeLegend_Resize(object sender, EventArgs e)
        {
            if (Height == 0 || Width == 0) return;  //not initialized yet - don't do anything
            if (this.Width > this.Height)
            {
                _isHorizontal = true;
                this.flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            }
            else
            {
                _isHorizontal = false;
                this.flowLayoutPanel.FlowDirection = FlowDirection.TopDown;
            }
        }

        /// <summary>
        /// Determines and sets the best height and width for the legend.  Also returns the height for drawing purposes.
        /// </summary>
        /// <remarks>To be used in the dynamic layout where width is variable.</remarks>
        private void CalculateOptimalDimensions()
        {
            int width;
            if (ConjureUsableWidth(out width))
            {
                int titleBlockOffset = GetTitleBlockOffset();
                CalculateOptimalDimensions(width - titleBlockOffset);
            }
        }
        
        private int GetTitleBlockOffset()
        {
            return this.Location.X + EdgeMargin;
        }

        private bool ConjureUsableWidth(out int width)
        {
            if (this.Parent != null)
            {
                width = this.Parent.Width;
                return true;
            }
            else
            {
                width = 0;
                return false;
            }
        }

        /// <summary>
        /// Determines and sets the best height and width for the legend.  Also returns the height for drawing purposes.
        /// </summary>
        /// <param name="usableWidth"></param>
        /// <param name="forceHorizontalLayout">ignore user adjustments to aspect ratio and force horizontal layout</param>
        /// <remarks>To be used in the static layout where we know what the available width is.</remarks>
        internal int CalculateOptimalDimensions(int usableWidth, bool forceHorizontalLayout = false)
        {
            //once in a while this is < 0 because the parent can initialize tiny at first and then resize after
            if (usableWidth < 200)
            {
                return 0;
            }

            var pedigreeLegendRows = this.flowLayoutPanel.Controls.OfType<PedigreeLegendRow>();
            var pedigreeLegendItems = pedigreeLegendRows as IList<PedigreeLegendRow> ?? pedigreeLegendRows.ToList();

            int controlCount = pedigreeLegendItems.Count();
            if (controlCount > 0)
            {
                this.Visible = true;

                if (this._isHorizontal || forceHorizontalLayout)
                {
                    return CalculateOptimateDimensionsHorizontalLayout(usableWidth, pedigreeLegendItems);
                }
                else
                {
                    return CalculateOptimalDimensionsVerticalLayout(pedigreeLegendItems);
                }
            }
            else
            {
                this.Visible = false;
                return 0;
            }
        }

        private int CalculateOptimalDimensionsVerticalLayout(IList<PedigreeLegendRow> pedigreeLegendItems)
        {
            int rowHeight =
                pedigreeLegendItems.Sum(
                    item =>
                        item.Height +
                        item.Padding.Top +
                        item.Padding.Bottom +
                        item.Margin.Top +
                        item.Margin.Bottom);

            int totalHeight =
                rowHeight +
                this.legendTitleLabel.Height +
                this.legendTitleLabel.Padding.Top +
                this.legendTitleLabel.Padding.Bottom +
                this.legendTitleLabel.Margin.Top +
                this.legendTitleLabel.Margin.Bottom + 
                this.flowLayoutPanel.Padding.Top + 
                this.flowLayoutPanel.Padding.Bottom +
                this.flowLayoutPanel.Margin.Top + 
                this.flowLayoutPanel.Margin.Bottom;

            int width =
                pedigreeLegendItems.Max(
                    item =>
                        item.Width +
                        item.Padding.Left +
                        item.Padding.Right +
                        item.Margin.Left +
                        item.Margin.Right);

            this.Width = width;
            this.flowLayoutPanel.Width = width;
            this.Height = totalHeight;
            this.flowLayoutPanel.Height = totalHeight;

            return totalHeight;
        }

        private int CalculateOptimateDimensionsHorizontalLayout(int usableWidth, IList<PedigreeLegendRow> pedigreeLegendItems)
        {
            this.Width = usableWidth;
            this.flowLayoutPanel.Width = usableWidth;

            int maxPlrHeight = pedigreeLegendItems.Max(plr => plr.Height);
            int rowCount = CalculateRowCount(usableWidth, pedigreeLegendItems);

            int height = CalculateHeight(pedigreeLegendItems, rowCount, maxPlrHeight);

            this.Height = height;
            this.flowLayoutPanel.Height = height;

            this.Refresh();
            return height;
        }

        private int CalculateRowCount(int usableWidth, IEnumerable<PedigreeLegendRow> pedigreeLegendItems)
        {
            int widthUsedByCurrentRow = 0;
            int rowCount = 1;
            foreach (var item in pedigreeLegendItems)
            {
                if (ItemFitsInRow(item, widthUsedByCurrentRow, usableWidth))
                {
                    widthUsedByCurrentRow += item.Width;
                }
                else
                {
                    widthUsedByCurrentRow = item.Width;
                    rowCount++;
                }
            }
            return rowCount;
        }

        private int CalculateHeight(IList<PedigreeLegendRow> pedigreeLegendItems, int rowCount, int maxPlrHeight)
        {
            int legendHeight = this.legendTitleLabel.Height;

            int titlePadding = this.legendTitleLabel.Padding.Top + this.legendTitleLabel.Padding.Bottom;
            int titleMargin = this.legendTitleLabel.Margin.Top + this.legendTitleLabel.Margin.Bottom;
            int rowPadding = pedigreeLegendItems.First().Padding.Top + pedigreeLegendItems.First().Padding.Bottom;
            int rowMargin = pedigreeLegendItems.First().Margin.Top + pedigreeLegendItems.First().Margin.Bottom;

            int height = (rowCount*(maxPlrHeight + rowPadding + rowMargin)) + //height of pedigree legend rows
                         legendHeight + titlePadding + titleMargin + //title height
                         ((rowPadding + rowMargin)*2); //whitespace height
            return height;
        }

        private bool ItemFitsInRow(PedigreeLegendRow item, int widthUsedByCurrentRow, int usableWidth)
        {
            int itemWidth = item.Width + item.Padding.Left + item.Padding.Right + item.Margin.Left + item.Margin.Right;
            int realWidth = usableWidth -
                            (this.flowLayoutPanel.Margin.Right +
                             this.flowLayoutPanel.Margin.Left +
                             this.flowLayoutPanel.Padding.Left +
                             this.flowLayoutPanel.Padding.Right);

            if (realWidth - widthUsedByCurrentRow >= itemWidth)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class PedigreeFlowLayoutPanel : FlowLayoutPanel
    {
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = (-1);
            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
            }
            else
            {
                base.WndProc(ref m);
            }
        } 
    }
}


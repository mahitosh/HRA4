using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace RiskApps3.View.Common.AutoSearchTextBox
{
    /// <summary>
    /// Summary description for CoolTextBox.
    /// </summary>
    public class CoolTextBox : UserControl {
        private Color borderColor = Color.LightSteelBlue;

        public Color BorderColor {
            get { return borderColor; }
            set {
                if (borderColor != value) {
                    borderColor = value;
                    Invalidate();
                }
            }
        }

        public Color SelectedItemBackColor {
            get { return autoCompleteTextBox1.PopupSelectionBackColor; }
            set { autoCompleteTextBox1.PopupSelectionBackColor = value; }
        }

        public Color SelectedItemForeColor {
            get { return autoCompleteTextBox1.PopupSelectionForeColor; }
            set { autoCompleteTextBox1.PopupSelectionForeColor = value; }
        }

        [Editor(typeof (AutoCompleteEntryCollection.AutoCompleteEntryCollectionEditor), typeof (UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public AutoCompleteEntryCollection Items {
            get { return autoCompleteTextBox1.Items; }
            set { autoCompleteTextBox1.Items = value; }
        }

        [Editor(typeof (AutoCompleteTriggerCollection.AutoCompleteTriggerCollectionEditor), typeof (UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public AutoCompleteTriggerCollection Triggers {
            get { return autoCompleteTextBox1.Triggers; }
            set { autoCompleteTextBox1.Triggers = value; }
        }

        [Browsable(true)]
        public override string Text {
            get { return autoCompleteTextBox1.Text; }
            set { autoCompleteTextBox1.Text = value; }
        }

        public override Color ForeColor {
            get { return autoCompleteTextBox1.ForeColor; }
            set { autoCompleteTextBox1.ForeColor = value; }
        }

        public override ContextMenu ContextMenu {
            get { return autoCompleteTextBox1.ContextMenu; }
            set { autoCompleteTextBox1.ContextMenu = value; }
        }

        public int PopupWidth {
            get { return autoCompleteTextBox1.PopupWidth; }
            set { autoCompleteTextBox1.PopupWidth = value; }
        }

        private AutoCompleteTextBox autoCompleteTextBox1;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components = null;

        public CoolTextBox() {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            Rectangle rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            Pen p = new Pen(BorderColor);
            e.Graphics.DrawRectangle(p, rect);

            p = new Pen(Color.FromArgb(100, BorderColor));
            rect.Inflate(-1, -1);
            e.Graphics.DrawRectangle(p, rect);

            p = new Pen(Color.FromArgb(45, BorderColor));
            rect.Inflate(-1, -1);
            e.Graphics.DrawRectangle(p, rect);

            p = new Pen(Color.FromArgb(15, BorderColor));
            rect.Inflate(-1, -1);
            e.Graphics.DrawRectangle(p, rect);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.autoCompleteTextBox1 = new AutoSearchTextBox.AutoCompleteTextBox();
            this.SuspendLayout();
            // 
            // autoCompleteTextBox1
            // 
            this.autoCompleteTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.autoCompleteTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoCompleteTextBox1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoCompleteTextBox1.Location = new System.Drawing.Point(4, 4);
            this.autoCompleteTextBox1.Name = "autoCompleteTextBox1";
            this.autoCompleteTextBox1.PopupBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.autoCompleteTextBox1.PopupOffset = new System.Drawing.Point(12, 4);
            this.autoCompleteTextBox1.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.autoCompleteTextBox1.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.autoCompleteTextBox1.PopupWidth = 120;
            this.autoCompleteTextBox1.Size = new System.Drawing.Size(120, 16);
            this.autoCompleteTextBox1.TabIndex = 0;
            this.autoCompleteTextBox1.SizeChanged += new System.EventHandler(this.TextBox_SizeChanged);
            // 
            // CoolTextBox
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.autoCompleteTextBox1);
            this.Name = "CoolTextBox";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(128, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void TextBox_SizeChanged(object sender, EventArgs e) {
            AutoCompleteTextBox tb = sender as AutoCompleteTextBox;

            Height = tb.Height + 8;
        }
    }
}
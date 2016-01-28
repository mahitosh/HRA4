using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace RiskApps3.View.Common.AutoSearchTextBox
{
    /// <summary>
    /// Summary description for CoolComboBox.
    /// </summary>
    public class CoolComboBox : UserControl
    {
        public AutoCompleteComboBox autoCompleteComboBox1;
        private Color borderColor = Color.LightSteelBlue;

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components;

        public CoolComboBox()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitializeComponent call
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    Invalidate();
                }
            }
        }

        public int DropDownWidth
        {
            get { return autoCompleteComboBox1.DropDownWidth; }
            set { autoCompleteComboBox1.DropDownWidth = value; }
        }

        public ComboBoxStyle DropDownStyle
        {
            get { return autoCompleteComboBox1.DropDownStyle; }
            set { autoCompleteComboBox1.DropDownStyle = value; }
        }

        public int MaxDropDownItems
        {
            get { return autoCompleteComboBox1.MaxDropDownItems; }
            set { autoCompleteComboBox1.MaxDropDownItems = value; }
        }

        public Color SelectedItemBackColor
        {
            get { return autoCompleteComboBox1.PopupSelectionBackColor; }
            set { autoCompleteComboBox1.PopupSelectionBackColor = value; }
        }

        public Color SelectedItemForeColor
        {
            get { return autoCompleteComboBox1.PopupSelectionForeColor; }
            set { autoCompleteComboBox1.PopupSelectionForeColor = value; }
        }

        public Font ComboFont
        {
            get { return autoCompleteComboBox1.Font; }
            set { autoCompleteComboBox1.Font = value; }
        }

        [Editor(typeof (AutoCompleteEntryCollection.AutoCompleteEntryCollectionEditor), typeof (UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public AutoCompleteEntryCollection AutoCompleteItems
        {
            get { return autoCompleteComboBox1.Autocompleteitems; }
            set { autoCompleteComboBox1.Autocompleteitems = value; }
        }

        public ComboBox.ObjectCollection Items
        {
            get { return autoCompleteComboBox1.Items; }
        }


        [Editor(typeof (AutoCompleteTriggerCollection.AutoCompleteTriggerCollectionEditor), typeof (UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public AutoCompleteTriggerCollection Triggers
        {
            get { return autoCompleteComboBox1.Triggers; }
            set { autoCompleteComboBox1.Triggers = value; }
        }

        [Browsable(true)]
        public override string Text
        {
            get { return autoCompleteComboBox1.Text; }
            set { autoCompleteComboBox1.Text = value; }
        }

        public override Color ForeColor
        {
            get { return autoCompleteComboBox1.ForeColor; }
            set { autoCompleteComboBox1.ForeColor = value; }
        }

        public override ContextMenu ContextMenu
        {
            get { return autoCompleteComboBox1.ContextMenu; }
            set { autoCompleteComboBox1.ContextMenu = value; }
        }

        public int PopupWidth
        {
            get { return autoCompleteComboBox1.PopupWidth; }
            set { autoCompleteComboBox1.PopupWidth = value; }
        }

        public ComboBox getComboBox()
        {
            return autoCompleteComboBox1;
        }

        public void Clear()
        {
            Items.Clear();
            AutoCompleteItems.Clear();
        }

        public void addItem(String item)
        {
            if (Items.Contains(item))
            {
                return;
            }
            Items.Add(item);
            AutoCompleteItems.Add(new AutoCompleteEntry(item, item));
        }

        public void setFont(Font theFont)
        {
            autoCompleteComboBox1.Font = theFont;
        }


        public void setSelectedIndexChangedEventHandler(EventHandler eventHandler)
        {
            autoCompleteComboBox1.SelectedIndexChanged += eventHandler;
        }

        public String getName()
        {
            return autoCompleteComboBox1.Name;
        }
        public void setText(String text)
        {
            try
            {
                Text = text;
            }catch(Exception e)
            {
                
            }
            autoCompleteComboBox1.HideList();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Rectangle rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            var p = new Pen(BorderColor);
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
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private void ComboBox_SizeChanged(object sender, EventArgs e)
        {
            var tb = sender as AutoCompleteComboBox;

            Height = tb.Height + 8;
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.autoCompleteComboBox1 = new RiskApps3.View.Common.AutoSearchTextBox.AutoCompleteComboBox();
            this.SuspendLayout();
            // 
            // autoCompleteComboBox1
            // 
            this.autoCompleteComboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.autoCompleteComboBox1.DropDownHeight = 400;
            this.autoCompleteComboBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoCompleteComboBox1.IntegralHeight = false;
            this.autoCompleteComboBox1.Location = new System.Drawing.Point(4, 4);
            this.autoCompleteComboBox1.Name = "autoCompleteComboBox1";
            this.autoCompleteComboBox1.PopupBorderStyle = System.Windows.Forms.BorderStyle.None;
            this.autoCompleteComboBox1.PopupOffset = new System.Drawing.Point(0, 4);
            this.autoCompleteComboBox1.PopupSelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.autoCompleteComboBox1.PopupSelectionForeColor = System.Drawing.SystemColors.HighlightText;
            this.autoCompleteComboBox1.PopupWidth = 300;
            this.autoCompleteComboBox1.Size = new System.Drawing.Size(120, 21);
            this.autoCompleteComboBox1.TabIndex = 0;
            this.autoCompleteComboBox1.SizeChanged += new System.EventHandler(this.ComboBox_SizeChanged);
            // 
            // CoolComboBox
            // 
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.autoCompleteComboBox1);
            this.Name = "CoolComboBox";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(128, 22);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
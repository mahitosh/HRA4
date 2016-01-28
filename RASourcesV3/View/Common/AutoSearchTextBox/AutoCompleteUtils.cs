using System;
using System.Data.Common;
using System.Drawing;
using System.Windows.Forms;


namespace RiskApps3.View.Common.AutoSearchTextBox
{
    public class AutoCompleteUtils
    {

        public static void AdjustWidthComboBox(CoolComboBox comboBox)
        {
            int width = comboBox.DropDownWidth;
            Graphics g = comboBox.autoCompleteComboBox1.CreateGraphics();
            Font font = comboBox.autoCompleteComboBox1.Font;
            int vertScrollBarWidth =
                (comboBox.Items.Count > comboBox.MaxDropDownItems)
                    ? SystemInformation.VerticalScrollBarWidth
                    : 0;

            int newWidth;
            foreach (string s in (comboBox.Items))
            {
                newWidth = (int)g.MeasureString(s, font).Width
                           + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            comboBox.DropDownWidth = width;
            comboBox.PopupWidth = width;
        }

        public static void fillComboBoxFromArray(CoolComboBox comboBox, String[] sArray)
        {
            //remove all existing items
            comboBox.Clear();

            //populate the combo
            foreach (String s in sArray)
            {
                comboBox.addItem(s);
            }
        }
    }
}
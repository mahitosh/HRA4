using System;
using System.Windows.Forms;

namespace RiskApps3.Utilities
{
    public class IntegerTextBox : TextBox
    {
        // Restricts the entry of characters to an integer and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
            }
        }

        public int IntValue
        {
            get { return Int32.Parse(Text); }
        }
    }
}
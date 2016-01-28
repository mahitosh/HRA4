using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace RiskApps3.View.Common.MaskedTextBox
{
    public enum Mask
    {
        None,
        DateOnly,
        DOBDate,
        PhoneWithArea,
        IpAddress,
        SSN,
        Decimal,
        Digit
    } ;

    public class MaskedTextBox : TextBox
    {
        private Container components;
        private int CountDot;
        private int DelimitNumber;
        private int digitPos;

        private ErrorProvider errorProvider1;
        private Mask m_mask = Mask.None;

        public MaskedTextBox()
        {
            InitializeComponent();
        }

        public Mask Masked
        {
            get { return m_mask; }
            set
            {
                m_mask = value;
                Text = "";
            }
        }

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

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            var sd = (MaskedTextBox) sender;
            switch (m_mask)
            {
                case Mask.DateOnly:
                case Mask.DOBDate:
                    sd.MaskDate(e);
                    break;
                case Mask.PhoneWithArea:
                    sd.MaskPhoneSSN(e, 3, 3);
                    break;
                case Mask.IpAddress:
                    sd.MaskIpAddr(e);
                    break;
                case Mask.SSN:
                    sd.MaskPhoneSSN(e, 3, 2);
                    break;
                case Mask.Decimal:
                    sd.MaskDecimal(e);
                    break;
                case Mask.Digit:
                    sd.MaskDigit(e);
                    break;
            }
        }

        private void OnLeave(object sender, EventArgs e)
        {
            var sd = (MaskedTextBox) sender;
            Regex regStr;
            Regex regStr2DigitYear;
            Regex regStr4DigitYear;
            switch (m_mask)
            {
                case Mask.DateOnly:
                    regStr2DigitYear = new Regex(@"\d{2}/\d{2}/\d{2}");
                    regStr4DigitYear = new Regex(@"\d{2}/\d{2}/\d{4}");
                    if (regStr2DigitYear.IsMatch(sd.Text) && regStr4DigitYear.IsMatch(sd.Text) == false)
                    {
                        DateTime today = DateTime.Now;
                        int currentYear = today.Year - 2000;
                        int enteredYear = Int32.Parse(sd.Text.Substring(6));
                        int century = 20;
                        if (enteredYear > currentYear)
                        {
                            century = 19;
                        }

                        String updatedDate = sd.Text.Substring(0, 6) + century + sd.Text.Substring(6);
                        sd.Text = updatedDate;
                    }
                    break;
                case Mask.DOBDate:
                    regStr2DigitYear = new Regex(@"\d{2}/\d{2}/\d{2}");
                    regStr4DigitYear = new Regex(@"\d{2}/\d{2}/\d{4}");
                    if (regStr2DigitYear.IsMatch(sd.Text) && regStr4DigitYear.IsMatch(sd.Text) == false)
                    {
                        String updatedDate = sd.Text.Substring(0, 6) + "19" + sd.Text.Substring(6);
                        sd.Text = updatedDate;
                    }
                    break;
                case Mask.PhoneWithArea:
                    regStr = new Regex(@"\d{3}-\d{3}-\d{4}");
                    if (!regStr.IsMatch(sd.Text))
                    {
                        // errorProvider1.SetError(this, "Phone number is not valid; xxx-xxx-xxxx");
                    }
                    break;
                case Mask.IpAddress:
                    short cnt = 0;
                    int len = sd.Text.Length;
                    for (short i = 0; i < len; i++)
                        if (sd.Text[i] == '.')
                        {
                            cnt++;
                            if (i + 1 < len)
                            {
                                if (sd.Text[i + 1] == '.')
                                {
                                    //errorProvider1.SetError(this, "IP Address is not valid; x??.x??.x??.x??");
                                    break;
                                }
                            }
                        }
                    if (cnt < 3 || sd.Text[len - 1] == '.')
                    {
                        //errorProvider1.SetError(this, "IP Address is not valid; x??.x??.x??.x??");
                    }
                    break;
                case Mask.SSN:
                    regStr = new Regex(@"\d{3}-\d{2}-\d{4}");
                    if (!regStr.IsMatch(sd.Text))
                    {
                        //errorProvider1.SetError(this, "SSN is not valid; xxx-xx-xxxx");
                    }
                    break;
                case Mask.Decimal:
                    break;
                case Mask.Digit:
                    break;
            }
        }

        private void MaskDigit(KeyPressEventArgs e)
        {
            //enable to using Keyboard Ctrl+C and Keyboard Ctrl+V
            if (e.KeyChar == (char) 3 || e.KeyChar == (char) 22 || e.KeyChar == (char) 24 || e.KeyChar == (char) 26)
            {
                e.Handled = false;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                //errorProvider1.SetError(this, "");
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
                //errorProvider1.SetError(this, "Only valid for Digit");
            }
        }

        private void MaskDecimal(KeyPressEventArgs e)
        {
            //enable to using Keyboard Ctrl+C and Keyboard Ctrl+V
            if (e.KeyChar == (char) 3 || e.KeyChar == (char) 22 || e.KeyChar == (char) 24 || e.KeyChar == (char) 26)
            {
                e.Handled = false;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8 || e.KeyChar == '-')
            {
                // if select all reset vars
                if (SelectionLength == Text.Length)
                {
                    if (e.KeyChar != (char) 22)
                    {
                        Text = null;
                    }
                }
                else
                {
                    if (ReplaceSelectionOrInsert(e, Text.Length))
                    {
                        return;
                    }
                }
                e.Handled = false;
                //errorProvider1.SetError(this, "");
                string str = Text;
                if (e.KeyChar == '.')
                {
                    int indx = str.IndexOf('.', 0);
                    if (indx > 0)
                    {
                        //errorProvider1.SetError(this, "Decimal can't have more than one dot");
                    }
                }
                if (e.KeyChar == '-' && str.Length > 0)
                {
                    e.Handled = true;
                    //errorProvider1.SetError(this, "'-' can be at start position only");
                }
            }
            else
            {
                e.Handled = true;
                //errorProvider1.SetError(this, "Only valid for Digit and dot");
            }
        }

        private bool ReplaceSelectionOrInsert(KeyPressEventArgs e, int len)
        {
            int selectStart = SelectionStart;
            int selectLen = SelectionLength;
            if (selectLen > 0)
            {
                string str;
                str = Text.Remove(selectStart, selectLen);
                Text = str.Insert(selectStart, e.KeyChar.ToString());
                e.Handled = true;
                SelectionStart = selectStart + 1;
                return true;
            }
            else if (selectLen == 0 && SelectionStart > 0 && SelectionStart < len)
            {
                string str = Text;
                if (e.KeyChar == 8)
                {
                    Text = str.Remove(selectStart - 1, 1);
                    SelectionStart = selectStart - 1;
                }
                else
                {
                    Text = str.Insert(selectStart, e.KeyChar.ToString());
                    SelectionStart = selectStart + 1;
                }
                e.Handled = true;
                return true;
            }
            return false;
        }

        private void handleDayBeforeMonth(KeyPressEventArgs e)
        {

            int len = Text.Length;
            int indx = Text.LastIndexOf("/");
            if (indx == 5)
            {
                DelimitNumber = 2;
            }

            //enable to using Keyboard Ctrl+C and Keyboard Ctrl+V
            if (e.KeyChar == (char)3 || e.KeyChar == (char)22 || e.KeyChar == (char)24 || e.KeyChar == (char)26)
            {
                e.Handled = false;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == 8)
            {
                // if select all reset vars
                if (SelectionLength == len)
                {
                    indx = -1;
                    digitPos = 0;
                    DelimitNumber = 0;
                    Text = null;
                }
                else
                {
                    if (ReplaceSelectionOrInsert(e, len))
                    {
                        return;
                    }
                }

                string tmp = Text;
                if (e.KeyChar != 8)
                {
                    if (e.KeyChar != '/')
                    {
                        if (indx > 0)
                        {
                            digitPos = len - indx;
                        }
                        else
                        {
                            digitPos++;
                        }

                        if (digitPos == 3 && DelimitNumber < 2)
                        {
                            if (e.KeyChar != '/')
                            {
                                DelimitNumber++;
                                AppendText("/");
                            }
                        }
                    }
                    else
                    {
                        if ((Text.Length == 3) || (Text.Length == 6) || DelimitNumber > 1)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            DelimitNumber++;
                            string tmp3;
                            if (indx == -1)
                            {
                                tmp3 = Text.Substring(indx + 1);
                            }
                            else
                            {
                                tmp3 = Text;
                            }
                            if (digitPos == 1)
                            {
                                Text = tmp3.Insert(indx + 1, "0");
                                AppendText("/");
                                e.Handled = true;
                            }
                        }
                    }
                }
                else //  if (Char.IsDigit(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == 8)
                {
                    e.Handled = false;
                    if ((len - indx) == 1)
                    {
                        DelimitNumber--;
                        if (indx > -1)
                        {
                            digitPos = 2;
                        }
                        else
                        {
                            digitPos--;
                        }
                    }
                    else
                    {
                        if (indx > -1)
                        {
                            digitPos = len - indx - 1;
                        }
                        else
                        {
                            digitPos = len - 1;
                        }
                    }
                }
            }
            else
            {
                e.Handled = true;
            }

        }

        private void MaskDate(KeyPressEventArgs e)
        {
            String datePattern = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
            datePattern = datePattern.ToUpper();
            if (datePattern.IndexOf("D") < datePattern.IndexOf("M"))
            {
                handleDayBeforeMonth(e);
                return;
            }

            int len = Text.Length;
            int indx = Text.LastIndexOf("/");
            if (indx == 5)
            {
                DelimitNumber = 2;
            }

            //enable to using Keyboard Ctrl+C and Keyboard Ctrl+V
            if (e.KeyChar == (char) 3 || e.KeyChar == (char) 22 || e.KeyChar == (char) 24 || e.KeyChar == (char) 26)
            {
                e.Handled = false;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '/' || e.KeyChar == 8)
            {
                // if select all reset vars
                if (SelectionLength == len)
                {
                    indx = -1;
                    digitPos = 0;
                    DelimitNumber = 0;
                    Text = null;
                }
                else
                {
                    if (ReplaceSelectionOrInsert(e, len))
                    {
                        return;
                    }
                }

                string tmp = Text;
                if (e.KeyChar != 8)
                {
                    if (e.KeyChar != '/')
                    {
                        if (indx > 0)
                        {
                            digitPos = len - indx;
                        }
                        else
                        {
                            digitPos++;
                        }

                        if (digitPos == 3 && DelimitNumber < 2)
                        {
                            if (e.KeyChar != '/')
                            {
                                DelimitNumber++;
                                AppendText("/");
                            }
                        }

                        if ((digitPos == 2 || (Int32.Parse(e.KeyChar.ToString()) > 1 && DelimitNumber == 0)))
                        {
                            string tmp2;
                            if (indx == -1)
                            {
                                tmp2 = e.KeyChar.ToString();
                            }
                            else
                            {
                                tmp2 = Text.Substring(indx + 1) + e.KeyChar;
                            }

                            if (DelimitNumber < 2)
                            {
                                if (digitPos == 1)
                                {
                                    AppendText("0");
                                }
                                AppendText(e.KeyChar.ToString());
                                if (indx < 0)
                                {
                                    if (Int32.Parse(Text) > 12) // check validation
                                    {
                                        string str;
                                        str = Text.Insert(0, "0");
                                        if (Int32.Parse(Text) > 13)
                                        {
                                            Text = str.Insert(2, "/0");
                                            DelimitNumber++;
                                            AppendText("/");
                                        }
                                        else
                                        {
                                            Text = str.Insert(2, "/");
                                            AppendText("");
                                        }
                                        DelimitNumber++;
                                    }
                                    else
                                    {
                                        AppendText("/");
                                        DelimitNumber++;
                                    }
                                    e.Handled = true;
                                }
                                else
                                {
                                    if (DelimitNumber == 1)
                                    {
                                        int m = Int32.Parse(Text.Substring(0, indx));
                                        if (!CheckDayOfMonth(m, Int32.Parse(tmp2)))
                                        {
                                            e.Handled = true;
                                            return;
                                        }

                                        AppendText("/");
                                        DelimitNumber++;
                                        e.Handled = true;
                                    }
                                }
                            }
                        }
                        else if (digitPos == 1 && Int32.Parse(e.KeyChar.ToString()) > 3 && DelimitNumber < 2)
                        {
                            if (digitPos == 1)
                            {
                                AppendText("0");
                            }
                            AppendText(e.KeyChar.ToString());
                            AppendText("/");
                            DelimitNumber++;
                            e.Handled = true;
                        }
                        else
                        {
                            if (digitPos == 1 && DelimitNumber == 2 && e.KeyChar > '2')
                            {
                                //errorProvider1.SetError(this, "The year should start with 1 or 2");
                            }
                        }
                        if (digitPos > 4)
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        if ((Text.Length == 3) || (Text.Length == 6) || DelimitNumber > 1)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            DelimitNumber++;
                            string tmp3;
                            if (indx == -1)
                            {
                                tmp3 = Text.Substring(indx + 1);
                            }
                            else
                            {
                                tmp3 = Text;
                            }
                            if (digitPos == 1)
                            {
                                Text = tmp3.Insert(indx + 1, "0");
                                AppendText("/");
                                e.Handled = true;
                            }
                        }
                    }
                }
                else
                {
                    e.Handled = false;
                    if ((len - indx) == 1)
                    {
                        DelimitNumber--;
                        if (indx > -1)
                        {
                            digitPos = 2;
                        }
                        else
                        {
                            digitPos--;
                        }
                    }
                    else
                    {
                        if (indx > -1)
                        {
                            digitPos = len - indx - 1;
                        }
                        else
                        {
                            digitPos = len - 1;
                        }
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void MaskPhoneSSN(KeyPressEventArgs e, int pos, int pos2)
        {
            int len = Text.Length;
            int indx = Text.LastIndexOf("-");
            //enable to using Keyboard Ctrl+C and Keyboard Ctrl+V
            if (e.KeyChar == (char) 3 || e.KeyChar == (char) 22 || e.KeyChar == (char) 24 || e.KeyChar == (char) 26)
            {
                e.Handled = false;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '-' || e.KeyChar == 8)
            {
                // only digit, Backspace and - are accepted
                // if select all reset vars
                if (SelectionLength == len)
                {
                    indx = -1;
                    digitPos = 0;
                    DelimitNumber = 0;
                    Text = null;
                }
                else
                {
                    if (ReplaceSelectionOrInsert(e, len))
                    {
                        return;
                    }
                }
                string tmp = Text;
                if (e.KeyChar != 8)
                {
//                    errorProvider1.SetError(this, "");
                    if (e.KeyChar != '-')
                    {
                        if (indx > 0)
                        {
                            digitPos = len - indx;
                        }
                        else
                        {
                            digitPos++;
                        }
                    }
                    if (indx > -1 && digitPos == pos2 && DelimitNumber == 1)
                    {
                        if (e.KeyChar != '-')
                        {
                            AppendText(e.KeyChar.ToString());
                            AppendText("-");
                            e.Handled = true;
                            DelimitNumber++;
                        }
                    }
                    if (digitPos == pos && DelimitNumber == 0)
                    {
                        if (e.KeyChar != '-')
                        {
                            AppendText(e.KeyChar.ToString());
                            AppendText("-");
                            e.Handled = true;
                            DelimitNumber++;
                        }
                    }
                    if (digitPos > 4)
                    {
                        e.Handled = true;
                    }
                }
                else
                {
                    e.Handled = false;
                    if ((len - indx) == 1)
                    {
                        DelimitNumber--;
                        if ((indx) > -1)
                        {
                            digitPos = len - indx;
                        }
                        else
                        {
                            digitPos--;
                        }
                    }
                    else
                    {
                        if (indx > -1)
                        {
                            digitPos = len - indx - 1;
                        }
                        else
                        {
                            digitPos = len - 1;
                        }
                    }
                }
            }
            else
            {
                e.Handled = true;
//                errorProvider1.SetError(this, "Only valid for Digit and -");
            }
        }

        private void MaskIpAddr(KeyPressEventArgs e)
        {
            int len = Text.Length;
            int indx = Text.LastIndexOf(".");
            //enable to using Keyboard Ctrl+C and Keyboard Ctrl+V
            if (e.KeyChar == (char) 3 || e.KeyChar == (char) 22 || e.KeyChar == (char) 24 || e.KeyChar == (char) 26)
            {
                e.Handled = false;
                return;
            }
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == 8)
            {
                // only digit, Backspace and dot are accepted
                // if select all reset vars
                if (SelectionLength == len)
                {
                    indx = -1;
                    digitPos = 0;
                    DelimitNumber = 0;
                    Text = null;
                }
                else
                {
                    if (ReplaceSelectionOrInsert(e, len))
                    {
                        return;
                    }
                }
                string tmp = Text;
                //errorProvider1.SetError(this, "");
                if (e.KeyChar != 8)
                {
                    if (e.KeyChar != '.')
                    {
                        if (indx > 0)
                        {
                            digitPos = len - indx;
                        }
                        else
                        {
                            digitPos++;
                        }
                        if (digitPos == 3)
                        {
                            string tmp2 = Text.Substring(indx + 1) + e.KeyChar;
                            if (Int32.Parse(tmp2) > 255) // check validation
                            {
                                //errorProvider1.SetError(this, "The number can't be bigger than 255");
                            }
                            else
                            {
                                if (DelimitNumber < 3)
                                {
                                    AppendText(e.KeyChar.ToString());
                                    AppendText(".");
                                    DelimitNumber++;
                                    e.Handled = true;
                                }
                            }
                        }
                        if (digitPos == 4)
                        {
                            if (DelimitNumber < 3)
                            {
                                AppendText(".");
                                DelimitNumber++;
                            }
                            else
                            {
                                e.Handled = true;
                            }
                        }
                    }
                    else
                    {
                        // added - MAC
                        // process the "."
                        if (DelimitNumber + 1 > 3) // cant have more than 3 dots (at least for IPv4)
                        {
                            //errorProvider1.SetError(this, "No more . please!");
                            e.Handled = true; // dont add 4th dot
                            Text.TrimEnd(e.KeyChar);
                        }
                        else
                        {
                            // got the right number, but don't allow two in a row
                            if (Text.EndsWith("."))
                            {
                                //errorProvider1.SetError(this, "Can't do two dots in a row");
                                e.Handled = true;
                            }
                            else
                            {
                                // ok, add the dot
                                DelimitNumber++;
                            }
                        }
                    }
                }
                else
                {
                    e.Handled = false;
                    if ((len - indx) == 1)
                    {
                        DelimitNumber--;
                        if (indx > -1)
                        {
                            digitPos = len - indx;
                        }
                        else
                        {
                            digitPos--;
                        }
                    }
                    else
                    {
                        if (indx > -1)
                        {
                            digitPos = len - indx - 1;
                        }
                        else
                        {
                            digitPos = len - 1;
                        }
                    }
                }
            }
            else
            {
                e.Handled = true;
                //errorProvider1.SetError(this, "Only valid for Digit abd dot");
            }
        }

        private bool CheckDayOfMonth(int mon, int day)
        {
            bool ret = true;
            if (day == 0)
            {
                ret = false;
            }
            switch (mon)
            {
                case 1:
                    if (day > 31)
                    {
                        ret = false;
                    }
                    break;
                case 2:
                    DateTime moment = DateTime.Now;
                    int year = moment.Year;
                    int d = ((year%4 == 0) && ((!(year%100 == 0)) || (year%400 == 0))) ? 29 : 28;
                    if (day > d)
                    {
                        ret = false;
                    }
                    break;
                case 3:
                    if (day > 31)
                    {
                        ret = false;
                    }
                    break;
                case 4:
                    if (day > 30)
                    {
                        ret = false;
                    }
                    break;
                case 5:
                    if (day > 31)
                    {
                        ret = false;
                    }
                    break;
                case 6:
                    if (day > 30)
                    {
                        ret = false;
                    }
                    break;
                case 7:
                    if (day > 31)
                    {
                        ret = false;
                    }
                    break;
                case 8:
                    if (day > 31)
                    {
                        ret = false;
                    }
                    break;
                case 9:
                    if (day > 30)
                    {
                        ret = false;
                    }
                    break;
                case 10:
                    if (day > 31)
                    {
                        ret = false;
                    }
                    break;
                case 11:
                    if (day > 30)
                    {
                        ret = false;
                    }
                    break;
                case 12:
                    if (day > 31)
                    {
                        ret = false;
                    }
                    break;
                default:
                    ret = false;
                    break;
            }
            return ret;
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            // 
            // MaskedTextBox
            // 
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.Leave += new System.EventHandler(this.OnLeave);
        }

        #endregion
    }
}
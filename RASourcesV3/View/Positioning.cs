using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace RiskApps3.View
{
    //TODO attribution: http://nickstips.wordpress.com/2010/11/08/c-programmatically-centering-a-control-extension-method/
    public static class Positioning
    {
        /// <summary>
        /// Centers the control both horizontially and vertically 
        /// according to the parent control that contains it.
        /// </summary>
        /// <param name="control"></param>
        public static void Center(this Control control)
        {
            control.CenterHorizontally();
            control.CenterVertically();
        }

        /// <summary>
        /// Centers the control horizontially according 
        /// to the parent control that contains it.
        /// </summary>
        public static void CenterHorizontally(this Control control)
        {
            if (control.Parent != null)
            {
                Rectangle parentRect = control.Parent.ClientRectangle;
                control.Left = (parentRect.Width - control.Width) / 2;
            }
        }

        /// <summary>
        /// Centers the control vertically according 
        /// to the parent control that contains it.
        /// </summary>
        public static void CenterVertically(this Control control)
        {
            if (control.Parent != null)
            {
                Rectangle parentRect = control.Parent.ClientRectangle;
                control.Top = (parentRect.Height - control.Height) / 2;
            }
        }
    }

    // Usage
    // -----
    // private void Form1_Load(object sender, EventArgs e)
    // {
    //     this.button1.CenterVertically();
    //     this.button1.CenterHorizontally();
    //     this.button1.Center();
    // }

}

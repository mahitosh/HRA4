using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.View.Common
{
    class FhxSortByRelativeID : IComparer
    {
        private int col;
        public FhxSortByRelativeID()
        {
            col = 0;
        }
        public FhxSortByRelativeID(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            ListViewItem lvi_x = (ListViewItem)x;
            ListViewItem lvi_y = (ListViewItem)y;

            Person p1 = (Person)lvi_x.Tag;
            Person p2 = (Person)lvi_y.Tag;
            if (p1 == null)
            {
                if (p2 == null)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (p2 == null)
                    return 1;
            }

            return p1.relativeID - p2.relativeID;
        }
    }
}
    

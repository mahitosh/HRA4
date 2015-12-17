using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Model.MetaData
{
    public class OrderTypeObject : HraObject
    {
        public int orderID;
        public string orderDescription;
        public string grouping;

        public override string ToString()
        {
            if (string.IsNullOrEmpty(this.orderDescription))
            {
                if (this.orderID == 0)
                {
                    return "Unknown Type";
                }
                else
                {
                    return this.orderID.ToString();
                }
            }
            else
            {
                return this.orderDescription;
            }
        }
    }
}

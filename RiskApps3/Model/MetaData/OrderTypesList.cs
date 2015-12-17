using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class OrderTypesList : HRAList
    {
        private object[] constructor_args;
        private ParameterCollection pc;

        public OrderTypesList()
        {
            this.constructor_args = new object[] { };
            this.pc = new ParameterCollection();
        }

        public override void BackgroundListLoad()
        {
            pc.Clear();

            LoadListArgs lla = new LoadListArgs(
                "sp_3_LoadOrderTypes",
                this.pc,
                typeof(OrderTypeObject),
                this.constructor_args);

            DoListLoad(lla);
        }

        public List<OrderTypeObject> OrderTypes
        {
            get
            {
                return this.Select(g => (OrderTypeObject)g).ToList();
            }
        }

        public OrderTypeObject this[string orderText]
        {
            get
            {
                object retval = this.SingleOrDefault(g => ((OrderTypeObject)g).orderDescription.Equals(orderText));
                if (retval == null)
                {
                    throw new IndexOutOfRangeException(
                        "The provided order description was not found in the list of lkpOrders.");
                }
                else if (retval is OrderTypeObject)
                {
                    return (OrderTypeObject)retval;
                }
                else
                {
                    throw new Exception(
                        "Unable to retrieve object from collection");
                }
            }
        }

        public OrderTypeObject this[int id]
        {
            get
            {
                object retval = this.SingleOrDefault(g => ((OrderTypeObject)g).orderID == id);
                if (retval == null)
                {
                    throw new IndexOutOfRangeException(
                        "The provided index was not found in the list of lkpOrders.");
                }
                else if (retval is OrderTypeObject)
                {
                    return (OrderTypeObject)retval;
                }
                else
                {
                    throw new Exception(
                        "Unable to retrieve object from collection");
                }
            }
        }
    }
}

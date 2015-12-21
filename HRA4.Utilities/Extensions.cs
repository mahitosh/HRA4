using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRA4.Utilities
{
    public static class Extensions
    {
        public static DateTime ToDateTime(this string sdatetime)
        {
            DateTime datetime = DateTime.MinValue;
            DateTime.TryParse(sdatetime, out datetime);
            return datetime;
        }
    }
}

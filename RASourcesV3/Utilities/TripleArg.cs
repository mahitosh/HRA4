using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Utilities
{
    /// <summary>
    /// This represents a command line argument, e.g., -k argName=argValue.  
    /// You might pass several of these in an array to an external command, for instance
    /// </summary>
    public class TripleArg
    {
        private string value;
        private string name;
        private string argSwitch;
        /// <summary>
        /// a three-tuple to hold a single command line argument, e.g., -k argName=argVal
        /// because .NET 3.5 doesn't have "Tuple", alas.
        /// </summary>
        /// <param name="sw">argument switch, e.g., -k</param>
        /// <param name="nm">argument name</param>
        /// <param name="val">argument value</param>
        public TripleArg(string sw, string nm, string val)
        {
            if(!String.IsNullOrEmpty(sw)) argSwitch = sw;
            if (!String.IsNullOrEmpty(nm)) name = nm;
            if (!String.IsNullOrEmpty(val)) value = val;

        }

        /// <summary>
        /// empty constructor, generate an empty struct
        /// </summary>
        public TripleArg()
        {

        }

        public string ArgSwitch
        {
            get { return argSwitch; }
            set { argSwitch = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
        
        public string dumpArg()
        {
            string retVal = "";
            if (!String.IsNullOrEmpty(argSwitch)) retVal = retVal + argSwitch;
            if (!String.IsNullOrEmpty(name)) retVal = retVal + " " + name;
            if (!String.IsNullOrEmpty(value)) retVal = retVal + "=" + value;
            return retVal;

        }
    }
}

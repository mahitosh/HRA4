using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using RiskApps3.Model.Session;
using WeifenLuo.WinFormsUI.Docking;

namespace RiskApps3.View
{
    public class HraView : DockContent
    {   //unfortunately this can't be an abstract class because the designer needs to be able to instantiate it :(

        private readonly Lookups _lookups;

        public MainForm.PushViewCallbackType PushViewStack;

        public bool ViewClosing = false;

        protected HraView()
        {
        }

        /// <summary>
        /// Builds the HraView and collects the lkpLookups data based on the given table.
        /// </summary>
        /// <param name="table">table used to filter results</param>
        /// <remarks>
        /// Must implement <code>GetControlsForLookups()</code> 
        /// when  using this constructor.
        /// </remarks>
        protected HraView(string table)
        {
            this._lookups = new Lookups(table);
            this.Load += this.HraView_Load;
        }

        public virtual void PoppedToFront()
        {

        }

        private void HraView_Load(object sender, EventArgs e)
        {
            if (this._lookups != null)
            {
                this._lookups.LoadFullObject();
                this.GetControlsForLookups().ToList().ForEach(AddLookups);
            }
        }

        private void AddLookups(Control control)
        {
            if (this._lookups.ContainsKey(control.Name))
            {
                ComboBox box = control as ComboBox;
                if (box != null)
                {
                    object[] items = this._lookups[control.Name].Select(o => (Object)o).ToArray();
                    box.Items.AddRange(items);
                }

                //TODO handle other types of controls here
            }
        }

        /// <summary>
        /// Selects the controls which are targets for population with lkpLookups data.
        /// </summary>
        /// <returns>list of controls</returns>
        /// <remarks>
        /// Must be overridden by subclasses which make use of the 
        /// <code>public HraView(string table)</code> constructor.
        /// </remarks>
        protected virtual IEnumerable<Control> GetControlsForLookups()
        {
            return new List<Control>();
        }
    }
}

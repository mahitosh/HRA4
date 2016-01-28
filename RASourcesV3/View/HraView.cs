using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using RiskApps3.Model;
using RiskApps3.Model.Session;
using RiskApps3.View.Appointments;
using WeifenLuo.WinFormsUI.Docking;

namespace RiskApps3.View
{
    public class HraView : DockContent
    {   //unfortunately this can't be an abstract class because the designer needs to be able to instantiate it :(

        internal readonly Lookups _lookups;

        private readonly Dictionary<string, string> _validationErrors; 

        public MainForm.PushViewCallbackType PushViewStack;

        public bool ViewClosing = false;

        protected HraView()
        {
            this._validationErrors = new Dictionary<string, string>();
        }

        /// <summary>
        /// Builds the HraView and collects the lkpLookups data based on the given table.
        /// </summary>
        /// <param name="tables">table used to filter results</param>
        /// <remarks>
        /// Must implement <code>GetControlsForLookups()</code> 
        /// when  using this constructor.
        /// </remarks>
        protected HraView(params string[] tables) : this()
        {
            this._lookups = new Lookups(tables);
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

                this.PostLoadHook();
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
            //this should blow so that users know what they have missed
            throw new NotImplementedException("Must implement this function when using the HraView(string table) override.");
        }

        /// <summary>
        /// Tries to fill controls with data from <code>HraObject</code>s.
        /// </summary>
        protected virtual void FillControls()
        {
            IEnumerable<Control> toBeFilled = GetControlsForFill();
            IEnumerable<HraObject> objectData = GetData();

            IEnumerable<HraObject> hraObjects = objectData as IList<HraObject> ?? objectData.ToList();
            foreach (Control control in toBeFilled)
            {
                string name = control.Name;
                
                foreach (HraObject hraObject in hraObjects)
                {
                    TryToSetControlValueFromHraObject(hraObject, name, control);
                }
            }
        }

        /// <summary>
        /// Attempts to fill a <code>Control.Text</code> value by finding the
        ///  member with the given name in the given HraObject.
        /// </summary>
        /// <param name="hraObject">object containing the data to set</param>
        /// <param name="name">name of field to search for</param>
        /// <param name="control">control to populate</param>
        private static void TryToSetControlValueFromHraObject(HraObject hraObject, string name, Control control)
        {
            FieldInfo hraField = hraObject.GetHraFieldWithNameLike(name);
            if (hraField != null)
            {
                object memberValue = hraField.GetValue(hraObject);
                if (memberValue is string)
                {
                    control.Text = memberValue as string;
                }
            }
        }

        /// <summary>
        /// Which objects can be used to find data to populate the control in this view?
        /// </summary>
        /// <remarks>Note that this will not always be called and will only blow up if 
        /// the HraView(string table) overload is used and this function is not overridden.</remarks>
        protected virtual IEnumerable<HraObject> GetData()
        {
            //this should blow so that users know what they have missed
            throw new NotImplementedException("Must implement this function when using the FillControls() function.");
        }

        /// <summary>
        /// Which controls in this view can be populated with data from a set of <code>HraObject</code>?
        /// </summary>
        /// <remarks>Note that this will not always be called and will only blow up if 
        /// the HraView(string table) overload is used and this function is not overridden.</remarks>
        protected virtual IEnumerable<Control> GetControlsForFill()
        {
            //this should blow up so that users know what they have missed
            throw new NotImplementedException("Must implement this function when using the FillControls() function.");
        }

        /// <summary>
        /// Provides a place for subclasses to add functionality which is 
        /// dependant on the class being totally constructed and controls thoroughly populated.
        /// 
        /// Called after lkpLookups are populated.  This way, if you are selecting items
        /// that would only be present after lookups are popualted,
        /// you can be certain that you are not trying select items which
        /// have been added yet.
        /// </summary>
        protected virtual void PostLoadHook()
        {
            //intentionally vacuous - subclasses should do whatever they want
        }

        private const string RevertAllChanges = "Are you sure you wish to close without saving?";
        private const string AreYouSureYouWishToCancel = "Unsaved Changes!";
        private const string CanNotSave = "Can not save";
        private const string CanNotSaveDueToTheFollowingErrors = "Can not save due to the following errors:";

        /// <summary>
        /// Presents a simple <code>MessageBox</code> 
        /// </summary>
        protected static bool UserConfirmedCloseWithoutSave
        {
            get
            {
                return MessageBox.Show(
                    RevertAllChanges,
                    AreYouSureYouWishToCancel,
                    MessageBoxButtons.YesNo) == DialogResult.Yes;
            }
        }

        protected void MarkInvalid(Control invalid, string message)
        {
            this._validationErrors.Add(invalid.Name, message);
        }

        protected void MarkValid(Control valid)
        {
            this._validationErrors.Remove(valid.Name);
        }

        protected void ValidateControlForEmptyString(Control control, string message)
        {
            if (string.IsNullOrEmpty(control.Text))
            {
                this.MarkInvalid(control, message);
            }
            else
            {
                this.MarkValid(control);
            }
        }

        protected bool FormIsValid()
        {
            return this._validationErrors.Count == 0;
        }

        protected void ShowValidationErrors()
        {
            MessageBox.Show(
                DescribeErrors(),
                CanNotSave,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private string DescribeErrors()
        {
            StringBuilder builder = new StringBuilder(CanNotSaveDueToTheFollowingErrors);
            builder.AppendLine();
            builder.AppendLine();
            foreach (string error in this._validationErrors.Values)
            {
                builder.AppendLine(string.Format("\t{0}", error));
            }
            return builder.ToString();
        }
    }
}

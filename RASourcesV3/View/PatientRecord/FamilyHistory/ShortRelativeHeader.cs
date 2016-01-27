using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    public partial class ShortRelativeHeader : UserControl
    {
        public ShortRelativeHeader()
        {
            InitializeComponent();
            name.Text = "[Name not provided]";
            relationship.Text = "";
        }

        public void setRelative(Person relative)
        {
            if (relative != null)
            {
                name.Text = string.IsNullOrEmpty(relative.name)?"[Name not provided]":relative.name;
                relationship.Text = relative.relationship;
            }
            else
            {
                name.Text = "";
                relationship.Text = "";
            }
        }
    }
}
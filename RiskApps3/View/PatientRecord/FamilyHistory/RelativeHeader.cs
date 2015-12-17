using System.Windows.Forms;
using RiskApps3.Model.PatientRecord;

namespace RiskApps3.View.PatientRecord.FamilyHistory
{
    public partial class RelativeHeader : UserControl
    {
        public RelativeHeader()
        {
            InitializeComponent();
            name.Text = "";
            relationship.Text = "";
            bloodline.Text = "";
            age.Text = "";
        }

        public void setRelative(Person relative)
        {
            if (relative != null)
            {
                name.Text = relative.name;
                relationship.Text = relative.relationship;
                bloodline.Text = relative.bloodline;
                if (string.IsNullOrEmpty(bloodline.Text))
                {
                    bloodline.Visible = false;
                    bloodlineHeader.Visible = false;
                }
                else
                {
                    bloodline.Visible = true;
                    bloodlineHeader.Visible = true;
                }
                age.Text = relative.age;
            }
            else
            {
                name.Text = "";
                relationship.Text = "";
                bloodline.Text = "";

                bloodline.Visible = false;
                bloodlineHeader.Visible = false;

                age.Text = "";
            }
        }
    }
}
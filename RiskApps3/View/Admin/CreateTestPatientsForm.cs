using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using RiskApps3.Utilities;

namespace RiskApps3.View.Admin
{
    public partial class CreateTestPatientsForm : Form
    {
        private Dictionary<int, string> surveyDic;
        private Dictionary<int, string> clinicDic;
        private List<string> names = new List<string> { "Mary", "Jane", "Sylvia", "Ruth", "Penelope", "Fernanda", "Gertrude", "Tiffany", "Cher", "Elizabeth", "Rene", "Chloe", "Rapunzel","Ashley","Jessica","Emily","Sarah","Samantha","Brittany","Amanda","Elizabeth","Taylor","Margarete","Stephanie","Kayley","Laura","Jennifer","Rachel","Anna","Nicole","Amber","Alexis","Courtney","Victoria","Daniela","Adelheid","Rebecca","Jasmine","Katherine","Melissa","Alexandra","Brianna","Chelsea","Michaela","Morgan","Kelsey","Tiffany","Kimberley","Christine","Madison","Heather","Selby","Anna","Mary","Mary","Alison","Sarah","Laura","Andrea","Olivia","Erin","Hayley","Abigail","Katherine","Jordan","Natalie","Vanessa","Kelly","Brooke","Erica","Christine","Julia","Crystal","Amata","Katherine","Mary","Lindsay","Paige","Cassandra","Sidney","Katherine","Katherine","Katherine","Emma","Shannon","Angela","Gabriele","Jacqueline","Johanna","Jacqueline","Mary","Adelheid","Brianna","Alexandra","Destiny","Miranda","Monica","Brittany","Katherine","Savannah","Sierra","Sabrina","Brianna","Whitney","Carla","Mary","Magdalena","Erica","Grace","Diana","Leah","Angelica","Lindsay","Christine","Katherine","Cynthia","Margarete","Cheyenne","Mackenzie","Margarete","Veronica","Melanie","Bailey","Christine","Blanche","Elizabeth","Holly","Christine","Alexandra","Ariela","Bethany","Hayley","Leslie","April","Casey","Brenda","Katherine","Julia","Patrizia","Autumn","Karin","Gabriele","Brandy","Anna","Rachel","Kendra","Karin","Dominika","Valerie","Desiree","Cara","Carla","Clare","Tara","Adriana","Kayley","Natalie","Michaela","Chloe","Jocelyn","Kylie","Crystal","Hayley","Katherine","Alison","Anna","Sophia","Daisy","Rebecca","Daniela","Juliana","Cassidy","Alexandra","Raven","Jade","Angela","Summer","Edeltraud","Gabriele","Chelsea","Alexandra","Adriana","Katherine","Claudia","Monica","Margarete","Johanna","Christine","Faith","Michaela","Brandy","Ciara","Michaela","Mallory","Christine","Diana","Jessenia","Ashley","Cynthia","Mercedes","Adelheid","Regina","Lydia","Felizia","Mackenzie","Zoe","Brigitte","Mary","Priska","Carla","Cassandra","Denise","Jasmine","Victoria","Isabella","Selina","Diamond","Evelyn","Anna","Amelia","Christine","Alison","Tabitha","Abigail","Ashley","Lacey","Jasmine","Isabella","Asia","Candace","Ciara","Sierra","Colleen","Jacqueline","Carla","Hope","Linda","Noemi","Helen","Mary","Theresa","Meredith","Guadalupe","Johanna","Renate","Nicole","Kendall","Jasmine","Tamara","Brittany","Justine","Theresa","Susanne","Tatjana","Tiara","Daniela","Maja","Adriana","Genesis","Rose","Myra","Kelly","Casey","Candace","Clare","Aubrey","Adriana","Nina","Theresa" };

        public CreateTestPatientsForm()
        {
            InitializeComponent();
        }

        private void CreateTestPatientsForm_Load(object sender, EventArgs e)
        {
            initializeDropDowns();
        }

        private void initializeDropDowns()
        {
            string surveyQuery = "SELECT surveyID, surveyName FROM lkpsurveys WHERE inactive = 0";
            string clinicQuery = "SELECT clinicID, clinicName FROM lkpclinics";

            try
            {
                surveyDic = getDicValues(surveyQuery);
                clinicDic = getDicValues(clinicQuery);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }


            cbSurvey.DataSource = new BindingSource(surveyDic, null);
            cbSurvey.DisplayMember = "Value";
            cbSurvey.ValueMember = "key";


            cbClinic.DataSource = new BindingSource(clinicDic, null);
            cbClinic.DisplayMember = "Value";
            cbClinic.ValueMember = "key";

            dtAppointmentDate.Value = DateTime.Now;

        }

        private Dictionary<int, string> getDicValues(string commandString)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();

            using (SqlDataReader reader = BCDB2.Instance.ExecuteReader(commandString))
            {
                while (reader.Read())
                {
                    dictionary.Add(reader.GetInt32(0), reader.GetString(1));
                }
                return dictionary;
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {

            int surveyID = 0;
            //KeyValuePair<int, string> kvpSurvey;
            KeyValuePair<int, string> kvpClinic;

            foreach (KeyValuePair<int, string> pair in surveyDic)
            {
                if (pair.Value == cbSurvey.Text)
                {
                    surveyID = pair.Key;
                }
            }


            int x = 0;
            if (cbNumPatients.SelectedItem != null)
            {
                int i = int.Parse(cbNumPatients.SelectedItem.ToString());
                int index = 0;
                string time;
                Random random = new Random();
                string firstName = "";

                string nextUnitNum = createTestUnitnum();
                long unitNum = Int64.Parse(nextUnitNum);

                while (i > x)
                {
                    RiskApps3.Utilities.ParameterCollection pc = new RiskApps3.Utilities.ParameterCollection();

                    if (x <= 9)
                    {
                        time = "0" + x.ToString();
                    }
                    else
                    {
                        time = (x % 60).ToString();
                    }

                    pc.Add("apptdate", dtAppointmentDate.Value.ToString("MM/dd/yyyy"));
                    pc.Add("appttime", "08:" + time + " AM");
                    index = random.Next(10000) % names.Count;
                    firstName = names[index];
                    pc.Add("firstname", firstName);
                    pc.Add("lastname", "Test" + cbSurvey.Text.Replace(" ",""));
                    pc.Add("gender", "F");
                    pc.Add("dob", "09/23/1970");
                    pc.Add("email", "test@test.com");
                    pc.Add("address1", (i * 10).ToString() + " Broadway");
                    pc.Add("city", "Cambridge");
                    pc.Add("state", "MA");
                    pc.Add("zip", "02473");
                    pc.Add("phone", "6175551212");
                    pc.Add("emailaddress", "test@hughesriskapps.com");
                    pc.Add("sendByEmail", 1);
                    pc.Add("machinename", "test");
                    pc.Add("preferredInstitutionName", "test");
                    if(cbClinic.SelectedItem != null)
                    {
                        kvpClinic = (KeyValuePair<int, string>)cbClinic.SelectedItem;
                        pc.Add("clinicID", kvpClinic.Key);
                    }
                    else
                    {
                        pc.Add("clinicID", 1);      // default
                    }
                    
                    pc.Add("unitnum", unitNum.ToString());
                    pc.Add("createdby", cbSurvey.Text);
                    pc.Add("insertApptWebRecord", 1);
                    pc.Add("surveyID", surveyID);

                    BCDB2.Instance.ExecuteReaderSPWithParams("sp_createApptFromSchedService", pc);
                    unitNum++;
                    x++;
                }
            }

            this.Close();
        }

        private string createTestUnitnum()
        {
            String mrn = "999";
            mrn = mrn + String.Format("{0:MMddyy}", DateTime.Now);

            int i = 1;
            while (true)
            {
                String possibleMRN = mrn + i.ToString("00");

                if (BCDB2.Instance.ExecuteReader("SELECT * FROM tblAppointments WHERE unitnum='" + possibleMRN + "'").HasRows == false)
                {
                    return possibleMRN;
                }
                i++;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}

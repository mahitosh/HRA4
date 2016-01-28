using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;

namespace RiskApps3.Model.Clinic
{
    public class Provider : HraObject
    {
        private const string Unprintable = "Unprintable";
        public Provider() { }  // Default constructor for serialization

        [DataMember] public int providerID = -1;

        #region HraAttributes
        // ReSharper disable InconsistentNaming
        [DataMember] [HraAttribute] public string title;
        [DataMember] [HraAttribute] public string firstName;
        [DataMember] [HraAttribute] public string middleName;
        [DataMember] [HraAttribute] public string lastName;
        [DataMember] [HraAttribute] public string degree;
        [DataMember] [HraAttribute] public string institution;
        [DataMember] [HraAttribute] public string address1;
        [DataMember] [HraAttribute] public string address2;
        [DataMember] [HraAttribute] public string city;
        [DataMember] [HraAttribute] public string state;
        [DataMember] [HraAttribute] public string zipcode;
        [DataMember] [HraAttribute] public string country;
        [DataMember] [HraAttribute] public string phone;
        [DataMember] [HraAttribute] public string fax;
        [DataMember] [HraAttribute] public string email;
        [DataMember] [HraAttribute] public string nationalProviderID;
        [DataMember] [HraAttribute] public string UPIN;
        [DataMember] [HraAttribute] public string defaultRole;
        [DataMember] [HraAttribute] public string fullName;
        [DataMember] [HraAttribute] public int riskClinic;
        [DataMember] [HraAttribute] public string dataSource;
        [DataMember] [HraAttribute] public string localProviderID;
        [DataMember] [HraAttribute] public string displayName;
        [DataMember] [HraAttribute] public string isApptProvider;
        [DataMember] [HraAttribute] public string uploadPath;
        [DataMember] [HraAttribute] public string letterName;
        [DataMember] [HraAttribute] public string letterTitle;
        [DataMember] [HraAttribute] public string networkID;
        [DataMember] [HraAttribute] public string photoPath;
        [DataMember] [HraAttribute] public string cellphone;
        [DataMember] [HraAttribute] public string pager;
        [DataMember] [HraAttribute] public string comments;
        [DataMember] [HraAttribute] public string suffixName;
        [DataMember] [HraAttribute] public string webURL;
        [DataMember] [HraAttribute] public string GT1;
        [DataMember] [HraAttribute] public string GT2;
        [DataMember] [HraAttribute] public string GT3;
        [DataMember] [HraAttribute] public string GT4;
        [DataMember] [HraAttribute] public string professionalTitle;
        [DataMember] [HraAttribute] public string associatedNurses;
        [DataMember] [HraAttribute] public string academicTitle;
        [DataMember] [HraAttribute] public string footerText;
        [DataMember] [HraAttribute] public string clinic;
        [DataMember] [HraAttribute] public string academicSite;
        [DataMember] [HraAttribute] public string professionalSite;
        [DataMember] [HraAttribute] public string showEmail;
        [DataMember] [HraAttribute] public string isDuplicate;
        [DataMember] [HraAttribute] public string documentStoragePath;
        [DataMember] [HraAttribute] public string webSite;
        public string providerPhotosPath;
        // ReSharper restore InconsistentNaming
        #endregion

        #region Gets/Sets

        #endregion

        [DataMember] public int apptid = -1;
        [DataMember] public bool PCP = false;
        [DataMember] public bool refPhys = false;

        public Provider(string providerName)
        {
            TryParseName(providerName);

            this.degree = "M.D.";
            this.country = "United States";
            this.providerPhotosPath = @"C:\Program Files\riskappsv2\ProviderPhotos";
        }

        private void TryParseName(string providerName)
        {
            //--------------------------------------------------
            // try to parse what the user typed as a name
            //--------------------------------------------------
            String[] tokens = providerName.Split(' ');
            if (tokens.Length == 1)
            {
                this.firstName = tokens[0];
            }
            else if (tokens.Length == 2)
            {
                this.firstName = tokens[0];
                this.lastName = tokens[1];
            }
            else
            {
                this.title = tokens[0];
                this.firstName = tokens[1];
                this.lastName = tokens[2];
            }
        }

        /*******************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("providerID", providerID,true);

            if (apptid > 0)
            {
                pc.Add("apptid", apptid, false);
                pc.Add("PCP", PCP, false);
                pc.Add("refPhys", refPhys, false);
            }

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_Provider",
                                      ref pc);

            this.providerID = (int)pc["providerID"].obj;
        }

        /// <summary>
        /// Can we formulate a reasonable printable name?
        /// </summary>
        /// <returns>true if:
        ///     display name is available, or
        ///     full name is available, or both of
        ///     title, and
        ///     lastname
        /// are available.  Else, false.</returns>
        public bool HasPrintableName
        {
            get
            {
                return  !string.IsNullOrEmpty(this.displayName) ||
                        !string.IsNullOrEmpty(this.fullName) ||
                        (!string.IsNullOrEmpty(this.title) && !string.IsNullOrEmpty(this.lastName));
            }
        }

        public string PrintableName
        {
            get
            {
                if (HasPrintableName)
                {
                    if (!string.IsNullOrEmpty(this.displayName))
                    {
                        return this.displayName;
                    }
                    else if (!string.IsNullOrEmpty(this.fullName))
                    {
                        return this.ToString();
                    }
                    else
                    {
                        return string.Format("{0} {1}", this.title, this.lastName);
                    }
                }
                else
                {
                    return Unprintable;
                }
            }
        }

        public override string ToString()
        {
            return fullName;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskApps3.Utilities
{
    public class InterfaceInstance
    {
        private int interfaceId = -1;

        public int InterfaceId
        {
            get { return interfaceId; }
            set { interfaceId = value; }
        }

        private string interfaceType = "";

        public string InterfaceType
        {
            get { return interfaceType; }
            set { interfaceType = value; }
        }
        private string ipAddress = "";

        public string IpAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }
        private string port = "";

        public string Port
        {
            get { return port; }
            set { port = value; }
        }
        private string aeTitleRemote = "";

        public string AeTitleRemote
        {
            get { return aeTitleRemote; }
            set { aeTitleRemote = value; }
        }
        private string aeTitleLocal = "";

        public string AeTitleLocal
        {
            get { return aeTitleLocal; }
            set { aeTitleLocal = value; }
        }
        private string dCMTKpath = "";

        public string DCMTKpath
        {
            get { return dCMTKpath; }
            set { dCMTKpath = value; }
        }
        private string accessionNumber = "";

        public string AccessionNumber
        {
            get { return accessionNumber; }
            set { accessionNumber = value; }
        }
        private string patientname = "";

        public string Patientname
        {
            get { return patientname; }
            set { patientname = value; }
        }
        private string dob = "";

        public string Dob
        {
            get { return dob; }
            set { dob = value; }
        }
        private string gender = "";

        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        private string apptDate = "";

        public string ApptDate
        {
            get { return apptDate; }
            set { apptDate = value; }
        }
        private string apptTime = "";

        public string ApptTime
        {
            get { return apptTime; }
            set { apptTime = value; }
        }
        private int pacsSubType = (int)pacsSubTypes.PDF;    // NEW jdg 8/7/15... which type of encapsulated DCM to generate?  Default to PDF.

        /// <summary>
        /// mask for enum pacsSubTypes : int { PDF=1,JPG,BMP }
        /// </summary>
        public int PacsSubType
        {
            get { return pacsSubType; }
            set { pacsSubType = value; }
        }

        private int timeSpanFrom = 0;

        /// <summary>
        /// number of minutes BEFORE current time to begin matching MWL records
        /// </summary>
        public int TimeSpanFrom
        {
            get { return timeSpanFrom; }
            set { timeSpanFrom = value; }
        }

        private int timeSpanTo = 0;

        /// <summary>
        /// number of minutes AFTER the current time to stop matching MWL record
        /// </summary>
        public int TimeSpanTo
        {
            get { return timeSpanTo; }
            set { timeSpanTo = value; }
        }

        private string modality = "OT";

        /// <summary>
        /// When sending to PACS this field might well be customer specific
        /// </summary>
        public string Modality
        {
            get { return modality; }
            set { modality = value; }
        }
        private string clinicFieldInMWLQuery = "SCHEDULEDSTATIONNAME";

        /// <summary>
        /// Turns out that differents PACS systems identify the "clinic" differently.  For some if is the 
        /// ScheduledStationName (e.g., NSMC), or perhaps "SCHEDULEDSTATIONAETITLE" (e.g., STRIC). 
        /// Or... it could be something else in the future.  It must be parameterized.  
        /// </summary>
        public string ClinicFieldInMWLQuery
        {
            get { return clinicFieldInMWLQuery; }
            set { clinicFieldInMWLQuery = value; }
        }

        private string additionalParams = "";

        /// <summary>
        /// Any leftover string args or switches can go here, and will be tacked onto the end of the PACS document submission.
        /// </summary>
        public string AdditionalParams
        {
            get { return additionalParams; }
            set { additionalParams = value; }
        }
    }
}

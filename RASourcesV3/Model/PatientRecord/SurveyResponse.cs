using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using RiskApps3.Utilities;
using RiskApps3.Controllers;


namespace RiskApps3.Model.PatientRecord
{
    [DataContract]
    public class SurveyResponse : HraObject
    {
        /**************************************************************************************************/
        public Patient owningPatient;

        //[DataMember] [HraAttribute] public int apptID;
        [DataMember] [HraAttribute] public int surveyID;
        [DataMember] [HraAttribute] public string responseTag;
        [DataMember] [HraAttribute] public string responseValue;
        [DataMember] [HraAttribute] public string comment;
        /*****************************************************/
        
        /**************************************************************************************************/
        public SurveyResponse()
        { 
        }
        public SurveyResponse(Patient proband)
        {
            this.owningPatient = proband;
        }
        public SurveyResponse(Patient proband, int surveyID, string responseTag, string responseValue, string comment)
            : this(proband)
        {
            //this.apptID = proband.apptid;
            this.surveyID = surveyID;
            this.responseTag = responseTag;
            this.responseValue = responseValue;
            this.comment = comment;
        }


        ///*****************************************************/
        //public int SurveyResponse_apptID
        //{
        //    get
        //    {
        //        return apptID;
        //    }
        //    set
        //    {
        //        if (value != apptID)
        //        {
        //            apptID = value;
        //            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
        //            args.updatedMembers.Add(GetMemberByName("apptID"));
        //            SignalModelChanged(args);
        //        }
        //    }
        //}
        /*****************************************************/
        public int SurveyResponse_surveyID
        {
            get
            {
                return surveyID;
            }
            set
            {
                if (value != surveyID)
                {
                    surveyID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("surveyID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SurveyResponse_responseTag
        {
            get
            {
                return responseTag;
            }
            set
            {
                if (value != responseTag)
                {
                    responseTag = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("responseTag"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SurveyResponse_responseValue
        {
            get
            {
                return responseValue;
            }
            set
            {
                if (value != responseValue)
                {
                    responseValue = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("responseValue"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string SurveyResponse_comment
        {
            get
            {
                return comment;
            }
            set
            {
                if (value != comment)
                {
                    comment = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("comment"));
                    SignalModelChanged(args);
                }
            }
        }
        /**************************************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("apptID", owningPatient.apptid); 
            pc.Add("surveyID", surveyID);
            pc.Add("responseTag", responseTag);
            pc.Add("responseValue", responseValue);
            if (comment == null) comment = "";
            pc.Add("comment", comment);
            BCDB2.Instance.RunSPWithParams("sp_setSurveyResponse", pc);

            //e.updatedMembers.Clear();
            //pc.Add("apptID", owningPatient.apptid);


            //pc.Add("surveyID", surveyID);
            //pc.Add("responseTag", responseTag);
            //pc.Add("responseValue", responseValue);
            //pc.Add("comment", comment);

            //DoPersistWithSpAndParams(e,
            //                          "sp_setSurveyResponse",
            //                          ref pc);
        }
        /*****************************************************/
    }
}

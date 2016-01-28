using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.MetaData
{
    public class User : HraObject
    {
        public string userLogin;

        #region HraAttributes    
        //[HraAttribute] public string userPassword;
        [HraAttribute] public string userLastName;
        [HraAttribute] public string userFirstName;
        [HraAttribute] public string userProviderID;
        [HraAttribute] public string userEmail;
        [HraAttribute] public string userFullName;
        [HraAttribute] public string newLogin;
        [HraAttribute] public DateTime passwordChangeDate;
        [HraAttribute] public int hraProviderID;
        [HraAttribute] public string ModuleName;
        [HraAttribute] public int groupingID;
        #endregion

        public readonly ClinicList UserClinicList = new ClinicList();

        #region gets/sets
        /*****************************************************/
        public string User_userLogin
        {
            get
            {
                return userLogin;
            }
            set
            {
                if (value != userLogin)
                {
                    userLogin = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("userLogin"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        //public string User_userPassword
        //{
        //    get
        //    {
        //        return userPassword;
        //    }
        //    set
        //    {
        //        if (value != userPassword)
        //        {
        //            userPassword = value;
        //            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
        //            args.updatedMembers.Add(GetMemberByName("userPassword"));
        //            SignalModelChanged(args);
        //        }
        //    }
        //}
        /*****************************************************/
        public string User_userLastName
        {
            get
            {
                return userLastName;
            }
            set
            {
                if (value != userLastName)
                {
                    userLastName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("userLastName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string User_userFirstName
        {
            get
            {
                return userFirstName;
            }
            set
            {
                if (value != userFirstName)
                {
                    userFirstName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("userFirstName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string User_userProviderID
        {
            get
            {
                return userProviderID;
            }
            set
            {
                if (value != userProviderID)
                {
                    userProviderID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("userProviderID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string User_userEmail
        {
            get
            {
                return userEmail;
            }
            set
            {
                if (value != userEmail)
                {
                    userEmail = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("userEmail"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string User_userFullName
        {
            get
            {
                return userFullName;
            }
            set
            {
                if (value != userFullName)
                {
                    userFullName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("userFullName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string User_newLogin
        {
            get
            {
                return newLogin;
            }
            set
            {
                if (value != newLogin)
                {
                    newLogin = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("newLogin"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public DateTime User_passwordChangeDate
        {
            get
            {
                return passwordChangeDate;
            }
            set
            {
                if (value != passwordChangeDate)
                {
                    passwordChangeDate = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("passwordChangeDate"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int User_hraProviderID
        {
            get
            {
                return hraProviderID;
            }
            set
            {
                if (value != hraProviderID)
                {
                    hraProviderID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("hraProviderID"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public string User_ModuleName
        {
            get
            {
                return ModuleName;
            }
            set
            {
                if (value != ModuleName)
                {
                    ModuleName = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("ModuleName"));
                    SignalModelChanged(args);
                }
            }
        }
        /*****************************************************/
        public int User_groupingID
        {
            get
            {
                return groupingID;
            }
            set
            {
                if (value != groupingID)
                {
                    groupingID = value;
                    HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
                    args.updatedMembers.Add(GetMemberByName("groupingID"));
                    SignalModelChanged(args);
                }
            }
        } 

        #endregion

        /*******************************************************************************/
        public override void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
            ParameterCollection pc = new ParameterCollection();
            pc.Add("userLogin", userLogin);

            DoPersistWithSpAndParams(e,
                                      "sp_3_Save_User",
                                      ref pc);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(userFullName))
            {
                string temp = userFirstName + " " + userLastName;
                if (string.IsNullOrEmpty(temp))
                {
                    return userLogin;
                }
                else
                    return temp;
            }
                
            else
                return userFullName;
        }

        public override void ReleaseListeners(object view)
        {
            UserClinicList.ReleaseListeners(view);
            base.ReleaseListeners(view);
        }
    }
}

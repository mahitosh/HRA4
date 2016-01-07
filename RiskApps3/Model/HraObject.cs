using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using RiskApps3.Controllers;
using RiskApps3.Utilities;
using System.Web;

#if international
using System.Globalization;
using System.Threading;
#endif

namespace RiskApps3.Model
{
    [DataContract(IsReference=true)]
    public abstract class HraObject
    {
        /*******************************************************************************/

        public enum States
        {
            Null,
            Loading,
            Ready
        } ;

        private bool _readOnly;

        /// <summary>
        /// Will this object be persisted to the database?
        /// </summary>
        /// <remarks>For use in enforcing the readonly nature of certains views of data.</remarks>
        public bool ReadOnly {
            [PublicAPI] get
            {
                lock (this._changeEventArgQueue)
                {
                    return this._readOnly;
                }
            }
            set
            {
                lock (this._changeEventArgQueue)
                {
                    this._readOnly = value;
                }
            }
        }

        /*******************************************************************************/

        public delegate void ChangedEventHandler(object sender, HraModelChangedEventArgs e);

        public delegate void LoadFinishedEventHandler(object sender, RunWorkerCompletedEventArgs e);

        public delegate void PersistFinishedEventHandler(object sender, RunWorkerCompletedEventArgs e);

        /*******************************************************************************/
        public States HraState;

        private readonly BackgroundWorker _loadThread;      //TODO should very strongly consider replacing this with a reference to some kind of object state manager which can use connection and/or thread pooling
        private readonly BackgroundWorker _persistThread;   //TODO should very strongly consider replacing this with a reference to some kind of object state manager which can use connection and/or thread pooling

        /*******************************************************************************/
        private int _loadSignalCount;

        private readonly ConcurrentQueue<HraModelChangedEventArgs> _changeEventArgQueue;

        public event ChangedEventHandler Changed;
        public event LoadFinishedEventHandler Loaded;
        public event PersistFinishedEventHandler Persisted;

        /*******************************************************************************/

        protected HraObject()
        {
            HraState = States.Null;

            _loadThread = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = false
            };
            _loadThread.DoWork += this.DbThread_DoWork;
            _loadThread.RunWorkerCompleted +=
                this.DbThread_RunWorkerCompleted;
            _loadThread.ProgressChanged +=
                this.DbThread_ProgressChanged;

            _loadSignalCount = 0;

            _changeEventArgQueue = new ConcurrentQueue<HraModelChangedEventArgs>();

            _persistThread = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = false
            };
            _persistThread.DoWork += this.PersistThread_DoWork;
            _persistThread.RunWorkerCompleted +=
                this.PersistThread_RunWorkerCompleted;
            _persistThread.ProgressChanged +=
                this.PersistThread_ProgressChanged;
        }

        /*******************************************************************************/
        /// <summary>
        /// Loads data synchronously (on the UI thread most likely).
        /// </summary>
        public virtual void LoadFullObject()
        {
            BackgroundLoadWork();
            HraState = States.Ready;
        }
        /*******************************************************************************/
        public virtual void PersistFullObject(HraModelChangedEventArgs e)
        {
            if (this.ReadOnly) return;
            BackgroundPersistWork(e);
        }

        /*******************************************************************************/
        public virtual bool LoadObject()
        {
            HraState = States.Loading;

            _loadSignalCount = _loadSignalCount + 1;

            if (_loadThread.IsBusy == false)
            {
                _loadThread.RunWorkerAsync();
            }

            return true;
        }
 

        /*******************************************************************************/
        public virtual void ReleaseListeners(object view)
        {
            if (Loaded != null)
            {
                foreach (Delegate d in Loaded.GetInvocationList())
                {
                    if (view == null || d.Target == view)
                        Loaded -= (LoadFinishedEventHandler) d;
                }
            }
            if (Changed != null)
            {
                foreach (Delegate d in Changed.GetInvocationList())
                {
                    if (view == null || d.Target == view)
                        Changed -= (ChangedEventHandler) d;
                }
            }
            if (Persisted != null)
            {
                foreach (Delegate d in Persisted.GetInvocationList())
                {
                    if (view == null || d.Target == view)
                        Persisted -= (PersistFinishedEventHandler) d;
                }
            }
        }

        /*******************************************************************************/
        private void DbThread_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_loadSignalCount > 0)
            {
                BackgroundLoadWork();
                _loadSignalCount = _loadSignalCount - 1;
            }
        }

        /*******************************************************************************/
        private void PersistThread_DoWork(object sender, DoWorkEventArgs e)
        {
            string hraAttributeList = "";
            HraModelChangedEventArgs hraE;
            while (_changeEventArgQueue.TryDequeue(out hraE))
            {
                BackgroundPersistWork(hraE);

                if (hraE.Delete)
                {
                    hraAttributeList += "DELETE|";
                }

                foreach (FieldInfo field in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    if (IsAuditable(field))
                    {
                        if (hraE.updatedMembers.Count == 0 || MemberListContainsName(hraE, field.Name))
                        {
                            object val = field.GetValue(this);
                            if (val != null)
                            {
                                hraAttributeList += field.Name + "=" + val + "|";
                            }

                            PersistThreadDoWorkChattyDebug();
                        }
                    }
                }
            }
            ExecuteAudit(hraAttributeList);
        }

        private static void PersistThreadDoWorkChattyDebug()
        {
#if CHATTY_DEBUG
            Logger.Instance.DebugToLog("User=" + SessionManager.Instance.ActiveUser.userLogin +
                " apptID=" + SessionManager.Instance.GetActivePatient().apptid.ToString() +
                " unitnum=" + SessionManager.Instance.GetActivePatient().unitnum +
                " relativeID=" + SessionManager.Instance.GetSelectedRelative().relativeID +
                " field: " + field.Name + "=" + field.GetValue(this).ToString() + 
                " this=" + this.GetType().ToString()  );
#endif
        }

        [SuppressMessage("ReSharper", "StringCompareIsCultureSpecific.3")]
        private bool MemberListContainsName(HraModelChangedEventArgs hraE, string p)
        {
            bool retval = false;
            foreach (MemberInfo member in hraE.updatedMembers)
            {
                //TODO should this be handle specially when international flag is set?
                if (string.Compare(member.Name,p,true)==0)
                {
                    retval = true;
                    break;
                }
            }

            return retval;
        }

        /*******************************************************************************/
        public virtual void BackgroundLoadWork()
        {
        }

        private void ExecuteAudit(string hraAttributeList)
        {
            if (!string.IsNullOrEmpty(hraAttributeList))
            {
                try
                {
                    string userLogin = "";
                    if (SessionManager.Instance.ActiveUser != null)
                    {
                        userLogin = SessionManager.Instance.ActiveUser.userLogin;
                    }

                    int apptId = -1;
                    string unitnum = "";

                    //CJL added check for null 2/14/14
                    if (SessionManager.Instance.GetActivePatient() != null)
                    {
                        apptId = SessionManager.Instance.GetActivePatient().apptid;
                        unitnum = SessionManager.Instance.GetActivePatient().unitnum;
                    }

                    string type = this.GetType().ToString();

                    //CJL added check for null 2/14/14
                    int relativeId = -1;
                    if (SessionManager.Instance.GetSelectedRelative() != null)
                    {
                        relativeId = SessionManager.Instance.GetSelectedRelative().relativeID;
                    }

                    AuditLogField(userLogin,
                                apptId,
                                unitnum,
                                type,
                                hraAttributeList,
                                relativeId);
                }
                catch (Exception exc)
                {
                    Logger.Instance.WriteToLog(exc.ToString());
                }
            }
        }


        /*******************************************************************************/

        protected void AuditFullObject()
        {
            string hraAttributeList = "";

            foreach (FieldInfo field in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (IsAuditable(field))
                {
                    object val = field.GetValue(this);
                    if (val != null)
                    {
                        hraAttributeList += field.Name + "=" + val + "|";
                    }
                }
            }
            ExecuteAudit(hraAttributeList);
        }

        /*******************************************************************************/
        public void DoLoadWithSpAndParams(String spName, ParameterCollection sPparams)
        {
            SetInternationalCulture();
            try
            {
                //////////////////////
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();

                    SqlCommand cmdProcedure = new SqlCommand(spName, connection)
                    {
                        CommandType = CommandType.StoredProcedure,

                        //change command timeout from default to 5 minutes
                        CommandTimeout = 300
                    };

                    if (sPparams != null)
                    {
                        foreach (string param in sPparams.getKeys())
                        {
                            cmdProcedure.Parameters.Add("@" + param, sPparams[param].sqlType);
                            cmdProcedure.Parameters["@" + param].Value = sPparams[param].obj;
                        }
                    }
                    try
                    {
                        SqlDataReader reader = cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection);
                        while (reader.Read())
                        {
                            if (_loadThread.CancellationPending)
                            {
                                reader.Close();
                                return;
                            }

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                if (reader.IsDBNull(i) == false)
                                {
                                    foreach (
                                        FieldInfo fi in
                                            this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                                    {
                                        string name = fi.Name;
                                        if (name == reader.GetName(i))
                                        {
                                            fi.SetValue(this, reader.GetValue(i));
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        reader.Close();
                    }
                    catch (Exception exception)
                    {
                        Logger.Instance.WriteToLog("**** " + this + "\n" + exception);
                    }
                } //end of using connection
            }
            catch (Exception exc)
            {
                Logger.Instance.WriteToLog("[DoLoadWithSpAndParams] Executing Stored Procedure - " + exc);
            }
        }

        private void SetInternationalCulture()
        {
#if international
            string region = RiskApps3.Utilities.Configurator.getNodeValue("globals", "CultureRegion");

            if (string.IsNullOrEmpty(region) == false)
            {
                CultureInfo culture = new CultureInfo(region);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }
#endif
        }

        /*******************************************************************************/
        public virtual void BackgroundPersistWork(HraModelChangedEventArgs e)
        {
        }

        /*******************************************************************************/
        public void DoPersistWithSpAndParams(HraModelChangedEventArgs e, String spName, ref ParameterCollection sPparams)
        {
            lock (BCDB2.Instance)
            {
                if (e.Persist == false)
                    return;

                try
                {
                    //////////////////////
                    using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                    {
                        connection.Open();

                        SqlCommand cmdProcedure = new SqlCommand(spName, connection)
                        {
                            CommandType = CommandType.StoredProcedure,
                            CommandTimeout = 300
                        };
                        //change command timeout from default to 5 minutes

                        if (e.Delete)
                        {
                            cmdProcedure.Parameters.Add("@delete", SqlDbType.Bit);
                            cmdProcedure.Parameters["@delete"].Value = e.Delete;
                        }

                        cmdProcedure.Parameters.Add("@user", SqlDbType.NVarChar);
                        if (SessionManager.Instance.ActiveUser == null)
                        {
                            cmdProcedure.Parameters["@user"].Value = "Not Available";
                        }
                        else
                        {
                            cmdProcedure.Parameters["@user"].Value = SessionManager.Instance.ActiveUser.userLogin;
                        }

                        if (sPparams != null)
                        {
                            foreach (string param in sPparams.getKeys())
                            {
                                if (sPparams[param].Size > 0)
                                {
                                    cmdProcedure.Parameters.Add("@" + param, sPparams[param].sqlType, sPparams[param].Size);
                                }
                                else
                                {
                                    cmdProcedure.Parameters.Add("@" + param, sPparams[param].sqlType);
                                }
                                cmdProcedure.Parameters["@" + param].Value = sPparams[param].obj;
                                if (sPparams[param].BiDirectional)
                                {
                                    cmdProcedure.Parameters["@" + param].Direction = ParameterDirection.InputOutput;
                                }
                            }
                        }
                        try
                        {
                            if (e.updatedMembers.Count > 0)
                            {
                                foreach (FieldInfo fi in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                                {
                                    string name = fi.Name;

                                    foreach (MemberInfo mi in e.updatedMembers)
                                    {
                                        if (name == mi.Name)
                                        {
                                            SqlDbType sType = BCDB2.Instance.GetSqlTypeFromModel(fi.FieldType);
                                            if (cmdProcedure.Parameters.Contains("@" + name))
                                            {
                                                if (cmdProcedure.Parameters["@" + name].Value.Equals(fi.GetValue(this)))
                                                {
                                                    //Logger.Instance.WriteToLog("[DoPersistWithSpAndParams] Executing Stored Procedure - "
                                                    //    + "Added the same parameter and value twice; parameter name = " + name + "; value = " + cmdProcedure.Parameters["@" + name].Value.ToString());
                                                }
                                                else
                                                {
                                                    Logger.Instance.WriteToLog("Attempted to Add the same parameter twice with differing values; first value = " + cmdProcedure.Parameters["@" + name].Value + ", "
                                                        + "Second value = " + fi.GetValue(this));
                                                }
                                                break; //don't add it
                                            }
                                            cmdProcedure.Parameters.Add("@" + name, sType);
                                            cmdProcedure.Parameters["@" + name].Value = fi.GetValue(this);
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (FieldInfo fi in this.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                                {
                                    bool persist = IsPersistable(fi);

                                    if (fi.FieldType == typeof(DateTime))
                                    {
                                        DateTime dt = (DateTime)fi.GetValue(this);
                                        if (!(dt > DateTime.MinValue && dt < DateTime.MaxValue))
                                            persist = false;
                                    }
                                    else if (fi.GetValue(this) == null)
                                    {
                                        persist = false;
                                    }

                                    if (persist)
                                    {
                                        SqlDbType sType = BCDB2.Instance.GetSqlTypeFromModel(fi.FieldType);
                                        if (cmdProcedure.Parameters.Contains("@" + fi.Name))
                                        {
                                            if (cmdProcedure.Parameters["@" + fi.Name].Value.Equals(fi.GetValue(this)))
                                            {
                                                Logger.Instance.WriteToLog("[DoPersistWithSpAndParams]  executing Stored Procedure ["
                                                    + cmdProcedure.CommandText
                                                    + "] - Added the same parameter and value twice; parameter name = " + fi.Name + "; value = " + cmdProcedure.Parameters["@" + fi.Name].Value);
                                            }
                                            else
                                            {
                                                Logger.Instance.WriteToLog("[DoPersistWithSpAndParams] executing Stored Procedure - ["
                                                    + cmdProcedure.CommandText
                                                + "] - Attempted to Add the same parameter twice with differing values; first value = " + cmdProcedure.Parameters["@" + fi.Name].Value + ", "
                                                    + "Second value = " + fi.GetValue(this));
                                            }
                                            continue; //don't add it
                                        }
                                        cmdProcedure.Parameters.Add("@" + fi.Name, sType);
                                        cmdProcedure.Parameters["@" + fi.Name].Value = (fi.GetValue(this) is Double ? (Double.IsNaN((Double)(fi.GetValue(this))) ? null : fi.GetValue(this)) : fi.GetValue(this));
                                    }
                                }
                            }

                            PersistWithSpAndParamsChattyDebug();

                            cmdProcedure.ExecuteNonQuery();

                            foreach (SqlParameter p in cmdProcedure.Parameters)
                            {
                                if (p.Direction == ParameterDirection.InputOutput)
                                {
                                    if (sPparams != null) sPparams.Add(p.ParameterName.Replace("@", ""), p.Value);
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            Logger.Instance.WriteToLog("[DoPersistWithSpAndParams] Executing Stored Procedure - "
                                + cmdProcedure.CommandText + "; " + exception);
                        }
                    } //end of using connection
                }
                catch (Exception exc)
                {
                    Logger.Instance.WriteToLog("[DoPersistWithSpAndParams] Executing Stored Procedure - " + exc);
                }
            }
        }

        private void PersistWithSpAndParamsChattyDebug()
        {
#if (CHATTY_DEBUG)
            
            string AppName;
            string sqlStr = "(SELECT APP_NAME())";

            try
            {
                SqlCommand cmd = new SqlCommand(sqlStr, connection);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    if (reader.Read() != null)
                    {
                        AppName = reader.GetString(0);
                        //Logger.Instance.WriteToLog("APPNAME=" + AppName);
                    }
                    reader.Close();
                }
            }
            catch (Exception ee)
            {
                Logger.Instance.WriteToLog("APP_NAME() Failed " + ee.ToString());
            }

            string msg = "*** Do Persist by : " + this.ToString() + System.Environment.NewLine;
            if (e.sendingView != null)
            {
                msg += "Sending View: " + e.sendingView + System.Environment.NewLine;
            }

            msg += "Sql: " + cmdProcedure.CommandText + System.Environment.NewLine;
            foreach (IDataParameter i in cmdProcedure.Parameters)
            {
                msg += i.ParameterName + ": " + i.Value + System.Environment.NewLine;
            }
                            
            Logger.Instance.DebugToLog(msg);
#endif
        }

        public static void AuditUserLogin(string userName)
        {
            //auditLogField(userName, -1, "", type, "userLogin=" + userName + "|", -1);

            ParameterCollection pc = new ParameterCollection();
            pc.Add("application", "RiskApps3");
            pc.Add("userLogin", userName);
            pc.Add("machineName", Environment.MachineName);
            pc.Add("message", "Logged in");

            BCDB2.Instance.RunSPWithParams("sp_3_AuditUserActivity", pc);
        }
        /*******************************************************************************/
        private static void AuditLogField(string user, int apptId, string unitnum, string type,  string valueList, int relativeId)
        {
            if (string.IsNullOrEmpty(valueList))
            {
                return;
            }

            SqlCommand cmdProcedure = new SqlCommand();
            try
            {
                //////////////////////
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();

                    cmdProcedure = new SqlCommand("sp_3_Save_Audit_HraAttribute", connection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 300
                    };
                    //change command timeout from default to 5 minutes
                    cmdProcedure.Parameters.Add("@user", SqlDbType.NVarChar);
                    cmdProcedure.Parameters.Add("@unitnum", SqlDbType.NVarChar);
                    cmdProcedure.Parameters.Add("@hraObject", SqlDbType.NVarChar);
                    cmdProcedure.Parameters.Add("@hraAttributeValueList", SqlDbType.NVarChar);
                    cmdProcedure.Parameters.Add("@relativeID", SqlDbType.Int);
                    cmdProcedure.Parameters.Add("@apptID", SqlDbType.Int);

                    cmdProcedure.Parameters["@user"].Value = user;
                    cmdProcedure.Parameters["@unitnum"].Value = unitnum;
                    cmdProcedure.Parameters["@hraObject"].Value = type;
                    cmdProcedure.Parameters["@hraAttributeValueList"].Value = valueList;
                    cmdProcedure.Parameters["@relativeID"].Value = relativeId;
                    cmdProcedure.Parameters["@apptID"].Value = apptId;

                    connection.Close();

#if CHATTY_DEBUG
                    Logger.Instance.DebugToLog("[auditLogField] type=" + type + ", parameters='" + valueList);
#endif
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog("[auditLogField] Executing Stored Procedure " + cmdProcedure + e);
            }
        }

        /*******************************************************************************/
        private void DbThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            HraState = States.Ready;
            LoadCompleted(sender, e);

            if (Loaded != null)
            {
#if (CHATTY_DEBUG)
                string msg2 = "*** HRA OBJECT Load Finished Firing on : " + this.ToString() + System.Environment.NewLine;
                        foreach (Delegate d in Loaded.GetInvocationList())
                        {
                            msg2 += "FOR: " + d.Target + Environment.NewLine;
                        }
                        Logger.Instance.DebugToLog(msg2);
#endif
                Loaded(this, e);
            }
        }
        
        /*******************************************************************************/
        public void ForceLoadedEvent()
        {
            HraState = States.Ready;
            if (Loaded != null)
            {
                Loaded(this, null);
            }
        }

        /*******************************************************************************/

        private void PersistThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PersistCompleted(sender, e);

            if (Persisted != null)
                Persisted(this, e);
        }

        /*******************************************************************************/

        protected virtual void LoadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        /*******************************************************************************/

        protected virtual void LoadReportsProgress(object sender, ProgressChangedEventArgs e)
        {
        }

        /*******************************************************************************/

        protected virtual void PersistCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        /*******************************************************************************/

        protected virtual void PersistReportsProgress(object sender, ProgressChangedEventArgs e)
        {
        }

        /*******************************************************************************/

        [PublicAPI]
        public void ReportLoadProgress(int percent, ProgressChangedEventArgs e)
        {
            _loadThread.ReportProgress(percent, e);
        }

        /*******************************************************************************/

        private void DbThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            LoadReportsProgress(sender, e);
        }

        /*******************************************************************************/

        private void PersistThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PersistReportsProgress(sender, e);
        }

        /*******************************************************************************/

        private void RunPersist(HraModelChangedEventArgs e)
        {
            //TODO use Task Parallel Library because its *WAY* easier to get this stuff correct!!!
            if (!ReadOnly)
            {
                lock (_persistThread)
                {
                    _changeEventArgQueue.Enqueue(e);

                    if (_persistThread.IsBusy == false)
                    {
                        RunPersistChattyDebugThreadyNotBusy();
                        _persistThread.RunWorkerAsync(e);
                    }
                    else
                    {
                        RunPersistChattyDebugThreadBusy();
                    }
                }
            }
        }

        private static void RunPersistChattyDebugThreadBusy()
        {
#if (CHATTY_DEBUG)
            string msg = "*** reusing persist thread : " + this.ToString() + System.Environment.NewLine;
            Logger.Instance.DebugToLog(msg);
#endif
        }

        private static void RunPersistChattyDebugThreadyNotBusy()
        {
#if (CHATTY_DEBUG)
            string msg = "*** Starting persist thread : " + this.ToString() + System.Environment.NewLine;
            Logger.Instance.DebugToLog(msg);
#endif
        }

        /*******************************************************************************/

        public void SignalModelChanged(HraModelChangedEventArgs e)
        {
            //TODO should very strongly consider moving this to some kind of object state manager class which can utilize connection and thread pooling
            if (HttpContext.Current != null)
                e.Persist = false;
            if (e.Persist)
                RunPersist(e);

            if (Changed != null)
            {
                #if (CHATTY_DEBUG)
                string msg = "*** Changed event fired on : " + this.ToString() + System.Environment.NewLine;
                if (e != null)
                {
                    if (e.sendingView != null)
                        msg += "FROM: " + e.sendingView.ToString();

                    if (e.updatedMembers != null)
                    {
                        foreach (MemberInfo mi in e.updatedMembers)
                        {
                            msg += "Member: " + mi.ToString() + Environment.NewLine;
                        }
                    }
                }

                Logger.Instance.DebugToLog(msg);
                #endif
                Changed(this, e);
            }
        }

        protected void SignalMemberChanged(MemberInfo info)
        {
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.updatedMembers.Add(info);
            SignalModelChanged(args);
        }

        private PropertyInfo[] _properties; 

        public PropertyInfo GetPropertyWithNameLike(string memberName)
        {
            //TODO use hash map instead of []
            if (this._properties == null)
            {
                this._properties = this.GetType().GetProperties();
            }
            return this._properties.FirstOrDefault(prop =>
                prop.Name.EndsWith(memberName, StringComparison.CurrentCultureIgnoreCase));
        }

        public MemberInfo GetMemberByName(String memberName)
        {
            MemberInfo[] mis = GetType().GetMember(memberName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (mis.Length > 0)
            {
                return mis[0];
            }
            return null;
        }

        private static bool IsPersistable(MemberInfo memberInfo)
        {
            Object[] myAttributes = memberInfo.GetCustomAttributes(true);
            if (myAttributes.Length > 0)
            {
                foreach (object attr in myAttributes)
                {
                    if (attr.GetType().ToString().EndsWith("HraAttribute"))
                    {
                        HraAttribute attribute = (HraAttribute)attr;
                        if (attribute.persistable)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static bool IsPersistable(FieldInfo fieldInfo)
        {
            return IsPersistable((MemberInfo)fieldInfo);
        }

        private static bool IsAuditable(MemberInfo memberInfo)
        {
            Object[] myAttributes = memberInfo.GetCustomAttributes(true);
            if (myAttributes.Length > 0)
            {
                foreach (object attr in myAttributes)
                {
                    if (attr.GetType().ToString().EndsWith("HraAttribute"))
                    {
                        HraAttribute attribute = (HraAttribute)attr;
                        if (attribute.auditable)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static bool IsAuditable(FieldInfo fieldInfo)
        {
            return IsAuditable((MemberInfo) fieldInfo);
        }

        public static bool DoesAffectTestingDecision(MemberInfo memberInfo)
        {
            Object[] myAttributes = memberInfo.GetCustomAttributes(true);
            if (myAttributes.Length > 0)
            {
                foreach (object attr in myAttributes)
                {
                    if (attr.GetType().ToString().EndsWith("HraAttribute"))
                    {
                        HraAttribute attribute = (HraAttribute)attr;
                        if (attribute.affectsTestingDecision)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        [PublicAPI]
        public static bool DoesAffectRiskProfile(MemberInfo memberInfo)
        {
            Object[] myAttributes = memberInfo.GetCustomAttributes(true);
            if (myAttributes.Length > 0)
            {
                foreach (object attr in myAttributes)
                {
                    if (attr.GetType().ToString().EndsWith("HraAttribute"))
                    {
                        HraAttribute attribute = (HraAttribute)attr;
                        if (attribute.affectsRiskProfile)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void AddHandlersWithLoad(ChangedEventHandler changedEventHandler,
                                  LoadFinishedEventHandler loadFinishedEventHandler,
                                  PersistFinishedEventHandler persistFinishedEventHandler)
        {

            #if (CHATTY_DEBUG)
            string msg = "*** HRA OBJECT AddHandlersWithLoad on : " + this.ToString() + System.Environment.NewLine;
            if (changedEventHandler != null)
                msg+= "By: " + changedEventHandler.Target.ToString();
            else if (loadFinishedEventHandler != null)
                msg += "By: " + loadFinishedEventHandler.Target.ToString();
            else if (persistFinishedEventHandler != null)
                msg += "By: " + persistFinishedEventHandler.Target.ToString();

            Logger.Instance.DebugToLog(msg);

            #endif


            if (changedEventHandler != null)
            {
                if (Changed == null)
                    Changed += changedEventHandler;
                else
                {
                    bool ok = true;
                    foreach (Delegate d in Changed.GetInvocationList())
                    {
                        if (d.Target == changedEventHandler.Target)
                            ok = false;
                    }
                    if (ok)
                        Changed += changedEventHandler;
                }
            }
            if (loadFinishedEventHandler != null)
            {
                if (Loaded == null)
                    Loaded += loadFinishedEventHandler;
                else
                {
                    bool ok = true;
                    foreach (Delegate d in Loaded.GetInvocationList())
                    {
                        if (d.Target == loadFinishedEventHandler.Target)
                            ok = false;
                    }
                    if (ok)
                        Loaded += loadFinishedEventHandler;
                }
            }
            if (persistFinishedEventHandler != null)
            {
                if (Persisted == null)
                    Persisted += persistFinishedEventHandler;
                else
                {
                    bool ok = true;
                    foreach (Delegate d in Persisted.GetInvocationList())
                    {
                        if (d.Target == persistFinishedEventHandler.Target)
                            ok = false;
                    }
                    if (ok)
                        Persisted += persistFinishedEventHandler;
                }
            }

            switch (HraState)
            {
                case States.Null:
                    LoadObject();
                    break;

                case States.Loading:
                    break;

                case States.Ready:
                    if (loadFinishedEventHandler != null)
                    {
                        RunWorkerCompletedEventArgs dummy = new RunWorkerCompletedEventArgs(this, null, false);
                        
                        #if (CHATTY_DEBUG)
                            string msg2 = "*** HRA OBJECT loadFinishedEventHandler Firing on : " + this.ToString() + System.Environment.NewLine;
                            msg2 += "FOR " + loadFinishedEventHandler.Target.ToString();
                            Logger.Instance.DebugToLog(msg2);
                        #endif

                        loadFinishedEventHandler.Invoke(null, dummy);
                    }
                    break;
            }
        }

        [PublicAPI]
        protected void SetFieldInfoValue(FieldInfo fi, Object value)
        {
            SetFieldInfoValue(fi, this, value);
        }

        public static void SetFieldInfoValue(FieldInfo fi, Object value, Object obj)
        {
            switch (fi.FieldType.Name)
            {
                case "Int16":
                    fi.SetValue(obj, (Int16) value);
                    break;
                case "UInt16":
                    fi.SetValue(obj, (UInt16) value);
                    break;
                case "Int32":
                    fi.SetValue(obj, (Int32) value);
                    break;
                case "UInt32":
                    fi.SetValue(obj, (UInt32) value);
                    break;
                case "Int64":
                    fi.SetValue(obj, (Int64) value);
                    break;
                case "UInt64":
                    fi.SetValue(obj, (UInt64) value);
                    break;
                case "Single":
                    fi.SetValue(obj, (float) value);
                    break;
                case "Double":
                    fi.SetValue(obj, (double) value);
                    break;
                case "Decimal":
                    fi.SetValue(obj, (decimal) value);
                    break;
                case "Byte":
                    fi.SetValue(obj, (byte) value);
                    break;
                case "SByte":
                    fi.SetValue(obj, (sbyte) value);
                    break;
                case "DateTime":
                    fi.SetValue(obj, (DateTime) value);
                    break;
                default:
                    if (fi.FieldType.ToString().Contains("System.Nullable") && fi.FieldType.ToString().Contains("System.DateTime")) fi.SetValue(obj, (DateTime?)value);
                    else if (fi.FieldType.Name == "String") fi.SetValue(obj, (string)value);
                    else if (fi.FieldType.Name == "Boolean") fi.SetValue(obj, (bool)value);
                    break;
            }
        }

        public override string ToString()
        {
            string readOnly = this.ReadOnly ? "Readonly: " : string.Empty;
            return string.Format("{0}{1}", readOnly, base.ToString());
        }
    }
}
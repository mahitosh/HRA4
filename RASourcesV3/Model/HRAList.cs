//#define CHATTY_DEBUG
//#define international

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using RiskApps3.Model.Security;
using RiskApps3.Utilities;

#if international
using System.Globalization;
using System.Threading;
#endif 

namespace RiskApps3.Model
{
    public class HRAList<T> : List<T> where T : HraObject
    {
        public event ListChangedEventHandler ListChanged;
        public event LoadFinishedEventHandler LoadFinished;
        public event ListItemChangedEventHandler ListItemChanged;


        public delegate void ListChangedEventHandler(HraListChangedEventArgs e);
        public delegate void LoadFinishedEventHandler(HraListLoadedEventArgs e);
        public delegate void ListItemChangedEventHandler(object sender, HraModelChangedEventArgs e);

        public BackgroundWorker LoadThread;

        private bool loaded = false;
        private int load_signal_count = 0;

        public bool IsLoaded
        {
            get
            {
                return loaded;
            }
            set
            {
                loaded = value;
            }
        }

        public HRAList()
        {
            LoadThread = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = false
            };
            LoadThread.DoWork += this.DbThread_DoWork;


            LoadThread.RunWorkerCompleted +=
                this.DbThread_RunWorkerCompleted;

        }

        /*******************************************************************************/
        private void DbThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loaded = true;
            HraListLoadedEventArgs args = new HraListLoadedEventArgs();
            args.sender = this;
            args.workerArgs = e;

            ListLoadingComplete();

            if (LoadFinished != null)
                LoadFinished(args);
        }
        /*******************************************************************************/
        public void ForceListLoadedEvent()
        {
            loaded = true;
            HraListLoadedEventArgs args = new HraListLoadedEventArgs();
            args.sender = this;

            ListLoadingComplete();

            if (LoadFinished != null)
                LoadFinished(args);
        }

        public virtual void PersistFullList(HraModelChangedEventArgs e)
        {
            foreach (HraObject o in this)
            {
                o.PersistFullObject(e);
            }
        }
        public virtual void BackgroundListLoad()
        {

        }
        protected virtual void ListLoadingComplete()
        {

        }

        protected void DoListLoad(LoadListArgs lla)
        {
            lock (this)
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
                this.Clear();
                //TODO initial contents are never attached to ItemChanged - is this intentional?
                this.AddRange(lla.initialContents);

                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();

                    SqlCommand cmdProcedure = new SqlCommand(lla.sp, connection);
                    cmdProcedure.CommandType = CommandType.StoredProcedure;
                    cmdProcedure.CommandTimeout = 600;
                    foreach (string param in lla.sPparams.getKeys())
                    {
                        cmdProcedure.Parameters.Add("@" + param, lla.sPparams[param].sqlType);
                        cmdProcedure.Parameters["@" + param].Value = lla.sPparams[param].obj;
                    }
                    string current = "";
                    try
                    {
                        SqlDataReader reader = cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection);

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                object o = Activator.CreateInstance(lla.type, lla.constructor_params);
                                T localHraObject = (T)o;
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    if (reader.IsDBNull(i) == false)
                                    {
                                        current = reader.GetName(i);
                                        foreach (FieldInfo fi in lla.type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                                        {
                                            string name = fi.Name;
                                            if (string.Compare(name, reader.GetName(i), true) == 0)
                                            {
                                                HraObject.SetFieldInfoValue(fi, reader.GetValue(i), localHraObject);
                                                break;
                                            }
                                        }
                                    }
                                }

                                localHraObject.HraState = HraObject.States.Ready;
                                this.AddToList(localHraObject, new HraModelChangedEventArgs(null), false);
                            }
                            reader.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Instance.WriteToLog("Error on " + current + " : " + ex.ToString());
                    }
                }
                loaded = true;
            }
        }
        /**************************************************************************************************/
        private void ItemChanged(object sender, HraModelChangedEventArgs e)
        {
            if (ListItemChanged != null)
                ListItemChanged(sender, e);
        }
        /*******************************************************************************/

        private void DbThread_DoWork(object sender, DoWorkEventArgs e)
        {

            while (load_signal_count > 0)
            {
                BackgroundListLoad();
                load_signal_count--;
            }

        }

        /// <summary>
        /// Adds the object to this <code>HRAList</code> given event args.
        /// </summary>
        /// <param name="hraObject">to add</param>
        /// <param name="args">args (members changed, etc...)</param>
        /// <param name="signalModelChanged">to immediately persist or not</param>
        public void AddToList(T hraObject, HraModelChangedEventArgs args, bool signalModelChanged = true)
        {
            //todo consider making this less accessible
            lock (this)
            {
                if (this.Contains(hraObject))
                    return;

                if (signalModelChanged)
                {
                    hraObject.SignalModelChanged(args);
                }

                hraObject.Changed += ItemChanged;

                this.Add(hraObject);
                if (ListChanged != null)
                {
                    ListChanged(new HraListChangedEventArgs(HraListChangedEventArgs.HraListChangeType.ADD, hraObject));
                }
            }
        }

        /// <summary>
        /// Iterates over <code>Remove(x)</code> foreach item matching <code>match</code>.
        /// Persists changes to db.
        /// </summary>
        /// <param name="match">condition for which to remove items from this</param>
        /// <returns>number of items removed</returns>
        public int RemoveAll(Func<T, bool> match)
        {
            List<T> toRemove = this.Where(match).ToList();
            toRemove.ForEach(gone => this.RemoveFromList(gone, null));
            return toRemove.Count;
        } 

        public bool RemoveFromList(T hraObject, SecurityContext p_securityContext)
        {
            bool removed = false;
            HraModelChangedEventArgs args = new HraModelChangedEventArgs(null);
            args.Delete = true;
            hraObject.SignalModelChanged(args);
            hraObject.ReleaseListeners(this);
            if (this.Remove(hraObject))
            {
                removed = true;
                if (ListChanged != null)
                {
                    ListChanged(new HraListChangedEventArgs(HraListChangedEventArgs.HraListChangeType.DELETE, hraObject));
                }
            }
            return removed;
        }
        /*******************************************************************************/
        public virtual void LoadFullList()
        {
            if (!loaded)
            {
                BackgroundListLoad();

                foreach(T t in this)
                {
                    t.LoadFullObject();
                }
            }
        }
        /*******************************************************************************/
        public virtual void LoadList()
        {
            loaded = false;
            load_signal_count++;

            if (LoadThread.IsBusy == false)
            {
                LoadThread.RunWorkerAsync();
            }
                
        }


        public void AddHandlersWithLoad(ListChangedEventHandler listChangedEventHandler, 
                                  LoadFinishedEventHandler loadFinishedEventHandler,
                                  ListItemChangedEventHandler itemChangedEventHandler)
        {
            #if (CHATTY_DEBUG)
            string msg = "*** HRA LIST AddHandlersWithLoad on : " + this.ToString() + System.Environment.NewLine;
            if (listChangedEventHandler != null)
                msg += "By: " + listChangedEventHandler.Target.ToString();
            else if (loadFinishedEventHandler != null)
                msg += "By: " + loadFinishedEventHandler.Target.ToString();
            else if (itemChangedEventHandler != null)
                msg += "By: " + itemChangedEventHandler.Target.ToString();

            Logger.Instance.DebugToLog(msg);
            #endif

            if (listChangedEventHandler != null)
            {
                if (ListChanged == null)
                    ListChanged += listChangedEventHandler;
                else
                {
                    bool ok = true;
                    foreach (Delegate d in ListChanged.GetInvocationList())
                    {
                        if (d.Target == listChangedEventHandler.Target)
                            ok = false;
                    }
                    if (ok)
                        ListChanged += listChangedEventHandler;
                }
            }

            if (loadFinishedEventHandler != null)
            {
                if (LoadFinished == null)
                    LoadFinished += loadFinishedEventHandler;
                else
                {
                    bool ok = true;
                    foreach (Delegate d in LoadFinished.GetInvocationList())
                    {
                        if (d.Target == loadFinishedEventHandler.Target)
                            ok = false;
                    }
                    if (ok)
                        LoadFinished += loadFinishedEventHandler;
                }
            }

            if (itemChangedEventHandler != null)
            {
                if (ListItemChanged == null)
                    ListItemChanged += itemChangedEventHandler;
                else
                {
                    bool ok = true;
                    foreach (Delegate d in ListItemChanged.GetInvocationList())
                    {
                        if (d.Target == loadFinishedEventHandler.Target)
                            ok = false;
                    }
                    if (ok)
                        ListItemChanged += itemChangedEventHandler;
                }
            }

            if (loaded)
            {
                if (loadFinishedEventHandler != null)
                {
                    HraListLoadedEventArgs args = new HraListLoadedEventArgs();
                    args.sender = this;
                    args.workerArgs = new RunWorkerCompletedEventArgs(this, null, false);
                    loadFinishedEventHandler.Invoke(args);
                }
            }
            else
            {
                LoadList();
            }
            
        }

        public virtual void ReleaseListeners(object target)
        {
            if (ListChanged != null)
            {
                foreach (Delegate d in ListChanged.GetInvocationList())
                {
                    if (target == null || d.Target == target)
                        ListChanged -= (ListChangedEventHandler)d;
                }
            }
            if (LoadFinished != null)
            {
                foreach (Delegate d in LoadFinished.GetInvocationList())
                {
                    if (target == null || d.Target == target)
                        LoadFinished -= (LoadFinishedEventHandler)d;
                }
            }
            if (ListItemChanged != null)
            {
                foreach (Delegate d in ListItemChanged.GetInvocationList())
                {
                    if (target == null || d.Target == target)
                        ListItemChanged -= (ListItemChangedEventHandler)d;
                }
            }

            foreach (T ho in this)
            {
                ho.ReleaseListeners(target);
            }
        }

        protected class LoadListArgs
        {
            public string sp;
            public ParameterCollection sPparams;
            public Type type;
            public object[] constructor_params;
            public List<T> initialContents;
            private string sp1;
            private ParameterCollection sPparams1;
            private object[] constructor_params1;
            private List<T> initialContents1;

            protected internal LoadListArgs(string sp, ParameterCollection sPparams, object[] constructor_params, List<T> initialContents)
            {
                this.type = typeof(T);
                this.sp = sp;
                this.sPparams = sPparams;
                this.constructor_params = constructor_params;
                this.initialContents = initialContents;
            }

            protected internal LoadListArgs(string sp, ParameterCollection sPparams, object[] constructor_params)
                : this(sp, sPparams, constructor_params, new List<T>()) { }
        }
    }
}
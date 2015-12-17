using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using RiskApps3.Utilities;

namespace RiskApps3.Model.Session
{
    /// <summary>
    /// Provides iteration over a set of lookup data (lkpLookups) 
    /// for use in populating form fields and menus.
    /// </summary>
    internal class Lookups : HraObject, IDictionary<string, List<string>>
    {
        private const string CmdText = "sp_3_LoadLkpLookups";
        private const string TableParamName = "@table";
        private const string ThisIsAReadonlyDictionary = "This is a readonly dictionary.";
        private const string Field = "field";
        private const string Value1 = "value1";
        private const string LogMessageFormat = "[Lookups.BackgroundLoadWork] - {0}";

        private readonly Dictionary<string, List<string>> _lookups;
        private readonly string _table;

        /// <summary>
        /// Creates a Lookups object
        /// </summary>
        /// <param name="table">used to filter the data</param>
        /// <remarks>
        /// Access lists of lookup data using the name of the field being 
        /// populated via an <code>IDictionary</code> interface.
        /// </remarks>
        public Lookups(string table)
        {
            this._table = table;
            this._lookups = new Dictionary<string, List<string>>();
        }

        public override void BackgroundLoadWork()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(BCDB2.Instance.getConnectionString()))
                {
                    connection.Open();

                    SqlCommand cmdProcedure = BuildCmdProcedure(connection);

                    SqlDataReader reader = cmdProcedure.ExecuteReader(CommandBehavior.CloseConnection);

                    while (reader.Read())
                    {
                        AddValuesToDictionary(reader);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Instance.WriteToLog(string.Format(LogMessageFormat, e));
            }
        }

        private SqlCommand BuildCmdProcedure(SqlConnection connection)
        {
            SqlCommand cmdProcedure = new SqlCommand(CmdText, connection)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 300
            };

            cmdProcedure.Parameters.Add(TableParamName, SqlDbType.NVarChar).Value = this._table;
            return cmdProcedure;
        }

        private void AddValuesToDictionary(SqlDataReader reader)
        {
            string field = reader[Field].ToString();
            string value = reader[Value1].ToString();

            if (this._lookups.ContainsKey(field))
            {
                this._lookups[field].Add(value);
            }
            else
            {
                this._lookups.Add(field, new List<string> {string.Empty, value});
            }
        }

        #region IDictionary methods
        public void Add(string key, List<string> value)
        {
            throw new NotImplementedException(ThisIsAReadonlyDictionary);
        }

        public bool ContainsKey(string key)
        {
            return this._lookups.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return this._lookups.Keys; }
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException(ThisIsAReadonlyDictionary);
        }

        public bool TryGetValue(string key, out List<string> value)
        {
            return this._lookups.TryGetValue(key, out value);
        }

        public ICollection<List<string>> Values
        {
            get { return this._lookups.Values; }
        }

        public List<string> this[string key]
        {
            get { return this._lookups[key]; }
            set { throw new NotImplementedException(ThisIsAReadonlyDictionary); }
        }

        public void Add(KeyValuePair<string, List<string>> item)
        {
            throw new NotImplementedException(ThisIsAReadonlyDictionary);
        }

        public void Clear()
        {
            throw new NotImplementedException(ThisIsAReadonlyDictionary);
        }

        public bool Contains(KeyValuePair<string, List<string>> item)
        {
            return this._lookups.Contains(item);
        }

        public void CopyTo(KeyValuePair<string, List<string>>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, List<string>>>)this._lookups).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this._lookups.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(KeyValuePair<string, List<string>> item)
        {
            throw new NotImplementedException(ThisIsAReadonlyDictionary);
        }

        public IEnumerator<KeyValuePair<string, List<string>>> GetEnumerator()
        {
            return this._lookups.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}

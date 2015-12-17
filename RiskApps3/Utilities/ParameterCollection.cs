using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RiskApps3.Utilities
{
    public class ParameterCollection
    {
        Dictionary<string, ParameterTuple> map;

        public ParameterCollection()
        {
            map = new Dictionary<string, ParameterTuple>();
        }

        public ParameterCollection(string paramName, Object paramValue) : this()
        {
            Add(paramName, paramValue);
            
        }
        public bool ContainsParameter(string param)
        {
            return map.ContainsKey(param);
        }
        public void Add(string paramName, Object paramValue)
        {
            if (paramValue != null)
            {
                ParameterTuple tuple = new ParameterTuple(paramValue);
                map[paramName] = tuple;
            }
        }
        public void Add(string paramName, Object paramValue, bool BiDirectional)
        {
            Add(paramName, paramValue);
            map[paramName].BiDirectional = BiDirectional;
        }
        public void Add(string paramName, Object paramValue, bool BiDirectional, int size)
        {
            Add(paramName, paramValue);
            map[paramName].BiDirectional = BiDirectional;
            map[paramName].Size = size;
        }
        public Object Get(string paramName)
        {
            return map[paramName];
        }

        public Dictionary<string, ParameterTuple>.KeyCollection getKeys()
        {
            return map.Keys;
        }

        public ParameterTuple this[string s]
        {
            get
            {
                return map[s];
            }
            set
            {
                map[s] = value;
            }
        }

        public void Clear()
        {
            map.Clear();   
        }
    }

    public class ParameterTuple
    {
        private bool m_BiDirectional = false;
        public bool BiDirectional
        {
            get
            {
                return m_BiDirectional;
            }
            set
            {
                m_BiDirectional = value;
            }
        }

        private int m_Size = -1;
        public int Size
        {
            get
            {
                return m_Size;
            }
            set
            {
                m_Size = value;
            }
        }

        private SqlDbType m_sqlType;
        public SqlDbType sqlType
        {
            get
            {
                return m_sqlType;
            }
            set
            {
                m_sqlType = value;
            }
        }

        private Object m_obj;
        public Object obj
        {
            get
            {
                return m_obj;
            }
            set
            {
                m_sqlType = BCDB2.Instance.GetSqlTypeFromModel(value.GetType());
                m_obj = value;
            }
        }

        public ParameterTuple()
        {
        }

        public ParameterTuple(Object myObj)
        {
            this.obj = myObj;
        }


    }
}

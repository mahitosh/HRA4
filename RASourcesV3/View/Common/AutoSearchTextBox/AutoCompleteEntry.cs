using System;

namespace RiskApps3.View.Common.AutoSearchTextBox
{
    /// <summary>
    /// Summary description for AutoCompleteDictionaryEntry.
    /// </summary>
    [Serializable]
    public class AutoCompleteEntry : IAutoCompleteEntry {
        private string[] matchStrings;
        private readonly object _value;

        public string[] MatchStrings {
            get {
                if (matchStrings == null) {
                    matchStrings = new string[] {DisplayName};
                }
                return matchStrings;
            }
        }

        private string displayName = string.Empty;

        public string DisplayName {
            get { return displayName; }
            set { displayName = value; }
        }

        public object Value
        {
            get {
                if (this._value == null)
                {
                    return DisplayName;
                }
                else
                {
                    return this._value;
                }
            }
        }

        public AutoCompleteEntry() {
        }

        public AutoCompleteEntry(string name, params string[] matchList) {
            displayName = name;
            matchStrings = matchList;
        }

        public AutoCompleteEntry(string name, object value, params string[] matchList) : this(name, matchList)
        {
            this._value = value;
        }

        public override string ToString() {
            return DisplayName;
        }
    }
}
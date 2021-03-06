﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.34014.
// 
#pragma warning disable 1591

namespace RiskApps3.HraCloudServices {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="RiskAppsCloudServicesSoap", Namespace="http://tempuri.org/")]
    public partial class RiskAppsCloudServices : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback FetchCompletedSurveysOperationCompleted;
        
        private System.Threading.SendOrPostCallback FetchPatientRecordOperationCompleted;
        
        private System.Threading.SendOrPostCallback SurveySummaryOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteSurveyOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public RiskAppsCloudServices() {
            this.Url = global::RiskApps3.Properties.Settings.Default.RiskApps3_HraCloudServices_RiskAppsCloudServices;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        public RiskAppsCloudServices(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                this.Url = global::RiskApps3.Properties.Settings.Default.RiskApps3_HraCloudServices_RiskAppsCloudServices;
            }
            else
            {
                this.Url = url;
            }
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event FetchCompletedSurveysCompletedEventHandler FetchCompletedSurveysCompleted;
        
        /// <remarks/>
        public event FetchPatientRecordCompletedEventHandler FetchPatientRecordCompleted;
        
        /// <remarks/>
        public event SurveySummaryCompletedEventHandler SurveySummaryCompleted;
        
        /// <remarks/>
        public event DeleteSurveyCompletedEventHandler DeleteSurveyCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/FetchCompletedSurveys", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode FetchCompletedSurveys() {
            object[] results = this.Invoke("FetchCompletedSurveys", new object[0]);
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void FetchCompletedSurveysAsync() {
            this.FetchCompletedSurveysAsync(null);
        }
        
        /// <remarks/>
        public void FetchCompletedSurveysAsync(object userState) {
            if ((this.FetchCompletedSurveysOperationCompleted == null)) {
                this.FetchCompletedSurveysOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFetchCompletedSurveysOperationCompleted);
            }
            this.InvokeAsync("FetchCompletedSurveys", new object[0], this.FetchCompletedSurveysOperationCompleted, userState);
        }
        
        private void OnFetchCompletedSurveysOperationCompleted(object arg) {
            if ((this.FetchCompletedSurveysCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FetchCompletedSurveysCompleted(this, new FetchCompletedSurveysCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/FetchPatientRecord", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode FetchPatientRecord(int apptid) {
            object[] results = this.Invoke("FetchPatientRecord", new object[] {
                        apptid});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void FetchPatientRecordAsync(int apptid) {
            this.FetchPatientRecordAsync(apptid, null);
        }
        
        /// <remarks/>
        public void FetchPatientRecordAsync(int apptid, object userState) {
            if ((this.FetchPatientRecordOperationCompleted == null)) {
                this.FetchPatientRecordOperationCompleted = new System.Threading.SendOrPostCallback(this.OnFetchPatientRecordOperationCompleted);
            }
            this.InvokeAsync("FetchPatientRecord", new object[] {
                        apptid}, this.FetchPatientRecordOperationCompleted, userState);
        }
        
        private void OnFetchPatientRecordOperationCompleted(object arg) {
            if ((this.FetchPatientRecordCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.FetchPatientRecordCompleted(this, new FetchPatientRecordCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SurveySummary", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SurveySummary(int apptid) {
            object[] results = this.Invoke("SurveySummary", new object[] {
                        apptid});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void SurveySummaryAsync(int apptid) {
            this.SurveySummaryAsync(apptid, null);
        }
        
        /// <remarks/>
        public void SurveySummaryAsync(int apptid, object userState) {
            if ((this.SurveySummaryOperationCompleted == null)) {
                this.SurveySummaryOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSurveySummaryOperationCompleted);
            }
            this.InvokeAsync("SurveySummary", new object[] {
                        apptid}, this.SurveySummaryOperationCompleted, userState);
        }
        
        private void OnSurveySummaryOperationCompleted(object arg) {
            if ((this.SurveySummaryCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SurveySummaryCompleted(this, new SurveySummaryCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DeleteSurvey", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DeleteSurvey(int apptid) {
            this.Invoke("DeleteSurvey", new object[] {
                        apptid});
        }
        
        /// <remarks/>
        public void DeleteSurveyAsync(int apptid) {
            this.DeleteSurveyAsync(apptid, null);
        }
        
        /// <remarks/>
        public void DeleteSurveyAsync(int apptid, object userState) {
            if ((this.DeleteSurveyOperationCompleted == null)) {
                this.DeleteSurveyOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteSurveyOperationCompleted);
            }
            this.InvokeAsync("DeleteSurvey", new object[] {
                        apptid}, this.DeleteSurveyOperationCompleted, userState);
        }
        
        private void OnDeleteSurveyOperationCompleted(object arg) {
            if ((this.DeleteSurveyCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteSurveyCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void FetchCompletedSurveysCompletedEventHandler(object sender, FetchCompletedSurveysCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FetchCompletedSurveysCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FetchCompletedSurveysCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void FetchPatientRecordCompletedEventHandler(object sender, FetchPatientRecordCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class FetchPatientRecordCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal FetchPatientRecordCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void SurveySummaryCompletedEventHandler(object sender, SurveySummaryCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SurveySummaryCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SurveySummaryCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.33440")]
    public delegate void DeleteSurveyCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591
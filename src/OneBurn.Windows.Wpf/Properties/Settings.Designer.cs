﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OneBurn.Windows.Wpf.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.8.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("-1")]
        public int GeneralNetworkRetryCount {
            get {
                return ((int)(this["GeneralNetworkRetryCount"]));
            }
            set {
                this["GeneralNetworkRetryCount"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("00:00:10")]
        public global::System.TimeSpan GeneralNetworkTimeSpanBetweenRetries {
            get {
                return ((global::System.TimeSpan)(this["GeneralNetworkTimeSpanBetweenRetries"]));
            }
            set {
                this["GeneralNetworkTimeSpanBetweenRetries"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Material")]
        public global::Telerik.Windows.Controls.Theme GeneralAppearanceTheme {
            get {
                return ((global::Telerik.Windows.Controls.Theme)(this["GeneralAppearanceTheme"]));
            }
            set {
                this["GeneralAppearanceTheme"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%CommonApplicationData%\\OneBurn\\odae.key")]
        public string OpticalDriveAuthoringEngineKeyFilePath {
            get {
                return ((string)(this["OpticalDriveAuthoringEngineKeyFilePath"]));
            }
            set {
                this["OpticalDriveAuthoringEngineKeyFilePath"] = value;
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SonarQube.TeamBuild.PostProcessor {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SonarQube.TeamBuild.PostProcessor.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Loading the SonarQube analysis config from {0}.
        /// </summary>
        internal static string DIAG_LoadingConfig {
            get {
                return ResourceManager.GetString("DIAG_LoadingConfig", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube analysis could not be completed because the analysis configuration file could not be found.
        ///Expected location: {0}.
        /// </summary>
        internal static string ERROR_ConfigFileNotFound {
            get {
                return ResourceManager.GetString("ERROR_ConfigFileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube post-processing cannot be performed - required settings are missing.
        /// </summary>
        internal static string ERROR_MissingSettings {
            get {
                return ResourceManager.GetString("ERROR_MissingSettings", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube analysis failed.
        /// </summary>
        internal static string Report_AnalysisFailed {
            get {
                return ResourceManager.GetString("Report_AnalysisFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube analysis succeeded. [Analysis results] ({0}).
        /// </summary>
        internal static string Report_AnalysisSucceeded {
            get {
                return ResourceManager.GetString("Report_AnalysisSucceeded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid projects: {0}, skipped projects: {1}, excluded projects: {2}.
        /// </summary>
        internal static string Report_InvalidSkippedAndExcludedMessage {
            get {
                return ResourceManager.GetString("Report_InvalidSkippedAndExcludedMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Product projects: {0}, test projects: {1}.
        /// </summary>
        internal static string Report_ProductAndTestMessage {
            get {
                return ResourceManager.GetString("Report_ProductAndTestMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube project: {0} ({1}), version: {2}.
        /// </summary>
        internal static string Report_ProjectInfoSummary {
            get {
                return ResourceManager.GetString("Report_ProjectInfoSummary", resourceCulture);
            }
        }
    }
}

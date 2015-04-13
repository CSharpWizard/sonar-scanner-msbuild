﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SonarQube.TeamBuild.Integration {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SonarQube.TeamBuild.Integration.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Code coverage command line tool: {0}.
        /// </summary>
        internal static string CONV_DIAG_CommandLineToolInfo {
            get {
                return ResourceManager.GetString("CONV_DIAG_CommandLineToolInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attempting to locate the CodeCoverage.exe tool....
        /// </summary>
        internal static string CONV_DIAG_LocatingCodeCoverageTool {
            get {
                return ResourceManager.GetString("CONV_DIAG_LocatingCodeCoverageTool", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple versions of VS are installed: {0}.
        /// </summary>
        internal static string CONV_DIAG_MultipleVsVersionsInstalled {
            get {
                return ResourceManager.GetString("CONV_DIAG_MultipleVsVersionsInstalled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to locate the code coverage command line tool.
        /// </summary>
        internal static string CONV_ERROR_FailToFindConversionTool {
            get {
                return ResourceManager.GetString("CONV_ERROR_FailToFindConversionTool", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to convert the binary coverage file to XML. The expected output file was not found: {0}.
        /// </summary>
        internal static string CONV_ERROR_OutputFileNotFound {
            get {
                return ResourceManager.GetString("CONV_ERROR_OutputFileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Downloading coverage file from {0} t0 {1}.
        /// </summary>
        internal static string DOWN_DIAG_DownloadCoverageReportFromTo {
            get {
                return ResourceManager.GetString("DOWN_DIAG_DownloadCoverageReportFromTo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Required environment variable could not be found: {0}.
        /// </summary>
        internal static string MissingEnvironmentVariable {
            get {
                return ResourceManager.GetString("MissingEnvironmentVariable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fetching code coverage report information from TFS....
        /// </summary>
        internal static string PROC_DIAG_FetchingCoverageReportInfoFromServer {
            get {
                return ResourceManager.GetString("PROC_DIAG_FetchingCoverageReportInfoFromServer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No code coverage reports were found for the current build..
        /// </summary>
        internal static string PROC_DIAG_NoCodeCoverageReportsFound {
            get {
                return ResourceManager.GetString("PROC_DIAG_NoCodeCoverageReportsFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Updating project info files with code coverage information....
        /// </summary>
        internal static string PROC_DIAG_UpdatingProjectInfoFiles {
            get {
                return ResourceManager.GetString("PROC_DIAG_UpdatingProjectInfoFiles", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to More than one code coverage result file was created. Only one report can be uploaded to SonarQube. Please modify the build definition so either SonarQube analysis is disabled or the only platform/flavor is built.
        /// </summary>
        internal static string PROC_ERROR_MultipleCodeCoverageReportsFound {
            get {
                return ResourceManager.GetString("PROC_ERROR_MultipleCodeCoverageReportsFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SonarQube Analysis Summary.
        /// </summary>
        internal static string SonarQubeSummarySectionHeader {
            get {
                return ResourceManager.GetString("SonarQubeSummarySectionHeader", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Connecting to TFS....
        /// </summary>
        internal static string URL_DIAG_ConnectingToTfs {
            get {
                return ResourceManager.GetString("URL_DIAG_ConnectingToTfs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Coverage Id: {0}, Platform {1}, Flavor {2}.
        /// </summary>
        internal static string URL_DIAG_CoverageReportInfo {
            get {
                return ResourceManager.GetString("URL_DIAG_CoverageReportInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fetching build information....
        /// </summary>
        internal static string URL_DIAG_FetchingBuildInfo {
            get {
                return ResourceManager.GetString("URL_DIAG_FetchingBuildInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Fetch code coverage report info....
        /// </summary>
        internal static string URL_DIAG_FetchingCoverageReportInfo {
            get {
                return ResourceManager.GetString("URL_DIAG_FetchingCoverageReportInfo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ...done..
        /// </summary>
        internal static string URL_DIAG_Finished {
            get {
                return ResourceManager.GetString("URL_DIAG_Finished", resourceCulture);
            }
        }
    }
}

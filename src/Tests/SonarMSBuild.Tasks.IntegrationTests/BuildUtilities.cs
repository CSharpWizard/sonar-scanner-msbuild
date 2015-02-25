﻿//-----------------------------------------------------------------------
// <copyright file="BuildUtilities.cs" company="SonarSource SA and Microsoft Corporation">
//   (c) SonarSource SA and Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.Build.Construction;
using Microsoft.Build.Execution;
using Microsoft.Build.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TestUtilities;

namespace SonarMSBuild.Tasks.IntegrationTests
{
    internal static class BuildUtilities
    {
        #region Public methods

        /// <summary>
        /// Creates and returns an empty MSBuild project using the data in the supplied descriptor.
        /// The project will import the analysis targets file and the standard C# targets file.
        /// The project name and GUID will be set if the values are supplied in the descriptor.
        /// </summary>
        public static ProjectRootElement CreateMsBuildProject(TestContext testContext, ProjectDescriptor descriptor, IDictionary<string, string> preImportProperties)
        {
            ProjectRootElement root = CreateMinimalBuildableProject(testContext.TestDeploymentDir, preImportProperties);

            if (!string.IsNullOrEmpty(descriptor.ProjectName))
            {
                root.AddProperty(TargetProperties.ProjectName, descriptor.ProjectName);
            }

            if (descriptor.ProjectGuid != Guid.Empty)
            {
                root.AddProperty(TargetProperties.ProjectGuid, descriptor.ProjectGuid.ToString("D"));
            }

            if (descriptor.ManagedSourceFiles != null)
            {
                foreach (string managedInput in descriptor.ManagedSourceFiles)
                {
                    root.AddItem("Compile", managedInput);
                }
            }

            if (descriptor.IsTestProject)
            {
                //TODO
            }
            else
            {
                //TODO
            }

            return root;
        }

        /// <summary>
        /// Creates and returns a minimal project file that can be built.
        /// The project imports the C# targets and the Sonar analysis targets.
        /// </summary>
        /// <param name="sonarTargetsDir">The directory containing the Sonar targets</param>
        /// <param name="preImportProperties">Any properties that need to be set before the C# targets are imported. Can be null.</param>
        public static ProjectRootElement CreateMinimalBuildableProject(string sonarTargetsDir, IDictionary<string, string> preImportProperties)
        {
            Assert.IsTrue(Directory.Exists(sonarTargetsDir), "Test error: the specified directory does not exist. Path: {0}", sonarTargetsDir);

            ProjectRootElement root = ProjectRootElement.Create();

            if (preImportProperties != null)
            {
                foreach(KeyValuePair<string, string> kvp in preImportProperties)
                {
                    root.AddProperty(kvp.Key, kvp.Value);
                }
            }

            // Import the MsBuild Sonar targets file
            string fullAnalysisTargetPath = Path.Combine(sonarTargetsDir, TargetConstants.AnalysisTargetFileName);
            Assert.IsTrue(File.Exists(fullAnalysisTargetPath), "Test error: the analysis target file does not exist. Path: {0}", fullAnalysisTargetPath);
            root.AddImport(fullAnalysisTargetPath);

            // Set the location of the task assembly
            root.AddProperty(TargetProperties.SonarBuildTasksAssemblyFile, typeof(WriteProjectInfoFile).Assembly.Location);

            // Import the standard Microsoft targets
            root.AddImport("$(MSBuildToolsPath)\\Microsoft.CSharp.targets");
            root.AddProperty("OutputType", "library"); // build a library so we don't need a Main method

            return root;
        }

        /// <summary>
        /// Builds the specified target and returns the build result.
        /// </summary>
        /// <param name="project">The project to build</param>
        /// <param name="logger">The build logger to use. If null then a default logger will be used that dumps the build output to the console.</param>
        /// <param name="targets">Optional list of targets to execute</param>
        /// <returns></returns>
        public static BuildResult BuildTarget(ProjectInstance project, ILogger logger, params string[] targets)
        {
            BuildParameters parameters = new BuildParameters();
            parameters.Loggers = new ILogger[] { logger ?? new BuildLogger() };

            BuildRequestData requestData = new BuildRequestData(project, targets);

            BuildManager mgr = new BuildManager("testHost");
            BuildResult result = mgr.Build(parameters, requestData);

            return result;
        }

        /// <summary>
        /// Dumps the project properties to the console
        /// </summary>
        /// <param name="projectInstance">The owning project</param>
        /// <param name="title">Optional title to be written to the console</param>
        public static void DumpProjectProperties(ProjectInstance projectInstance, string title)
        {
            if (projectInstance == null)
            {
                throw new ArgumentNullException("projectInstance");
            }
            
            Console.WriteLine();
            Console.WriteLine("******************************************************");
            Console.WriteLine(title ?? "Project properties");
            foreach (ProjectPropertyInstance property in projectInstance.Properties ?? Enumerable.Empty<ProjectPropertyInstance>())
            {
                Console.WriteLine("{0} = {1}{2}",
                    property.Name,
                    property.EvaluatedValue,
                    property.IsImmutable ? ", IMMUTABLE" : null);
            }
            Console.WriteLine("******************************************************");
            Console.WriteLine();
        }

        #endregion

        #region Assertions

        /// <summary>
        /// Checks that building the specified target succeeded.
        /// </summary>
        public static void AssertTargetSucceeded(BuildResult result, string target)
        {
            AssertExpectedTargetOutput(result, target, BuildResultCode.Success);
        }

        /// <summary>
        /// Checks that building the specified target failed.
        /// </summary>
        public static void AssertTargetFailed(BuildResult result, string target)
        {
            AssertExpectedTargetOutput(result, target, BuildResultCode.Failure);
        }

        /// <summary>
        /// Checks that building the specified target produced the expected result.
        /// </summary>
        public static void AssertExpectedTargetOutput(BuildResult result, string target, BuildResultCode resultCode)
        {
            DumpTargetResult(result, target);

            TargetResult targetResult;
            if (!result.ResultsByTarget.TryGetValue(target, out targetResult))
            {
                Assert.Inconclusive(@"Could not find result for target ""{0}""", target);
            }
            Assert.AreEqual<BuildResultCode>(resultCode, result.OverallResult, "Unexpected build result");
        }


        /// <summary>
        /// Checks that the specified item group is empty
        /// </summary>
        public static void AssertItemGroupIsEmpty(ProjectInstance project, string itemType)
        {
            Assert.IsTrue(project.GetItems(itemType).Count() == 0, "Item group '{0}' should be empty", itemType);

        }

        /// <summary>
        /// Checks that the specified item group is not empty
        /// </summary>
        public static void AssertItemGroupIsNotEmpty(ProjectInstance project, string itemType)
        {
            Assert.IsTrue(project.GetItems(itemType).Count() > 0, "Item group '{0}' should be not empty", itemType);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Writes the build and target output to the output stream
        /// </summary>
        public static void DumpTargetResult(BuildResult result, string target)
        {
            Console.WriteLine("Overall build result: {0}", result.OverallResult.ToString());

            TargetResult targetResult;
            if (!result.ResultsByTarget.TryGetValue(target, out targetResult))
            {
                Console.WriteLine(@"Could not find result for target ""{0}""", target);
            }
            else
            {
                Console.WriteLine(@"Results for target ""{0}""", target);
                Console.WriteLine("\tTarget exception: {0}", targetResult.Exception == null ? "{null}" : targetResult.Exception.Message);
                Console.WriteLine("\tTarget result: {0}", targetResult.ResultCode.ToString());
            }
        }

        #endregion

    }
}

﻿using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TestUtilities;
using Sonar.Common;

namespace SonarProjectPropertiesGenerator.Tests
{
    [TestClass]
    public class ProjectLoaderTest
    {
        public TestContext TestContext { get; set; }

        #region Tests

        [TestMethod]
        [DeploymentItem("ProjectLoaderTest", "ProjectLoaderTest")]
        public void ProjectLoader()
        {
            // Arrange
            string testSourcePath = Path.Combine(this.TestContext.DeploymentDirectory, "ProjectLoaderTest");
            Assert.IsTrue(Directory.Exists(testSourcePath), "Test error: failed to locate the ProjectLoaderTest folder: {0}", testSourcePath);

            // The test folders that can be set up statically will have been copied to testSourcePath.
            // Now add the test folders that need to be set up dynamically (i.e. those that need a valid full path
            // embedded in the ProjectInfo.xml).
            ProjectDescriptor validTestProject = new ProjectDescriptor()
            {
                ProjectName = "validTestProject",
                ParentDirectoryPath = testSourcePath,
                ProjectFolderName = "validTestProjectDir",
                ProjectFileName = "validTestProject.csproj",
                ProjectGuid = Guid.NewGuid(),
                IsTestProject = true,
                ManagedSourceFiles = new string[] { "TestFile1.cs", "TestFile2.cs" },
            };
            CreateFilesFromDescriptor(validTestProject, "testCompileListFile");

            ProjectDescriptor validNonTestProject = new ProjectDescriptor()
            {
                ProjectName = "validNonTestProject",
                ParentDirectoryPath = testSourcePath,
                ProjectFolderName = "validNonTestProjectDir",
                ProjectFileName = "validNonTestproject.proj",
                ProjectGuid = Guid.NewGuid(),
                IsTestProject = false,
                ManagedSourceFiles = new string[] { "ASourceFile.vb", "AnotherSourceFile.vb" }
            };
            CreateFilesFromDescriptor(validNonTestProject, "list.txt"); // name of the compile list file should not matter

            // Act
            List<Project> projects = SonarProjectPropertiesGenerator.ProjectLoader.LoadFrom(testSourcePath);


            // Assert
            Assert.AreEqual(2, projects.Count);

            Project actualTestProject = AssertProjectResultExists(validTestProject.ProjectName, projects);
            AssertProjectMatchesDescriptor(validTestProject, actualTestProject);

            Project actualNonTestProject = AssertProjectResultExists(validNonTestProject.ProjectName, projects);
            AssertProjectMatchesDescriptor(validNonTestProject, actualNonTestProject);
        }

        [TestMethod]
        [Description("Checks that the loader only looks in the top-level folder for project folders")]
        public void ProjectLoader_NonRecursive()
        {
            // 0. Setup
            string rootTestDir = Path.Combine(this.TestContext.DeploymentDirectory, "ProjectLoader_NonRecursive");
            string childDir = Path.Combine(rootTestDir, "Child1");

            // Create a valid project in the child directory
            ProjectDescriptor validNonTestProject = new ProjectDescriptor()
            {
                ProjectName = "validNonTestProject",
                ParentDirectoryPath = childDir,
                ProjectFolderName = "validNonTestProjectDir",
                ProjectFileName = "validNonTestproject.proj",
                ProjectGuid = Guid.NewGuid(),
                IsTestProject = false,
                ManagedSourceFiles = new string[] { "ASourceFile.vb", "AnotherSourceFile.vb" }
            };
            CreateFilesFromDescriptor(validNonTestProject, "CompileList.txt");

            // 1. Run against the root dir -> not expecting the project to be found
            List<Project> projects = SonarProjectPropertiesGenerator.ProjectLoader.LoadFrom(rootTestDir);
            Assert.AreEqual(0, projects.Count);

            // 2. Run against the child dir -> project should be found
            projects = SonarProjectPropertiesGenerator.ProjectLoader.LoadFrom(childDir);
            Assert.AreEqual(1, projects.Count);
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Creates a folder containing a ProjectInfo.xml and compiled file list as
        /// specified in the supplied descriptor
        /// </summary>
        private static void CreateFilesFromDescriptor(ProjectDescriptor descriptor, string compileListFileName)
        {
            if (!Directory.Exists(descriptor.FullDirectoryPath))
            {
                Directory.CreateDirectory(descriptor.FullDirectoryPath);
            }

            ProjectInfo projectInfo = descriptor.CreateProjectInfo();

            // Create the compile list if any input files have been specified
            if (descriptor.ManagedSourceFiles != null)
            {
                string fullCompileListName = Path.Combine(descriptor.FullDirectoryPath, compileListFileName);
                File.WriteAllLines(fullCompileListName, descriptor.ManagedSourceFiles);

                // Add the compile list as an analysis result
                projectInfo.AnalysisResults.Add(new AnalysisResult() { Id = AnalysisType.ManagedCompilerInputs.ToString(), Location = fullCompileListName });
            }

            // Save a project info file in the target directory
            projectInfo.Save(Path.Combine(descriptor.FullDirectoryPath, FileConstants.ProjectInfoFileName));
        }

        #endregion

        #region Assertions

        private static Project AssertProjectResultExists(string expectedProjectName, List<Project> actualProjects)
        {
            Project actual = actualProjects.FirstOrDefault(p => expectedProjectName.Equals(p.Name));
            Assert.IsNotNull(actual, "Failed to find project with the expected name: {0}", expectedProjectName);
            return actual;
        }

        private static void AssertProjectMatchesDescriptor(ProjectDescriptor expected, Project actual)
        {
            Assert.AreEqual(expected.ProjectName, actual.Name);
            Assert.AreEqual(expected.ProjectGuid, actual.Guid);
            Assert.AreEqual(expected.FullFilePath, actual.MsBuildProject);
            Assert.AreEqual(expected.IsTestProject, actual.IsTest);

            if (expected.ManagedSourceFiles == null)
            {
                Assert.AreEqual(0, actual.Files.Count);
            }
            else
            {
                Assert.AreEqual(expected.ManagedSourceFiles.Length, actual.Files.Count);
                CollectionAssert.AreEqual(expected.ManagedSourceFiles, actual.Files);
            }
        }

        #endregion
    }
}

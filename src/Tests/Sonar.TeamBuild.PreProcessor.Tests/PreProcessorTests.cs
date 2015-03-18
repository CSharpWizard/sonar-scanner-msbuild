﻿//-----------------------------------------------------------------------
// <copyright file="PreProcessorTests.cs" company="SonarSource SA and Microsoft Corporation">
//   (c) SonarSource SA and Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sonar.Common;
using Sonar.TeamBuild.Integration;
using System.IO;
using System.Text;
using TestUtilities;

namespace Sonar.TeamBuild.PreProcessor.Tests
{
    [TestClass]
    public class PreProcessorTests
    {
        public TestContext TestContext { get; set; }

        #region Tests

        [TestMethod]
        public void PreProc_EmptySonarRunnerProperties()
        {
            // Checks the pre-processor creates a valid config file

            // Arrange
            string testDir = TestUtils.CreateTestSpecificFolder(this.TestContext);

            string propertiesFile = CreateEmptyPropertiesFile(testDir);

            MockPropertiesFetcher mockPropertiesFetcher = new MockPropertiesFetcher();
            MockRulesetGenerator mockRulesetGenerator = new MockRulesetGenerator();
            TestLogger logger = new TestLogger();

            string expectedConfigFileName;

            using (PreprocessTestUtils.CreateValidScope("tfs uri", "build uri", testDir))
            {
                TeamBuildSettings settings = TeamBuildSettings.GetSettingsFromEnvironment(new ConsoleLogger());
                Assert.IsNotNull(settings, "Test setup error: TFS environment variables have not been set correctly");
                expectedConfigFileName = settings.AnalysisConfigFilePath;

                TeamBuildPreProcessor preProcessor = new TeamBuildPreProcessor(logger, mockPropertiesFetcher, mockRulesetGenerator);

                // Act
                preProcessor.Execute(logger, "key", "name", "ver", propertiesFile);
            }

            // Assert
            Assert.IsTrue(File.Exists(expectedConfigFileName), "Config file does not exist: {0}", expectedConfigFileName);
            AnalysisConfig config = AnalysisConfig.Load(expectedConfigFileName);
            Assert.IsTrue(Directory.Exists(config.SonarOutputDir), "Output directory was not created: {0}", config.SonarOutputDir);
            Assert.IsTrue(Directory.Exists(config.SonarConfigDir), "Config directory was not created: {0}", config.SonarConfigDir);
            Assert.AreEqual("key", config.SonarProjectKey);
            Assert.AreEqual("name", config.SonarProjectName);
            Assert.AreEqual("ver", config.SonarProjectVersion);
            Assert.AreEqual("build uri", config.GetBuildUri());
            Assert.AreEqual("tfs uri", config.GetTfsUri());
            Assert.AreEqual(propertiesFile, config.SonarRunnerPropertiesPath);

            mockPropertiesFetcher.AssertFetchPropertiesCalled();
            mockPropertiesFetcher.CheckFetcherArguments("key", "http://localhost:9000", null, null);

            mockRulesetGenerator.AssertGenerateCalled();
            mockRulesetGenerator.CheckGeneratorArguments("key", "http://localhost:9000", null, null);
        }

        [TestMethod]
        public void PreProc_NonEmptySonarRunnerProperties()
        {
            // Checks the ruleset generator is called with the expected arguments
            // Arrange
            string testDir = TestUtils.CreateTestSpecificFolder(this.TestContext);

            string propertiesFile = CreatePropertiesFile(testDir, "my url", "my user name", "my password");

            MockPropertiesFetcher mockPropertiesFetcher = new MockPropertiesFetcher();
            MockRulesetGenerator mockRulesetGenerator = new MockRulesetGenerator();
            TestLogger logger = new TestLogger();

            string expectedConfigFileName;

            using (PreprocessTestUtils.CreateValidScope("tfs uri", "build uri", testDir))
            {
                TeamBuildSettings settings = TeamBuildSettings.GetSettingsFromEnvironment(new ConsoleLogger());
                Assert.IsNotNull(settings, "Test setup error: TFS environment variables have not been set correctly");
                expectedConfigFileName = settings.AnalysisConfigFilePath;

                TeamBuildPreProcessor preProcessor = new TeamBuildPreProcessor(logger, mockPropertiesFetcher, mockRulesetGenerator);

                // Act
                preProcessor.Execute(logger, "key", "name", "ver", propertiesFile);
            }

            // Assert
            mockPropertiesFetcher.AssertFetchPropertiesCalled();
            mockPropertiesFetcher.CheckFetcherArguments("key", "my url", "my user name", "my password");

            mockRulesetGenerator.AssertGenerateCalled();
            mockRulesetGenerator.CheckGeneratorArguments("key", "my url", "my user name", "my password");
            logger.AssertErrorsLogged(0);
            logger.AssertWarningsLogged(0);
        }

        #endregion

        #region Private methods

        private static string CreateEmptyPropertiesFile(string outputDirectory)
        {
            return CreatePropertiesFile(outputDirectory, null, null, null);
        }

        private static string CreatePropertiesFile(string outputDirectory, string url, string userName, string password)
        {
            string propertiesFile = Path.Combine(outputDirectory, "propertiesFile.txt");
            Assert.IsFalse(File.Exists(propertiesFile), "Test setup error: the properties file already exists. File: {0}", propertiesFile);

            StringBuilder sb = new StringBuilder();
            if (url != null)
            {
                sb.AppendFormat("sonar.host.url={0}", url);
                sb.AppendLine();
            }
            if (userName != null)
            {
                sb.AppendFormat("sonar.login={0}", userName);
                sb.AppendLine();
            }
            if (password != null)
            {
                sb.AppendFormat("sonar.password={0}", password);
                sb.AppendLine();
            }
            
            File.WriteAllText(propertiesFile, sb.ToString());

            return propertiesFile;
        }

        #endregion
    }
}

﻿//-----------------------------------------------------------------------
// <copyright file="SummaryReportBuilder.cs" company="SonarSource SA and Microsoft Corporation">
//   Copyright (c) SonarSource SA and Microsoft Corporation.  All rights reserved.
//   Licensed under the MIT License. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

using SonarQube.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SonarRunner.Shim
{
    /// <summary>
    /// Outputs a summary report of the post-processing activities.
    /// This is not used by SonarQube: it is only for debugging purposes.
    /// </summary>
    internal class SummaryReportBuilder
    {
        private const string ReportFileName = "AnalysisSummary.log";

        private AnalysisConfig config;
        private AnalysisRunResult analysisResult;
        private ILogger logger;

        private StringBuilder sb;

        #region Public methods

        public static void WriteSummaryReport(AnalysisConfig config, AnalysisRunResult result, ILogger logger)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }
            if (result == null)
            {
                throw new ArgumentNullException("result");
            }
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            SummaryReportBuilder builder = new SummaryReportBuilder(config, result, logger);
            builder.Generate();
        }

        #endregion

        #region Private methods

        private SummaryReportBuilder(AnalysisConfig config, AnalysisRunResult result, ILogger logger)
        {
            this.config = config;
            this.analysisResult = result;
            this.logger = logger;
            this.sb = new StringBuilder();
        }

        private void Generate()
        {
            IEnumerable<ProjectInfo> validProjects = GetProjectsByStatus(ProcessingStatus.Valid);

            WriteTitle(Resources.REPORT_ProductProjectsTitle);
            WriteFileList(validProjects.Where(p => p.ProjectType == ProjectType.Product));
            WriteGroupSpacer();

            WriteTitle(Resources.REPORT_TestProjectsTitle);
            WriteFileList(validProjects.Where(p => p.ProjectType == ProjectType.Test));
            WriteGroupSpacer();

            WriteTitle(Resources.REPORT_InvalidProjectsTitle);
            WriteFilesByStatus(ProcessingStatus.DuplicateGuid);
            WriteFilesByStatus(ProcessingStatus.InvalidGuid);
            WriteGroupSpacer();

            WriteTitle(Resources.REPORT_SkippedProjectsTitle);
            WriteFilesByStatus(ProcessingStatus.NoFilesToAnalyze);
            WriteGroupSpacer();

            WriteTitle(Resources.REPORT_ExcludedProjectsTitle);
            WriteFilesByStatus(ProcessingStatus.ExcludeFlagSet);
            WriteGroupSpacer();

            string reportFileName = Path.Combine(config.SonarOutputDir, ReportFileName);
            logger.LogMessage(Resources.DIAG_WritingSummary, reportFileName);
            File.WriteAllText(reportFileName, sb.ToString());
        }

        private IEnumerable<ProjectInfo> GetProjectsByStatus(ProcessingStatus status)
        {
            return this.analysisResult.Projects.Where(p => p.Value == status).Select(p => p.Key);
        }

        private void WriteTitle(string title)
        {
            this.sb.AppendLine(title);
            this.sb.AppendLine("---------------------------------------");
        }

        private void WriteGroupSpacer()
        {
            this.sb.AppendLine();
            this.sb.AppendLine();
        }

        private void WriteFilesByStatus(params ProcessingStatus[] statuses)
        {
            IEnumerable<ProjectInfo> projects = Enumerable.Empty<ProjectInfo>();

            foreach (ProcessingStatus status in statuses)
            {
                projects = projects.Concat(GetProjectsByStatus(status));
            }

            if (!projects.Any())
            {
                this.sb.AppendLine(Resources.REPORT_NoProjectsOfType);
            }
            else
            {
                WriteFileList(projects);
            }
        }

        private void WriteSeparator()
        {
            this.sb.AppendLine("*************************************");
        }

        private void WriteFileList(IEnumerable<ProjectInfo> projects)
        {
            foreach(ProjectInfo project in projects)
            {
                this.sb.AppendLine(project.FullPath);
            }
        }

        #endregion
    }
}

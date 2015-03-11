﻿//-----------------------------------------------------------------------
// <copyright file="AnalysisContext.cs" company="SonarSource SA and Microsoft Corporation">
//   (c) SonarSource SA and Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using Sonar.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonarTeamBuildPostProcessor
{
    internal class AnalysisContext
    {
        public string TfsUri { get; set; }

        public string BuildUri { get; set; }

        public string SonarConfigDir { get; set; }

        public string SonarOutputDir { get; set; }

        public ILogger Logger { get; set; }

        #region Sonar project properties

        public string SonarProjectKey { get; set; }

        public string SonarProjectVersion { get; set; }

        public string SonarProjectName { get; set; }

        public string SonarRunnerPropertiesPath { get; set; }

        #endregion
    }
}

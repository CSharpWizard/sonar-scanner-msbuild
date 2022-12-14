//-----------------------------------------------------------------------
// <copyright file="PropertiesFetcher.cs" company="SonarSource SA and Microsoft Corporation">
//   Copyright (c) SonarSource SA and Microsoft Corporation.  All rights reserved.
//   Licensed under the MIT License. See License.txt in the project root for license information.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Collections.Generic;

namespace SonarQube.TeamBuild.PreProcessor
{
    public class PropertiesFetcher : IPropertiesFetcher
    {
        #region Public methods

        public IDictionary<string, string> FetchProperties(SonarWebService ws, string sonarProjectKey)
        {
            if (string.IsNullOrWhiteSpace(sonarProjectKey))
            {
                throw new ArgumentNullException("sonarProjectKey");
            }         

            return ws.GetProperties(sonarProjectKey);
        }

        #endregion
    }
}

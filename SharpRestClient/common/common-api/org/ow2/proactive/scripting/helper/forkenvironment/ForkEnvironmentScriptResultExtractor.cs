﻿using System;
using System.Collections.Generic;

/*
 * ProActive Parallel Suite(TM):
 * The Open Source library for parallel and distributed
 * Workflows & Scheduling, Orchestration, Cloud Automation
 * and Big Data Analysis on Enterprise Grids & Clouds.
 *
 * Copyright (c) 2007 - 2017 ActiveEon
 * Contact: contact@activeeon.com
 *
 * This library is free software: you can redistribute it and/or
 * modify it under the terms of the GNU Affero General Public License
 * as published by the Free Software Foundation: version 3 of
 * the License.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 *
 * If needed, contact us to obtain a release under GPL Version 2 or 3
 * or a different license than the AGPL.
 */
namespace org.ow2.proactive.scripting.helper.forkenvironment
{


	[Serializable]
	public class ForkEnvironmentScriptResultExtractor
	{
		public const string PRE_JAVA_CMD_KEY = "preJavaHomeCmd";

		public virtual string[] getJavaPrefixCommand(IDictionary<string, object> bindingsMap)
		{
			// Null checks
			if (bindingsMap == null || bindingsMap[PRE_JAVA_CMD_KEY] == null)
			{
				return new string[0];
			}

			// The java prefix command is split by spaces. That can introduce issues, e.g. with paths
			// which contain spaces. That behavior is address in the user interface (studio), so when
			// this split behavior is changed communicate it in the user interface.
			if (bindingsMap[PRE_JAVA_CMD_KEY] is string)
			{
				return bindingsMap[PRE_JAVA_CMD_KEY].ToString().Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries);
			}
			return new string[0];
		}
	}

}
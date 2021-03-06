﻿using System;

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
namespace org.ow2.proactive.scripting
{

	using ForkEnvironmentScriptResultExtractor = org.ow2.proactive.scripting.helper.forkenvironment.ForkEnvironmentScriptResultExtractor;


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PublicAPI @XmlAccessorType(XmlAccessType.FIELD) public class ForkEnvironmentScript extends Script<ForkEnvironmentScriptResult>
	[Serializable]
	public class ForkEnvironmentScript : Script<ForkEnvironmentScriptResult>
	{

		internal ForkEnvironmentScriptResultExtractor forkEnvironmentScriptResultExtractor = new ForkEnvironmentScriptResultExtractor();

		protected internal override string DefaultScriptName
		{
			get
			{
				return "ForkEnvironmentScript";
			}
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: public ForkEnvironmentScript(Script<?> original) throws InvalidScriptException
//JAVA TO C# CONVERTER TODO TASK: Wildcard generics in constructor parameters are not converted. Move the generic type parameter and constraint to the class header:
		public ForkEnvironmentScript(Script<ForkEnvironmentScriptResult> original) : base(original)
		{
		}

	}

}
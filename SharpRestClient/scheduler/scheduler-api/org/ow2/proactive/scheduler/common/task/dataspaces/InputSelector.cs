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
namespace org.ow2.proactive.scheduler.common.task.dataspaces
{


	using FileSelector = org.objectweb.proactive.extensions.dataspaces.vfs.selector.FileSelector;


	/// <summary>
	/// InputSelector is a couple of <seealso cref="FileSelector"/> and <seealso cref="InputAccessMode"/>
	/// 
	/// @author The ProActive Team
	/// @since ProActive Scheduling 1.1
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PublicAPI @XmlAccessorType(XmlAccessType.FIELD) public class InputSelector implements java.io.Serializable
	[Serializable]
	public class InputSelector
	{

		private FileSelector inputFiles = null;

		private InputAccessMode mode;

		public InputSelector()
		{
		}

		/// <summary>
		/// Create a new instance of InputSelector
		/// </summary>
		/// <param name="inputFiles"> </param>
		/// <param name="mode"> </param>
		public InputSelector(FileSelector inputFiles, InputAccessMode mode)
		{
			this.inputFiles = inputFiles;
			this.mode = mode;
		}

		/// <summary>
		/// Get the inputFiles
		/// </summary>
		/// <returns> the inputFiles </returns>
		public virtual FileSelector InputFiles
		{
			get
			{
				return inputFiles;
			}
			set
			{
				this.inputFiles = value;
			}
		}


		/// <summary>
		/// Get the mode
		/// </summary>
		/// <returns> the mode </returns>
		public virtual InputAccessMode Mode
		{
			get
			{
				return mode;
			}
			set
			{
				this.mode = value;
			}
		}


		/// <summary>
		/// Return a string representation of this selector.
		/// </summary>
		public override string ToString()
		{
			return "(" + this.mode + "-" + this.inputFiles + ")";
		}

	}

}
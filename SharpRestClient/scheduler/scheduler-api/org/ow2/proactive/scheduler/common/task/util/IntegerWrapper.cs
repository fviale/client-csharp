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
namespace org.ow2.proactive.scheduler.common.task.util
{


	/// <summary>
	/// IntegerWrapper is the Scheduler Wrapper object for Integer.
	/// It is mostly used for Hibernate when using Integer with parametric type.
	/// 
	/// @author The ProActive Team
	/// @since ProActive Scheduling 0.9.1
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PublicAPI @XmlAccessorType(XmlAccessType.FIELD) public class IntegerWrapper implements java.io.Serializable
	[Serializable]
	public class IntegerWrapper
	{
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @XmlValue private System.Nullable<int> value;
		private int? value;

		/// <summary>
		/// Create a new instance of IntegerWrapper.
		/// </summary>
		/// <param name="value"> the integer value of this wrapper. </param>
		public IntegerWrapper(int? value)
		{
			this.value = value;
		}

		/// <summary>
		/// Get the Integer value.
		/// </summary>
		/// <returns> the Integer value. </returns>
		public virtual int? IntegerValue
		{
			get
			{
				return value;
			}
		}

		public override int GetHashCode()
		{
			const int prime = 31;
			int result = 1;
			result = prime * result + ((value == null) ? 0 : value.GetHashCode());
			return result;
		}

		public override bool Equals(object obj)
		{
			if (this == obj)
			{
				return true;
			}
			if (obj == null)
			{
				return false;
			}
			if (!(obj is IntegerWrapper))
			{
				return false;
			}
			IntegerWrapper other = (IntegerWrapper) obj;
			if (value == null)
			{
				if (other.value != null)
				{
					return false;
				}
			}
			else if (!value.Equals(other.value))
			{
				return false;
			}
			return true;
		}

	}

}
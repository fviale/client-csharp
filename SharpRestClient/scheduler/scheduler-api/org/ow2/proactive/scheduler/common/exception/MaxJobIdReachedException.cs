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
namespace org.ow2.proactive.scheduler.common.exception
{


	/// <summary>
	/// Exception generated by the scheduler JobId nextId() method informing that max Id has been reached.<br>
	/// In this case, the Scheduler stops, and administrator may check what to do with the Database.<br>
	/// For information, max Id is the Integer.MAX_VALUE.
	/// 
	/// @author The ProActive Team
	/// @since ProActive Scheduling 1.0
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PublicAPI public class MaxJobIdReachedException extends InternalException
	public class MaxJobIdReachedException : InternalException
	{
		/// <summary>
		/// Create a new instance of MaxJobIdReachedException with the given message.
		/// </summary>
		/// <param name="msg"> the message to attach. </param>
		public MaxJobIdReachedException(string msg) : base(msg)
		{
		}

		/// <summary>
		/// Create a new instance of MaxJobIdReachedException.
		/// </summary>
		public MaxJobIdReachedException() : base()
		{
		}

		/// <summary>
		/// Create a new instance of MaxJobIdReachedException with the given message and cause
		/// </summary>
		/// <param name="msg"> the message to attach. </param>
		/// <param name="cause"> the cause of the exception. </param>
		public MaxJobIdReachedException(string msg, Exception cause) : base(msg, cause)
		{
		}

		/// <summary>
		/// Create a new instance of MaxJobIdReachedException with the given cause.
		/// </summary>
		/// <param name="cause"> the cause of the exception. </param>
		public MaxJobIdReachedException(Exception cause) : base(cause)
		{
		}
	}

}
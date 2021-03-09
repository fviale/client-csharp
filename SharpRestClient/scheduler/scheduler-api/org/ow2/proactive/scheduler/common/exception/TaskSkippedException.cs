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
	/// Exception generated when the job skip a task
	/// 
	/// @author The ProActive Team
	/// @since ProActive Scheduling 3.4
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @PublicAPI public class TaskSkippedException extends SchedulerException
	public class TaskSkippedException : SchedulerException
	{

		/// <summary>
		/// Create a new instance of TaskSkippedException
		/// </summary>
		/// <param name="msg"> the message to attach. </param>
		public TaskSkippedException(string msg) : base("The task was skipped in the workflow")
		{
		}

		/// <summary>
		/// Create a new instance of TaskSkippedException
		/// </summary>
		public TaskSkippedException() : base()
		{
		}

		/// <summary>
		/// Create a new instance of TaskSkippedException
		/// </summary>
		/// <param name="msg"> the message to attach. </param>
		/// <param name="cause"> the cause of the exception. </param>
		public TaskSkippedException(string msg, Exception cause) : base(msg, cause)
		{
		}

		/// <summary>
		/// Create a new instance of TaskSkippedException
		/// </summary>
		/// <param name="cause"> the cause of the exception. </param>
		public TaskSkippedException(Exception cause) : base(cause)
		{
		}

	}

}
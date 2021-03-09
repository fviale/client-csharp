﻿/*
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
namespace org.ow2.proactive.scheduler.common
{
	using NotConnectedException = org.ow2.proactive.scheduler.common.exception.NotConnectedException;
	using PermissionException = org.ow2.proactive.scheduler.common.exception.PermissionException;


	public interface SchedulerSpaceInterface
	{

		/// <summary>
		/// Check if a file in the specified DataSpace is a folder </summary>
		/// <param name="dataspace"> the target DataSpace name. It has two possible values, 'USERSPACE' or 'GLOBALSPACE'. </param>
		/// <param name="pathname"> the file path to check </param>
		/// <returns> whether the specified file is a folder </returns>
		/// <exception cref="NotConnectedException"> if you are not authenticated </exception>
		/// <exception cref="PermissionException"> if you can't access to the global data space </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: boolean isFolder(String dataspace, String pathname) throws org.ow2.proactive.scheduler.common.exception.NotConnectedException, org.ow2.proactive.scheduler.common.exception.PermissionException;
		bool isFolder(string dataspace, string pathname);

		/// <summary>
		/// Check if a file exists in the specified location in the DataSpace </summary>
		/// <param name="dataspace"> the target DataSpace name. It has two possible values, 'USERSPACE' or 'GLOBALSPACE'. </param>
		/// <param name="pathname"> the file path to check </param>
		/// <returns> whether the specified file exists in the GLOBAL DataSpace </returns>
		/// <exception cref="NotConnectedException"> if you are not authenticated </exception>
		/// <exception cref="PermissionException"> if you can't access to the global data space </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in C#:
//ORIGINAL LINE: boolean checkFileExists(String dataspace, String pathname) throws org.ow2.proactive.scheduler.common.exception.NotConnectedException, org.ow2.proactive.scheduler.common.exception.PermissionException;
		bool checkFileExists(string dataspace, string pathname);
	}

}
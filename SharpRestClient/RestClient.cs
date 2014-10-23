﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SharpRestClient.Exceptions;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

namespace SharpRestClient
{
    public class SchedulerClient
    {
        private const int REQUEST_TIMEOUT_MS = 600000;
        private const int RETRY_INTERVAL_MS = 1000;

        private readonly RestClient restClient;
        private readonly string username;
        private readonly string password; // to do later use SecureString see http://www.experts-exchange.com/Programming/Languages/.NET/Q_22829139.html        

        private SchedulerClient(RestClient newRestClient, string newUsername, string newPassword)
        {
            this.restClient = newRestClient;
            this.username = newUsername;
            this.password = newPassword;
        }

        public static SchedulerClient Connect(string restUrl, string username, string password)
        {

            RestClient restClient = new RestClient(restUrl);

            RestRequest request = new RestRequest("/scheduler/login", Method.POST);
            request.AddParameter("username", username, ParameterType.GetOrPost);
            request.AddParameter("password", password, ParameterType.GetOrPost);

            IRestResponse response = restClient.Execute(request);

            if (response.ErrorException != null)
            {
                throw new InvalidOperationException("Unable to connect to " + restUrl, response.ErrorException);
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException("Unable to connect to " + restUrl + " descrition: " + response.StatusDescription);
            }

            // if not exception and the response contect size is correct then it's ok
            string sessionid = response.Content;
            Console.WriteLine("---------------------status: " + response.ResponseStatus);
            Console.WriteLine("---------------------status: " + response.StatusCode);
            Console.WriteLine("---------------------received: " + sessionid);
            restClient.Authenticator = new SIDAuthenticator(sessionid);

            return new SchedulerClient(restClient, username, password);
        }

        public bool IsConnected()
        {
            RestRequest request = new RestRequest("/scheduler/isconnected", Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = restClient.Execute(request);
            string data = response.Content;
            return JsonConvert.DeserializeObject<bool>(data);
        }

        public Version GetVersion()
        {
            RestRequest request = new RestRequest("/scheduler/version", Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = restClient.Execute(request);
            string data = response.Content;
            return JsonConvert.DeserializeObject<Version>(data);
        }

        public SchedulerStatus GetStatus()
        {
            RestRequest request = new RestRequest("/scheduler/status", Method.GET);
            request.AddHeader("Accept", "application/json");

            IRestResponse response = restClient.Execute(request);
            string data = response.Content;
            return JsonConvert.DeserializeObject<SchedulerStatus>(data);
        }

        /// <summary>
        /// throws NotConnectedException, UnknownJobException, PermissionException
        /// </summary>
        public bool PauseJob(JobId jobId)
        {
            RestRequest request = new RestRequest("/scheduler/jobs/{jobid}/pause", Method.PUT);
            request.AddHeader("Accept", "application/json");
            request.AddUrlSegment("jobid", Convert.ToString(jobId.Id));

            IRestResponse response = restClient.Execute(request);
            string data = response.Content;
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                dynamic obj = JObject.Parse(data);
                throw new UnknownJobException((string)obj.errorMessage);
            }
            return JsonConvert.DeserializeObject<bool>(data);
        }

        public bool ResumeJob(JobId jobId)
        {
            RestRequest request = new RestRequest("/scheduler/jobs/{jobid}/resume", Method.PUT);
            request.AddHeader("Accept", "application/json");
            request.AddUrlSegment("jobid", Convert.ToString(jobId.Id));

            IRestResponse response = restClient.Execute(request);
            string data = response.Content;
            return JsonConvert.DeserializeObject<bool>(data);
        }

        public bool KillJob(JobId jobId)
        {
            RestRequest request = new RestRequest("/scheduler/jobs/{jobid}/kill", Method.PUT);
            request.AddHeader("Accept", "application/json");
            request.AddUrlSegment("jobid", Convert.ToString(jobId.Id));

            IRestResponse response = restClient.Execute(request);
            string data = response.Content;
            return JsonConvert.DeserializeObject<bool>(data);
        }

        public bool RemoveJob(JobId jobId)
        {
            RestRequest request = new RestRequest("/scheduler/jobs/{jobid}", Method.DELETE);
            request.AddHeader("Accept", "application/json");
            request.AddUrlSegment("jobid", Convert.ToString(jobId.Id));

            IRestResponse response = restClient.Execute(request);
            string data = response.Content;
            return JsonConvert.DeserializeObject<bool>(data);
        }

        // todo add stop/start/shutdown ...
        public JobId SubmitXml(string filePath)
        {
            RestRequest request = new RestRequest("/scheduler/submit", Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Accept", "application/json");
            request.Timeout = 600000;

            string name = Path.GetFileName(filePath);
            
            using (var xml = new FileStream(filePath, FileMode.Open))
            {
                request.AddFile("file", ReadToEnd(xml), name, "application/xml");
            }
            var response = restClient.Execute(request);

            return JsonConvert.DeserializeObject<JobId>(response.Content);
        }

        public JobState GetJobState(JobId jobId)
        {
            RestRequest request = new RestRequest("/scheduler/jobs/{jobid}", Method.GET);
            request.AddUrlSegment("jobid", Convert.ToString(jobId.Id));
            request.AddHeader("Accept", "application/json");

            IRestResponse response = restClient.Execute(request);
            string data = response.Content;
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                dynamic obj = JObject.Parse(data);
                throw new UnknownJobException((string)obj.errorMessage);
            }
            return JsonConvert.DeserializeObject<JobState>(response.Content);
        }

        // based on GetJobState
        public bool isJobAlive(JobId jobId)
        {
            return this.GetJobState(jobId).JobInfo.IsAlive();
        }

        public JobResult GetJobResult(JobId jobId)
        {
            RestRequest request = new RestRequest("/scheduler/jobs/{jobid}/result", Method.GET);
            request.AddUrlSegment("jobid", Convert.ToString(jobId.Id));
            request.AddHeader("Accept-Encoding", "gzip");
            request.AddHeader("Accept", "application/json");

            IRestResponse response = restClient.Execute(request);
            return JsonConvert.DeserializeObject<JobResult>(response.Content);
        }

        public IDictionary<string, string> GetJobResultValue(JobId jobId)
        {
            RestRequest request = new RestRequest("/scheduler/jobs/{jobid}/result/value", Method.GET);
            request.AddUrlSegment("jobid", Convert.ToString(jobId.Id));
            request.AddHeader("Accept-Encoding", "gzip");
            request.AddHeader("Accept", "application/json");

            IRestResponse response = restClient.Execute(request);
            return JsonConvert.DeserializeObject<IDictionary<string,string>>(response.Content);
        }

        /// <summary>
        /// The job is paused waiting for user to resume it.
        /// NotConnectedException, UnknownJobException, PermissionException, TimeoutException
        /// </summary>
        public JobResult WaitForJobResult(JobId jobId, int timeoutInMs)
        {
            var cts = new CancellationTokenSource(timeoutInMs);
            Task<JobResult> tr = Task.Run(async delegate
                {
                    return await WaitForJobResultAsync(jobId, cts.Token);
                }, cts.Token);
            try
            {
                tr.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException) // occurs in case of timeout
                    {
                        throw new TimeoutException("Timeout waiting for the job " + jobId);
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
            return tr.Result;
        }

        /// <summary>
        /// The job is paused waiting for user to resume it.
        /// NotConnectedException, UnknownJobException, PermissionException, TimeoutException
        /// </summary>
        public IDictionary<string,string> WaitForJobResultValue(JobId jobId, int timeoutInMs)
        {
            var cts = new CancellationTokenSource(timeoutInMs);
            Task<IDictionary<string, string>> tr = Task.Run(async delegate
            {
                return await WaitForJobResultValueAsync(jobId, cts.Token);
            }, cts.Token);
            try
            {
                tr.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                {
                    if (e is TaskCanceledException) // occurs in case of timeout
                    {
                        throw new TimeoutException("Timeout waiting for the job " + jobId);
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
            return tr.Result;
        }

        private async Task<JobResult> WaitForJobResultAsync(JobId jobId, CancellationToken cancelToken)
        {
            JobState state = GetJobState(jobId);
            if (!state.JobInfo.IsAlive())
            {
                return GetJobResult(jobId);
            }
            else
            {
                await Task.Delay(RETRY_INTERVAL_MS, cancelToken);
                return await WaitForJobResultAsync(jobId, cancelToken);
            }
        }

        private async Task<IDictionary<string, string>> WaitForJobResultValueAsync(JobId jobId, CancellationToken cancelToken)
        {
            JobState state = GetJobState(jobId);
            if (!state.JobInfo.IsAlive())
            {
                return GetJobResultValue(jobId);
            }
            else
            {
                await Task.Delay(RETRY_INTERVAL_MS, cancelToken);
                return await WaitForJobResultValueAsync(jobId, cancelToken);
            }
        }

        // example PushFile("GLOBALSPACE", "", "file.txt", "c:\tmp\file.txt")
        public bool PushFile(string spacename, string pathname, string filename, string file)
        {
            return this.PushFile(spacename, pathname, filename, file, SchedulerClient.REQUEST_TIMEOUT_MS);
        }

        public bool PushFile(string spacename, string pathname, string filename, string file, int timeout)
        {
            StringBuilder urlBld = new StringBuilder("/scheduler/dataspace/");
            // spacename: GLOBALSPACE or USERSPACE
            urlBld.Append(spacename).Append("/");
            // path example: /dir1/dir2/..
            urlBld.Append(pathname);

            var request = new RestRequest(urlBld.ToString(), Method.POST);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Accept", "application/json");
            request.Timeout = timeout;
            request.AddParameter("fileName", filename, ParameterType.GetOrPost);
            using (FileStream xml = new FileStream(file, FileMode.Open))
            {
                request.AddFile("fileContent", ReadToEnd(xml), filename, "application/octet-stream");
            }

            var response = restClient.Execute(request);
            //Console.WriteLine("-------------> response " + response.Content);
            //Console.WriteLine("---------------------status: " + response.ResponseStatus);
            //Console.WriteLine("---------------------status: " + response.StatusCode);
            return JsonConvert.DeserializeObject<bool>(response.Content);
        }

        // !! DANGEROUS !! - Loads all data in memory before writing to a file
        public bool PullFile(String spacename, String pathname, String outputFile)
        {
            StringBuilder urlBld = new StringBuilder("/scheduler/dataspace/");
            // spacename: GLOBALSPACE or USERSPACE
            urlBld.Append(spacename).Append("/");
            // path example: /dir1/dir2/..
            urlBld.Append(pathname);

            var request = new RestRequest(urlBld.ToString(), Method.GET);
            request.AddHeader("Accept", "application/octet-stream");
            //var response = restClient.Execute(request);
            byte[] data = restClient.DownloadData(request);

            File.WriteAllBytes(outputFile, data);

            return true;
        }

        //method for converting stream to byte[]
        public byte[] ReadToEnd(System.IO.Stream stream)
        {
            long originalPosition = stream.Position;
            stream.Position = 0;

            try
            {
                byte[] readBuffer = new byte[4096];

                int totalBytesRead = 0;
                int bytesRead;

                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
                {
                    totalBytesRead += bytesRead;

                    if (totalBytesRead == readBuffer.Length)
                    {
                        int nextByte = stream.ReadByte();
                        if (nextByte != -1)
                        {
                            byte[] temp = new byte[readBuffer.Length * 2];
                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
                            readBuffer = temp;
                            totalBytesRead++;
                        }
                    }
                }

                byte[] buffer = readBuffer;
                if (readBuffer.Length != totalBytesRead)
                {
                    buffer = new byte[totalBytesRead];
                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
                }
                return buffer;
            }
            finally
            {
                stream.Position = originalPosition;
            }
        }

    }

    sealed class SIDAuthenticator : IAuthenticator
    {
        private readonly string sessionid;

        public SIDAuthenticator(string newSessionid)
        {
            this.sessionid = newSessionid;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            // NetworkCredentials always makes two trips, even if with PreAuthenticate,
            // it is also unsafe for many partial trust scenarios
            // request.Credentials = Credentials;
            // thanks TweetSharp!
            // request.Credentials = new NetworkCredential(_username, _password);
            // only add the Authorization parameter if it hasn't been added by a previous Execute
            if (!request.Parameters.Any(p => p.Name.Equals("sessionid", StringComparison.OrdinalIgnoreCase)))
            {
                //var token = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _username, _password)));
                //var authHeader = string.Format("Basic {0}", token);
                request.AddParameter("sessionid", this.sessionid, ParameterType.HttpHeader);
            }
        }
    }
}

﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpRestClient;
using System.IO;

namespace Tests
{
    [TestClass]
    public class RestClientTests
    {
        public static readonly string LOCAL_REST_SERVER_URL = "http://localhost:8080/rest";

        public static readonly string TRY_REST_SERVER_URL = "https://try.activeeon.com/rest";

        public static SchedulerClient sc;        

        [ClassInitialize]
        public static void BeforeAll(TestContext ctx)
        {
            try
            {
                Console.WriteLine("Trying to connect to " + LOCAL_REST_SERVER_URL);
                sc = SchedulerClient.Connect(LOCAL_REST_SERVER_URL, "admin", "admin");
            }
            catch (Exception)
            {
                Console.WriteLine("No Scheduler running on localhost, trying to connect to " + TRY_REST_SERVER_URL);
                try
                {
                    string p = Environment.GetEnvironmentVariable("TRY_DEMO_PASS", EnvironmentVariableTarget.Machine);
                    sc = SchedulerClient.Connect(TRY_REST_SERVER_URL, "demo", p);
                }
                catch (Exception)
                {
                    Assert.Fail("Unable to run tests! There is no scheduler running on try!");
                }
            }

            // Check Scheduler Verison is 6.X
            SharpRestClient.Version ver = sc.GetVersion();
            Assert.IsNotNull(ver.Scheduler);
            Assert.IsNotNull(ver.Rest);            
            if (!ver.Scheduler.StartsWith("6"))
            {
                Assert.Fail("The Scheduler version is not 6.X");
            }
            if (!ver.Rest.StartsWith("6"))
            {
                Assert.Fail("The Rest version is not 6.X");
            }
        }

        [ClassCleanup]
        public static void AfterAll()
        {
            //Console.WriteLine("----------------ClassCleanup---------------");
        }

        [TestMethod]
        public void IsConnected()
        {
            Assert.AreEqual<bool>(true, sc.IsConnected());
        }

        [TestMethod]
        public void GetSchedulerStatus()
        {            
            Assert.AreEqual<SchedulerStatus>(SchedulerStatus.STARTED, sc.GetStatus());
        }

        [TestMethod]
        public void PushPullFile()
        {
            // Create temp file in temp dir
            string tempFilePath = Path.GetTempFileName();
            FileInfo fileInfo = new FileInfo(tempFilePath);
            try
            {
                bool ok = sc.PushFile("GLOBALSPACE", "", fileInfo.Name, tempFilePath);
                Assert.IsTrue(ok);
            }
            finally
            {
                fileInfo.Delete();
            }
        }

        [TestMethod]
        public void SubmitXml()
        {
            JobId jid = sc.SubmitXml(@"C:\tmp\ProActiveWorkflowsScheduling-windows-x64-6.0.1\samples\workflows\01_simple_task.xml");
            Assert.AreNotEqual<long>(0, jid.Id);
            Assert.AreEqual<string>("01_simple_task", jid.ReadableName);
            Assert.AreEqual<bool>(true, sc.isJobAlive(jid));
            //JobState jobState = sc.GetJobState(jid);            
            System.Threading.Thread.Sleep(5000);
            Assert.AreEqual<bool>(false, sc.PauseJob(jid));
            Assert.AreEqual<bool>(false, sc.ResumeJob(jid));
            Assert.AreEqual<bool>(true, sc.KillJob(jid));
            Assert.AreEqual<bool>(true, sc.RemoveJob(jid));
        }
    }
}

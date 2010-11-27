using ZencoderWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Newtonsoft.Json;
using System.IO;

namespace ZencoderWrapper.Tests
{
    
    
    /// <summary>
    ///This is a test class for OutputSettingsTest and is intended
    ///to contain all OutputSettingsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OutputSettingsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for OutputSettings Constructor
        ///</summary>
        [TestMethod()]
        public void OutputSettingsConstructorTest()
        {
            OutputSetting target = new OutputSetting();
            target.NotificationSettings.Add(new NotificationSetting("json", "http://cdeutsch.com"));
            target.NotificationSettings.Add(new NotificationSetting(NotificationType.Email, "cd@cdeutsch.com"));
            target.NotificationSettings.Add(new NotificationSetting(NotificationType.Url, "http://blog.cdeutsch.com"));
            target.thumbnails.number = 2;
            target.thumbnails.public_s3 = true;
            //RestSharp.Serializers.ISerializer serializer = new RestSharp.Serializers.JsonSerializer();
            string serializedOutput = Serialize(target);

            System.Diagnostics.Debug.Write(serializedOutput);

            
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        public string Serialize(object obj)
        {
            var serializer = new Newtonsoft.Json.JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include
            };

            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    serializer.Serialize(jsonTextWriter, obj);

                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }
    }
}

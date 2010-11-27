using ZencoderWrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ZencoderWrapper.Tests
{
    
    
    /// <summary>
    ///This is a test class for ZencoderClientTest and is intended
    ///to contain all ZencoderClientTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ZencoderClientTest
    {
        public string API_KEY = "YOUR ZENCODER API KEY";
        public string InputSample = "http://mirror.cessen.com/blender.org/peach/trailer/trailer_1080p.mov";
        public string OutputBaseUrl = "ftp://cdeutsch.com/";
        public string OutputFilename = "output.mp4";
        public string WatermarkImageUrl = "http://qwikcast.tv/images/verytoparrow.gif";

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
        ///A test for SubmitJob
        ///</summary>
        [TestMethod()]
        public void SubmitJobTest()
        {
            ZencoderClient client = new ZencoderClient(API_KEY, true);
            JobRequest jobRequest = new JobRequest(InputSample, new OutputSetting(OutputBaseUrl, OutputFilename));

            JobResponse jobResponse = client.SubmitJob(jobRequest);
            //check progress.
            JobOutputProgressResponse progress = client.GetJobOutputProgress(jobResponse.outputs[0].id);
            //list jobs.
            List<JobListingResponse> jobList = client.ListJobs();
            JobListingResponse job = client.GetJob(jobResponse.id);

            //cleanup.
            client.CancelJob(jobResponse.id);
            client.DeleteJob(jobResponse.id);

            Assert.AreEqual("queued", progress.state, "Progress does not equal 'queued'.");
        }

        /// <summary>
        ///A test for SubmitJob
        ///</summary>
        [TestMethod()]
        public void Submit_OneLiner_JobTest()
        {           
            //encode video with one line of code:
            JobResponse jobResponse = new ZencoderClient(API_KEY, true).SubmitJob(InputSample, OutputBaseUrl, OutputFilename);

            //check progress.
            ZencoderClient client = new ZencoderClient(API_KEY, true);
            JobOutputProgressResponse progress = client.GetJobOutputProgress(jobResponse.outputs[0].id);

            //cleanup.
            client.CancelJob(jobResponse.id);
            client.DeleteJob(jobResponse.id);

            Assert.AreEqual("queued", progress.state, "Progress does not equal 'queued'.");
        }

        /// <summary>
        ///A test for SubmitJob
        ///</summary>
        [TestMethod()]
        public void Submit_Advanced_JobTest()
        {
            ZencoderClient client = new ZencoderClient(API_KEY, true);
            JobRequest jobRequest = new JobRequest(InputSample, new OutputSetting(OutputBaseUrl, OutputFilename));
            jobRequest.download_connections = 3;
            jobRequest.region = RegionSetting.UnitedStates;
            OutputSetting output2 = new OutputSetting(OutputBaseUrl, "output.ogg");
            output2.aspect_mode = VideoAspectModeSetting.Letterbox;
            output2.audio_bitrate = 96;
            output2.audio_channels = 1;
            output2.audio_codec = AudioCodecSetting.Vorbis;
            output2.audio_quality = QualitySetting.MediumHi;
            output2.audio_sample_rate = 44100;
            output2.autolevel = true;
            output2.bitrate_cap = 500;
            output2.buffer_size = 300;
            output2.clip_length = "6.5";
            output2.deblock = true;
            output2.deinterlace = VideoDeinterlaceSetting.Detect;
            output2.denoise = VideoDenoiseSetting.Strong;
            output2.frame_rate = 24;
            output2.height = 220;
            output2.keyframe_interval = 100;
            output2.label = "vorbis";
            output2.max_frame_rate = 30;
            output2.NotificationSettings = new List<NotificationSetting>();
            output2.NotificationSettings.Add(new NotificationSetting(NotificationType.Email, "cd@cdeutsch.com"));
            output2.NotificationSettings.Add(new NotificationSetting(new NotificationAdvancedSettings("json", "http://cdeutsch.com")));
            output2.quality = QualitySetting.High;
            output2.skip_audio = false;
            output2.skip_video = false;
            output2.speed = VideoSpeedSetting.Slow_BetterCompression;
            output2.start_clip = "0:00:02";
            output2.thumbnails = new ThumbnailSetting();
            output2.thumbnails.base_url = OutputBaseUrl;
            output2.thumbnails.format = ThumbnailFormatSetting.PNG;
            output2.thumbnails.interval = 5;
            output2.thumbnails.number = 3;
            output2.thumbnails.prefix = "thumb_";
            output2.thumbnails.size = "120x80";
            output2.thumbnails.start_at_first_frame = true;
            output2.thumbnails.public_s3 = false;
            output2.upscale = true;
            output2.video_bitrate = 400;
            output2.video_codec = VideoCodecSetting.Theora;
            output2.watermark = new WatermarkSetting();
            output2.watermark.url = WatermarkImageUrl;
            output2.watermark.width = "10";
            output2.watermark.height = "10";
            output2.watermark.x = "20";
            output2.watermark.y = "-10%";
            output2.width = 400;

            jobRequest.outputs.Add(output2);

            JobResponse jobResponse = client.SubmitJob(jobRequest);
            //check progress.
            JobOutputProgressResponse progress = client.GetJobOutputProgress(jobResponse.outputs[0].id);

            //get job and run tests.
            JobListingResponse job = client.GetJob(jobResponse.id);
            //tests
            Assert.AreEqual(jobRequest.input, job.job.input_media_file.url, true, "Input does not match");
            //you can testing test these if the job has reached a certain status.
            if (job.job.state == "failed" || job.job.state == "finished")
            {
                Assert.AreEqual(jobRequest.outputs[1].audio_bitrate, job.job.output_media_files[1].audio_bitrate_in_kbps, "Audio bitrate does not match");
                Assert.AreEqual(jobRequest.outputs[1].audio_codec, job.job.output_media_files[1].audio_codec, true, "Audio codec does not match");
                Assert.AreEqual(jobRequest.outputs[1].audio_sample_rate, job.job.output_media_files[1].audio_sample_rate, "Audio sample rate does not match");
                Assert.AreEqual(jobRequest.outputs[1].audio_channels.Value.ToString(), job.job.output_media_files[1].channels, "Audio channels does not match");
                Assert.AreEqual(jobRequest.outputs[1].frame_rate, job.job.output_media_files[1].frame_rate, "Video frame rate does not match");
                Assert.AreEqual(jobRequest.outputs[1].height, job.job.output_media_files[1].height, "Video height does not match");
                Assert.AreEqual(jobRequest.outputs[1].label, job.job.output_media_files[1].label, true, "Video label does not match");
                Assert.AreEqual(jobRequest.outputs[1].video_codec, job.job.output_media_files[1].video_codec, true, "Video codec does not match");
                Assert.AreEqual(jobRequest.outputs[1].width, job.job.output_media_files[1].width, "Video width does not match");
            }
            //cleanup.
            client.CancelJob(jobResponse.id);
            client.DeleteJob(jobResponse.id);            
        }

        /// <summary>
        ///A test for GetJobOutputProgress
        ///</summary>
        [TestMethod()]
        public void GetJobOutputProgressTest()
        {
            ZencoderClient client = new ZencoderClient(API_KEY, true); 
            int OutputID = 1;
            JobOutputProgressResponse response = client.GetJobOutputProgress(OutputID);
            
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAccount
        ///</summary>
        [TestMethod()]
        public void GetAccountTest()
        {
            ZencoderClient client = new ZencoderClient(API_KEY, true);
            AccountResponse account = client.GetAccount();
            Assert.AreEqual("active", account.account_state, "Account state is not 'active'.");

            //test changing integration mode if this account has a payment plan.
            if (account.plan.ToLower() != "test")
            {
                //update integration mode to off.
                client.UpdateIntegrationMode(false);
                account = client.GetAccount();
                //test                 
                Assert.IsFalse(account.integration_mode);

                //update integration mode to on.
                client.UpdateIntegrationMode(true);
                account = client.GetAccount();
                //test
                Assert.IsTrue(account.integration_mode);
            }
        }

        /// <summary>
        ///A test for CreateAccount
        ///</summary>
        [TestMethod()]
        public void CreateAccountTest()
        {
            //commented out so lots of bogus accounts aren't created.
            //ZencoderClient client = new ZencoderClient();
            //string password = "Test123";
            //string email = "test999@test.com";
            //CreateAccountRequest request = new CreateAccountRequest(email, password);
            //request.newsletter = false;
            ////CreateAccountResponse account = client.CreateAccount("test@test.com", password);
            //CreateAccountResponse account = client.CreateAccount(request);

            //Assert.AreEqual(password, account.password, "Account password does not match.");
        }
    }
}

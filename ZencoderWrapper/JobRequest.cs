using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ZencoderWrapper
{
    public class JobRequest
    {
        /// <summary>
        /// The URL of your input file: http, https, s3, ftp, ftps, or sftp.
        /// </summary>
        public string input { get; set; }

        /// <summary>
        /// The region where your file will be processed.
        /// </summary>
        public string region { get; set; }

        /// <summary>
        /// Utilize multiple, simultaneous connections to speed downloads or reduce them to throttle.
        /// </summary>
        public int? download_connections { get; set; }
        
        public List<OutputSetting> outputs { get; set; }

        /// <summary>
        /// Your Zencoder Api Key.
        /// </summary>
        /// <remarks>This setting will be automatically set by the ZencoderClient.</remarks>
        public string api_key = null;

        /// <summary>
        /// Indicates if this is a test job.
        /// </summary>
        /// <remarks>This setting will be automatically set by the ZencoderClient.</remarks>
        public bool? test = true;

        public JobRequest()
        {

        }

        public JobRequest(string input)
            :this()
        {
            this.input = input;
        }

        public JobRequest(string input, OutputSetting output)
            :this(input)
        {
            if (output != null)
            {
                this.outputs = new List<OutputSetting>();
                this.outputs.Add(output);
            }
        }

        public JobRequest(string input, string output_base_url, string output_filename)
            :this(input)
        {
            this.outputs = new List<OutputSetting>();
            this.outputs.Add(new OutputSetting(output_base_url, output_filename));
        }
    }    
}

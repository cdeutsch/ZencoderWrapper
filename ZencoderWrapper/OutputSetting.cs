using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ZencoderWrapper
{
    [JsonObject(MemberSerialization.OptOut)]
    public class OutputSetting
    {
        public string base_url { get; set; }
        public string filename { get; set; }
        public string label { get; set; }
        public string video_codec { get; set; }
        public int? quality { get; set; }
        public int? speed { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public string aspect_mode { get; set; }
        public bool? upscale { get; set; }
        public string audio_codec { get; set; }
        public int? audio_quality { get; set; }
        public ThumbnailSetting thumbnails { get; set; }
        public S3HeadersSetting s3_headers { get; set; }
        public string deinterlace { get; set; }
        public decimal? max_frame_rate { get; set; }
        public decimal? frame_rate { get; set; }
        public int? keyframe_interval { get; set; }
        public int? video_bitrate { get; set; }
        public int? bitrate_cap { get; set; }
        public int? buffer_size { get; set; }
        public bool? skip_video { get; set; }
        public int? audio_bitrate { get; set; }
        public int? audio_channels { get; set; }
        public int? audio_sample_rate { get; set; }
        public bool? skip_audio { get; set; }
        public bool? autolevel { get; set; }
        public bool? deblock { get; set; }
        public string denoise { get; set; }
        public string start_clip { get; set; }
        public string clip_length { get; set; }
        public WatermarkSetting watermark { get; set; }

        [JsonProperty(PropertyName = "public")]
        public bool? public_s3 { get; set; }

        public List<AccessControlSetting> access_control { get; set; }

        public object[] notifications
        {
            get
            {
                //manually build notifications.
                if (NotificationSettings != null)
                {
                    List<Object> rslt = new List<object>();
                    foreach (NotificationSetting setting in NotificationSettings)
                    {
                        rslt.Add(setting.data);
                    }
                    return rslt.ToArray();
                }
                else
                {
                    return null;
                }
            }
        }

        [JsonIgnore]
        public List<NotificationSetting> NotificationSettings { get; set; }

        public OutputSetting()
        {
            //NotificationSettings = new List<NotificationSetting>();
            //thumbnails = new ThumbnailSetting();
        }

        public OutputSetting(string base_url, string filename)
            : this()
        {
            this.base_url = base_url;
            this.filename = filename;
        }

        public OutputSetting(string base_url, string filename, string label, string video_codec, 
            int? quality, int? speed, int? width, int? height, string aspect_mode, 
            string audio_codec, int? audio_quality)
            : this(base_url, filename)
        {
            this.label = label;
            this.video_codec = video_codec;
            this.quality = quality;
            this.speed = speed;
            this.width = width;
            this.height = height;
            this.aspect_mode = aspect_mode;
            this.audio_codec = audio_codec;
            this.audio_quality = audio_quality;
        }

        public OutputSetting(string base_url, string filename, string label, string video_codec,
            int? quality, int? speed, int? width, int? height, string aspect_mode,
            string audio_codec, int? audio_quality, ThumbnailSetting thumbnails)
            : this(base_url, filename, label, video_codec, quality, speed, width, height, aspect_mode, audio_codec, audio_quality)
        {
            this.thumbnails = thumbnails;
        }
    }


}

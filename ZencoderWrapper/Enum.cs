using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZencoderWrapper
{
    public struct RegionSetting
    {
        public const string Asia = "asia";
        public const string Europe = "europe";
        public const string UnitedStates = "us";
    }

    public struct QualitySetting
    {
        public const int Poor = 1;
        public const int MediumLo = 2;
        public const int Medium = 3;
        public const int MediumHi = 4;
        public const int High = 5;
    }

    public struct VideoCodecSetting
    {
        public const string H264 = "h264";
        public const string VP8 = "vp8";
        public const string Theora = "theora";
        public const string VP6 = "vp6";
        public const string MPEG4 = "mpeg4";        
    }
    
    public struct VideoSpeedSetting
    {
        public const int Slow_BetterCompression = 1;
        public const int MediumLo = 2;
        public const int Medium = 3;
        public const int MediumHi = 4;
        public const int Fast_WorseCompression = 5;
    }

    public struct VideoAspectModeSetting
    {
        public const string PreserveAspectRatio = "preserve";
        public const string CropToFitOutputAspectRatio = "crop";
        public const string Letterbox = "pad";
        public const string Distort = "stretch";        
    }

    public struct VideoDenoiseSetting
    {
        public const string Weak = "weak";
        public const string Medium = "medium";
        public const string Strong = "strong";
        public const string Strongest = "strongest";
    }

    public struct VideoDeinterlaceSetting
    {
        public const string Detect = "detect";
        public const string On = "on";
        public const string Off = "off";
    }

    public struct AudioCodecSetting
    {
        public const string AAC = "aac";
        public const string MP3 = "mp3";
        public const string Vorbis = "vorbis";
    }

    

    public struct ThumbnailFormatSetting
    {
        public const string PNG = "png";
        public const string JPG = "jpg";
    }

    public enum NotificationType
    {
        Url,
        Email,
        Advanced
    }

}

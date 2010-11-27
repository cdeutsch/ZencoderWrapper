using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ZencoderWrapper
{
    public class NotificationSetting
    {
        public NotificationType type { get; set; }
        public object data { get; set; }

        public NotificationSetting()
        {

        }
        public NotificationSetting(NotificationType type)
        {
            this.type = type;
        }
        public NotificationSetting(NotificationType type, string value)
            : this(type)
        {
            switch (type)
            {
                case NotificationType.Url:
                case NotificationType.Email:
                    data = value;
                    break;

                case NotificationType.Advanced:
                    throw new ArgumentException("For Advanced NotificationTypes you must use a different overload.");
            }
        }
        public NotificationSetting(NotificationAdvancedSettings settings)
        {
            type = NotificationType.Advanced;
            data = settings;
        }
        public NotificationSetting(string format, string url)
            : this(new NotificationAdvancedSettings(format, url))
        {
        }

        public override string ToString()
        {
            return data.ToString();
        }
    }

    public class NotificationAdvancedSettings
    {
        public string format { get; set; }
        public string url { get; set; }

        public NotificationAdvancedSettings()
        {

        }
        public NotificationAdvancedSettings(string format, string url)
        {
            this.format = format;
            this.url = url;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

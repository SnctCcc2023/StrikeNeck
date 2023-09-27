using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strikeneck.AppSetting
{
    public class NotificationInterval
    {
        public readonly NotificationIntervalList notificationInterval;

        public NotificationInterval(NotificationIntervalList notificationInterval = NotificationIntervalList.fifteenMinutes)
        {
            this.notificationInterval = notificationInterval;
        }
    }

    public enum NotificationIntervalList
    {
        oneMinute = 1,
        fifteenMinutes = 15,
        thirtyMinutes = 30,
        oneHour = 60
    };
}

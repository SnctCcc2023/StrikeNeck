using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace strikeneck.AppSetting
{
    public class SettingAccessor
    {
        public static void Save(Setting setting)
        {
            SecureStorage.Default.SetAsync("isNotificationEnabled", setting.isNotificationEnabled ? "true" : "false" );
            SecureStorage.Default.SetAsync("notificationInterval", setting.detectionSensitivity.sensitivity.ToString());
            SecureStorage.Default.SetAsync("detectionSensitivity", setting.notificationInterval.notificationInterval.ToString());
        }

        public static Setting Load()
        {
            var isNotificationEnabled = LoadNotificationEnabled();
            var notificationInterval = LoadNotificationInterval();
            var detectionSensitivity = detectionSensiticity();

            return new Setting(notificationInterval, detectionSensitivity, isNotificationEnabled);
        }

        private static bool LoadNotificationEnabled()
        {
            var isNotificationEnabled = SecureStorage.Default.GetAsync("isNotificationEnabled").Result;
            return (isNotificationEnabled == "true");
        }

        private static NotificationInterval LoadNotificationInterval()
        {
            var notificationInterval = SecureStorage.Default.GetAsync("notificationInterval").Result;
            return new NotificationInterval((NotificationIntervalList)Enum.Parse(typeof(NotificationIntervalList), notificationInterval));
        }

        private static DetectionSensitivity detectionSensiticity()
        {
            var detectionSensitivity = SecureStorage.Default.GetAsync("detectionSensitivity").Result;
            return new DetectionSensitivity(int.Parse(detectionSensitivity));
        }
    }
}

namespace strikeneck.AppSetting
{
    public class Setting
    {
        public readonly bool isNotificationEnabled;
        public readonly NotificationInterval notificationInterval;
        public readonly DetectionSensitivity detectionSensitivity;

        public Setting(NotificationInterval notificationInterval, DetectionSensitivity detectionSensitivity, bool isNotificationEnabled)
        {
            this.notificationInterval = notificationInterval;
            this.isNotificationEnabled = isNotificationEnabled;
            this.detectionSensitivity = detectionSensitivity;
        }

        public Setting()
        {
            detectionSensitivity = new DetectionSensitivity();
            isNotificationEnabled = true;
            notificationInterval = new NotificationInterval();
        }
    }
}

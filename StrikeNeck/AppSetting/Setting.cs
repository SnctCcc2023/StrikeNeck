namespace strikeneck.AppSetting
{
    public class Setting
    {
        readonly bool isNotificationEnabled;
        readonly NotificationInterval notificationInterval;
        readonly DetectionSensitivity detectionSensitivity;

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

namespace StrikeNeck.StrikeNeck.AppSetting
{
    public class Setting
    {
        private readonly bool isNotificationEnabled;
        private readonly NotificationInterval notificationInterval;
        private readonly DetectionSensitivity detectionSensitivity;

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

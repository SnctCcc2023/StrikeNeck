
namespace strikeneck
{
	public partial class Settings : ContentPage
	{
        bool isSwitchOn;
        string selectedValue;
		public Settings()
		{
			InitializeComponent();
		}
        private void ToggleSwitch_Toggled(Object sender, ToggledEventArgs e)
        {
            isSwitchOn = e.Value;
        }
        private void NotificationIntervalPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Picker で選択されたアイテムを取得
            selectedValue = notificationIntervalPicker.SelectedItem as string;
            
        }
        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double Value = e.NewValue; // スライダーの新しい値を取得
                                       
        }

        private async void ToStats(Object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//Stats");
		}
        private async void CompleteButton_Clicked(Object sender, EventArgs e)
        {
            Preferences.Default.Set("A",selectedValue);
            Preferences.Default.Set("IsNotification", toggleSwitch.IsToggled);
            await Shell.Current.GoToAsync("//Stats");
        }

        private async void ToInit1(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Init1");
        }
    }
}
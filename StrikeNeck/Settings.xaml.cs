namespace strikeneck
{
	public partial class Settings : ContentPage
	{
		public Settings()
		{
			InitializeComponent();
		}
        private void ToggleSwitch_Toggled(Object sender, ToggledEventArgs e)
        {
            bool isSwitchOn = e.Value;
        }
        private void NotificationIntervalPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Picker �őI�����ꂽ�A�C�e�����擾
            var selectedValue = notificationIntervalPicker.SelectedItem as string;

            
        }
        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            double Value = e.NewValue; // �X���C�_�[�̐V�����l���擾
                                       
        }

        private async void ToStats(Object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//Stats");
		}
        private async void CompleteButton_Clicked(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Stats");
        }

        private async void ToInit1(Object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Init1");
        }
    }
}
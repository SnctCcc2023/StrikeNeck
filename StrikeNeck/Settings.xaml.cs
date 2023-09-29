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
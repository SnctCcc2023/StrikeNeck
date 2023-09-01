namespace strikeneck.Init
{

	public partial class Init3 : ContentPage
	{
		public Init3()
		{
			InitializeComponent();
		}

		private async void ToStats(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//Stats");
        }

		private async void ToInit1(object sender, EventArgs e)
		{
			await Shell.Current.GoToAsync("//Init1");
		}
    }
}
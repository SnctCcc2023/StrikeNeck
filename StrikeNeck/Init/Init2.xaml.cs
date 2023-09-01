namespace strikeneck.Init
{
    public partial class Init2 : ContentPage
    {
        public Init2()
        {
            InitializeComponent();
        }
        private async void ToInit3(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Init3");
        }
        private async void ToInit1(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Init1");
        }
    }
}


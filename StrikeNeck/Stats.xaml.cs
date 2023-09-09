
namespace strikeneck
{
    public partial class Stats : ContentPage
    {
        public Stats()
        {
            InitializeComponent();
        }
        
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Settings");
        }
    }
}
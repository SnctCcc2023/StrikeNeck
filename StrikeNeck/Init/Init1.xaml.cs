using System;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace strikeneck.Init

{
    public partial class Init1 : ContentPage
    {
        public Init1()
        {
            InitializeComponent();
        }

        private async void OnClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Init2");
        }

    }

}
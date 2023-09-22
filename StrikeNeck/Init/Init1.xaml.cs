
using System;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Data;


namespace strikeneck.Init

{
    public partial class Init1 : ContentPage
    {
        private TimeOnly StartTime = TimeOnly.FromDateTime(DateTime.Now);
        public Init1()
        {
            InitializeComponent();
        }

        private async void OnClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Init2");
        }

        private void cameraView_CamerasLoaded(object sender, EventArgs e)
        {
            cameraView.Camera = cameraView.Cameras.First();
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                var result = await cameraView.StartCameraAsync();

            });
        }
        private async void TakePhoto(object sender, EventArgs e)
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                StartTime = StartTime.Add(TimeSpan.FromMilliseconds(-10));

                if (StartTime.Second %10 == 0)
                {
                    StartTime = TimeOnly.FromDateTime(DateTime.Now);
                    myImage.Source = cameraView.GetSnapShot(Camera.MAUI.ImageFormat.PNG);
                }
            }
        }

    }

}
using System;
using System.Threading.Tasks;
using System.Data;


namespace strikeneck.Init

{
    public partial class Init1 : ContentPage
    {
        private TimeOnly StartTime = TimeOnly.FromDateTime(DateTime.Now);
        int takeCount = 0;
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
            await cameraView.StopCameraAsync();
            var result = await cameraView.StartCameraAsync();
            while (true)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                StartTime = TimeOnly.FromDateTime(DateTime.Now);

                if (StartTime.Second %1 == 0)
                {
                    StartTime = TimeOnly.FromDateTime(DateTime.Now);
                    string mainDir = FileSystem.Current.CacheDirectory;
                    string SubDir = "CorrectPic";
                    string DirPath = mainDir + SubDir;
                    if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);
                    string PicName = "judge";
                    PicName += takeCount.ToString();
                    PicName += ".JPEG";
                    string filePath = Path.Combine(DirPath, PicName);
                    await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.JPEG,filePath);
                    takeCount++;
                    await Task.Delay(TimeSpan.FromMilliseconds(50));
                    StartTime = TimeOnly.FromDateTime(DateTime.Now);
                }
                if (takeCount > 40) break;
            }
        }

    }

}
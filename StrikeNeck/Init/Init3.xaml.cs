namespace strikeneck.Init
{

	public partial class Init3 : ContentPage
	{
        private TimeOnly StartTime = TimeOnly.FromDateTime(DateTime.Now);
        bool moving;
        public Init3()
		{
		    InitializeComponent();
            moving = false;
            TakePhoto();
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

        private async void TakePhoto()
        {
            await cameraView.StopCameraAsync();
            var result = await cameraView.StartCameraAsync();
            while (!moving)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                StartTime = TimeOnly.FromDateTime(DateTime.Now);

                if (StartTime.Second % 1 == 0)
                {
                    StartTime = TimeOnly.FromDateTime(DateTime.Now);
                    string mainDir = FileSystem.Current.CacheDirectory;
                    string SubDir = "TestPic";
                    string DirPath = mainDir + SubDir;
                    if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);
                    string PicName = "test.png";
                    string filePath = Path.Combine(DirPath, PicName);
                    await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.JPEG, filePath);
                    await Task.Delay(TimeSpan.FromMilliseconds(50));
                    StartTime = TimeOnly.FromDateTime(DateTime.Now);
                }
                
            }
        }

        private async void ToStats(object sender, EventArgs e)
		{
            moving = true;
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            await Shell.Current.GoToAsync("//Stats");
        }

		private async void ToInit1(object sender, EventArgs e)
		{
            moving = true;
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            await Shell.Current.GoToAsync("//Init1");
		}
        private async void ToInit2(object sender,EventArgs e)
        {
            moving = true;
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            await Shell.Current.GoToAsync("//Init2");
        }
    }
}
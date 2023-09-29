using strikeneck.Imaging;

namespace strikeneck.Init
{
    public partial class Init2 : ContentPage
    {
        int takeCount = 0;
        private TimeOnly StartTime = TimeOnly.FromDateTime(DateTime.Now);
        public Init2()
        {
            InitializeComponent();
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
            string DirPath = "";
            while (true)
            {
                
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                StartTime = TimeOnly.FromDateTime(DateTime.Now);
                if (StartTime.Second % 1 == 0)
                {
                    StartTime = TimeOnly.FromDateTime(DateTime.Now);
                    string mainDir = FileSystem.Current.CacheDirectory;
                    string SubDir = "WrongPic";
                    DirPath = mainDir + SubDir;
                    if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);
                    string PicName = "judge";
                    PicName += takeCount.ToString();
                    PicName += ".JPEG";
                    takeCount++;
                    string filePath = Path.Combine(DirPath, PicName);
                    await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.JPEG, filePath);
                    await Task.Delay(TimeSpan.FromMilliseconds(50));
                    StartTime = TimeOnly.FromDateTime(DateTime.Now);
                }
                if (takeCount > 40) break;
            }
            ForwardLeanDetector fd = new ForwardLeanDetector();
            var detect_path = new DirectoryInfo(DirPath);
            string c_mainDir = FileSystem.Current.CacheDirectory;
            string c_subDir = "CorrectPic";
            string c_DirPath = c_mainDir + c_subDir;
            var c_detect_path = new DirectoryInfo(c_DirPath);
            fd.retrain(c_detect_path,detect_path);
            await Task.Delay(TimeSpan.FromSeconds(2));
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


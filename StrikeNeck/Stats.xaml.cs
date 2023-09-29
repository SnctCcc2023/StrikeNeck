
using Camera.MAUI;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using ZXing;
using strikeneck.Imaging;

namespace strikeneck
{
    public partial class Stats : ContentPage
    {
        private TimeOnly StartTime = TimeOnly.FromDateTime(DateTime.Now);
        bool moving;
        // private TimeOnly StartTime = TimeOnly.FromDateTime(DateTime.Now);
        public Stats()
        {
            InitializeComponent();
            moving = false;
            bool first_boot = Preferences.Default.Get("booted", true);
            if (first_boot)
            {
                Preferences.Default.Set("booted", false);
                ToInit1();
            }
            else
            {
                cameraView_Reloaded();
                TakePhoto();
            }
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
        private void cameraView_Reloaded()
        {
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await cameraView.StopCameraAsync();
                var result = await cameraView.StartCameraAsync();

            });

        }
        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            moving = true;
            await Task.Delay(TimeSpan.FromMilliseconds(100));
            await Shell.Current.GoToAsync("//Settings");
        }
        private async void ToInit1()
        {
            await Task.Delay(1);
            await Shell.Current.GoToAsync("//Init1");
        }
        private async void MakeToast()
        {

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
            };

            string text = "姿勢が悪くなっています！\n" + "もし姿勢が悪くなっていないのにこの通知が出ている場合は以下のボタンから検知感度を調整してください。\n";
            string actionButtonText = "設定画面を開く";
            Action action = async () => await Shell.Current.GoToAsync("//Settings");
            TimeSpan duration = TimeSpan.FromSeconds(3);

            var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);
        }
        public async void TakePhoto()
        {
            await cameraView.StopCameraAsync();
            var result = await cameraView.StartCameraAsync();
            int interval_cnt = 0;
            while (!moving)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                StartTime = TimeOnly.FromDateTime(DateTime.Now);
                if (StartTime.Second % 60 == 0)
                {
                    interval_cnt++;
                    string mainDir = FileSystem.Current.CacheDirectory;
                    string SubDir = "JudgePic";
                    string DirPath = mainDir + SubDir;
                    if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);
                    string filePath = Path.Combine(DirPath, "judge.jpeg");
                    await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.JPEG, filePath);
                    ForwardLeanDetector fd = new ForwardLeanDetector();
                    var detect_path = new FileInfo(filePath);
                    bool IsLeaned = fd.examin(detect_path);
                    bool Notification = Preferences.Default.Get("IsNotification", true);
                    string interval = "1";
                    // interval = Preferences.Default.Get("A", "1");
                    int tmp = int.Parse(interval);
                    if (interval_cnt >=tmp)
                    {
                        interval_cnt = 0;
                        MakeToast();
                    }
                    if (interval_cnt > 10000) interval_cnt = 0;
                    await Task.Delay(TimeSpan.FromMilliseconds(1000));
                    StartTime = TimeOnly.FromDateTime(DateTime.Now);
                }
            }
        }
    }
}

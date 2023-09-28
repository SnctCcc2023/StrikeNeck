
using Camera.MAUI;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using ZXing;

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
        private async void MakeToast(object sender, EventArgs e)
        {

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Colors.Red,
                TextColor = Colors.Green,
                ActionButtonTextColor = Colors.Yellow,
                CornerRadius = new CornerRadius(10),
                Font = Microsoft.Maui.Font.SystemFontOfSize(14),
                ActionButtonFont = Microsoft.Maui.Font.SystemFontOfSize(14),
                CharacterSpacing = 0.5
            };

            string text = "テスト通知";
            string actionButtonText = "設定を開く";
            Action action = async () => await Shell.Current.GoToAsync("//Settings");
            TimeSpan duration = TimeSpan.FromSeconds(3);

            var snackbar = Snackbar.Make(text, action, actionButtonText, duration, snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);
        }
        public async void TakePhoto()
        {
            while (!moving)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(10));
                StartTime = TimeOnly.FromDateTime(DateTime.Now);

                if (StartTime.Second % 60 == 0)
                {
                    string mainDir = FileSystem.Current.CacheDirectory;
                    string SubDir = "JudgePic";
                    string DirPath = mainDir + SubDir;
                    if (!Directory.Exists(DirPath)) Directory.CreateDirectory(DirPath);
                    string filePath = Path.Combine(DirPath, "judge.jpeg");
                    File.Delete(Path.Combine(DirPath, "judge.jpeg"));
                    await cameraView.SaveSnapShot(Camera.MAUI.ImageFormat.JPEG, filePath);
                    await Task.Delay(TimeSpan.FromMilliseconds(1000));
                    StartTime = TimeOnly.FromDateTime(DateTime.Now);
                }
            }
        }
    }
}

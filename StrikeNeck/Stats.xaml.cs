
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;


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
        private void DurationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Picker で選択されたアイテムを取得
            var selectedValue = DurationPicker.SelectedItem as string;


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

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}
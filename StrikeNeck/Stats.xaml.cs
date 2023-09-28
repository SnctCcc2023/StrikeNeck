using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using StrikeNeck.ViewModels;
using Day_Return;
using Hour_Return;
using Month_Return;
namespace strikeneck
{
    public partial class Stats : ContentPage
    {
        private StatsViewModel statsViewModel;
        public StatsViewModel StatsViewModel
        {
            get { return statsViewModel; }
            set
            {
                statsViewModel = value;
                BindingContext = statsViewModel;
            }
        }
        public Stats()
        {
            InitializeComponent();
            Binding binding = new Binding();
            StatsViewModel = new StatsViewModel();
        }


        private void DurationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedIndex = picker.SelectedIndex;
            Label myLabel = this.FindByName<Label>("unit");

            switch (selectedIndex)
            {
                case 0:
                    StatsViewModel.SetStartUpTime(new int[] { 10, 20, 30, 40, 50, 60, 70 });
                    StatsViewModel.SetPoorPostureTime(new int[] { 20, 30, 90, 50, 60, 70, 80 });
                    StatsViewModel.SetAxisLabels(new string[] { "1", "2", "3", "4", "5", "6", "7" });
                    myLabel.Text = "(分)";
                    break;
                case 1:
                    myLabel.Text = "(時間)";
                    StatsViewModel.SetStartUpTime(new int[] { 20, 30, 40, 50, 60, 70, 80 });
                    StatsViewModel.SetPoorPostureTime(new int[] { 30, 40, 50, 60, 70, 10, 90 });
                    StatsViewModel.SetAxisLabels(new string[] { "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun" });
                    break;
                case 2:
                    myLabel.Text = "(時間)";
                    StatsViewModel.SetStartUpTime(new int[] { 30, 40, 50, 60, 70, 80, 90 });
                    StatsViewModel.SetPoorPostureTime(new int[] { 40, 50, 6, 70, 80, 90, 100 });
                    StatsViewModel.SetAxisLabels(new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul" });
                    break;
            }

            StatsViewModel.Series = StatsViewModel.Series;
            StatsViewModel.XAxes = StatsViewModel.XAxes;
            StatsViewModel.UpdateGraph();
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Settings");
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
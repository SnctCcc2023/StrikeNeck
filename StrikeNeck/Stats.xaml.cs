using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using StrikeNeck.ViewModels;
using Day_Return;
using Hour_Return;
using System;
using Month_Return;
namespace strikeneck
{
    

    public class DayOfWeekUtility
    {
        public static int GetCurrentDayOfWeek()
        {
            // 現在の日時を取得
            DateTime now = DateTime.Now;

            // 現在の曜日を取得し、0から6の数字に変換
            int dayOfWeek = (int)now.DayOfWeek;

            // 日曜から始まるように調整
            if (dayOfWeek == 0)
            {
                dayOfWeek = 6;
            }
            else
            {
                dayOfWeek--;
            }

            return dayOfWeek;
        }
    }

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
                    Hour_Returnee val = new Hour_Returnee();
                    val.HourReturnee();
                    int currentDayOfWeek = DayOfWeekUtility.GetCurrentDayOfWeek();
                    
                    List<int>[] keys = new List<int>[24];
                    List<float>[] values = new List<float>[24];
                    for(int i=0;i < 24; i++)
                    {
                        keys[i]=new List<int>() { currentDayOfWeek,i};
                        values[i] = val.HourResult[keys[i]];
                    }
                    
                    StatsViewModel.SetStartUpTime(new float[] { 
                        values[0][1], 
                        values[1][1], 
                        values[2][1], 
                        values[3][1], 
                        values[4][1],
                        values[5][1],
                        values[6][1],
                        values[7][1],
                        values[8][1],
                        values[9][1],
                        values[10][1],
                        values[11][1],
                        values[12][1],
                        values[13][1],
                        values[14][1],
                        values[15][1],
                        values[16][1],
                        values[17][1],
                        values[18][1],
                        values[19][1],
                        values[20][1],
                        values[21][1],
                        values[22][1],
                        values[23][1],
                        });
                    StatsViewModel.SetPoorPostureTime(new float[] {
                        values[0][0],
                        values[1][0],
                        values[2][0],
                        values[3][0],
                        values[4][0],
                        values[5][0],
                        values[6][0],
                        values[7][0],
                        values[8][0],
                        values[9][0],
                        values[10][0],
                        values[11][0],
                        values[12][0],
                        values[13][0],
                        values[14][0],
                        values[15][0],
                        values[16][0],
                        values[17][0],
                        values[18][0],
                        values[19][0],
                        values[20][0],
                        values[21][0],
                        values[22][0],
                        values[23][0],
                    });
                    StatsViewModel.SetAxisLabels(new string[] {
                        "0時",
                        "1時",
                        "2時",
                        "3時",
                        "4時",
                        "5時",
                        "6時",
                        "7時",
                        "8時",
                        "9時",
                        "10時",
                        "11時",
                        "12時",
                        "13時",
                        "14時",
                        "15時",
                        "16時",
                        "17時",
                        "18時",
                        "19",
                        "20時",
                        "21時",
                        "22時",
                        "23時",
                    });
                    myLabel.Text = "(分)";
                    break;
                case 1:
                    Day_Returnee dayval = new Day_Returnee();
                    dayval.DayReturnee();
                    int daycurrentDayOfWeek = DayOfWeekUtility.GetCurrentDayOfWeek();

                    List<int>[] daykeys = new List<int>[24];
                    List<float>[] dayvalues = new List<float>[24];
                    for (int i = 0; i < 24; i++)
                    {
                        daykeys[i] = new List<int>() { daycurrentDayOfWeek, i };
                        dayvalues[i] = dayval.DayResult[daykeys[i]];
                    }

                    StatsViewModel.SetStartUpTime(new float[] {
                        dayvalues[0][1],
                        dayvalues[1][1],
                        dayvalues[2][1],
                        dayvalues[3][1],
                        dayvalues[4][1],
                        dayvalues[5][1],
                        dayvalues[6][1],
                        dayvalues[7][1],
                        dayvalues[8][1],
                        dayvalues[9][1],
                        dayvalues[10][1],
                        dayvalues[11][1],
                        dayvalues[12][1],
                        dayvalues[13][1],
                        dayvalues[14][1],
                        dayvalues[15][1],
                       
                        });
                    StatsViewModel.SetPoorPostureTime(new float[] {
                       
                    });
                    StatsViewModel.SetAxisLabels(new string[] {
                        "0時",
                        "1時",
                        "2時",
                        "3時",
                        "4時",
                        "5時",
                        "6時",
                        "7時",
                        "8時",
                        "9時",
                        "10時",
                        "11時",
                        "12時",
                        "13時",
                        "14時",
                        "15時",
                        "16時",
                        "17時",
                        "18時",
                        "19",
                        "20時",
                        "21時",
                        "22時",
                        "23時",
                    });
                    myLabel.Text = "(時間)";
                    break;
                case 2:
                    myLabel.Text = "(時間)";
                    StatsViewModel.SetStartUpTime(new float[] { 30, 40, 50, 60, 70, 80, 90 });
                    StatsViewModel.SetPoorPostureTime(new float[] { 40, 50, 6, 70, 80, 90, 100 });
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
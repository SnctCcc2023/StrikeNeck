using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using LiveCharts.Wpf;
using StrikeNeck.ViewModels;
using System;

namespace strikeneck

{
    enum DayOfWeek
    {
        SUN, MON, TUE, WED, THU, FRI, SAT
    }

    public class WeekCountUtility
    {
        public static int GetWeekCount(DateTime targetDate)
        {
            // 指定した年と月の最初の日を取得
            DateTime firstDayOfMonth = new DateTime(targetDate.Year, targetDate.Month, 1);

            // 指定した年と月の最後の日を取得
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            // 最初の日から最後の日までの日数を計算
            int totalDays = (int)(lastDayOfMonth - firstDayOfMonth).TotalDays + 1;

            // 週数を計算
            int weekCount = (int)Math.Ceiling((double)totalDays / 7);

            return weekCount;
        }
    }
    public class MonthUtility
    {
        public static int GetCurrentMonth()
        {
            // 現在の日時を取得し、その月の番号を返す
            return DateTime.Now.Month;
        }
    }

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

        DateTime date = DateTime.Now;
        private void DurationPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedIndex = picker.SelectedIndex;
            Label myLabel = this.FindByName<Label>("unit");
            DataAccessor val = new DataAccessor();
            date = DateTime.Now;
            switch (selectedIndex)
            {   
                case 0:
                    var analyticsPerDay = DataAccessor.GetAnalyticsPerDay(date);
                    float[] activateTimes = new float[24];
                    float[] forwardLeanTimes = new float[24];
                    for (int i = 0; i < 24; i++)
                    {
                        activateTimes[i]= analyticsPerDay.GetAnalyticsPerHour(i).ActiveTime;
                        forwardLeanTimes[i]=analyticsPerDay.GetAnalyticsPerHour(i).ForwardLeanTime;
                    }
                    StatsViewModel.SetStartUpTime(activateTimes);
                        
                    StatsViewModel.SetPoorPostureTime(forwardLeanTimes);
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
                    var analyticsPerWeek = DataAccessor.GetAnalyticsPerWeek(date);
                    float ActivateSun = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SUN).ActiveTime; 
                    float ActivateMon = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.MON).ActiveTime;
                    float ActivateTue = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.TUE).ActiveTime;
                    float ActivateWed = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.WED).ActiveTime;
                    float ActivateThu = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.THU).ActiveTime;
                    float ActivateFri = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.FRI).ActiveTime;
                    float ActivateSat = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SAT).ActiveTime;

                    float fowardLeanSun = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SUN).ForwardLeanTime;
                    float fowardLeanMon = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.MON).ForwardLeanTime;
                    float fowardLeanTue = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.TUE).ForwardLeanTime;
                    float fowardLeanWed = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.WED).ForwardLeanTime;
                    float fowardLeanThu = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.THU).ForwardLeanTime;
                    float fowardLeanFri = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.FRI).ForwardLeanTime;
                    float fowardLeanSat = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SAT).ForwardLeanTime;

                    StatsViewModel.SetStartUpTime(new float[] {
                        ActivateSun,
                        ActivateMon,
                        ActivateTue,
                        ActivateWed,
                        ActivateThu,
                        ActivateFri,
                        ActivateSat,
                        });
                    StatsViewModel.SetPoorPostureTime(new float[] {
                        fowardLeanSun,
                        fowardLeanMon,
                        fowardLeanTue,
                        fowardLeanWed,
                        fowardLeanThu,
                        fowardLeanFri,
                        fowardLeanSat,
                    });
                    StatsViewModel.SetAxisLabels(new string[] {
                        "SUN",
                        "MON",
                        "TUE",
                        "WED",
                        "THU",
                        "FRI",
                        "SAT",
                        
                    });
                    myLabel.Text = "(時間)";
                    break;
                case 2:
                    var analyticsPerMonth = DataAccessor.GetAnalyticsPerMonth(date);
                    int weekCount = WeekCountUtility.GetWeekCount(date);
                    float[] monthActivateTimes = new float[weekCount];
                    float[] monthForwardLeanTimes = new float[weekCount];
                    string[] monthAxis = new string[weekCount];
                    for (int i = 0; i < weekCount; i++)
                    {
                        monthActivateTimes[i] = analyticsPerMonth.GetAnalyticsPerWeek(i).ActiveTime;
                        monthForwardLeanTimes[i] = analyticsPerMonth.GetAnalyticsPerWeek(i).ForwardLeanTime;
                        monthAxis[i] = $"第{i}週";
                    }

                    StatsViewModel.SetStartUpTime(monthActivateTimes);
                    StatsViewModel.SetPoorPostureTime(monthForwardLeanTimes);
                    StatsViewModel.SetAxisLabels(monthAxis);
                    myLabel.Text = "(時間)";
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

        private void BackButton_Clicked(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedIndex = picker.SelectedIndex;
            Label myLabel = this.FindByName<Label>("unit");
            DataAccessor val = new DataAccessor();
            
            switch (selectedIndex)
            {
                case 0:
                    date = date.AddDays(-1);
                    var analyticsPerDay = DataAccessor.GetAnalyticsPerDay(date);
                    float[] activateTimes = new float[24];
                    float[] forwardLeanTimes = new float[24];
                    for (int i = 0; i < 24; i++)
                    {
                        activateTimes[i] = analyticsPerDay.GetAnalyticsPerHour(i).ActiveTime;
                        forwardLeanTimes[i] = analyticsPerDay.GetAnalyticsPerHour(i).ForwardLeanTime;
                    }
                    StatsViewModel.SetStartUpTime(activateTimes);
                    StatsViewModel.SetPoorPostureTime(forwardLeanTimes);
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
                    date = date.AddDays(-7);
                    var analyticsPerWeek = DataAccessor.GetAnalyticsPerWeek(date);
                    float ActivateSun = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SUN).ActiveTime;
                    float ActivateMon = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.MON).ActiveTime;
                    float ActivateTue = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.TUE).ActiveTime;
                    float ActivateWed = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.WED).ActiveTime;
                    float ActivateThu = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.THU).ActiveTime;
                    float ActivateFri = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.FRI).ActiveTime;
                    float ActivateSat = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SAT).ActiveTime;

                    float fowardLeanSun = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SUN).ForwardLeanTime;
                    float fowardLeanMon = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.MON).ForwardLeanTime;
                    float fowardLeanTue = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.TUE).ForwardLeanTime;
                    float fowardLeanWed = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.WED).ForwardLeanTime;
                    float fowardLeanThu = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.THU).ForwardLeanTime;
                    float fowardLeanFri = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.FRI).ForwardLeanTime;
                    float fowardLeanSat = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SAT).ForwardLeanTime;

                    StatsViewModel.SetStartUpTime(new float[] {
                        ActivateSun,
                        ActivateMon,
                        ActivateTue,
                        ActivateWed,
                        ActivateThu,
                        ActivateFri,
                        ActivateSat,
                        });
                    StatsViewModel.SetPoorPostureTime(new float[] {
                        fowardLeanSun,
                        fowardLeanMon,
                        fowardLeanTue,
                        fowardLeanWed,
                        fowardLeanThu,
                        fowardLeanFri,
                        fowardLeanSat,
                    });
                    StatsViewModel.SetAxisLabels(new string[] {
                        "SUN",
                        "MON",
                        "TUE",
                        "WED",
                        "THU",
                        "FRI",
                        "SAT",

                    });
                    myLabel.Text = "(時間)";
                    break;
                case 2:
                    date = date.AddMonths(-1);
                    var analyticsPerMonth = DataAccessor.GetAnalyticsPerMonth(date);
                    int weekCount = WeekCountUtility.GetWeekCount(date);
                    float[] monthActivateTimes = new float[weekCount];
                    float[] monthForwardLeanTimes = new float[weekCount];
                    string[] monthAxis = new string[weekCount];
                    for (int i = 0; i < weekCount; i++)
                    {
                        monthActivateTimes[i] = analyticsPerMonth.GetAnalyticsPerWeek(i).ActiveTime;
                        monthForwardLeanTimes[i] = analyticsPerMonth.GetAnalyticsPerWeek(i).ForwardLeanTime;
                        monthAxis[i] = $"第{i}週";
                    }

                    StatsViewModel.SetStartUpTime(monthActivateTimes);
                    StatsViewModel.SetPoorPostureTime(monthForwardLeanTimes);
                    StatsViewModel.SetAxisLabels(monthAxis);
                    myLabel.Text = "(時間)";
                    break;
            }

            StatsViewModel.Series = StatsViewModel.Series;
            StatsViewModel.XAxes = StatsViewModel.XAxes;
            StatsViewModel.UpdateGraph();
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedIndex = picker.SelectedIndex;
            Label myLabel = this.FindByName<Label>("unit");
            DataAccessor val = new DataAccessor();

            switch (selectedIndex)
            {
                case 0:
                    date = date.AddDays(1);
                    var analyticsPerDay = DataAccessor.GetAnalyticsPerDay(date);
                    float[] activateTimes = new float[24];
                    float[] forwardLeanTimes = new float[24];
                    for (int i = 0; i < 24; i++)
                    {
                        activateTimes[i] = analyticsPerDay.GetAnalyticsPerHour(i).ActiveTime;
                        forwardLeanTimes[i] = analyticsPerDay.GetAnalyticsPerHour(i).ForwardLeanTime;
                    }
                    StatsViewModel.SetStartUpTime(activateTimes);
                    StatsViewModel.SetPoorPostureTime(forwardLeanTimes);
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
                    date = date.AddDays(7);
                    var analyticsPerWeek = DataAccessor.GetAnalyticsPerWeek(date);
                    float ActivateSun = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SUN).ActiveTime;
                    float ActivateMon = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.MON).ActiveTime;
                    float ActivateTue = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.TUE).ActiveTime;
                    float ActivateWed = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.WED).ActiveTime;
                    float ActivateThu = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.THU).ActiveTime;
                    float ActivateFri = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.FRI).ActiveTime;
                    float ActivateSat = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SAT).ActiveTime;

                    float fowardLeanSun = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SUN).ForwardLeanTime;
                    float fowardLeanMon = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.MON).ForwardLeanTime;
                    float fowardLeanTue = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.TUE).ForwardLeanTime;
                    float fowardLeanWed = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.WED).ForwardLeanTime;
                    float fowardLeanThu = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.THU).ForwardLeanTime;
                    float fowardLeanFri = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.FRI).ForwardLeanTime;
                    float fowardLeanSat = analyticsPerWeek.GetAnalyticsPerDay(DayOfWeek.SAT).ForwardLeanTime;

                    StatsViewModel.SetStartUpTime(new float[] {
                        ActivateSun,
                        ActivateMon,
                        ActivateTue,
                        ActivateWed,
                        ActivateThu,
                        ActivateFri,
                        ActivateSat,
                        });
                    StatsViewModel.SetPoorPostureTime(new float[] {
                        fowardLeanSun,
                        fowardLeanMon,
                        fowardLeanTue,
                        fowardLeanWed,
                        fowardLeanThu,
                        fowardLeanFri,
                        fowardLeanSat,
                    });
                    StatsViewModel.SetAxisLabels(new string[] {
                        "SUN",
                        "MON",
                        "TUE",
                        "WED",
                        "THU",
                        "FRI",
                        "SAT",

                    });
                    myLabel.Text = "(時間)";
                    break;
                case 2:
                    date = date.AddMonths(1);
                    var analyticsPerMonth = DataAccessor.GetAnalyticsPerMonth(date);
                    int weekCount = WeekCountUtility.GetWeekCount(date);
                    float[] monthActivateTimes = new float[weekCount];
                    float[] monthForwardLeanTimes = new float[weekCount];
                    string[] monthAxis = new string[weekCount];
                    for (int i = 0; i < weekCount; i++)
                    {
                        monthActivateTimes[i] = analyticsPerMonth.GetAnalyticsPerWeek(i).ActiveTime;
                        monthForwardLeanTimes[i] = analyticsPerMonth.GetAnalyticsPerWeek(i).ForwardLeanTime;
                        monthAxis[i] = $"第{i}週";
                    }

                    StatsViewModel.SetStartUpTime(monthActivateTimes);
                    StatsViewModel.SetPoorPostureTime(monthForwardLeanTimes);
                    StatsViewModel.SetAxisLabels(monthAxis);
                    myLabel.Text = "(時間)";
                    break;
            }

            StatsViewModel.Series = StatsViewModel.Series;
            StatsViewModel.XAxes = StatsViewModel.XAxes;
            StatsViewModel.UpdateGraph();
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
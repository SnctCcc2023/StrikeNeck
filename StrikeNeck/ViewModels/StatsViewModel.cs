using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SkiaSharp.Views.Maui;
using Microsoft.Maui.Graphics;


namespace StrikeNeck.ViewModels
{
    public class StatsViewModel : INotifyPropertyChanged
    {
        private ISeries[] series;
        private static int[] startUpTime = { 1, 2, 3, 4, 5, 6, 7 };
        private static int[] poorPostureTime = { 7, 6, 5, 4, 3, 2, 1 };
        public StatsViewModel()
        {
            series = new ISeries[]
            {
        new ColumnSeries<int> {
            Values = startUpTime,
            MaxBarWidth = 60,
            IgnoresBarPosition = true},

        new ColumnSeries<int> {
            Values = poorPostureTime,
            MaxBarWidth = 30,
            IgnoresBarPosition = true}
            };
            xAxes = new[]
            {
        new Axis { Labels = axisLabels }
    };
        }
        public ISeries[] Series
        {
            get { return series; }
            set
            {
                series = value;
                OnPropertyChanged();
            }
        }

        private Axis[] xAxes;
        public Axis[] XAxes
        {
            get { return xAxes; }
            set
            {
                xAxes = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // Values ÇïœêîÇ…ë„ì¸Ç∑ÇÈó·

        public static void SetStartUpTime(int[] newValues)
        {
            startUpTime = newValues;
        }
        public static void SetPoorPostureTime(int[] newValues)
        {
            poorPostureTime = newValues;
        }

        public void UpdateGraph()
        {
            series = new ISeries[]
            {
        new ColumnSeries<int> {
            Values = startUpTime,
            Stroke = null,
            MaxBarWidth = 60,
            IgnoresBarPosition = true,
        },
        new ColumnSeries<int> {
            Values = poorPostureTime,
            Stroke = null,
            MaxBarWidth = 30,
            IgnoresBarPosition = true,
        }
            };
            XAxes = new[]
            {
        new Axis { Labels = axisLabels }
    };
            OnPropertyChanged(nameof(Series));
            OnPropertyChanged(nameof(XAxes));
        }

        // Labels ÇïœêîÇ…ë„ì¸Ç∑ÇÈó·
        private static string[] axisLabels = { "20", "21", "22", "23", "24", "25", "26" };
        public static void SetAxisLabels(string[] newValues)
        {
            axisLabels = newValues;
        }
    }
}
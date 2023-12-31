using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

using System.ComponentModel;
using System.Runtime.CompilerServices;



namespace StrikeNeck.ViewModels
{
    public class StatsViewModel : INotifyPropertyChanged
    {
        private ISeries[] series;
        private static float[] startUpTime = { 1, 2, 3, 4, 5, 6, 7 };
        private static float[] poorPostureTime = { 7, 6, 5, 4, 3, 2, 1 };
        public StatsViewModel()
        {
            series = new ISeries[]
            {
        new ColumnSeries<float> {
            Values = startUpTime,
            MaxBarWidth = 60,
            Fill = new SolidColorPaint(SKColors.Blue),

            IgnoresBarPosition = true},

        new ColumnSeries<float> {
            Values = poorPostureTime,
            MaxBarWidth = 30,
            Fill = new SolidColorPaint(SKColors.Red),
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
        // Values を変数に代入する例

        public static void SetStartUpTime(float[] newValues)
        {
            startUpTime = newValues;
        }
        public static void SetPoorPostureTime(float[] newValues)
        {
            poorPostureTime = newValues;
        }

        public void UpdateGraph()
        {
            series = new ISeries[]
            {
        new ColumnSeries<float> {
            Values = startUpTime,
            Fill = new SolidColorPaint(SKColors.Blue),
            
            MaxBarWidth = 60,
            IgnoresBarPosition = true,
        },
        new ColumnSeries<float> {
            Values = poorPostureTime,
            Fill = new SolidColorPaint(SKColors.Red),
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

        // Labels を変数に代入する例
        private static string[] axisLabels = { "20", "21", "22", "23", "24", "25", "26" };
        public static void SetAxisLabels(string[] newValues)
        {
            axisLabels = newValues;
        }
    }
}
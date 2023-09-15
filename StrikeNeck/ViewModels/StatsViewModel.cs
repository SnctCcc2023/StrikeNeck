using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace StrikeNeck.ViewModels
{
    public class StatsViewModel
    {
        public ISeries[] Series { get; set; } =
        {
            new ColumnSeries<double>
            {
                Values = new double[] { 20, 12, 13, 15, 2, 14, 6 },
                Stroke = null,
                Fill = new SolidColorPaint(SKColors.CornflowerBlue),
                IgnoresBarPosition = true
            },
            new LineSeries<double>
            {
                Values = new double[] { 18, 4, 6, 8, 12, 13, 8 },
                Stroke = null,
                Fill = null
            }
        };

        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                Labels = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul" },
                LabelsRotation = 0,
                SeparatorsPaint = new SolidColorPaint(new SKColor(180, 180, 180)),
                SeparatorsAtCenter = false,
                TicksPaint = new SolidColorPaint(new SKColor(100, 100, 100)),
                TicksAtCenter = true
            }
        };
    }
}
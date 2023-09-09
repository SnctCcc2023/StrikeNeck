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
            }
        };
    }
}
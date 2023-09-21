using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;

using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.Painting.ImageFilters;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace StrikeNeck.ViewModels
{
    
    public class StatsViewModel
    {
        public ISeries[] Series { get; set; } =
        {
            new ColumnSeries<int>
            {
                Values = new[] { 6, 3, 5, 7, 3, 4, 6, 3 },
                Stroke = null,
                MaxBarWidth = 60,
                IgnoresBarPosition = true,
             
            },
            new ColumnSeries<int>
            {
                Values = new[] { 2, 4, 5, 9, 5, 2, 4, 7 },
                Stroke = null,
                MaxBarWidth = 30,
                IgnoresBarPosition = true,
               

            },
            new LineSeries<double>
            {
                Values = new double[] { 2, 1, 3, 5, 3, 4, 6, 8 },
                Fill = null,
            }

        };
        


    }
}

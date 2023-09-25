using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace StrikeNeck.ViewModels
{

    public partial class StatsViewModel
    {
        public ISeries[] Series { get; set; } =
        {
            new ColumnSeries<int>
            {
                Values = new[] { 6, 3, 5, 7, 3, 4, 6,  },
                Stroke = null,
                MaxBarWidth = 60,
                IgnoresBarPosition = true
            },
            new ColumnSeries<int>
            {
                Values = new[] { 2, 4, 8, 9, 5, 2, 4,  },
                Stroke = null,
                MaxBarWidth = 30,
                IgnoresBarPosition = true
            }
        };
        public Axis[] XAxes { get; set; }
        = new Axis[]
        {
            new Axis
            {
                // Use the labels property to define named labels.
                Labels = new string[] {"20", "21", "22", "23" ,"24","25","26"}
                }
            };
    }
    
}
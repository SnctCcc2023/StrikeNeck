using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
namespace strikeneck;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

    }

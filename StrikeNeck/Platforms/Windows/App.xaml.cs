﻿using Microsoft.UI.Xaml;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace strikeneck.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : MauiWinUIApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        static Mutex? mutex;

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!IsSingleInstance())
            {
                Process.GetCurrentProcess().Kill();
            }
            else
            {
                base.OnLaunched(args);
            }
        }

        static bool IsSingleInstance()
        {
            const string applicationId = "YOUR_APP_ID_FROM_CSPROJ";
            mutex = new Mutex(false, applicationId);
            GC.KeepAlive(mutex);

            try
            {
                return mutex.WaitOne(0, false);
            }
            catch (AbandonedMutexException)
            {
                mutex.ReleaseMutex();
                return mutex.WaitOne(0, false);
            }
        }
    }
}
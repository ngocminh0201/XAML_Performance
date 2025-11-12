using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Test3
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        private Window? _window;
        private static readonly string PerfLogPath = System.IO.Path.Combine("ResourceScope", "perf_log.txt");

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            var sw = Stopwatch.StartNew();
            InitializeComponent();
            sw.Stop();
            var msg = $"[{DateTime.Now:O}] InitializeComponent() took {sw.Elapsed.TotalMilliseconds} ms";
            Debug.WriteLine(msg);
            try
            {
                System.IO.Directory.CreateDirectory("ResourceScope");
                System.IO.File.AppendAllText(PerfLogPath, msg + Environment.NewLine);
            }
            catch { /* best-effort logging */ }
        }

        /// <summary>
        /// Invoked when the application is launched.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            var sw = Stopwatch.StartNew();
            _window = new MainWindow();
            _window.Activate();
            sw.Stop();
            var msg = $"[{DateTime.Now:O}] App OnLaunched total (create+activate MainWindow) took {sw.Elapsed.TotalMilliseconds} ms";
            Debug.WriteLine(msg);
            try
            {
                System.IO.File.AppendAllText(PerfLogPath, msg + Environment.NewLine);
            }
            catch { /* best-effort logging */ }
        }
    }
}

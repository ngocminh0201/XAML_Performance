using Microsoft.UI.Xaml;
using System.Collections.Generic;
using Test3.DataVirtualization;
using Test3.ReduceVisualTree;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Test3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        // Keep strong references to any additional windows so they aren't GC'd and closed.
        private readonly List<Window> _openWindows = new();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HeavyListViewButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window()
            {
                Content = new HeavyListViewPage()
            };
            _openWindows.Add(win);
            win.Activate();
        }

        private void OptimizedListViewButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window
            {
                Content = new OptimizedListViewPage()
            };
            _openWindows.Add(win);
            win.Activate();
        }

        private void WidthButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window
            {
                Content = new WidthPage()
            };
            _openWindows.Add(win);
            win.Activate();
        }

        private void CollapsedVisibility_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window
            {
                Content = new CollapsedVisibilityPage()
            };
            _openWindows.Add(win);
            win.Activate();
        }
        private void xLoad_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window
            {
                Content = new xLoadPage()
            };
            _openWindows.Add(win);
            win.Activate();
        }

        private void InefficientProperty_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window
            {
                Content = new XAMLMarkup.ReduceVisualTree.UsePanelLayoutProperties.InefficientPage()
            };
            _openWindows.Add(win);
            win.Activate();
        }

        private void EfficientProperty_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window
            {
                Content = new XAMLMarkup.ReduceVisualTree.UsePanelLayoutProperties.EfficientPage()
            };
            _openWindows.Add(win);
            win.Activate();
        }

        private void InefficientImage_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window
            {
                Content = new XAMLMarkup.ReduceVisualTree.UseImageInstead.InefficientPage()
            };
            _openWindows.Add(win);
            win.Activate();
        }

        private void EfficientImage_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window
            {
                Content = new XAMLMarkup.ReduceVisualTree.UseImageInstead.EfficientPage()
            };
            _openWindows.Add(win);
            win.Activate();

        }

        private void OptimizedResourceDefine_Click(object sender, RoutedEventArgs e)
        {
            var win = new Window
            {
                Content = new XAMLMarkup.ResourceAndResourceDictionary.Definition()
            };
            _openWindows.Add(win);
            win.Activate();
        }
    }
}

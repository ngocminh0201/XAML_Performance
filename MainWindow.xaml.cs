using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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

        void CreateNewWindow(Page page)
        {
            var win = new Window
            {
                Content = page
            };
            _openWindows.Add(win);
            win.Activate();
        }

        private void HeavyListViewButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new HeavyListViewPage());
        }

        private void OptimizedListViewButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new OptimizedListViewPage());
        }

        private void WidthButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new WidthPage());
        }

        private void CollapsedVisibility_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new CollapsedVisibilityPage());
        }
        private void xLoad_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new xLoadPage());
        }

        private void InefficientProperty_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new XAMLMarkup.ReduceVisualTree.UsePanelLayoutProperties.InefficientPage());
        }

        private void EfficientProperty_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new XAMLMarkup.ReduceVisualTree.UsePanelLayoutProperties.EfficientPage());
        }

        private void InefficientImage_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new XAMLMarkup.ReduceVisualTree.UseImageInstead.InefficientPage());
        }

        private void EfficientImage_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new XAMLMarkup.ReduceVisualTree.UseImageInstead.EfficientPage());
        }

        private void OptimizedResourceDefine_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new XAMLMarkup.ResourceAndResourceDictionary.Definition());
        }

        private void UseXNameResource_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new XAMLMarkup.ResourceAndResourceDictionary.xNamePage());
        }

        private void UseXKeyResource_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new XAMLMarkup.ResourceAndResourceDictionary.xKeyPage());
        }

        private void UserControlResource_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new XAMLMarkup.ResourceInUserControl.ResourceInUserControlPage());
        }

        private void NotUserControlResource_Click(object sender, RoutedEventArgs e)
        {
            CreateNewWindow(new XAMLMarkup.ResourceAndResourceDictionary.ResourceNotInUserControl.ResourceNotInUserControlPage());
        }
    }
}

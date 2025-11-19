using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Test3.DataVirtualization
{
    public sealed partial class OptimizedListViewPage : Page
    {
        public IncrementalProductSource DataSource { get; } = new();
        public OptimizedListViewPage()
        {
            InitializeComponent();
            DataSource.LoadMore();

            // Attach scroll-based incremental trigger
            ProductList.Loaded += (s, e) =>
            {
                var sv = FindDescendant<ScrollViewer>(ProductList);
                if (sv != null)
                    sv.ViewChanged += OnScrollViewerViewChanged;
            };
        }
        private void OnScrollViewerViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            var sv = (ScrollViewer)sender;
            if (sv.VerticalOffset + sv.ViewportHeight >= sv.ExtentHeight - 400)
            {
                DataSource.LoadMore();
            }
        }
        private static T? FindDescendant<T>(DependencyObject d) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(d);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(d, i);
                if (child is T t) return t;
                var res = FindDescendant<T>(child);
                if (res != null) return res;
            }
            return null;
        }
    }

    public class IncrementalProductSource : ObservableCollection<Product>
    {
        private const int PageSize = 50;
        private const int MaxItems = 2000;
        private int _loaded = 0;
        private bool _busy = false;
        string imagePath = "/Assets/sample_large_image2.png";

        public void LoadMore()
        {
            if (_loaded >= MaxItems) return;
            int take = Math.Min(PageSize, MaxItems - _loaded);
            for (int i = 0; i < take; i++)
            {
                int idx = _loaded + i;
                Add(new Product
                {
                    Name = $"Product {idx}",
                    Price = string.Format("{0:C}", idx * 1.25),
                    Description = "This is a long description used to keep the visual shape identical to the heavy page. " +
                                  "However, decoding and UI container creation are minimized via virtualization and delayed image decode.",
                    ImagePath = imagePath
                });
            }
            _loaded += take;
        }
    }

    public class ImageLoaderConverter : IValueConverter
    {
        private static Uri CreateUriFromPath(string path)
        {
            if (Uri.TryCreate(path, UriKind.Absolute, out var absolute))
                return absolute;

            var p = path.Replace('\\', '/').TrimStart('/');
            return new Uri($"ms-appx:///{p}", UriKind.Absolute);
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string? path = value as string;
            if (string.IsNullOrEmpty(path)) return null;

            int decodeWidth = 0;
            if (parameter is string p && int.TryParse(p, out int w)) decodeWidth = w;

            try
            {
                var uri = CreateUriFromPath(path);
                var bmp = new BitmapImage(uri);
                if (decodeWidth > 0) bmp.DecodePixelWidth = decodeWidth;
                return bmp;
            }
            catch
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}



using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Test3.DataVirtualization
{
    public sealed partial class HeavyListViewPage : Page
    {
        public ObservableCollection<Product> Products { get; set; } = new();
        public HeavyListViewPage()
        {
            InitializeComponent();

            for (int i = 0; i < 1000; i++)
            {
                Products.Add(new Product
                {
                    Name = $"Product {i}",
                    Price = $"{i * 1.25:C}",
                    Description = "This is a very long description text that slows down layout rendering.",
                    ImagePath = "/Assets/sample_large_image2.png",
                    IsSpecial = i % 5 == 0 // every 5th item is 'special'
                });
            }
        }      
    }
    public class Product
    {
        public string Name { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public bool IsSpecial { get; set; }
    }
}

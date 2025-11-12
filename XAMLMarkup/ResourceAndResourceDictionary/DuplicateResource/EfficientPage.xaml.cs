using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Test3.XAMLMarkup.ResourceAndResourceDictionary.DuplicateResource
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EfficientPage : Page
    {
        public EfficientPage()
        {
            InitializeComponent();
            var res = (SolidColorBrush)Resources["MyOrangeBrush"];
            Debug.WriteLine($"Res TextBlock vs Button same?  {ReferenceEquals(TextBlock.Foreground, Button.Background)}");
            Debug.WriteLine($"Res TextBlock vs Rectangle same?  {ReferenceEquals(TextBlock.Foreground, Rectangle.Fill)}");
            Debug.WriteLine($"Res Rectangle vs res same?  {ReferenceEquals(Rectangle.Fill, res)}");
        }
    }
}

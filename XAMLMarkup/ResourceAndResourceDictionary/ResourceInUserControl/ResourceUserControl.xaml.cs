using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Test3.XAMLMarkup.ResourceInUserControl
{
     public sealed partial class ResourceUserControl : UserControl
     {
         public int Number
         {
             get { return (int)GetValue(NumberProperty); }
             set { SetValue(NumberProperty, value); }
         }

         public static readonly DependencyProperty NumberProperty =
                DependencyProperty.Register("Number", typeof(int), typeof(ResourceUserControl), new PropertyMetadata(0));

         public ResourceUserControl()
         {
             this.InitializeComponent();
         }
     }
}
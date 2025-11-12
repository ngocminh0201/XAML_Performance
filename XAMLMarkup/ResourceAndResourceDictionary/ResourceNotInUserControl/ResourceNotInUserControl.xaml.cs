using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Test3.XAMLMarkup.ResourceInUserControl;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Test3.XAMLMarkup.ResourceAndResourceDictionary.ResourceNotInUserControl
{
    public sealed partial class ResourceNotInUserControl : UserControl
    {
        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, value); }
        }

        public static readonly DependencyProperty NumberProperty =
               DependencyProperty.Register("Number", typeof(int), typeof(ResourceUserControl), new PropertyMetadata(0));

        public ResourceNotInUserControl()
        {
            InitializeComponent();
        }
    }
}

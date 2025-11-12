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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ResourceNotInUserControlPage : Page
    {
        public ObservableCollection<int> Items { get; } = new();
        public ResourceNotInUserControlPage()
        {
            InitializeComponent();
            for (int i = 1; i <= 1000; i++)
            {
                Items.Add(i);
            }
            var player = new MediaPlayer
            {
                Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/happy.mp4")),
                IsLoopingEnabled = true
            };
            Player.SetMediaPlayer(player);
            player.Play();
        }
    }
}

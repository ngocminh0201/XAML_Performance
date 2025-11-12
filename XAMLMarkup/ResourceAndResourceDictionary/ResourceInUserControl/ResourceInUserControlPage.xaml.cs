using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using Windows.Media.Core;
using Windows.Media.Playback;

namespace Test3.XAMLMarkup.ResourceInUserControl
{
     public sealed partial class ResourceInUserControlPage : Page
     {
         public ObservableCollection<int> Items { get; } = new();

         public ResourceInUserControlPage()
         {
             this.InitializeComponent();
             for (int i = 1; i <= 1000; i++)
             {
                Items.Add(i);
             }
            var player = new MediaPlayer
            {
                Source = MediaSource.CreateFromUri(new Uri("ms-appx:///Assets/sad.mp4")),
                IsLoopingEnabled = true
            };
            Player.SetMediaPlayer(player);
            player.Play();
        }
     }
}
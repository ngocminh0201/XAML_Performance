using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Test3.XAMLLayout.EventLayoutChanged
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SizeChangedPage : Page
    {
        private static int Counter = 0;
        private const string ExampleXaml =
@"<Grid x:Name=""RootGrid""
      Background=""LightGray""
      SizeChanged=""RootGrid_SizeChanged"">
    <StackPanel HorizontalAlignment=""Center""
                VerticalAlignment=""Center"">
        <Rectangle x:Name=""MyRect""
                   Width=""100""
                   Height=""100""
                   Margin=""0,0,0,20""
                   Fill=""Red"" />
        <Button x:Name=""ResizeButton""
                Click=""ResizeButton_Click""
                Content=""Tăng kích thước hình vuông"" />
    </StackPanel>
</Grid>";


        public SizeChangedPage()
        {
            InitializeComponent();
            UpdateTextBlock();
            BuildHighlightedCode();
        }

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            MyRect.Width += 10;
            MyRect.Height += 10;
            UpdateTextBlock();
        }

        private void UpdateTextBlock()
        {
            SizeInfoTextBlock.Text = $"Width: {MyRect.Width}, Height: {MyRect.Height}";
        }

        private void RootGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SizeChangedText.Text = $"Sự kiện SizeChanged đã được gọi {Counter++} lần.";
        }
        private void BuildHighlightedCode()
        {
            CodeBlock.Blocks.Clear();
            var paragraph = new Paragraph();

            string s = ExampleXaml;
            int i = 0;
            while (i < s.Length)
            {
                char c = s[i];
                if (c == '<')
                {
                    int end = s.IndexOf('>', i);
                    if (end == -1) end = s.Length - 1;
                    string tagSegment = s.Substring(i, end - i + 1);
                    int tagNameStart = (tagSegment.Length > 1 && tagSegment[1] == '/') ? 2 : 1;
                    paragraph.Inlines.Add(new Run { Text = tagSegment.Substring(0, tagNameStart), Foreground = new SolidColorBrush(Colors.Black) });
                    int j = tagNameStart;
                    while (j < tagSegment.Length && !char.IsWhiteSpace(tagSegment[j]) && tagSegment[j] != '>' && tagSegment[j] != '/') j++;
                    if (j > tagNameStart)
                        paragraph.Inlines.Add(new Run { Text = tagSegment.Substring(tagNameStart, j - tagNameStart), Foreground = new SolidColorBrush(Colors.DarkBlue) });
                    int k = j;
                    while (k < tagSegment.Length)
                    {
                        char ch = tagSegment[k];
                        if (ch == '"')
                        {
                            int valEnd = tagSegment.IndexOf('"', k + 1);
                            if (valEnd == -1) valEnd = tagSegment.Length - 1;
                            string val = tagSegment.Substring(k, valEnd - k + 1);
                            paragraph.Inlines.Add(new Run { Text = val, Foreground = new SolidColorBrush(Colors.DarkGreen) });
                            k = valEnd + 1;
                        }
                        else
                        {
                            int nextQuote = tagSegment.IndexOf('"', k);
                            int nextSpace = tagSegment.IndexOf(' ', k + 1);
                            int next = -1;
                            if (nextQuote == -1 && nextSpace == -1) next = tagSegment.Length;
                            else if (nextQuote == -1) next = nextSpace;
                            else if (nextSpace == -1) next = nextQuote;
                            else next = System.Math.Min(nextQuote, nextSpace);
                            string part = tagSegment.Substring(k, next - k);
                            paragraph.Inlines.Add(new Run { Text = part, Foreground = new SolidColorBrush(Colors.DarkRed) });
                            k = next;
                        }
                    }

                    i = end + 1;
                }
                else
                {
                    int next = s.IndexOf('<', i);
                    if (next == -1) next = s.Length;
                    string txt = s.Substring(i, next - i);
                    paragraph.Inlines.Add(new Run { Text = txt, Foreground = new SolidColorBrush(Colors.Black) });
                    i = next;
                }
            }
            paragraph.FontSize = 20;
            CodeBlock.Blocks.Add(paragraph);
        }

        private void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            var dp = new DataPackage();
            dp.SetText(ExampleXaml);
            Clipboard.SetContent(dp);
        }
    }
}

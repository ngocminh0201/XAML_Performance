using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;

namespace Test3.XAMLLayout.ReduceLayoutStructure
{
    public sealed partial class RelativePanelPage : Page
    {
        private const string ExampleXaml =
@"<RelativePanel>
    <TextBlock x:Name=""Options"" Text=""Options:"" />
    <CheckBox
        x:Name=""PowerUser""
        Content=""Power User""
        RelativePanel.Below=""Options"" />
    <CheckBox
        Margin=""20,0,0,0""
        Content=""Admin""
        RelativePanel.Below=""Options""
        RelativePanel.RightOf=""PowerUser"" />
    <TextBlock
        x:Name=""BasicInformation""
        RelativePanel.Below=""PowerUser""
        Text=""Basic information:"" />
    <TextBlock RelativePanel.AlignVerticalCenterWith=""NameBox"" Text=""Name:"" />
    <TextBox
        x:Name=""NameBox""
        Width=""200""
        Margin=""75,0,0,0""
        RelativePanel.Below=""BasicInformation"" />
    <TextBlock RelativePanel.AlignVerticalCenterWith=""EmailBox"" Text=""Email:"" />
    <TextBox
        x:Name=""EmailBox""
        Width=""200""
        Margin=""75,0,0,0""
        RelativePanel.Below=""NameBox"" />
    <TextBlock RelativePanel.AlignVerticalCenterWith=""PasswordBox"" Text=""Password:"" />
    <TextBox
        x:Name=""PasswordBox""
        Width=""200""
        Margin=""75,0,0,0""
        RelativePanel.Below=""EmailBox"" />
    <Button Content=""Save"" RelativePanel.Below=""PasswordBox"" />
</RelativePanel>";

        public RelativePanelPage()
        {
            this.InitializeComponent();
            BuildHighlightedCode();
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

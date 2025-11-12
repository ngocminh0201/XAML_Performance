using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;

namespace Test3.XAMLLayout.ReduceLayoutStructure
{
    public sealed partial class StackPanelPage : Page
    {
        private const string ExampleXaml =
@"<StackPanel>
    <TextBlock Text=""Options:"" />
    <StackPanel Orientation=""Horizontal"">
        <CheckBox Content=""Power User"" />
        <CheckBox Margin=""20,0,0,0"" Content=""Admin"" />
    </StackPanel>
    <TextBlock Text=""Basic information:"" />
    <StackPanel Orientation=""Horizontal"">
        <TextBlock Width=""75"" Text=""Name:"" />
        <TextBox Width=""200"" />
    </StackPanel>
    <StackPanel Orientation=""Horizontal"">
        <TextBlock Width=""75"" Text=""Email:"" />
        <TextBox Width=""200"" />
    </StackPanel>
    <StackPanel Orientation=""Horizontal"">
        <TextBlock Width=""75"" Text=""Password:"" />
        <TextBox Width=""200"" />
    </StackPanel>
    <Button Content=""Save"" />
</StackPanel>";


        public StackPanelPage()
        {
            this.InitializeComponent();
            BuildHighlightedCode();
        }

        private void BuildHighlightedCode()
        {
            // Very small, manual syntax coloring for XAML -- not a full parser.
            CodeBlock.Blocks.Clear();
            var paragraph = new Paragraph();

            // Split by characters and produce different runs for tags, attributes, strings.
            // This simple approach highlights: tags (blue), attribute names (red), attribute values (green), punctuation (black).
            string s = ExampleXaml;
            int i = 0;
            while (i < s.Length)
            {
                char c = s[i];
                if (c == '<')
                {
                    // tag until '>'
                    int end = s.IndexOf('>', i);
                    if (end == -1) end = s.Length - 1;
                    string tagSegment = s.Substring(i, end - i + 1);
                    int pos = 0;
                    // '<' or '</'
                    int tagNameStart = (tagSegment.Length > 1 && tagSegment[1] == '/') ? 2 : 1;
                    // write '<' or '</'
                    paragraph.Inlines.Add(new Run { Text = tagSegment.Substring(0, tagNameStart), Foreground = new SolidColorBrush(Colors.Black) });
                    // tag name
                    int j = tagNameStart;
                    while (j < tagSegment.Length && !char.IsWhiteSpace(tagSegment[j]) && tagSegment[j] != '>' && tagSegment[j] != '/') j++;
                    if (j > tagNameStart)
                        paragraph.Inlines.Add(new Run { Text = tagSegment.Substring(tagNameStart, j - tagNameStart), Foreground = new SolidColorBrush(Colors.DarkBlue) });
                    // rest (attributes)
                    int k = j;
                    while (k < tagSegment.Length)
                    {
                        char ch = tagSegment[k];
                        if (ch == '"')
                        {
                            // attribute value
                            int valEnd = tagSegment.IndexOf('"', k + 1);
                            if (valEnd == -1) valEnd = tagSegment.Length - 1;
                            string val = tagSegment.Substring(k, valEnd - k + 1);
                            paragraph.Inlines.Add(new Run { Text = val, Foreground = new SolidColorBrush(Colors.DarkGreen) });
                            k = valEnd + 1;
                        }
                        else
                        {
                            // attribute name or punctuation
                            int nextQuote = tagSegment.IndexOf('"', k);
                            int nextSpace = tagSegment.IndexOf(' ', k + 1);
                            int next = -1;
                            if (nextQuote == -1 && nextSpace == -1) next = tagSegment.Length;
                            else if (nextQuote == -1) next = nextSpace;
                            else if (nextSpace == -1) next = nextQuote;
                            else next = System.Math.Min(nextQuote, nextSpace);
                            string part = tagSegment.Substring(k, next - k);
                            paragraph.Inlines.Add(new Run { Text = part, Foreground = new   SolidColorBrush(Colors.DarkRed) });
                            k = next;
                        }
                    }

                    i = end + 1;
                }
                else
                {
                    // plain text or whitespace until next '<'
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

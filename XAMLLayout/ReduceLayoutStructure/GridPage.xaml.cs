using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Windows.ApplicationModel.DataTransfer;

namespace Test3.XAMLLayout.ReduceLayoutStructure
{
    public sealed partial class GridPage : Page
    {
        private const string ExampleXaml =
@"<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height=""Auto"" />
        <RowDefinition Height=""Auto"" />
        <RowDefinition Height=""Auto"" />
        <RowDefinition Height=""Auto"" />
        <RowDefinition Height=""Auto"" />
        <RowDefinition Height=""Auto"" />
        <RowDefinition Height=""Auto"" />
    </Grid.RowDefinitions>
    <Grid.ColumnDefinitions>
        <ColumnDefinition Width=""Auto"" />
        <ColumnDefinition Width=""Auto"" />
    </Grid.ColumnDefinitions>
    <TextBlock Grid.ColumnSpan=""2"" Text=""Options:"" />
    <CheckBox
        Grid.Row=""1""
        Grid.ColumnSpan=""2""
        Content=""Power User"" />
    <CheckBox
        Grid.Row=""1""
        Grid.ColumnSpan=""2""
        Margin=""150,0,0,0""
        Content=""Admin"" />
    <TextBlock
        Grid.Row=""2""
        Grid.ColumnSpan=""2""
        Text=""Basic information:"" />
    <TextBlock
        Grid.Row=""3""
        Width=""75""
        Text=""Name:"" />
    <TextBox
        Grid.Row=""3""
        Grid.Column=""1""
        Width=""200"" />
    <TextBlock
        Grid.Row=""4""
        Width=""75""
        Text=""Email:"" />
    <TextBox
        Grid.Row=""4""
        Grid.Column=""1""
        Width=""200"" />
    <TextBlock
        Grid.Row=""5""
        Width=""75""
        Text=""Password:"" />
    <TextBox
        Grid.Row=""5""
        Grid.Column=""1""
        Width=""200"" />
    <Button Grid.Row=""6"" Content=""Save"" />
</Grid>";

        public GridPage()
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

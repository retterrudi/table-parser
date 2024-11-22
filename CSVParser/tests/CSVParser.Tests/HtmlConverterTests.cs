using CSVParser.CSVParser;
using Xunit;

namespace CSVParser.tests.CSVParser.Tests;

public class HtmlConverterTests
{
    [Fact]
    public void FillTemplate_simpleTable_convertTableToHtmlString()
    {
        var table = new Table(
            ["Name", "Age", "City"],
            [
                new Dictionary<string, string> { { "Name", "Alice" }, { "Age", "39" }, {"City", "Berlin"} },
                new Dictionary<string, string> { {"Name", "Bernd"}, {"Age", "24"}, {"City", "Hamburg"} }
            ]
        );

        var htmlFileTemplate = @"<html lang=""en"">
<body>
{{rows}}
</body>
</html>";
        var htmlRowsTemplate = @"<div class=""row"">
{{Name}} | {{Age}} | {{City}}
</div>";

        var html = HtmlConverter.FillTemplate(table, htmlFileTemplate, htmlRowsTemplate);

        var expected = @"<html lang=""en"">
<body>
<div class=""row"">
Alice | 39 | Berlin
</div><div class=""row"">
Bernd | 24 | Hamburg
</div>
</body>
</html>";
        
        Assert.Equal(expected, html);
    }
}
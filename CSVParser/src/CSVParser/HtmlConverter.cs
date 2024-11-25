namespace CSVParser.CSVParser;

public static class HtmlConverter
{
    // TODO: Tests
    internal static string FillTemplate(
        Table table, 
        string fileTemplate, 
        string rowsTemplate)
    {
        var rows = "";
        
        foreach (var row in table.rows)
        {
            var filledRowsTemplate = rowsTemplate;
            foreach (var column in table.header)
            {
                filledRowsTemplate = 
                    filledRowsTemplate.Replace($"{{{{{column}}}}}", row[column]);
            }

            rows += filledRowsTemplate;
        }

        return fileTemplate.Replace("{{rows}}", rows);
    }
    
}
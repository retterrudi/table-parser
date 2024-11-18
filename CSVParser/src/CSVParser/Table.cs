namespace CSVParser.CSVParser;

public class Table(
    string[] header,
    List<Dictionary<string, string>> rows)
{
    public string[] header { get; } = header;
    public List<Dictionary<string, string>> rows { get; } = rows;
    
    public static Table TableFromCsv(string csvText, char columnDelimiter)
    {
        var lines = csvText.Split('\n', StringSplitOptions.TrimEntries);
        
        var header = lines
            .First()
            .Split(columnDelimiter, StringSplitOptions.TrimEntries)
            .Where(it => it != "").ToArray();
        var rows = lines.Skip(1).Select(line =>
            {
                var dict = new Dictionary<string, string>();
                var splitLine = line.Split(columnDelimiter, StringSplitOptions.TrimEntries);
                    for (var i = 0; i < splitLine.Length; i++)
                    {
                        // TODO: Refactor - empty fields should be possible but no empty line
                        if (splitLine[i] != "") 
                        {
                            dict.Add(header[i], splitLine[i]);
                        }
                    }
                return dict;
            }
        ).Where((dict) => dict.Count != 0)
        .ToList();

        return new Table(header, rows);
    }

}


public static class HtmlConverter
{
    // TODO: Validate that the template(s) are compatible with the table data
    // TODO: Tests
    static string FillTemplate(Table table, string fileTemplate, string rowsTemplate)
    {
        string rows = "";
        
        foreach (var row in table.rows)
        {
            var filledRowsTemplate = rowsTemplate;
            foreach (var column in table.header)
            {
                filledRowsTemplate = filledRowsTemplate.Replace($"{{{{{column}}}}}", row[column]);
            }

            rows += filledRowsTemplate;
        }

        return fileTemplate.Replace("{{{{rows}}}}", rows);
    }
    
}

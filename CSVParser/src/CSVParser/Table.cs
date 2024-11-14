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
        
        var header = lines.First().Split(columnDelimiter, StringSplitOptions.TrimEntries)
            .Where(it => it != "").ToArray();
        var rows = lines.Skip(1).Select(line =>
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                var splitLine = line.Split(columnDelimiter, StringSplitOptions.TrimEntries);
                    for (var i = 0; i < splitLine.Length; i++)
                    {
                        if (splitLine[i] != "") 
                        {
                            dict.Add(header[i], splitLine[i]);
                        }
                    }
                return dict;
            }
        ).Where((dict) => dict.Count != 0).ToList();

        return new Table(header, rows);
    }

}
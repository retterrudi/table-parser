using System.CommandLine;

internal class Programm
{
    internal static async Task Main(string[] args)
    {
        // Set default to empty string for now 
        // Not sure if I need a default here or if it should be nullable
        // TODO: How to implement raw input vs. file input? 
        var inputArgument = new Argument<string?>(
            name: "csv-input",
            description: "Input with CSV-data");

        var inputFileOption = new Option<string>(
            name: "--input-file",
            description: "File with input csv-data");

        var fileTemplate = new Argument<string>(
            name: "file_template",
            description: "Template for the output");

        var rowsTemplate = new Argument<string>(
            name: "rows_template",
            description: "Template for the formated rows");
        
        var outputFileOption = new Option<string>(
            name: "--output",
            description: "Output file name or location");
        
        var rootCommand = new RootCommand();
        rootCommand.Add(inputArgument);
        rootCommand.AddOption(inputFileOption);
        rootCommand.AddOption(outputFileOption);

        rootCommand.SetHandler((inputArgument, inputFileOption, outputFileOption) => 
            {
                Console.WriteLine($"Input Argument: {inputArgument}");
                Console.WriteLine($"Input Option: {inputFileOption}");
                Console.WriteLine($"Output Option: {outputFileOption}");
            }
            , inputArgument, inputFileOption, outputFileOption);
        
        await rootCommand.InvokeAsync(args);
    }
}
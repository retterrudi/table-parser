using System.Data;
using CSVParser.CSVParser;
using Xunit;

namespace CSVParser.tests.CSVParser.Tests;

public class TableTests
{
    [Fact]
    public void TableFromCsv_SimpleCSV_CreateCorrectTable()
    {
        // Arrange
        const string csvText = "Name, Age\nAlice, 30";
        const char delimiter = ',';

        // Act
        var table = Table.TableFromCsv(csvText, delimiter);
        
        // Assert
        Assert.Equal(new[] { "Name", "Age"}, table.header);
        Assert.Single(table.rows);
        var row = table.rows[0];
        Assert.Equal("Alice", row["Name"]);
        Assert.Equal("30", row["Age"]);
    }
    
    [Fact]
    public void TableFromCsv_MultipleRows_CreateCorrectTable()
    {
        const string csvText = "Name,Age,City\nAlice,30 ,London\nBob,25,Paris\nCharlie,35,Berlin";
        const char delimiter = ',';

        var table = Table.TableFromCsv(csvText, delimiter);
        
        Assert.Equal(new[] { "Name", "Age", "City" }, table.header);
        Assert.Equal(3 ,table.rows.Count);
        Assert.Equal("London", table.rows[0]["City"]);
        Assert.Equal("30", table.rows[0]["Age"]);
        Assert.Equal("Paris", table.rows[1]["City"]);
        Assert.Equal("Charlie", table.rows[2]["Name"]);
    }
    
    [Fact]
    public void TableFromCsv_EmptyCsv_CreatesEmptyTable()
    {
        const string csvText = "";
        const char delimiter = ',';

        var table = Table.TableFromCsv(csvText, delimiter);
        
        Assert.Empty(table.header);
        Assert.Empty(table.rows);
    }
    
    [Theory]
    [InlineData("Name, Age, City")]
    [InlineData("Name, Age, City\n")]
    public void TableFromCsv_NoRows_CreatesTableWithoutContent(string csvText)
    {
        const char delimiter = ',';
        
        var table = Table.TableFromCsv(csvText, delimiter);
        
        Assert.NotEmpty(table.header);
        Assert.Empty(table.rows);
    }
}
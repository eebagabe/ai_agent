using AiAgent;

namespace AiAgent.Tests;

public class GetFileContentTests(WorkingDirectoryFixture fixture) : IClassFixture<WorkingDirectoryFixture>
{
    [Fact]
    public void ReadsFile()
    {
        Assert.Equal("wait, this isn't lorem ipsum", Functions.GetFileContent(fixture.Path, "lorem.txt"));
    }

    [Fact]
    public void ReadsFileInSubdirectory()
    {
        Assert.Equal("class Calculator", Functions.GetFileContent(fixture.Path, "pkg/calculator.txt"));
    }

    [Fact]
    public void TruncatesLongFile()
    {
        File.WriteAllText(Path.Combine(fixture.Path, "big.txt"), new string('a', 10001));

        var result = Functions.GetFileContent(fixture.Path, "big.txt");

        Assert.Equal(new string('a', 10000) + "[...File \"big.txt\" truncated at 10000 characters]", result);
    }

    [Fact]
    public void RejectsFileOutsideWorkingDirectory()
    {
        var result = Functions.GetFileContent(fixture.Path, "/bin/cat");

        Assert.Equal("Error: Cannot read \"/bin/cat\" as it is outside the permitted working directory", result);
    }

    [Fact]
    public void RejectsMissingFile()
    {
        var result = Functions.GetFileContent(fixture.Path, "pkg/does_not_exist.py");

        Assert.Equal("Error: File not found or is not a regular file: \"pkg/does_not_exist.py\"", result);
    }
}

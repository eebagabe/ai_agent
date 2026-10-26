using AiAgent;

namespace AiAgent.Tests;

public class GetFilesInfoTests(WorkingDirectoryFixture fixture) : IClassFixture<WorkingDirectoryFixture>
{
    [Fact]
    public void ListsCurrentDirectory()
    {
        var result = Functions.GetFilesInfo(fixture.Path, ".");

        Assert.Contains("- lorem.txt: file_size=28 bytes, is_dir=False", result);
        Assert.Contains("- pkg: file_size=0 bytes, is_dir=True", result);
    }

    [Fact]
    public void ListsSubdirectory()
    {
        var result = Functions.GetFilesInfo(fixture.Path, "pkg");

        Assert.Equal("- calculator.txt: file_size=16 bytes, is_dir=False", result);
    }

    [Theory]
    [InlineData("/bin")]
    [InlineData("../")]
    public void RejectsDirectoryOutsideWorkingDirectory(string directory)
    {
        var result = Functions.GetFilesInfo(fixture.Path, directory);

        Assert.Equal($"Error: Cannot list \"{directory}\" as it is outside the permitted working directory", result);
    }

    [Fact]
    public void RejectsFile()
    {
        var result = Functions.GetFilesInfo(fixture.Path, "lorem.txt");

        Assert.Equal("Error: \"lorem.txt\" is not a directory", result);
    }
}

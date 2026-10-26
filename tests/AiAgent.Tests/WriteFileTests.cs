namespace AiAgent.Tests;

public class WriteFileTests(WorkingDirectoryFixture fixture) : IClassFixture<WorkingDirectoryFixture>
{
    [Fact]
    public void OverwritesFile()
    {
        var result = Functions.WriteFile(fixture.Path, "lorem.txt", "wait, this isn't lorem ipsum");

        Assert.Equal("Successfully wrote to \"lorem.txt\" (28 characters written)", result);
        Assert.Equal("wait, this isn't lorem ipsum", File.ReadAllText(Path.Combine(fixture.Path, "lorem.txt")));
    }

    [Fact]
    public void CreatesMissingDirectories()
    {
        var result = Functions.WriteFile(fixture.Path, "new/morelorem.txt", "lorem ipsum dolor sit amet");

        Assert.Equal("Successfully wrote to \"new/morelorem.txt\" (26 characters written)", result);
        Assert.Equal("lorem ipsum dolor sit amet", File.ReadAllText(Path.Combine(fixture.Path, "new", "morelorem.txt")));
    }

    [Fact]
    public void RejectsFileOutsideWorkingDirectory()
    {
        var result = Functions.WriteFile(fixture.Path, "/tmp/temp.txt", "this should not be allowed");

        Assert.Equal("Error: Cannot write to \"/tmp/temp.txt\" as it is outside the permitted working directory", result);
    }

    [Fact]
    public void RejectsDirectory()
    {
        var result = Functions.WriteFile(fixture.Path, "pkg", "content");

        Assert.Equal("Error: Cannot write to \"pkg\" as it is a directory", result);
    }
}

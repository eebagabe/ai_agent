namespace AiAgent.Tests;

public sealed class WorkingDirectoryFixture : IDisposable
{
    public string Path { get; } = Directory.CreateTempSubdirectory("ai_agent_").FullName;

    public WorkingDirectoryFixture()
    {
        Directory.CreateDirectory(System.IO.Path.Combine(Path, "pkg"));
        File.WriteAllText(System.IO.Path.Combine(Path, "lorem.txt"), "wait, this isn't lorem ipsum");
        File.WriteAllText(System.IO.Path.Combine(Path, "pkg", "calculator.txt"), "class Calculator");
    }

    public void Dispose()
    {
        Directory.Delete(Path, recursive: true);
    }
}

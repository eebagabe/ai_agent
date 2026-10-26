namespace AiAgent;

public static partial class Functions
{
    public static string GetFilesInfo(string workingDirectory, string directory = ".")
    {
        var workingDirectoryAbs = Path.GetFullPath(workingDirectory);

        var targetDir = Path.GetFullPath(Path.Combine(workingDirectoryAbs, directory));

        if (!IsInsideDirectory(workingDirectoryAbs, targetDir))
        {
            return $"Error: Cannot list \"{directory}\" as it is outside the permitted working directory";
        }

        if (!Directory.Exists(targetDir))
        {
            return $"Error: \"{directory}\" is not a directory";
        }

        var lines = new List<string>();

        foreach (var itemPath in Directory.EnumerateFileSystemEntries(targetDir))
        {
            var isDir = Directory.Exists(itemPath);
            var size = isDir ? 0 : new FileInfo(itemPath).Length;

            lines.Add($"- {Path.GetFileName(itemPath)}: file_size={size} bytes, is_dir={isDir}");
        }

        return string.Join("\n", lines);
    }
}

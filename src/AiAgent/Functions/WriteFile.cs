namespace AiAgent;

public static partial class Functions
{
    public static string WriteFile(string workingDirectory, string filePath, string content)
    {
        try
        {
            var baseDir = Path.GetFullPath(workingDirectory);
            var target = Path.GetFullPath(Path.Combine(workingDirectory, filePath));

            if (!IsInsideDirectory(baseDir, target))
            {
                return $"Error: Cannot write to \"{filePath}\" as it is outside the permitted working directory";
            }

            if (Directory.Exists(target))
            {
                return $"Error: Cannot write to \"{filePath}\" as it is a directory";
            }

            Directory.CreateDirectory(Path.GetDirectoryName(target)!);

            File.WriteAllText(target, content);

            return $"Successfully wrote to \"{filePath}\" ({content.Length} characters written)";
        }
        catch (Exception error)
        {
            return $"Error: {error.Message}";
        }
    }
}

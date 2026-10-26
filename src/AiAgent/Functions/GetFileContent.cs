namespace AiAgent;

public static partial class Functions
{
    private const int MaxLengthToRead = 10000;

    public static string GetFileContent(string workingDirectory, string filePath)
    {
        try
        {
            var baseDir = Path.GetFullPath(workingDirectory);
            var target = Path.GetFullPath(Path.Combine(workingDirectory, filePath));

            if (!IsInsideDirectory(baseDir, target))
            {
                return $"Error: Cannot read \"{filePath}\" as it is outside the permitted working directory";
            }

            if (!File.Exists(target))
            {
                return $"Error: File not found or is not a regular file: \"{filePath}\"";
            }

            using var file = new StreamReader(target);
            var buffer = new char[MaxLengthToRead];
            var content = new string(buffer, 0, file.ReadBlock(buffer, 0, MaxLengthToRead));

            if (file.Read() != -1)
            {
                content += $"[...File \"{filePath}\" truncated at {MaxLengthToRead} characters]";
            }

            return content;
        }
        catch (Exception e)
        {
            return $"Error: {e.Message}";
        }
    }
}

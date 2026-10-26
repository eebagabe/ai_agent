namespace AiAgent;

public static partial class Functions
{
    private static bool IsInsideDirectory(string directory, string target)
    {
        return target == directory || target.StartsWith(directory + Path.DirectorySeparatorChar);
    }
}

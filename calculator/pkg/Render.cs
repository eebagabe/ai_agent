using System.Text.Encodings.Web;
using System.Text.Json;

namespace CalculatorApp.Pkg;

public static class Render
{
    public static string FormatJsonOutput(string expression, double result)
    {
        object resultToDump = double.IsInteger(result) ? (long)result : result;

        var outputData = new Dictionary<string, object>
        {
            ["expression"] = expression,
            ["result"] = resultToDump,
        };
        return JsonSerializer.Serialize(outputData, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        });
    }
}

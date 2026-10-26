using CalculatorApp.Pkg;

var calculator = new Calculator();
if (args.Length == 0)
{
    Console.WriteLine("Calculator App");
    Console.WriteLine("Usage: dotnet run -- \"<expression>\"");
    Console.WriteLine("Example: dotnet run -- \"3 + 5\"");
    return;
}

var expression = string.Join(" ", args);
try
{
    var result = calculator.Evaluate(expression);
    if (result is not null)
    {
        var toPrint = Render.FormatJsonOutput(expression, result.Value);
        Console.WriteLine(toPrint);
    }
    else
    {
        Console.WriteLine("Error: Expression is empty or contains only whitespace.");
    }
}
catch (Exception e)
{
    Console.WriteLine($"Error: {e.Message}");
}

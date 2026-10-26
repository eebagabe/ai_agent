using System.Globalization;

namespace CalculatorApp.Pkg;

public class Calculator
{
    private readonly Dictionary<string, Func<double, double, double>> _operators = new()
    {
        ["+"] = (a, b) => a + b,
        ["-"] = (a, b) => a - b,
        ["*"] = (a, b) => a * b,
        ["/"] = (a, b) => b == 0 ? throw new DivideByZeroException("float division by zero") : a / b,
    };

    private readonly Dictionary<string, int> _precedence = new()
    {
        ["+"] = 1,
        ["-"] = 1,
        ["*"] = 2,
        ["/"] = 2,
    };

    public double? Evaluate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
        {
            return null;
        }
        var tokens = expression.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries);
        return EvaluateInfix(tokens);
    }

    private double EvaluateInfix(string[] tokens)
    {
        var values = new Stack<double>();
        var operators = new Stack<string>();

        foreach (var token in tokens)
        {
            if (_operators.ContainsKey(token))
            {
                while (
                    operators.Count > 0
                    && _operators.ContainsKey(operators.Peek())
                    && _precedence[operators.Peek()] >= _precedence[token])
                {
                    ApplyOperator(operators, values);
                }
                operators.Push(token);
            }
            else
            {
                if (!double.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out var value))
                {
                    throw new ArgumentException($"invalid token: {token}");
                }
                values.Push(value);
            }
        }

        while (operators.Count > 0)
        {
            ApplyOperator(operators, values);
        }

        if (values.Count != 1)
        {
            throw new ArgumentException("invalid expression");
        }

        return values.Peek();
    }

    private void ApplyOperator(Stack<string> operators, Stack<double> values)
    {
        if (operators.Count == 0)
        {
            return;
        }

        var op = operators.Pop();
        if (values.Count < 2)
        {
            throw new ArgumentException($"not enough operands for operator {op}");
        }

        var b = values.Pop();
        var a = values.Pop();
        values.Push(_operators[op](a, b));
    }
}

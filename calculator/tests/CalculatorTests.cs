using CalculatorApp.Pkg;

namespace CalculatorApp.Tests;

public class CalculatorTests
{
    private readonly Calculator _calculator = new();

    [Fact]
    public void Addition()
    {
        var result = _calculator.Evaluate("3 + 5");
        Assert.Equal(8, result);
    }

    [Fact]
    public void Subtraction()
    {
        var result = _calculator.Evaluate("10 - 4");
        Assert.Equal(6, result);
    }

    [Fact]
    public void Multiplication()
    {
        var result = _calculator.Evaluate("3 * 4");
        Assert.Equal(12, result);
    }

    [Fact]
    public void Division()
    {
        var result = _calculator.Evaluate("10 / 2");
        Assert.Equal(5, result);
    }

    [Fact]
    public void NestedExpression()
    {
        var result = _calculator.Evaluate("3 * 4 + 5");
        Assert.Equal(17, result);
    }

    [Fact]
    public void ComplexExpression()
    {
        var result = _calculator.Evaluate("2 * 3 - 8 / 2 + 5");
        Assert.Equal(7, result);
    }

    [Fact]
    public void EmptyExpression()
    {
        var result = _calculator.Evaluate("");
        Assert.Null(result);
    }

    [Fact]
    public void InvalidOperator()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate("$ 3 5"));
    }

    [Fact]
    public void NotEnoughOperands()
    {
        Assert.Throws<ArgumentException>(() => _calculator.Evaluate("+ 3"));
    }
}

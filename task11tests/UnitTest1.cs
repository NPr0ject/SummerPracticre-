using task11;

namespace task11tests;

public class UnitTest1
{
    private readonly dynamic _calculator;
    public UnitTest1()
    {
        _calculator = Calculator.test();
    }

    [Fact]
    public void AddReturnsCorrectResult()
    {
        int result = _calculator.Add(10, 5);
        Assert.Equal(15, result);
    }
    [Fact]
    public void MinusReturnsCorrectResult()
    {
        int result = _calculator.Minus(10, 5);
        Assert.Equal(5, result);
    }
    [Fact]
    public void MulReturnsCorrectResult()
    {
        int result = _calculator.Mul(10, 5);
        Assert.Equal(50, result);
    }
    [Fact]
    public void DivReturnsCorrectResult()
    {
        int result = _calculator.Div(10, 5);
        Assert.Equal(2, result);
    }
}

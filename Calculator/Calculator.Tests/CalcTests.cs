using Microsoft.QualityTools.Testing.Fakes;
using Xunit;

namespace Calculator.Tests;
public class CalcTests
{
    [Fact]
    public void Sum_3_and_4_results_7()
    {
        //Arrange
        var calc = new Calc();

        //Act
        var result = calc.Sum(3, 4);

        //Assert
        Assert.Equal(7, result);
    }

    [Fact]
    public void Sum_0_and_0_results_0()
    {
        //Arrange
        var calc = new Calc();

        //Act
        var result = calc.Sum(0, 0);

        //Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Sum_MAX_and_1_results_throws()
    {
        //Arrange
        var calc = new Calc();

        //Act
        Assert.Throws<OverflowException>(() => calc.Sum(int.MaxValue, 1));

        Assert.Throws<OverflowException>(() => calc.Sum(int.MinValue, -1));
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 0, 1)]
    [InlineData(0, 1, 1)]
    [InlineData(5, 12, 17)]
    [InlineData(-5, 12, 7)]
    public void Sum(int a, int b, int exp)
    {
        var calc = new Calc();

        var result = calc.Sum(a, b);

        Assert.Equal(exp, result);
    }


    [Fact]
    public void IsWeekend()
    {
        var calc = new Calc();

        using (ShimsContext.Create())
        {
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 9, 6);
            Assert.False(calc.IsWeekend());
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 9, 7);
            Assert.False(calc.IsWeekend());
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 9, 8);
            Assert.False(calc.IsWeekend());
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 9, 9);
            Assert.False(calc.IsWeekend());
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 9, 10);
            Assert.False(calc.IsWeekend());
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 9, 11);
            Assert.True(calc.IsWeekend());
            System.Fakes.ShimDateTime.NowGet = () => new DateTime(2021, 9, 12);
            Assert.True(calc.IsWeekend());
        }
    }

}
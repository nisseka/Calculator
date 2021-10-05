using System;
using Xunit;
using Calculator;

namespace Calculator.Test
{
    public class CalculatorTests
    {
	[Fact]
	public void DivideByZeroTest()
	{
	    // Arrange
	    double num1 = 16;
	    double num2 = 0;
	    double result = 0;
	    string resultTitle = "";

	    // Act
	    bool success = Program.Division(num1, num2, ref result, ref resultTitle);

	    // Assert
	    Assert.False(success);
	}

	[Fact]
	public void SquareRootFromNegativeNumberTest()
	{
	    // Arrange
	    double num1 = -12;
	    double result = 0;
	    string resultTitle = "";

	    // Act
	    bool success = Program.SquareRoot(num1, ref result, ref resultTitle);

	    // Assert
	    Assert.False(success);
	}

	[Theory]
	[InlineData(1, 2, 3)]
	public void AdditionTest(double num1, double num2, double expectedResult)
	{
	    // Arrange
	    string resultTitle;

	    // Act
	    double result = Program.Addition(num1, num2, out resultTitle);

	    // Assert
	    Assert.Equal(expectedResult, result, 3);
	}

	[Theory]
	[InlineData(new double[] {8,25},33, true)]
	[InlineData(new double[] { 8 }, 8, true)]
	[InlineData(new double[] {  }, 0, false)]	    // Test with an empty array of numbers
	public void AdditionArrayOfValuesTest(double[] values, double expectedResult, bool expectedSuccess)
	{
	    // Arrange
	    string resultTitle;
	    double result=0;
	    bool success;

	    // Act
	    try
	    {
		result = Program.Addition(values, out resultTitle);
		success = true;
	    }
	    catch (Exception e)
	    {
		success = false;
	    }

	    // Assert
	    Assert.Equal(expectedSuccess, success);
	    Assert.Equal(expectedResult, result, 3);
	}

	[Theory]
	[InlineData(new double[] { 8, 25 }, -17, true)]
	[InlineData(new double[] { }, 0, false)]            // Test with an empty array of numbers
	public void SubtractionArrayOfValuesTest(double[] values, double expectedResult, bool expectedSuccess)
	{
	    // Arrange
	    string resultTitle;
	    double result = 0;
	    bool success;

	    // Act
	    try
	    {
		result = Program.Subtraction(values, out resultTitle);
		success = true;
	    }
	    catch (Exception e)
	    {
		success = false;
	    }

	    // Assert
	    Assert.Equal(expectedSuccess, success);
	    Assert.Equal(expectedResult, result, 3);
	}

	[Theory]
	[InlineData(1, 2, -1)]
	public void SubtractionTest(double num1, double num2, double expectedResult)
	{
	    // Arrange
	    string resultTitle;

	    // Act
	    double result = Program.Subtraction(num1, num2, out resultTitle);

	    // Assert
	    Assert.Equal(expectedResult, result, 3);
	}

	[Theory]
	[InlineData(5, 35, 175)]
	public void MultiplicationTest(double num1, double num2, double expectedResult)
	{
	    // Arrange
	    string resultTitle;

	    // Act
	    double result = Program.Multiplication(num1, num2, out resultTitle);

	    // Assert
	    Assert.Equal(expectedResult, result, 3);
	}

	[Theory]
	[InlineData(12, 3, 4,true)]
	[InlineData(23, 5, 4.6, true)]
	public void DivisionTest(double num1, double num2, double expectedResult, bool expectedSuccess)
	{
	    // Arrange
	    double result = 0;
	    string resultTitle = "";

	    // Act
	    bool success = Program.Division(num1, num2, ref result, ref resultTitle);

	    // Assert
	    Assert.Equal(expectedSuccess, success);
	    Assert.Equal(expectedResult, result, 3);
	}

	[Theory]
	[InlineData(0, 0)]
	[InlineData(45, 0.707)]
	[InlineData(90, 1)]
	public void SinusTest(double angle, double expectedResult)
	{
	    // Arrange
	    string resultTitle;

	    // Act
	    double result = Program.Sinus(angle, out resultTitle);

	    // Assert
	    Assert.Equal(expectedResult, result,3);
	}

	[Theory]
	[InlineData(0, 1)]
	[InlineData(45, 0.707)]
	[InlineData(90, 0)]
	public void CosinusTest(double angle, double expectedResult)
	{
	    // Arrange
	    string resultTitle;

	    // Act
	    double result = Program.Cosinus(angle, out resultTitle);

	    // Assert
	    Assert.Equal(expectedResult, result, 3);
	}

	[Theory]
	[InlineData(4, 2, true)]
	[InlineData(2, 1.414, true)]
	[InlineData(-58, 0, false)]		// Test Square Root of a negative number
	public void SquareRootTest(double value, double expectedResult, bool expectedSuccess)
	{
	    // Arrange
	    double result = 0;
	    string resultTitle = "";

	    // Act
	    bool success = Program.SquareRoot(value,ref result,ref resultTitle);

	    // Assert
	    Assert.Equal(expectedSuccess,success);
	    Assert.Equal(expectedResult, result, 3);
	}

	[Theory]
	[InlineData(2, 3, 8)]
	public void PowTest(double num1, double num2, double expectedResult)
	{
	    // Arrange
	    string resultTitle;

	    // Act
	    double result = Program.Pow(num1, num2, out resultTitle);

	    // Assert
	    Assert.Equal(expectedResult, result, 3);
	}

	[Theory]
	[InlineData(100, 2)]
	[InlineData(25, 1.398)]
	public void LogTest(double value, double expectedResult)
	{
	    // Arrange
	    string resultTitle;

	    // Act
	    double result = Program.Log(value, out resultTitle);

	    // Assert
	    Assert.Equal(expectedResult, result, 3);
	}

    }

}

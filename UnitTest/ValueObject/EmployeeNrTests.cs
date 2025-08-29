using Domain.ValueObject;
using Shouldly;
using System;
using Xunit;

namespace Application.UnitTest.ValueObject;

public class EmployeeNrTests
{
    [Theory]
    [InlineData("12345678")]
    [InlineData("00000001")]
    public void Constructor_WithValidValue_ShouldCreateObject(string value)
    {
        // Arrange & Act
        var employeeNr = new EmployeeNr(value);

        // Assert
        employeeNr.ShouldNotBeNull();
        employeeNr.ToString().ShouldBe(value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("1234567")] // Too short
    [InlineData("123456789")] // Too long
    public void Constructor_WithInvalidValue_ShouldThrowArgumentException(string value)
    {
        // Arrange & Act & Assert
        Should.Throw<ArgumentException>(() => new EmployeeNr(value));
    }
}
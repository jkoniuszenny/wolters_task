using Application.UnitTest.Fixtures;
using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObject;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Entities;

public class EmployeeTests
{

    [Fact]
    public async Task Constructor_WithValidArguments_ShouldCreateEmployee()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var employeeNr = new EmployeeNr("00000005");
        var lastName = new LastName("Nowak");
        var sex = new Sex(Gender.Male);

        // Act
        var employee = new Employee(id, employeeNr, lastName, sex);

        // Assert
        employee.ShouldNotBeNull();
        employee.Id.ShouldBe(id);
        employee.EmployeeNr.ShouldBe(employeeNr);
        employee.LastName.ShouldBe(lastName);
        employee.Sex.ShouldBe(sex);
    }

    [Fact]
    public void UpdateInfo_WithNewValidData_ShouldUpdateProperties()
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var employeeNr = new EmployeeNr("00000005");
        var lastName = new LastName("Nowak");
        var sex = new Sex(Gender.Male);

        var employee = new Employee(id, employeeNr, lastName, sex);

        var newLastName = new LastName("Kowalski");
        var newSex = new Sex(Gender.Male);

        // Act
        employee.UpdateInfo(newLastName, newSex);

        // Assert
        employee.LastName.ShouldBe(newLastName);
        employee.Sex.ShouldBe(newSex);
    }


    [Theory]
    [InlineData(null, "Kowalski", "Male")]
    [InlineData("00000005", null, "Male")]
    [InlineData("00000099", "Nowak", null)]
    public void Constructor_WithNullArgument_ShouldThrowArgumentNullException(string employeeNrInput, string lastNameInput, string sexInput)
    {
        // Arrange
        var id = Guid.NewGuid().ToString();
        var employeeNr = employeeNrInput!= null ? new EmployeeNr(employeeNrInput) : null;
        var lastName = lastNameInput != null ? new LastName(lastNameInput) : null;
        var sex = sexInput != null ? new Sex((Gender)Enum.Parse(typeof(Gender), sexInput)) : null;

        // Act & Assert
        Should.Throw<ArgumentNullException>(() =>
      new Employee(
          id: id,
          employeeNr: employeeNr!,
          lastName: lastName!,
          sex: sex!
      )
  );
    }


    [Theory]
    [InlineData(null, "Male")]
    [InlineData("Nowak", null)]
    public void UpdateInfo_WithNullArgument_ShouldThrowArgumentNullException(
    string newLastName, string newSex)
    {
        var id = Guid.NewGuid().ToString();
        var employeeNr = new EmployeeNr("00000005");
        var lastName = new LastName("Nowak");
        var sex = new Sex(Gender.Male);

        var employee = new Employee(id, employeeNr, lastName, sex);

        var newLastNameObject = newLastName != null ? new LastName(newLastName) : null;
        var newSexObject = newSex != null ? new Sex((Gender)Enum.Parse(typeof(Gender), newSex)) : null;


        // Act & Assert
        Should.Throw<ArgumentNullException>(() =>
        employee.UpdateInfo(
            newLastNameObject!,
            newSexObject!
        )
    );

    }
}

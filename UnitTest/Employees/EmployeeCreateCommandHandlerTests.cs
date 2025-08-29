using Application.CQRS.Employees.Commands.Create;
using Application.UnitTest.Fixtures;
using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObject;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Employees;

public class EmployeeCreateCommandHandlerTests
{
    private readonly EmployeesFixture _fixture = new();
    

    [Fact]
    public async Task EmployeeCreateCommandHandlerTests_Return_WithoutError()
    {
        // Arrange
        var data = _fixture.Build<EmployeeCreateData>()
            .With(w => w.LastName, "Nowak")
            .With(w=>w.Gender, Gender.Male)
            .Create();

        var requestValue = new EmployeeCreateInput(
           LastName: new LastName(data.LastName),
           Sex: new Sex(data.Gender)
           );

        var command = new EmployeeCreateCommand(requestValue);

        var employeeFromGenerate = new EmployeeNr("00000005");
            
        _fixture.GenerateNr(employeeFromGenerate);
        var repoMock = _fixture.RepositoryMock();

        var handler = _fixture.Create<EmployeeCreateCommandHandler>();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        repoMock.Verify(v => v.Insert(It.IsAny<Employee>()), Times.Once);
        result.Succeded.ShouldBeTrue();

    }


    [Fact]
    public async Task EmployeeCreateCommandHandlerTests_Return_VerifyEmployeeInsert()
    {
        // Arrange
        var data = _fixture.Build<EmployeeCreateData>()
            .With(w => w.LastName, "Nowak")
            .With(w => w.Gender, Gender.Male)
            .Create();

        var requestValue = new EmployeeCreateInput(
           LastName: new LastName(data.LastName),
           Sex: new Sex(data.Gender)
           );

        var command = new EmployeeCreateCommand(requestValue);

        var employeeFromGenerate = new EmployeeNr("00000005");

        _fixture.GenerateNr(employeeFromGenerate);

        var repoMock = _fixture.RepositoryMock();

        var handler = _fixture.Create<EmployeeCreateCommandHandler>();

        // Act 
        await handler.Handle(command, CancellationToken.None);


        // Assert
        repoMock.Verify(r => r.Insert(It.Is<Employee>(e =>
            e.EmployeeNr.ToString() == employeeFromGenerate.ToString() &&
            e.LastName.ToString() == command.LastName.ToString() &&
            e.Sex.ToString() == command.Sex.ToString() 
        )), Times.Once);

    }

    [Fact]
    public async Task EmployeeCreateCommandHandlerTests_Return_ErrorFromInsert()
    {
        // Arrange
        var data = _fixture.Build<EmployeeCreateData>()
            .With(w => w.LastName, "Nowak")
            .With(w => w.Gender, Gender.Male)
            .Create();

        var requestValue = new EmployeeCreateInput(
           LastName: new LastName(data.LastName),
           Sex: new Sex(data.Gender)
           );

        var employeeFromGenerate = new EmployeeNr("00000005");

        _fixture.GenerateNr(employeeFromGenerate);

        var command = new EmployeeCreateCommand(requestValue);

        _fixture.RepositoryEmployeeInsertThrow();

        var handler = _fixture.Create<EmployeeCreateCommandHandler>();

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, CancellationToken.None));

    }

    [Fact]
    public async Task EmployeeCreateCommandHandlerTests_Return_ErrorFromGenerateNr()
    {
        // Arrange
        var data = _fixture.Build<EmployeeCreateData>()
            .With(w => w.LastName, "Nowak")
            .With(w => w.Gender, Gender.Male)
            .Create();

        var requestValue = new EmployeeCreateInput(
           LastName: new LastName(data.LastName),
           Sex: new Sex(data.Gender)
           );


        var command = new EmployeeCreateCommand(requestValue);

        _fixture.GenerateNrThrow();

        var handler = _fixture.Create<EmployeeCreateCommandHandler>();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));

    }
}

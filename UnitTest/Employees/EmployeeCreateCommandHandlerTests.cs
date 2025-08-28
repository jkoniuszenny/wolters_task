using Application.CQRS.Employees.Commands.Create;
using Application.Interfaces.Repositories;
using Application.UnitTest.Fixtures;
using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObject;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Employees;

public class EmployeeCreateCommandHandlerTests
{
    private readonly EmployeesFixture _fixture = new();
    private readonly Mock<IAsyncRepository> _repositoryMock;

    public EmployeeCreateCommandHandlerTests()
    {
        _repositoryMock = new Mock<IAsyncRepository>();
    }

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

        var handler = _fixture.Create<EmployeeCreateCommandHandler>();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        _fixture.RepositoryMock().Verify(v => v.Insert(It.IsAny<Employee>()), Times.Once);
        result.Succeded.ShouldBeTrue();

    }

}

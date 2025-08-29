using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoFixture;
using Domain.Entities;
using Domain.ValueObject;
using Moq;
using System;

namespace Application.UnitTest.Fixtures;

public class EmployeesFixture : BaseFixture
{

    public void GenerateNr(EmployeeNr output) =>
        this.Freeze<Mock<IEmployeeNrGeneratorService>>()
        .Setup(s => s.GenerateNr())
        .ReturnsAsync(output);
        
    public void GenerateNrThrow() =>
        this.Freeze<Mock<IEmployeeNrGeneratorService>>()
        .Setup(s => s.GenerateNr())
        .ThrowsAsync(new InvalidOperationException());

    public void RepositoryEmployeeInsertThrow() =>
        this.Freeze<Mock<IAsyncRepository>>()
        .Setup(s => s.Insert(It.IsAny<Employee>()))
        .ThrowsAsync(new Exception());

    public Mock<IAsyncRepository> RepositoryMock() =>
        this.Freeze<Mock<IAsyncRepository>>();
}


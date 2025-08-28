using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using AutoFixture;
using AutoFixture.AutoMoq;
using Domain.ValueObject;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Application.UnitTest.Fixtures;

public class EmployeesFixture : BaseFixture
{

    public void GenerateNr(EmployeeNr output) =>
        this.Freeze<Mock<IEmployeeNrGeneratorService>>()
        .Setup(s => s.GenerateNr())
        .ReturnsAsync(output);

    public Mock<IAsyncRepository> RepositoryMock() =>
        this.Freeze<Mock<IAsyncRepository>>();
}


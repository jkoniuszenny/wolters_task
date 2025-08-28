using Domain.ValueObject;

namespace Application.Interfaces.Services;

public interface IEmployeeNrGeneratorService : IService
{
    Task<EmployeeNr> GenerateNr();
}

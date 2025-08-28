using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.CQRS.Employees.Commands.Create;

internal sealed class EmployeeCreateCommandHandler(
    IAsyncRepository repository,
    IEmployeeNrGeneratorService employeeNrGeneratorService) : IRequestHandler<EmployeeCreateCommand, GlobalResponse>
{
    public async Task<GlobalResponse> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid().ToString();

        var employeeNr = await employeeNrGeneratorService.GenerateNr();

        var employee = new Employee(
            id: id,
            employeeNr: employeeNr,
            lastName: request.LastName,
            sex: request.Sex
            );

        await  repository.Insert(employee);

        return await GlobalResponse.SuccessAsync();
    }
}

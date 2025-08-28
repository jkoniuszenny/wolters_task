using Application.Interfaces.Repositories;
using Domain.Entities;
using Shared.Settings;
using System.Xml.Linq;

namespace Application.CQRS.Employees.Commands.Create;

internal sealed class EmployeeCreateCommandHandler : IRequestHandler<EmployeeCreateCommand, GlobalResponse>
{
    private readonly IAsyncRepository _repository;

    public EmployeeCreateCommandHandler(
        IAsyncRepository repository)
    {
        _repository = repository;
    }

    public async Task<GlobalResponse> Handle(EmployeeCreateCommand request, CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid().ToString();


        var employeeNr = "";


        var employee = new Employee(
            id: id,
            employeeNr: new EmployeeNr(employeeNr),
            lastName: request.LastName,
            sex: request.Sex
            );



        var number = _numberGenerator.Next();
        var employee = new Employee(number, name, email);

        _repository.Add(employee);

        return employee;

        return await GlobalResponse.SuccessAsync();
    }
}

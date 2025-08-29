using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.ValueObject;

namespace Application.CQRS.Employees.Commands.Update;

internal sealed class EmployeeUpdateCommandHandler(
    IAsyncRepository repository) : IRequestHandler<EmployeeUpdateCommand, GlobalResponse>
{
    public async Task<GlobalResponse> Handle(EmployeeUpdateCommand request, CancellationToken cancellationToken)
    {

        var newLastName = new LastName(request.LastName.ToString());
        var newGender = request.Sex;

        var existedEmployee = await repository.Select<Employee>(a => a.Id == request.EmployeeId) 
            ?? throw new InvalidOperationException($"Pracownik o ID '{request.EmployeeId}' nie znaleziony.");

        existedEmployee.UpdateInfo(newLastName, newGender);

        await repository.Update(existedEmployee);

        return await GlobalResponse.SuccessAsync();
    }
}

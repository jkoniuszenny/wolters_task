using Application.CQRS.Employee.Commands.Create;

namespace Application.CQRS.Employees.Commands.Update;

public record EmployeeUpdateCommand : EmployeeUpdateInput, IRequest<GlobalResponse>
{
    public EmployeeUpdateCommand(EmployeeUpdateInput input) : base(input) { }
}

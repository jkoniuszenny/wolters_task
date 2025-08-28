namespace Application.CQRS.Employees.Commands.Create;

public record EmployeeCreateCommand : EmployeeCreateInput, IRequest<GlobalResponse>
{
    public EmployeeCreateCommand(EmployeeCreateInput input) : base(input) { }
}

using Domain.Enums;
using Domain.ValueObject;

namespace Application.CQRS.Employees.Commands.Create;


public record EmployeeCreateData(string LastName , Gender Gender);


public record EmployeeCreateInput(LastName LastName, Sex Sex);
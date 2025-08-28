using Domain.Enums;
using Domain.ValueObject;

namespace Application.CQRS.Employees.Commands.Update;

public record EmployeeUpdateData(string LastName, Gender Sex);

public record EmployeeUpdateInput(string EmployeeId, LastName LastName, Sex Sex);
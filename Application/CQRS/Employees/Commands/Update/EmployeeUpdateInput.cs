using Shared.Enums;

namespace Application.CQRS.Employees.Commands.Update;

public record EmployeeUpdateData
{
    public string LastName { get; init; }
    public Gender Sex { get; init; }

    public EmployeeUpdateData(string lastName, Gender sex)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Nazwisko nie może być puste.", nameof(lastName));
        }

        if (!Enum.IsDefined(typeof(Gender), sex))
        {
            throw new ArgumentException("Nieprawidłowa wartość dla płci.", nameof(sex));
        }

        LastName = lastName;
        Sex = sex;
    }
}


public record EmployeeUpdateInput
{
    public string EmployeeId { get; init; }
    public string LastName { get; init; }
    public Gender Sex { get; init; }

    public EmployeeUpdateInput(string employeeId, string lastName, Gender sex)
    {
        if (string.IsNullOrWhiteSpace(employeeId))
        {
            throw new ArgumentException("Employee ID cannot be null or whitespace.", nameof(employeeId));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be null or whitespace.", nameof(lastName));
        }

        if (!Enum.IsDefined(typeof(Gender), sex))
        {
            throw new ArgumentException("Invalid value for gender.", nameof(sex));
        }

        EmployeeId = employeeId;
        LastName = lastName;
        Sex = sex;
    }
}
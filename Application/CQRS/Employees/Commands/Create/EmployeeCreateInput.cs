using Shared.Enums;

namespace Application.CQRS.Employees.Commands.Create;

public record EmployeeCreateInput
{
    public string LastName { get; init; }
    public Gender Sex { get; init; }

    public EmployeeCreateInput(string lastName, Gender sex)
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
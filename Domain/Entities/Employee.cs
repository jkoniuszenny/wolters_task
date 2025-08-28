using Shared.Enums;

namespace Domain.Entities;

public class Employee(string id, EmployeeNr employeeNr, string lastName, Gender sex) 
{
    public string Id { get; } = id;
    public EmployeeNr EmployeeNr { get; } = employeeNr;
    public string LastName { get; } = lastName;
    public Gender Sex { get; } = sex;
}


public record EmployeeNr
{
    private readonly string _value;

    public EmployeeNr(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 8)
        {
            throw new ArgumentException("Błędny format EmployeeNr");
        }
        _value = value;
    }

    public static implicit operator string(EmployeeNr employeeNr) => employeeNr._value;
    public override string ToString() => _value;
}
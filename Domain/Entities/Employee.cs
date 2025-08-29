using Domain.ValueObject;

namespace Domain.Entities;

public class Employee
{
    public string Id { get; init; }
    public EmployeeNr EmployeeNr { get; init; }
    public LastName LastName { get; private set; }
    public Sex Sex { get; private set; }

    public Employee(string id, EmployeeNr employeeNr, LastName lastName, Sex sex)
    {
        Id = id;
        EmployeeNr = employeeNr ?? throw new ArgumentNullException(nameof(employeeNr));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        Sex = sex ?? throw new ArgumentNullException(nameof(sex));
    }

    public void UpdateInfo(LastName newLastName, Sex newSex)
    {
        LastName = newLastName ?? throw new ArgumentNullException(nameof(newLastName));
        Sex = newSex ?? throw new ArgumentNullException(nameof(newSex));
    }
}
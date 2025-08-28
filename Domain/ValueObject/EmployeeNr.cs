namespace Domain.ValueObject;

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

    public static EmployeeNr FormatNr(long number) => new(number.ToString("D8"));

    public override string ToString() => _value;
}

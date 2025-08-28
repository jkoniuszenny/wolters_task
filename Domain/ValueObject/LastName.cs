namespace Domain.ValueObject;

public record LastName
{
    private readonly string _value;

    public LastName(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > 50)
        {
            throw new ArgumentException("Nazwisko musi mieć od 1 do 50 znaków.");
        }

        _value = value;
    }


    public override string ToString() => _value;
}

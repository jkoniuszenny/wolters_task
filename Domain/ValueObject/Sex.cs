using Domain.Enums;

namespace Domain.ValueObject;

public record Sex
{
    private readonly Gender _value;

    public Sex(Gender value)
    {
        if (!Enum.IsDefined(typeof(Gender), value))
        {
            throw new ArgumentException("Nieprawidłowa wartość dla płci.");
        }

        _value = value;
    }

    public override string ToString() => _value.ToString();
}

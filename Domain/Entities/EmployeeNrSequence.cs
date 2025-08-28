using Domain.ValueObject;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class EmployeeNrSequence
{
    public long LastNumber { get; private set; }

    [Timestamp]
    public byte[] RowVersion { get; private set; }

    public EmployeeNr Next()
    {
        LastNumber++;
        return EmployeeNr.FormatNr(LastNumber);
    }
}

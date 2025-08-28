using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.ValueObject;

namespace Infrastructure.Service;

public class EmployeeNrGeneratorService(
    IAsyncRepository repository) : IEmployeeNrGeneratorService
{
    public async Task<EmployeeNr> GenerateNr()
    {

        //RowVersion zapewnia, że podbita liczba zostanie zapisana
        var lastNumber = await repository.Select<EmployeeNrSequence>(a => true);

        var nextValue = lastNumber.Next();

        //W przypadku błędu z zapisu zmiany należałoby dodać ponowienie, żeby uzyskać kolejny numer
        await repository.Update(lastNumber);

        return nextValue;
    }
}

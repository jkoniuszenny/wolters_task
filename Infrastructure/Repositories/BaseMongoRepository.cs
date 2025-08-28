using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObject;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BaseMongoRepository: IAsyncRepository
{

    public BaseMongoRepository()
    {
    }



    public async Task Insert<T>(T entity) where T : class
    {
        await Task.CompletedTask;
    }

    public async Task<T> Select<T>(Expression<Func<T, bool>> filters) where T : class
    {
        await Task.CompletedTask;

        object? result = typeof(T) switch
        {
            Type t when t == typeof(Employee) => new Employee(
                id: Guid.NewGuid().ToString(),
                employeeNr: new EmployeeNr("00000005"),
                lastName: new LastName("Nowak"),
                sex: new Sex(Gender.Male)
            ),
            Type t when t == typeof(EmployeeNrSequence) => new EmployeeNrSequence(),
            _ => null
        };

        return (T)result!;
    }


    public async Task Update<T>(T entity) where T : class
    {
        await Task.CompletedTask;
    }

}

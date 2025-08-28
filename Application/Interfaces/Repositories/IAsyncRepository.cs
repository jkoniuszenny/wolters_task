using System.Linq.Expressions;

namespace Application.Interfaces.Repositories;

public interface IAsyncRepository : IRepository
{
    Task Insert<T>(T entity) where T : class;
    Task<T> Select<T>(Expression<Func<T, bool>> filters) where T : class;
    Task Update<T>(T entity) where T : class;
}


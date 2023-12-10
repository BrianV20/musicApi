using musicApi2.Models.User;
using System.Linq.Expressions;

namespace musicApi2.Services
{
    public interface IEntityInterface<T>
    {
        Task Add(T entity);

        Task Update(T entity);

        Task Delete(T entity);

        Task<T> GetOne(Expression<Func<T, bool>>? filter = null);

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? filter = null);

        Task Save();
    }
}

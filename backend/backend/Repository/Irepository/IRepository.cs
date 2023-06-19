using System.Linq.Expressions;

namespace backend.Repository.Irepository
{
    public interface IRepository<T> where T : class
    {

        Task Save();
        Task Create(T entity);

        Task<T> Get(Expression<Func<T, bool>> filter);
    }
}

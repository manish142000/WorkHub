using backend.Models;

namespace backend.Repository.Irepository
{
    public interface IUserRepository : IRepository<User> 
    {
        Task Update(User entity);
    }
}

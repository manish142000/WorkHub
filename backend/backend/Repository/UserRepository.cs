using backend.Data;
using backend.Models;
using backend.Repository.Irepository;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace backend.Repository
{
    public class UserRepository : Repository<User>, IUserRepository 
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public async Task Update( User entity )
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}

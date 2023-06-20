using backend.Data;
using backend.Models;
using backend.Repository.Irepository;

namespace backend.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _dbOrder;
        public OrderRepository( ApplicationDbContext dbOrder ) : base( dbOrder ) 
        {
            _dbOrder = dbOrder;
        }
    }
}

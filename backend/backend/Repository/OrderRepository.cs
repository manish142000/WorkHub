using backend.Data;
using backend.Models;
using backend.Repository.Irepository;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _dbOrder;
        public OrderRepository( ApplicationDbContext dbOrder ) : base( dbOrder ) 
        {
            _dbOrder = dbOrder;
        }

        public async Task<IEnumerable<Order>> ByOrder( Boolean order, string email)
        {
            IEnumerable<Order> list;
            if( order)
            {
                list = _dbOrder.Orders.Where( u => u.UserEmail == email ).OrderBy(f => f.DateCreated);
            }
            else
            {
                list = _dbOrder.Orders.Where(u => u.UserEmail == email).OrderByDescending(f => f.DateCreated);
            }

            return list;
        }
    }
}

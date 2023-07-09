using backend.Data;
using backend.Models;
using backend.Repository.Irepository;
using Microsoft.EntityFrameworkCore;
using backend.Logging;

namespace backend.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _dbOrder;

        private readonly Ilogging _logger;
        public OrderRepository( ApplicationDbContext dbOrder, Ilogging logger ) : base( dbOrder ) 
        {
            _dbOrder = dbOrder;
            _logger = logger;
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

        public async Task<IEnumerable<Order>> ByStartDate(IEnumerable<Order> obj, string start)
        {
            IEnumerable<Order> list;

            DateTime d;

            if (DateTime.TryParseExact(start, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d))
            {
                list = obj.Where(u => u.DateCreated >= d).OrderBy(f => f.DateCreated);
            }
            else
            {
                list = obj;
                _logger.Log("ye ho rha ab kya!", "");
            }
            

            return list;
        }

        public async Task<IEnumerable<Order>> ByEndDate(IEnumerable<Order> obj, string end)
        {
            IEnumerable<Order> list;
            DateTime d;
            if (DateTime.TryParseExact(end, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out d))
            {
                _logger.Log(d.ToString() + "Here it is!", "");
                list = obj.Where(u => u.DateCreated <= d).OrderBy(f => f.DateCreated);
                if( list.Any())
                {
                    _logger.Log("Entries aa rhi", "");
                }
                else
                {
                    _logger.Log("Nahi aa rhi entries", "");
                }
            }
            else
            {
                list = obj;
            }

            return list;
        }
    }
}

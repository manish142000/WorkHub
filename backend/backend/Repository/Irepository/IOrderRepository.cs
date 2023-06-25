using backend.Models;

namespace backend.Repository.Irepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> ByOrder( Boolean order, string email);
    }
}

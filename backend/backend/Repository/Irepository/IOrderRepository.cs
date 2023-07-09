using backend.Models;

namespace backend.Repository.Irepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> ByOrder( Boolean order, string email);

        Task<IEnumerable<Order>> ByStartDate(IEnumerable<Order> obj, string date);

        Task<IEnumerable<Order>> ByEndDate(IEnumerable<Order> obj, string end);
    }
}

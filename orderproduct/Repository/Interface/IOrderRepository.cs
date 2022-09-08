using orderproduct.Model;

namespace orderproduct.Repository.Interface
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> Getorders();
        public Task<Order> Getorderbyid(int id);
    }
}

using orderproduct.Model;

namespace orderproduct.Repository.Interface
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<Order>> Getorders();
        public Task<Order> Getorderbyid(int id);
        public Task<int> Insertorder(Order order);
        public Task<double> AddProduct(List<Product> products, int cid);
        public Task<int> Updateorder(Order order);
        public Task<int> Deleteorder(int id);
    }
}

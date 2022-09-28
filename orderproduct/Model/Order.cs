using orderproduct.Model;

namespace orderproduct.Model
{
    public class Order
    {
        public int oid { get; set; }
        public string cname { get; set; }
        public string shippingaddress { get; set; }
        public DateTime odate { get; set; }
        
        public List<Product> products { get; set; }
        public double finalamt { get; set; }
    }
}


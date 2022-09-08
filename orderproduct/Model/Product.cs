using System.Security.Cryptography;

namespace orderproduct.Model
{
    public class Product
    {
        public int oid { get; set; }
        public int pid { get; set; }
        public string pname { get; set; }
        public int qty { get; set; }       
        public double price { get; set; }
        public double totalamt { get; set; }

    }
}
  
using Dapper;
using orderproduct.Context;
using orderproduct.Model;
using orderproduct.Repository.Interface;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace orderproduct.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DapperContext _context;

        public OrderRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> Getorders()
        {
            var query = "select * from torder";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<Order>(query);

                foreach (var order in result)
                {

                    var result1 = await connection.QueryAsync<Product>("select * from product where oid =@oid", new {oid=order.oid });
                    order.products = result1.ToList();
                }
                return result;
            }
        }
        /*public async Task<IEnumerable<Order>> Getorders()
{
    List<Order> ordlist = new List<Order>();
    var query = "select * from torder";
    using (var connection = _context.CreateConnection())
    {
        var result = await connection.QueryAsync<Order>(query);
        ordlist = result.ToList();
        foreach (var order in result)
        {
            var result1 = await connection.QueryAsync<Product>("select * from product");
            order.products = result1.ToList();
        }
        return ordlist;
    }
}*/

        public async Task<Order> Getorderbyid(int id)
        {
            var query = "Select * from torder where @oid=oid";
            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Order>(query, new { @oid = id });

                if (result != null)
                {
                    var result1 = await connection.QueryAsync<Product>("select * from product where @oid=oid", new { @oid = id });
                }
                return result;
            }
        }

        /*public async Task<int> InsertOrder(Order order)
        {
            var qry = @"insert into tblOrder(custName,shippingAddress,orderDate,finalAmount)
                        values(@custName,@shippingAddress,@orderDate,@finalAmount);
                             SELECT CAST(SCOPE_IDENTITY() as int)";
            using (var connection = _context.CreateConnection())
            {
                var res = await connection.QuerySingleAsync<int>(qry, order);
                if (res != 0)
                {
                    double fa = await AddProduct(order.products, res);
                    await connection.ExecuteAsync("update tblOrder set finalAmount=@finalAmount where orderId=@id", new { finalAmount = fa, id = res });
                }
                return res;
            }

        }*/
        public async Task<int> Insertorder(Order order)
        {
            var query = @"insert into torder (cname,shippingaddress,odate,finalamt) values (@cname,@shippingaddress,@odate,@finalamt); SELECT CAST(SCOPE_IDENTITY() AS int)";
            using(var connection =_context.CreateConnection())
            {
                var result = await connection.QuerySingleAsync<int>(query,order);
                if(result != null)
                {
                    double result1 = await AddProduct(order.products, result);
                    await connection.ExecuteAsync("update torder set finalamt =@finalamt where oid=@oid", new { finalamt = result1, oid = result });
                }
                return result;
            }
         
        }
        public async Task<double> AddProduct(List<Product> products, int cid)
        {
            int res = 0;
            double grandtotal = 0;
            using (var connection = _context.CreateConnection())
            {
                if (products.Count > 0)
                {

                    foreach (var product in products)
                    {
                        product.oid = cid;
                        product.totalamt = product.price * product.qty;

                        var qry = @"insert into product(oid,pname,qty,price,totalamt)
                            values(@oid,@pname,@qty,@price,@totalamt)";
                        var res1 = await connection.ExecuteAsync(qry, product);
                        res = res + res1;
                        grandtotal += product.totalamt;


                    }

                }
                return grandtotal;
            }           
        }

        public async Task<int> Updateorder(Order order)
        {
            
            double grandtotal = 0;
            var query = @"update torder set cname=@cname,shippingaddress=@shippingaddress,odate=@odate,finalamt=@finalamt where oid=@oid ";
            using (var connection = _context.CreateConnection())
            {
               var result = await connection.ExecuteAsync(query, order);
                foreach (var up in order.products)
                {
                    double sum = 0;
                    sum = up.price * up.qty;
                    up.totalamt = sum;
                    var res = await connection.ExecuteAsync(@"update product set pname=@pname,qty=@qty,price=@price,totalamt=@totalamt where pid=@pid", up);
                    
                    grandtotal = grandtotal + sum;
                                      
                }
                var query1 = @"update torder set finalamt=@grandtotal where oid=@oid ";
                var result1 = await connection.ExecuteAsync(query1, new { grandtotal,order.oid });
                return result1;
            }
        }
         
        public async Task<int> Deleteorder(int id)
        {
            var query = "delete from torder where oid=@oid";
            using(var connection=_context.CreateConnection())
            {
                var result = await connection.ExecuteAsync(query, new { oid = id });
                if(result != 0)
                {
                    var result1 = await connection.ExecuteAsync("delete from product where pid=@pid", new { pid = id });
                }
                return result;
            }
        }
    }
    }
/*create table torder
(
oid int primary key identity,
cname varchar(25),
shippingaddress varchar(25),
odate date,
finalamt decimal
)
Insert into torder values ('priya','fcasdfcsdc','2022-9-9','789')
create table product
(
oid int primary key identity,
pid int,
pname varchar(25),
qty int,
price decimal,
totalamt decimal
)
Insert into product values (1,'pen',5,50,500)*/
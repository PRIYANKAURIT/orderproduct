using Dapper;
using orderproduct.Context;
using orderproduct.Model;
using orderproduct.Repository.Interface;
using System.Collections.Generic;

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
                    var result1 = await connection.QueryAsync<Order>("select * from product");
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
            var query ="Select * from "
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
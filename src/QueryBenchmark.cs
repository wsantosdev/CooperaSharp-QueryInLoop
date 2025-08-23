using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using ZLinq;

namespace CooperaSharp_QueryInLoop
{
    [MemoryDiagnoser]
    public class QueryBenchmark
    {
        private readonly AppDbContext _dbContext = new(); 

        [Benchmark]
        public List<CustomerIdentifiedOrderView> Original()
        {
            var orders = _dbContext.Orders.ToList();

            var customerIdentifiedOrders = new List<CustomerIdentifiedOrderView>();

            foreach (var order in orders)
            {
                var customer = _dbContext.Customers.FirstOrDefault(c => c.Id == order.CustomerId);
                customerIdentifiedOrders.Add(new CustomerIdentifiedOrderView
                {
                    OrderId = order.Id,
                    CreationDate = order.CreationDate,
                    CustomerName = customer?.Name!
                });
            }

            return customerIdentifiedOrders;
        }

        [Benchmark]
        public IList<CustomerIdentifiedOrderView> Solution()
        {
            var result = _dbContext.Orders
                                   .AsNoTracking()
                                   .AsValueEnumerable()
                                   .Join(_dbContext.Customers.AsNoTracking().AsValueEnumerable(),
                                         order => order.CustomerId,
                                         customer => customer.Id,
                                         (order, customer) => new CustomerIdentifiedOrderView
                                         {
                                             OrderId = order.Id,
                                             CreationDate = order.CreationDate,
                                             CustomerName = customer.Name
                                         })
                                   .ToList();
            return result;
        }
    }
}

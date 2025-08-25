using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using ZLinq;

namespace CooperaSharp_QueryInLoop
{
    [MemoryDiagnoser]
    public class QueryBenchmark
    {
        [Benchmark]
        public List<CustomerIdentifiedOrderView> Original()
        {
            using var dbContext = new AppDbContext();

            var orders = dbContext.Orders.ToList();

            var customerIdentifiedOrders = new List<CustomerIdentifiedOrderView>();

            foreach (var order in orders)
            {
                var customer = dbContext.Customers.FirstOrDefault(c => c.Id == order.CustomerId);
                customerIdentifiedOrders.Add(new CustomerIdentifiedOrderView
                {
                    OrderId = order.Id,
                    CreationDate = order.CreationDate,
                    CustomerName = customer!.Name
                });
            }

            return customerIdentifiedOrders;
        }

        [Benchmark]
        public IList<CustomerIdentifiedOrderViewAsStruct> Solution()
        {
            using var dbContext = new AppDbContext();
            
            var result = dbContext.Orders
                                  .Include(o => o.Customer)
                                  .AsNoTracking()
                                  .AsValueEnumerable()
                                  .Select(order => new CustomerIdentifiedOrderViewAsStruct
                                  {
                                      OrderId = order.Id,
                                      CreationDate = order.CreationDate,
                                      CustomerName = order.Customer!.Name
                                  })
                                  .ToList();
            return result;
        }
    }
}

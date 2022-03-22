using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private StoreDbContext context;
        public EFOrderRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Order> Orders => context.Orders
                                .Include(x => x.Lines)
                                .ThenInclude(x => x.Product);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(x => x.Product));
            if(order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}

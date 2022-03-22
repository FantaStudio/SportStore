using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;
        public IQueryable<Product> Products => context.Products;

        public EFStoreRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public void SaveProduct(Product product)
        {
            context.SaveChanges();
        }

        public void CreateProduct(Product product)
        {
            context.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            context.Remove(product);
            context.SaveChanges();
        }
    }
}

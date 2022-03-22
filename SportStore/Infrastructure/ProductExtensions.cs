using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Infrastructure
{
    public static class ProductExtensions
    {
        public static decimal GetPriceWithDiscount(this Product product)
        {
            return product.Price - (product.Price * 
                Convert.ToDecimal(product.Discount) / 100);
        }
    }
}

using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Test
{
    public class CartTest
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            Product product1 = new Product { ProductID = 1, Name = "prod1", Price = 50 };
            Product product2 = new Product { ProductID = 2, Name = "prod2", Price = 100 };

            Cart cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);

            CartLine[] results = cart.Lines.ToArray();

            Assert.Equal("prod1", results[0].Product.Name);
            Assert.Equal("prod2", results[1].Product.Name);

            Assert.Equal(2, results.Length);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Product product1 = new Product { ProductID = 1, Name = "prod1", Price = 50 };
            Product product2 = new Product { ProductID = 2, Name = "prod2", Price = 100 };

            Cart cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);

            cart.AddItem(product2, 4);

            CartLine[] results = cart.Lines.ToArray();

            Assert.Equal(1, results[0].Quantity);
            Assert.Equal(6, results[1].Quantity);

            Assert.Equal(2, results.Length);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            Product product1 = new Product { ProductID = 1, Name = "prod1", Price = 50 };
            Product product2 = new Product { ProductID = 2, Name = "prod2", Price = 100 };

            Cart cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);

            cart.RemoveItem(product2);

            CartLine[] results = cart.Lines.ToArray();

            Assert.Equal("prod1", results[0].Product.Name);
            Assert.Empty(results.Where(x => x.Product == product2));
            Assert.Single(results);
        }

        [Fact]
        public void Can_Calculate_Toal_Value()
        {
            Product product1 = new Product { ProductID = 1, Name = "prod1", Price = 50 };
            Product product2 = new Product { ProductID = 2, Name = "prod2", Price = 100 };

            Cart cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);

            decimal result = cart.ComputeTotalValue();

            Assert.Equal(250, result);
        }

        [Fact]
        public void Can_Clear_Content()
        {
            Product product1 = new Product { ProductID = 1, Name = "prod1", Price = 50 };
            Product product2 = new Product { ProductID = 2, Name = "prod2", Price = 100 };

            Cart cart = new Cart();

            cart.AddItem(product1, 1);
            cart.AddItem(product2, 2);

            cart.Clear();

            Assert.Empty(cart.Lines);
        }
    }
}

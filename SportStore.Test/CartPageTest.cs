using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using SportStore.Models.ViewModels;
using SportStore.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Test
{
    public class CartPageTest
    {
        [Fact]

        public void Can_Load_Cart()
        {
            Product product1 = new Product { ProductID = 1, Name = "prod1", Price = 50 };
            Product product2 = new Product { ProductID = 2, Name = "prod2", Price = 100 };

            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(x => x.Products).Returns(new Product[] { product1, product2 }.AsQueryable());

            Cart testCart = new Cart();
            testCart.AddItem(product1, 2);
            testCart.AddItem(product2, 1);

            CartController controller = new CartController(mock.Object, testCart);

            // Act
            CartModel viewModel = (controller.Index("myUrl") as ViewResult).ViewData.Model as CartModel;

            // Assert
            Assert.Equal(2, viewModel.Cart.Lines.Count());
            Assert.Equal("myUrl", viewModel.ReturnUrl);
        }

        [Fact]
        public void Can_Update_Cart()
        {
            Product product1 = new Product { ProductID = 1, Name = "prod1", Price = 50 };

            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(x => x.Products).Returns(new Product[] { product1 }.AsQueryable());

            Cart testCart = new Cart();

            CartController controller = new CartController(mock.Object, testCart);

            // Act
            controller.Index(1, "myUrl");

            // Assert
            Assert.Single(testCart.Lines);
            Assert.Equal("prod1", testCart.Lines.First().Product.Name);
            Assert.Equal(1, testCart.Lines.First().Quantity);

        }
    }
}

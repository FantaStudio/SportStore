using Microsoft.AspNetCore.Mvc;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Test
{
    public class OrderControllerTest
    {
        [Fact]
        public void Cannot_Checkout_Empty_Cart()
        {
            Mock<IOrderRepository> orderMock = new Mock<IOrderRepository>();

            Cart cart = new Cart();

            Order order = new Order();

            OrderController controller = new OrderController(orderMock.Object, cart);

            ViewResult result = controller.Checkout(order) as ViewResult;

            orderMock.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);

        }

        [Fact]
        public void Cannot_Checkout_Invalid_ShiipingData()
        {
            Mock<IOrderRepository> orderMock = new Mock<IOrderRepository>();

            Cart cart = new Cart();
            cart.AddItem(new Product { Name = "p1", Price = 5 }, 1);

            Order order = new Order();

            OrderController controller = new OrderController(orderMock.Object, cart);

            controller.ModelState.AddModelError("error", "error");

            ViewResult result = controller.Checkout(order) as ViewResult;

            // Проверяет, что SaveOrder ни разу не был вызван
            orderMock.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Never);

            Assert.True(string.IsNullOrEmpty(result.ViewName));
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public void Cannot_Checkout_And_Submit_Order()
        {
            Mock<IOrderRepository> orderMock = new Mock<IOrderRepository>();

            Cart cart = new Cart();
            cart.AddItem(new Product { Name = "p1", Price = 5 }, 1);

            Order order = new Order()
            {
                Name = "Lolka",
                Line1 = "Balashixa",
                City = "Pols",
                CityArea = "PolsArea",
                Country = "Polsha"
            };


            OrderController controller = new OrderController(orderMock.Object, cart);

            RedirectToPageResult result = controller.Checkout(order) as RedirectToPageResult;

            // Проверяет, что SaveOrder был вызван 1 раз
            orderMock.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Once);

            Assert.Equal("/Completed", result.PageName);
        }
    }
}

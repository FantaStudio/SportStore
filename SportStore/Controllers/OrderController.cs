using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;

        public OrderController(IOrderRepository repository, Cart cart)
        {
            this.repository = repository;
            this.cart = cart;
        }

        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Извините, но ваша корзина пуста");
            }
            if(ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                repository.SaveOrder(order);
                cart.Clear();
                return RedirectToAction("Complete", new { orderId = order.OrderID });
            } else
            {
                return View();
            }
        }


        public IActionResult Complete(long orderId)
        {
            return View(orderId);
        }
    }
}

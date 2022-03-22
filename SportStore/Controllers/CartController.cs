using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Controllers
{
    public class CartController : Controller
    {
        private IStoreRepository repository;

        private CartModel cartModel;

        public CartController(IStoreRepository repository, Cart cart)
        {
            this.repository = repository;
            cartModel = new CartModel() { 
                Cart = cart 
            };
        }

        [HttpGet]
        public IActionResult Index(string returnUrl)
        {
            cartModel.ReturnUrl = returnUrl;
            return View(cartModel);
        }

        [HttpPost]
        public IActionResult Index(long productId, string returnUrl)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            cartModel.Cart.AddItem(product, 1);
            return RedirectToAction("Index", new { returnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult Delete(long productId, string returnUrl)
        {
            cartModel.Cart.RemoveItem(cartModel.Cart.Lines.First(x => x.Product.ProductID == productId).Product);
            return RedirectToAction("Index", new { returnUrl = returnUrl });
        }
    }
}

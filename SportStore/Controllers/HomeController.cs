using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 8;

        public HomeController(IStoreRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index(string category, int productPage = 1, string q = null)
        {

            var filteredProducts = repository.Products
                .Where(p => (category == null 
                            || p.Category == category)
                            && (q == null
                            || p.Name.Trim().ToLower()
                            .Contains(q.Trim().ToLower())));

            var productListViewModel = new ProductsListViewModel()
            {
                Products = filteredProducts
                    .OrderBy(x => x.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),

                CurrentCategory = category,
                SearchQuery = q,
                PagingInfo = new PagingInfo {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = filteredProducts.Count()
                }
            };
            return View(productListViewModel);
        }

        public IActionResult Delivery()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View(new Dictionary<string, string>
            {
                ["Почему мой заказ был отменён"] = "Есть несколько причин, по которым Ваш заказ был автоматически отменен:\n" +
                "- В заказе указан некорректный телефон / e - mail / адрес доставки;\n" +
                "- Регион, который был указан в заказе не обслуживается службой доставки;\n" +
                "- Товар закончился на складе.\n",

                ["Есть ли ограничения на количество товаров в заказе?"] = "Один заказ не должен содержать больше 10 единиц товара. " +
                "При этом у нас нет ограничений на количество заказов. При необходимости всегда можно сделать еще один заказ.",

                ["Могу ли я изменить информацию о заказе после его оформления (адрес доставки/телефон/размер)?"] 
                = "Все заказы, которые размещаются в нашем интернет-магазине, " +
                "обрабатываются автоматически. Как только оформление заказа будет завершено, изменить адрес, " +
                "способ доставки, товарный ассортимент или вариант оплаты невозможно.",

                ["Как понять, что заказ создан?"] = "На последнем шаге размещения заказа появится страница с его номером."
            });
        }

        public IActionResult About()
        {
            return View();
        }
    }
}

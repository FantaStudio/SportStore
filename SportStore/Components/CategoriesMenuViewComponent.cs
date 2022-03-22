using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Components
{
    public class CategoriesMenuViewComponent : ViewComponent
    {
        private IStoreRepository repository;
        public CategoriesMenuViewComponent(IStoreRepository repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c));
        }
    }
}

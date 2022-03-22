using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportStore.Components;
using SportStore.Controllers;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SportStore.Test
{
    public class CategoriesViewComponentTest
    {
        [Fact]
        public void Can_Select_Categories()
        {
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(x => x.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product { ProductID = 3, Name = "P3", Category = "Cat2" },
                new Product { ProductID = 4, Name = "P4", Category = "Cat3" },
                new Product { ProductID = 5, Name = "P5", Category = "Cat1" }
            }).AsQueryable());

            CategoriesMenuViewComponent nav = new CategoriesMenuViewComponent(mock.Object);
            string[] result = ((IEnumerable<string>)(nav.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            // Assert
            Assert.True(Enumerable.SequenceEqual(result, new string[] { "Cat1", "Cat2", "Cat3" }));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            // Arange
            string categoryToSelect = "Cat2";


            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(x => x.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product { ProductID = 3, Name = "P3", Category = "Cat2" },
                new Product { ProductID = 4, Name = "P4", Category = "Cat3" },
                new Product { ProductID = 5, Name = "P5", Category = "Cat1" }
            }).AsQueryable());

            CategoriesMenuViewComponent nav = new CategoriesMenuViewComponent(mock.Object);

            nav.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext()
                {
                    RouteData = new Microsoft.AspNetCore.Routing.RouteData()
                }
            };

            nav.RouteData.Values["category"] = categoryToSelect;

            // Action
            string result = (string)(nav.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            Assert.Equal(categoryToSelect, result);
        }
    }
}

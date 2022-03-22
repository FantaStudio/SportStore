using Microsoft.AspNetCore.Mvc;
using Moq;
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
    public class HomeControllerTest
    {
        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
                new Product { ProductID = 4, Name = "P4" },
                new Product { ProductID = 5, Name = "P5" },
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object) { PageSize = 3 };

            // Act
            ProductsListViewModel viewModel = (controller.Index(null, 2) as ViewResult).ViewData.Model as ProductsListViewModel;

            // Assert

            Assert.Equal(2, viewModel.PagingInfo.TotalPages);
            Assert.Equal(5, viewModel.PagingInfo.TotalItems);
            Assert.Equal(2, viewModel.PagingInfo.CurrentPage);
            Assert.Equal(3, viewModel.PagingInfo.ItemsPerPage);
        }

        [Fact]
        public void Generate_Category_Specific_Product_Count()
        {
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(x => x.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product { ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product { ProductID = 3, Name = "P3", Category = "Cat2"},
                new Product { ProductID = 4, Name = "P4", Category = "Cat3"},
                new Product { ProductID = 5, Name = "P5", Category = "Cat1"},
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object) { PageSize = 3 };

            Func<ViewResult, ProductsListViewModel> GetModel = result =>
                result?.ViewData?.Model as ProductsListViewModel;

            int? res1 = GetModel(controller.Index("Cat1") as ViewResult)?.PagingInfo.TotalItems;
            int? res2 = GetModel(controller.Index("Cat2") as ViewResult)?.PagingInfo.TotalItems;
            int? res3 = GetModel(controller.Index("Cat3") as ViewResult)?.PagingInfo.TotalItems;
            int? resAll = GetModel(controller.Index(null) as ViewResult)?.PagingInfo.TotalItems;

            // Assert
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }

        [Fact]
        public void Generate_Search_Specific_Product_Count()
        {
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(x => x.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "ball1", Category = "Cat1"},
                new Product { ProductID = 2, Name = "ball2", Category = "Cat2"},
                new Product { ProductID = 3, Name = "t-shirt", Category = "Cat2"},
                new Product { ProductID = 4, Name = "trousers", Category = "Cat3"},
                new Product { ProductID = 5, Name = "voleyball", Category = "Cat1"},
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object) { PageSize = 3 };

            Func<ViewResult, ProductsListViewModel> GetModel = result =>
                result?.ViewData?.Model as ProductsListViewModel;

            string searchQuery = "ball";

            int? res1 = GetModel(controller.Index(null, 1, searchQuery) as ViewResult)?.PagingInfo.TotalItems;
            int? resAll = GetModel(controller.Index(null) as ViewResult)?.PagingInfo.TotalItems;

            // Assert
            Assert.Equal(3, res1);
            Assert.Equal(5, resAll);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportStore.Test
{
    public class ProductControllerTest
    {
        [Fact]
        public void Can_Use_Repository()
        {
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"}
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object);

            // Act
            ProductsListViewModel result = (controller.Index(null) as ViewResult).ViewData.Model as ProductsListViewModel;

            // Assert
            Product[] prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1"},
                new Product { ProductID = 2, Name = "P2"},
                new Product { ProductID = 3, Name = "P3"},
                new Product { ProductID = 4, Name = "P4"},
                new Product { ProductID = 5, Name = "P5"},
            }).AsQueryable());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            // Act
            ProductsListViewModel result = (controller.Index(null, 2) as ViewResult).ViewData.Model as ProductsListViewModel;

            // Assert
            var prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Filter_Category_Products()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product { ProductID = 2, Name = "P2", Category = "Cat2"},
                new Product { ProductID = 3, Name = "P3", Category = "Cat2"},
                new Product { ProductID = 4, Name = "P4", Category = "Cat3"},
                new Product { ProductID = 5, Name = "P5", Category = "Cat1"},
            }).AsQueryable());

            // Arrange
            HomeController controller = new HomeController(mock.Object);

            // Act
            ProductsListViewModel result = (controller.Index("Cat2", 1) as ViewResult).ViewData.Model as ProductsListViewModel;

            // Assert
            var prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.True(prodArray[0].Name == "P2" && prodArray[0].Category == "Cat2");
            Assert.True(prodArray[1].Name == "P3" && prodArray[1].Category == "Cat2");
        }

        [Fact]
        public void Can_Filter_Search_Products()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "ball1", Category = "Cat1"},
                new Product { ProductID = 2, Name = "ball2", Category = "Cat2"},
                new Product { ProductID = 3, Name = "t-shirt", Category = "Cat2"},
                new Product { ProductID = 4, Name = "trousers", Category = "Cat3"},
                new Product { ProductID = 5, Name = "voleyball", Category = "Cat1"},
            }).AsQueryable());


            // Arrange
            HomeController controller = new HomeController(mock.Object);

            string searchQuery = "ball";

            // Act
            ProductsListViewModel result = (controller.Index(null, 1, searchQuery) as ViewResult).ViewData.Model as ProductsListViewModel;

            // Assert
            var prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 3);
            Assert.True(prodArray[0].Name == "ball1");
            Assert.True(prodArray[1].Name == "ball2");
            Assert.True(prodArray[2].Name == "voleyball");
        }

        [Fact]
        public void Can_Multiple_Filter()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product { ProductID = 1, Name = "ball1", Category = "Cat1"},
                new Product { ProductID = 2, Name = "ball2", Category = "Cat2"},
                new Product { ProductID = 3, Name = "t-shirt", Category = "Cat2"},
                new Product { ProductID = 4, Name = "trousers", Category = "Cat3"},
                new Product { ProductID = 5, Name = "voleyball", Category = "Cat1"},
            }).AsQueryable());


            // Arrange
            HomeController controller = new HomeController(mock.Object);

            string searchQuery = "ball";
            string category = "Cat1";

            // Act
            ProductsListViewModel result = (controller.Index(category, 1, searchQuery) as ViewResult).ViewData.Model as ProductsListViewModel;

            // Assert
            var prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.True(prodArray[0].Name == "ball1");
            Assert.True(prodArray[1].Name == "voleyball");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using WarehouseSystem.Infrastructure.Interfaces;
using WarehouseSystem.Web.Controllers;
using Xunit;

namespace WarehouseSystem.Infrastructure.Tests
{
    public class TestDataStoreService
    {
        private Mock<IDataStoreService> _dataStoreServiceMock;
        public TestDataStoreService()
        {
            _dataStoreServiceMock = new Mock<IDataStoreService>();
        }

        [Fact]
        public void TestAddnGetInventory()
        {
            var setData = new Dtos.InventoryDto { ArtId = 4, Name = "Test Name", Stock = 5 };
            _dataStoreServiceMock.Setup(p => p.AddInventory(setData))
                .Returns(setData);
            WarehouseController warehouseController = new WarehouseController(_dataStoreServiceMock.Object);
            var result = warehouseController.GetInventory();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }
        [Fact]
        public void TestAddnGetProducts()
        {
            var setData = new Dtos.ProductDto { Name = "Product A", Price = 20d, 
                ContainArticles = new HashSet<Dtos.ContainArticles>() { new Dtos.ContainArticles { ArticleCount = "10", ArticleId= "1" },
                new Dtos.ContainArticles { ArticleCount = "15", ArticleId= "2" },
                new Dtos.ContainArticles { ArticleCount = "20", ArticleId= "3" }}
            };
            _dataStoreServiceMock.Setup(p => p.AddProduct(setData))
                .Returns(setData);
            WarehouseController warehouseController = new WarehouseController(_dataStoreServiceMock.Object);
            var result = warehouseController.GetProducts();
            var okResult = result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
        

    }
}

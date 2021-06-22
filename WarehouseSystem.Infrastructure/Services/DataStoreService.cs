using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseSystem.Core.Entities;
using WarehouseSystem.Core.Interfaces;
using WarehouseSystem.Infrastructure.Dtos;
using WarehouseSystem.Infrastructure.Interfaces;

namespace WarehouseSystem.Infrastructure.Services
{
    public class DataStoreService : IDataStoreService
    {
        private readonly IDataStoreRepository dataStoreRepository;

        public DataStoreService(IDataStoreRepository dataStoreRepository)
        {
            this.dataStoreRepository = dataStoreRepository;
        }
        public InventoryDto AddInventory(InventoryDto inventoryDto)
        {
            if (inventoryDto == null)
                throw new ArgumentException("Inventory cannot be null", "original");
            // Simple validations not to have duplicates
            if(!dataStoreRepository.GetInventories().Any(p=> p.Id == inventoryDto.ArtId))
                dataStoreRepository.AddInventory(Inventory.Create(inventoryDto.ArtId, inventoryDto.Name, inventoryDto.Stock));
            return inventoryDto;
        }

        public ProductDto AddProduct(ProductDto productDto)
        {
            if (productDto == null)
                throw new ArgumentException("Product cannot be null", "original");
            if (!dataStoreRepository.GetProducts().Any(p => p.Name == productDto.Name))
            {
                Dictionary<int, int> dict = productDto.ContainArticles.ToDictionary(d => int.Parse(d.ArticleId), d => int.Parse(d.ArticleCount));
                dataStoreRepository.AddProduct(Product.Create(productDto.Name, productDto.Price ?? 0d, dict));
            }
            return productDto;
        }

        public IEnumerable<AvailableProductDto> GetAllAvailableProductData()
        {
            var products = dataStoreRepository.GetProducts();
            var availableProducts = new List<AvailableProductDto>();
            foreach (var obj in products)
            {
                var returnVal = GetAvailableProductDto(obj.Name);
                if (returnVal != null)
                {
                    availableProducts.Add(returnVal);
                }
            }
            return availableProducts;
        }

        private AvailableProductDto GetAvailableProductDto(string productName)
        {
            var product = dataStoreRepository.GetProducts().FirstOrDefault(p => p.Name == productName);
            /* to get the available product we need to divide the amount of articles needed 
                by the items available in Inventory and take the least count to determine how many products 
                we can sell*/
            int leastCount = 0;
            var articles = product.Articles;
            for (int i = 0; i < articles.Count; i++)
            {
                // get Inventory by Id 
                var inv = dataStoreRepository.GetInventories().FirstOrDefault(j => j.Id == articles.ElementAt(i).Key);
                if (i == 0)
                {
                    // divide product articles count by inventory count and get the minimum count 
                    leastCount = (int)Math.Floor(Convert.ToDecimal(inv.Stock) / Convert.ToDecimal(articles.ElementAt(i).Value));
                    continue;
                }
                int currentLeast = (int)Math.Floor(Convert.ToDecimal(inv.Stock) / Convert.ToDecimal(articles.ElementAt(i).Value));
                if (currentLeast < leastCount)
                {
                    leastCount = currentLeast;
                }
            }
            // we check atleast we have 1 
            if (leastCount > 0)
            {
                return new AvailableProductDto { Name = product.Name, ProductCount = leastCount };
            }
            return null;

        }

        public IEnumerable<InventoryDto> GetAllInventoryData()
        {
            return dataStoreRepository.GetInventories().Select(s => new InventoryDto { ArtId = s.Id, Stock = s.Stock, Name = s.Name });
        }

        public IEnumerable<ProductDto> GetAllProductData()
        {
            return dataStoreRepository.GetProducts().Select(s => new ProductDto { Price = s.Price, 
                ContainArticles = s.Articles.Select(d=> new ContainArticles { ArticleId = d.Key.ToString(), ArticleCount = d.Value.ToString()}).ToHashSet(), 
                Name = s.Name });
        }

        public SoldProduct SellProduct(ProductCartDto productCartDto)
        {
                var product = dataStoreRepository.GetProducts().FirstOrDefault(p => p.Name == productCartDto.Name);
                var isSold = UpdateInventory(product.Articles);
                if (isSold)
                    return new SoldProduct { Name = product.Name, Description = $"Successfully bought { product.Name } for {product.Price}" };
                return null;
        }
        private bool UpdateInventory(Dictionary<int, int> articles)
        {
            try
            {
                foreach (var k in articles)
                {
                    var inv = dataStoreRepository.GetInventories().FirstOrDefault(p => p.Id == k.Key);
                    var stock = inv.Stock - k.Value;
                    dataStoreRepository.RemoveInventory(inv.Id);
                    dataStoreRepository.AddInventory(Inventory.Create(inv.Id, inv.Name, stock));
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
    }
}

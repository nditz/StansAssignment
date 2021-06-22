using System;
using System.Collections.Generic;
using System.Text;
using WarehouseSystem.Core.Entities;
using WarehouseSystem.Core.Interfaces;

namespace WarehouseSystem.Library.Infrastructure
{
    public class DataStoreRepository : IDataStoreRepository
    {
        private readonly HashSet<Inventory> inventoryAggregates;
        private readonly HashSet<Product> productAggregates;
        public DataStoreRepository()
        {
            inventoryAggregates = new HashSet<Inventory>();
            productAggregates = new HashSet<Product>();
        }
        public Inventory AddInventory(Inventory inventory)
        {
            inventoryAggregates.Add(inventory);
            return inventory;
        }

        public Product AddProduct(Product product)
        {
            productAggregates.Add(product);
            return product;
        }

        public IEnumerable<Inventory> GetInventories()
        {
            return inventoryAggregates;
        }

        public IEnumerable<Product> GetProducts()
        {
            return productAggregates;
        }

        public void RemoveInventory(int Id)
        {
            inventoryAggregates.RemoveWhere(p => p.Id == Id);
        }
    }
}

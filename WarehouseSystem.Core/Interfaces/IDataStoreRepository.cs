using System;
using System.Collections.Generic;
using System.Text;
using WarehouseSystem.Core.Entities;

namespace WarehouseSystem.Core.Interfaces
{
    public interface IDataStoreRepository
    {
        Inventory AddInventory(Inventory inventory);
        void RemoveInventory(int Id);
        IEnumerable<Inventory> GetInventories();
        Product AddProduct(Product product);
        IEnumerable<Product> GetProducts();
    }
}

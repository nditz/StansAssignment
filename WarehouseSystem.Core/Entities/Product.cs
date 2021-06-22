using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseSystem.Core.Entities
{
    public class Product
    {
        private Product(string name, double price, Dictionary<int, int> articles)
        {
            Name = name;
            Price = price;
            Articles = articles;
        }
        public static Product Create(string name, double price, Dictionary<int, int> articles)
        {
            return new Product(name, price, articles);
        }
        public string Name { get;  }
        public double Price { get;  }
        public Dictionary<int, int> Articles { get; }
    }

}

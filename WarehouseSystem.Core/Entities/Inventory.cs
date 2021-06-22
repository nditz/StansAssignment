using System;
using System.Collections.Generic;
using System.Text;

namespace WarehouseSystem.Core.Entities
{
    public class Inventory
    {
        private Inventory(int id, string name, int stock)
        {
            Id = id;
            Name = name;
            Stock = stock;
        }
        public static Inventory Create(int id, string name, int stock)
        {
            return new Inventory(id, name, stock);
        }
        public int Id { get;  }
        public string Name { get;  }
        public int Stock { get;  }
    }
}


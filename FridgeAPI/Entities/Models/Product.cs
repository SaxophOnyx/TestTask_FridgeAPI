using System;

namespace FridgeAPI.Entities.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? DefaultQuantity { get; set; }
    }
}

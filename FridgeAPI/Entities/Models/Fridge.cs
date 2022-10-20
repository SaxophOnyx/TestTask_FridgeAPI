using System;
using System.Collections.Generic;

namespace FridgeAPI.Entities.Models
{
    public class Fridge
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? OwnerName { get; set; }

        public FridgeModel Model { get; set; }

        public List<StoredProduct> Products { get; set; }
    }
}

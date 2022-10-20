using System;

namespace FridgeAPI.Entities.Models
{
    public class StoredProduct
    {
        public Guid Id { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}

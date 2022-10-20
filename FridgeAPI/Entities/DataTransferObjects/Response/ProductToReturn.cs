using System;

namespace FridgeAPI.Entities.DataTransferObjects.Response
{
    public class ProductToReturn
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? DefaultQuantity { get; set; }
    }
}

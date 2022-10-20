using System;

namespace FridgeAPI.DataTransferObjects.DataTransferObjects.Response
{
    public class ProductToReturn
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? DefaultQuantity { get; set; }
    }
}

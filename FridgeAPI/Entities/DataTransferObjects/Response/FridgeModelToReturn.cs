using System;

namespace FridgeAPI.Entities.DataTransferObjects.Response
{
    public class FridgeModelToReturn
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? Year { get; set; }
    }
}

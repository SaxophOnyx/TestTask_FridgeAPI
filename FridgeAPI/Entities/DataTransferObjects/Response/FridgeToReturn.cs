using System;

namespace FridgeAPI.Entities.DataTransferObjects.Response
{
    public class FridgeToReturn
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? OwnerName { get; set; }

        public FridgeModelToReturn Model { get; set; }
    }
}

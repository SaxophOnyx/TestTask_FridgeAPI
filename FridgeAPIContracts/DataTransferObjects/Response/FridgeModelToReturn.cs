using System;

namespace FridgeAPI.DataTransferObjects.DataTransferObjects.Response
{
    public class FridgeModelToReturn
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? Year { get; set; }
    }
}

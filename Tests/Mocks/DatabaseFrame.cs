using FridgeAPI.Entities.Models;
using System.Collections.Generic;

namespace Tests.Mocks
{
    public class DatabaseFrame
    {
        public List<Product> Products { get; set; }

        public List<FridgeModel> FridgeModels { get; set; }

        public List<Fridge> Fridges { get; set; }
    }
}

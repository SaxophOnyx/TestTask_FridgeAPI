using FridgeAPI.Entities.Models;
using System;
using System.Collections.Generic;

namespace FridgeAPI.Entities.DatabaseAccess.Abstractions
{
    public interface IFridgeModelRepository
    {
        FridgeModel GetModel(Guid modelId);

        IEnumerable<FridgeModel> GetModels();
    }
}

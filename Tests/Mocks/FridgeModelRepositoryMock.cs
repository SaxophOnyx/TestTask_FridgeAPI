using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using FridgeAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Mocks
{
    internal class FridgeModelRepositoryMock : IFridgeModelRepository
    {
        private List<FridgeModel> _fridgeModels;


        public FridgeModelRepositoryMock(List<FridgeModel> models)
        {
            _fridgeModels = models;
        }


        public FridgeModel GetModel(Guid modelId)
        {
            return _fridgeModels.FirstOrDefault(m => m.Id == modelId);
        }

        public IEnumerable<FridgeModel> GetModels()
        {
            return _fridgeModels;
        }
    }
}

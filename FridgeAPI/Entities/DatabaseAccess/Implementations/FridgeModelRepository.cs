using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using FridgeAPI.Entities.DatabaseAccess.Implmentations;
using FridgeAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace FridgeAPI.Entities.DatabaseAccess.Implementations
{
    public class FridgeModelRepository : GenericRepository<FridgeModel>, IFridgeModelRepository
    {
        public FridgeModelRepository(DbContext context) 
            : base(context)
        {

        }


        public FridgeModel GetModel(Guid modelId)
        {
            return GetEntity(modelId);
        }

        public IEnumerable<FridgeModel> GetModels()
        {
            return GetEntities();
        }
    }
}

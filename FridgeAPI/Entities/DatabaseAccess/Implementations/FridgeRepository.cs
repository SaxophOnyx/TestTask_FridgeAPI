using FridgeAPI.Entities.Models;
using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FridgeAPI.Entities.DatabaseAccess.Implmentations
{
    public class FridgeRepository : GenericRepository<Fridge>, IFridgeRepository
    {
        public FridgeRepository(DbContext context)
            : base(context)
        {

        }

        public Fridge GetFridge(Guid fridgeId)
        {
            return GetFridges().FirstOrDefault(x => x.Id == fridgeId);
        }

        public IEnumerable<Fridge> GetFridges()
        {
            return GetEntities().Include(x => x.Model)
                               .Include(x => x.Products).ThenInclude(x => x.Product).AsEnumerable();
        }

        public void AddFridge(Fridge fridge)
        {
            AddEntity(fridge);
        }

        public void DeleteFridge(Guid fridgeId)
        {
            var fridge = GetFridges().FirstOrDefault(x => x.Id == fridgeId);

            if (fridge != null)
                DeleteEntity(fridge);
        }

        public void FillFridges()
        {

        }
    }
}

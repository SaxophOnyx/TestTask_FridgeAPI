using FridgeAPI.Entities.Models;
using System;
using System.Collections.Generic;

namespace FridgeAPI.Entities.DatabaseAccess.Abstractions
{
    public interface IFridgeRepository
    {
        Fridge GetFridge(Guid fridgeId);

        IEnumerable<Fridge> GetFridges();

        void AddFridge(Fridge fridge);

        void DeleteFridge(Guid fridgeId);

        int Save();

        void FillFridges();
    }
}

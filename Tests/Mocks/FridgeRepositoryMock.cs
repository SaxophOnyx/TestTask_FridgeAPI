using FridgeAPI.Entities.DatabaseAccess.Abstractions;
using FridgeAPI.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests.Mocks
{
    internal class FridgeRepositoryMock : IFridgeRepository
    {
        private List<Fridge> _fridges;


        public FridgeRepositoryMock(List<Fridge> fridges)
        {
            _fridges = fridges;
        }


        public Fridge GetFridge(Guid fridgeId)
        {
            return _fridges.Find(i => i.Id == fridgeId);
        }

        public IEnumerable<Fridge> GetFridges()
        {
            return _fridges;
        }

        public int Save()
        {
            return 0;
        }

        public List<Fridge> GetFridgeCollectionDeepcopy()
        {
            var list = new List<Fridge>();

            foreach (var f in _fridges)
            {
                var model = new FridgeModel()
                {
                    Id = f.Model.Id,
                    Name = f.Model.Name,
                    Year = f.Model.Year,
                };

                var fridge = new Fridge()
                {
                    Id = f.Id,
                    Name = f.Name,
                    OwnerName = f.OwnerName,
                    Products = new List<StoredProduct>(f.Products),
                    Model = model
                };

                list.Add(fridge);
            }

            return list;
        }

        public void DeleteFridge(Guid fridgeId)
        {
            var fridge = GetFridge(fridgeId);

            if (fridge != null)
                _fridges.Remove(fridge);
        }

        public void AddFridge(Fridge fridge)
        {
            _fridges.Add(fridge);
        }

        public void FillFridges()
        {
            throw new NotImplementedException();
        }
    }
}

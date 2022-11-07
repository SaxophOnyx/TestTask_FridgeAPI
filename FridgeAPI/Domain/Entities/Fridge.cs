using System.Collections.Generic;
using System;

namespace FridgeAPI.Domain.Entities
{
    public class Fridge
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? OwnerName { get; set; }

        public FridgeModel Model { get; set; }

        public List<StoredProduct> Products { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            Fridge fridge = obj as Fridge;

            if (fridge == null)
                return false;

            return Id.Equals(fridge.Id)
                && Name.Equals(fridge.Name)
                && OwnerName.Equals(fridge.OwnerName)
                && Model.Equals(fridge.Model)
                && Products.Equals(fridge.Products);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, OwnerName, Model, Products);
        }
    }
}

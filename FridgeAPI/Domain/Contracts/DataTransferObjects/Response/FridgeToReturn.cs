using System;

namespace FridgeAPI.Domain.Contracts.DataTransferObjects.Response
{
    public class FridgeToReturn
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? OwnerName { get; set; }

        public FridgeModelToReturn Model { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            FridgeToReturn fridge = obj as FridgeToReturn;

            if (fridge == null)
                return false;

            return Id.Equals(fridge.Id)
                && Name.Equals(fridge.Name)
                && OwnerName.Equals(fridge.OwnerName)
                && Model.Equals(fridge.Model);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, OwnerName, Model);
        }
    }
}

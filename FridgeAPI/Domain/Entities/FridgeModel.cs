using System;

namespace FridgeAPI.Domain.Entities
{
    public class FridgeModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? Year { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            FridgeModel model = obj as FridgeModel;

            if (model == null)
                return false;

            return Id.Equals(model.Id)
                && Name.Equals(model.Name)
                && Year.Equals(model.Year);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Year);
        }
    }
}

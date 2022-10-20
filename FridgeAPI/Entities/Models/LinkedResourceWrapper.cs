using System.Collections.Generic;

namespace FridgeAPI.Entities.Models
{
    public class LinkedResourceWrapper<T>
    {
        public T Value { get; set; }

        public List<Link> Links { get; set; }


        public LinkedResourceWrapper(T value)
        {
            Value = value;
            Links = new List<Link>();
        }

        public LinkedResourceWrapper(T value, List<Link> links)
        {
            Value = value;
            Links = links;
        }
    }
}

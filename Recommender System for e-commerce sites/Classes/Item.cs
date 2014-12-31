using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommender_System_for_e_commerce_sites.Classes
{
    class Item : IComparable<Item>
    {
        public string Name { get; set; }
        public double Support { get; set; }

        public int CompareTo(Item other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}

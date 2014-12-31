using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Recommender_System_for_e_commerce_sites.Classes;

namespace Recommender_System_for_e_commerce_sites
{
    class ItemsDictionary : KeyedCollection<string, Item>
    {
        protected override string GetKeyForItem(Item item)
        {
            return item.Name;
        }

        internal void ConcatItems(IList<Item> frequentItems)
        {
            foreach (var item in frequentItems)
            {
                this.Add(item);
            }
        }
    }
}

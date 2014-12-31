using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Recommender_System_for_e_commerce_sites.Classes
{
    class Output
    {
        public IList<Rule> StrongRules { get; set; }
        public ItemsDictionary FrequentItems { get; set; } 
    }

    class PrintableRules
    {
        public List<string> combination { get; set;}
        public List<string> remaining { get; set; }

        public PrintableRules(List<string> comb, List<string> rem)
        {
            this.combination = comb;
            this.remaining = rem;
        }
    }
}

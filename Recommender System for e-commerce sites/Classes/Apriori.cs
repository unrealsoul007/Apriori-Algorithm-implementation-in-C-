using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Recommender_System_for_e_commerce_sites.Classes;
using Recommender_System_for_e_commerce_sites;

namespace Recommender_System_for_e_commerce_sites
{
    class Apriori
    {
        #region Public Method
        public Output ProcessTransaction(double minSupport, double minConfidence, IEnumerable<string> items, List<string> transactions)
        {
            List<Item> frequentItems = GetL1FrequentItems(minSupport, items, transactions);
            ItemsDictionary allFrequentItems = new ItemsDictionary();
            allFrequentItems.ConcatItems(frequentItems);
            Dictionary<string, double> candidates = new Dictionary<string, double>();
            double transactionsCount = transactions.Count();

            do
            {
                candidates = GenerateCandidates(frequentItems, transactions);
                frequentItems = GetFrequentItems(candidates, minSupport, transactionsCount);
                allFrequentItems.ConcatItems(frequentItems);
            }
            while (candidates.Count != 0);

            HashSet<Rule> rules = GenerateRules(allFrequentItems);
            HashSet<Rule> hashedRules = GetStrongRules(minConfidence, rules, allFrequentItems);
            //Dictionary<string, Dictionary<string, double>> closedItemSets = GetClosedItemSets(allFrequentItems);
            //IList<string> maximalItemSets = GetMaximalItemSets(closedItemSets);

            List<Rule> strongRules = new List<Rule>();
            foreach (var rule in hashedRules)
            {
                strongRules.Add(rule);
            }

            return new Output
            {
                StrongRules = strongRules,
                //MaximalItemSets = maximalItemSets,
                //ClosedItemSets = closedItemSets,
                FrequentItems = allFrequentItems
            };
        }
        #endregion

        # region Private Methods

        private List<Item> GetL1FrequentItems(double minSupport, IEnumerable<string> items, IEnumerable<string> transactions)
        {
            var frequentItemsL1 = new List<Item>();
            double transactionsCount = transactions.Count();

            foreach (var item in items)
            {
                double support = GetSupport(item, transactions);

                if (support / transactionsCount >= minSupport)
                {
                    frequentItemsL1.Add(new Item { Name = item, Support = support });
                }
            }
            //frequentItemsL1.Sort();
            return frequentItemsL1;
        }

        // If we don't map "names_of_items" to chars then transactionsList should be IEnumerable<List<string>>
        private double GetSupport(string generatedCandidate, IEnumerable<string> transactionsList)
        {
            double support = 0;

            foreach (string transaction in transactionsList)
            {
                if (CheckIsSubset(generatedCandidate, transaction))
                {
                    support++;
                }
            }

            return support;
        }

        // check the validity of this function.
        // I think child and parent will be List<string> instead of simply string.
        private bool CheckIsSubset(string child, string parent)
        {
            foreach (char c in child)
            {
                if (!parent.Contains(c))
                {
                    return false;
                }
            }

            return true;
        }

        // uses GenerateCandidate for the generation of next level candidates
        private Dictionary<string, double> GenerateCandidates(List<Item> frequentItems, IEnumerable<string> transactions)
        {
            Dictionary<string, double> candidates = new Dictionary<string, double>();

            for (int i = 0; i < frequentItems.Count - 1; i++)
            {
                char[] firstItems = frequentItems[i].Name.ToCharArray();
                Array.Sort(firstItems);
                string firstItem = new string(firstItems);

                for (int j = i + 1; j < frequentItems.Count; j++)
                {
                    char[] secondItems = frequentItems[j].Name.ToCharArray();
                    Array.Sort(secondItems);
                    string secondItem = new string(secondItems);

                    string generatedCandidate = GenerateCandidate(firstItem, secondItem);

                    if (generatedCandidate != string.Empty)
                    {
                        double support = GetSupport(generatedCandidate, transactions);
                        candidates.Add(generatedCandidate, support);
                    }
                }
            }

            return candidates;
        }

        // correct the algorithm here
        private string GenerateCandidate(string firstItem, string secondItem)
        {
            int length = firstItem.Length;

            if (length == 1)
            {
                return firstItem + secondItem;
            }
            else
            {
                string firstSubString = firstItem.Substring(0, length - 1);
                string secondSubString = secondItem.Substring(0, length - 1);

                if (firstSubString == secondSubString)
                {
                    return firstItem + secondItem[length - 1];
                }

                return string.Empty;
            }
        }

        private List<Item> GetFrequentItems(IDictionary<string, double> candidates, double minSupport, double transactionsCount)
        {
            var frequentItems = new List<Item>();

            foreach (var item in candidates)
            {
                if (item.Value / transactionsCount >= minSupport)
                {
                    frequentItems.Add(new Item { Name = item.Key, Support = item.Value });
                }
            }

            return frequentItems;
        }

        private HashSet<Rule> GenerateRules(ItemsDictionary allFrequentItems)
        {
            var rulesList = new HashSet<Rule>();

            foreach (var item in allFrequentItems)
            {
                if (item.Name.Length > 1)
                {
                    IEnumerable<string> subsetsList = GenerateSubsets(item.Name);

                    foreach (var subset in subsetsList)
                    {
                        string remaining = GetRemaining(subset, item.Name);
                        Rule rule = new Rule(subset, remaining, 0);

                        if (!rulesList.Contains(rule))
                        {
                            rulesList.Add(rule);
                        }
                    }
                }
            }

            return rulesList;
        }

        private IEnumerable<string> GenerateSubsets(string item)
        {
            IEnumerable<string> allSubsets = new string[] { };
            int subsetLength = item.Length / 2;

            for (int i = 1; i <= subsetLength; i++)
            {
                IList<string> subsets = new List<string>();
                GenerateSubsetsRecursive(item, i, new char[item.Length], subsets);
                allSubsets = allSubsets.Concat(subsets);
            }

            return allSubsets;
        }

        private void GenerateSubsetsRecursive(string item, int subsetLength, char[] temp, IList<string> subsets, int q = 0, int r = 0)
        {
            if (q == subsetLength)
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < subsetLength; i++)
                {
                    sb.Append(temp[i]);
                }

                subsets.Add(sb.ToString());
            }
            else
            {
                for (int i = r; i < item.Length; i++)
                {
                    temp[q] = item[i];
                    GenerateSubsetsRecursive(item, subsetLength, temp, subsets, q + 1, i + 1);
                }
            }
        }

        private string GetRemaining(string child, string parent)
        {
            for (int i = 0; i < child.Length; i++)
            {
                int index = parent.IndexOf(child[i]);
                parent = parent.Remove(index, 1);
            }

            return parent;
        }

        private HashSet<Rule> GetStrongRules(double minConfidence, HashSet<Rule> rules, ItemsDictionary allFrequentItems)
        {
            var strongRules = new HashSet<Rule>();

            foreach (Rule rule in rules)
            {
                char[] xys = (rule.X + rule.Y).ToCharArray();
                Array.Sort(xys);
                string xy = new string(xys);

                AddStrongRule(rule, xy, strongRules, minConfidence, allFrequentItems);
            }

            //strongRules.Sort();
            return strongRules;
        }

        private void AddStrongRule(Rule rule, string XY, HashSet<Rule> strongRules, double minConfidence, ItemsDictionary allFrequentItems)
        {
            double confidence = GetConfidence(rule.X, XY, allFrequentItems);

            if (confidence >= minConfidence)
            {
                Rule newRule = new Rule(rule.X, rule.Y, confidence);
                strongRules.Add(newRule);
            }

            confidence = GetConfidence(rule.Y, XY, allFrequentItems);

            if (confidence >= minConfidence)
            {
                Rule newRule = new Rule(rule.Y, rule.X, confidence);
                strongRules.Add(newRule);
            }
        }

        private double GetConfidence(string X, string XY, ItemsDictionary allFrequentItems)
        {
            double supportX = allFrequentItems[X].Support;
            double supportXY = allFrequentItems[XY].Support;
            return supportXY / supportX;
        }

        #endregion
    }
}

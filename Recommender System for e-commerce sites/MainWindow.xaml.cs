using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Recommender_System_for_e_commerce_sites.Classes;
using Recommender_System_for_e_commerce_sites;
using Microsoft.Win32;

namespace Recommender_System_for_e_commerce_sites
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Class variables

        // list of all the items
        List<string> items;

        // list of all the transactions. Basically each transaction is a string of chars where each char represents an item.
        List<string> transactions;

        // Output containing the strongRules based on which query will be handled
        Output Rules;
        IList<Rule> strongRules;
        List<PrintableRules> printableRules;
        SuggestionsWindow secondWindow;

        // this back copy is used in query output
        Dictionary<char, string> CharToNames;
        Dictionary<string, char> NamesToChar;
        int cur;
        string csvFileName;
        
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            initialise();
        }

        private void initialise()
        {
            #region Variable initialisations
            items = new List<string>();
            transactions = new List<string>();
            NamesToChar = new Dictionary<string, char>();
            CharToNames = new Dictionary<char, string>();
            csvFileName = "";
            printableRules = new List<PrintableRules>();
            #endregion

            #region GUI related
            // GUI realated initialisations
            queryTextBox.IsReadOnly = true;
            suggestionsButton.IsEnabled = false;
            otherSuggestionsButton.Visibility = System.Windows.Visibility.Hidden;

            
            #endregion

        }
        // Read the transactions from the .csv file
        private void Read_Transactions_Click(object sender, RoutedEventArgs e)
        {
            cur = 0;
            items = new List<string>();
            using (CsvFileReader reader = new CsvFileReader(csvFileName))
            {
                CsvRow row = new CsvRow();
                while (reader.ReadRow(row))
                {
                    for (int i = 1; i < row.Count; i++)
                    {
                        if (!NamesToChar.ContainsKey(row[i].Trim()))
                        {
                            bool result = AddToDictionary(row[i].Trim());
                            if (result == false)
                            {
                                MessageBox.Show("More than 75 items inserted. Too many items to process..!! :(");
                                this.Close();
                            }
                        }
                    }
                    char[] itemChars = new char[row.Count - 1];
                    for (int i = 1; i < row.Count; i++)
                    {
                        itemChars[i - 1] = NamesToChar[row[i].Trim()];
                    }
                    transactions.Add(new string(itemChars));
                }
            }
            MessageBox.Show("Transactions have been read..!! You may proceed to process them.");
        }

        // converts item_string to char i.e. maps item_string to char
        // also maintains a back copy of char to item_string
        private bool AddToDictionary(string item)
        {
            if (cur <= 75)
            {
                NamesToChar.Add(item, (char)('0' + cur));
                CharToNames.Add((char)('0' + cur), item);

                string curChar = ((char)('0' + cur)).ToString();
                items.Add(curChar);
                cur++;
                return true;
            }
            else
                return false;
        }

        // call process Transactions method of Apriori class.
        private void Process_transactions_Click(object sender, RoutedEventArgs e)
        {
            Apriori _apriori = new Apriori();
            // obtain these values from the slider
            double minSupport, minConfidence;

            minSupport = SuppSlider.Value / 100;
            minConfidence = ConfiSlider.Value / 100;

            // obtain the list of items and transactions from the csv file read
            Rules = _apriori.ProcessTransaction(minSupport, minConfidence, items, transactions);
            strongRules = Rules.StrongRules;
            
            //convert them to printable rules
            foreach (Rule rule in strongRules)
            {
                List<string> combination = new List<string>();
                foreach (char c in rule.X)
                {
                    string name = CharToNames[c];
                    combination.Add(name);
                }

                List<string> remaining = new List<string>();
                foreach (char c in rule.Y)
                {
                    string name = CharToNames[c];
                    remaining.Add(name);
                }

                printableRules.Add(new PrintableRules(combination, remaining));
            }
            queryTextBox.IsReadOnly = false;
            suggestionsButton.IsEnabled = true;
            MessageBox.Show("Transactions have been processed!");
        }

        private void displayRules()
        {
            if (secondWindow != null)
                secondWindow = null;
            secondWindow = new SuggestionsWindow();
            secondWindow.Title = "Suggestion Window";
            secondWindow.Hide();
            List<string> allRules = new List<string>();
            foreach (var pRule in printableRules)
            {
                string _rule = "";
                for (int i = 0; i < pRule.combination.Count - 1; i++)
                {
                    _rule += pRule.combination[i] + " + ";
                }
                _rule += pRule.combination[pRule.combination.Count - 1] + " => ";

                for (int i = 0; i < pRule.remaining.Count - 1; i++)
                {
                    _rule += pRule.remaining[i] + " , ";
                }
                _rule += pRule.remaining[pRule.remaining.Count - 1];

                allRules.Add(_rule);
            }
            secondWindow.AddContentToTextBox(allRules);
            secondWindow.Show();
        }
        
        private void SuppSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV documents (.csv)|*.csv";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                csvFileName = dlg.FileName;
                FileNameTextBox.Text = csvFileName;
            }
        }

        private void suggestionsButton_Click(object sender, RoutedEventArgs e)
        {
            queryTextBox.IsReadOnly = true;

            // display suggestion in the suggestionLabel or No suggestions and enable otherSuggestionsButton
            string query = queryTextBox.Text;
            string[] items = query.Split(new char[] { ',' });
            char [] mappedItems = new char[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                string curItem = items[i].Trim();
                if (NamesToChar.ContainsKey(curItem.ToUpper()))
                    mappedItems[i] = NamesToChar[curItem.ToUpper()];
                else
                {
                    MessageBox.Show("No such item " + curItem.ToUpper() + " present in the database. Check spelling..!!\n Please re-enter");
                    queryTextBox.IsReadOnly = false;
                    return;
                }
            }
            Array.Sort(mappedItems);
            query = new string(mappedItems);

            string output = "";
            bool ruleFound = false;
            foreach (var rule in strongRules)
            {
                if (rule.X == query)
                {
                    ruleFound = true;
                    output += rule.Y;
                }
            }
            //remove duplicate suggestions
            output = new string(output.ToCharArray().Distinct().ToArray());

            if(ruleFound == true)
            {
                string content = "";

                foreach (char c in output)
                {
                    string curItem = CharToNames[c];
                    content += curItem + ",";
                }
                content.Remove(content.Length - 2);
                resultLabel.Content = content;
            }
            else
            {
                resultLabel.Content = "Sorry!! There are no suggestions for this list of items! :(.\n" + 
                                        "But you can try other suggestions button!! :)";
            }

            queryTextBox.IsReadOnly = false;
            otherSuggestionsButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void otherSuggestionsButton_Click(object sender, RoutedEventArgs e)
        {
            displayRules();
            otherSuggestionsButton.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ViewItemList()
        {
            if (secondWindow != null)
                secondWindow = null;
            secondWindow = new SuggestionsWindow();
            secondWindow.Title = "Item List";
            secondWindow.Hide();

            List<string> itemList = new List<string>();
            foreach (string s in items)
            {
                char c = s[0];
                string item = CharToNames[c];
                itemList.Add(item);
            }
            secondWindow.AddContentToTextBox(itemList);
            secondWindow.Show();
        }

        private void View_Item_List_Click(object sender, RoutedEventArgs e)
        {
            ViewItemList();
        }
    }
}

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
using System.Windows.Shapes;

namespace Recommender_System_for_e_commerce_sites
{
    /// <summary>
    /// Interaction logic for SuggestionsWindow.xaml
    /// </summary>
    public partial class SuggestionsWindow : Window
    {
        public SuggestionsWindow()
        {
            InitializeComponent();
            suggestionsTextBox.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
        }

        public void AddContentToTextBox(List<string> content)
        {
            suggestionsTextBox.Text = "";
            string _content = "";
            foreach (string s in content)
            {
                _content += s;
                _content += "\n";
            }
            _content += "\n\nThank You so much..!!\n";
            suggestionsTextBox.Text = _content;
        }

        private void SuggestionWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
        }
    }
}

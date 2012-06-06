using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Libraries.Forms.Components.Behaviors
{
    public class AutoCompleter
    {
        private readonly ListBox suggestions;
        private readonly TextBox search;

        private readonly SortedSet<string> _source;
        private readonly AutoCompleteStringCollection _autoComplete;


        /// <summary>
        /// Wires auto-complete functionality between <param name="search"></param> and <param name="suggestions"></param>
        /// </summary>
        /// <param name="search">TextBox to search</param>
        /// <param name="suggestions">ListBox to populate with suggestions</param>
        /// <param name="autoCompleteStrings">Strings to suggest to user</param>
        public AutoCompleter(TextBox search, ListBox suggestions, IEnumerable<string> autoCompleteStrings )
        {
            this.search = search;
            this.suggestions = suggestions;
            _source = new SortedSet<string>(autoCompleteStrings);

            _autoComplete = new AutoCompleteStringCollection();

            suggestions.SelectedIndexChanged += Suggestions_SelectedIndexChanged;
            search.TextChanged += Search_OnTextChanged;
        }

        /// <summary>
        /// When an item is clicked in the <code>suggestions</code> ListBox
        /// Then set the <code>search</code> text to the text of that item
        /// </summary>
        private void Suggestions_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            search.Text = suggestions.SelectedItem.ToString();
        }

        void Search_OnTextChanged(object sender, System.EventArgs eventArgs)
        {
            if (search.Text.Length < 3)
            {
                suggestions.Items.Clear();
                return;
            }

            if (!suggestions.Visible) suggestions.Visible = true;

            var currentSearchText = search.Text.ToLower();

            foreach (var possibleSuggestion in _source)
            {
                var alreadySuggested = suggestions.Items.Contains(possibleSuggestion);

                var startsWith = 
                    possibleSuggestion.ToLower().StartsWith(
                        currentSearchText, 
                        StringComparison.InvariantCultureIgnoreCase);

                if (startsWith && !alreadySuggested)
                {
                    suggestions.Items.Add(possibleSuggestion);
                }
                else if (!startsWith && alreadySuggested)
                {
                    suggestions.Items.Remove(possibleSuggestion);
                }
            }
        }

    }
}

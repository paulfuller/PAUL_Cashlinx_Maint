using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Common.Libraries.Forms.Pawn.Products.DescribeMerchandise;
using NUnit.Framework;
using TechTalk.SpecFlow;
using System.Collections.Specialized;

namespace CommonTests.Libraries.Pawn.Products.DescribeMerchandise
{
    public interface IAutoCompleter
    {
        TextBox Entry { get; }
        ListBox Suggestions { get; }        
    }

    public class OldBox : FindMerchandise, IAutoCompleter
    {
        public OldBox()
            : base(null)
        {
        }

        public TextBox Entry { get { return manufacturerTextBox; } }
        public ListBox Suggestions { get { return suggestedManufacturerListBox; } }
    }

    public class NewBox : NewFindMerchandise, IAutoCompleter
    {
        public NewBox()
            : base(null)
        {
        }

        public TextBox Entry { get { return manufacturerTextBox; } }
        public ListBox Suggestions { get { return suggestedManufacturerListBox; } }
    }


    [Binding]
    public class AutoCompleterSteps
    {
        private OldBox _old = new OldBox();
        private NewBox _new = new NewBox();

        [Given("the following hints for autocomplete")]
        public void given_the_following_hints_for_autocomplete(string hints)
        {
            var hintCollection = hints.Split(
                new[]
                {
                    "\r\n"
                }, StringSplitOptions.RemoveEmptyEntries);

        }

        private void STAAction(IAutoCompleter completer, Action<IAutoCompleter> action)
        {
            var t = new Thread(() => action(completer));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        [Given("I have entered (.*)")]
        public void given_I_have_entered(string text)
        {
            Action<IAutoCompleter> setText = b =>
            {
                b.Entry.Text = text;
            };

            STAAction(_old, setText);
            STAAction(_new, setText);
        }

        [When("I press (.*)")]
        public void when_I_press(string key)
        {
            Action<IAutoCompleter> appendText = b => b.Entry.AppendText(key);
            STAAction(_old, appendText);
            STAAction(_new, appendText);
        }

        [Then("the autocomplete suggestions should include (.*)")]
        public void then_the_autocomplete_suggestions_should_include(string result)
        {
            Func<IAutoCompleter, bool> suggestionsContains =
                b => b.Suggestions.Items.Contains(result);

            var error = result + " was not there";
            Assert.IsTrue(suggestionsContains(_old), error);
            Assert.IsTrue(suggestionsContains(_new), error);
        }

        [Then("there should be (.*) autocomplete suggestions")]
        public void then_the_autocomplete_suggestions_should_have_count_entries(int count)
        {
            Func<IAutoCompleter, int> countEntries =
                b => b.Suggestions.Items.Count;

            Assert.AreEqual(countEntries(_old), countEntries(_new), "New and old are behaving differently");

            Assert.AreEqual(count, countEntries(_old), "Old had an unexpected number of entries");
            Assert.AreEqual(count, countEntries(_new), "New had an unexpected number of entries");
        }

        [Then("the autocomplete suggestions should be empty")]
        public void then_the_autocomplete_suggestions_should_be_empty()
        {
            then_the_autocomplete_suggestions_should_have_count_entries(0);
        }

    }
}

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
    public class MerchandiseBox : FindMerchandise
    {
        public MerchandiseBox() : base(null)
        {
        }

        public TextBox Entry { get { return manufacturerTextBox; } }
        public ListBox Suggestions { get { return suggestedManufacturerListBox; } }

    }


    [Binding]
    public class TypeAheadSteps
    {
        private MerchandiseBox f = new MerchandiseBox();

        [Given("the following hints for typeahead")]
        public void given_the_following_hints_for_typeahead(string hints)
        {
            var hintCollection = hints.Split(
                new[]
                {
                    "\r\n"
                }, StringSplitOptions.RemoveEmptyEntries);

        }

        private void DoSomethingSTA(Action action)
        {
            var t = new Thread(() =>
                               {
                                   action();
                               });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
        }

        [Given("I have entered (.*)")]
        public void given_I_have_entered(string first)
        {

            DoSomethingSTA(() =>
                           {
                               f = new MerchandiseBox(); 
                               f.Entry.Text = first;
                           });
        }

        [When("I press (.*)")]
        public void when_I_press(string key)
        {
            DoSomethingSTA(() => f.Entry.AppendText(key));
        }

        [Then("the typeahead options should include (.*)")]
        public void then_the_typeahead_options_should_include(string result)
        {
            Assert.IsTrue(f.Suggestions.Items.Contains(result), result+" was not there");
        }

        [Then("there should be (.*) typeahead option.*")]
        public void then_the_typeahead_options_should_have_count_entries(int count)
        {
            Assert.AreEqual(count, f.Suggestions.Items.Count);
        }

        [Then("the typeahead options should be empty")]
        public void then_the_typeahead_options_should_be_empty()
        {
            then_the_typeahead_options_should_have_count_entries(0);
        }



    }
}

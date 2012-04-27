using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Common.Libraries.Objects.Rules.Structure;
using PawnRulesManagerWPF.Business;

namespace PawnRulesManagerWPF.Views
{
    /// <summary>
    /// Interaction logic for EditBusinessRule.xaml
    /// </summary>
    public partial class EditBusinessRule : UserControl
    {
        private BusinessRuleNodeVO _rule = null;
        private bool _isReadOnly;
        private bool _isAdd = false;
        private string _origName = "";
        //RuleAction _action;

        public EditBusinessRule(BusinessRuleNodeVO rule, bool isReadOnly)
        {
            InitializeComponent();

            _rule = rule;
            
            _isReadOnly = isReadOnly;
            
            if(isReadOnly)
            {
                this.cmdCancel.Visibility = Visibility.Collapsed;
                this.cmdOkay.Visibility = Visibility.Collapsed;
                this.txtRuleName.IsReadOnly = true;
            }

            if (_rule == null)
            {
                _rule = new BusinessRuleNodeVO("", Guid.NewGuid());
                _rule.ComponentList =
                    new System.Collections.ObjectModel.ObservableCollection<BusinessRuleComponentVO>
                        ();
                _isAdd = true;
            }
            _origName = _rule.Code;

            this.DataContext = _rule;


            var total = _rule.ComponentList.Traverse(x => x.Children).Sum(x => x.Children == null? 0 : x.Children.Count());
            total += _rule.ComponentList == null ? 0 : _rule.ComponentList.Count();
            txtComponentCount.Text = total.ToString();
        }

        private void cmdOkay_Click(object sender, RoutedEventArgs e)
        {
           
            if(this.txtRuleName.Text.Trim() == "")
            {
                MessageBox.Show("Please enter a rule name.", "No Rule Name");

            }
            else if (App.RulesTreeView.RuleNameAlreadyInUse(this.txtRuleName.Text, _isAdd))
            {
                MessageBox.Show(
                    "This rule name is already being used.  Please enter a new rule name.", "Rule Name Already Exists");                
            }
            else
            {
                
                App.RulesTreeView.UpdateBusinessRules(_rule, _isAdd);
                
                ControlHelper.CloseParentWindow(this);
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            //If was an edit, since this is bound to the tree 
            //make sure the code is et back to the same.
            //if(!_isAdd)
            //{
            //   this._rule.Code = _origName;
            //}
            ControlHelper.CloseParentWindow(this);
        }
    }
}

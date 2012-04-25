using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Windows;
using System.Windows.Controls;
using Common.Libraries.Objects.Rules.Structure;
using PawnRulesManagerWPF.Business;

namespace PawnRulesManagerWPF.Views
{
    /// <summary>
    /// Interaction logic for EditBusinessRuleComponent.xaml
    /// </summary>
    public partial class EditBusinessRuleComponent : UserControl
    {
        BusinessRuleNodeVO _parentRule = null;
        BusinessRuleComponentVO _parentComponent = null;
        BusinessRuleComponentVO _component = null;
        private bool _isReadOnly = true;
        private bool _isAdd = false;

        public EditBusinessRuleComponent(BusinessRuleComponentVO component, BusinessRuleComponentVO parent, bool isReadOnly)
        : this(component, isReadOnly)
        {
            _parentComponent = parent;
            if (_parentComponent != null)
            {
                _component.ParentId = _parentComponent.Id;
            }
        }

        public EditBusinessRuleComponent(BusinessRuleComponentVO component, BusinessRuleNodeVO parent, bool isReadOnly)
        : this(component, isReadOnly)
        {
            _parentRule = parent;
        }

        public EditBusinessRuleComponent(BusinessRuleComponentVO component, bool isReadOnly)
        {
            InitializeComponent();

            _component = component;
            _isReadOnly = isReadOnly;

            //Add new component.
            if (_component == null)
            {
                _isAdd = true;
                _component = new BusinessRuleComponentVO(Guid.NewGuid(), "", BusinessRuleComponentVO.RuleValueType.PARAM, true);
                
                _component.FromDate = DateTime.Parse("1/1/1950");
                _component.ToDate = DateTime.Parse("1/1/2050");

                //Go ahead and create an empty list of children so that
                //the tree view nodes get the notify property changed event when a child is added.
                _component.Children =
                new System.Collections.ObjectModel.ObservableCollection<BusinessRuleComponentVO>();
            }

            if (!_component.IsEditable)
            {
                _isReadOnly = false;
            }

            this.DataContext = _component;            
            InitControls();
        }

        private void InitControls()
        {
            if (_isReadOnly)
            {
                ControlHelper.RecurseTree(this, true);                
            }
            
            cboCompTypes.ItemsSource = Enum.GetValues(
                typeof (BusinessRuleComponentVO.RuleValueType));
        }

        private void cmdOkay_Click(object sender, RoutedEventArgs e)
        {
            if (this._component.Code.Trim() == "")
            {
                MessageBox.Show("Component must have a name.", "No Name");
            }
            else
            {
                App.RulesTreeView.UpdateBusinessComponent(_component, _parentRule, _isAdd);
                ControlHelper.CloseParentWindow(this);
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            ControlHelper.CloseParentWindow(this);
        }

        private void cboCompTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BusinessRuleComponentVO.RuleValueType value = (BusinessRuleComponentVO.RuleValueType)e.AddedItems[0];

            grpParam.Visibility = Visibility.Collapsed;
            grpInterest.Visibility = Visibility.Collapsed;
            grpFee.Visibility = Visibility.Collapsed;
            grpMethod.Visibility = Visibility.Collapsed;

            if (value == BusinessRuleComponentVO.RuleValueType.PARAM)
                grpParam.Visibility = Visibility.Visible;
            if (value == BusinessRuleComponentVO.RuleValueType.INTEREST)
                grpInterest.Visibility = Visibility.Visible;
            if (value == BusinessRuleComponentVO.RuleValueType.FEES)
                grpFee.Visibility = Visibility.Visible;
            if (value == BusinessRuleComponentVO.RuleValueType.METHOD)
                grpMethod.Visibility = Visibility.Visible;
        }
    }
}
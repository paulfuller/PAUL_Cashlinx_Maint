using Common.Controllers.Rules.Data;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;

namespace PawnRulesManagerWPF.Views
{
    /// <summary>
    /// Interaction logic for ComponentList.xaml
    /// </summary>
    public partial class ComponentListWindow : Window
    {
        #region Private Members

        private bool _isSelection = false;

        #endregion

        #region Constructor
        
        public ComponentListWindow(bool isSelection)
        {
            InitializeComponent();

            _isSelection = isSelection;
            if (_isSelection)
            {
                spCommands.Visibility = Visibility.Collapsed;
                spSelect.Visibility = Visibility.Visible;                
            }
        }

        #endregion Constructor

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            EditBusinessRuleCompWindow w = new EditBusinessRuleCompWindow();
            w.ShowDialog();

            Refresh();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            BusinessRuleComponentVO c = (BusinessRuleComponentVO)lvComponents.SelectedItem;
            EditComponent(c);
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Are you sure you want to permanently delete this component?", "Are you sure?", MessageBoxButton.YesNo);

            if (r == MessageBoxResult.Yes)
            {
                BusinessRuleComponentVO c = (BusinessRuleComponentVO)lvComponents.SelectedItem;
                c.Deleted = true;
                App.RulesTreeView.UpdateBusinessComponent(c, null, true);
            }
            Refresh();
        }

        private void cmdSelect_Click(object sender, RoutedEventArgs e)
        {
            if (lvComponents.SelectedItem == null)
            {
                MessageBox.Show("You must first select a component to add.", "Select a component");
            }
            else
            {
                SelectComponent((BusinessRuleComponentVO)lvComponents.SelectedItem);
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmdView_Click(object sender, RoutedEventArgs e)
        {
            BusinessRuleComponentVO c = (BusinessRuleComponentVO)lvComponents.SelectedItem;
            if (c == null)
            {
                MessageBox.Show("Please select a component to view.");
            }
            else
            {
                ViewComponent(c);
            }
        }

        protected void HandleDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BusinessRuleComponentVO comp = ((ListViewItem)sender).Content as BusinessRuleComponentVO; //Casting back to the binded Track

            if (_isSelection)
            {
                SelectComponent(comp);
            }
            else
            {
                EditComponent(comp);
            }
        }

        #endregion Events

        #region Private Methods

        private void SelectComponent(BusinessRuleComponentVO c)
        {
            App.RulesTreeView.AddExistingComponent(c);
            this.Close();

        }

        private void ViewComponent(BusinessRuleComponentVO c)
        {
            EditBusinessRuleCompWindow w = new EditBusinessRuleCompWindow(Utilities.DeepCopy(c), true);

            w.ShowDialog();

        }
        
        private void Refresh()
        {
            List<BusinessRuleComponentVO> list = RulesHelper.GetComponentList();
            if (_isSelection)
            {
                //Only show parent components.
                list = (from l in list
                        where l.ParentId == Guid.Empty
                        select l).ToList();
            }

            lvComponents.ItemsSource = list;
        }

        private void EditComponent(BusinessRuleComponentVO c)
        {
            EditBusinessRuleCompWindow w = new EditBusinessRuleCompWindow(Utilities.DeepCopy(c), false);
            w.ShowDialog();

            Refresh();

        }

        #endregion Private Methods
    }
}

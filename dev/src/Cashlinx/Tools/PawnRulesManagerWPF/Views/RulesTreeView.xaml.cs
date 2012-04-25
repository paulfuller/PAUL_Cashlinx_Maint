using Common.Controllers.Rules.Data;
using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using PawnRulesManagerWPF.Business;

namespace PawnRulesManagerWPF.Views
{
    /// <summary>
    /// Interaction logic for RulesTreeView.xaml
    /// </summary>
    public partial class RulesTreeView : UserControl
    {
        #region Private Methods

        private ObservableCollection<RootNode> _rootNode = null;
        private RulesChangeTracker _tracker = new RulesChangeTracker();
        private bool _isDirty = false;
        private object _selectedNode = null;
        private bool _isLoadedFromFile = false;
        #endregion Private Methods

        #region Constructor

        public RulesTreeView()
        {
            InitializeComponent();

            RulesHelper.CreateWorkableTempFile();

            _tracker.DataChanged += new RulesChangeTracker.DataChangeHandler(_tracker_DataChanged);
            RulesHelper.DataCommitted += new RulesHelper.ChangesCommittedHandler(_tracker_DataCommitted);

            InitTreeData();

            treeRules.ContextMenu = treeRules.Resources["NodeContext"] as System.Windows.Controls.ContextMenu;

        }

        #endregion Constructor

        #region Events

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //After the control is loaded expand the top level.
            ControlHelper.TreeViewHelper.ExpandAndSelectTopLevel(this.treeRules);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            AttemptSave();
        }

        private void ItemAdd_Click(object sender, RoutedEventArgs e)
        {
            NodeAdded();
        }

        private void ItemEdit_Click(object sender, RoutedEventArgs e)
        {
            NodeEditted();
        }

        private void ItemDelete_Click(object sender, RoutedEventArgs e)
        {
            NodeDeleted();
        }

        private void cmdAdd_Click(object sender, RoutedEventArgs e)
        {
            NodeAdded();
        }

        private void cmdEdit_Click(object sender, RoutedEventArgs e)
        {
            NodeEditted();
        }

        private void cmdDelete_Click(object sender, RoutedEventArgs e)
        {
            NodeDeleted();
        }

        private void treeRules_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _selectedNode = e.NewValue;
            if (_selectedNode == null) return;

            //Clear out the children nodes to make room for the new node details
            spNodeDetails.Children.Clear();

            if (_selectedNode.GetType() == typeof(BusinessRuleNodeVO))
                spNodeDetails.Children.Add(new EditBusinessRule((BusinessRuleNodeVO)_selectedNode, true));
            else if (_selectedNode.GetType() == typeof(BusinessRuleComponentVO))
                spNodeDetails.Children.Add(new EditBusinessRuleComponent((BusinessRuleComponentVO)_selectedNode, true));
            else
                spNodeDetails.Children.Add(new BusinessRulesDetails());

            //TreeViewItem tvi = e.NewValue as TreeViewItem;
            //if (tvi != null)
            //    tvi.BringIntoView();
            if (_selectedNode.GetType() == typeof(BusinessRuleNodeVO))
            {
                cmdAddExisting.IsEnabled = true;
            }
            else
            {
                cmdAddExisting.IsEnabled = false;
            }
            ContextMenu menu = (ContextMenu)this.treeRules.FindResource("NodeContext");
            //ContextMenu menu = (ContextMenu)this.FindResource("treeMenu");
            foreach (MenuItem m in menu.Items)
            {
                if (m.Name == "miAddExisting")
                {
                    m.IsEnabled = cmdAddExisting.IsEnabled;
                }
            }

        }

        private void treeRules_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
        }

        private void treeRules_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            TreeViewItem treeViewItem = ControlHelper.TreeViewHelper.VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
        }

        private void cmdAddExisting_Click(object sender, RoutedEventArgs e)
        {
            ComponentListWindow w = new ComponentListWindow(true);
            w.ShowDialog();
        }

        private void cmdComponentList_Click(object sender, RoutedEventArgs e)
        {
            ComponentListWindow w = new ComponentListWindow(false);
            w.ShowDialog();
        }

        private void cmdOtherRules_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog d = new Microsoft.Win32.OpenFileDialog();
            d.DefaultExt = ".xml";
            d.Filter = "XML documents (.xml)|*.xml";
            d.CheckFileExists = true;
            d.CheckPathExists = true;
            d.Multiselect = false;
            d.Title = "Please open the rules file.";
            // Show open file dialog box
            bool? result = d.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = d.FileName;

                //Backup original file.
                //RulesHelper.CreateBackup();
                //Create file from backup
                RulesHelper.LoadFromFile(filename);

                InitTreeData();
                _isLoadedFromFile = true;
                _tracker_DataChanged();
            }



        }

        private void cmdExit_Click(object sender, RoutedEventArgs e)
        {
            ControlHelper.CloseParentWindow(this);
        }


        #endregion Events

        #region Private Methods

        private void NodeAdded()
        {            
            //If nothing has been selected or the the 
            //selected node is the top node then treat as an add 
            //to business rule else businessrule component.
            if (_selectedNode == null ||
            _selectedNode.GetType() == typeof(RootNode))
            {
                EditBusinessRule(null);
            }
            else if (_selectedNode.GetType() == typeof(BusinessRuleNodeVO))
            {
                EditBusinessRuleComponent(null, (BusinessRuleNodeVO)_selectedNode);
            }
            else if (_selectedNode.GetType() == typeof(BusinessRuleComponentVO))
            {
                EditBusinessRuleComponent(null, (BusinessRuleComponentVO)_selectedNode);
            }

        }

        private void NodeEditted()
        {
            if (_selectedNode == null ||
                 _selectedNode.GetType() == typeof(RootNode))
            {
                MessageBox.Show("Please select a business rule or component to edit.", "Select a Node");
            }
            else if (_selectedNode.GetType() == typeof(BusinessRuleNodeVO))
            {
                EditBusinessRule((BusinessRuleNodeVO)_selectedNode);
            }
            else if (_selectedNode.GetType() == typeof(BusinessRuleComponentVO))
            {
                EditBusinessRuleComponent((BusinessRuleComponentVO)_selectedNode);
            }


        }

        private void NodeDeleted()
        {
            if (_selectedNode == null ||
                _selectedNode.GetType() == typeof(RootNode))
            {
                MessageBox.Show("Please select a rule or component to delete.");
                return;
            }
            MessageBoxResult result = MessageBox.Show(
                "Are you SURE you want to delete this item?", "Are you sure??", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (_selectedNode.GetType() ==
                    typeof(BusinessRuleNodeVO))
                {
                    DeleteBusinessRule((BusinessRuleNodeVO)_selectedNode);
                }
                else if (_selectedNode.GetType() ==
                         typeof(BusinessRuleComponentVO))
                {
                    DeleteBusinessRuleComponent((BusinessRuleComponentVO)_selectedNode);
                }
                this.spNodeDetails.Children.Clear();
            }
        }

        private void EditBusinessRule(BusinessRuleNodeVO rule)
        {
            
            EditBusinessRuleWindow w;
            if (rule == null)
            {
                w = new EditBusinessRuleWindow(rule);
                
            } else
            {
                BusinessRuleNodeVO n = Utilities.DeepCopy<BusinessRuleNodeVO>(rule);
                w = new EditBusinessRuleWindow(n);
            }
            w.ShowDialog();
        }

        private void EditBusinessRuleComponent(BusinessRuleComponentVO comp)        
        {
            EditBusinessRuleCompWindow w = new EditBusinessRuleCompWindow(comp, false);
            w.ShowDialog();
        }

        private void EditBusinessRuleComponent(BusinessRuleComponentVO comp, BusinessRuleComponentVO parent)
        {
            EditBusinessRuleCompWindow w = new EditBusinessRuleCompWindow(comp, parent, false);
            w.ShowDialog();
        }

        private void EditBusinessRuleComponent( BusinessRuleComponentVO comp, BusinessRuleNodeVO parent)
        {
            EditBusinessRuleCompWindow w = new EditBusinessRuleCompWindow(comp, parent, false);
            w.ShowDialog();
        }

        private void DeleteBusinessRule(BusinessRuleNodeVO rule)
        {
            rule.Deleted = true;
            _rootNode[0].Nodes.Remove(rule);
            _tracker.ChangedRules.Add(rule);
            _tracker.SaveChangesToWorkingFile();
        }

        private void DeleteBusinessRuleComponent(BusinessRuleComponentVO comp)
        {
            //Will need to delete
            comp.Deleted = true;

            //Remove the node from the visual tree in all instances.
            foreach(BusinessRuleNodeVO node in _rootNode[0].Nodes)
            {
                if (node.ComponentList != null && node.ComponentList.Count() > 0)
                {
                    FindAndRemoveChildNodes(node.ComponentList, comp);
                }
            }

            _tracker.ChangedComponents.Add(comp);
            _tracker.SaveChangesToWorkingFile();
        }


        private void FindAndRemoveChildNodes(ObservableCollection<BusinessRuleComponentVO> compList, BusinessRuleComponentVO comp)
        {
            BusinessRuleComponentVO removeComp = null;

            //Search the list for the Id, if it's found break.
            foreach(BusinessRuleComponentVO node in compList)
            {
                if(node.Children != null && node.Children.Count() > 0)
                {
                    FindAndRemoveChildNodes(node.Children, comp);
                }
                if (node.Id == comp.Id)
                {
                    //Found the node so remove and break from loop.
                    removeComp = node;
                    break;          
                }
            }
            
            if(removeComp != null)
            {
                compList.Remove(removeComp);
            }
        }

        void _tracker_DataCommitted()
        {
            this.cmdSave.IsEnabled = false;
            _isDirty = false;
        }

        void _tracker_DataChanged()
        {
            
            this.cmdSave.IsEnabled = true;
            _isDirty = true;
        }

        private void ReplaceComponentNodes(BusinessRuleComponentVO replacementNode)
        {
            foreach(BusinessRuleNodeVO r in _rootNode[0].Nodes)
            {
                if (r.ComponentList != null)
                {
                    foreach (BusinessRuleComponentVO c in r.ComponentList)
                    {
                        SearchReplaceChildNodes(c, replacementNode);
                    }
                }
            }
        }

        private void AddComponentNode(BusinessRuleComponentVO newNode, BusinessRuleNodeVO parentNode)
        {
            if (parentNode != null)
            {
                BusinessRuleNodeVO node = (from n in _rootNode[0].Nodes
                                           where n.Id == parentNode.Id
                                           select n).First();
                node.ComponentList.Add(newNode);
            }
            else
            {
                foreach (BusinessRuleNodeVO r in _rootNode[0].Nodes)
                {
                    if (r.ComponentList != null)
                    {
                        foreach (BusinessRuleComponentVO c in r.ComponentList)
                        {
                            SearchAddChildNodes(c, newNode);
                        }
                    }
                }        
            }

        }

        private void SearchAddChildNodes(BusinessRuleComponentVO parentComp, BusinessRuleComponentVO newNode)
        {
            if (newNode.ParentId == parentComp.Id)
            {
                if (parentComp.Children == null)
                    parentComp.Children = new ObservableCollection<BusinessRuleComponentVO>();
                parentComp.Children.Add(newNode);
            }
            if (parentComp.Children != null)
            {
                if (parentComp.Children.Count > 0)
                {
                    foreach (BusinessRuleComponentVO c in parentComp.Children)
                    {
                        SearchAddChildNodes(c, newNode);
                    }
                }
            }

        }


        private void SearchReplaceChildNodes(BusinessRuleComponentVO parentComp, BusinessRuleComponentVO replacementNode)
        {
            if(replacementNode.Id == parentComp.Id)
            {
                parentComp = replacementNode;
            }
            if (parentComp.Children != null)
            {
                if(parentComp.Children.Count > 0)
                {
                    foreach (BusinessRuleComponentVO c in parentComp.Children)
                    {
                        SearchReplaceChildNodes(c, replacementNode);
                    }
                }
            }

        }

        private void InitTreeData()
        {
            this.treeRules.DataContext = null;
            _rootNode = new ObservableCollection<RootNode>();
            _rootNode.Add(
                new RootNode
                {
                    Name = "Business Rules"
                });

            _rootNode[0].Nodes = RulesHelper.GetRulesHierarchy();

            this.treeRules.DataContext = _rootNode;

        }

        private void AttemptSave()
        {
            try
            {                
                MessageBoxResult result = MessageBoxResult.No;
                if (_isLoadedFromFile)
                {
                    result = MessageBox.Show(
                        "These rules were loaded from an archive file, are you sure you want to save them they will overwrite your current rules.xml file?",
                        "Are you sure?",
                        MessageBoxButton.YesNo);
                }

                if (!_isLoadedFromFile ||
                    result == MessageBoxResult.Yes)
                {
                    bool success = RulesHelper.CommitRules();
                    if (success)
                    {
                        MessageBox.Show("Rules were successfully saved.");
                        _isLoadedFromFile = false;
                    }
                }

            }catch(Exception ex)
            {          
                MessageBox.Show("There was a problem trying to save the business rules, please try again. ");
                FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Business Rules Manager has failed to save: " + ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }

        #endregion Private Methods

        #region Public Methods

        /// <summary>
        /// Returns if the business rule name can be found in the
        /// existing tree to prevent duplicate naming.  
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RuleNameAlreadyInUse(string name, bool isAdd)
        {
            //Find a better way to test -- 
            //if it's a new add it's not bound yet so any that is greater 
            //than 0 means one already exists.
            if (isAdd)
            {
                if ((from r in _rootNode[0].Nodes
                     where r.Code.ToUpper() == name.ToUpper().Trim()
                     select r).Count() > 0)
                    return true;
            }
            else
            {
                if ((from r in _rootNode[0].Nodes
                     where r.Code.ToUpper() == name.ToUpper().Trim()
                     select r).Count() > 1)
                    return true;
            }

            return false;
        }

        public void UpdateBusinessRules(BusinessRuleNodeVO rule, bool isAdd)
        {
            _tracker.ChangedRules.Add(rule);
            _tracker.SaveChangesToWorkingFile();
            if (isAdd)
            {
                _rootNode[0].Nodes.Add(rule);
                ControlHelper.TreeViewHelper.ExpandAndSelectTreeItem(this.treeRules, rule);

            }
            else
            {
                BusinessRuleNodeVO ruleNode = (from n in _rootNode[0].Nodes
                                               where n.Id == rule.Id
                                               select n).First<BusinessRuleNodeVO>();

                int idx = _rootNode[0].Nodes.IndexOf(ruleNode);
                _rootNode[0].Nodes[idx] = rule;
            }
        }

        public void UpdateBusinessComponent(BusinessRuleComponentVO comp, BusinessRuleNodeVO parentNode, bool isAdd)
        {
            //Adds to visual tree.
            if (isAdd)
            {
                //If it's a business rule as parent, will need to add the component
                //so that the relationship is updated.
                if (parentNode != null)
                {
                    if(parentNode.ComponentList == null)
                    {
                        parentNode.ComponentList =
                            new ObservableCollection<BusinessRuleComponentVO>();
                    }
                    parentNode.ComponentList.Add(comp);
                    
                }
                else
                {
                    AddComponentNode(comp, parentNode);
                }
                ControlHelper.TreeViewHelper.ExpandAndSelectTreeItem(this.treeRules, comp);                
                
            }
            else
            {
                ReplaceComponentNodes(comp);
            }

            //Add changes to change tracking.
            _tracker.ChangedComponents.Add(comp);
            if(parentNode != null)
                _tracker.ChangedRules.Add(parentNode);

            _tracker.SaveChangesToWorkingFile();
        }

        public void AddExistingComponent(BusinessRuleComponentVO comp)
        {
            if (_selectedNode is BusinessRuleNodeVO)
            {
                BusinessRuleNodeVO parentNode = (BusinessRuleNodeVO)_selectedNode;

                if (parentNode.ComponentList == null)
                {
                    parentNode.ComponentList =
                        new ObservableCollection<BusinessRuleComponentVO>();                
                }

                //If the comp has no children then add them so the tree can update.
                if(comp.Children == null)
                {
                    comp.Children = new ObservableCollection<BusinessRuleComponentVO>();
                }

                RulesHelper.PopulateComponentHeirarchy(comp);
                parentNode.ComponentList.Add(comp);

                _tracker.ChangedRules.Add(parentNode);
                _tracker.SaveChangesToWorkingFile();
                
                ControlHelper.TreeViewHelper.ExpandAndSelectTreeItem(this.treeRules, comp);
            }
        }

        public void PromptChangesNotSaved()
        {
            if (_isDirty)
            {
                MessageBoxResult result =
                    MessageBox.Show(
                        "You have unsaved changes, would you like to save them before leaving this form?",
                        "Unsaved Changes",
                        MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    AttemptSave();
                }
            }
        }

        #endregion Public Methods

        #region Class

        //ToDo: Find a better way.
        /// <summary>
        /// Since this is bound, haven't figured out how to set a
        /// root node so creating a class for this. 
        /// </summary>
        public class RootNode
        {
            public ObservableCollection<BusinessRuleNodeVO> Nodes { get; set; }
            public string Name { get; set; }
        }

        #endregion Class

    }
}

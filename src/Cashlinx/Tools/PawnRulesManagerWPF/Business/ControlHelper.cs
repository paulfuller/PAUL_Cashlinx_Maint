using Common.Libraries.Forms.Components;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Application;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;

namespace PawnRulesManagerWPF.Business
{
    public static class ControlHelper
    {
        public static void CloseParentWindow(Control control)
        {

            DependencyObject parent = control.Parent;

            while (parent != null && !(parent is Window))
                parent = LogicalTreeHelper.GetParent(parent);

            Window w = parent as Window;


            if (w != null)
                w.Close();
        }

        public static void RecurseTree(FrameworkElement control,
                                   bool isReadOnly)
        {
            foreach (var c in LogicalTreeHelper.GetChildren(control))
            {
                var frameworkElement = c as FrameworkElement;
                if (frameworkElement == null) continue;

                var tb = c as TextBox;
                if (tb != null)
                {
                    tb.IsReadOnly = isReadOnly;
                    continue;
                }

                if(c is Button)
                {
                    if (isReadOnly)
                    {
                        var button = c as Button;
                        button.Visibility = Visibility.Collapsed;
                    }
                }

                if (c is GroupBox || c is DockPanel || c is Grid
                    || c is StackPanel || c is TabControl)
                {
                    RecurseTree(frameworkElement, isReadOnly);
                }
                else
                {
                    frameworkElement.IsEnabled = !isReadOnly;
                }
            }
        }


        public static class TreeViewHelper
        {
            private static bool isItemSelected = false;

            public static void ExpandAndSelectTreeItem(TreeView treeView, object newItem)
            {
                isItemSelected = false;

                //ControlHelper.SelectItemInTreeView(newNode, this.treeRules);
                WaitFor(DispatcherPriority.SystemIdle);

                TreeViewItem treeItem = treeView.ItemContainerGenerator.ContainerFromItem(treeView.Items[0]) as TreeViewItem;

                // Expand TreeViewItem and then select the newly added item.
                ExpandTreeViewItemThenSelectAddedItem(treeItem, newItem);                
            }

            public static void ExpandAndSelectTopLevel(TreeView tree)
            {
                foreach (object item in tree.Items)
                {
                    TreeViewItem treeItem = tree.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                    treeItem.IsExpanded = true;
                    treeItem.IsSelected = true;
                }

            }

            public static void ExpandAllNodes(TreeView treeView)
            {
                ExpandSubContainers(treeView);
            }

            public static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
            {
                while (source != null && source.GetType() != typeof(T))
                    source = VisualTreeHelper.GetParent(source);

                return source;
            }

            private static void ExpandSubContainers(ItemsControl parentContainer)
            {
                foreach (Object item in parentContainer.Items)
                {
                    TreeViewItem currentContainer = parentContainer.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                    if (currentContainer != null && currentContainer.Items.Count > 0)
                    {
                        currentContainer.IsExpanded = true;
                        if (currentContainer.ItemContainerGenerator.Status != GeneratorStatus.ContainersGenerated)
                        {
                            currentContainer.ItemContainerGenerator.StatusChanged += delegate
                            {
                                ExpandSubContainers(currentContainer);
                            };
                        }
                        else
                        {
                            ExpandSubContainers(currentContainer);
                        }
                    }
                }
            }

            private static void ExpandTreeViewItemThenSelectAddedItem(TreeViewItem treeitem, object newItem)
            {
                if (treeitem == null) return;
                if (Object.ReferenceEquals(treeitem.Header, newItem))
                {
                    TreeViewItem parentItem = ControlHelper.TreeViewHelper.VisualUpwardSearch<TreeViewItem>(treeitem) as TreeViewItem;
                    parentItem.IsExpanded = true;

                    treeitem.IsSelected = true;
                    treeitem.BringIntoView();
                    //Focus supposedly brings treeitem into view.
                    //treeitem.Focus();                    
                    isItemSelected = true;
                    return;
                }
                //treeitem.IsExpanded = true;
                WaitFor(DispatcherPriority.SystemIdle);

                for (int i = 0; i < treeitem.Items.Count; i++)
                {
                    if (isItemSelected) break;
                    TreeViewItem item = (TreeViewItem)treeitem.ItemContainerGenerator.ContainerFromIndex(i);
                    ExpandTreeViewItemThenSelectAddedItem(item, newItem);
                }
            }

            private static void WaitFor(DispatcherPriority priority)
            {
                DispatcherTimer timer = new DispatcherTimer(priority);
                timer.Tick += new EventHandler(OnDispatched);
                DispatcherFrame dispatcherFrame = new DispatcherFrame(false);
                timer.Tag = dispatcherFrame;
                timer.Start();
                Dispatcher.PushFrame(dispatcherFrame);
                //((DispatcherObject)this).Dispatcher.PushFrame(dispatcherFrame);
            }

            private static void OnDispatched(object sender, EventArgs args)
            {
                DispatcherTimer timer = (DispatcherTimer)sender;
                timer.Tick -= new EventHandler(OnDispatched);
                timer.Stop();
                DispatcherFrame frame = (DispatcherFrame)timer.Tag;
                frame.Continue = false;
            }


        }
    }
}

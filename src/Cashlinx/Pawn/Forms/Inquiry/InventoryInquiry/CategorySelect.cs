using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Inquiry.InventoryInquiry
{
    public partial class CategorySelect : Form
    {
        private List<TreeNode> selectedCategories = new List<TreeNode>();

        public CategorySelect(DataSet treeDataSet)
        {
            if (treeDataSet != null)
            {
                DataTable treeData = treeDataSet.Tables["CATEGORIES"];

                InitializeComponent();

                var parents = new Stack<TreeNode>(10);
                int currentLevel = 0;
                TreeNode currentNode = null;

                foreach(DataRow r in treeData.Rows)
                {
                    int theLevel = decimal.ToInt32(r.Field<decimal>("LVL"));

                    if (theLevel == 1)
                    {
                        currentNode = new TreeNode(r.Field<string>("CAT_DESC"))
                                        {
                                            Tag = r.Field<int>("CAT_CODE")
                                        };

                        this.CategoryTree.Nodes.Add(currentNode);
                        currentLevel = theLevel;
                        parents.Clear();
                    }
                    else if (theLevel > currentLevel && currentNode != null)
                    {
                        currentLevel = theLevel;
                        if (parents.Count == 0 || currentNode != parents.Peek())
                            parents.Push(currentNode);
                        var aNewNode = new TreeNode(r.Field<string>("CAT_DESC"))
                                        {
                                            Tag = r.Field<int>("CAT_CODE")
                                        };
                        
                        currentNode.Nodes.Add(aNewNode);
                        currentNode = aNewNode;
                    }
                    else if (theLevel == currentLevel)
                    {
                        var aNewNode = new TreeNode(r.Field<string>("CAT_DESC"))
                                        {
                                            Tag = r.Field<int>("CAT_CODE")
                                        };
                        TreeNode theParent = parents.Peek();

                        if (theParent.Level + 2 == currentLevel)
                        {
                            theParent.Nodes.Add(aNewNode);
                            currentNode = aNewNode;
                        }

                    }

                    else if (theLevel < currentLevel && currentNode != null)
                    {
                        var aNewNode = new TreeNode(r.Field<string>("CAT_DESC"))
                                        {
                                            Tag = r.Field<int>("CAT_CODE")
                                        };
                         
                        TreeNode theParent = null;

                        do
                        {
                            if (parents.Count > 0)
                            {
                                parents.Pop(); 
                                theParent = parents.Peek();                                
                            }
                            
                        }
                        while (parents.Count > 0 && theParent != null && theParent.Level + 2 > theLevel);
                       
                        if (theParent.Level + 2 == theLevel)
                        {
                            theParent.Nodes.Add(aNewNode);
                            currentNode = aNewNode;
                            currentLevel = theLevel;
                        }

                    }

                }
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }



        public string[] getCatCodes ()
        {
            
            var query = from TreeNode s in this.selectedCategories                        
                        select s.Tag.ToString();

            return query.ToArray();
        }


        //--------------------
        public IEnumerable<TreeNode> topMostSelectedParent (IEnumerable<TreeNode> nodes)
        {
            if (nodes.Count() == 0)
                return nodes;


            var retval = from TreeNode n in nodes
                         where n.Parent == null  ||  // is a Top Parent
                               (n.Nodes.Count > 0 && // OR is a Parent
                                                    //n.Nodes.Count == (from TreeNode c in n.Nodes
                                                    //                            where c.Checked 
                                                    //                            select c).Count() && // Has all Children Checked
                                                    n.StateImageIndex == TriStateTreeView.IMAGE_INDEX_CHECKED && // who has all children checked
                                                    n.Parent.StateImageIndex != TriStateTreeView.IMAGE_INDEX_CHECKED // and whose parent doesn't have all children checked
                                                    )
                         select n; // Top most parent, or parent with all children checked



            if (retval.Count() == 0)
            {
                retval = topMostSelectedParent((from TreeNode n in nodes
                                                where n.Parent != null
                                                select n.Parent).Distinct());
            }
            else
            {
                var allParents = (from TreeNode n in nodes
                                  select n.Parent).Distinct();

                if (retval.Count() < allParents.Count())
                {
                    retval = retval.Concat(topMostSelectedParent(nodes.Except(retval)).Distinct());
                }
            }

            return retval;

        }



        //----------


        public string[] getCatNames()
        {

            var query = (from n in selectedCategories
                             where n.Parent.StateImageIndex != TriStateTreeView.IMAGE_INDEX_CHECKED
                             select n); // return only those selected categories that aren't part of a select All

            if (query.Count() < selectedCategories.Count)  // if there are selectedCategories that are part of select All, then only include the parents
                query = query.Concat(
                                 topMostSelectedParent(selectedCategories.Except( query))
                                 ).Distinct();
                           
                            
            



           return (from q in query select q.Text).ToArray();
        }

        private bool bulkChangeInProcess = false;

        private void clearParents (TreeNode node)
        {
            if (node != null)
            {
                node.Checked = false;

                if (node.Parent != null)
                    clearParents((node.Parent));
            }
                           
        }


        public void clear()
        {
            
            bulkChangeInProcess = true;

            foreach (var n in selectedCategories)
            {
                n.Checked = false;

                clearParents(n.Parent);
            }

            CategoryTree.Refresh();
            

            selectedCategories.Clear();
            bulkChangeInProcess = false;
            

        }

        //private IEnumerable<TreeNode> getAllChildren (IEnumerable<TreeNode> nodes)
        //{
        //    IEnumerable<TreeNode> parents = from TreeNode n in nodes
        //                  where n.Nodes.Count > 0
        //                  select n;

        //    IEnumerable<TreeNode> children = from TreeNode n in nodes
        //                  where n.Nodes.Count > 0
        //                  select n;

        //    if (parents.Count() == 0)
        //        return children;
        //    else
        //        return children.Concat((from TreeNode p in parents select getAllChildren(from TreeNode gc in p.Nodes select gc)).S;
        //}

        
        private void CategoryTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (bulkChangeInProcess)
                return;

            if (e.Node.Checked && e.Node.Nodes.Count == 0)
            {
                if (!selectedCategories.Contains(e.Node))
                    selectedCategories.Add(e.Node);
            }
            else if (e.Node.Nodes.Count == 0)
            {
                selectedCategories.Remove(e.Node);
            }

            /*
            if (bulkChangeInProcess)
                return;

            if (e.Node.Checked)
            {
                if (!selectedCategories.Contains(e.Node))
                {
                    
                    // Select children if any (select all)
                    //var test = e.Node.Nodes.
                    //    ;

                    var results  = from TreeNode n in e.Node.Nodes
                                  select n;

                    if (results.Count() > 0)
                    {
                        e.Node.ForeColor = Color.Gray;

                   
                        foreach (TreeNode r in results)
                        {
                            if (selectedCategories.Contains(r))
                                continue;

                            selectedCategories.Add(r);
                            r.Checked = true;
                        }                        
                    }

                    if (!selectedCategories.Contains(e.Node))
                        selectedCategories.Add(e.Node);



                    bulkChangeInProcess = true;
                    // need to set parents so as to provide some indication that sub-items are selected
                    for(TreeNode n = e.Node.Parent; n != null; n = n.Parent)
                    {
                        n.ForeColor = Color.Gray;
                        n.Checked = true;
                        if (!selectedCategories.Contains(n))
                            selectedCategories.Add(n);
                    }

                    bulkChangeInProcess = false;
                }

            }
                

            else
            {

                // remove any children selected
                var results = from TreeNode n in e.Node.Nodes
                                select n;

                if (results.Count() > 0)
                    e.Node.ForeColor = Color.Empty;

                bulkChangeInProcess = true;
                foreach (TreeNode r in results)
                {
                    r.ForeColor = Color.Empty;
                    r.Checked = false;
                    selectedCategories.Remove(r);
                }
                bulkChangeInProcess = false;



                selectedCategories.Remove(e.Node);

                IEnumerable<TreeNode> query = from n in selectedCategories
                                                where n.Parent == e.Node.Parent
                                                select n;
                    
                // clear parents if all associated children are cleared
                if (query.Count() == 0)
                {



                    bulkChangeInProcess = true;
                    for (TreeNode n = e.Node.Parent; n != null; n = n.Parent)
                    {
                        n.ForeColor = Color.Empty;
                        n.Checked = false;
                        selectedCategories.Remove(n);
                    }
                    bulkChangeInProcess = false;
                }

               



            }
*/                
        }


        private void ClearBtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void ContinueBtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }
    }
}

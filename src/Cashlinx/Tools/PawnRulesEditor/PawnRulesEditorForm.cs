using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using PawnRulesSystem;
using PawnUtilities.Collection;

namespace PawnRulesEditor
{
    public partial class PawnRulesEditor : Form
    {
        public enum FileStatus : uint 
        {
            NOTLOADED = 0,
            LOADED = 1,
            EDITED = 2,
            SAVED = 3
        };

        private FileStatus rulesFileStatus;       

        public PawnRulesEditor()
        {
            InitializeComponent();
            rulesFileStatus = FileStatus.NOTLOADED;
        }

        private void rulesDataTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void rulesDataTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void rulesDataTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void rulesDataTreeView_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {

        }

        private void rulesDataTreeView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void addRuleButton_Click(object sender, EventArgs e)
        {
            //Add a new business rule
            MessageBox.Show("Cannot add business rules at this time");
        }

        private void editRuleButton_Click(object sender, EventArgs e)
        {
            //Edit the hierarchy of the business rule
            MessageBox.Show("Cannot edit business rules at this time");
        }

        private void deleteRuleButton_Click(object sender, EventArgs e)
        {
            //Delete a business rule
            MessageBox.Show("Cannot delete business rules at this time");
        }

        private void addComponent_Click(object sender, EventArgs e)
        {

        }

        private void editComponentButton_Click(object sender, EventArgs e)
        {

        }

        private void removeComponentButton_Click(object sender, EventArgs e)
        {

        }

        private void loadRulesFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Load rules file
            //If one is already loaded, prompt to save
            OpenFileDialog openFileDialog;
            openFileDialog = new OpenFileDialog();
            openFileDialog.AddExtension = true;
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.DefaultExt = "xml";
            openFileDialog.Filter = "XML files (*.xml)|*.xml";
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = "c:\\";
            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                RulesHelper.LoadFromFile(openFileDialog.FileName);
                rulesFileStatus = FileStatus.LOADED;
                putXmlDataIntoTree();
            }
            else
            {
                MessageBox.Show("No rules file loaded.  Please try again");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Exit rules editor
            //If a rules file has pending changes, prompt to save
        }

        private void saveRulesFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Save rules file
            //If no rules file loaded, or no changes made, prompt warning
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            //Submit rules component change
            //Prompt message
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //Cancel rules component change
            //Prompt message
        }

        private void clearXmlDataTree()
        {
            if (this.rulesDataTreeView.Nodes != null)
            {
                this.rulesDataTreeView.BeginUpdate();
                this.rulesDataTreeView.Nodes.Clear();
                this.rulesDataTreeView.EndUpdate();
            }
        }

        private void addNodeToXmlDataTree(TreeNode parent, TreeNode node)
        {
            if (node == null)
                return;

            if (parent == null)
            {
                //Insert at the root
                this.rulesDataTreeView.BeginUpdate();
                this.rulesDataTreeView.Nodes.Add(node);
                this.rulesDataTreeView.EndUpdate();
            }
            else
            {
                //Add node to parent
                //parent.Nodes.Add(node);
                TreeNode[] nodes = this.rulesDataTreeView.Nodes.Find(parent.Name, true);
                if (nodes != null && nodes.Length > 0)
                {
                    //Pick first node
                    TreeNode root = nodes[0];
                    if (root != null)
                    {
                        root.Nodes.Add(node);
                    }
                }
            }
        }

        private void putXmlDataIntoTree()
        {
            //Ensure the file is loaded
            if (rulesFileStatus != FileStatus.LOADED)
            {
                return;
            }
            //Clear the tree first
            clearXmlDataTree();

            //Add root node
            TreeNode rootNode = new TreeNode("BusinessRules");
            addNodeToXmlDataTree(null, rootNode);

            //Get document from RulesXML class
            XDocument doc = RulesHelper.RulesXML.Instance.Document;
            var elAfterSelf = doc.Descendants("BusinessRule");
            if (elAfterSelf != null && elAfterSelf.Count() > 0)
            {
                //this.rulesDataTreeView.BeginUpdate();
                this.rulesDataTreeView.BeginUpdate();
                foreach (var curEl in elAfterSelf)
                {
                    if (curEl == null)continue;

                    TreeNode newNode = new TreeNode(curEl.Name.ToString());
                    
                    //if 

                    addNodeToXmlDataTree(rootNode, newNode);
                }
                this.rulesDataTreeView.EndUpdate();
            }
        }
    }
}

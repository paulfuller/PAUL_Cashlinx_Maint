using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Common.Libraries.Forms.Components
{
    public class TriStateTreeView : TreeView
    {
        private const int CHECKBOX_WIDTH = 16;
        private const int CHECKBOX_HEIGHT = 16;

        public const int IMAGE_INDEX_UNCHECKED = 0;
        public const int IMAGE_INDEX_CHECKED = 1;
        public const int IMAGE_INDEX_MIXED = 2;

        # region Constructors

        public TriStateTreeView()
            : base()
        {
            StateImageList = new ImageList();
            StateImageList.Images.Add(GenerateCheckboxStateImage(CheckBoxState.UncheckedNormal));
            StateImageList.Images.Add(GenerateCheckboxStateImage(CheckBoxState.CheckedNormal));
            StateImageList.Images.Add(GenerateCheckboxStateImage(CheckBoxState.MixedNormal));
        }

        # endregion

        # region Properties

        [Browsable(false)]
        public new ImageList StateImageList
        {
            get { return base.StateImageList; }
            set { base.StateImageList = value; }
        }

        private bool PreventCheckEvent { get; set; }

        # endregion

        # region Public Methods

        public override void Refresh()
        {
            base.Refresh();
            base.CheckBoxes = false;

            InitializeStateImageIndex();
        }

        # endregion

        # region Protected Methods

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            Refresh();
        }

        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            base.OnAfterExpand(e);

            foreach (TreeNode tn in e.Node.Nodes)
            {
                SetStateImage(tn);
            }
        }

        protected override void OnAfterCheck(TreeViewEventArgs e)
        {
            base.OnAfterCheck(e);

            if (PreventCheckEvent)
            {
                return;
            }

            OnNodeMouseClick(new TreeNodeMouseClickEventArgs(e.Node, MouseButtons.None, 0, 0, 0));
        }

        protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            base.OnNodeMouseClick(e);

            PreventCheckEvent = true;

            if ((e.X > e.Node.Bounds.Left || e.X < (e.Node.Bounds.Left - CHECKBOX_WIDTH)) && e.Button != MouseButtons.None)
            {
                return;
            }

            TreeNode node = e.Node;
            if (e.Button == MouseButtons.Left)
            {
                node.Checked = !node.Checked;
            }

            node.StateImageIndex = node.Checked ? IMAGE_INDEX_CHECKED : IMAGE_INDEX_UNCHECKED;

            OnAfterCheck(new TreeViewEventArgs(node, TreeViewAction.ByMouse));

            SetAllChildNodesCheckedState(e.Node);
            SetAllParentImageStates(e.Node);

            PreventCheckEvent = false;
        }

        # endregion

        # region Helper Methods

        private Bitmap GenerateCheckboxStateImage(CheckBoxState state)
        {
            Bitmap bitmap = new Bitmap(CHECKBOX_WIDTH, CHECKBOX_HEIGHT);
            Graphics graphics = Graphics.FromImage(bitmap);
            CheckBoxRenderer.DrawCheckBox(graphics, new Point(2, 2), state);
            graphics.Save();
            return bitmap;
        }

        private void InitializeStateImageIndex()
        {
            Stack<TreeNode> nodes = new Stack<TreeNode>(this.Nodes.Count);

            foreach (TreeNode tn in this.Nodes)
            {
                nodes.Push(tn);
            }

            while (nodes.Count > 0)
            {
                TreeNode tn = nodes.Pop();
                SetStateImage(tn);
                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    nodes.Push(tn.Nodes[i]);
                }
            }
        }

        private void SetAllChildNodesCheckedState(TreeNode node)
        {
            bool checkedState = node.Checked;
            Stack<TreeNode> stNodes = new Stack<TreeNode>(node.Nodes.Count);
            stNodes.Push(node);
            do
            {
                node = stNodes.Pop();
                node.Checked = checkedState;
                node.StateImageIndex = node.Checked ? IMAGE_INDEX_CHECKED : IMAGE_INDEX_UNCHECKED;
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    stNodes.Push(node.Nodes[i]);
                }
            } while (stNodes.Count > 0);
        }

        private void SetAllParentImageStates(TreeNode node)
        {
            bool bMixedState = false;
            while (node.Parent != null)
            {
                foreach (TreeNode tnChild in node.Parent.Nodes)
                {
                    bMixedState |= (tnChild.Checked != node.Checked | tnChild.StateImageIndex == IMAGE_INDEX_MIXED);
                }

                int state = (int)Convert.ToUInt32(node.Checked);
                node.Parent.Checked = bMixedState || (state > 0);

                if (bMixedState)
                {
                    node.Parent.StateImageIndex = IMAGE_INDEX_MIXED;
                }
                else
                {
                    node.Parent.StateImageIndex = state;
                }

                node = node.Parent;
            }
        }

        private void SetStateImage(TreeNode tn)
        {
            if (tn.StateImageIndex == -1)
            {
                tn.StateImageIndex = tn.Checked ? 1 : 0;
            }
        }

        # endregion
    }
}

/**************************************************************************************************************
* CashlinxDesktop
* LocationsTreenode
* This user control is used to provide the individual nodes on the LoanInquiryLocations treeview
* S.Murphy 2/14/2010 Initial version
**************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.LoanInquiry;

namespace Pawn.Forms.UserControls
{
    public partial class ShopsTreenode : UserControl
    {
        public enum NodeLevel
        { Company, Region, Market, Shop }
        public enum NodeState
        { Collapsed, Expanded, None }

        [Category("Level")]
        [Description("The Level of the Location")]
        [DefaultValue(NodeLevel.Shop)]
        public NodeLevel Level { set; get; }
        [Category("State")]
        [Description("The State (Collapsed/Expanded/None) of the Location")]
        [DefaultValue(NodeState.Collapsed)]
        public NodeState State { set; get; }
        [Category("Inset")]
        [Description("The visual inset in pixels for the Location position")]
        [DefaultValue(0)]
        public int Inset { set; get; }
        [Category("NodeParent")]
        [Description("The parent node  for the Location")]
        [DefaultValue(0)]
        public int NodeParent { set; get; }
        [Category("NodeRegion")]
        [Description("The Node Region for the Location")]
        [DefaultValue(1)]
        public int NodeRegion { set; get; }
        [Category("NodeMarket")]
        [Description("The Node market for the Location")]
        [DefaultValue(1)]
        public int NodeMarket { set; get; }
        [Category("NodeID")]
        [Description("The unique ID for the Location")]
        [DefaultValue(0)]
        public int NodeID { set; get; }
        [Category("NodeValue")]
        [Description("The Node Value for the Location")]
        [DefaultValue("")]
        public string NodeValue{ set; get; }
        [Category("Label")]
        [Description("The Label for the Location")]
        [DefaultValue("Cash America")]
        public Label Label
        {
            get
            {
                return this.NodeLabel;
            }
            set
            {
                this.NodeLabel = value;
            }
        }
        [Category("Checkbox")]
        [Description("The Checkbox for the Location")]
        [DefaultValue(null)]
        public CheckBox Checkbox
        {
            get
            {
                return this.NodeCheckBox;
            }
            set
            {
                this.NodeCheckBox = value;
            }
        }
     
        private Int16 _count = 0;

        public ShopsTreenode()
        {
            InitializeComponent();
        }
        private ShopsTreenode(NodeState state)
        {
            InitializeComponent();

            switch (state)
            {
                case (NodeState.Expanded):
                    this.NodePictureBox.Image = global::Common.Properties.Resources.Expanded;
                    break;
                case (NodeState.Collapsed):
                    this.NodePictureBox.Image = global::Common.Properties.Resources.Collapsed;
                    break;
                case (NodeState.None):
                    this.NodePictureBox.Image = null;
                    break;
            }
        }
        //[Category("Picturebox")]
        //[Description("The Expand/Collapse Image for the Location")]
        //[DefaultValue(null)]
        //form events
        private void NodePictureBox_Click(object sender, EventArgs e)
        {
            //get the current instance of the form
            LoanInquiryShops loanInquiryLocations = (LoanInquiryShops)this.Parent.Parent.Parent;

            if (this.State.Equals(ShopsTreenode.NodeState.Collapsed))
            {
                this.NodePictureBox.Image = global::Common.Properties.Resources.Expanded;
                this.State = ShopsTreenode.NodeState.Expanded;
                Expand();
            }
            else if (this.State.Equals(ShopsTreenode.NodeState.Expanded))
            {
                this.NodePictureBox.Image = global::Common.Properties.Resources.Collapsed;
                this.State = ShopsTreenode.NodeState.Collapsed;
                Collapse();
            }

            this.PerformLayout();
        }
        private void NodeCheckBox_Click(object sender, EventArgs e)
        {
            List<ShopsTreenode> list;

            //check all children & enable buttons
            if (this.NodeCheckBox.Checked)
            {
                list = FindChildren();

            }
            else
            {
                list = LoanInquiryShops.LocationsTreeview.FindAll(delegate(ShopsTreenode parent)
                { return parent.NodeID.Equals(0) || parent.NodeID.Equals(this.NodeRegion) || parent.NodeID.Equals(this.NodeMarket); });
            }

            foreach (ShopsTreenode node in list)
            {
                node.NodeCheckBox.Checked = this.NodeCheckBox.Checked;
            }

            //put back in main treeview
            CopyToTreeview(list);

            //check to see if buttons need to be disabled/enabled
            int i = 0;
            for (; i < LoanInquiryShops.LocationsTreeview.Count; i++)
            {
                if (LoanInquiryShops.LocationsTreeview[i].NodeCheckBox.Checked)
                {
                    break;
                }
            }

            Panel loanInquiry = (Panel)this.Parent.Parent;
            Control[] cntrls = loanInquiry.Controls.Find("customButtonClear", false);

            if (i.Equals(LoanInquiryShops.LocationsTreeview.Count))
            {
                loanInquiry.Controls.Find("customButtonClear", false)[0].Enabled = false;
                loanInquiry.Controls.Find("customButtonContinue", false)[0].Enabled = false;
            }
            else
            {
                loanInquiry.Controls.Find("customButtonClear", false)[0].Enabled = true;
                loanInquiry.Controls.Find("customButtonContinue", false)[0].Enabled = true;
            }
        }
        //called internally
        private void ShowVisible()
        {
            ShowVisibleCommon((Panel)this.Parent);                    
        }
        //called externally
        public void ShowVisible(Panel parentPanel)
        {
            ShowVisibleCommon(parentPanel);
        }
        //common ShowVisible code
        public void ShowVisibleCommon(Panel parentPanel)
        {
            int nodeCount = 0;
            parentPanel.Controls.Clear();

            foreach(Control cntrl in parentPanel.Controls)
            {
                cntrl.Dispose();
            }

            foreach (ShopsTreenode locNode in LoanInquiryShops.LocationsTreeview)
            {
                if (locNode.Visible)
                {
                    locNode.Location = new System.Drawing.Point(locNode.Inset, 20 * nodeCount++);
                    parentPanel.Controls.Add(locNode);
                }
            }

            PerformLayout();
        }        
        //treeview methods
        public void ClearSelection()
        {
            foreach (ShopsTreenode node in LoanInquiryShops.LocationsTreeview)
            {
                node.NodeCheckBox.Checked = false;
            }

            try
            {
                Panel loanInquiry = (Panel)this.Parent.Parent;
                Control[] cntrls = loanInquiry.Controls.Find("customButtonClear", false);
                cntrls[0].Enabled = false;

                cntrls = loanInquiry.Controls.Find("customButtonContinue", false);
                cntrls[0].Enabled = false;
            }
            catch
            {
                return;
            }
        }
        private void Collapse()
        {
            //get list of nodes to collapse
            List<ShopsTreenode> collapseList = FindChildren();

            //collapse nodes
            foreach (ShopsTreenode node in collapseList)
            {
                if (node.NodeID.Equals(this.NodeID))
                {
                    node.Visible = true;
                    node.NodePictureBox.Image = global::Common.Properties.Resources.Collapsed;
                    node.State = ShopsTreenode.NodeState.Collapsed;
                }
                else
                {
                    node.Visible = false;

                    if (node.Level.Equals(NodeLevel.Shop))
                    {
                        node.NodePictureBox.Image = null;
                        node.State = ShopsTreenode.NodeState.None;
                    }
                    else
                    {
                        node.NodePictureBox.Image = global::Common.Properties.Resources.Collapsed;
                        node.State = ShopsTreenode.NodeState.Collapsed;
                    }
                }
            }

            //copy collapsed back to main treeview list
            CopyToTreeview(collapseList);

            ShowVisible();
        }
        private void Expand()
        {

            foreach (ShopsTreenode node in LoanInquiryShops.LocationsTreeview)
            {
                if (node.NodeParent.Equals(this.NodeID) || node.NodeID.Equals(this.NodeID))
                {
                    if (node.NodeID.Equals(this.NodeID))
                    {
                        node.NodePictureBox.Image = global::Common.Properties.Resources.Expanded;
                        node.State = ShopsTreenode.NodeState.Expanded;
                    }
                    else if (!node.Level.Equals(ShopsTreenode.NodeLevel.Shop))
                    {
                        node.NodePictureBox.Image = global::Common.Properties.Resources.Collapsed;
                        node.State = ShopsTreenode.NodeState.Collapsed;
                    }

                    if (node.Level.Equals(ShopsTreenode.NodeLevel.Shop))
                    {
                        node.NodePictureBox.Image = null;
                        node.State = ShopsTreenode.NodeState.None;
                    }

                    node.Visible = true;
                }
            }

            ShowVisible();
        }
        private List<ShopsTreenode> FindChildren()
        {
            List<ShopsTreenode> list = new List<ShopsTreenode>();
            //get list of nodes to collapse
            switch (this.Level)
            {
                case NodeLevel.Company:
                    list = LoanInquiryShops.LocationsTreeview;
                    break;

                case NodeLevel.Region:
                    list = LoanInquiryShops.LocationsTreeview.FindAll(delegate(ShopsTreenode child) { return child.NodeRegion.Equals(this.NodeRegion); });
                    break;

                case NodeLevel.Market:
                    list = LoanInquiryShops.LocationsTreeview.FindAll(delegate(ShopsTreenode child) { return child.NodeMarket.Equals(this.NodeMarket); });
                    break;
            }

            return list;
        }
        private void CopyToTreeview(List<ShopsTreenode> list)
        {
            foreach (ShopsTreenode node in list)
            {
                foreach (ShopsTreenode node2 in LoanInquiryShops.LocationsTreeview)
                {
                    if (node.NodeID.Equals(node2.NodeID))
                    {
                        node2.Visible = node.Visible;
                        node2.NodePictureBox = node.NodePictureBox;
                        node2.State = node.State;
                        break;
                    }
                }
            }
        }
        //data methods
        public void LoadTreeview()
        {
            //sort by region
            LoanInquiryShops.LocationsTreeview.Clear();

            List<Shops> regions = new List<Shops>();
            List<Shops> regionsMarkets = new List<Shops>();
            List<Shops> regionsMarketsStores = new List<Shops>();

            int regParent = 0;
            int mktParent = 0;

            _count = 0;

            AddNode(NodeState.Expanded, NodeLevel.Company, "Cash America", new System.Drawing.Font("Arial", 9, System.Drawing.FontStyle.Bold), 
                                0, -1, true, -1, -1);

            LoanInquiryShops.Shops.Sort(delegate(Shops region1, Shops region2) { return region1.Region.CompareTo(region2.Region); });

            foreach (Shops reg in LoanInquiryShops.Shops)
            {
                regParent = _count;
                //find regions
                regions = LoanInquiryShops.Shops.FindAll(delegate(Shops region) { return region.Region.Equals(reg.Region); });

                AddNode(NodeState.Collapsed, NodeLevel.Region, reg.Region, new System.Drawing.Font("Arial", 9), 20, 0, true, regParent, -1);

                foreach (Shops mkt in regions)
                {
                    mktParent = _count;
                    //find markets & sort
                    regionsMarkets = regions.FindAll(delegate(Shops market) { return market.Market.Equals(mkt.Market); });
                    regionsMarkets.Sort(delegate(Shops market1, Shops market2) { return System.String.CompareOrdinal(market1.Region, market2.Region); });

                    AddNode(NodeState.Collapsed, NodeLevel.Market, reg.Market, new System.Drawing.Font("Arial", 9), 40, regParent, false, regParent, mktParent);

                    //find stores & sort
                    regionsMarketsStores = regionsMarkets.FindAll(delegate(Shops store) { return store.Market.Equals(mkt.Market); });
                    regionsMarketsStores.Sort(delegate(Shops store1, Shops store2) { return System.String.CompareOrdinal(store1.Region, store2.Region); });

                    foreach (Shops str in regionsMarketsStores)
                    {
                        AddNode(NodeState.None, NodeLevel.Shop, reg.Shop, new System.Drawing.Font("Arial", 9), 60, mktParent, false, regParent, mktParent);
                    }
                }
            }
        }
        private void AddNode(NodeState state, NodeLevel level, string text, Font font, int inset, int parent, bool visible, int region, int market)
        {
            ShopsTreenode locNode = new ShopsTreenode(state);
            locNode.NodeID = _count++;
            if (level.Equals(NodeLevel.Company))
            {
                locNode.Label.Text = text;
            }
            else
            {
                locNode.Label.Text = string.Format("{0} {1}", level, text);
            }
            locNode.NodeValue = text;
            locNode.Label.Font = font;
            locNode.Inset = inset;
            locNode.Level = level;
            locNode.State = state;
            locNode.NodeParent = parent;
            locNode.NodeRegion = region;
            locNode.NodeMarket = market;
            locNode.Visible = visible;
            LoanInquiryShops.LocationsTreeview.Add(locNode);
        }
    }
}

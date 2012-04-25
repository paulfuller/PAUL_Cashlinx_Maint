using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database;
using Common.Libraries.Forms.Components.Behaviors;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Retail;

namespace Pawn.Forms.UserControls
{
    public partial class UserItemCommentsControl : UserControl
    {
        #region public properties
        public RetailItem Item { get; set; }
        #endregion

        #region Public Events
        //public event EventHandler<CommentsChangedEventArgs> EventCommentsChanged;
        #endregion

        #region Constructors
        public UserItemCommentsControl(RetailItem item)
        {
            Item = item;
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        private void ParseItems()
        {
            if(Item != null)
            {
                lblDescription.Text = Item.TicketDescription;
                txtComments.Text = Item.UserItemComments;
                lblICN.Text = Item.Icn;
            }
        }
        #endregion

        #region Private Events Methods
        /*private void RaiseEventCommentsChanged(RetailItem item)
        {
            if (EventCommentsChanged == null)
            {
                return;
            }
            EventCommentsChanged(this, new CommentsChangedEventArgs(item));
        }*/

        private void UserItemCommentsControl_Load(object sender, EventArgs e)
        {
            ParseItems();
        }

        private void txtComments_Leave(object sender, EventArgs e)
        {
            //also check for bad language here
            string errorText = null;
            string errorCode = null;
            
            if (RetailProcedures.FilterBadLanguage(txtComments.Text, out errorCode, out errorText))
            {
                MessageBox.Show("No bad language allowed.");
                //txtComments.Focus();
                return;
            }
            if (txtComments.Text != Item.UserItemComments)
            {
                Item.TempUserItemComments = txtComments.Text.Replace("\r\n", " ");
            }
        }
        #endregion

        #region Helper Classes
        /*public class CommentsChangedEventArgs : EventArgs
        {
            public CommentsChangedEventArgs(RetailItem item)
            {
                Item = item;
            }
            public RetailItem Item { get; private set; }
        }*/
        #endregion

    }
}

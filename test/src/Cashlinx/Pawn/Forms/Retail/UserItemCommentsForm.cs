using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;
using Common.Libraries.Forms.Retail;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Layaway;
using Common.Libraries.Objects.Retail;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Shared;
using Pawn.Forms.Pawn.Loan;
using Pawn.Forms.Pawn.Services.Pickup;
using Pawn.Forms.UserControls;
using Pawn.Logic;

namespace Pawn.Forms.Retail
{
    public partial class UserItemCommentsForm : Form
    {
        #region Public Fields
        public List<RetailItem> listRetailItem;
        #endregion

        #region Public Events
        public event EventHandler OnCommentsChanged;
        #endregion

        #region Constructors
        public UserItemCommentsForm(List<RetailItem> retailItemList)
        {
            listRetailItem = new List<RetailItem>();
            InitializeComponent();
            listRetailItem = retailItemList;
        }
        #endregion

        #region Events Setup
        private void RaiseOnCommentsChanged(object sender, EventArgs e)
        {
            if (OnCommentsChanged == null)
            {
                return;
            }
            OnCommentsChanged(sender, e);
        }
        #endregion

        #region Private Events Methods
     
        private void UserItemCommentsForm_Load(object sender, EventArgs e)
        {
            LoadCommentsUserControls();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
           bool updated = false;
           foreach (RetailItem item in listRetailItem)
           {
               //item.UserItemComments = item.TempUserItemComments;
               //find item in GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems where they are same
               int index = GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems.FindIndex(p => p.Icn == item.Icn);
               if (index >= 0)
               {
                   GlobalDataAccessor.Instance.DesktopSession.ActiveRetail.RetailItems[index].UserItemComments =
                       item.TempUserItemComments;
                   updated = true;
               }
           }
           if(updated)
           {
               //fire off event to publishusercontrols
               RaiseOnCommentsChanged(null, null);
           }
           this.Close();
        }


        /*
        private void control_EventCommentsChanged(object sender, UserItemCommentsControl.CommentsChangedEventArgs e)
        {
            foreach (RetailItem item in listRetailItem)
            {
                if (e.Item == item)
                    e.Item.TempUserItemComments = item.TempUserItemComments;
            }
        }*/
        #endregion

        #region Private Methods
        private void LoadCommentsUserControls()
        {
            foreach (RetailItem item in listRetailItem)
            {
                //for (int i = 0; i <= 7; i++)
                //{
                    UserItemCommentsControl control = new UserItemCommentsControl(item);
                    //control.EventCommentsChanged += new EventHandler<UserItemCommentsControl.CommentsChangedEventArgs>(control_EventCommentsChanged);
                    tableLayoutPanel1.Height = tableLayoutPanel1.Height + control.Height;
                    tableLayoutPanel1.Controls.Add(control);
               // }
            }
        }
        #endregion

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            //listRetailItem.Clear();
        }

      
    }
}

/**************************************************************************
 * MerchandiseComments.cs 
 * 
 * History:
 *  ?? ??/??/???? Initial implementation
 *  DAndrews 1/27/2011 Corrected comments where not changed when initially created from 
 *      copy of PFI_Verify.cs. Code changed to work with IItem generally, ScrapItem when 
 *      that type of item is passed into the constructor.
 **************************************************************************/

using System;
using System.Windows.Forms;
using Common.Libraries.Objects.Business;

namespace Pawn.Forms.Pawn.Services.MerchandiseTransfer
{
    public partial class MerchandiseComment : Form
    {
        #region Private Members

        private IItem _item;
        private string _comment;

        #endregion

        #region Public Members

        /// <summary>
        /// The Comment to be applied to the current Item.
        /// </summary>
        public string Comment
        {
            get { return _comment; }
        }

        /// <summary>
        /// The item whose Comment is being provided by this dialog.
        /// </summary>
        public IItem Item
        {
            get { return _item; }
        }

        #endregion

        #region Constructor

        public MerchandiseComment(IItem item)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
            _item = item;
            Setup();
        }

        #endregion

        #region Private Methods

        private void Setup()
        {
            this.txtComment.Text = _item.Comment;
            this.txtDesc.Text = _item.TicketDescription;
            this.txtICN.Text = _item.Icn;
            if (_item is ScrapItem)
            {
                this.txtJewelryCase.Visible = true;
                this.txtJewelryCase.Text = ((ScrapItem)_item).JewelryCaseNumber;
            }
            else
            {
                this.txtJewelryCase.Visible = false;
                this.txtJewelryCase.Text = string.Empty;
            }
        }

        #endregion

        #region Events

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();           
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            this._comment = this.txtComment.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

    }
}

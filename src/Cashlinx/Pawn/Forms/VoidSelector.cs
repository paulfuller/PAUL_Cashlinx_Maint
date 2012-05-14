using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;

namespace Pawn.Forms
{
    public partial class VoidSelector : Form
    {
        /*
         *
        Void Buy
        Void Pawn Loan
        Void Retail Sale
        Void Item Cost Revision
        Void Merchandise Transfer Out
        Void Cash Transfer Shop To Bank
        Void Cash Transfer Bank to Shop
        Void Cash Transfer Shop to Shop
        Void Police Seize (Loan, Buys, Inventory)
        Void Restitution Payment
        Void Release to Claimant
        Void Layaway Activity
        Void PFI
         */
        public static string[] VOIDTRANSACTIONTYPES =
        {
            "Void Buy",
            "Void Pawn Loan",
            "Void Retail Sale",
            "Void Item Cost Revision",
            "Void Merchandise Transfer Out",
            "Void Merchandise Transfer In",
            "Void Cash Transfer Shop to Bank",
            "Void Cash Transfer Bank to Shop",
            "Void Cash Transfer Shop to Shop",
            "Void Police Seize (Loan, Buys, Inventory)",
            "Void Restitution Payment",
            "Void Release to Claimant",
            "Void Layaway Activity",
            "Void Release Fingerprints",
            "Void PFI"

        };

        public enum VoidTransactionType : uint
        {
            VOIDBUY = 0,
            VOIDPAWNLOAN = 1,
            VOIDRETAILSALE = 2,
            VOIDITEMCOSTREV = 3,
            VOIDMDSETRANSOUT = 4,
            VOIDMDSETRANSIN = 5,
            VOIDCASHTRANSSHOPTOBANK = 6,
            VOIDCASHTRANSBANKTOSHOP = 7,
            VOIDCASHTRANSSHOPTOSHOP = 8,
            VOIDPOLICESEIZE = 9,
            VOIDRESTITUTION = 10,
            VOIDRTC = 11,
            VOIDLAYAWAY = 12,
            VOIDRELEASEFINGERPRINTS = 13,
            VOIDPFI = 14

        }

        public VoidTransactionType SelectedVoidTransactionType { private set; get; }
        private bool selectedValue;

        /// <summary>
        /// Constructor
        /// </summary>
        public VoidSelector()
        {
            InitializeComponent();
            SelectedVoidTransactionType = VoidTransactionType.VOIDBUY;
            AddReleaseFingerprintsIfNeeded();
            selectedValue = false;
        }

        /// <summary>
        /// Process the continue button click event
        /// Determine if the user has selected a value or not before
        /// allowing them to proceed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continueButton_Click(object sender, EventArgs e)
        {
            if (!selectedValue)
            {
                MessageBox.Show("Please select a void transaction type.",
                                "Cashlinx Pawn Warning Message",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Process the cancel button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            var res = MessageBox.Show("Are you sure you want to cancel the void operation?",
                            "Cashlinx Pawn Warning Message",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);
            if (res == DialogResult.Yes)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        /// <summary>
        /// Process the index selection for the void transaction type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void voidTransactionTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedValue = false;
            //Determine which type was selected and match it to the enumeration
            if (this.voidTransactionTypeComboBox.SelectedIndex >= 0)
            {
                var sIdx = this.voidTransactionTypeComboBox.SelectedIndex;
                if (sIdx < this.voidTransactionTypeComboBox.Items.Count)
                {
                    var strObj = this.voidTransactionTypeComboBox.Items[sIdx];
                    //Once we have determined we are within range, ensure string integrity
                    if (strObj != null)
                    {
                        var strVal = strObj.ToString();
                        if (strVal.Equals(VOIDTRANSACTIONTYPES[sIdx],
                                          StringComparison.OrdinalIgnoreCase))
                        {
                            //If the string selected matches the index back into the originating array, parse the
                            //string index value into the proper enumerated type
                            SelectedVoidTransactionType =
                                    (VoidTransactionType)
                                    Enum.Parse(typeof(VoidTransactionType), sIdx.ToString());
                            selectedValue = true;
                        }
                    }
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.ActiveControl.Equals(cancelButton) && cancelButton.Enabled && keyData == Keys.Enter)
            {
                cancelButton.PerformClick();
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                continueButton.PerformClick();
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                cancelButton.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void AddReleaseFingerprintsIfNeeded()
        {
            if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsSubpoenaRequiredForReleaseFingerprints(GlobalDataAccessor.Instance.CurrentSiteId))
                voidTransactionTypeComboBox.Items.Add("Void Release Fingerprints");

        }
    }
}

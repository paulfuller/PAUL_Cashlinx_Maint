using System;
using System.Collections.Generic;
using System.Drawing;
using Common.Controllers.Application;
using Common.Libraries.Forms.Components;
using System.Windows.Forms;
using System.Text;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Shared;

namespace Common.Libraries.Forms
{
    public partial class ChargeOff : CustomBaseForm
    {
        public ChargeOff(ChargeOffContext chargeOffContext)
        {
            ChargeOffContext = chargeOffContext;
            InitializeComponent();
        }

        public ChargeOffContext ChargeOffContext { get; private set; }
        public DesktopSession DesktopSession 
        { 
            get { return ChargeOffContext.DesktopSession; } 
        }
        public event EventHandler PerformChargeOff;

        private void ChargeOff_Load(object sender, EventArgs e)
        {
            if (ChargeOffContext.MultipleItems)
            {
                lblICN.Text = lblChargeOffAmount.Text = txtDescription.Text = "Multiple Items";
                lblICN.Font = lblChargeOffAmount.Font = txtDescription.Font = new Font(lblICN.Font, FontStyle.Italic | FontStyle.Bold);
            }
            else
            {
                lblICN.Text = ChargeOffContext.Icn.GetShortCode();
                lblChargeOffAmount.Text = ChargeOffContext.ChargeOffAmount.ToString("c");
                txtDescription.Text = ChargeOffContext.Description;
            }
            Setup();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var reasonCode = ddlReason.SelectedItem as ItemReasonCode;

            if (reasonCode == null)
            {
                return;
            }

            if (!RequiredFieldsAreValid())
            {
                return;
            }

            ChargeOffContext.ItemReason = reasonCode.Reason;

            ChargeOffContext.ChargeOffDatabaseContext = new ChargeOffDatabaseContext();
            ChargeOffContext.ChargeOffDatabaseContext.AtfIncidentId = txtATFIncidentNumber.Text.Trim();
            ChargeOffContext.ChargeOffDatabaseContext.AuthorizedBy = txtAuthorizedBy.Text.Trim();
            ChargeOffContext.ChargeOffDatabaseContext.CharitableAddress = txtCharitableOrganization.Text.Trim();
            ChargeOffContext.ChargeOffDatabaseContext.CharitableCity = txtCharitableOrganizationCity.Text.Trim();
            ChargeOffContext.ChargeOffDatabaseContext.CharitableOrganization = txtCharitableOrganization.Text.Trim();
            ChargeOffContext.ChargeOffDatabaseContext.CharitablePostalCode = txtCharitableOrganizationPostalCode.Text.Trim();
            ChargeOffContext.ChargeOffDatabaseContext.CharitableState = txtCharitableOrganizationState.Text.Trim();
            ChargeOffContext.ChargeOffDatabaseContext.Comments = txtComment.Text.Trim();
            ChargeOffContext.ChargeOffDatabaseContext.Destroyed = chkMerchandiseDestoyed.Checked;
            ChargeOffContext.ChargeOffDatabaseContext.ItemReason = reasonCode.Reason;
            ChargeOffContext.ChargeOffDatabaseContext.PoliceCaseNumber = txtPoliceCaseNumber.Text.Trim();
            ChargeOffContext.ChargeOffDatabaseContext.ReplacedIcn = txtReplacedICN.Text.Trim();

            if (PerformChargeOff != null)
            {
                PerformChargeOff(this, EventArgs.Empty);
            }

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool RequiredFieldsAreValid()
        {
            // Remove the following when supporting the additional charge off fields.
            return true;

// ReSharper disable HeuristicUnreachableCode
            /*var asteriskLabels = new List<Label>();
            asteriskLabels.Add(lblAuthorizedByAsterisk);
            asteriskLabels.Add(lblCommentAsterisk);
            asteriskLabels.Add(lblPoliceCaseNumberAsterisk);
            asteriskLabels.Add(lblATFIncidentNumberAsterisk);
            asteriskLabels.Add(lblCharitableOrganizationAsterisk);
            asteriskLabels.Add(lblCharitableOrganizationAddressAsterisk);
            asteriskLabels.Add(lblCharitableOrganizationCityAsterisk);
            asteriskLabels.Add(lblCharitableOrganizationStateAsterisk);
            asteriskLabels.Add(lblCharitableOrganizationPostalCodeAsterisk);
            asteriskLabels.Add(lblPawnTicketNumberAsterisk);
            asteriskLabels.Add(lblReplacedICNAsterisk);
            asteriskLabels.Add(lblMerchandiseDestroyedAsterisk);

            var errorFields = new StringBuilder();
            int errors = 0;

            foreach (Label asteriskLabel in asteriskLabels)
            {
                if (!asteriskLabel.Visible || !asteriskLabel.Text.Equals("*"))
                {
                    continue;
                }

                var lbl = tableLayoutPanel1.Controls.Find(asteriskLabel.Name.Replace("Asterisk", ""), true)[0] as Label;
                var txt = tableLayoutPanel1.Controls.Find(lbl.Name.Replace("lbl", "txt"), true)[0] as TextBox;

                if (string.IsNullOrWhiteSpace(txt.Text))
                {
                    errors++;
                    errorFields.AppendLine(lbl.Text.Substring(0, lbl.Text.Length - 1));
                }
            }

            if (errors == 0)
            {
                return true;
            }
            else if (errors == 1)
            {
                string message = string.Format("The following is a required field:{0}{0}{1}", Environment.NewLine, errorFields.ToString());
                MessageBox.Show(message);
            }
            else
            {
                string message = string.Format("The following are required fields:{0}{0}{1}", Environment.NewLine, errorFields.ToString());
                MessageBox.Show(message);
            }

            return false;*/
// ReSharper restore HeuristicUnreachableCode
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Abort;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void Setup()
        {
            List<ItemReasonCode> ptChargeCodes = ItemReasonFactory.Instance.GetChargeOffCodes(PawnSecApplication.Audit, ChargeOffContext.IsGun);

            if (ChargeOffContext.MultipleItems)
            {
                ptChargeCodes.Remove(ptChargeCodes.Find(rc => rc.Reason == ItemReason.COFFREPLPROP));
            }

            // Populate Combo Box Charge Off Codes from Enum
            ddlReason.DataSource = ptChargeCodes;
            ddlReason.DisplayMember = "Description";

            int defaultIndex = ptChargeCodes.FindIndex(rc => rc.Reason == ItemReason.COFFMISSING);
            ddlReason.SelectedIndex = defaultIndex;

            // Remove the following when supporting the additional charge off fields.
            flowLayoutPanelLabels.Visible = false;
            flowLayoutPanelAsterisks.Visible = false;
            flowLayoutPanelFields.Visible = false;
        }

        private void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            var rc = ddlReason.SelectedItem as ItemReasonCode;

            if (rc == null)
            {
                return;
            }

            if (ChargeOffContext.IsGun)
            {
                lblComment.Visible = lblCommentAsterisk.Visible = txtComment.Visible = true;
                lblAuthorizedBy.Visible = lblAuthorizedByAsterisk.Visible = txtAuthorizedBy.Visible = true;
                lblPoliceCaseNumber.Visible = lblPoliceCaseNumberAsterisk.Visible = txtPoliceCaseNumber.Visible = rc.Reason == ItemReason.COFFLOSTTRNS
                                                    || rc.Reason == ItemReason.COFFBURGROBB
                                                    || rc.Reason == ItemReason.COFFMISSING
                                                    || rc.Reason == ItemReason.COFFCASUALTY
                                                    || rc.Reason == ItemReason.COFFDESTROY
                                                    || rc.Reason == ItemReason.COFFINTTHEFT;
                lblATFIncidentNumber.Visible = lblATFIncidentNumberAsterisk.Visible = txtATFIncidentNumber.Visible = rc.Reason == ItemReason.COFFLOSTTRNS
                                                    || rc.Reason == ItemReason.COFFBURGROBB
                                                    || rc.Reason == ItemReason.COFFMISSING
                                                    || rc.Reason == ItemReason.COFFCASUALTY
                                                    || rc.Reason == ItemReason.COFFDESTROY
                                                    || rc.Reason == ItemReason.COFFINTTHEFT;
                lblCharitableOrganization.Visible = lblCharitableOrganizationAsterisk.Visible = txtCharitableOrganization.Visible =
                    lblCharitableOrganizationAddress.Visible = lblCharitableOrganizationAddressAsterisk.Visible = txtCharitableOrganizationAddress.Visible =
                    lblCharitableOrganizationCity.Visible = lblCharitableOrganizationCityAsterisk.Visible = txtCharitableOrganizationCity.Visible =
                    lblCharitableOrganizationState.Visible = lblCharitableOrganizationStateAsterisk.Visible = txtCharitableOrganizationState.Visible =
                    lblCharitableOrganizationPostalCode.Visible = lblCharitableOrganizationPostalCodeAsterisk.Visible = txtCharitableOrganizationPostalCode.Visible = rc.Reason == ItemReason.COFFDONATION;
                lblPawnTicketNumber.Visible = lblPawnTicketNumberAsterisk.Visible = txtPawnTicketNumber.Visible = rc.Reason == ItemReason.COFFREPLPROP;
                lblReplacedICN.Visible = lblReplacedICNAsterisk.Visible = txtReplacedICN.Visible = rc.Reason == ItemReason.COFFREPLPROP;
                lblMerchandiseDestroyed.Visible = lblMerchandiseDestroyedAsterisk.Visible = chkMerchandiseDestoyed.Visible = false;

                lblCommentAsterisk.Text = "*";
                lblAuthorizedByAsterisk.Text = "*";
                lblPoliceCaseNumberAsterisk.Text = rc.Reason == ItemReason.COFFLOSTTRNS 
                                                    || rc.Reason == ItemReason.COFFBURGROBB 
                                                    || rc.Reason == ItemReason.COFFMISSING
                                                    || rc.Reason == ItemReason.COFFINTTHEFT ? "*" : "";
                lblATFIncidentNumberAsterisk.Text = rc.Reason == ItemReason.COFFLOSTTRNS
                                                    || rc.Reason == ItemReason.COFFBURGROBB
                                                    || rc.Reason == ItemReason.COFFMISSING
                                                    || rc.Reason == ItemReason.COFFINTTHEFT ? "*" : "";
                lblCharitableOrganizationAsterisk.Text = rc.Reason == ItemReason.COFFDONATION ? "*" : "";
                lblCharitableOrganizationAddressAsterisk.Text = "";
                lblCharitableOrganizationCityAsterisk.Text = "";
                lblCharitableOrganizationStateAsterisk.Text = "";
                lblCharitableOrganizationPostalCodeAsterisk.Text = "";
                lblPawnTicketNumberAsterisk.Text = rc.Reason == ItemReason.COFFREPLPROP ? "*" : "";
                lblReplacedICNAsterisk.Text = rc.Reason == ItemReason.COFFREPLPROP ? "*" : "";
                lblMerchandiseDestroyedAsterisk.Text = "";
            }
            else
            {
                lblComment.Visible = lblCommentAsterisk.Visible = txtComment.Visible = true;
                lblAuthorizedBy.Visible = lblAuthorizedByAsterisk.Visible = txtAuthorizedBy.Visible = true;
                lblPoliceCaseNumber.Visible = lblPoliceCaseNumberAsterisk.Visible = txtPoliceCaseNumber.Visible = rc.Reason == ItemReason.COFFBURGROBB || rc.Reason == ItemReason.COFFMISSING
                    || rc.Reason == ItemReason.COFFDESTROY || rc.Reason == ItemReason.COFFINTTHEFT;
                lblATFIncidentNumber.Visible = lblATFIncidentNumberAsterisk.Visible = txtATFIncidentNumber.Visible = false;
                lblCharitableOrganization.Visible = lblCharitableOrganizationAsterisk.Visible = txtCharitableOrganization.Visible =
                    lblCharitableOrganizationAddress.Visible = lblCharitableOrganizationAddressAsterisk.Visible = txtCharitableOrganizationAddress.Visible =
                    lblCharitableOrganizationCity.Visible = lblCharitableOrganizationCityAsterisk.Visible = txtCharitableOrganizationCity.Visible =
                    lblCharitableOrganizationState.Visible = lblCharitableOrganizationStateAsterisk.Visible = txtCharitableOrganizationState.Visible =
                    lblCharitableOrganizationPostalCode.Visible = lblCharitableOrganizationPostalCodeAsterisk.Visible = txtCharitableOrganizationPostalCode.Visible = rc.Reason == ItemReason.COFFDONATION;
                lblPawnTicketNumber.Visible = lblPawnTicketNumberAsterisk.Visible = txtPawnTicketNumber.Visible = rc.Reason == ItemReason.COFFREPLPROP;
                lblReplacedICN.Visible = lblReplacedICNAsterisk.Visible = txtReplacedICN.Visible = rc.Reason == ItemReason.COFFREPLPROP;
                lblMerchandiseDestroyed.Visible = lblMerchandiseDestroyedAsterisk.Visible = chkMerchandiseDestoyed.Visible = rc.Reason == ItemReason.COFFBRKN;

                lblCommentAsterisk.Text = "*";
                lblAuthorizedByAsterisk.Text = rc.Reason == ItemReason.COFFMISSING || rc.Reason == ItemReason.COFFDESTROY || rc.Reason == ItemReason.COFFINTTHEFT ? "*" : "";
                lblPoliceCaseNumberAsterisk.Text = rc.Reason == ItemReason.COFFMISSING || rc.Reason == ItemReason.COFFINTTHEFT ? "*" : "";
                lblATFIncidentNumberAsterisk.Text = "";
                lblCharitableOrganizationAsterisk.Text = rc.Reason == ItemReason.COFFDONATION ? "*" : "";
                lblCharitableOrganizationAddressAsterisk.Text = "";
                lblCharitableOrganizationCityAsterisk.Text = "";
                lblCharitableOrganizationStateAsterisk.Text = "";
                lblCharitableOrganizationPostalCodeAsterisk.Text = "";
                lblPawnTicketNumberAsterisk.Text = rc.Reason == ItemReason.COFFREPLPROP ? "*" : "";
                lblReplacedICNAsterisk.Text = rc.Reason == ItemReason.COFFREPLPROP ? "*" : "";
                lblMerchandiseDestroyedAsterisk.Text = "";
            }
        }
    }
}

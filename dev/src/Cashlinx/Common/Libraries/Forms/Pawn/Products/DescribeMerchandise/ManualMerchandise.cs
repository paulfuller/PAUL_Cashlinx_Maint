using System;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    public partial class ManualMerchandise : Form
    {
        private Item pawnItem;
        private static readonly string SPACE = " ";
        private static readonly string EMPTY = "";
        private static readonly string SEMI_COLON = ";";

        public DesktopSession DesktopSession { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="desktopSession"> </param>
        /// <param name="manufacturer"></param>
        /// <param name="model"></param>
        public ManualMerchandise(DesktopSession desktopSession, string manufacturer, string model)
        {
            DesktopSession = desktopSession;
            InitializeComponent();

            this.BackgroundImage = DesktopSession.ResourceProperties.newDialog_400_BlueScale;

            if (manufacturer != null && manufacturer != EMPTY)
            {
                this.manufacturerTextBox.Text = manufacturer;
            }

            if (model != null && model != EMPTY)
            {
                this.modelTextBox.Text = model;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Item PawnItem
        {
            get
            {
                return pawnItem;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManualMerchandise_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void continueButton_Click(object sender, EventArgs e)
        {
            if (this.manufacturerTextBox.Text == "" || this.modelTextBox.Text == "" ||
                this.descriptionTextBox.Text == "" || this.loanAmountTextBox.Text == "")
            {
                this.errorLabel.Visible = true;
            }
            else
            {
                double loanAmount = 0.0d;

                if (double.TryParse(loanAmountTextBox.Text.ToString(), out loanAmount))
                {
                    this.errorLabel.Visible = false;

                    this.pawnItem = new Item();
                    this.pawnItem.CaccLevel = -1;
                    this.pawnItem.ItemAmount = Convert.ToDecimal(loanAmount);

                    StringBuilder sb = new StringBuilder();
                    sb.Append(this.descriptionTextBox.Text + SEMI_COLON);
                    sb.Append(SPACE + this.manufacturerTextBox.Text + SEMI_COLON);
                    sb.Append(SPACE + this.modelTextBox.Text + SEMI_COLON + SPACE);
                                     
                    //this.pawnItem.Description = sb.ToString().ToUpper();
                    //this.pawnItem.LoanRange = "No suggestion";
                    //this.pawnItem.SuggestedRetailAmount = "No suggestion";
                    //this.pawnItem.Manufacturer = this.manufacturerTextBox.Text;
                    //this.pawnItem.Model = this.modelTextBox.Text;

                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    this.errorLabel.Visible = true;
                }
            }           
        }
    }
}

using Common.Libraries.Forms.Components;
using System;
using System.Windows.Forms;
using Common.Libraries.Utility;

namespace Pawn.Forms.Retail
{
    public partial class ApplyCoupon : Form
    {

        public string MdseDescription;
        public CouponData CouponDetails;
        private decimal couponAmt = 0.0m;
        public ApplyCoupon()
        {
            InitializeComponent();
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();           
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            if (customTextBoxCouponCode.isValid)
            {
                if (this.CouponDetails != null)
                {
                    this.CouponDetails.CouponCode = this.customTextBoxCouponCode.Text;
                    this.CouponDetails.CouponPercentage = Utilities.GetDecimalValue(this.customTextBoxCouponPercentage.Text, 0);
                    this.CouponDetails.CouponAmount = Utilities.GetDecimalValue(couponAmt, 0);
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Please enter coupon code");
                return;
            }
        }

        private void ApplyCoupon_Load(object sender, EventArgs e)
        {
            labelMdseDesc.Text = MdseDescription;
            this.labelMdseDesc.Visible = !string.IsNullOrEmpty(this.MdseDescription);
            labelCouponAmtValue.Text=string.Empty;
            if (CouponDetails.CouponType == CouponData.CouponTypes.TRANSACTION)
            {
                labelItemAmt.Text = "Sale Amount:";
                labelItemAmtValue.Text = CouponDetails.TransactionAmount.ToString("C");
                labelTotalAmount.Text = "New Sale Amount:";
                labelTotalAmtValue.Text = CouponDetails.TransactionAmount.ToString("C");
            }
            else
            {
                labelItemAmtValue.Text = CouponDetails.ItemAmount.ToString("C");
                labelTotalAmtValue.Text = CouponDetails.ItemAmount.ToString("C");
            }
            if (CouponDetails.CouponAmount > 0)
            {
                customTextBoxCouponAmount.Text = CouponDetails.CouponAmount.ToString();
                decimal totalAmt = CalculateCouponAmtFromAmount(CouponDetails.CouponAmount);
                labelTotalAmtValue.Text = totalAmt.ToString("C");
                labelCouponAmtValue.Text = couponAmt.ToString("C");


            }
            if (CouponDetails.CouponPercentage > 0)
            {
                customTextBoxCouponPercentage.Text = CouponDetails.CouponPercentage.ToString();
                decimal totalAmt = CalculateCouponAmtFromPercentage(CouponDetails.CouponPercentage);
                labelTotalAmtValue.Text = totalAmt.ToString("C");
                labelCouponAmtValue.Text = couponAmt.ToString("C");

            }
            customTextBoxCouponCode.Text = CouponDetails.CouponCode;
            
        }

        private void customTextBoxCouponPercentage_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(customTextBoxCouponPercentage.Text))
            {

                customTextBoxCouponAmount.Text = string.Empty;
                
                decimal couponValueEntered = Utilities.GetDecimalValue(customTextBoxCouponPercentage.Text, 0);
                if (couponValueEntered == 0)
                {
                    MessageBox.Show("Invalid coupon amount entered. Please try again");
                    customTextBoxCouponPercentage.Text = string.Empty;
                    return;
                }
                
                decimal totalAmt = CalculateCouponAmtFromPercentage(couponValueEntered);
                labelTotalAmtValue.Text = totalAmt.ToString("C");
                labelCouponAmtValue.Text = couponAmt.ToString("C");
                
            }
        }

        private decimal CalculateCouponAmtFromPercentage(decimal couponValueEntered)
        {
            decimal totalAmt = 0.0m;
            if (CouponDetails.CouponType == CouponData.CouponTypes.ITEM)
            {
                couponAmt = Math.Round(CouponDetails.ItemAmount * (couponValueEntered / 100),2);
                if (couponAmt > CouponDetails.ItemAmount)
                {
                    MessageBox.Show("Coupon amount cannot be more than the item amount");
                    customTextBoxCouponPercentage.Focus();
                }
                totalAmt = Math.Round(CouponDetails.ItemAmount - couponAmt,2);
            }
            else
            {
                couponAmt = Math.Round(CouponDetails.TransactionAmount * (couponValueEntered / 100),2);
                if (couponAmt > CouponDetails.TransactionAmount)
                {
                    MessageBox.Show("Coupon amount cannot be more than the transaction amount");
                    customTextBoxCouponPercentage.Focus();
                }

                totalAmt = Math.Round(CouponDetails.TransactionAmount - couponAmt,2);
            }
            return totalAmt;
        }

        private void customTextBoxCouponAmount_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(customTextBoxCouponAmount.Text))
            {

                customTextBoxCouponPercentage.Text = string.Empty;
                decimal couponValueEntered = Utilities.GetDecimalValue(customTextBoxCouponAmount.Text, 0);
                if (couponValueEntered == 0)
                {
                    MessageBox.Show("Invalid coupon amount entered. Please try again");
                    customTextBoxCouponAmount.Text = string.Empty;
                    return;
                }
                decimal totalAmt = CalculateCouponAmtFromAmount(couponValueEntered);
                labelTotalAmtValue.Text = totalAmt.ToString("C");
                labelCouponAmtValue.Text = couponAmt.ToString("C");

            }            
        }

        private decimal CalculateCouponAmtFromAmount(decimal couponValueEntered)
        {
            decimal totalAmt = 0.0m;
            if (CouponDetails.CouponType == CouponData.CouponTypes.ITEM)
            {
                couponAmt = couponValueEntered;
                if (couponAmt > CouponDetails.ItemAmount)
                {
                    MessageBox.Show("Coupon amount cannot be more than the item amount");
                    customTextBoxCouponAmount.Focus();
                }

                totalAmt = Math.Round(CouponDetails.ItemAmount - couponAmt,2);
            }
            else
            {
                couponAmt = couponValueEntered;
                if (couponAmt > CouponDetails.TransactionAmount)
                {
                    MessageBox.Show("Coupon amount cannot be more than the transaction amount");
                    customTextBoxCouponAmount.Focus();
                }

                totalAmt = Math.Round(CouponDetails.TransactionAmount - couponAmt, 2);
            }
            return totalAmt;

        }

 


        private void customTextBoxCouponAmount_TextChanged(object sender, EventArgs e)
        {
            if (!radioButtonDollar.Checked && !string.IsNullOrEmpty(customTextBoxCouponAmount.Text))
            {
                radioButtonPercentage.Checked = false;
                radioButtonDollar.Checked = true;
            }
        }

        private void customTextBoxCouponPercentage_TextChanged(object sender, EventArgs e)
        {
            if (!radioButtonPercentage.Checked && !string.IsNullOrEmpty(customTextBoxCouponPercentage.Text))
            {
                radioButtonPercentage.Checked = true;
                radioButtonDollar.Checked = false;
            }

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DialogResult dgr = MessageBox.Show("This coupon will be removed. Do you want to continue?", "Confirmation", MessageBoxButtons.YesNo);
            if (dgr == DialogResult.Yes)
            {
                if (this.CouponDetails != null)
                {
                    CouponDetails.CouponAmount = 0;
                    CouponDetails.CouponPercentage = 0;
                    CouponDetails.CouponCode = string.Empty;
                }
                this.Close();
            }
            else
                return;
        }

 
    }
    public class CouponData
    {
        public string CouponCode;
        public decimal CouponPercentage;
        public decimal CouponAmount;
        public CouponTypes CouponType;
        public decimal ItemAmount;
        public decimal TransactionAmount;


        public enum CouponTypes
        {
            ITEM,
            TRANSACTION
        }
    }
}

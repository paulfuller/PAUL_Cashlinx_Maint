/********************************************************************
* CashlinxDesktop.UserControls
* CurrencyEntry
* This user control will allow entry of cash denominations in coins and notes
* Sreelatha Rengarajan 6/23/2010 Initial version
* SR 7/27/2010 Added logic for setting data in the controls based on
 * data passed to it
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CashlinxDesktop.UserControls;
using Common.Libraries.Utility;

namespace Pawn.Forms.UserControls
{
    public partial class CurrencyEntry : UserControl
    {
        private const string DENOMINATIONCURRENCY = "USD";
        private List<decimal> otherTenderData;
        public delegate void AddHandler(decimal currencyTotal);
        public delegate void AddOTClick();
        public event AddOTClick OtherTenderClick;
        public event AddHandler Calculate;
        Panel otpanel = new Panel();
        

        public CurrencyEntry()
        {
            InitializeComponent();
            otherTenderData = new List<decimal>();
            
        }

        private void customTextBoxCent1Cnt_Leave(object sender, EventArgs e)
        {

            double cent1Amt = 0;
            int cent1Cnt = Utilities.GetIntegerValue(customTextBoxCent1Cnt.Text,0);
            if (cent1Cnt > 0)
            {
                cent1Amt = .01 * cent1Cnt;
            }
            customTextBoxCent1Amt.Text = cent1Amt.ToString();
            updateCoinTotal();
        }

        private void customTextBoxCent5Cnt_Leave(object sender, EventArgs e)
        {
            double cent5Amt = 0;
            int cent5Cnt = Utilities.GetIntegerValue(customTextBoxCent5Cnt.Text, 0);
            if (cent5Cnt > 0)
            {
                cent5Amt = .05 * cent5Cnt;
            }
            customTextBoxCent5Amt.Text = cent5Amt.ToString();
            updateCoinTotal();
        }

        private void customTextBoxCent10Cnt_Leave(object sender, EventArgs e)
        {
            double cent10Amt = 0;
            int cent10Cnt = Utilities.GetIntegerValue(customTextBoxCent10Cnt.Text, 0);
            if (cent10Cnt > 0)
            {
                cent10Amt = .10 * cent10Cnt;
            }
            customTextBoxCent10Amt.Text = cent10Amt.ToString();
            updateCoinTotal();
        }

        private void customTextBoxCent25Cnt_Leave(object sender, EventArgs e)
        {
            double cent25Amt = 0;
            int cent25Cnt = Utilities.GetIntegerValue(customTextBoxCent25Cnt.Text, 0);
            if (cent25Cnt > 0)
            {
                cent25Amt = .25 * cent25Cnt;
            }
            customTextBoxCent25Amt.Text = cent25Amt.ToString();
            updateCoinTotal();
        }

        private void customTextBoxCent50Cnt_Leave(object sender, EventArgs e)
        {
            double cent50Amt = 0;
            int cent50Cnt = Utilities.GetIntegerValue(customTextBoxCent50Cnt.Text, 0);
            if (cent50Cnt > 0)
            {
                cent50Amt = .50 * cent50Cnt;
            }
            customTextBoxCent50Amt.Text = cent50Amt.ToString();
            updateCoinTotal();
        }

        private void customTextBoxCent100Cnt_Leave(object sender, EventArgs e)
        {
            double cent100Amt = 0;
            int cent100Cnt = Utilities.GetIntegerValue(customTextBoxCent100Cnt.Text, 0);
            if (cent100Cnt > 0)
            {
                cent100Amt = 1 * cent100Cnt;
            }
            customTextBoxCent100Amt.Text = cent100Amt.ToString();
            updateCoinTotal();
        }

        private void updateCoinTotal()
        {
            double totalCoinAmt = 0.0d;
            totalCoinAmt = Utilities.GetDoubleValue(customTextBoxCent1Amt.Text,0.0d) +
                Utilities.GetDoubleValue(customTextBoxCent5Amt.Text,0.0d) +
                Utilities.GetDoubleValue(customTextBoxCent10Amt.Text,0.0d) +
                Utilities.GetDoubleValue(customTextBoxCent25Amt.Text,0.0d) +
                Utilities.GetDoubleValue(customTextBoxCent50Amt.Text,0.0d) +
                Utilities.GetDoubleValue(customTextBoxCent100Amt.Text,0.0d);
            customTextBoxCoinTotal.Text = string.Format("{0:C}",totalCoinAmt);

        }

        private void customTextBoxCent1Amt_Leave(object sender, EventArgs e)
        {
            int cent1Cnt = 0;
            double cent1Amt = Utilities.GetDoubleValue(customTextBoxCent1Amt.Text, 0.0d);
            if (cent1Amt > 0.0d)
            {
                cent1Cnt = Utilities.GetIntegerValue(cent1Amt / .01,0);
            }
            customTextBoxCent1Cnt.Text = cent1Cnt.ToString();
            updateCoinTotal();

        }

        private void customTextBoxCent5Amt_Leave(object sender, EventArgs e)
        {
            int cent5Cnt = 0;
            double cent5Amt = Utilities.GetDoubleValue(customTextBoxCent5Amt.Text, 0.0d);
            if (cent5Amt > 0.0d)
            {
                
                double quotient =Utilities.GetDoubleValue(cent5Amt/.05,0);
                if (Math.Round(quotient) != quotient)
                {
                    MessageBox.Show("Amount entered " + cent5Amt.ToString() + " is not correct for the denomination");
                    customTextBoxCent5Amt.Text = "0";
                    return;
                }

                cent5Cnt = Utilities.GetIntegerValue(cent5Amt / .05, 0);
            }
            customTextBoxCent5Cnt.Text = cent5Cnt.ToString();
            updateCoinTotal();

        }

        private void customTextBoxCent10Amt_Leave(object sender, EventArgs e)
        {
            int cent10Cnt = 0;
            double cent10Amt = Utilities.GetDoubleValue(customTextBoxCent10Amt.Text, 0.0d);
            if (cent10Amt > 0.0d)
            {
                double quotient = Utilities.GetDoubleValue(cent10Amt / .1, 0);
                if (Math.Round(quotient) != quotient)
                {
                    MessageBox.Show("Amount entered " + cent10Amt.ToString() + " is not correct for the denomination");
                    customTextBoxCent10Amt.Text = "0";
                    return;
                }
                cent10Cnt = Utilities.GetIntegerValue(cent10Amt / .1, 0);
            }
            customTextBoxCent10Cnt.Text = cent10Cnt.ToString();
            updateCoinTotal();

        }

        private void customTextBoxCent25Amt_Leave(object sender, EventArgs e)
        {
            int cent25Cnt = 0;
            double cent25Amt = Utilities.GetDoubleValue(customTextBoxCent25Amt.Text, 0.0d);
            if (cent25Amt > 0.0d)
            {
                double quotient = Utilities.GetDoubleValue(cent25Amt / .25, 0);
                if (Math.Round(quotient) != quotient)
                {
                    MessageBox.Show("Amount entered " + cent25Amt.ToString() + " is not correct for the denomination");
                    customTextBoxCent25Amt.Text = "0";
                    return;
                }
                cent25Cnt = Utilities.GetIntegerValue(cent25Amt / .25, 0);
            }
            customTextBoxCent25Cnt.Text = cent25Cnt.ToString();
            updateCoinTotal();

        }

        private void customTextBoxCent50Amt_Leave(object sender, EventArgs e)
        {
            int cent50Cnt = 0;
            double cent50Amt = Utilities.GetDoubleValue(customTextBoxCent50Amt.Text, 0.0d);
            if (cent50Amt > 0.0d)
            {
                double quotient = Utilities.GetDoubleValue(cent50Amt / .50, 0);
                if (Math.Round(quotient) != quotient)
                {
                    MessageBox.Show("Amount entered " + cent50Amt.ToString() + " is not correct for the denomination");
                    customTextBoxCent50Amt.Text = "0";
                    return;
                }
                cent50Cnt = Utilities.GetIntegerValue(cent50Amt / .50, 0);
            }
            customTextBoxCent50Cnt.Text = cent50Cnt.ToString();
            updateCoinTotal();

        }

        private void customTextBoxCent100Amt_Leave(object sender, EventArgs e)
        {
            int cent100Cnt = 0;
            double cent100Amt = Utilities.GetDoubleValue(customTextBoxCent100Amt.Text, 0.0d);
            if (cent100Amt > 0.0d)
            {
                cent100Cnt = Utilities.GetIntegerValue(cent100Amt / 1, 0);
            }
            customTextBoxCent100Cnt.Text = cent100Cnt.ToString();
            updateCoinTotal();

        }

        private void customTextBoxDollar1Cnt_Leave(object sender, EventArgs e)
        {
            double dollar1Amt = 0;
            int dollar1Cnt = Utilities.GetIntegerValue(customTextBoxDollar1Cnt.Text, 0);
            if (dollar1Cnt > 0)
            {
                dollar1Amt = 1 * dollar1Cnt;
            }
            customTextBoxDollar1Amt.Text = dollar1Amt.ToString();
            updateDollarTotal();

        }

        private void updateDollarTotal()
        {
            double totalDollarAmt = 0.0d;
            totalDollarAmt = Utilities.GetDoubleValue(customTextBoxDollar1Amt.Text, 0.0d) +
                Utilities.GetDoubleValue(customTextBoxDollar2Amt.Text, 0.0d) +
                Utilities.GetDoubleValue(customTextBoxDollar5Amt.Text, 0.0d) +
                Utilities.GetDoubleValue(customTextBoxDollar10Amt.Text, 0.0d) +
                Utilities.GetDoubleValue(customTextBoxDollar20Amt.Text, 0.0d) +
                Utilities.GetDoubleValue(customTextBoxDollar50Amt.Text, 0.0d) +
                Utilities.GetDoubleValue(customTextBoxDollar100Amt.Text, 0.0d);
            customTextBoxCurrencyTotal.Text = string.Format("{0:C}",totalDollarAmt);

        }

        private void customTextBoxDollar2Cnt_Leave(object sender, EventArgs e)
        {
            double dollar2Amt = 0;
            int dollar2Cnt = Utilities.GetIntegerValue(customTextBoxDollar2Cnt.Text, 0);
            if (dollar2Cnt > 0)
            {
                dollar2Amt = 2 * dollar2Cnt;
            }
            customTextBoxDollar2Amt.Text = dollar2Amt.ToString();
            updateDollarTotal();

        }

        private void customTextBoxDollar5Cnt_Leave(object sender, EventArgs e)
        {
            double dollar5Amt = 0;
            int dollar5Cnt = Utilities.GetIntegerValue(customTextBoxDollar5Cnt.Text, 0);
            if (dollar5Cnt > 0)
            {
                dollar5Amt = 5 * dollar5Cnt;
            }
            customTextBoxDollar5Amt.Text = dollar5Amt.ToString();
            updateDollarTotal();

        }

        private void customTextBoxDollar10Cnt_Leave(object sender, EventArgs e)
        {
            double dollar10Amt = 0;
            int dollar10Cnt = Utilities.GetIntegerValue(customTextBoxDollar10Cnt.Text, 0);
            if (dollar10Cnt > 0)
            {
                dollar10Amt = 10 * dollar10Cnt;
            }
            customTextBoxDollar10Amt.Text = dollar10Amt.ToString();
            updateDollarTotal();

        }

        private void customTextBoxDollar20Cnt_Leave(object sender, EventArgs e)
        {
            double dollar20Amt = 0;
            int dollar20Cnt = Utilities.GetIntegerValue(customTextBoxDollar20Cnt.Text, 0);
            if (dollar20Cnt > 0)
            {
                dollar20Amt = 20 * dollar20Cnt;
            }
            customTextBoxDollar20Amt.Text = dollar20Amt.ToString();
            updateDollarTotal();

        }

        private void customTextBoxDollar50Cnt_Leave(object sender, EventArgs e)
        {
            double dollar50Amt = 0;
            int dollar50Cnt = Utilities.GetIntegerValue(customTextBoxDollar50Cnt.Text, 0);
            if (dollar50Cnt > 0)
            {
                dollar50Amt = 50 * dollar50Cnt;
            }
            customTextBoxDollar50Amt.Text = dollar50Amt.ToString();
            updateDollarTotal();

        }

        private void customTextBoxDollar100Cnt_Leave(object sender, EventArgs e)
        {
            double dollar100Amt = 0;
            int dollar100Cnt = Utilities.GetIntegerValue(customTextBoxDollar100Cnt.Text, 0);
            if (dollar100Cnt > 0)
            {
                dollar100Amt = 100 * dollar100Cnt;
            }
            customTextBoxDollar100Amt.Text = dollar100Amt.ToString();
            updateDollarTotal();

        }

        private void customTextBoxDollar1Amt_Leave(object sender, EventArgs e)
        {

            double dollar1Amt = Utilities.GetDoubleValue(customTextBoxDollar1Amt.Text, 0.0d);
            if (dollar1Amt > 0.0d)
            {
                customTextBoxDollar1Cnt.Text = dollar1Amt.ToString();
            }
            updateDollarTotal();
        }

        private void customTextBoxDollar2Amt_Leave(object sender, EventArgs e)
        {
            int dollar2Cnt = 0;
            double dollar2Amt = Utilities.GetDoubleValue(customTextBoxDollar2Amt.Text, 0.0d);
            if (dollar2Amt > 0.0d)
            {
                double quotient = Utilities.GetDoubleValue(dollar2Amt / 2, 0);
                if (Math.Round(quotient) != quotient)
                {
                    MessageBox.Show("Amount entered " + dollar2Amt + " is not correct for the denomination");
                    customTextBoxDollar2Amt.Text = "0";
                    return;
                }

                dollar2Cnt = Utilities.GetIntegerValue(dollar2Amt / 2, 0);
            }
            customTextBoxDollar2Cnt.Text = dollar2Cnt.ToString();
            updateDollarTotal();
        }

        private void customTextBoxDollar5Amt_Leave(object sender, EventArgs e)
        {
            int dollar5Cnt = 0;
            double dollar5Amt = Utilities.GetDoubleValue(customTextBoxDollar5Amt.Text, 0.0d);
            if (dollar5Amt > 0.0d)
            {
                double quotient = Utilities.GetDoubleValue(dollar5Amt / 5, 0);
                if (Math.Round(quotient) != quotient)
                {
                    MessageBox.Show("Amount entered " + dollar5Amt.ToString() + " is not correct for the denomination");
                    customTextBoxDollar5Amt.Text = "0";
                    return;
                }

                dollar5Cnt = Utilities.GetIntegerValue(dollar5Amt / 5, 0);
            }
            customTextBoxDollar5Cnt.Text = dollar5Cnt.ToString();
            updateDollarTotal();

        }

        private void customTextBoxDollar10Amt_Leave(object sender, EventArgs e)
        {
            int dollar10Cnt = 0;
            double dollar10Amt = Utilities.GetDoubleValue(customTextBoxDollar10Amt.Text, 0.0d);
            if (dollar10Amt > 0.0d)
            {
                double quotient = Utilities.GetDoubleValue(dollar10Amt / 10, 0);
                if (Math.Round(quotient) != quotient)
                {
                    MessageBox.Show("Amount entered " + dollar10Amt.ToString() + " is not correct for the denomination");
                    customTextBoxDollar10Amt.Text = "0";
                    return;
                }

                dollar10Cnt = Utilities.GetIntegerValue(dollar10Amt / 10, 0);
            }
            customTextBoxDollar10Cnt.Text = dollar10Cnt.ToString();
            updateDollarTotal();

        }

        private void customTextBoxDollar20Amt_Leave(object sender, EventArgs e)
        {
            int dollar20Cnt = 0;
            double dollar20Amt = Utilities.GetDoubleValue(customTextBoxDollar20Amt.Text, 0.0d);
            if (dollar20Amt > 0.0d)
            {
                double quotient = Utilities.GetDoubleValue(dollar20Amt / 20, 0);
                if (Math.Round(quotient) != quotient)
                {
                    MessageBox.Show("Amount entered " + dollar20Amt.ToString() + " is not correct for the denomination");
                    customTextBoxDollar20Amt.Text = "0";
                    return;
                }

                dollar20Cnt = Utilities.GetIntegerValue(dollar20Amt / 20, 0);
            }
            customTextBoxDollar20Cnt.Text = dollar20Cnt.ToString();
            updateDollarTotal();

        }

        private void customTextBoxDollar50Amt_Leave(object sender, EventArgs e)
        {
            int dollar50Cnt = 0;
            double dollar50Amt = Utilities.GetDoubleValue(customTextBoxDollar50Amt.Text, 0.0d);
            if (dollar50Amt > 0.0d)
            {
                double quotient = Utilities.GetDoubleValue(dollar50Amt / 50, 0);
                if (Math.Round(quotient) != quotient)
                {
                    MessageBox.Show("Amount entered " + dollar50Amt.ToString() + " is not correct for the denomination");
                    customTextBoxDollar50Amt.Text = "0";
                    return;
                }

                dollar50Cnt = Utilities.GetIntegerValue(dollar50Amt / 50, 0);
            }
            customTextBoxDollar50Cnt.Text = dollar50Cnt.ToString();
            updateDollarTotal();

        }

        private void customTextBoxDollar100Amt_Leave(object sender, EventArgs e)
        {
            int dollar100Cnt = 0;
            double dollar100Amt = Utilities.GetDoubleValue(customTextBoxDollar100Amt.Text, 0.0d);
            if (dollar100Amt > 0.0d)
            {
                double quotient = Utilities.GetDoubleValue(dollar100Amt / 100, 0);
                if (Math.Round(quotient) != quotient)
                {
                    MessageBox.Show("Amount entered " + dollar100Amt.ToString() + " is not correct for the denomination");
                    customTextBoxDollar100Amt.Text = "0";
                    return;
                }

                dollar100Cnt = Utilities.GetIntegerValue(dollar100Amt / 100, 0);
            }
            customTextBoxDollar100Cnt.Text = dollar100Cnt.ToString();
            updateDollarTotal();

        }

        public List<string> CurrencyEntryData()
        {
            List<String> currencyData = new List<String>();

            if (!string.IsNullOrEmpty(customTextBoxCent1Cnt.Text) && Utilities.GetDecimalValue(customTextBoxCent1Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 0.01");
                currencyData.Add(customTextBoxCent1Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxCent5Cnt.Text) && Utilities.GetDecimalValue(customTextBoxCent5Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 0.05");
                currencyData.Add(customTextBoxCent5Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxCent10Cnt.Text) && Utilities.GetDecimalValue(customTextBoxCent10Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 0.10");
                currencyData.Add(customTextBoxCent10Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxCent25Cnt.Text) && Utilities.GetDecimalValue(customTextBoxCent25Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 0.25");
                currencyData.Add(customTextBoxCent25Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxCent50Cnt.Text) && Utilities.GetDecimalValue(customTextBoxCent50Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 0.50");
                currencyData.Add(customTextBoxCent50Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxCent100Cnt.Text) && Utilities.GetDecimalValue(customTextBoxCent100Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " COIN 1");
                currencyData.Add(customTextBoxCent100Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxDollar1Cnt.Text) && Utilities.GetDecimalValue(customTextBoxDollar1Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 1");
                currencyData.Add(customTextBoxDollar1Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxDollar2Cnt.Text) && Utilities.GetDecimalValue(customTextBoxDollar2Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 2");
                currencyData.Add(customTextBoxDollar2Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxDollar5Cnt.Text) && Utilities.GetDecimalValue(customTextBoxDollar5Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 5");
                currencyData.Add(customTextBoxDollar5Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxDollar10Cnt.Text) && Utilities.GetDecimalValue(customTextBoxDollar10Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 10");
                currencyData.Add(customTextBoxDollar10Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxDollar20Cnt.Text) && Utilities.GetDecimalValue(customTextBoxDollar20Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 20");
                currencyData.Add(customTextBoxDollar20Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxDollar50Cnt.Text) && Utilities.GetDecimalValue(customTextBoxDollar50Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 50");
                currencyData.Add(customTextBoxDollar50Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxDollar100Cnt.Text) && Utilities.GetDecimalValue(customTextBoxDollar100Cnt.Text, 0) > 0)
            {
                currencyData.Add(DENOMINATIONCURRENCY + " 100");
                currencyData.Add(customTextBoxDollar100Cnt.Text);
            }
            if (!string.IsNullOrEmpty(customTextBoxOTAmount.Text))
            {
                string otAmount = customTextBoxOTAmount.Text;
                if (otAmount.StartsWith("$"))
                    otAmount = otAmount.Substring(1);
                if (Utilities.GetDecimalValue(otAmount, 0) > 0)
                {
                    currencyData.Add("OTHER TENDER");
                    currencyData.Add(otAmount);
                }
            }

            if (currencyData.Count == 0)
            {
                currencyData.Add("");
            }
            return currencyData;
        }


        public bool SetCurrencyData(List<string> currencyEntryData,bool amountPassed,out string errorMesg)
        {
            int reminder = 0;
            int quotient=Math.DivRem(currencyEntryData.Count, 2, out reminder);
            if (reminder != 0)
            {
                errorMesg = "Invalid number of elements passed";
                return false;
            }
            for (int i = 0,j=0; i < quotient; i++,j=j+2)
            {
                int x = j;
                int y = x + 1;
                string denominationData = currencyEntryData[x];
                string denominationAmount = currencyEntryData[y];
                if (denominationData == "0.01" || denominationData == "USD 0.01")
                {
                    if (amountPassed)
                    {
                    customTextBoxCent1Amt.Text = denominationAmount;
                    customTextBoxCent1Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / .01).ToString(); 
                    }
                    else
                    {
                        customTextBoxCent1Amt.Text=(Utilities.GetDecimalValue(.01)*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxCent1Cnt.Text= denominationAmount;
                    }
                }
                if (denominationData == "0.05" || denominationData == "USD 0.05")
                {

                    if (amountPassed)
                    {
                        customTextBoxCent5Amt.Text = denominationAmount;
                        customTextBoxCent5Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / .05).ToString();
                    }
                    else
                    {
                        customTextBoxCent5Amt.Text=(Utilities.GetDecimalValue(.05)*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxCent5Cnt.Text = denominationAmount;
                    }
                }
                if (denominationData == "0.10" || denominationData == "USD 0.10")
                {
                    
                    if (amountPassed)
                    {
                        customTextBoxCent10Amt.Text = denominationAmount;
                        customTextBoxCent10Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / .1).ToString();
                    }
                    else
                    {
                        customTextBoxCent10Amt.Text=(Utilities.GetDecimalValue(.10)*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxCent10Cnt.Text = denominationAmount;
                    }
                }
                if (denominationData == "0.25" || denominationData == "USD 0.25")
                {
                    
                    if (amountPassed)
                    {
                        customTextBoxCent25Amt.Text = denominationAmount;
                        customTextBoxCent25Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / .25).ToString();
                    }
                    else
                    {
                        customTextBoxCent25Amt.Text=(Utilities.GetDecimalValue(.25)*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxCent25Cnt.Text = denominationAmount;
                    }
                }
                if (denominationData == "0.50" || denominationData == "USD 0.50")
                {
                    
                    if (amountPassed)
                    {
                        customTextBoxCent50Amt.Text = denominationAmount;
                        customTextBoxCent50Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / .50).ToString();
                    }
                    else
                    {
                        customTextBoxCent50Amt.Text=(Utilities.GetDecimalValue(.50)*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxCent50Cnt.Text = denominationAmount;
                    }
                }
                if (denominationData == "1.00" || denominationData == "USD COIN 1")
                {
                    customTextBoxCent100Amt.Text = denominationAmount;
                    if (amountPassed)
                    {
                        customTextBoxCent100Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / 1).ToString();
                    }
                    else
                        customTextBoxCent100Cnt.Text = denominationAmount;
                }
                if (denominationData == "1" || denominationData == "USD 1")
                {
                    customTextBoxDollar1Amt.Text = denominationAmount;
                    if (amountPassed)
                    {
                        customTextBoxDollar1Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / 1).ToString();
                    }
                    else
                        customTextBoxDollar1Cnt.Text = denominationAmount;
                }
                if (denominationData == "2" || denominationData == "USD 2")
                {
                    
                    if (amountPassed)
                    {
                        customTextBoxDollar2Amt.Text = denominationAmount;
                        customTextBoxDollar2Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / 2).ToString();
                    }
                    else
                    {
                        customTextBoxDollar2Amt.Text=(2*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxDollar2Cnt.Text = denominationAmount;
                    }
                }
                if (denominationData == "5" || denominationData == "USD 5")
                {
                    
                    if (amountPassed)
                    {
                        customTextBoxDollar5Amt.Text = denominationAmount;
                    customTextBoxDollar5Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / 5).ToString();
                    }
                    else
                    {
                        customTextBoxDollar5Amt.Text=(5*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxDollar5Cnt.Text = denominationAmount;
                    }
                }
                if (denominationData == "10" || denominationData == "USD 10")
                {
                    if (amountPassed)
                    {
                    customTextBoxDollar10Amt.Text = denominationAmount;
                    customTextBoxDollar10Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / 10).ToString();
                    }
                    else
                    {
                        customTextBoxDollar10Amt.Text=(10*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxDollar10Cnt.Text = denominationAmount;

                    }
                }
                if (denominationData == "20" || denominationData == "USD 20")
                {
                    if (amountPassed)
                    {
                    customTextBoxDollar20Amt.Text = denominationAmount;
                    customTextBoxDollar20Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / 20).ToString();
                    }
                    else
                    {
                        customTextBoxDollar20Amt.Text=(20*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxDollar20Cnt.Text = denominationAmount;

                    }
                }
                if (denominationData == "50" || denominationData == "USD 50")
                {
                    if (amountPassed)
                    {
                    customTextBoxDollar50Amt.Text = denominationAmount;
                    customTextBoxDollar50Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / 50).ToString();
                    }
                    else
                    {
                        customTextBoxDollar50Amt.Text=(50*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxDollar50Cnt.Text = denominationAmount;

                    }
                }
                if (denominationData == "100" || denominationData == "USD 100")
                {
                    if (amountPassed)
                    {
                    customTextBoxDollar100Amt.Text = denominationAmount;
                    customTextBoxDollar100Cnt.Text = (Utilities.GetDoubleValue(denominationAmount) / 100).ToString();
                    }
                    else
                    {
                        customTextBoxDollar100Amt.Text=(100*Utilities.GetDecimalValue(denominationAmount,0)).ToString();
                        customTextBoxDollar100Cnt.Text = denominationAmount;

                    }
                }
            }
            updateCoinTotal();
            updateDollarTotal();
            errorMesg = "Success";
            return true;

        }

        private void customButtonCalculate_Click(object sender, EventArgs e)
        {
            if (customButtonCalculate.Enabled)
            {
                //Remove the $ sign from the data in coin total and dollar total and other tender total
                string coinData = customTextBoxCoinTotal.Text;
                if (coinData.StartsWith("$"))
                    coinData = coinData.Substring(1);
                decimal coinTotal = Utilities.GetDecimalValue(coinData, 0);
                string currencyData = customTextBoxCurrencyTotal.Text;
                if (currencyData.StartsWith("$"))
                    currencyData = currencyData.Substring(1);
                decimal dollarTotal = Utilities.GetDecimalValue(currencyData, 0);
                string OtData = customTextBoxOTAmount.Text;
                if (OtData.StartsWith("$"))
                    OtData = OtData.Substring(1);
                decimal otherTenderTotal = Utilities.GetDecimalValue(OtData, 0);
                if (coinTotal >= 0 || dollarTotal >= 0 || otherTenderTotal >= 0)
                {
                    Calculate(coinTotal + dollarTotal + otherTenderTotal);
                }
            }
        }

  

        private void customButtonOtherTender_Click(object sender, EventArgs e)
        {
            if (customButtonOtherTender.Enabled)
            {
                OtherTenderClick();
                OtherTender otControl = new OtherTender();
                otControl.AmtContinue += otControl_AmtContinue;
                if (otherTenderData != null)
                {
                    decimal totAmount = (from amt in otherTenderData select amt).Sum();
                    string amtEntered=customTextBoxOTAmount.Text;
                    if (amtEntered.StartsWith("$"))
                        amtEntered=amtEntered.Substring(1);
                    if (totAmount != Utilities.GetDecimalValue(amtEntered, 0))
                        otherTenderData = new List<decimal>();
                }
                otControl.OtherTenderAmounts = otherTenderData;
                panelMain.Visible = false;
                otpanel.Location = new System.Drawing.Point(3, 3);
                otpanel.Size = new System.Drawing.Size(627, 320);
                otpanel.Controls.Clear();
                otpanel.Controls.Add(otControl);
                otpanel.Visible = true;
                this.Controls.Add(otpanel);
            }


            
        }

        void otControl_AmtContinue(decimal otherTenderTotal,int qnty,List<decimal> otherTenderAmounts)
        {
            customTextBoxOTAmount.Text = string.Format("{0:C}", otherTenderTotal);
            customTextBoxOTCount.Text = qnty.ToString();
            otherTenderData = new List<decimal>();
            otherTenderData = otherTenderAmounts;
            otpanel.Visible = false;
            panelMain.Visible = true;
            OtherTenderClick();
        }

        private void CurrencyEntry_EnabledChanged(object sender, EventArgs e)
        {
            this.customButtonCalculate.Enabled = this.Enabled;
            this.customButtonOtherTender.Enabled = this.Enabled;
        }

    }
}

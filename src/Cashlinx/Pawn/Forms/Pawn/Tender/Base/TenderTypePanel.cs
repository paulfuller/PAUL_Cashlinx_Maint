using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Type;

namespace Pawn.Forms.Pawn.Tender.Base
{
    public partial class TenderTypePanel : UserControl
    {
        private TenderTypePanelType tenderPanelType;
        public TenderTypePanelType TenderPanelType
        {
            get
            {
                return (this.tenderPanelType);
            }
        }
        private List<PairType<TenderTypeButton, bool>> tenderTypeButtonEnableFlags;

        public static string[] CREDITBILLAPLABELS =
        {
            "Credit Card", "Bill To AP", "Credit Card"
        };

        public delegate bool ButtonFxn(Button b, object parm);

        public delegate void ButtonClicked(TenderTypeButton t);

        public enum TenderTypePanelType : uint
        {
            NORMALSET = 0,
            BILLAPSET = 1,
            TENDEROUT = 2
        }

        public enum TenderTypeButton : uint
        {
            CASH           = 0,
            CHECK          = 1,
            CREDITORBILLAP = 2,
            DEBIT          = 3,
            SHOPCREDIT     = 4,
            PAYPAL         = 5,
            TTYPELENGTH    = 6
        }

        private string[] tenderTypeButtonEnumNames;
        private ButtonClicked buttonClickedHandler;
        private int buttonMissingAdjustment;

        private bool setButtonEnable(Button b, object enabled)
        {
            if (b == null || enabled == null) return (false);
            var bVal = (bool)enabled;
            b.Enabled = bVal;
            b.Update();
            return (true);
        }

        private bool setButtonVisible(Button b, object visible)
        {
            if (b == null || visible == null) return (false);
            var bVal = (bool)visible;
            if (!bVal)
            {
                b.Hide();
            }
            else
            {
                b.Show();
            }
            return (true);
        }

        private bool moveButtonVertical(Button b, object amount)
        {
            if (b == null || amount == null)
                return (false);
            var iVal = (int)amount;
            int bX = b.Location.X;
            int bY = b.Location.Y;
            b.Location = new Point(bX, bY + iVal);
            return (true);
        }
        
        private void initPanel(TenderTypePanelType tpType, 
            List<PairType<TenderTypeButton, bool>> enableFlags,
            ButtonClicked buttonClickedFxn)
        {
            this.tenderPanelType = tpType;
            this.tenderTypeButtonEnableFlags = enableFlags;
            this.tenderTypeButtonEnumNames = Enum.GetNames(typeof(TenderTypeButton));
            this.buttonMissingAdjustment = 0;
            this.buttonClickedHandler = buttonClickedFxn;
        }

        public TenderTypePanel()
        {
            InitializeComponent();
            this.initPanel(TenderTypePanelType.NORMALSET, null, null);
        }

        public void SetupPanel(
            TenderTypePanelType tpType,
            List<PairType<TenderTypeButton, bool>> enableFlags,
            ButtonClicked buttonClickedFxn)
        {
            //InitializeComponent();
            this.initPanel(tpType, enableFlags, buttonClickedFxn);
        }

        public void SetEnableButton(TenderTypeButton button, bool enabled)
        {
            ButtonFxn fxn = this.setButtonEnable;
            object parm = enabled;
            this.setButtonFxn(button, fxn, parm);
        }

        public void SetVisibleButton(TenderTypeButton button, bool visible)
        {
            ButtonFxn fxn = this.setButtonVisible;
            object parm = visible;
            this.setButtonFxn(button, fxn, parm);
        }

        public void MoveButtonVertical(TenderTypeButton button, int amt)
        {
            ButtonFxn fxn = this.moveButtonVertical;
            object parm = amt;
            this.setButtonFxn(button, fxn, parm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        /// <param name="fxn"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        private bool setButtonFxn(TenderTypeButton button, ButtonFxn fxn, object parm)
        {
            if (fxn == null || parm == null)
                return(false);

            Button btn = null;
            switch(button)
            {
                case TenderTypeButton.CASH:
                    btn = this.cashButton;
                    break;
                case TenderTypeButton.CHECK:
                    btn = this.checkButton;
                    break;
                case TenderTypeButton.CREDITORBILLAP:
                    btn = this.creditCardButton;
                    break;
                case TenderTypeButton.DEBIT:
                    btn = this.debitCardButton;
                    break;
                case TenderTypeButton.SHOPCREDIT:
                    btn = this.shopCreditButton;
                    break;
                case TenderTypeButton.PAYPAL:
                    btn = this.paypalButton;
                    break;
                default:
                    break;
            }
            var rt = false;
            if (btn != null)
            {
                rt = fxn.Invoke(btn, parm);
            }

            return (rt);
        }
        
        private void TenderTypePanel_Load(object sender, EventArgs e)
        {
            if (DesignMode || CollectionUtilities.isEmpty(this.tenderTypeButtonEnableFlags)) return;

            //Compute the amount to dynamically move buttons up 
            //in the case of the Tender out case to cover
            //the gap made by the check button
            this.buttonMissingAdjustment = 
                this.creditCardButton.Location.Y -
                this.checkButton.Location.Y;

            //Run through the list and then set defaults
            switch(this.tenderPanelType)
            {
                case TenderTypePanelType.NORMALSET:
                    foreach (var s in this.tenderTypeButtonEnableFlags)
                    {
                        if (s == null) continue;
                        var ttB = s.Left;
                        var enabled = s.Right;
                        SetEnableButton(ttB, enabled);
                    }
                    break;
                case TenderTypePanelType.BILLAPSET:
                    //Set the available enable flags for
                    //the bill ap set
                    foreach (var s in this.tenderTypeButtonEnableFlags)
                    {
                        if (s == null) continue;
                        var ttB = s.Left;
                        if (ttB > TenderTypeButton.CREDITORBILLAP)
                        {
                            continue;
                        }
                        var enabled = s.Right;
                        SetEnableButton(ttB, enabled);
                    }

                    //Manually disable buttons not in the bill AP set
                    for (var j = TenderTypeButton.DEBIT; j < TenderTypeButton.TTYPELENGTH; ++j)
                    {
                        this.SetEnableButton(j, false);
                        this.SetVisibleButton(j, false);
                    }
                    break;
                case TenderTypePanelType.TENDEROUT:
                    //Set the available enable flags for
                    //the tender out set
                    foreach (var s in this.tenderTypeButtonEnableFlags)
                    {
                        if (s == null)
                            continue;
                        var ttB = s.Left;
                        if (ttB == TenderTypeButton.CHECK)
                        {
                            continue;
                        }
                        var enabled = s.Right;
                        SetEnableButton(ttB, enabled);
                    }

                    //Manually disable buttons not in the bill AP set
                    for (var j = TenderTypeButton.PAYPAL; j < TenderTypeButton.TTYPELENGTH; ++j)
                    {
                        this.SetEnableButton(j, false);
                        this.SetVisibleButton(j, false);
                    }

                    //Also hide the check button
                    this.SetEnableButton(TenderTypeButton.CHECK, false);
                    this.SetVisibleButton(TenderTypeButton.CHECK, false);

                    //Move buttons after CASH, not including CHECK
                    for (var j = TenderTypeButton.CREDITORBILLAP; j < TenderTypeButton.TTYPELENGTH; ++j)
                    {
                        this.MoveButtonVertical(j, -this.buttonMissingAdjustment);
                    }
                    break;
            }

            //By default, enable only CASH
            this.SetEnableButton(TenderTypeButton.CASH, true);
            this.SetVisibleButton(TenderTypeButton.PAYPAL, false);


            //Set the credit card button proper label
            this.creditCardButton.Text =
                TenderTypePanel.CREDITBILLAPLABELS[(uint)this.tenderPanelType];
            this.creditCardButton.Update();
        }

        private void cashButton_Click(object sender, EventArgs e)
        {
            if (this.buttonClickedHandler != null)
            {
                this.buttonClickedHandler.Invoke(TenderTypeButton.CASH);
            }
        }

        private void checkButton_Click(object sender, EventArgs e)
        {
            if (this.buttonClickedHandler != null)
            {
                this.buttonClickedHandler.Invoke(TenderTypeButton.CHECK);
            }
        }

        private void creditCardButton_Click(object sender, EventArgs e)
        {
            if (this.buttonClickedHandler != null)
            {
                this.buttonClickedHandler.Invoke(TenderTypeButton.CREDITORBILLAP);
            }
        }

        private void debitCardButton_Click(object sender, EventArgs e)
        {
            if (this.buttonClickedHandler != null)
            {
                this.buttonClickedHandler.Invoke(TenderTypeButton.DEBIT);
            }
        }

        private void shopCreditButton_Click(object sender, EventArgs e)
        {
            if (this.buttonClickedHandler != null)
            {
                this.buttonClickedHandler.Invoke(TenderTypeButton.SHOPCREDIT);
            }
        }



        private void paypalButton_Click(object sender, EventArgs e)
        {
            if (this.buttonClickedHandler != null)
            {
                this.buttonClickedHandler.Invoke(TenderTypeButton.PAYPAL);
            }
        }
    }
}

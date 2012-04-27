/*********************************************************************************
* Namespace:       CommonUI.DesktopForms.Pawn.Products.DescribeMerchandise
* Class:           DescribeStones
* 
* Description      Popup Form to View/Edit/Add stones to Described Item of Pawn 
*                  Loan.
* 
* History
* David D Wise, Initial Development
*  PWNU00000677 SMurphy 4/26/2010 Navigation issues and limit on total characters in DescribeMerchandise description (commented out)
*      also some refactoring
*  PWNU00000751 SMurphy 5/17/2010 AnswerCode should remain 0 - only for stone color
*  PWNU00000674 SMurphy 5/26/2010 no defaults and only activate cancel button when rows are valid
*  PWNU00000677 SMurphy 6/1/2010 put in message and disallow Continue when description > 200 char
*  no ticket SMurphy 6/2/2010 issue with Continue button activating correctly for broken stones
*  SR 7/23/2010 Fixed the issue wherein deleting an empty row raised exception
*  Madhu 12/21/2010 fix for bugzilla defect 2
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Objects;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;

namespace Common.Libraries.Forms.Pawn.Products.DescribeMerchandise
{
    public partial class DescribeStones : Form
    {
        private const string COLUMN_CLARITY = "DE_900";
        private const string COLUMN_COLOR = "DE_901";
        private const string COLUMN_POINTS = "DE_14";
        private const string COLUMN_QUANTITY = "DE_366";
        private const string COLUMN_SHAPE = "DE_77";
        private const string COLUMN_TYPE = "DE_11";

        private const int ATTRIBUTE_CODE_CLARITY = 900;
        private const int ATTRIBUTE_CODE_COLOR = 901;
        private const int ATTRIBUTE_CODE_POINTS = 14;
        private const int ATTRIBUTE_CODE_QUANTITY = 15;
        private const int ATTRIBUTE_CODE_SHAPE = 77;
        private const int ATTRIBUTE_CODE_TYPE = 11;

        private CurrentContext _CurrentContext;   // Variable holding desktop flow current context
        private int _CurrentRowIndex;
        // Stores local instance of Jewelry passed to Edit
        private List<string> _requiredAttributes;

        // List of Required Attributes
        public Form ReturnForm { get; set; }

        public DesktopSession DesktopSession { get; set; }

        #region set/get
        public List<JewelrySet> Jewelry { get; set; }
    
        #endregion

        public DescribeStones(DesktopSession desktopSession,CurrentContext currentContext)
        {
            DesktopSession = desktopSession;
            InitializeComponent();
            this.colDelete.HeaderText = DesktopSession.ResourceProperties.OverrideMachineName;
            this.continueEditStonesButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.deleteEditStonesButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.cancelEditStonesButton.BackgroundImage = DesktopSession.ResourceProperties.vistabutton_blue;
            this.BackgroundImage = DesktopSession.ResourceProperties.newDialog_400_BlueScale;
            _CurrentContext = currentContext;
        }

        private void DescribeStones_Load(object sender, EventArgs e)
        {
            Setup();
            _CurrentRowIndex = 0;
        }

        /// <summary>
        /// Initial Setup of Form
        /// </summary>
        private void Setup()
        {
            if (Jewelry.Count == 0)//need a default row - don't have one coming from PFI Verify
                DoDefaultJewelryAdd();

            if (Jewelry.Count > 0)
            {
                int countTextboxValues = 0;
                Jewelry[0].ItemAttributeList.ForEach(delegate(ItemAttribute iaPawnJewelItem)
                                                     {
                                                         string sDescription = iaPawnJewelItem.Description;
                                                         string sColumnName = iaPawnJewelItem.Description;
                                                         bool bIsReadOnly = (_CurrentContext == CurrentContext.READ_ONLY) ? true : iaPawnJewelItem.IsPreAnswered;
                                                         bool bIsRequired = iaPawnJewelItem.IsRequired;
                                                         int iOptionIdx = 0;     // Default Index for Combo Boxes
                                                         bool bIsRestricted = iaPawnJewelItem.IsRestricted;

                                                         List<string> answerList = new List<string>();

                                                         // Shorten column Header Text
                                                         switch (iaPawnJewelItem.AttributeCode)
                                                         {
                                                             case ATTRIBUTE_CODE_QUANTITY:
                                                                 sDescription = "Quantity";
                                                                 sColumnName = COLUMN_QUANTITY;
                                                                 break;
                                                             case ATTRIBUTE_CODE_SHAPE:
                                                                 sDescription = "Shape";
                                                                 sColumnName = COLUMN_SHAPE;
                                                                 break;
                                                             case ATTRIBUTE_CODE_POINTS:
                                                                 sDescription = "Points";
                                                                 sColumnName = COLUMN_POINTS;
                                                                 break;
                                                             case ATTRIBUTE_CODE_TYPE:
                                                                 sDescription = "Type";
                                                                 sColumnName = COLUMN_TYPE;
                                                                 break;
                                                             case ATTRIBUTE_CODE_CLARITY:
                                                                 sDescription = "Clarity";
                                                                 sColumnName = COLUMN_CLARITY;
                                                                 break;
                                                             case ATTRIBUTE_CODE_COLOR:
                                                                 sDescription = "Color";
                                                                 sColumnName = COLUMN_COLOR;
                                                                 break;
                                                         }

                                                         if (bIsReadOnly)
                                                         {
                                                             answerList.Add(iaPawnJewelItem.Answer.AnswerText);
                                                             DataGridViewTextBoxColumn attributeTextBox = new DataGridViewTextBoxColumn();
                                                             attributeTextBox.Name = sColumnName;
                                                             BuildOutAttributeTextBox(ref attributeTextBox, answerList, iaPawnJewelItem.ValidationDataType.ToString(), bIsRestricted);
                                                             AddAttributeToForm(sDescription, attributeTextBox, bIsRequired);
                                                         }
                                                         else
                                                         {
                                                             if (iaPawnJewelItem.AnswerList.Count > 0)
                                                             {
                                                                 switch (iaPawnJewelItem.InputControl)
                                                                 {
                                                                     case ControlType.COMBOBOX_ONLY:
                                                                         iaPawnJewelItem.AnswerList.Sort(delegate(Answer Anwser_1, Answer Answer_2)
                                                                                                         {
                                                                                                             return Anwser_1.DisplayOrder.CompareTo(Answer_2.DisplayOrder);
                                                                                                         });
                                                                         bIsRestricted = countTextboxValues != 2;

                                                                         iaPawnJewelItem.AnswerList.ForEach(delegate(Answer ansAnswerList)
                                                                                                            {
                                                                                                                answerList.Add(ansAnswerList.AnswerText);
                                                                                                            });
                                                                         if (!string.IsNullOrEmpty(iaPawnJewelItem.MaskDefault))
                                                                         {
                                                                             iOptionIdx = iaPawnJewelItem.AnswerList.FindIndex(delegate(Answer ansAnswerList)
                                                                                                                               {
                                                                                                                                   return ansAnswerList.InputKey == iaPawnJewelItem.MaskDefault;
                                                                                                                               });
                                                                         }
                                                                         if (!string.IsNullOrEmpty(iaPawnJewelItem.MaskDefault) && iOptionIdx < 0)
                                                                         {
                                                                             answerList.Add("");
                                                                             iOptionIdx = answerList.Count - 1;
                                                                         }

                                                                         // Build out the ComboBox based upon above criteria
                                                                         DataGridViewComboBoxColumn attributeComboBox_Only = new DataGridViewComboBoxColumn();
                                                                         attributeComboBox_Only.Name = sColumnName;
                                                                         attributeComboBox_Only.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                                                         BuildOutAttributeComboBox(ref attributeComboBox_Only, answerList, iaPawnJewelItem.ValidationDataType.ToString(), iOptionIdx, bIsRestricted);
                                                                         AddAttributeToForm(sDescription, attributeComboBox_Only, bIsRequired);
                                                                         break;
                                                                     case ControlType.COMBOBOX_TEXT_ENABLED:
                                                                         iaPawnJewelItem.AnswerList.Sort(delegate(Answer Anwser_1, Answer Answer_2)
                                                                                                         {
                                                                                                             return Anwser_1.DisplayOrder.CompareTo(Answer_2.DisplayOrder);
                                                                                                         });
                                                                         // FTN 3.3.a.ii.1.c.ii.2
                                                                         //bIsRestricted = false;
                                                                         iaPawnJewelItem.AnswerList.ForEach(delegate(Answer ansAnswerList)
                                                                                                            {
                                                                                                                answerList.Add(ansAnswerList.AnswerText);
                                                                                                            });
                                                                         // FTN 3.3.a.ii.1.c.ii.3.a-b
                                                                         if (!string.IsNullOrEmpty(iaPawnJewelItem.MaskDefault))
                                                                         {
                                                                             iOptionIdx = iaPawnJewelItem.AnswerList.FindIndex(delegate(Answer ansAnswerList)
                                                                                                                               {
                                                                                                                                   return ansAnswerList.InputKey == iaPawnJewelItem.MaskDefault;
                                                                                                                               });
                                                                         }
                                                                         // FTN 3.3.a.ii.1.c.ii.4
                                                                         if (!string.IsNullOrEmpty(iaPawnJewelItem.MaskDefault) && iOptionIdx < 0)
                                                                         {
                                                                             answerList.Add("");
                                                                             iOptionIdx = answerList.Count - 1;
                                                                         }
                                    
                                                                         // Build out the ComboBox based upon above criteria
                                                                         DataGridViewComboBoxColumn attributeComboBox_Text_Enabled = new DataGridViewComboBoxColumn();
                                                                         attributeComboBox_Text_Enabled.Name = sColumnName;
                                                                         attributeComboBox_Text_Enabled.Tag = "DropDown";
                                                                         attributeComboBox_Text_Enabled.DefaultCellStyle.BackColor = Color.White;
                                                                         attributeComboBox_Text_Enabled.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                                                         BuildOutAttributeComboBox(ref attributeComboBox_Text_Enabled, answerList, iaPawnJewelItem.ValidationDataType.ToString(), iOptionIdx, bIsRestricted);
                                                                         AddAttributeToForm(sDescription, attributeComboBox_Text_Enabled, bIsRequired);
                                                                         gvEditStones.DataError += new DataGridViewDataErrorEventHandler(gvEditStones_DataError);
                                                                         break;
                                                                 }
                                                             }
                                                             else
                                                             {
                                                                 // FTN 3.3.a.ii.1.c.iii
                                                                 if (iaPawnJewelItem.InputControl == ControlType.TEXTFIELD)
                                                                 {
                                                                     answerList.Add("");
                                                                     DataGridViewTextBoxColumn attributeBlankTextBox = new DataGridViewTextBoxColumn();
                                                                     attributeBlankTextBox.Name = sColumnName;
                                                                     attributeBlankTextBox.MaxInputLength = 5;
                                                                     attributeBlankTextBox.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                                                     BuildOutAttributeTextBox(ref attributeBlankTextBox, answerList, iaPawnJewelItem.ValidationDataType.ToString(), bIsRestricted);
                                                                     AddAttributeToForm(sDescription, attributeBlankTextBox, bIsRequired);
                                                                     countTextboxValues++;
                                                                 }
                                                             }
                                                         }
                                                     });
            }
            AddCurrentJewelryData();
            deleteEditStonesButton.Enabled = false;
            customButtonAdd.Visible = !(_CurrentContext == CurrentContext.READ_ONLY);
            deleteEditStonesButton.Visible = !(_CurrentContext == CurrentContext.READ_ONLY);
            continueEditStonesButton.Visible = !(_CurrentContext == CurrentContext.READ_ONLY);
        }

        void gvEditStones_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //do nothing
        }

        private void deleteEditStonesButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvEditStones.Rows.Count; i++)
            {
                if (gvEditStones.Rows[i].Cells["colDelete"].Value != null && (bool)gvEditStones.Rows[i].Cells["colDelete"].Value)
                {
                    int iRowIndex = gvEditStones.Rows[i].Index;
                    if (Jewelry.Count > 1 && Jewelry.Count > iRowIndex)
                    {
                        Jewelry.RemoveAt(iRowIndex);
                    }
                    else if (Jewelry.Count == 1)
                    {
                        for (int j = 0; j < Jewelry[0].ItemAttributeList.Count; j++)
                        {
                            ItemAttribute myAttribute = Jewelry[0].ItemAttributeList[j];
                            myAttribute.Answer = new Answer();
                            Jewelry[0].ItemAttributeList.RemoveAt(j);
                            Jewelry[0].ItemAttributeList.Insert(j, myAttribute);
                        }
                    }

                    gvEditStones.Rows.Remove(gvEditStones.Rows[i]);
                    i--;
                }
            }
            EnableDeleteButton();
        }

        private void cancelEditStonesButton_Click(object sender, EventArgs e)
        {
            exitPage();
        }

        private void continueEditStonesButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow myRow in gvEditStones.Rows)
            {
                int iQuantity = 0;
                string sShape = "";
                decimal dPoints = 0;
                string sType = "";
                string sClarity = "";
                string sColor = "";

                if (myRow.Cells[COLUMN_QUANTITY].Value != null && !string.IsNullOrEmpty(myRow.Cells[COLUMN_QUANTITY].Value.ToString().Trim()))
                {
                    if (ValidateRowHasValidValues(myRow))
                    {
                        iQuantity = Convert.ToInt32(myRow.Cells[COLUMN_QUANTITY].EditedFormattedValue);
                        //if (myRow.Cells[COLUMN_SHAPE] != null)
                        sShape = Utilities.GetStringValue(myRow.Cells[COLUMN_SHAPE].EditedFormattedValue, "");
                        //if (myRow.Cells[COLUMN_POINTS] != null)
                        dPoints = Utilities.GetDecimalValue(myRow.Cells[COLUMN_POINTS].EditedFormattedValue, 0);
                        //if (myRow.Cells[COLUMN_TYPE] != null)
                        sType = Utilities.GetStringValue(myRow.Cells[COLUMN_TYPE].EditedFormattedValue, "");
                        //if (myRow.Cells[COLUMN_CLARITY] != null)
                        sClarity = Utilities.GetStringValue(myRow.Cells[COLUMN_CLARITY].EditedFormattedValue, "");
                        if (myRow.Cells[COLUMN_COLOR] != null)
                            sColor = Utilities.GetStringValue(myRow.Cells[COLUMN_COLOR].EditedFormattedValue, "");

                        if (iQuantity > 0 && dPoints > 0)
                        {
                            AddAttributeAnswer(myRow.Index, ATTRIBUTE_CODE_QUANTITY, iQuantity.ToString());
                            AddAttributeAnswer(myRow.Index, ATTRIBUTE_CODE_SHAPE, sShape);
                            AddAttributeAnswer(myRow.Index, ATTRIBUTE_CODE_POINTS, dPoints.ToString());
                            AddAttributeAnswer(myRow.Index, ATTRIBUTE_CODE_TYPE, sType);
                            AddAttributeAnswer(myRow.Index, ATTRIBUTE_CODE_CLARITY, sClarity);
                            AddAttributeAnswer(myRow.Index, ATTRIBUTE_CODE_COLOR, sColor);
                        }
                    }
                    else
                    {
                        MessageBox.Show("You entered an invalid data type. Please re-enter", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (typeof(DescribeItem) == ReturnForm.GetType())
            {
                ((DescribeItem)ReturnForm).AddDescribeStonesAttributes(Jewelry);
                int length = ((DescribeItem)ReturnForm).GenerateDynamicTicketDescriptionLength();
                if (length > 200)
                {
                    MessageBox.Show("Description length must be less than 200, current length = " +
                                    length.ToString(), "Retail Amount Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            this.Close();
        }

        //grid methods
        private void gvEditStones_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (_CurrentContext == CurrentContext.READ_ONLY)
                return;
            EnableDeleteButton();

            if (gvEditStones.CurrentCell.OwningColumn.Tag != null && gvEditStones.CurrentCell.OwningColumn.Tag.Equals("DropDown"))
            {
                DataGridViewComboBoxEditingControl cbo = e.Control as DataGridViewComboBoxEditingControl;
                if (cbo != null)
                {
                    cbo.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
        }
        
        private void gvEditStones_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            /* bool stringUtilsIsdecimal = StringUtilities.IsDecimal(gvEditStones[gvEditStones.Columns[COLUMN_POINTS].Index, e.RowIndex].EditedFormattedValue.ToString().Trim());
            if (gvEditStones.Rows.Count - 1 == e.RowIndex && gvEditStones.Rows.Count < 100 &&
            e.ColumnIndex == gvEditStones.Columns[COLUMN_COLOR].Index && stringUtilsIsdecimal)
            {
            gvEditStones.Rows.Add();
            }*/
        }

        private void gvEditStones_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_CurrentContext == CurrentContext.READ_ONLY)
                return;

            try
            {
                DataGridViewRow gvRow = gvEditStones.CurrentRow;
                if (gvRow == null)
                    return;

                _CurrentRowIndex = e.RowIndex;
                string blanks = "          ";

                switch (e.ColumnIndex)
                {
                    case 1://(gvEditStones.Columns[COLUMN_QUANTITY].Index):
                        if (!String.IsNullOrEmpty(e.FormattedValue.ToString().Trim()) && (!StringUtilities.IsInteger(e.FormattedValue.ToString()) || Convert.ToInt16(e.FormattedValue) < 1))
                        {
                            MessageBox.Show("You entered an invalid data type. Please re-enter", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            gvEditStones[e.ColumnIndex,e.RowIndex].Value = blanks;
                            gvEditStones.RefreshEdit();
                            e.Cancel = true;

                            return;
                        }
                        break;
                    case 2:
                        DataGridViewComboBoxColumn cboColumn = (DataGridViewComboBoxColumn)gvEditStones.Columns[2];

                        if (!cboColumn.Items.Contains(gvRow.Cells[COLUMN_SHAPE].EditedFormattedValue))
                        {
                            cboColumn.Items.Add(gvRow.Cells[COLUMN_SHAPE].EditedFormattedValue);
                        }
                        gvRow.Cells[COLUMN_SHAPE].Value = gvRow.Cells[COLUMN_SHAPE].EditedFormattedValue;
                        break;
                    
                    case 3://gvEditStones.Columns[COLUMN_TYPE].Index:
                        if (gvRow.Cells[COLUMN_TYPE].EditedFormattedValue.Equals("DIA-TEST POS"))
                        {
                            gvRow.Cells[COLUMN_CLARITY].ReadOnly = false;
                            gvRow.Cells[COLUMN_COLOR].ReadOnly = false;
                        }
                        else
                        {
                            gvRow.Cells[COLUMN_CLARITY].ReadOnly = true;
                            gvRow.Cells[COLUMN_CLARITY].Value = string.Empty;
                            gvRow.Cells[COLUMN_COLOR].ReadOnly = true;
                            gvRow.Cells[COLUMN_COLOR].Value = string.Empty;
                        }
                        DataGridViewComboBoxColumn cboComboColumn = (DataGridViewComboBoxColumn)gvEditStones.Columns[3];

                        if (!cboComboColumn.Items.Contains(gvRow.Cells[COLUMN_TYPE].EditedFormattedValue))
                        {
                            cboComboColumn.Items.Add(gvRow.Cells[COLUMN_TYPE].EditedFormattedValue);
                        }
                        gvRow.Cells[COLUMN_TYPE].Value = gvRow.Cells[COLUMN_TYPE].EditedFormattedValue;

                        break;

                        //Madhu 12/21/2010 fix for bugzilla defect 2
                    case 4://(gvEditStones.Columns[COLUMN_POINTS].Index):
                        if (Convert.ToInt16(gvRow.Cells[COLUMN_QUANTITY].EditedFormattedValue) > 0 &&
                            (!StringUtilities.IsFloat(e.FormattedValue.ToString()) || Convert.ToDouble(e.FormattedValue) < 0.5))
                        {
                            MessageBox.Show("You entered an invalid data type. Please re-enter", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            gvEditStones[e.ColumnIndex,e.RowIndex].Value = blanks;
                            gvEditStones.RefreshEdit();
                            e.Cancel = true;

                            return;
                        }

                        //Madhu 12/21/2010 changed from ToInt16 to ToInt64
                        if (Convert.ToInt64(e.FormattedValue) > 999)
                        {
                            MessageBox.Show("Points can only be less than 999", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            gvEditStones[e.ColumnIndex,e.RowIndex].Value = blanks;
                            gvEditStones.RefreshEdit();
                            e.Cancel = true;

                            return;
                        }

                        if (gvRow.Cells[COLUMN_TYPE].EditedFormattedValue.Equals("DIA-TEST POS") && Convert.ToInt16(e.FormattedValue) > 0)
                        {
                            gvRow.Cells[COLUMN_CLARITY].ReadOnly = false;
                            //gvRow.Cells[COLUMN_COLOR].ReadOnly = false;
                        }
                        else
                        {
                            gvRow.Cells[COLUMN_CLARITY].ReadOnly = true;
                            gvRow.Cells[COLUMN_CLARITY].Value = string.Empty;
                            gvRow.Cells[COLUMN_COLOR].ReadOnly = true;
                            gvRow.Cells[COLUMN_COLOR].Value = string.Empty;
                        }

                        break;

                    case 5://(gvEditStones.Columns[COLUMN_CLARITY].Index):
                        // FTN 3.3.d.i.7 Enabled only when Clarity is <Good, Better, Best>
                        if (e.FormattedValue.ToString().IndexOf("CL2") >= 0 || e.FormattedValue.ToString().IndexOf("CL3") >= 0 ||
                            e.FormattedValue.ToString().IndexOf("CL4") >= 0)
                        {
                            gvRow.Cells[COLUMN_COLOR].ReadOnly = false;
                        }
                        else
                        {
                            gvRow.Cells[COLUMN_COLOR].ReadOnly = true;
                            gvRow.Cells[COLUMN_COLOR].Value = string.Empty;
                        }
                        break;
                }
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Exception thrown: {0}", eX);
                }
                BasicExceptionHandler.Instance.AddException("DescribeStones", eX);
            }
        }

        private void gvEditStones_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (gvEditStones.Columns[e.ColumnIndex].Name == gvEditStones.Columns[COLUMN_CLARITY].Name
                && !gvEditStones.Rows[_CurrentRowIndex].Cells[COLUMN_CLARITY].ReadOnly)
            {
                DescribeStones_Images myForm = new DescribeStones_Images(DesktopSession);
                myForm.UpdateClarity += new DescribeStones_Images.ClarityHandler(Stone_UpdateClarity);
                myForm.ShowDialog();
            }
        }

        private void gvEditStones_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            LastRowCheck();
        }

        private void gvEditStones_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            LastRowCheck();
        }

        //// FTN 3.3.c.iii Validates values in each row's fields and returns Boolean
        private bool ValidateRowHasValidValues(DataGridViewRow gvRow)
        {
            try
            {
                if (Utilities.GetIntegerValue(gvRow.Cells[COLUMN_QUANTITY].EditedFormattedValue, 0) < 1)
                    return false;
                if (string.IsNullOrEmpty(Utilities.GetStringValue(gvRow.Cells[COLUMN_SHAPE].EditedFormattedValue, string.Empty)))
                    return false;
                //Madhu 12/21/2010 fix for bugzilla defect 2
                if (Utilities.GetDoubleValue(gvRow.Cells[COLUMN_POINTS].EditedFormattedValue, 0) < 0.5)
                    return false;
                if (string.IsNullOrEmpty(Utilities.GetStringValue(gvRow.Cells[COLUMN_TYPE].EditedFormattedValue, string.Empty)))
                    return false;
                if (Utilities.GetStringValue(gvRow.Cells[COLUMN_TYPE].EditedFormattedValue, string.Empty) == "DIA-TEST POS" &&
                    Utilities.GetDecimalValue(gvRow.Cells[COLUMN_POINTS].EditedFormattedValue, 0) < 1)
                    return (!string.IsNullOrEmpty(Utilities.GetStringValue(gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue, string.Empty)));
                if (Utilities.GetStringValue(gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue, string.Empty).IndexOf("CL2", System.StringComparison.Ordinal) >= 0
                    || Utilities.GetStringValue(gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue, string.Empty).IndexOf("CL3", System.StringComparison.Ordinal) >= 0
                    || Utilities.GetStringValue(gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue, string.Empty).IndexOf("CL4", System.StringComparison.Ordinal) >= 0)
                    return (!string.IsNullOrEmpty(Utilities.GetStringValue(gvRow.Cells[COLUMN_COLOR].EditedFormattedValue, string.Empty)));
                if (Utilities.GetStringValue(gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue, string.Empty).IndexOf("CL1", System.StringComparison.Ordinal) >= 0
                    && !string.IsNullOrEmpty(Utilities.GetStringValue(gvRow.Cells[COLUMN_COLOR].EditedFormattedValue, string.Empty)))
                    gvRow.Cells[COLUMN_COLOR].Value = string.Empty;

                return true;
            }
            catch
            {
                return false;
            }
        }

        // FTN 3.3.c.vi Only enabled Delete button if not on the first row and 
        // at least one delete button is checked
        private void EnableDeleteButton()
        {
            gvEditStones.EndEdit();
            deleteEditStonesButton.Enabled = false;
            foreach (DataGridViewRow gvrRow in gvEditStones.Rows)
            {
                if (gvrRow.Cells["colDelete"].Value != null && (bool)gvrRow.Cells["colDelete"].Value)// && gvEditStones.Rows.Count > 1)
                {
                    deleteEditStonesButton.Enabled = true;
                    break;
                }
            }
            if (gvEditStones.RowCount == 0)
            {
                gvEditStones.Rows.Add();
            }
        }

        // Add the Attribute Control to the Form
        private void AddAttributeToForm(string sDescriptionLabel, DataGridViewColumn attributeControl, bool bIsRequired)
        {
            attributeControl.HeaderText = sDescriptionLabel;
            if (bIsRequired)
            {
                if (_requiredAttributes == null)
                    _requiredAttributes = new List<string>();
                _requiredAttributes.Add(attributeControl.Name);
            }
            // Add Controls to Form
            gvEditStones.Columns.Add(attributeControl);
        }

        // Add Answer to associated ItemAttribute
        private void AddAttributeAnswer(int iRowIndex, int iAttributeCode, string sValueOfControl)
        {
            if (iRowIndex >= Jewelry.Count)
            {
                JewelrySet newJewelrySet = new JewelrySet();
                newJewelrySet.CaccLevel = Jewelry[0].CaccLevel;
                newJewelrySet.Category = Jewelry[0].Category;
                newJewelrySet.CategoryDescription = Jewelry[0].CategoryDescription;
                newJewelrySet.ItemAttributeList = new List<ItemAttribute>();

                for (int i = 0; i < Jewelry[0].ItemAttributeList.Count; i++)
                {
                    ItemAttribute newItemAttribute = new ItemAttribute();
                    newItemAttribute.AnswerList = Jewelry[0].ItemAttributeList[i].AnswerList;
                    newItemAttribute.AttributeCode = Jewelry[0].ItemAttributeList[i].AttributeCode;
                    newItemAttribute.Description = Jewelry[0].ItemAttributeList[i].Description;
                    newItemAttribute.DescriptionOrder = Jewelry[0].ItemAttributeList[i].DescriptionOrder;
                    newItemAttribute.InputControl = Jewelry[0].ItemAttributeList[i].InputControl;
                    newItemAttribute.InputType = Jewelry[0].ItemAttributeList[i].InputType;
                    newItemAttribute.IsIncludedInDescription = Jewelry[0].ItemAttributeList[i].IsIncludedInDescription;
                    newItemAttribute.IsPreAnswered = Jewelry[0].ItemAttributeList[i].IsPreAnswered;
                    newItemAttribute.IsRequired = Jewelry[0].ItemAttributeList[i].IsRequired;
                    newItemAttribute.IsRestricted = Jewelry[0].ItemAttributeList[i].IsRestricted;
                    newItemAttribute.LoanOrder = Jewelry[0].ItemAttributeList[i].LoanOrder;
                    newItemAttribute.MaskDefault = Jewelry[0].ItemAttributeList[i].MaskDefault;
                    newItemAttribute.MaskLevel = Jewelry[0].ItemAttributeList[i].MaskLevel;
                    newItemAttribute.MaskOrder = Jewelry[0].ItemAttributeList[i].MaskOrder;
                    newItemAttribute.PFIOrder = Jewelry[0].ItemAttributeList[i].PFIOrder;
                    newItemAttribute.Prefix = Jewelry[0].ItemAttributeList[i].Prefix;
                    newItemAttribute.Suffix = Jewelry[0].ItemAttributeList[i].Suffix;
                    newItemAttribute.ValidationDataType = Jewelry[0].ItemAttributeList[i].ValidationDataType;
                    newJewelrySet.ItemAttributeList.Add(newItemAttribute);
                }
                Jewelry.Add(newJewelrySet);
            }

            ItemAttribute attributeFind = Jewelry[iRowIndex].ItemAttributeList.Find(delegate(ItemAttribute att)
                                                                                    {
                                                                                        return att.AttributeCode == iAttributeCode;
                                                                                    });

            int iAttributeIndex = Jewelry[iRowIndex].ItemAttributeList.FindIndex(delegate(ItemAttribute att)
                                                                                 {
                                                                                     return att.AttributeCode == iAttributeCode;
                                                                                 });
            Answer newAnswer = Jewelry[iRowIndex].ItemAttributeList[iAttributeIndex].Answer;
            Answer attributeAnswer;
            if (Jewelry[iRowIndex].ItemAttributeList[iAttributeIndex].AnswerList != null && Jewelry[iRowIndex].ItemAttributeList[iAttributeIndex].AnswerList.Count > 0)
            {
                attributeAnswer = Jewelry[iRowIndex].ItemAttributeList[iAttributeIndex].AnswerList.Find(
                    a => a.AnswerText == sValueOfControl);

                //SMurphy AnswerCode should remain 0 - but only for stone
                if (Jewelry[iRowIndex].ItemAttributeList[iAttributeIndex].Description.Contains("Color"))
                    newAnswer.AnswerCode = attributeAnswer.AnswerCode;
                else
                    newAnswer.AnswerCode = attributeAnswer.AnswerCode == 0 ? 999 : attributeAnswer.AnswerCode;
            }
            else
                newAnswer.AnswerCode = 999;

            newAnswer.AnswerText = sValueOfControl;
            newAnswer.InputKey = null;
            newAnswer.OutputKey = null;

            attributeFind.Answer = newAnswer;
            if (iRowIndex < Jewelry.Count)
            {
                Jewelry[iRowIndex].ItemAttributeList.RemoveAt(iAttributeIndex);
                Jewelry[iRowIndex].ItemAttributeList.Insert(iAttributeIndex, attributeFind);
            }
        }

        // Build the properties of the Attributed ComboBox
        private void BuildOutAttributeComboBox(ref DataGridViewComboBoxColumn attributeControl, List<string> answerList, string sValidationType, int iOptionIdx, bool bIsRestricted)
        {
            if (iOptionIdx >= 0)
                attributeControl.DefaultCellStyle.NullValue = answerList[iOptionIdx];
            
            attributeControl.Items.Add(string.Empty);
            attributeControl.DefaultCellStyle.NullValue = string.Empty;
            if (!bIsRestricted)
            {
                attributeControl.Tag = "DropDown";
            }
            //Add the answerList to the ComboBox Items Collection        
            foreach (string sAnswer in answerList)
            {
                attributeControl.Items.Add(BuildOutValidationTypeFormat(sAnswer, sValidationType));
            }
        }

        // Build the properties of the Attribute TextBox Control
        private void BuildOutAttributeTextBox(ref DataGridViewTextBoxColumn attributeControl, List<string> answerList, string sValidationType, bool bIsRestricted)
        {
            attributeControl.ReadOnly = bIsRestricted;
        }

        // FTN 3.3.a.ii.2 Format the answer to correct Validation Type format
        private string BuildOutValidationTypeFormat(string sAnswer, string sValidationType)
        {
            switch (sValidationType)
            {
                case "":
                case "S":
                    return sAnswer;
                case "A":
                    return String.Format("{0:C}", Convert.ToDecimal(sAnswer));
                case "F":
                    return String.Format("{0:0.00}", Convert.ToDecimal(sAnswer));
                case "N":
                    return String.Format("{0:0}", Convert.ToInt32(sAnswer));
                case "D":
                    return String.Format("{0:d}", Convert.ToDecimal(sAnswer));
                default:
                    return sAnswer;
            }
        }

        private void exitPage()
        {
            this.Close();
        }

        void Stone_UpdateClarity(int iClarityIndex)
        {
            DataGridViewComboBoxCell dataGridViewComboBoxColumn =
            (DataGridViewComboBoxCell)gvEditStones.Rows[_CurrentRowIndex].Cells[COLUMN_CLARITY];

            string sClarityValue = "";

            DataGridViewComboBoxColumn d = (DataGridViewComboBoxColumn)gvEditStones.Columns[COLUMN_CLARITY];
            sClarityValue = d.Items[iClarityIndex].ToString();

            gvEditStones.EndEdit();
            dataGridViewComboBoxColumn.Value = sClarityValue;
            gvEditStones.Rows[_CurrentRowIndex].Cells[COLUMN_COLOR].Selected = true;
            //          gvEditStones.UpdateCellValue(gvEditStones.Columns[COLUMN_CLARITY].Index, _CurrentRowIndex); 
        }

        private void AddCurrentJewelryData()
        {
            int iRowIndex = 0;
            Jewelry.ForEach(delegate(JewelrySet iaJewelrySet)
                            {
                                gvEditStones.Rows.Add();
                                iaJewelrySet.ItemAttributeList.ForEach(delegate(ItemAttribute iaPawnJewelItem)
                                                                       {
                                                                           string sControlName = iaPawnJewelItem.Description;
                                                                           bool bInitialReadOnly = false;

                                                                           switch (iaPawnJewelItem.AttributeCode)
                                                                           {
                                                                               case 15:
                                                                                   sControlName = COLUMN_QUANTITY;
                                                                                   break;
                                                                               case 77:
                                                                                   sControlName = COLUMN_SHAPE;
                                                                                   break;
                                                                               case 14:
                                                                                   sControlName = COLUMN_POINTS;
                                                                                   break;
                                                                               case 11:
                                                                                   sControlName = COLUMN_TYPE;
                                                                                   break;
                                                                               case 900:
                                                                                   sControlName = COLUMN_CLARITY;
                                                                                   bInitialReadOnly = true;
                                                                                   break;
                                                                               case 901:
                                                                                   sControlName = COLUMN_COLOR;
                                                                                   bInitialReadOnly = true;
                                                                                   break;
                                                                           }
                                                                           if (iaPawnJewelItem.Answer.AnswerText != null)
                                                                           {
                                                                               gvEditStones[sControlName,iRowIndex].Value = iaPawnJewelItem.Answer.AnswerText;
                                                                           }
                                                                           gvEditStones[sControlName,iRowIndex].ReadOnly = bInitialReadOnly;
                                                                       });
                                iRowIndex++;
                            });
        }

        private void DoDefaultJewelryAdd()
        {
            DescribedMerchandise tmpPawnItem = new DescribedMerchandise(1700);
            Jewelry = tmpPawnItem.SelectedPawnItem.Jewelry;
        }

        private void LastRowCheck()
        {
            DataGridViewRow gvRow = gvEditStones.CurrentRow;

            //check to enable button shape, qty & points must have data
            if (!gvRow.Cells[COLUMN_POINTS].EditedFormattedValue.Equals(string.Empty) && !gvRow.Cells[COLUMN_QUANTITY].EditedFormattedValue.Equals(string.Empty) &&
                !gvRow.Cells[COLUMN_SHAPE].EditedFormattedValue.Equals(string.Empty) && gvRow.Cells[COLUMN_CLARITY] != null)
            {
                //if is diamond, not promo clarity or broken & has color selected
                if (gvRow.Cells[COLUMN_TYPE].EditedFormattedValue.Equals("DIA-TEST POS") && !gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue.Equals(string.Empty) &&
                    gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue.ToString().IndexOf("CL1", System.StringComparison.Ordinal) == -1 && gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue.ToString().IndexOf("CL5", System.StringComparison.Ordinal) == -1 &&
                    !gvRow.Cells[COLUMN_COLOR].EditedFormattedValue.Equals(string.Empty))
                    continueEditStonesButton.Enabled = true;
                //if is diamond is promo clarity and no color selected 
                else if (gvRow.Cells[COLUMN_TYPE].EditedFormattedValue.Equals("DIA-TEST POS") && (gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue.ToString().IndexOf("CL1", System.StringComparison.Ordinal) > -1 ||
                                                                                                  gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue.ToString().IndexOf("CL5", System.StringComparison.Ordinal) > -1))
                {
                    gvRow.Cells[COLUMN_COLOR].Value = string.Empty;
                    continueEditStonesButton.Enabled = true;
                }
                //not diamond no clarity & no color selected
                else if (!gvRow.Cells[COLUMN_TYPE].EditedFormattedValue.Equals("DIA-TEST POS") && gvRow.Cells[COLUMN_CLARITY].EditedFormattedValue.Equals(string.Empty) &&
                         gvRow.Cells[COLUMN_COLOR].EditedFormattedValue.Equals(string.Empty))
                    continueEditStonesButton.Enabled = true;
                else
                    continueEditStonesButton.Enabled = false;
            }
            //blank row
            else if (gvRow.Cells[COLUMN_POINTS].EditedFormattedValue.Equals(string.Empty) && gvRow.Cells[COLUMN_QUANTITY].EditedFormattedValue.Equals(string.Empty) &&
                     gvRow.Cells[COLUMN_SHAPE].EditedFormattedValue.Equals(string.Empty) && gvRow.Cells[COLUMN_TYPE].EditedFormattedValue.Equals(string.Empty) &&
                     gvRow.Cells[COLUMN_CLARITY].FormattedValue.Equals(string.Empty))
                continueEditStonesButton.Enabled = true;
            else
                continueEditStonesButton.Enabled = false;
        }

        private void gvEditStones_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_CurrentContext == CurrentContext.READ_ONLY)
                return;
            if (e.RowIndex >= 0)
                _CurrentRowIndex = e.RowIndex;
        }

        private void gvEditStones_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_CurrentContext == CurrentContext.READ_ONLY)
                return;
            EnableDeleteButton();
        }

        private void customButtonAdd_Click(object sender, EventArgs e)
        {
            gvEditStones.Rows.Add();
        }
    }
}
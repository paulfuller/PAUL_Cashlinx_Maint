/********************************************************************
* Namespace: CashlinxDesktop.Desktop.PrintQueue
* FileName: TransferOutReport
* Prints the store to store transfer report
* Sreelatha Rengarajan 1/20/2010 Initial version
 * SR 3/17/10 Added store name
*******************************************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Rules.Structure;
using Common.Libraries.Utility.BarcodeGenerator;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;

namespace Pawn.Logic.PrintQueue
{
    public partial class TransferOutScrapsReport : Form
    {
        private Bitmap _bitMap;
        
        public List<TransferItemVO> MdseTransfer
        {
            get;
            set;
        }
        public List<IItem> PawnItems { get; set; }

        private List<AggScrapItemInfo> _aggData = new List<AggScrapItemInfo>();

        private List<string> _listOtherTypes = new List<string>();
        private List<string> _listGoldTypes = new List<string>();

        private static string _STORE_NAME;


        public TransferOutScrapsReport()
        {
            InitializeComponent();
        }

        private void TransferOutReport_Load(object sender, EventArgs e)
        {
            try
            {
                _STORE_NAME = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                labelDate.Text = ShopDateTime.Instance.ShopDate.ToShortDateString();
                labelFromStore.Text = GlobalDataAccessor.Instance.CurrentSiteId.StoreName + "#" +
                                      _STORE_NAME;
                labelDestStore.Text = "CATCO";
                labelEmpID.Text = GlobalDataAccessor.Instance.DesktopSession.UserName;
                int numOfItems = 0;
                decimal totalCost = 0;
                int pageNo = 1;
                int mdseTransferItemsPerPage = 8;
                int remainder = 0;
                Math.DivRem(MdseTransfer.Count, mdseTransferItemsPerPage, out remainder);
                int numberOfPages = (MdseTransfer.Count / mdseTransferItemsPerPage);
                int numOfPages = remainder > 0 ? numberOfPages + 1 : numberOfPages;
                int rowNum = 0;
                int rowNumOther = 0;
                int itemCount = 0;

                foreach (TransferItemVO transferObj in MdseTransfer)
                {
                    numOfItems++;
                    itemCount++;
                    rowNum++;

                    labelTransferNo.Text = transferObj.TransferNumber.ToString();

                    Image barcodeImage = Barcode.DoEncode(
                        EncodingType.CODE128C, transferObj.TransferNumber.ToString());
                    if (barcodeImage != null)
                        pictureBoxBarCode.Image = barcodeImage;
                    else
                        pictureBoxBarCode.Visible = false;

                    labelPageNo.Text = String.Format("Page {0} of {1}", pageNo, numOfPages);

                    //1 Number
                    CreateNewColumn(
                        tableLayoutPanelTransferItems, 
                        numOfItems.ToString(), 
                        0, 
                        rowNum);
                    //2 ICN
                    CreateNewColumn(
                        tableLayoutPanelTransferItems, 
                        transferObj.ICN.ToString(), 
                        1, 
                        rowNum);
                    //3 item description
                    CreateNewColumn(
                        tableLayoutPanelTransferItems,
                        String.Format("[{0}]{1}", transferObj.ICNQty, transferObj.ItemDescription),
                        2,
                        rowNum);
                    //4 item cost
                    CreateNewColumn(
                        tableLayoutPanelTransferItems,
                        String.Format("{0:C}", transferObj.ItemCost),
                        3,
                        rowNum);

                    totalCost += transferObj.ItemCost;

                    if (itemCount >= 30)
                    {
                        itemCount = 0;
                        if (pageNo == numOfPages)
                        {
                            labelTotalCostHeading.Visible = true;
                            labelTotalCost.Visible = true;
                            labelTotalCost.Text = String.Format("{0:C}", totalCost);
                        }
                        else
                        {
                            labelTotalCostHeading.Visible = false;
                            labelTotalCost.Visible = false;
                        }
                        Print();
                        tableLayoutPanelTransferItems.Controls.Clear();
                        pageNo++;
                        rowNum = 0;
                    }
                } //end foreach (TransferItemVO transferObj in MdseTransfer)

                if (PawnItems != null && PawnItems.Count > 0 && PawnItems[0] is ScrapItem)
                {
                    //Do headers for bottom table.
                    CreateNewColumn(tblDetails, "Gold", 0, 0);
                    CreateNewColumn(tblDetails, "Total Weight", 1, 0);
                    CreateNewColumn(tblDetails, "Total Cost", 2, 0);

                    CreateNewColumn(tblDetails2, "Other", 0, 0);
                    CreateNewColumn(tblDetails2, "Total Weight", 1, 0);
                    CreateNewColumn(tblDetails2, "Total Cost", 2, 0);

                    PopulateMetalTypes();
                    AggregateData();

                    rowNum = 1;
                    rowNumOther = 1;
                    //Order the data.
                    _aggData = (from a in _aggData
                                orderby a.ApproximateKarats
                                select a).ToList();

                    foreach (AggScrapItemInfo aggInfo in _aggData)
                    {
                        if (aggInfo.IsGold)
                        {
                            CreateNewColumn(tblDetails, aggInfo.ApproximateKarats, 0, rowNum);
                            CreateNewColumn(tblDetails, aggInfo.Weight.ToString(), 1, rowNum);
                            CreateNewColumn(tblDetails,
                                            String.Format("{0:C}", aggInfo.Cost),
                                            2,
                                            rowNum);
                            rowNum++;
                        }
                        else
                        {
                            CreateNewColumn(tblDetails2, aggInfo.TypeOfMetal, 0, rowNumOther);
                            CreateNewColumn(tblDetails2, aggInfo.Weight.ToString(), 1, rowNumOther);
                            CreateNewColumn(tblDetails2,
                                            String.Format("{0:C}", aggInfo.Cost),
                                            2,
                                            rowNumOther);
                            rowNumOther++;
                        }
                    }

                    ////The following will be executed if there is only 1 page to print
                    //if (tableLayoutPanelTransferItems.Controls.Count > 0)
                    //{
                    //    labelTotalCostHeading.Visible = true;
                    //    labelTotalCost.Visible = true;
                    //    labelTotalCost.Text = String.Format("{0:C}", totalCost);
                    //    //Bug 8, print the document twice
                    //    Print(2);
                    //}
                } //end print aggregate data if Scrap transferred

                //The following will be executed if there is only 1 page to print
                if (tableLayoutPanelTransferItems.Controls.Count > 0)
                {
                    labelTotalCostHeading.Visible = true;
                    labelTotalCost.Visible = true;
                    labelTotalCost.Text = String.Format("{0:C}", totalCost);
                    Print(2);
                }
            }
            catch(Exception ex)
            {
                if (FileLogger.Instance.IsLogDebug)
                    FileLogger.Instance.logMessage(LogLevel.DEBUG, null, 
                        "Error Generating Transfer Scraps Report: " + ex.Message + Environment.NewLine + "Trace: " + ex.StackTrace);
            }
            finally
            {
                Application.DoEvents();
                Close();
            }
        }

        private void Print()
        {
            _bitMap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            this.DrawToBitmap(_bitMap, new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            PrintingUtilities.PrintBitmapDocument(_bitMap, GlobalDataAccessor.Instance.DesktopSession);
        }

        /**
         * Defect Fix 0008, John k Added method to print the document more than once
         * Added 
         * */
        private void Print(int noOfTimes)
        {
            _bitMap = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            this.DrawToBitmap(_bitMap, new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height));
            for (int i = 0; i < noOfTimes; i++)
            {
                PrintingUtilities.PrintBitmapDocument(_bitMap, GlobalDataAccessor.Instance.DesktopSession);
            }
        }

        private void CreateNewColumn(TableLayoutPanel panel, string text, int colNo, int rowNo)
        {
            Label newLabel = new Label();
            newLabel.AutoSize = true;
            newLabel.Font = this.labelNumberHeading.Font;
            newLabel.Text = text;
            panel.Controls.Add(newLabel, colNo, rowNo);
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private BusinessRuleVO GetBusinessRule(string sBusinessRule)
        {
            BusinessRuleVO _BusinessRule = null;
            if (GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO.ContainsKey(sBusinessRule))
            {
                _BusinessRule = GlobalDataAccessor.Instance.DesktopSession.PawnBusinessRuleVO[sBusinessRule];
            }

            return _BusinessRule;
        }

        private void PopulateMetalTypes()
        {
            bool bPawnValue;
            string sMetalType = String.Empty;
            string sComponentValue = String.Empty;

            BusinessRuleVO brMETAL_TYPES = GetBusinessRule("PWN_BR-061");
            bPawnValue = brMETAL_TYPES.getComponentValue("GOLD_TYPES", ref sComponentValue);
            if (bPawnValue)
            {
                _listGoldTypes.AddRange(sComponentValue.Split('|'));

                //if (listGoldTypes.FindIndex(delegate(string s)
                //{
                //    return s == _Item.Attributes[iMetalIdx].Answer.AnswerText;
                //}) >= 0)
                //{
                //    sMetalType = "GOLD";
                //}
            }

            if (sMetalType == String.Empty)
            {
                bPawnValue = brMETAL_TYPES.getComponentValue("OTHER_TYPES", ref sComponentValue);
                if (bPawnValue)
                {
                    _listOtherTypes.AddRange(sComponentValue.Split('|'));

                    //if (listOtherTypes.FindIndex(delegate(string s)
                    //{
                    //    return s == _Item.Attributes[iMetalIdx].Answer.AnswerText;
                    //}) >= 0)
                    //{
                    //    sMetalType = "PLATINUM";
                    //}
                }
            }


        }

        private void AggregateData()
        {
            foreach (IItem item in PawnItems)
            {
                bool isGold = false;
                if (item is ScrapItem)
                {
                    ScrapItem scrap = item as ScrapItem;
                    if (_listGoldTypes.Contains(scrap.TypeOfMetal))
                    {
                        isGold = true;
                    }
                    // Have we added this type of ScrapItem to _aggData yet?
                    var results = from a in _aggData
                                  where a.IsGold == isGold
                                        && a.TypeOfMetal == scrap.TypeOfMetal
                                        && a.ApproximateKarats == (scrap.ApproximateKarats + "K")
                                  select a;

                    AggScrapItemInfo info;
                    if (results.Any())
                    {
                        // Increment existing _aggData Cost and Weight values.
                        info = results.First();
                        info.Cost += scrap.ItemAmount;
                        info.Weight += Convert.ToDecimal(scrap.ApproximateWeight);
                    }
                    else
                    {
                        // Add ScrapItem to _aggData since not added yet.
                        info = new AggScrapItemInfo();
                        info.IsGold = isGold;
                        info.TypeOfMetal = scrap.TypeOfMetal;
                        info.ApproximateKarats = scrap.ApproximateKarats + "K";
                        info.Weight = Convert.ToDecimal(scrap.ApproximateWeight);
                        info.Cost = scrap.ItemAmount;
                        _aggData.Add(info);
                    }
                } //end if (item is ScrapItem)
            } //end foreach (PawnItems)
        }

        #region Class

        /// <summary>
        /// Class for aggregating scrap data for a report.
        /// </summary>
        internal class AggScrapItemInfo
        {
            public AggScrapItemInfo()
            {
            }

            public string TypeOfMetal {get; set;}
            public string ApproximateKarats { get; set; }
            public decimal Weight { get; set; }
            public decimal Cost { get; set; }
            public bool IsGold { get; set; }
        }

        #endregion Class
    }
}

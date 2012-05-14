/*RWB:  Updated code to print 3 copies of the Merchandise Purchase ticket per BZ-0128
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.Controllers.Application;
using Common.Controllers.Database.Couch;
using Common.Controllers.Security;
using Common.Libraries.Objects.Business;
using Common.Libraries.Objects.Customer;
using Common.Libraries.Objects.Purchase;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using Utilities = Common.Libraries.Utility.Utilities;
using Common.Libraries.Utility;

namespace Common.Controllers.Database
{
    public class PurchaseDocumentGenerator
    {
        private const string COMMA_SPACE = ", ";
        private const string SPACE = " ";
        public const string ISVENDOR_METATAG = "IsVendor";
        public const string ISPURCHASE_METATAG = "IsPurchase";
        public const string ISRETURN_METATAG = "IsReturn";
        public const string PURCHASE_AUXINFOTAG = "purchase";
        public static object mutex = new object();

        public class BuyDocumentReportStorage
        {
            public Font HeaderTitleFont { private set; get; }

            public Font BuyLabelFont { private set; get; }

            public Font StandardFont { private set; get; }

            public Font StandardBoldFont { private set; get; }

            public Font TableFont { private set; get; }

            public string BuyReportTitle { private set; get; }

            public string BuyLabel { private set; get; }

            public string BuyNumber { set; get; }

            public string UserId { set; get; }

            public string DiscloseStatement { private set; get; }

            public string NameData { set; get; }

            public string AddressData { set; get; }

            public string LocationData { set; get; }

            public string TransactionDateTime { set; get; }

            public string ShopData { set; get; }

            public string ShopLocationData { set; get; }

            public bool IsCustomerDataFirst { private set; get; }

            public bool IsReturn { private set; get; }

            public bool IsVendor { private set; get; }

            public string ItemHeader { private set; get; }

            public string SecondaryItemHeader { private set; get; }

            public string CustomerPurchaseDisclaimerLine1 { private set; get; }

            public string CustomerPurchaseDisclaimerLine2 { private set; get; }

            public string CustomerNameData { set; get; }

            public string CustomerAddressData { set; get; }

            public string CustomerLocationData { set; get; }

            public string CustomerPhoneNumber { set; get; }

            public string VendorPhoneNumber { set; get; }

            public string FederalTaxId { set; get; }

            public string Identification { set; get; }

            public IdentificationVO IdentificationInfo { set; get; }

            //Customer vitals
            public string DateOfBirth { set; get; }

            public string Height { set; get; }

            public string Age { set; get; }

            public string Sex { set; get; }

            public string Weight { set; get; }

            public string Race { set; get; }

            public string TenderType { set; get; }

            // Footer Constants
            public string IndemnityStatementLine1 { private set; get; }

            public string IndemnityStatementLine2 { private set; get; }

            public string IndemnityStatementLine3 { private set; get; }

            public PdfTemplate PageXYTemplate { set; get; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="isReturn">Set to true if this is a return</param>
            /// <param name="isVendor">Set to true if the customer is a vendor</param>
            public BuyDocumentReportStorage(bool isReturn, bool isVendor)
            {
                ItemHeader = string.Empty;
                SecondaryItemHeader = string.Empty;
                IsReturn = isReturn;
                IsVendor = isVendor;
                IsCustomerDataFirst = IsReturn;

                int fontOffset = 0;
                if (!IsReturn && !IsVendor && GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
                {
                    // Ohio is a 1/2 page report
                    fontOffset = -2;
                }

                //Initialize fonts
                this.BuyLabelFont =
                    new Font(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 8 + fontOffset);
                this.HeaderTitleFont =
                    new Font(BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10 + fontOffset);
                this.StandardFont =
                    new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10 + fontOffset);
                this.StandardBoldFont =
                    new Font(BaseFont.CreateFont(BaseFont.TIMES_BOLD, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 10 + fontOffset);
                this.TableFont =
                    new Font(BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 8 + fontOffset);

                if (!this.IsReturn)
                {
                    this.ItemHeader = @"Items conveyed";
                    this.SecondaryItemHeader = @"(include serial numbers)";
                    // Footer
                    this.IndemnityStatementLine1 = @"I CERTIFY THAT THIS INFORMATION IS TRUE AND COMPLETE";
                    this.IndemnityStatementLine2 = @"AND I HAVE THE RIGHT TO POSSESS AND SELL THIS PROPERTY.";

                    if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Oklahoma)) // Oklahoma
                    {
                        this.IndemnityStatementLine3 = @"I HAVE OWNED THE MERCHANDISE DESCRIBED ABOVE FOR_______(YEARS/MONTHS/DAYS).";
                    }
                }
                else
                {
                    this.ItemHeader = @"Items Returned";
                    this.SecondaryItemHeader = string.Empty;
                    //Footer
                    this.IndemnityStatementLine1 =
                        @"By signing below, customer / vendor verifies having received the above listed merchandise";
                    this.IndemnityStatementLine2 = @"to be credited for the return amount above.";
                    this.IndemnityStatementLine3 = string.Empty; // Oklahoma
                }

                this.CustomerPurchaseDisclaimerLine1 =
                    @"For the purchase price set below, the undersigned seller warranting good title, that transfer thereof is";
                this.CustomerPurchaseDisclaimerLine2 =
                    @"rightful and that such goods are free of any security interest, other lien, or encumbrance.";

                this.BuyLabel =
                    (this.IsReturn ? @"Return" :
                     (this.IsVendor ? @"Vendor Buy" : @"Customer Buy"));
                this.BuyReportTitle =
                    @"Merchandise " +
                    (this.IsReturn ? @"Return" :
                     (this.IsVendor ? @"Purchase (Vendor)" : @"Purchase"));
                this.DiscloseStatement =
                    @"This instrument evidences the " +
                    (this.IsReturn ? @"return" : @"sale") +
                    @" of the following described items to:";   
            }
        }

        public class BuyPdfCustomHandler : PdfPageEventHelper
        {
            public override void OnOpenDocument(PdfWriter writer, Document document)
            {
                try
                {
                    buyRptStore.PageXYTemplate = writer.DirectContent.CreateTemplate(50, 50);
                }
                catch (DocumentException)
                {
                    //TODO: Do something
                    return;
                }
                catch (IOException)
                {
                    //TODO: Do something
                    return;
                }
                catch (Exception)
                {
                    //TODO: Do something
                    return;
                }
            }

            /// <summary>
            /// Generates a blank PdfPCell object that spans multiple columns, forming a blank line of text
            /// </summary>
            /// <param name="colSpan"></param>
            /// <returns></returns>
            public static PdfPCell generateBlankLine(int colSpan)
            {
                var blankLineCell = new PdfPCell(new Phrase(" "));
                blankLineCell.Colspan = colSpan;
                blankLineCell.Border = PdfPCell.NO_BORDER;
                return (blankLineCell);
            }

            /// <summary>
            /// Generates a graphical line by utilizing the border settings of blank text cells
            /// </summary>
            /// <param name="lineColor">Color of the line</param>
            /// <param name="colSpan">Number of columns to span</param>
            /// <param name="lineHeight">Point height of the </param>
            /// <returns></returns>
            public static PdfPCell generateLine(BaseColor lineColor, int colSpan, float lineHeight)
            {
                var lineCell = new PdfPCell(new Phrase(" "));
                if (lineHeight < 0.1f)
                    lineHeight = 0.1f;
                lineCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                lineCell.Colspan = colSpan;
                lineCell.FixedHeight = 1.0f;
                lineCell.Border = PdfPCell.TOP_BORDER;
                lineCell.BorderColorTop = lineColor;
                lineCell.BorderWidthTop = lineHeight;
                return (lineCell);
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="colSpan"></param>
            /// <returns></returns>
            public static PdfPCell generateBlankCell(int colSpan)
            {
                //Default blank cell
                var blankCell = new PdfPCell(new Phrase(string.Empty));
                blankCell.Colspan = colSpan;
                blankCell.Border = PdfPCell.NO_BORDER;
                blankCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                blankCell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
                return (blankCell);
            }

            /// <summary>
            /// Sets majority of default data for cells
            /// </summary>
            /// <param name="cell">Reference to cell being modified</param>
            /// <param name="colSpan">Number of columns to span</param>
            /// <param name="align">Alignment value (0 = left, 1 = center, 2 = right)</param>
            /// <param name="isReturn"></param>
            /// <param name="isVendor"></param>
            public static void setDefault(ref PdfPCell cell, int colSpan, int align, bool isReturn, bool isVendor)
            {
                if (cell == null)
                    return;
                cell.HorizontalAlignment =
                    ((align == L) ? PdfPCell.ALIGN_LEFT :
                    ((align == C) ? PdfPCell.ALIGN_CENTER : PdfPCell.ALIGN_RIGHT));
                cell.Border = PdfPCell.NO_BORDER;
                cell.Colspan = colSpan;

                if (!isReturn && !isVendor && GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
                {
                    // Ohio is a 1/2 page report
                    cell.PaddingTop = 0;
                    cell.PaddingBottom = 1;
                }
            }

            /// <summary>
            /// Generates a text cell that is either bolded or not
            /// </summary>
            /// <param name="text">Text to display in the cell</param>
            /// <param name="bold">If true, uses the bold font, otherwise the standard font</param>
            /// <param name="isReturn"></param>
            /// <param name="isVendor"></param>
            /// <returns></returns>
            private PdfPCell genTextCell(string text, bool bold, bool isReturn, bool isVendor)
            {
                Font bFont = (bold ? buyRptStore.StandardBoldFont : buyRptStore.StandardFont);
                var newCell = new PdfPCell(new Phrase(text, bFont));

                if (!isReturn && !isVendor && GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
                {
                    // Ohio is a 1/2 page report
                    newCell.PaddingTop = 0;
                    newCell.PaddingBottom = 1;
                }

                return (newCell);
            }

            /// <summary>
            /// Generates a text cell with a specified font
            /// </summary>
            /// <param name="text">Text to display in the cell</param>
            /// <param name="f">Font to use in displaying the text</param>
            /// <param name="isReturn"></param>
            /// <param name="isVendor"></param>
            /// <returns></returns>
            public static PdfPCell genTextCell(string text, Font f, bool isReturn, bool isVendor)
            {
                var newCell = new PdfPCell(new Phrase(text, f));

                if (!isReturn && !isVendor && GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
                {
                    // Ohio is a 1/2 page report
                    newCell.PaddingTop = 0;
                    newCell.PaddingBottom = 1;
                }

                return (newCell);
            }

            /// <summary>
            /// Generates the header for the purchase tickest
            /// </summary>
            /// <param name="writer">The pdf writer writing the PDF file</param>
            /// <returns>The header table generated</returns>
            private PdfPTable generateBuyHeader(PdfWriter writer)
            {
                //Buy Header Logic
                var pTable = new PdfPTable(3);
                pTable.DefaultCell.Border = PdfPCell.NO_BORDER;
                pTable.DefaultCell.BorderColor = BaseColor.WHITE;

                int templateMatrixXOffset = 0;

                //Row #1
                if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio) &&
                    !this.buyRptStore.IsVendor &&
                    !this.buyRptStore.IsReturn)
                {
                    // OH
                    pTable.AddCell(" ");
                    templateMatrixXOffset = -330;
                }
                else
                {
                    var logoImg = Image.GetInstance(Properties.Resources.logo, BaseColor.WHITE);
                    var imgCell = new PdfPCell(logoImg, true);
                    setDefault(ref imgCell, 1, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(imgCell);
                }

                pTable.AddCell(generateBlankCell(1));
                var buyCell = new PdfPCell(new Phrase(buyRptStore.BuyLabel + " #: " + buyRptStore.BuyNumber, buyRptStore.BuyLabelFont));
                setDefault(ref buyCell, 1, R, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                pTable.AddCell(buyCell);

                //Row #2
                pTable.AddCell(generateBlankLine(3));

                //Row #3
                var titleCell = new PdfPCell(new Phrase(buyRptStore.BuyReportTitle, buyRptStore.HeaderTitleFont));
                setDefault(ref titleCell, 3, C, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                pTable.AddCell(titleCell);

                //Row #4
                pTable.AddCell(generateLine(BaseColor.DARK_GRAY, 3, 1.0f));

                //Row #5
                var discloseCell = genTextCell(buyRptStore.DiscloseStatement, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                setDefault(ref discloseCell, 3, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                pTable.AddCell(discloseCell);

                //Row #6 (Page X of Y cell) (Blank line with right aligned template)
                pTable.AddCell(generateBlankLine(2));
                var b = writer.DirectContent;
                var pageN = writer.PageNumber;
                var text = string.Format("Page {0} of ", pageN);
                var len = buyRptStore.StandardFont.BaseFont.GetWidthPoint(text, 6);
                b.BeginText();
                b.SetFontAndSize(buyRptStore.StandardFont.BaseFont, 6);
                b.SetTextMatrix(458, 665 + templateMatrixXOffset);
                b.ShowText(text);
                b.EndText();
                b.AddTemplate(buyRptStore.PageXYTemplate, 458 + len, 665 + templateMatrixXOffset);
                pTable.AddCell(generateBlankCell(1));

                //If customer data is not first, print shop data
                if (!buyRptStore.IsCustomerDataFirst)
                {
                    //Row #7
                    var shopDataCell = genTextCell(buyRptStore.ShopData, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref shopDataCell, 2, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(shopDataCell);

                    var userIdCell = genTextCell(buyRptStore.UserId, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref userIdCell, 1, R, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(userIdCell);

                    //Row #8
                    var shopLocCell = genTextCell(buyRptStore.ShopLocationData, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref shopLocCell, 2, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(shopLocCell);

                    var transDataCell = genTextCell(buyRptStore.TransactionDateTime, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref transDataCell, 1, R, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(transDataCell);

                    //Row #9
                    pTable.AddCell(generateBlankLine(3));

                    //Check if not return, print disclaimer
                    if (!buyRptStore.IsReturn && !buyRptStore.IsVendor)
                    {
                        //Row #10
                        var custDisc1Cell = genTextCell(buyRptStore.CustomerPurchaseDisclaimerLine1, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                        setDefault(ref custDisc1Cell, 3, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                        pTable.AddCell(custDisc1Cell);

                        //Row #11
                        var custDisc2Cell = genTextCell(buyRptStore.CustomerPurchaseDisclaimerLine2, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                        setDefault(ref custDisc2Cell, 3, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                        
                        pTable.AddCell(custDisc2Cell);
                    }

                    //Next Row
                    pTable.AddCell(generateBlankLine(3));

                    //Next Row - Item Header with optional secondary header
                    if (!string.IsNullOrEmpty(buyRptStore.SecondaryItemHeader))
                    {
                        if (!buyRptStore.IsVendor)
                        {
                            //Generate individual cells
                            var leadingPhraseCell = genTextCell(buyRptStore.ItemHeader + " ", true, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                            setDefault(ref leadingPhraseCell, 1, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                            leadingPhraseCell.NoWrap = true;
                            var secondaryPhraseCell = genTextCell(buyRptStore.SecondaryItemHeader + ":", false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                            setDefault(ref secondaryPhraseCell, 1, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                            secondaryPhraseCell.NoWrap = true;

                            //Concatenate cells with an inner table
                            var innerTable = new PdfPTable(2);
                            innerTable.AddCell(leadingPhraseCell);
                            innerTable.AddCell(secondaryPhraseCell);

                            //Insert inner table into single cell
                            var itemHeadCell = new PdfPCell(innerTable);
                            setDefault(ref itemHeadCell, 1, C, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                            pTable.AddCell(itemHeadCell);
                            pTable.AddCell(generateBlankCell(2));
                        }
                    }
                    else
                    {
                        var leadingPhraseCell = genTextCell(buyRptStore.ItemHeader + ":", true, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                        setDefault(ref leadingPhraseCell, 3, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                        pTable.AddCell(leadingPhraseCell);
                    }
                }
                else
                {
                    //Next Row
                    var custInfCell = genTextCell(buyRptStore.CustomerNameData, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref custInfCell, 1, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(custInfCell);
                    pTable.AddCell(generateBlankCell(1));
                    var userIdCell = genTextCell(buyRptStore.UserId, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref userIdCell, 1, R, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(userIdCell);

                    //Next Row
                    var custAddCell = genTextCell(buyRptStore.CustomerAddressData, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref custAddCell, 1, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(custAddCell);
                    pTable.AddCell(generateBlankCell(1));
                    var transDataCell = genTextCell(buyRptStore.TransactionDateTime, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref transDataCell, 1, R, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(transDataCell);

                    //Next row
                    var custLocCell = genTextCell(buyRptStore.CustomerLocationData, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref custLocCell, 3, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(custLocCell);

                    //Next Row - "from:" static text row
                    var fromCell = genTextCell("from:", false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref fromCell, 3, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(fromCell);

                    //Next Row 
                    var shopDataCell = genTextCell(buyRptStore.ShopData, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref shopDataCell, 3, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(shopDataCell);

                    //Next Row
                    var shopLocCell = genTextCell(buyRptStore.ShopLocationData, false, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    setDefault(ref shopLocCell, 3, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                    pTable.AddCell(shopLocCell);

                    //Blank row
                    pTable.AddCell(generateBlankLine(3));

                    //If is return, add items returned block
                    if (buyRptStore.IsReturn)
                    {
                        var itemRetCell = genTextCell(buyRptStore.ItemHeader + ":", true, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                        setDefault(ref itemRetCell, 3, L, this.buyRptStore.IsReturn, this.buyRptStore.IsVendor);
                        pTable.AddCell(itemRetCell);
                    }

                    //Blank row
                    pTable.AddCell(generateBlankLine(3));
                }

                return (pTable);
            }

            public override void OnStartPage(PdfWriter writer, Document document)
            {
                document.Add(generateBuyHeader(writer));
            }

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                // Footer
                //document.Add(generateBuyFooter(writer));
            }

            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                buyRptStore.PageXYTemplate.BeginText();
                buyRptStore.PageXYTemplate.SetFontAndSize(buyRptStore.StandardFont.BaseFont, 8);
                buyRptStore.PageXYTemplate.ShowText((writer.PageNumber - 1).ToString().PadLeft(3, ' '));
                buyRptStore.PageXYTemplate.EndText();
            }

            public BuyDocumentReportStorage buyRptStore { private set; get; }

            // ReSharper disable MemberHidesStaticFromOuterClass
            public const int L = 0;
            public const int C = 1;
            public const int R = 2;
            // ReSharper restore MemberHidesStaticFromOuterClass
            public BuyPdfCustomHandler(bool isReturn, bool isVendor)
            {
                this.buyRptStore = new BuyDocumentReportStorage(isReturn, isVendor);
            }
        }

        public const int L = 0;
        public const int C = 1;
        public const int R = 2;

        public static bool GeneratePurchaseDocument(
            DesktopSession dSession,
            PurchaseVO purchase,            
            out string fileNameGenerated)
        {
            fileNameGenerated = string.Empty;
            const string here = "PurchaseDocumentGenerator";
            var fLog = FileLogger.Instance;
            if (fLog.IsLogDebug)
            {
                fLog.logMessage(LogLevel.DEBUG, here, "GeneratePurchaseDocument()...");
            }
            if (purchase == null)
            {
                fLog.logMessage(LogLevel.ERROR, here, "Purchase object is null");
                return (false);
            }
            var isVendor = (dSession.ActiveVendor != null && !string.IsNullOrEmpty(dSession.ActiveVendor.Name));
            //!string.IsNullOrEmpty(purchase.EntityType) && purchase.EntityType.Equals("V", StringComparison.OrdinalIgnoreCase);
            var isReturn = purchase.LoanStatus == ProductStatus.RET;
            if (fLog.IsLogInfo)
            {
                fLog.logMessage(LogLevel.INFO,
                                here,
                                "Loan state data: LoanStatus={0}, LoanEntityType={1}, TicketNumber={2}",
                                purchase.LoanStatus.ToString("g"),
                                purchase.EntityType,
                                purchase.TicketNumber);
                fLog.logMessage(LogLevel.INFO,
                                here,
                                "Printing state data: IsVendor={0}, IsReturn={1}",
                                isVendor,
                                isReturn);
            }

            var customPdfHandler =
                new BuyPdfCustomHandler(
                    isReturn,
                    isVendor);
            //Log the pdf handler state computation results
            if (fLog.IsLogDebug)
            {
                fLog.logMessage(LogLevel.DEBUG,
                                here,
                                "Calculated Purchase Doc Title={0}",
                                customPdfHandler.buyRptStore.BuyReportTitle);
            }

            //Make sure the purchase object has items to display
            if (CollectionUtilities.isEmpty(purchase.Items))
            {
                fLog.logMessage(LogLevel.ERROR, here, "Purchase object has no items");
                return (false);
            }

            var pageSize = PageSize.LETTER;
            float marginTop = 50;
            float marginBotton = 50;
            if (GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio) &&
                !isVendor &&
                !isReturn)
            {
                // Spec change for Ohio
                pageSize = PageSize.HALFLETTER.Rotate();
                marginTop = 5;
                marginBotton = 5;
            }

            var document = new Document(pageSize, 50, 50, marginTop, marginBotton);

            //Generate file name
            fileNameGenerated =
                SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + 
                "\\purchdoc_" + DateTime.Now.Ticks + "_gen.out";

            if (fLog.IsLogDebug)
            {
                fLog.logMessage(LogLevel.DEBUG, here, "Output file name={0}", fileNameGenerated);
            }

            try
            {
                var writer = PdfWriter.GetInstance(document, new FileStream(fileNameGenerated, FileMode.Create));
                //Set event handler
                writer.PageEvent = customPdfHandler;

                //Get store data
                var activeStore = GlobalDataAccessor.Instance.CurrentSiteId;

                //Set handler data for the store
                customPdfHandler.buyRptStore.ShopData =
                    activeStore.StoreName.Trim() + SPACE + activeStore.StoreNumber.Trim();
                customPdfHandler.buyRptStore.ShopLocationData =
                    (activeStore.StoreAddress1.Trim() ?? string.Empty);

                if (activeStore.StoreAddress2.Trim().Length != 0)
                {
                    customPdfHandler.buyRptStore.ShopLocationData += SPACE + activeStore.StoreAddress2.Trim();
                }
                customPdfHandler.buyRptStore.ShopLocationData += COMMA_SPACE +
                    (activeStore.StoreCityName.Trim() ?? string.Empty) + COMMA_SPACE +
                    (activeStore.State.Trim() ?? string.Empty) + SPACE +
                    (activeStore.StoreZipCode.Trim() ?? string.Empty); 

                customPdfHandler.buyRptStore.UserId =
                    dSession.ActiveUserData.CurrentUserName;

                if (fLog.IsLogDebug)
                {
                    fLog.logMessage(LogLevel.DEBUG,
                                    here,
                                    "ShopData={0}, ShopLocationData={1}",
                                    customPdfHandler.buyRptStore.ShopData,
                                    customPdfHandler.buyRptStore.ShopLocationData);
                }

                //Tender type
                customPdfHandler.buyRptStore.TenderType = "Cash";

                //Set handler data for the customer
                if (!isVendor)
                {
                    if (fLog.IsLogDebug)
                    {
                        fLog.logMessage(LogLevel.DEBUG, here, "Using customer information");
                    }
                    var curCust = dSession.ActiveCustomer;
                    var address =
                    curCust.getHomeAddress();
                    customPdfHandler.buyRptStore.CustomerAddressData =
                        (address.Address1 ?? string.Empty) + SPACE +
                        (address.Address2 ?? string.Empty);

                    customPdfHandler.buyRptStore.CustomerLocationData =
                        (address.City ?? string.Empty) + COMMA_SPACE +
                        (address.State_Name ?? string.Empty) + SPACE +
                        (address.ZipCode);

                    var contact = 
                    curCust.getPrimaryContact();
                    if (contact == null)
                    {
                        customPdfHandler.buyRptStore.CustomerPhoneNumber = string.Empty;
                    }
                    else
                    {
                        customPdfHandler.buyRptStore.CustomerPhoneNumber =
                            "(" + (contact.ContactAreaCode ?? string.Empty) +
                            ") " + (contact.ContactPhoneNumber ?? string.Empty);
                    }

                    //Get name information
                    var lastName = curCust.LastName ?? string.Empty;
                    var firstName = curCust.FirstName ?? string.Empty;
                    var middleInitial = curCust.MiddleInitial ?? string.Empty;

                    customPdfHandler.buyRptStore.CustomerNameData =
                    lastName + COMMA_SPACE + firstName + SPACE + middleInitial;

                    //Populate vitals
                    customPdfHandler.buyRptStore.Age = curCust.Age.ToString();
                    customPdfHandler.buyRptStore.DateOfBirth = curCust.DateOfBirth.FormatDate();
                    customPdfHandler.buyRptStore.Height = curCust.Height;
                    customPdfHandler.buyRptStore.Identification = firstName + SPACE + middleInitial + SPACE + lastName;
                    // = curCust.
                    var idInfo = IdentificationVosLINQ(curCust);
                    if (!idInfo.Any())
                    {
                        customPdfHandler.buyRptStore.IdentificationInfo = curCust.getFirstIdentity();
                    }
                    else
                    {
                        foreach (var idVO in idInfo)
                        {
                            customPdfHandler.buyRptStore.IdentificationInfo = idVO;
                        }
                    }
                    customPdfHandler.buyRptStore.Race = curCust.Race ?? "Unknown";
                    customPdfHandler.buyRptStore.Sex = curCust.Gender;
                    customPdfHandler.buyRptStore.Weight = curCust.Weight.ToString();

                    //Set tax id to empty
                    customPdfHandler.buyRptStore.FederalTaxId = string.Empty;
                }
                else
                {
                    if (fLog.IsLogDebug)
                    {
                        fLog.logMessage(LogLevel.DEBUG, here, "Using vendor information");
                    }

                    var curVend = dSession.ActiveVendor;
                    customPdfHandler.buyRptStore.CustomerAddressData =
                        (curVend.Address1 ?? string.Empty) + SPACE +
                        (curVend.Address2 ?? string.Empty);

                    customPdfHandler.buyRptStore.CustomerLocationData =
                        (curVend.City ?? string.Empty) + COMMA_SPACE +
                        (curVend.State ?? string.Empty) + SPACE +
                        (curVend.ZipCode);

                    customPdfHandler.buyRptStore.CustomerPhoneNumber =
                        curVend.ContactPhone ?? string.Empty;

                    //Get name information
                    customPdfHandler.buyRptStore.CustomerNameData = curVend.Name;

                    //Get tax id
                    customPdfHandler.buyRptStore.FederalTaxId = curVend.TaxID;
                }

                if (fLog.IsLogDebug)
                {
                    fLog.logMessage(LogLevel.DEBUG,
                                    here,
                                    "{0}Name={1},{2}Address={3},{4}Location={5},{6}Phone={7},{8}TaxID={9}",
                                    Environment.NewLine,
                                    customPdfHandler.buyRptStore.CustomerNameData,
                                    Environment.NewLine,
                                    customPdfHandler.buyRptStore.CustomerAddressData,
                                    Environment.NewLine,
                                    customPdfHandler.buyRptStore.CustomerLocationData,
                                    Environment.NewLine,
                                    customPdfHandler.buyRptStore.CustomerPhoneNumber,
                                    Environment.NewLine,
                                    customPdfHandler.buyRptStore.FederalTaxId);
                }

                //Set handler data for the transaction
                customPdfHandler.buyRptStore.BuyNumber =
                    purchase.TicketNumber.ToString();
                //                customPdfHandler.buyRptStore.TransactionDateTime =
                //                        purchase.StatusDate.FormatDate() + SPACE +
                //                        purchase.StatusTime.ToString("hh:mm tt");
                var shopDateTime = ShopDateTime.Instance.FullShopDateTime;
                customPdfHandler.buyRptStore.TransactionDateTime =
                    shopDateTime.FormatDate() + " " + shopDateTime.ToString("hh:mm tt");
                    
                if (fLog.IsLogDebug)
                {
                    fLog.logMessage(LogLevel.DEBUG, here,
                                    "BuyNumber={0}, TransactionDateTime={1}",
                                    customPdfHandler.buyRptStore.BuyNumber,
                                    customPdfHandler.buyRptStore.TransactionDateTime);
                }

                if (fLog.IsLogInfo)
                {
                    fLog.logMessage(LogLevel.INFO, here, 
                                    "Starting {0} document generation", 
                                    customPdfHandler.buyRptStore.BuyReportTitle);
                }

                //Open the document
                document.Open();

                //Create item table
                const int itemTableCols = 6;
                var itemTable = new PdfPTable(itemTableCols);
                itemTable.DefaultCell.Border = PdfPCell.NO_BORDER;
                itemTable.DefaultCell.BorderColor = BaseColor.WHITE;

                // Add a blank line for separation
                itemTable.AddCell(BuyPdfCustomHandler.generateBlankLine(itemTableCols));

                //Get standard font
                Font tabFont = customPdfHandler.buyRptStore.TableFont;

                if (fLog.IsLogDebug)
                {
                    fLog.logMessage(LogLevel.DEBUG, here, "Generating {0} item lines(s)", purchase.Items.Count);
                }

                //Start adding items
                decimal totalAmt = 0.0M;
                int itemIdx = 1;
                int itemQty = 0;
                bool expenseItem = false;
                foreach (var item in purchase.Items)
                {
                    if (item == null) continue;

                    if (item.Quantity > 1)
                    {
                        Item itemCopy = Utilities.CloneObject(item);
                        string sItemPrefix;
                        string sDescription;
                        //Get updated mdse description for each serial number
                        Item.RemoveSerialNumberFromDescription(ref itemCopy, out sItemPrefix, out sDescription);
                        item.TicketDescription = sDescription;
                        itemQty = item.Quantity;
                    }
                    else
                    {
                        if (item.CaccLevel == 0 && isVendor)
                        {
                            itemQty = item.QuickInformation.Quantity;
                        }
                        else
                        {
                            itemQty = 1;
                        }
                    }
                    //Cell #1                          || Cell #2      || Cell #3       || Cell #4 || Cell #5
                    //[Item Number] [Item Description] || [Quantity] @ || [Item Amount] || <blank> || [Buy Amt]

                    //Cell #1
                    var itemNumCell = BuyPdfCustomHandler.genTextCell(itemIdx + ") " + item.TicketDescription, tabFont, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    BuyPdfCustomHandler.setDefault(ref itemNumCell, 2, BuyPdfCustomHandler.L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    itemNumCell.NoWrap = false;
                    itemTable.AddCell(itemNumCell);

                    if(item.IsExpenseItem)
                    {
                        //Cell #1
                        expenseItem = true;
                        var itemExpenseCell = BuyPdfCustomHandler.genTextCell("[E]", tabFont, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                        BuyPdfCustomHandler.setDefault(ref itemExpenseCell, 1, BuyPdfCustomHandler.C, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                        itemExpenseCell.NoWrap = false;
                        itemTable.AddCell(itemExpenseCell);
                    }
                    else
                    {
                        var itemExpenseCell = BuyPdfCustomHandler.genTextCell(string.Empty, tabFont, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                        BuyPdfCustomHandler.setDefault(ref itemExpenseCell, 1, BuyPdfCustomHandler.C, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                        itemExpenseCell.NoWrap = false;
                        itemTable.AddCell(itemExpenseCell);
                    }

                    //Cell #2
                    var itemQtyCell = BuyPdfCustomHandler.genTextCell(itemQty + " @ ", tabFont, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    BuyPdfCustomHandler.setDefault(ref itemQtyCell, 1, BuyPdfCustomHandler.R, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    itemQtyCell.NoWrap = true;
                    itemTable.AddCell(itemQtyCell);

                    //Cell #3
                    //itemTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));

                    //Cell #4
                    var itemAmtCell = BuyPdfCustomHandler.genTextCell(String.Format("{0:C}", item.ItemAmount), tabFont, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    BuyPdfCustomHandler.setDefault(ref itemAmtCell, 1, BuyPdfCustomHandler.L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    itemAmtCell.NoWrap = true;
                    itemTable.AddCell(itemAmtCell);

                    //Cell #5
                    //itemTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));

                    //Compute price paid for item (quantity * item price)

                    decimal priceOfItemRow = item.ItemAmount * itemQty;
                    if (fLog.IsLogDebug)
                    {
                        fLog.logMessage(LogLevel.DEBUG,
                                        here,
                                        "Item {0} -> {1}, {2} @ {3} = {4}",
                                        itemIdx,
                                        item.TicketDescription,
                                        String.Format("{0:C}", item.ItemAmount),
                                        item.Quantity,
                                        String.Format("{0:C}", priceOfItemRow));
                    }

                    //Cell #6
                    var itemTotAmtCell = BuyPdfCustomHandler.genTextCell(String.Format("{0:C}", priceOfItemRow),
                                                                         tabFont, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    BuyPdfCustomHandler.setDefault(ref itemTotAmtCell, 1, BuyPdfCustomHandler.R, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    itemTotAmtCell.NoWrap = true;
                    itemTable.AddCell(itemTotAmtCell);

                    //Add total to total amount
                    totalAmt += priceOfItemRow;

                    if (fLog.IsLogDebug)
                    {
                        fLog.logMessage(LogLevel.DEBUG, here, "Current Total After {0} Item(s) = {1}", itemIdx, totalAmt);
                    }

                    //Increment item index
                    ++itemIdx;
                }

                //Add a blank line to the end of the table
                itemTable.AddCell(BuyPdfCustomHandler.generateBlankLine(itemTableCols));
                //Add item detail table to document
                document.Add(itemTable);

                //PAID BY: [Tender Type]								            TOTAL:  [Total Amt]
                //Add total amount of all items row table
                var totalTable = new PdfPTable(2);
                string paidBy = "CASH";
                if (string.Equals(dSession.ActivePurchase.PurchaseTenderType, PurchaseTenderTypes.BILLTOAP.ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    paidBy = "Bill To AP";
                }

                var totTendTypeCell = BuyPdfCustomHandler.genTextCell("PAID BY: " + paidBy, tabFont, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                BuyPdfCustomHandler.setDefault(ref totTendTypeCell, 1, BuyPdfCustomHandler.L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                totTendTypeCell.NoWrap = true;
                totalTable.AddCell(totTendTypeCell);
                var totAmtCell =
                    BuyPdfCustomHandler.genTextCell("TOTAL: " + String.Format("{0:C}", totalAmt).PadLeft(6, ' '), tabFont, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                BuyPdfCustomHandler.setDefault(ref totAmtCell, 1, BuyPdfCustomHandler.R, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                totAmtCell.NoWrap = true;
                totalTable.AddCell(totAmtCell);

                //Add total table to document
                document.Add(totalTable);

                var footerTable = generateBuyFooter(customPdfHandler, expenseItem);
                document.Add(footerTable);
                //Close out document
                document.Close();
                
                if (fLog.IsLogInfo)
                {
                    fLog.logMessage(LogLevel.INFO, here, "Document generation complete");
                }

                //Verify that document was generated
                var fInf = new FileInfo(fileNameGenerated);
                if (fInf == null || !fInf.Exists || fInf.Length <= 0)
                {
                    //File was not generated
                    throw new Exception("File specified by report generator output does not exist: " + fileNameGenerated);
                }

                //Print document
                //Check if printing is enabled or if laser printer is valid
                if (SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled &&
                    dSession.LaserPrinter.IsValid)
                {
                    if (FileLogger.Instance.IsLogInfo)
                    {
                        FileLogger.Instance.logMessage(LogLevel.INFO, "PurchaseDocumentGenerator", "Printing purchase ticket on {0}",
                                                       dSession.LaserPrinter);
                    }
                    //RB: BZ-0128: Changed last number of copies to 3 from 1 per 
                    var res = PrintingUtilities.printDocument(
                        fileNameGenerated,
                        dSession.LaserPrinter.IPAddress,
                        dSession.LaserPrinter.Port,
                        3);
                    if (string.IsNullOrEmpty(res) || res.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        if (fLog.IsLogError)
                        {
                            fLog.logMessage(LogLevel.ERROR,
                                            here,
                                            "Could not print {0} document.",
                                            customPdfHandler.buyRptStore.BuyReportTitle);
                        }
                    }
                    else if (res.IndexOf("SUCCESS", StringComparison.OrdinalIgnoreCase) != -1)
                    {
                        if (fLog.IsLogInfo)
                        {
                            fLog.logMessage(LogLevel.INFO,
                                            here,
                                            "Successfully printed {0} document.",
                                            customPdfHandler.buyRptStore.BuyReportTitle);
                        }
                    }
                }
                else
                {
                    if (fLog.IsLogError)
                    {
                        fLog.logMessage(LogLevel.ERROR,
                                        here,
                                        "Could not print {0} document. Either printing is disabled or printer is invalid: {1}",
                                        customPdfHandler.buyRptStore.BuyReportTitle, dSession.LaserPrinter);                        
                    }
                }

                //Get oracle data accessor
                var dA = GlobalDataAccessor.Instance.OracleDA;
                if (dA == null || dA.Initialized == false)
                {
                    throw new Exception(string.Format("Could not store document {0}, invalid data accessor", customPdfHandler.buyRptStore.BuyReportTitle));
                }

                //Regardless of printer success, attempt to store document
                var couchConn = GlobalDataAccessor.Instance.CouchDBConnector;
                if (couchConn == null)
                {
                    throw new Exception("Could not store document " + customPdfHandler.buyRptStore.BuyReportTitle +
                                        ", invalid couch db connection");
                }

                //Create pawn doc info
                var pwnDocInfo = new CouchDbUtils.PawnDocInfo();
                pwnDocInfo.DocFileName = fileNameGenerated;
                pwnDocInfo.SetDocumentSearchType(CouchDbUtils.DocSearchType.STORE_TICKET);
                var metaDictionary = new Dictionary<string, string>(3);
                metaDictionary.Add(ISVENDOR_METATAG, isVendor.ToString());
                metaDictionary.Add(ISPURCHASE_METATAG, Boolean.TrueString);
                metaDictionary.Add(ISRETURN_METATAG, isReturn.ToString());
                pwnDocInfo.UseCurrentShopDateTime = true;
                pwnDocInfo.StoreNumber = activeStore.StoreNumber;
                pwnDocInfo.TicketNumber = purchase.TicketNumber;
                pwnDocInfo.CustomerNumber = ((!isVendor) ? dSession.ActiveCustomer.CustomerNumber : dSession.ActiveVendor.ID);

                //Call add pawn document
                string errTxt;
                if (!CouchDbUtils.AddPawnDocument(dA, couchConn, dSession.UserName, metaDictionary, PURCHASE_AUXINFOTAG, ref pwnDocInfo, out errTxt))
                {
                    //throw new Exception("Could not store document " + customPdfHandler.buyRptStore.BuyReportTitle +
                    //                    ", add pawn document failed. " + errTxt ?? string.Empty);

                    if (FileLogger.Instance.IsLogError)
                    {
                        FileLogger.Instance.logMessage(LogLevel.ERROR,
                                                        "Could not store document " + customPdfHandler.buyRptStore.BuyReportTitle +
                                                        ", add pawn document failed. ", errTxt);
                    }
                }

                return (true);
            }
            catch (Exception eX)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR,
                                                   "PurchaseDocumentGenerator",
                                                   "Exception caught while storing purchase document: {0}",
                                                   eX.Message);
                }

                BasicExceptionHandler.Instance.AddException(
                    "Could not store " +
                    customPdfHandler.buyRptStore.BuyReportTitle +
                    " form in document storage",
                    new ApplicationException(
                        "Could not store " + customPdfHandler.buyRptStore.BuyReportTitle + 
                        " form in document storage: " +
                        eX.Message));
            }

            return (false);
        }

        private static IEnumerable<IdentificationVO> IdentificationVosLINQ(CustomerVO curCust)
        {
            var idInfo = curCust.CustomerIDs.Where(p => p.IsLatest);
            return idInfo;
        }

        /// <summary>
        /// Generates the footer of the Purchase Reports
        /// </summary>
        /// <returns>A PDFTable object to display on the generated PDF</returns>
        private static PdfPTable generateBuyFooter(BuyPdfCustomHandler customPdfHandler, bool expenseitem)
        {
            float lineHeight = 12;

            if (!customPdfHandler.buyRptStore.IsReturn && 
                !customPdfHandler.buyRptStore.IsVendor && 
                GlobalDataAccessor.Instance.CurrentSiteId.State.Equals(States.Ohio))
            {
                lineHeight = 10;
            }

            //Buy Footer Logic
            var pTable = new PdfPTable(3);
            pTable.DefaultCell.Border = Rectangle.NO_BORDER;
            pTable.DefaultCell.BorderColor = BaseColor.WHITE;

            // Add a blank line for separation
            pTable.AddCell(BuyPdfCustomHandler.generateBlankLine(3));

            // If we are not dealing with a vendor
            if (!customPdfHandler.buyRptStore.IsVendor)
            {
                var indemnityCell1 =
                    new PdfPCell(new Phrase(customPdfHandler.buyRptStore.IndemnityStatementLine1, customPdfHandler.buyRptStore.StandardFont));
                BuyPdfCustomHandler.setDefault(ref indemnityCell1, 3, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                pTable.AddCell(indemnityCell1);

                var indemnityCell2 =
                    new PdfPCell(new Phrase(customPdfHandler.buyRptStore.IndemnityStatementLine2, customPdfHandler.buyRptStore.StandardFont));
                BuyPdfCustomHandler.setDefault(ref indemnityCell2, 3, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                pTable.AddCell(indemnityCell2);

                if (!string.IsNullOrWhiteSpace(customPdfHandler.buyRptStore.IndemnityStatementLine3)) // Oklahoma
                {
                    var indemnityCell3 =
                        new PdfPCell(new Phrase(customPdfHandler.buyRptStore.IndemnityStatementLine3, customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref indemnityCell3, 3, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);   
                    pTable.AddCell(indemnityCell3);
                }
            }

            pTable.AddCell(BuyPdfCustomHandler.generateBlankLine(3));

            // If purchase, we need to have the four customer related fields at the bottom right
            if (!customPdfHandler.buyRptStore.IsReturn)
            {
                // If customer purchase, we need signature line immediately on left side
                if (!customPdfHandler.buyRptStore.IsVendor)
                {
                    var signatureCell =
                        new PdfPCell(new Phrase(@"X_________________________", customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref signatureCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    pTable.AddCell(signatureCell);
                }
                else
                {
                    pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));
                }

                // Add a blank cell in the middle regardless of condition
                pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));

                // Create first row of customer information
                var custInfoCell = new PdfPCell(new Phrase(customPdfHandler.buyRptStore.CustomerNameData, customPdfHandler.buyRptStore.StandardFont));
                BuyPdfCustomHandler.setDefault(ref custInfoCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                pTable.AddCell(custInfoCell);

                var innerIDTable = new PdfPTable(2);
                // If customer purchase, we need Identification line immediately on left side
                if (!customPdfHandler.buyRptStore.IsVendor)
                {
                    /*var identificationCell =
                    new PdfPCell(new Phrase(@"ID", buyRptStore.StandardFont));
                    setDefault(ref identificationCell, 1, L);
                    pTable.AddCell(identificationCell);*/
                    if (customPdfHandler.buyRptStore.IdentificationInfo != null)
                    {
                        if (customPdfHandler.buyRptStore.IdentificationInfo.IdType == "DRIVERLIC")
                        {
                            var idTextCell =
                                new PdfPCell(new Phrase(@"DL", customPdfHandler.buyRptStore.StandardFont));
                            BuyPdfCustomHandler.setDefault(ref idTextCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                            idTextCell.FixedHeight = lineHeight;
                            idTextCell.NoWrap = true;
                            idTextCell.BorderWidth = 0.0f;
                            innerIDTable.AddCell(idTextCell);
                        }
                        else
                        {
                            var idTextCell =
                                new PdfPCell(new Phrase(@"ID", customPdfHandler.buyRptStore.StandardFont));
                            BuyPdfCustomHandler.setDefault(ref idTextCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                            idTextCell.FixedHeight = lineHeight;
                            idTextCell.NoWrap = true;
                            idTextCell.BorderWidth = 0.0f;
                            innerIDTable.AddCell(idTextCell);
                            //if(buyRptStore.IdentificationInfo.
                        }
                        var idCell =
                            new PdfPCell(new Phrase(customPdfHandler.buyRptStore.IdentificationInfo.IdValue, customPdfHandler.buyRptStore.StandardFont));

                        BuyPdfCustomHandler.setDefault(ref idCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                        idCell.FixedHeight = lineHeight;
                        idCell.NoWrap = true;
                        innerIDTable.AddCell(idCell);
                        pTable.AddCell(innerIDTable);
                    }
                }
                else
                {
                    if (expenseitem)
                    {
                        var expenseCell =
                            new PdfPCell(new Phrase("[E]: Expense Item", customPdfHandler.buyRptStore.StandardFont));
                        BuyPdfCustomHandler.setDefault(ref expenseCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                        pTable.AddCell(expenseCell);

                    }
                    else
                    {
                        pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));
                    }
                    
                }

                // Add a blank cell in the middle regardless of condition
                pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));

                // Create the second row of customer information

                var custAddressCell =
                    new PdfPCell(new Phrase(customPdfHandler.buyRptStore.CustomerAddressData, customPdfHandler.buyRptStore.StandardFont));
                BuyPdfCustomHandler.setDefault(ref custAddressCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                pTable.AddCell(custAddressCell);

                // If customer purchase, we need DOB and Height line immediately on left side
                // If vendor purchase, we need Emp. line on left side
                if (!customPdfHandler.buyRptStore.IsVendor)
                {
                    var dOBTextCell =
                        new PdfPCell(new Phrase(@"DOB", customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref dOBTextCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    dOBTextCell.FixedHeight = lineHeight;
                    dOBTextCell.NoWrap = true;
                    dOBTextCell.BorderWidth = 0.0f;

                    var dOBCell =
                        new PdfPCell(new Phrase(customPdfHandler.buyRptStore.DateOfBirth, customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref dOBCell, 1, R, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    dOBCell.FixedHeight = lineHeight;
                    dOBCell.NoWrap = true;

                    var heightTextCell =
                        new PdfPCell(new Phrase(@"Ht.", customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref heightTextCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    heightTextCell.FixedHeight = lineHeight;
                    heightTextCell.NoWrap = true;

                    var heightCell =
                        new PdfPCell(new Phrase(customPdfHandler.buyRptStore.Height, customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref heightCell, 1, R, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    heightCell.FixedHeight = lineHeight;
                    heightCell.NoWrap = true;

                    // Make four column table to hold the data for DOB and Height
                    var innerTable = new PdfPTable(5);
                    innerTable.AddCell(dOBTextCell);
                    innerTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));
                    innerTable.AddCell(dOBCell);
                    innerTable.AddCell(heightTextCell);
                    innerTable.AddCell(heightCell);

                    pTable.AddCell(innerTable);
                }
                else
                {

                    var empSignatureCell =
                        new PdfPCell(new Phrase(@"Emp.______________________", customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref empSignatureCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    pTable.AddCell(empSignatureCell);
                }

                // Add a blank cell in the middle regardless of condition
                pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));

                // Create the third row of customer information
                var custLocationCell =
                new PdfPCell(new Phrase(customPdfHandler.buyRptStore.CustomerLocationData, customPdfHandler.buyRptStore.StandardFont));
                BuyPdfCustomHandler.setDefault(ref custLocationCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                pTable.AddCell(custLocationCell);

                // If customer purchase, we need Age and Sex line immediately on left side
                if (!customPdfHandler.buyRptStore.IsVendor)
                {
                    var ageTextCell =
                    new PdfPCell(new Phrase(@"Age", customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref ageTextCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    ageTextCell.FixedHeight = lineHeight;
                    ageTextCell.NoWrap = true;

                    var ageCell =
                    new PdfPCell(new Phrase(customPdfHandler.buyRptStore.Age, customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref ageCell, 1, R, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    ageCell.FixedHeight = lineHeight;
                    ageCell.NoWrap = true;

                    var sexTextCell =
                    new PdfPCell(new Phrase(@"Sex", customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref sexTextCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    sexTextCell.FixedHeight = lineHeight;
                    sexTextCell.NoWrap = true;

                    var sexCell =
                    new PdfPCell(new Phrase(customPdfHandler.buyRptStore.Sex, customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref sexCell, 1, R, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    sexCell.FixedHeight = lineHeight;
                    sexCell.NoWrap = true;

                    // Make four column table to hold the data for DOB and Height
                    var secondInnerTable = new PdfPTable(5);
                    secondInnerTable.AddCell(ageTextCell);
                    secondInnerTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));
                    secondInnerTable.AddCell(ageCell);
                    secondInnerTable.AddCell(sexTextCell);
                    secondInnerTable.AddCell(sexCell);

                    pTable.AddCell(secondInnerTable);
                }
                else
                {
                    pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));
                }

                // Add a blank cell in the middle regardless of condition
                pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));

                // Create the fourth row of customer information
                var custPhoneCell =
                new PdfPCell(new Phrase(@"Phone: " + customPdfHandler.buyRptStore.CustomerPhoneNumber,
                                        customPdfHandler.buyRptStore.StandardFont));
                BuyPdfCustomHandler.setDefault(ref custPhoneCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                pTable.AddCell(custPhoneCell);

                // If customer purchase, we need Weight and Race line immediately on left side
                if (!customPdfHandler.buyRptStore.IsVendor)
                {
                    var weightTextCell =
                    new PdfPCell(new Phrase(@"Wt.", customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref weightTextCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    weightTextCell.NoWrap = true;

                    var weightCell =
                    new PdfPCell(new Phrase(customPdfHandler.buyRptStore.Weight, customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref weightCell, 1, R, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    weightCell.NoWrap = true;

                    var raceTextCell =
                    new PdfPCell(new Phrase(@"Race", customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref raceTextCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    raceTextCell.NoWrap = true;

                    var raceCell =
                    new PdfPCell(new Phrase(customPdfHandler.buyRptStore.Race, customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref raceCell, 1, R, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    raceCell.NoWrap = true;

                    // Make four column table to hold the data for DOB and Height
                    var thirdInnerTable = new PdfPTable(5);
                    thirdInnerTable.AddCell(weightTextCell);
                    thirdInnerTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));
                    thirdInnerTable.AddCell(weightCell);
                    thirdInnerTable.AddCell(raceTextCell);
                    thirdInnerTable.AddCell(raceCell);

                    pTable.AddCell(thirdInnerTable);
                    pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));

                    // Need to put in Aux Id field
                    var custAuxIdCell =
                    new PdfPCell(new Phrase(@"Aux. ID:_________________", customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref custAuxIdCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    pTable.AddCell(custAuxIdCell);
                }
                else
                {
                    pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(2));

                    // Need to put in Federal Tax Id field
                    var federalTaxIdCell =
                    new PdfPCell(new Phrase(@"Fed. Tax ID: " + customPdfHandler.buyRptStore.FederalTaxId,
                                            customPdfHandler.buyRptStore.StandardFont));
                    BuyPdfCustomHandler.setDefault(ref federalTaxIdCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                    pTable.AddCell(federalTaxIdCell);
                }
            }
            else
            {
                pTable.AddCell(BuyPdfCustomHandler.generateBlankLine(3));
                pTable.AddCell(BuyPdfCustomHandler.generateBlankLine(3));

                var signatureCell = new PdfPCell(new Phrase(@"X_________________________", customPdfHandler.buyRptStore.StandardFont));
                BuyPdfCustomHandler.setDefault(ref signatureCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                pTable.AddCell(signatureCell);

                pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));

                var empSignatureCell =
                new PdfPCell(new Phrase(@"X_________________________", customPdfHandler.buyRptStore.StandardFont));
                BuyPdfCustomHandler.setDefault(ref empSignatureCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                pTable.AddCell(empSignatureCell);

                var signatureTextCell =
                new PdfPCell(new Phrase(@"Customer / Vendor Signature", customPdfHandler.buyRptStore.StandardFont));
                BuyPdfCustomHandler.setDefault(ref signatureTextCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                pTable.AddCell(signatureTextCell);

                pTable.AddCell(BuyPdfCustomHandler.generateBlankCell(1));

                var empSignatureTextCell =
                new PdfPCell(new Phrase(@"Employee Signature", customPdfHandler.buyRptStore.StandardFont));
                BuyPdfCustomHandler.setDefault(ref empSignatureTextCell, 1, L, customPdfHandler.buyRptStore.IsReturn, customPdfHandler.buyRptStore.IsVendor);
                pTable.AddCell(empSignatureTextCell);
            }

            return (pTable);
        }
    }
}

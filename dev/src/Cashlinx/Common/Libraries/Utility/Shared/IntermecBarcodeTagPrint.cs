using System;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;

namespace Common.Libraries.Utility.Shared
{
    public class IntermecBarcodeTagPrint
    {
        public DesktopSession DesktopSession { get; private set; }

        private const int LINE_LENGTH = 23;
        private const int LINE_LENGTH3 = 80;

        private string _CompanyName;
        private string _GunWremoteNremote;
        private string _IplPrintData;
        private string _LineBarFormat_1;
        private string _LineBarFormat_2;
        private string _LineBarFormat_3;
        private string _LineBarFormat_4;
        private string _LineBarFormat_7;
        private string _LineBarFormat_8;
        private string _LineBarFormat_9;
        private string _LineBarFormat_10;
        private string _LineBarEnd;
        private string _LineBlank;
        private string _LineEnd;
        private string _LineFormat;
        private string _LineStars;
        private string _LineStart;
        private string _MdseGunNo;
        private Item _PawnItem;
        private string printerIPAddress;
        private uint printerPort;
        private string _RetailPrice;
        private bool _PrintRetailPriceTNR;
        private int _SiteID;
        private StateStatus _StateStatus;

        private int _transferNumber = 0;

        //private ScrapItem _scrapItem;

        public enum PrinterModel
        {
            Intermec_2800,
            Intermec_3400,
            Intermec_PD41,
            Intermec_PM4i
        }

        public enum TagMedias
        {
            BAR_BELL,
            BARCODE,
            COMBO,
            BAG_TAG
        }

        // Derived from barcode.fs:  FORM barcode | BEFORE FORM
        public IntermecBarcodeTagPrint(
            string sCompanyName,
            int i_SiteID,
            PrinterModel pmPrinterModel,
            string ipAddress, uint port, DesktopSession desktopSession)
        {
            DesktopSession = desktopSession;
            _CompanyName = sCompanyName;
            _SiteID = i_SiteID;
            this.printerIPAddress = ipAddress;
            this.printerPort = port;

            if (pmPrinterModel == PrinterModel.Intermec_PD41
                || pmPrinterModel == PrinterModel.Intermec_PM4i)
            {
                _LineStart = "<STX>";
                _LineEnd = "<CR><ETX>";
                _LineBarFormat_1 = "<STX><ESC>E1<CAN><ETX>";
                _LineBarFormat_2 = "<STX><ESC>E2<CAN><ETX>";
                _LineBarFormat_3 = "<STX><ESC>E3<CAN><ETX>";
                _LineBarFormat_4 = "<STX><ESC>E4<CAN><ETX>";
                _LineBarFormat_7 = "<STX><ESC>E7<CAN><ETX>";  // Dynamic 1
                _LineBarFormat_8 = "<STX><ESC>E8<CAN><ETX>";  // Dynamic 2
                _LineBarFormat_9 = "<STX><ESC>E9<CAN><ETX>";  // Dynamic 3
                _LineBarFormat_10 = "<STX><ESC>E10<CAN><ETX>"; // Dynamic 4
                //_LineBarFormat_EmployeeSecurityBarcode = "<STX><ESC>E99<CAN><ETX>"; // Security Barcode Tag
                _LineBarEnd = "<STX><ETB><ETX>";
            }
            else
            {
                _LineStart = "D,";
                _LineEnd = ";";
                _LineBarFormat_1 = char.ConvertFromUtf32(24) + ";" + char.ConvertFromUtf32(27) + "E1";
                _LineBarFormat_2 = char.ConvertFromUtf32(24) + ";" + char.ConvertFromUtf32(27) + "E2";
                _LineBarEnd = char.ConvertFromUtf32(31) + char.ConvertFromUtf32(49) + ";" + char.ConvertFromUtf32(23) + ";";
            }

            _LineBlank = _LineStart + _LineEnd;
            _LineStars = _LineStart + (new string('*', 10)) + _LineEnd;
        }

        public void PrintUserBarCode(string sEncryptedString, string sUserFirstName)//, string sUserLastName)
        {
            if (string.IsNullOrEmpty(sUserFirstName))
            {
                sUserFirstName = " ";
            }

            if (string.IsNullOrEmpty(sEncryptedString))
            {
                return;
            }
            _IplPrintData = "<STX><ESC>C<ETX>";
            _IplPrintData += "<STX><ESC>P<ETX>";
            _IplPrintData += "<STX>E98;F98;<ETX>";
            _IplPrintData += "<STX>H0,employeeName;o150,1;f3;r0;c20;h2;w1;b0;d0,32;<ETX>";
            _IplPrintData += "<STX>B1,employeeBar;o90,0;f3;c6,0;h50;w1;r1;i0;d0,32;<ETX>";
            _IplPrintData += "<STX>R<ETX>";
            _IplPrintData += "<STX><ESC>E98<CAN><ETX>";
            _IplPrintData += "<STX>" + sUserFirstName + "<CR><ETX>";
            _IplPrintData += "<STX>" + sEncryptedString + "<CR><ETX>";
            _IplPrintData += "<STX><ETB><ETX>";
            _IplPrintData += "<STX><ESC>C<ETX>";
            _IplPrintData += "<STX><ESC>P<ETX>";
            _IplPrintData += "<STX>E98;<ETX>";
            _IplPrintData += "<STX>R<ETX>";
            #region Temp Centering Code
            // In case we want to center align, requires sending temporary format prior to printing
            //_IplPrintData = "<STX><ESC>C<ETX>";
            //_IplPrintData += "<STX><ESC>P<ETX>";
            //_IplPrintData += "<STX>E98;F98;<ETX>";
            //_IplPrintData += "<STX>H0,employeeName;o150," + CenterAlignString(sUserFirstName + " " + sUserLastName) + ";f3;r0;c20;h2;w1;b0;d0,30;<ETX>";
            //_IplPrintData += "<STX>B1,employeeBar;o90," + CenterAlignString(sEncryptedString) + ";f3;c6,0;h50;w2.5;r1;i0;d0,18;<ETX>";
            //_IplPrintData += "<STX>R<ETX>";
            //_IplPrintData += "<STX><ESC>E98<CAN><ETX>";
            //_IplPrintData += "<STX>" + sUserFirstName + " " + sUserLastName + "<CR><ETX>";
            //_IplPrintData += "<STX>" + sEncryptedString + "<CR><ETX>";
            //_IplPrintData += "<STX><ETB><ETX>";
            //_IplPrintData += "<STX><ESC>C<ETX>";
            //_IplPrintData += "<STX><ESC>P<ETX>";
            //_IplPrintData += "<STX>E98;<ETX>";
            //_IplPrintData += "<STX>R<ETX>";
            #endregion

            PrintTag(_IplPrintData);
        }

        public void PrintBagTag(DateTime dtPfiDate,
                                Item pawnItem,
                                int transferNumber,
                                bool bNoName,
                                bool bPrintCaccTags,
                                bool bPrintCostCode,
                                bool bPrintPrice,
                                bool bPrintTnrRetailPrice,
                                TagMedias tag_media)
        {
            string sError = "";
            string sMessage = "";

            //if (pawnItem is ScrapItem)
            //    _scrapItem = pawnItem as ScrapItem;

            _transferNumber = transferNumber;

            PrintTag(dtPfiDate,
                     pawnItem,
                     bNoName,
                     bPrintCaccTags,
                     bPrintCostCode,
                     bPrintPrice,
                     bPrintTnrRetailPrice,
                     tag_media,
                     CurrentContext.READ_ONLY,
                     out sError,
                     out sMessage);

            _transferNumber = 0; //reset class-level variable value
        }
        
        //Added Method For printing Bag Tag With out item
        // This method will be called by manager transfer
        public void PrintBagTagForTransfer(
            string fromStore,
            int transferNumber,
            decimal transAmount,
            int noOfItems,
            string _catcoType,
            string toName)
        {
            //if (pawnItem is ScrapItem)
            //    _scrapItem = pawnItem as ScrapItem;
            _transferNumber = transferNumber;
            string transAmountStr = transAmount.ToString("C");
            string noOfItemsStr = noOfItems.ToString();
            PrintBagTag(fromStore, transAmountStr, noOfItemsStr, _catcoType, toName);
            _transferNumber = 0; //reset class-level variable value
        }

        public void ReprintTag(Item item)
        {
            RePrintTag(item);
        }

        public void PrintTag(DateTime dtPfiDate,
                     Item pawnItem,
                     bool bNoName,
                     bool bPrintCaccTags,
                     bool bPrintCostCode,
                     bool bPrintPrice,
                     bool bPrintTnrRetailPrice,
                     TagMedias tag_media,
                    CurrentContext context,
                     out string sError,
                     out string sMessage)
        {
            bool print_this_icn = true;
            bool is_set = false;
            bool parse_set = true;
            string set_text = "";

            try
            {
                sError = "0";
                sMessage = string.Empty;
                if(pawnItem.PfiDate != DateTime.MinValue)
                    dtPfiDate = pawnItem.PfiDate;
                _PawnItem = pawnItem;
                _PrintRetailPriceTNR = bPrintTnrRetailPrice;
                _StateStatus = _PawnItem.TempStatus;
                //if (_StateStatus == StateStatus.BLNK)
                    //bPrintPrice = false;
                //              G_PARAM3( 'CACC_TRACK', CF:$CACC_TRACK, FALSE );
                bool bCaccTrack = false;

                // Call to get printer info.  A couple of queries
                _LineFormat = string.Empty;

                //          G_PARAM3( 'NEW_REFURB', CF:$new_refurb, 'N' );
                bool bNewRefurb = _PawnItem.PfiAssignmentType == PfiAssignment.Refurb || Utilities.GetIntegerValue(_PawnItem.RefurbNumber, 0) > 0;

                string sMdseTicketDesc = Utilities.GetStringValue(_PawnItem.TicketDescription, "").PadRight(80, ' ');

                if (_StateStatus == StateStatus.PFIE || _StateStatus == StateStatus.PFI)
                {
                    parse_set = false;
                    if (Utilities.GetIntegerValue(_PawnItem.RefurbNumber, 0) > 0 ||
                        _PawnItem.PfiAssignmentType == PfiAssignment.Refurb ||
                        _PawnItem.ItemStatus == ProductStatus.PS ||
                        _PawnItem.ItemStatus == ProductStatus.OFF ||
                        (_PawnItem.ItemReason == ItemReason.COFFBRKN ||
                         _PawnItem.ItemReason == ItemReason.CACC ||
                         _PawnItem.ItemReason == ItemReason.COFFNXT ||
                         _PawnItem.ItemReason == ItemReason.COFFSTLN ||
                         _PawnItem.ItemReason == ItemReason.COFFSTRU) ||
                        (
                        _PawnItem.CaccLevel == 0 && !bPrintCaccTags
                        )
                    )
                    {
                        print_this_icn = false;
                    }
                    else
                    {
                        if ((Utilities.GetIntegerValue(_PawnItem.QuickInformation.Quantity, 0) > 0) &&
                            (_PawnItem.CaccLevel < 0))
                        {
                            is_set = true;
                            set_text = "SET OF " + _PawnItem.QuickInformation.Quantity;
                        }
                    }
                }
                if (print_this_icn)
                {
                    
                    string sDateCode = string.Empty;
                    int iPfiAmount = 0;
                    if(context == CurrentContext.VENDOR_PURCHASE)
                        iPfiAmount = Convert.ToInt32(Utilities.GetDecimalValue(_PawnItem.ItemAmount, 0) * 100);
                    else
                        iPfiAmount = Convert.ToInt32(Utilities.GetDecimalValue(_PawnItem.PfiAmount, 0) * 100);
                    string sMaryLouiseCode = CONV_M_CODE(Utilities.GetStringValue(iPfiAmount, "").PadLeft(7, ' '));
                    if (_PawnItem.CaccLevel != 0)
                    {
                        if (_PawnItem.mDocType == "7"
                            || _PawnItem.ItemReason == ItemReason.CACC)
                        {
                            if (bCaccTrack)
                            {
                                // SET CF:$sMaryLouiseCode TO getenv$('CTAGDOC')
                                sMaryLouiseCode = sMaryLouiseCode ?? " ";
                            }
                            else
                            {
                                sMaryLouiseCode = " ";
                                sDateCode = "CACC";
                            }
                        }
                        else if (!bPrintCostCode)
                        {
                            sMaryLouiseCode = " ";
                        }
                    }
                    if (parse_set)
                    {
                        int desc_len = sMdseTicketDesc.Length;
                        int l_desc_ctr = (desc_len - 12) < 1 ? 1 : desc_len - 12;

                        for (int desc_ctr = 0; desc_ctr < l_desc_ctr; desc_ctr++)
                        {
                            if (sMdseTicketDesc.Substring(desc_ctr, 6).ToLower() == "SET OF")
                            {
                                is_set = true;
                                set_text = sMdseTicketDesc.Substring(desc_ctr);
                                set_text = set_text.Replace(";", " ");
                                break;
                            }
                        }
                    }
                    if (Utilities.GetDecimalValue(_PawnItem.RetailPrice, 0) == 0 || !bPrintPrice)
                        _RetailPrice = string.Empty;
                    else
                        _RetailPrice = System.String.Format("{0:0.00}", Utilities.GetDecimalValue(_PawnItem.RetailPrice, 0));

                    if (!string.IsNullOrEmpty(_RetailPrice) 
                        || (context == CurrentContext.PFI_REPLACE 
                        ||context == CurrentContext.PFI_MERGE
                        || context == CurrentContext.PFI_REDESCRIBE
                        || context == CurrentContext.VENDOR_PURCHASE))
                    {
                        sDateCode = dtPfiDate.Month.ToString().PadLeft(2, '0')
                        + dtPfiDate.Year.ToString().Substring(dtPfiDate.Year.ToString().Length - 1);
                    }
                    //if (string.IsNullOrEmpty(_RetailPrice))
                        //_RetailPrice = String.Format("{0:0.00}", Utilities.GetDecimalValue(_PawnItem.ItemAmount, 0));
                    sMdseTicketDesc = sMdseTicketDesc.Substring(0, LINE_LENGTH3);
                    sMdseTicketDesc = sMdseTicketDesc.Replace(";", " ");
                    _GunWremoteNremote = "N";

                    _MdseGunNo = _PawnItem.GunNumber.ToString();

                    if (Utilities.GetLongValue(_PawnItem.GunNumber, 0) != 0)
                    {
                        _GunWremoteNremote = "G";
                    }
                    else
                    {
                        if (_PawnItem.Attributes != null
                            && _PawnItem.Attributes.FindIndex(m => m.Answer.AnswerCode == 121) >= 0)
                        {
                            _MdseGunNo = "W/Remote";
                            _GunWremoteNremote = "W";
                        }
                        else
                        {
                            if (is_set)
                            {
                                _MdseGunNo = set_text;
                                _GunWremoteNremote = "W";
                            }
                            else if (Utilities.IsGun(_PawnItem.GunNumber, _PawnItem.CategoryCode, _PawnItem.IsJewelry, _PawnItem.MerchandiseType)
                                     && Utilities.GetStringValue(_PawnItem.QuickInformation.SerialNumber, string.Empty) != string.Empty)
                            {
                                if (_PawnItem.GunNumber > 0)
                                    _MdseGunNo = "G# " + _PawnItem.GunNumber;
                                else
                                    _MdseGunNo = string.Empty;
                                _GunWremoteNremote = "W";
                            }
                        }
                    }

                    if (_StateStatus == StateStatus.BLNK && _PawnItem.ItemStatus != ProductStatus.PFI)
                    {
                        if(context != CurrentContext.PFI_REPLACE 
                            && context != CurrentContext.PFI_MERGE 
                            && context != CurrentContext.PFI_REDESCRIBE 
                            && context != CurrentContext.VENDOR_PURCHASE
                            && context != CurrentContext.PFI_ADD)
                            sDateCode = string.Empty;
                    }
                    string sTagMedia = tag_media.ToString();
                   
                    if (sTagMedia == "BAR_BELL")
                        PrintBarBell(sMdseTicketDesc, sDateCode, sMaryLouiseCode, bNoName);
                    else if (sTagMedia == "COMBO")
                        PrintCombo(sMdseTicketDesc, sDateCode, sMaryLouiseCode, bPrintCaccTags, bNewRefurb);
                    else if (sTagMedia == "BAG_TAG")
                        PrintBagTag(_PawnItem);
                    else
                        PrintBarCode(sMdseTicketDesc, sDateCode, sMaryLouiseCode, bPrintCaccTags);
                    //}
                }//end if (print_this_icn)
            }
            catch (System.Exception exp)
            {
                sError = "ERROR";
                sMessage = "CALL SUPPORT NOW." + " barcode-1 PRINTER RETRIEVE FAILED - STATUS = " + exp.Message;
            }
        }

     
        private void RePrintTag(Item item)
        {
            //<STX>B1,employeeBar;o90,40;f3;c6,0;h50;w2.5;r1;i0;d0,18;<ETX>
            string catcode = string.Empty;
            string itemDescription = string.Empty;
            string lineDesc0 = string.Empty;
            string lineDesc1 = string.Empty;
            string lineDesc2 = string.Empty;
            string lineDesc3 = string.Empty;
            string lineDesc4 = string.Empty;
            string lineDesc5 = string.Empty;
            string lineDesc6 = string.Empty;
            string lineDesc7 = string.Empty;
            string lineDesc8 = string.Empty;
            string lineDesc9 = string.Empty;
            bool catcodeAdded = false;
            try
            {
                if (!string.IsNullOrEmpty(item.TicketDescription))
                {
                    int descriptionLength = item.TicketDescription.Length;
                    if (descriptionLength > 200)
                        itemDescription = item.TicketDescription.Substring(0, 200);
                    else
                        itemDescription = item.TicketDescription;
                    string[] descriptData = itemDescription.Split(new char[] { ';' });
                    for (int i = 0; i <= descriptData.Length - 1; i++)
                    {
                        if (i == 0)
                        {
                            lineDesc0 = descriptData[i].Trim();
                            if (lineDesc0.Length > 12)
                                lineDesc0 = lineDesc0.Substring(0, 12);
                        }
                        if (i == 1)
                        {
                            lineDesc1 = descriptData[i].Trim();
                            if (lineDesc1.Length > 12)
                                lineDesc1 = lineDesc1.Substring(0, 12);
                        }
                        if (i == 2)
                        {
                            lineDesc2 = descriptData[i].Trim();
                            if (lineDesc2.Length > 12)
                                lineDesc2 = lineDesc2.Substring(0, 12);
                        }
                        if (i == 3)
                        {
                            lineDesc3 = descriptData[i].Trim();
                            if (lineDesc3.Length > 12)
                                lineDesc3 = lineDesc3.Substring(0, 12);
                        }
                        if (i == 4)
                        {
                            lineDesc4 = descriptData[i].Trim();
                            if (lineDesc4.Length > 12)
                                lineDesc4 = lineDesc4.Substring(0, 12);
                        }
                        if (i == 5)
                        {
                            lineDesc5 = descriptData[i].Trim();
                            if (lineDesc5.Length > 12)
                                lineDesc5 = lineDesc5.Substring(0, 12);
                        }
                        if (i == 6)
                        {
                            lineDesc6 = descriptData[i].Trim();
                            if (lineDesc6.Length > 12)
                                lineDesc6 = lineDesc6.Substring(0, 12);
                        }
                        if (i == 7)
                        {
                            lineDesc7 = descriptData[i].Trim();
                            if (lineDesc7.Length > 12)
                                lineDesc7 = lineDesc7.Substring(0, 12);
                        }
                        if (i == 8)
                        {
                            lineDesc8 = descriptData[i].Trim();
                            if (lineDesc8.Length > 12)
                                lineDesc8 = lineDesc8.Substring(0, 12);
                        }
                        if (i == 9)
                        {
                            lineDesc9 = descriptData[i].Trim();
                            if (lineDesc9.Length > 12)
                                lineDesc9 = lineDesc9.Substring(0, 12);
                        }
                    }
                   
                }
                if (!string.IsNullOrEmpty(item.CategoryCode.ToString()))
                {
                    catcode = item.CategoryCode.ToString();
                }
                _IplPrintData = "<STX>R<ETX>";
                _IplPrintData += "<STX><ESC>C<ETX>";
                _IplPrintData += "<STX><ESC>P<ETX>";
                _IplPrintData += "<STX>E6;F6;<ETX>";
                _IplPrintData += "<STX>H0,F1;o1,40;f0;r0;c0;h2;w2;b0;d0,24;<ETX>";
                if (!string.IsNullOrEmpty(lineDesc0))
                {
                    _IplPrintData += "<STX>H1,F1;o1,70;f0;r0;c0;h2;w2;b0;d0,24;<ETX>";
                }
                if (!string.IsNullOrEmpty(lineDesc1))
                {
                    _IplPrintData += "<STX>H2,F1;o1,100;f0;r0;c0;h2;w2;b0;d0,22;<ETX>";//description Line 1
                }
                else
                {
                    if (!string.IsNullOrEmpty(catcode))
                    {
                        _IplPrintData += "<STX>L7;o1,125;f0;l180;w2;h5<ETX>";//line
                        _IplPrintData += "<STX>H8,F1;o1,135;f0;r0;c0;h2;w2;b0;d0,20;<ETX>";//Catcode
                        catcodeAdded = true;
                    }
                }
                if (!string.IsNullOrEmpty(lineDesc2))
                {
                    _IplPrintData += "<STX>H3,F1;o1,130;f0;r0;c0;h2;w2;b0;d0,20;<ETX>";//description Line 2
                }
                else
                {
                    if (!string.IsNullOrEmpty(catcode) && !catcodeAdded)
                    {
                        _IplPrintData += "<STX>L7;o1,125;f0;l180;w2;h5<ETX>";//line
                        _IplPrintData += "<STX>H8,F1;o1,135;f0;r0;c0;h2;w2;b0;d0,20;<ETX>";//Catcode
                        catcodeAdded = true;
                    }
                }
                if (!string.IsNullOrEmpty(lineDesc3))
                {
                    _IplPrintData += "<STX>H4,F1;o1,160;f0;r0;c0;h2;w2;b0;d0,20;<ETX>";//description Line 3
                }
                else
                {
                    if (!string.IsNullOrEmpty(catcode) && !catcodeAdded)
                    {
                        _IplPrintData += "<STX>L7;o1,155;f0;l180;w2;h5<ETX>";//line
                        _IplPrintData += "<STX>H8,F1;o1,165;f0;r0;c0;h2;w2;b0;d0,20;<ETX>";//Catcode
                        catcodeAdded = true;
                    }
                }
                if (!string.IsNullOrEmpty(lineDesc4))
                {
                    _IplPrintData += "<STX>H5,F1;o1,190;f0;r0;c0;h2;w2;b0;d0,20;<ETX>";//description Line 4
                }
                else
                {
                    if (!string.IsNullOrEmpty(catcode) && !catcodeAdded)
                    {
                        _IplPrintData += "<STX>L7;o1,185;f0;l180;w2;h5<ETX>";//line
                        _IplPrintData += "<STX>H8,F1;o1,195;f0;r0;c0;h2;w2;b0;d0,20;<ETX>";//Catcode
                        catcodeAdded = true;
                    }
                }
                if (!string.IsNullOrEmpty(lineDesc5))
                {
                    _IplPrintData += "<STX>H6,F1;o1,220;f0;r0;c0;h2;w2;b0;d0,20;<ETX>";//description Line 5
                }
                else
                {
                    if (!string.IsNullOrEmpty(catcode) && !catcodeAdded)
                    {
                        _IplPrintData += "<STX>L7;o1,215;f0;l180;w2;h5<ETX>";//line
                        _IplPrintData += "<STX>H8,F1;o1,225;f0;r0;c0;h2;w2;b0;d0,20;<ETX>";//Catcode
                        catcodeAdded = true;
                    }
                }

                /*if (!string.IsNullOrEmpty(catcode))
                {
                    _IplPrintData += "<STX>L7;o1,250;f0;l180;w2;h5<ETX>";//line
                    _IplPrintData += "<STX>H8,F1;o1,270;f0;r0;c0;h2;w2;b0;d0,20;<ETX>";//Catcode
                }*/
                _IplPrintData += "<STX>TEST STRING<CR><ETX>";
                _IplPrintData += "<STX>R<ETX>";
                _IplPrintData += "<STX><ESC>E6<CAN><ETX>";

                // _IplPrintData += "<STX>* Features *<CR><ETX>";
                //  _IplPrintData += "<STX>" + item.TicketDescription.Substring(0, 10) +  "<CR><ETX>";
                //  _IplPrintData += "<STX>__________________<CR><ETX>";
                _IplPrintData += "<STX>* Features *<CR><ETX>";
                if (!string.IsNullOrEmpty(lineDesc0))
                    _IplPrintData += "<STX>" + lineDesc0 + "<CR><ETX>"; //description Line 1
                if (!string.IsNullOrEmpty(lineDesc1))
                    _IplPrintData += "<STX>" + lineDesc1 + "<CR><ETX>"; //description Line 2
                if (!string.IsNullOrEmpty(lineDesc2))
                    _IplPrintData += "<STX>" + lineDesc2 + "<CR><ETX>"; //description Line 3
                if (!string.IsNullOrEmpty(lineDesc3))
                    _IplPrintData += "<STX>" + lineDesc3 + "<CR><ETX>"; //description Line 4
                if (!string.IsNullOrEmpty(lineDesc4))
                    _IplPrintData += "<STX>" + lineDesc4 + "<CR><ETX>"; //description Line 5
                if (!string.IsNullOrEmpty(lineDesc5))
                    _IplPrintData += "<STX>" + lineDesc5 + "<CR><ETX>"; //description Line 6
                if (!string.IsNullOrEmpty(catcode))
                    _IplPrintData += "<STX>" + catcode + "<CR><ETX>"; //Catcode

                //_IplPrintData += "<STX>__________<CR><ETX>";
                _IplPrintData += "<STX><ETB><ETX>";
                _IplPrintData += "<STX><ESC>C<ETX>";
                _IplPrintData += "<STX><ESC>P<ETX>";
                _IplPrintData += "<STX>E6;<ETX>";
                _IplPrintData += "<STX>R<ETX>";

                PrintTag(_IplPrintData);
                //RePrintTag(item);
            }
            catch (System.Exception ex)
            {
                BasicExceptionHandler.Instance.AddException("IntermecBarcodeTagPrint Exception when reprinting", ex);
            }
        }

        private void PrintBagTag(Item item)
        {
            //<STX>B1,employeeBar;o90,40;f3;c6,0;h50;w2.5;r1;i0;d0,18;<ETX>
            _IplPrintData = "<STX>R<ETX>";
            _IplPrintData += "<STX><ESC>C<ETX>";
            _IplPrintData += "<STX><ESC>P<ETX>";
            _IplPrintData += "<STX>E6;F6;<ETX>";
            _IplPrintData += "<STX>H0,F1;o207,15;f3;r0;c0;h2;w2;b0;d0,24;<ETX>";
            _IplPrintData += "<STX>H1,F2;o177,15;f3;r0;c0;h2;w2;b0;d0,24;<ETX>";
            _IplPrintData += "<STX>H2,F3;o147,15;f3;r0;c0;h2;w2;b0;d0,24;<ETX>";
            _IplPrintData += "<STX>H3,F4;o117,15;f3;r0;c0;h2;w2;b0;d0,24;<ETX>";
            _IplPrintData += "<STX>H4,F5;o87,15;f3;r0;c0;h2;w2;b0;d0,24;<ETX>";
            _IplPrintData += "<STX>B6,icn;o50,30;f3;c6,0;h30;w2;i0;d1,24;<ETX>";
            _IplPrintData += "<STX>H7,F5;o10,35,15;f3;r0;c0;h1;w1;b0;d0,24;<ETX>";
            _IplPrintData += "<STX>TEST STRING<CR><ETX>";
            _IplPrintData += "<STX>R<ETX>";
            _IplPrintData += "<STX><ESC>E6<CAN><ETX>";

            if (_transferNumber > 0)
                _IplPrintData += "<STX>" + "Transf#: \t" + _transferNumber + "<CR><ETX>";
            
            _IplPrintData += "<STX>" + "From:" + item.mStore + "<CR><ETX>";
            _IplPrintData += "<STX>" + "To:" + DesktopSession.CurrentSiteId.StoreNumber + "<CR><ETX>";
            _IplPrintData += "<STX>" + "Qty:" + item.Quantity + "<CR><ETX>";
            _IplPrintData += "<STX>" + "Amount: " + item.ItemAmount.ToString("C") +"<CR><ETX>";
            _IplPrintData += "<STX>" + item.Icn + "<CR><ETX>";
            _IplPrintData += "<STX>" + item.Icn + "<CR><ETX>";
            _IplPrintData += "<STX><ETB><ETX>";
            _IplPrintData += "<STX><ESC>C<ETX>";
            _IplPrintData += "<STX><ESC>P<ETX>";
            _IplPrintData += "<STX>E6;<ETX>";
            _IplPrintData += "<STX>R<ETX>";

            PrintTag(_IplPrintData);
        }

      
        private void PrintBagTag(string fromStore, string amount, string quantity, string catcoType, string toName)
        {
            //<STX>B1,employeeBar;o90,40;f3;c6,0;h50;w2.5;r1;i0;d0,18;<ETX>
            _IplPrintData = "<STX>R<ETX>";
            _IplPrintData += "<STX><ESC>C<ETX>";
            _IplPrintData += "<STX><ESC>P<ETX>";
            _IplPrintData += "<STX>E20;F20;<ETX>";
            _IplPrintData += "<STX>H0;o190,125;f3;r0;c0;h2;w2;b0;d0,24;<ETX>";//trans no
            _IplPrintData += "<STX>H1;o160,125;f3;r0;c0;h2;w2;b0;d0,24;<ETX>";//from
            _IplPrintData += "<STX>H2;o130,125;f3;r0;c0;h2;w2;b0;d0,24;<ETX>";//To
            _IplPrintData += "<STX>H3;o100,125;f3;r0;c0;h2;w2;b0;d0,24;<ETX>";//Qty
            _IplPrintData += "<STX>H4;o70,125;f3;r0;c0;h2;w2;b0;d0,24;<ETX>";//amt
            _IplPrintData += "<STX>B5;o40,125;f3;c6,0;h20;w2;i1;d0,24;<ETX>";//tranno barcode
            if (System.String.IsNullOrEmpty(catcoType) == false)
            {
                _IplPrintData += "<STX>H6;o25,50;f0;r0;c0;h4;w2;b0;d0,24;<ETX>";//catco type
                _IplPrintData += "<STX>L7;o20,95;f0;1500;w2;h5<ETX>";//line
            }
            _IplPrintData += "<STX>R;<ETX>";
            _IplPrintData += "<STX><ESC>E20<CAN><ETX>";
            _IplPrintData += "<STX>" + "Transf#: \t" + _transferNumber + "<CR><ETX>";
            _IplPrintData += "<STX>" + "From:" + fromStore + "<CR><ETX>";
            _IplPrintData += "<STX>" + "To:" + toName + "<CR><ETX>";
            _IplPrintData += "<STX>" + "Qty:" + quantity + "<CR><ETX>";
            _IplPrintData += "<STX>" + "Amount: " + amount + "<CR><ETX>";
            _IplPrintData += "<STX>" + _transferNumber + "<CR><ETX>";
            if (System.String.IsNullOrEmpty(catcoType) == false)
            {
                _IplPrintData += "<STX>" + catcoType + "<CR><ETX>";
            }
            _IplPrintData += "<STX><ETB><ETX>";
            _IplPrintData += "<STX><ESC>C<ETX>";
            _IplPrintData += "<STX><ESC>P<ETX>";
            _IplPrintData += "<STX>E20;<ETX>";
            _IplPrintData += "<STX>R<ETX>";

            PrintTag(_IplPrintData);
        }

        private void PrintBarBell(string sMdseTicketDesc, string sDateCode, string sMaryLouiseCode, bool bNoName)
        {
            int iNumberOfTags = Utilities.GetIntegerValue(_PawnItem.PfiTags, 1);
            string lineDesc1 = sMdseTicketDesc.Substring(0, 16);
            string lineDesc2 = sMdseTicketDesc.Substring(16, 16);
            string lineDesc3 = sMdseTicketDesc.Substring(32, 16);
            string lineDesc4 = sMdseTicketDesc.Substring(48, 16);
            string lineDesc5 = sMdseTicketDesc.Substring(64, 16);

            string icn = CONV_ICN(_PawnItem.mStore,
                                  _PawnItem.mYear,
                                  _PawnItem.mDocNumber,
                                  _PawnItem.mItemOrder,
                                  0,
                                  _PawnItem.mDocType);

            if (bNoName)
                _LineFormat = _LineBarFormat_2;
            else
                _LineFormat = _LineBarFormat_1;

            _IplPrintData = "";
            _IplPrintData += _LineFormat;
            _IplPrintData += _LineStart + sMaryLouiseCode.PadLeft(8, ' ') + _LineEnd;
            _IplPrintData += _LineStart + icn.PadLeft(18, ' ') + _LineEnd;
            _IplPrintData += _LineStart + lineDesc1 + _LineEnd;
            _IplPrintData += _LineStart + lineDesc2 + _LineEnd;
            _IplPrintData += _LineStart + lineDesc3 + _LineEnd;
            _IplPrintData += _LineStart + lineDesc4 + _LineEnd;
            _IplPrintData += _LineStart + lineDesc5 + _LineEnd;
            _IplPrintData += _LineStart + sDateCode.PadLeft(3, ' ') + _LineEnd;
            _IplPrintData += _LineStart + _RetailPrice.PadLeft(8, ' ') + _LineEnd;
            _IplPrintData += _LineStart
                             + Utilities.GetStringValue(_PawnItem.mDocNumber, "").PadLeft(6, ' ')
                             + "."
                             + _PawnItem.mItemOrder
                             + _LineEnd;
            _IplPrintData += _LineBarEnd;

            for (int i = 0; i < iNumberOfTags; i++)
            {
                PrintTag(_IplPrintData);
            }
        }

        private void PrintBarCode(string sMdseTicketDesc, string sDateCode, string sMaryLouiseCode, bool bPrintCaccTags)
        {
            int iNumberOfTags = Utilities.GetIntegerValue(_PawnItem.PfiTags, 1);
            int iTagCountLength = iNumberOfTags.ToString().Length;

            string l_icn = CONV_ICN(_PawnItem.mStore,
                                    _PawnItem.mYear,
                                    _PawnItem.mDocNumber,
                                    _PawnItem.mItemOrder,
                                    0,
                                    _PawnItem.mDocType);

            l_icn = _LineStart + l_icn + _LineEnd;

            string lineDateCode = _LineStart + sDateCode + _LineEnd;
            string lineDesc1 = _LineStart + sMdseTicketDesc.Substring(0, 23) + _LineEnd;
            string lineDesc2 = _LineStart + sMdseTicketDesc.Substring(23, 23) + _LineEnd;
            string lineDocNum = _LineStart + _PawnItem.mDocNumber + "." + _PawnItem.mItemOrder + _LineEnd;
            string lineGunNo = _MdseGunNo;
            string lineMaryCode1 = _LineStart + sMaryLouiseCode + _LineEnd;
            string lineMaryCode2 = _LineStart + sMaryLouiseCode + "{0}" + _LineEnd;
            string lineRetailPrice = _LineStart + _RetailPrice + _LineEnd;

            if (bPrintCaccTags && _PawnItem.CaccLevel > 0 && _PawnItem.mDocType != "7")
            {
                iNumberOfTags = 0;
                if (_StateStatus == StateStatus.PFIE)
                {
                    //IF CF:PREV_FORM = 'pfiedit' AND MA:$CACC_DOC_SUB <> UNDEFINED THEN 
                    //    SET CF:$cacc_doc_string TO MA:$CACC_DOC_SUB 
                }
                else if (_StateStatus == StateStatus.RO)
                {
                    //ELSE IF CF:PREV_FORM = 'sbosnew' AND sbosnew:$CACC_DOC_SUB <> UNDEFINED THEN 
                    //   SET CF:$cacc_doc_string TO sbosnew:$CACC_DOC_SUB 
                }
                else
                {
                    //ELSE 
                    //  SET CF:$cacc_doc_string TO ' ' 
                }
                //SET CF:$CACC_CMD TO '/usr/tops/bin/CACC_TAGS.sh ' + 
                //    val_to_str$(CF:$md_catg) + ' ' + val_to_str$(CF:$md_quantity) + 
                //    ' ' + CF:$cacc_doc_string + ' > /dev/null 2>&1' 
                //system_call(CF:$CACC_CMD) 
            }

            for (int i = 0; i < iNumberOfTags; i++)
            {
                _IplPrintData = "";

                int l_len2 = (i + 1).ToString().Length;
                string x_of_x = "{0} of " + iNumberOfTags;
                if (i == 0)
                {
                    x_of_x = System.String.Format(x_of_x, i + 1);
                    x_of_x = x_of_x.PadLeft(LINE_LENGTH - 7, ' ');
                    lineMaryCode2 = System.String.Format(lineMaryCode2, x_of_x);
                    _LineFormat = _LineBarFormat_1;
                }
                else
                    _LineFormat = _LineBarFormat_2;

                if (_GunWremoteNremote == "N")
                {
                    lineGunNo = (i + 1) + " of " + iNumberOfTags;
                    lineGunNo = lineGunNo.PadLeft(LINE_LENGTH, ' ');
                }
                else if (_GunWremoteNremote == "G")
                {
                    int iLength = LINE_LENGTH - iTagCountLength - l_len2 - 7;
                    lineGunNo = "G# " + _MdseGunNo.Substring(0, iLength);
                    lineGunNo = lineGunNo.PadRight(iLength, ' ');
                    lineGunNo = lineGunNo + (i + 1) + " of " + iNumberOfTags;
                }
                else if (_GunWremoteNremote == "W")
                {
                    int iLength = LINE_LENGTH - iTagCountLength - l_len2 - 4;
                    lineGunNo = _MdseGunNo.Substring(0, iLength);
                    lineGunNo = lineGunNo.PadRight(iLength, ' ');
                    lineGunNo = lineGunNo + (i + 1) + " of " + iNumberOfTags;
                }

                lineGunNo = _LineStart + lineGunNo + _LineEnd;

                _IplPrintData += _LineFormat;
                _IplPrintData += lineDesc1;
                _IplPrintData += lineDesc2;
                _IplPrintData += lineGunNo;

                if (i == 0)
                    _IplPrintData += lineRetailPrice;
                else
                    _IplPrintData += _LineBlank;

                _IplPrintData += l_icn;
                _IplPrintData += lineDateCode;
                _IplPrintData += lineDocNum;

                if (i == 0)
                    _IplPrintData += lineMaryCode1;

                _IplPrintData += l_icn;

                if (i == 0)
                    _IplPrintData += lineMaryCode1;

                _IplPrintData += lineDateCode;
                _IplPrintData += lineDesc1;
                _IplPrintData += lineDesc2;

                if (i == 0)
                    _IplPrintData += lineMaryCode2;
                else
                    _IplPrintData += lineGunNo;

                _IplPrintData += lineDateCode;

                if (i == 0)
                    _IplPrintData += lineRetailPrice;
                else
                    _IplPrintData += _LineBlank;

                _IplPrintData += lineDocNum;
                _IplPrintData += l_icn;
                _IplPrintData += _LineBarEnd;

                PrintTag(_IplPrintData);
            }
        }

        private void PrintCombo(string sMdseTicketDesc, string sDateCode, string sMaryLouiseCode, bool bPrintCaccTags, bool bNewRefurb)
        {
            if (string.IsNullOrEmpty(sDateCode))
                sDateCode = "***";
            int iNumberOfTags = Utilities.GetIntegerValue(_PawnItem.PfiTags, 1);
            int iTagCountLength = iNumberOfTags.ToString().Length;

            string l_icn = CONV_ICN(_PawnItem.mStore,
                                    _PawnItem.mYear,
                                    _PawnItem.mDocNumber,
                                    _PawnItem.mItemOrder,
                                    0,
                                    _PawnItem.mDocType);

            l_icn = _LineStart + l_icn + _LineEnd;

            string todaysnewretail = "";
            string bdesc1 = _LineStart + sMdseTicketDesc.Substring(0, 16) + _LineEnd;
            string bdesc2 = _LineStart + sMdseTicketDesc.Substring(16, 16) + _LineEnd;
            string bdesc3 = _LineStart + sMdseTicketDesc.Substring(32, 16) + _LineEnd;
            string bdesc4 = _LineStart + sMdseTicketDesc.Substring(48, 16) + _LineEnd;
            //            string bdesc5           = _LineStart + sMdseTicketDesc.Substring(64, 16) + _LineEnd;  Not Used in Barcode.fs

            string lineDateCode = _LineStart + sDateCode + _LineEnd;
            string lineDesc1 = _LineStart + sMdseTicketDesc.Substring(0, 23) + _LineEnd;
            string lineDesc2 = _LineStart + sMdseTicketDesc.Substring(23, 23) + _LineEnd;
            string lineDocNum = _LineStart + _PawnItem.mDocNumber + "." + _PawnItem.mItemOrder + _LineEnd;
            string lineGunNo = _MdseGunNo;
            string lineRetailPrice = _LineStart + _RetailPrice + _LineEnd;
            string linePadRetailPrice = _RetailPrice.PadLeft(8, ' ');
            linePadRetailPrice = _LineStart + linePadRetailPrice + _LineEnd;
            string lineMaryCode1 = _LineStart + sMaryLouiseCode.PadLeft(7, ' ') + _LineEnd;
            string lineMaryCode2 = _LineStart + sMaryLouiseCode + "{0}" + _LineEnd;

            if (_PrintRetailPriceTNR)
            {
                if (_PawnItem.SelectedProKnowMatch.proCallData.NewRetail > 0)
                    todaysnewretail = _LineStart 
                                      + "TNR " 
                                      + System.String.Format("{0:C}", _PawnItem.SelectedProKnowMatch.proCallData.NewRetail) 
                                      + _LineEnd;
            }

            if (bPrintCaccTags && _PawnItem.CaccLevel == 0 && _PawnItem.mDocType != "7")
            {
                iNumberOfTags = 0;
                if (_StateStatus == StateStatus.PFIE)
                {
                    //IF CF:PREV_FORM = 'pfiedit' AND MA:$CACC_DOC_SUB <> UNDEFINED THEN \
                    //    SET CF:$cacc_doc_string TO MA:$CACC_DOC_SUB \
                }
                else if (_StateStatus == StateStatus.RO)
                {
                    //ELSE IF CF:PREV_FORM = 'sbosnew' AND sbosnew:$CACC_DOC_SUB <> UNDEFINED THEN \
                    //    SET CF:$cacc_doc_string TO sbosnew:$CACC_DOC_SUB \
                }
                else
                {
                    //ELSE \
                    //    SET CF:$cacc_doc_string TO ' ' \
                }
                //SET CF:$CACC_CMD TO '/usr/tops/bin/CACC_TAGS.sh ' + \
                //    val_to_str$(CF:$md_catg) + ' ' + val_to_str$(CF:$md_quantity) + \
                //    ' ' + CF:$cacc_doc_string + ' > /dev/null 2>&1' \
                //system_call(CF:$CACC_CMD)
            }

            for (int i = 0; i < iNumberOfTags; i++)
            {
                _IplPrintData = "";

                int l_len2 = (i + 1).ToString().Length;
                string x_of_x = "{0} of " + iNumberOfTags;

                x_of_x = System.String.Format(x_of_x, i + 1);
                x_of_x = x_of_x.PadLeft(LINE_LENGTH - 7, ' ');
                lineMaryCode2 = System.String.Format(lineMaryCode2, x_of_x);
                x_of_x = _LineStart + System.String.Format("{0} of " + iNumberOfTags, i + 1) + _LineEnd;

                if (_GunWremoteNremote == "N")
                {
                    lineGunNo = (i + 1) + " of " + iNumberOfTags;
                    lineGunNo = lineGunNo.PadLeft(LINE_LENGTH, ' ');
                }
                else if (_GunWremoteNremote == "G")
                {
                    int iLength = LINE_LENGTH - iTagCountLength - l_len2 - 7;
                    lineGunNo = "G# " + _MdseGunNo.Substring(0, _MdseGunNo.Length > iLength ? iLength : _MdseGunNo.Length);
                    lineGunNo = lineGunNo.PadRight(iLength, ' ');
                    lineGunNo = lineGunNo + (i + 1) + " of " + iNumberOfTags;
                }
                else if (_GunWremoteNremote == "W")
                {
                    int iLength = LINE_LENGTH - iTagCountLength - l_len2 - 4;
                    lineGunNo = _MdseGunNo.Substring(0, _MdseGunNo.Length > iLength ? iLength : _MdseGunNo.Length);
                    lineGunNo = lineGunNo.PadRight(iLength, ' ');
                    lineGunNo = lineGunNo + (i + 1) + " of " + iNumberOfTags;
                }

                lineGunNo = _LineStart + lineGunNo + _LineEnd;

                if (i == 0)
                {
                    _LineFormat = _LineBarFormat_1;
                    if (_CompanyName != "")
                        _LineFormat = _LineBarFormat_7;
                    /*if (_PawnItem.IsJewelry)
                        _LineFormat = _LineBarFormat_3;
                    if (_PawnItem.IsJewelry && _CompanyName != "")
                        _LineFormat = _LineBarFormat_9;*/
                }
                else
                {
                    _LineFormat = _LineBarFormat_2;
                    if (_CompanyName != "")
                        _LineFormat = _LineBarFormat_8;
                    /*if (_PawnItem.IsJewelry)
                        _LineFormat = _LineBarFormat_4;
                    if (_PawnItem.IsJewelry && _CompanyName != "")
                        _LineFormat = _LineBarFormat_10;*/
                }

                _IplPrintData += _LineFormat;

                if (!_PawnItem.IsJewelry && (_LineFormat == _LineBarFormat_1
                    || _LineFormat == _LineBarFormat_2
                    || _LineFormat == _LineBarFormat_7
                    || _LineFormat == _LineBarFormat_8)
                )
                {
                    _IplPrintData += _LineStars;
                    _IplPrintData += _LineStars;
                    _IplPrintData += _LineStars;
                    _IplPrintData += _LineStars;
                    _IplPrintData += _LineStars;
                    _IplPrintData += _LineStars;
                    _IplPrintData += _LineBlank;
                    _IplPrintData += _LineStars;
                    _IplPrintData += _LineStars;
                    _IplPrintData += _LineStars;
                }
                else
                {
                    if (i == 0)
                        _IplPrintData += lineMaryCode1;
                    else
                        _IplPrintData += _LineBlank;
                    _IplPrintData += l_icn;
                    _IplPrintData += bdesc1;
                    _IplPrintData += bdesc2;
                    _IplPrintData += bdesc3;
                    _IplPrintData += bdesc4;
                    _IplPrintData += x_of_x;
                    _IplPrintData += lineDateCode;

                    if (i == 0)
                        _IplPrintData += linePadRetailPrice;
                    else
                        _IplPrintData += _LineBlank;
                    _IplPrintData += lineDocNum;
                }

                _IplPrintData += lineDesc1;
                _IplPrintData += lineDesc2;
                _IplPrintData += lineGunNo;

                if (i == 0)
                    _IplPrintData += lineRetailPrice;
                else
                    _IplPrintData += _LineBlank;

                _IplPrintData += l_icn;
                _IplPrintData += lineDateCode;
                _IplPrintData += lineDocNum;

                if (i == 0)
                    _IplPrintData += lineMaryCode1;
                else
                    _IplPrintData += _LineBlank;

                if (_LineFormat == _LineBarFormat_3
                    || _LineFormat == _LineBarFormat_4
                    || _LineFormat == _LineBarFormat_9
                    || _LineFormat == _LineBarFormat_10
                )
                {
                    if (i == 0)
                        _IplPrintData += lineMaryCode1;
                    else
                        _IplPrintData += _LineBlank;

                    _IplPrintData += l_icn;
                    _IplPrintData += bdesc1;
                    _IplPrintData += bdesc2;
                    _IplPrintData += bdesc3;
                    _IplPrintData += bdesc4;
                    _IplPrintData += x_of_x;

                    string sTmp_SiteID = _LineStart + _SiteID.ToString().PadLeft(5, ' ') + _LineEnd;

                    if (bNewRefurb)
                        _IplPrintData += sTmp_SiteID;
                    else
                        _IplPrintData += lineDateCode;

                    if (i == 0)
                        _IplPrintData += lineRetailPrice;
                    else
                        _IplPrintData += _LineBlank;

                    _IplPrintData += lineDocNum;

                    if (_LineFormat == _LineBarFormat_9
                        || _LineFormat == _LineBarFormat_10
                    )
                        _IplPrintData += _LineStart + _CompanyName + _LineEnd;
                }
                else
                {
                    _IplPrintData += l_icn;

                    if (i == 0)
                        _IplPrintData += lineMaryCode1;
                    else
                        _IplPrintData += _LineBlank;

                    _IplPrintData += lineDateCode;

                    if (_LineFormat == _LineBarFormat_7
                        || _LineFormat == _LineBarFormat_8
                    )
                        _IplPrintData += _LineStart + _CompanyName + _LineEnd;

                    if (_LineFormat == _LineBarFormat_1
                        || _LineFormat == _LineBarFormat_7
                    )
                        _IplPrintData += todaysnewretail;
                }

                _IplPrintData += _LineBarEnd;

                PrintTag(_IplPrintData);
            }
        }

        private void PrintTag(string sRawTagData)
        {
            FileLogger.Instance.logMessage(LogLevel.INFO, this, "Printing thermal tag...");
            string msg;
            var res = PrintingUtilities.SendASCIIStringToPrinter(
                this.printerIPAddress,
                this.printerPort,
                sRawTagData, out msg);
            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG,
                                               this,
                                               "- Result of thermal tag print = {0}",
                                               (res ? "success" : "failure"));
                FileLogger.Instance.logMessage(LogLevel.DEBUG,
                                               this,
                                               "- Thermal tag print msg       = {0}",
                                               msg);
            }
            FileLogger.Instance.logMessage(LogLevel.INFO,
                                           "... {0} thermal tag",
                                           (res ? "Successfully printed" : "Failed to print"));
        }

        /// <summary>
        /// Method to create the Mary Louise Code from
        /// </summary>
        /// <returns></returns>
        private static string CONV_M_CODE(string sPfiAmount)
        {
            string sMaryLouiseCode = "";

            try
            {
                for (int i = 0; i < 7; i++)
                {
                    switch (sPfiAmount.Substring(i, 1))
                    {
                        case " ":
                            sMaryLouiseCode += " ";
                            break;
                        case "1":
                            sMaryLouiseCode += "M";
                            break;
                        case "2":
                            sMaryLouiseCode += "A";
                            break;
                        case "3":
                            sMaryLouiseCode += "R";
                            break;
                        case "4":
                            sMaryLouiseCode += "Y";
                            break;
                        case "5":
                            sMaryLouiseCode += "L";
                            break;
                        case "6":
                            sMaryLouiseCode += "O";
                            break;
                        case "7":
                            sMaryLouiseCode += "U";
                            break;
                        case "8":
                            sMaryLouiseCode += "I";
                            break;
                        case "9":
                            sMaryLouiseCode += "S";
                            break;
                        case "0":
                            sMaryLouiseCode += "E";
                            break;
                    }
                }
            }
            catch
            {
                sMaryLouiseCode = "  Error";
            }

            return sMaryLouiseCode;
        }

        private static string CONV_ICN(int iIcnStore,
                                       int iIcnYear,
                                       int iIcnDoc,
                                       int iIcnItem,
                                       int iIcnSubItem,
                                       string sIcnDocType)
        {
            var sICN = string.Empty;
            string sTempDocType = sIcnDocType;

            if (sIcnDocType == "L")
                sTempDocType = "1";
            else if (sIcnDocType == "S")
                sTempDocType = "2";

            sICN = iIcnStore.ToString().PadLeft(5, '0');
            sICN += iIcnYear;
            sICN += iIcnDoc.ToString().PadLeft(6, '0');
            sICN += sTempDocType;
            sICN += iIcnItem.ToString().PadLeft(3, '0');
            sICN += iIcnSubItem.ToString().PadLeft(2, '0');

            return sICN;
        }
    }
}

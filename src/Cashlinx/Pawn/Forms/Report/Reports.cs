/************************************************************************
* Namespace:       CashlinxDesktop.DesktopForms.Report
* Class:           Reports
* 
* Description      Form to gather report parameters the present pdf 
*                  report results in pdf reader
*                  MainMenu->Reports
* 
* History
*  S Murphy 1/20/2010, Initial Development
*  no ticket 3/4/2010 S. Murphy add Gun Disposition report
*  no ticket 3/8/2010 S. Murphy error handling for Gun Disposition report
*  no ticket 3/11/2010 SMurphy added visible validation checks
*  PWNU00000582 SMurphy 3/31/2010 Added "Multiple" to drop down value for Gun Disposition report
*  PWNU00000582 4/1/2010 SMurphy added start date info to Gun Disposition report  
*  no ticket SMurphy 4/14/2010 restructured to match other reports
*  no ticket SMurphy 4/21/2010 Changes to remove iText from ReportObject per Sree 
*          and moved common hard-coded strings to ReportConstants
*  bz0095  Tracy McConnell 1/18/2011
*      Added code to allow display and update of sort & detail for loan_audit_report
*      at the same time as the display of the reporting date range.
*  BZ0029 RBrickler 2/7/2011 Added code to form initialization to force the first drop
*      down box to be initialized to Select.
 * 12/29/11 Ed Waltmon
 *      CR#INTG100014929/ BZ#1394/ CR# INTG100014281/ BZ#1406
 *      Firearms reports restructuring
 * 1/16/12 Ed Waltmon
 *      CR 14647 - MAL Removed
 * 2/3/12 Ed Waltmon
 *      CR 1449 - "Multiple Gun Disposition Rifles" Report removed from all states but TX.
 *      
***********************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Security;
using Common.Libraries.Objects.Authorization;
using Common.Libraries.Objects.Config;
using Common.Libraries.Utility;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Common.Libraries.Utility.String;
using Pawn.Forms.Inquiry.Retail;
using Pawn.Forms.Pawn.ShopAdministration;
using Pawn.Forms.UserControls;
using Pawn.Forms.UserControls.Reports;
using Pawn.Logic;
using Pawn.Logic.DesktopProcedures;
using Pawn.Logic.PrintQueue;
using Reports;
using Reports.MAL;

namespace Pawn.Forms.Report
{
    public partial class Reports : Form
    {
        public NavBox NavControlBox;
        private Form _ownerfrm;
        private ReportObject _report = new ReportObject();
        private List<System.Drawing.Point> _controlDefaultLocations = new List<System.Drawing.Point>();
        private DailySalesCriteria_panel criteriaPanel;
        private const string ASTERISK = "*";
        private const string STARTDATELABEL = "Start Date:";

        public Reports()
        {
            InitializeComponent();
            NavControlBox = new NavBox();
        }

        //set control visibility
        private void Reports_Load(object sender, EventArgs e)
        {
            _ownerfrm = Owner;
            NavControlBox.Owner = this;

            //RB BZ0029:  Added the following line to initialize first drop down box to Select on form load
            this.comboboxReportType.Text = ReportHeaders.SELECT;
            this.comboboxReportName.Enabled = false;

            this.labelReportStartDate.Visible = false;
            _controlDefaultLocations.Add(this.labelReportStartDate.Location);
            this.labelAsterisk3.Visible = false;
            _controlDefaultLocations.Add(this.labelAsterisk3.Location);
            this.dateCalendarStart.Visible = false;
            _controlDefaultLocations.Add(this.dateCalendarStart.Location);

            this.labelReportEndDate.Visible = false;
            _controlDefaultLocations.Add(this.labelReportEndDate.Location);
            this.labelAsterisk4.Visible = false;
            _controlDefaultLocations.Add(this.labelAsterisk4.Location);
            this.dateCalendarEnd.Visible = false;
            _controlDefaultLocations.Add(this.dateCalendarEnd.Location);

            this.labelReportDetail.Visible = false;
            _controlDefaultLocations.Add(this.labelReportDetail.Location);
            this.labelAsterisk5.Visible = false;
            _controlDefaultLocations.Add(this.labelAsterisk5.Location);
            this.comboboxReportDetail.Visible = false;
            _controlDefaultLocations.Add(this.comboboxReportDetail.Location);

            this.labelReportSortType.Visible = false;
            _controlDefaultLocations.Add(this.labelReportSortType.Location);
            this.labelAsterisk6.Visible = false;
            _controlDefaultLocations.Add(this.labelAsterisk6.Location);
            this.comboboxSortType.Visible = false;
            _controlDefaultLocations.Add(this.comboboxSortType.Location);

            this.labelReportShopNumber.Visible = false;
            _controlDefaultLocations.Add(this.labelReportShopNumber.Location);
            this.labelAsterisk7.Visible = false;
            _controlDefaultLocations.Add(this.labelAsterisk7.Location);
            this.comboboxShopNumber.Visible = false;
            _controlDefaultLocations.Add(this.comboboxShopNumber.Location); //14

            this.lblAisle.Visible = false;
            _controlDefaultLocations.Add(this.lblAisle.Location); //15
            this.txtAisle.Visible = false;
            _controlDefaultLocations.Add(this.txtAisle.Location); //16

            this.lblShelf.Visible = false;
            _controlDefaultLocations.Add(this.lblShelf.Location); //17
            this.txtShelf.Visible = false;
            _controlDefaultLocations.Add(this.txtShelf.Location); //18

            this.lblOther.Visible = false;
            _controlDefaultLocations.Add(this.lblOther.Location); //19
            this.cmbOther.Visible = false;
            _controlDefaultLocations.Add(this.cmbOther.Location);//20

            this.textboxReport.Visible = false;
            this.PerformLayout();
        }

        //cancel - return to main menu
        private void custombuttonCancelReport_Click(object sender, EventArgs e)
        {
            GlobalDataAccessor.Instance.DesktopSession.HistorySession.Back();
        }

        //select Report Type
        private void comboboxReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.criteriaPanel != null)
                this.criteriaPanel.Visible = false;

            if (this.comboboxReportType.Text != ReportHeaders.SELECT)
            {
                ArrayList reportTitle = new ArrayList();
                reportTitle.Add(new ReportTitle(0, ReportHeaders.SELECT));

                switch (this.comboboxReportType.SelectedItem.ToString())
                {
                    case ("Daily"):
                        if (SecurityProfileProcedures.CanUserViewResource("DEVRESOURCE", GlobalDataAccessor.Instance.DesktopSession.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance))
                        {
                            reportTitle.Add(new ReportTitle((int)ReportIDs.Dstr, "dvt: DSTR"));
                            reportTitle.Add(new ReportTitle((int)ReportIDs.CheckDeposit, "dvt: Check Deposit Report"));
                        }

                        for (int i = 0; i < ReportConstants.DailyNumbers.Length; i++)
                        {
                            if (UserHasResourceAccessForDailyReport(ReportConstants.DailyNumbers[i]))
                            {
                                reportTitle.Add(new ReportTitle(ReportConstants.DailyNumbers[i], ReportConstants.DailyTitles[i]));
                            }
                        }
                        break;

                    case ("Monthly"):
                        for (int i = 0; i < ReportConstants.MonthlyNumbers.Length; i++)
                        {
                            if (UserHasResourceAccessForMonthlyReport(ReportConstants.MonthlyNumbers[i]))
                            {
                                reportTitle.Add(new ReportTitle(ReportConstants.MonthlyNumbers[i], ReportConstants.MonthlyTitles[i]));
                            }
                        }
                        break;

                    case ("Inquiry"):
                        for (int i = 0; i < ReportConstants.InquiryNumbers.Length; i++)
                        {
                            int numba = ReportConstants.InquiryNumbers[i];
                            reportTitle.Add(new ReportTitle(ReportConstants.InquiryNumbers[i], ReportConstants.InquiryTitles[i]));
                        }
                        break;
                }

                this.comboboxReportName.DataSource = reportTitle;
                this.comboboxReportName.DisplayMember = "Title";
                this.comboboxReportName.ValueMember = "Number";
                this.comboboxReportName.Enabled = true;
            }
            else
            {
                this.comboboxReportName.Enabled = false;
            }
            HideControls();
        }

        private bool UserHasResourceAccessForDailyReport(int reportId)
        {
            if (reportId == (int)ReportIDs.FullLocationsReport)
            {
                return SecurityProfileProcedures.CanUserViewResource("FULL LOCATIONS", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance);
            }
            if (reportId == (int)ReportIDs.Snapshot)
            {
                return SecurityProfileProcedures.CanUserViewResource("SNAPSHOT", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance) ||
                       SecurityProfileProcedures.CanUserViewResource("SNAPSHOT MULTIPLE LOCATIONS", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance);
            }

            return true; // all other reports do not require access control
        }

        private bool UserHasResourceAccessForMonthlyReport(int reportId)
        {
            if (reportId == (int)ReportIDs.GunAuditReport)
            {
                return SecurityProfileProcedures.CanUserViewResource("DETAIL GUN AUDIT", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance);
            }
            if (reportId == (int)ReportIDs.DetailInventory)
            {
                bool found;
                found = SecurityProfileProcedures.CanUserViewResource("DETAIL INVENTORY", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance);
                return found;
            }
            return true; // all other reports do not require access control
        }

        //select Report Name
        private void comboboxReportName_SelectedIndexChanged(object sender, EventArgs e)
        {
            const string DATELABEL = "Date:";

            HideControls();
            ReportTitle reportTitle = (ReportTitle)this.comboboxReportName.SelectedItem;
            switch (reportTitle.Number)
            {
                case (int)ReportIDs.DailySales:
                    if (criteriaPanel != null)
                    {
                        criteriaPanel.Visible = true;
                    }
                    else
                    {

                        criteriaPanel = new DailySalesCriteria_panel();
                        //criteriaPanel.Top = 150;

                        this.Controls.Add(criteriaPanel);
                        this.Height += 40;
                    }
                    break;


                case (int)ReportIDs.Dstr://DSTR
                    //Location, Visible, Enabled, Text/SelectedDate - ""/0 if no change

                    break;

                case (int)ReportIDs.LoanAuditReport://Loan Audit report
                    //Location, Visible, Enabled, Text/SelectedDate - ""/0 if no change
                    MoveControls(labelReportStartDate, new object[] { _controlDefaultLocations[0], true, true, STARTDATELABEL });
                    MoveControls(labelAsterisk3, new object[] { _controlDefaultLocations[1], true, true, ASTERISK });
                    MoveControls(dateCalendarStart, new object[] { _controlDefaultLocations[2], true, true, "01/01/1900" });

                    MoveControls(labelReportEndDate, new object[] { _controlDefaultLocations[3], true, true, string.Empty });
                    MoveControls(labelAsterisk4, new object[] { _controlDefaultLocations[4], true, true, ASTERISK });
                    MoveControls(dateCalendarEnd, new object[] { _controlDefaultLocations[5], true, true, DateTime.Now.ToString("MM/dd/yyyy") });

                    //put Report Detail & Report Sort comboboxes back at the bottom
                    //-->bz0095  TLM 1/18/2011 --
                    MoveControls(labelReportDetail, new object[] { _controlDefaultLocations[6], true, true, string.Empty });
                    MoveControls(labelAsterisk5, new object[] { _controlDefaultLocations[7], true, true, ASTERISK });
                    MoveControls(comboboxReportDetail, new object[] { _controlDefaultLocations[8], true, true, string.Empty });

                    MoveControls(labelReportSortType, new object[] { _controlDefaultLocations[9], true, true, string.Empty });
                    MoveControls(labelAsterisk6, new object[] { _controlDefaultLocations[10], true, true, ASTERISK });
                    MoveControls(comboboxSortType, new object[] { _controlDefaultLocations[11], true, true, string.Empty });

                    ReportDetailCombobox();
                    SortTypeDetailCombobox();
                    //---<bz0095 -----

                    break;

                case (int)ReportIDs.FullLocationsReport://Full Locations report
                    //Location, Visible, Enabled, Text/SelectedDate - ""/0 if no change
                    const int offset = 200;
                    MoveControls(labelReportStartDate, new object[]
                                 {
                                     new System.Drawing.Point(_controlDefaultLocations[0].X + offset, _controlDefaultLocations[0].Y), 
                                     true, true, StringUtilities.fillString(" ", 5) + DATELABEL
                                 });
                    MoveControls(labelAsterisk3, new object[]
                                 {
                                     new System.Drawing.Point(_controlDefaultLocations[1].X + offset, _controlDefaultLocations[1].Y), 
                                     true, true, ASTERISK
                                 });
                    MoveControls(dateCalendarStart, new object[]
                                 {
                                     new System.Drawing.Point(_controlDefaultLocations[2].X + offset, _controlDefaultLocations[2].Y), 
                                     true, true, DateTime.Now.ToString("MM/dd/yyyy")
                                 });

                    break;

                case (int)ReportIDs.Snapshot:
                    bool canViewSnapshotMultipleLocations = SecurityProfileProcedures.CanUserViewResource("SNAPSHOT MULTIPLE LOCATIONS", CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile, CashlinxDesktopSession.Instance);
                    MoveControls(labelReportShopNumber, new object[] { _controlDefaultLocations[12], canViewSnapshotMultipleLocations, true, string.Empty });
                    MoveControls(labelAsterisk7, new object[] { _controlDefaultLocations[13], canViewSnapshotMultipleLocations, true, ASTERISK });
                    MoveControls(comboboxShopNumber, new object[] { _controlDefaultLocations[14], canViewSnapshotMultipleLocations, true, string.Empty });

                    PopulateStoreNumberCombobox();
                    break;

                case (int)ReportIDs.InPawnJewelryLocationReport://In Pawn Jewelry Location report
                    //no additional input parms
                    break;

                case (int)ReportIDs.DetailInventory:
                    //System.Drawing.Point pointlblDetail = _controlDefaultLocations[6];
                    //System.Drawing.Point pointlblAsterisk5 = _controlDefaultLocations[7];
                    //System.Drawing.Point pointCmbReportDetail = _controlDefaultLocations[8];
                    //pointlblDetail.X = 261;
                    //pointlblDetail.Y = 112;
                    //pointlblAsterisk5.X = 360;
                    //pointlblAsterisk5.Y = 112;
                    //pointCmbReportDetail.X = 380;
                    //pointCmbReportDetail.Y = 112;
                    //_controlDefaultLocations[6] = pointlblDetail;
                    //_controlDefaultLocations[7] = pointlblAsterisk5;
                    //_controlDefaultLocations[8] = pointCmbReportDetail;

                    MoveControls(labelReportStartDate, new object[] { _controlDefaultLocations[0], true, true, STARTDATELABEL });
                    MoveControls(labelAsterisk3, new object[] { _controlDefaultLocations[1], true, true, ASTERISK });
                    MoveControls(dateCalendarStart, new object[] { _controlDefaultLocations[2], true, true, "01/01/1900" });

                    MoveControls(labelReportEndDate, new object[] { _controlDefaultLocations[3], true, true, string.Empty });
                    MoveControls(labelAsterisk4, new object[] { _controlDefaultLocations[4], true, true, ASTERISK });
                    MoveControls(dateCalendarEnd, new object[] { _controlDefaultLocations[5], true, true, DateTime.Now.ToString("MM/dd/yyyy") });


                    MoveControls(labelReportDetail, new object[] { _controlDefaultLocations[6], true, true, string.Empty });
                    MoveControls(labelAsterisk5, new object[] { _controlDefaultLocations[7], true, true, ASTERISK });
                    MoveControls(comboboxReportDetail, new object[] { _controlDefaultLocations[8], true, true, string.Empty });
                    MoveControls(lblAisle, new object[] { _controlDefaultLocations[15], true, true, string.Empty });
                    MoveControls(txtAisle, new object[] { _controlDefaultLocations[16], true, true, string.Empty });
                    MoveControls(lblShelf, new object[] { _controlDefaultLocations[17], true, true, string.Empty });
                    MoveControls(txtShelf, new object[] { _controlDefaultLocations[18], true, true, string.Empty });
                    MoveControls(lblOther, new object[] { _controlDefaultLocations[19], true, true, string.Empty });
                    MoveControls(cmbOther, new object[] { _controlDefaultLocations[20], true, true, string.Empty });
                    ReportDetailCombobox();
                    OtherComboBoxPopulate();
                    break;

                case (int)ReportIDs.JewelryCount:
                    ReportDetailCombobox();
                    break;

                case (int)ReportIDs.FirearmsReport:
                    ReportDetailCombobox();

                    break;
            }

            if (comboboxReportDetail.SelectedIndex > 0)
            {
                comboboxReportDetail.SelectedIndex = 0;
            }
        }

        //select Report Detail
        private void comboboxReportDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReportTitle reportTitle = (ReportTitle)this.comboboxReportName.SelectedItem;
            _report.ReportTitle = reportTitle.Title;
            _report.ReportNumber = reportTitle.Number;

            labelReportStartDate.Visible = false;
            labelAsterisk3.Visible = false;
            dateCalendarStart.Visible = false;

            labelReportEndDate.Visible = false;
            labelAsterisk4.Visible = false;
            dateCalendarEnd.Visible = false;

            switch (_report.ReportNumber)
            {
                case (int)ReportIDs.FirearmsReport:
                    ReportDetail reportDetail = (ReportDetail)comboboxReportDetail.SelectedItem;

                    switch (reportDetail.Display)
                    {
                        // EDW - CR 14647 - MAL Removed                            
                        //case "Manager Action Log": //225, 
                        //    MoveControls(labelReportStartDate, new object[] { _controlDefaultLocations[0], true, true, STARTDATELABEL });
                        //    MoveControls(labelAsterisk3, new object[] { _controlDefaultLocations[1], true, true, ASTERISK });
                        //    MoveControls(dateCalendarStart, new object[] { _controlDefaultLocations[2], 
                        //                true, true, DateTime.Now.ToString("MM/dd/yyyy") });
                        //    textboxReport.BringToFront();

                        //    MoveControls(labelReportEndDate, new object[] { _controlDefaultLocations[3], true, true, "End Date:" });
                        //    MoveControls(labelAsterisk4, new object[] { _controlDefaultLocations[4], true, true, ASTERISK });
                        //    MoveControls(dateCalendarEnd, new object[] { _controlDefaultLocations[5], true, true, DateTime.Now.ToString("MM/dd/yyyy") });

                        //    labelReportSortType.Visible = false;
                        //    labelAsterisk6.Visible = false;
                        //    comboboxSortType.Visible = false;

                        //    comboboxSortType.Visible = false;
                        //    comboboxSortType.Enabled = false;
                        //    break;

                        case "ATF Open Records":
                            labelReportSortType.Visible = true;
                            labelAsterisk6.Visible = true;
                            comboboxSortType.Visible = true;

                            comboboxSortType.Enabled = true;
                            AddATFOpenRecordItems();
                            break;

                        case "Gun Disposition": // previously this was the Summary Report
                            MoveControls(labelReportStartDate, new object[] { _controlDefaultLocations[0], true, true, STARTDATELABEL });
                            MoveControls(labelAsterisk3, new object[] { _controlDefaultLocations[1], true, true, ASTERISK });
                            MoveControls(dateCalendarStart, new object[] { _controlDefaultLocations[2], 
                                        true, true, DateTime.Now.ToString("MM/dd/yyyy") });
                            textboxReport.BringToFront();

                            MoveControls(labelReportEndDate, new object[] { _controlDefaultLocations[3], true, true, "End Date:" });
                            MoveControls(labelAsterisk4, new object[] { _controlDefaultLocations[4], true, true, ASTERISK });
                            MoveControls(dateCalendarEnd, new object[] { _controlDefaultLocations[5], true, true, DateTime.Now.ToString("MM/dd/yyyy") });

                            labelReportSortType.Visible = false;
                            labelAsterisk6.Visible = false;
                            comboboxSortType.Visible = false;
                            comboboxSortType.Enabled = false;
                            break;

                        case "Multiple Gun Disposition Handguns":
                        case "Multiple Gun Disposition Rifles":
                            labelReportSortType.Visible = false;
                            labelAsterisk6.Visible = false;
                            comboboxSortType.Visible = false;

                            MoveControls(labelReportEndDate, new object[] { new System.Drawing.Point(_controlDefaultLocations[0].X + 190, _controlDefaultLocations[0].Y), true, true, "Report Date:" });
                            MoveControls(labelAsterisk4, new object[] { new System.Drawing.Point(_controlDefaultLocations[1].X + 200, _controlDefaultLocations[0].Y), true, true, ASTERISK });
                            MoveControls(dateCalendarEnd, new object[] { new System.Drawing.Point(_controlDefaultLocations[2].X + 200, _controlDefaultLocations[2].Y), true, true, DateTime.Now.ToString("MM/dd/yyyy") });
                            break;

                        case "Gun Audit Summary":
                            labelReportSortType.Visible = false;
                            labelAsterisk6.Visible = false;

                            comboboxSortType.Visible = false;
                            comboboxSortType.Enabled = false;
                            break;

                        case "Select":
                            labelReportSortType.Visible = false;
                            labelAsterisk6.Visible = false;
                            comboboxSortType.Visible = false;
                            break;

                        default:
                            labelReportSortType.Visible = false;
                            labelAsterisk6.Visible = false;
                            comboboxSortType.Visible = false;

                            System.Diagnostics.Debug.Assert(false, "Unhandled case. Forget to add handler?");
                            break;
                    }
                    break;

                case (int)ReportIDs.DetailInventory:
                    reportDetail = (ReportDetail)comboboxReportDetail.SelectedItem;

                    switch (reportDetail.Display)
                    {
                        case ("All merchandise except jewelry"):
                        case ("All Merchandise"):
                        case ("Jewelry"):
                        case ("Guns Only"):

                            //labelReportStartDate.Visible = true;
                            //labelAsterisk3.Visible = true;
                            //dateCalendarStart.Visible = true;

                            //labelReportEndDate.Visible = true;
                            //labelAsterisk4.Visible = true;
                            //dateCalendarEnd.Visible = true;
                            break;
                    }
                    break;

                case (int)ReportIDs.LoanAuditReport:
                    reportDetail = (ReportDetail)comboboxReportDetail.SelectedItem;

                    switch (reportDetail.Display)
                    {
                        case ("All merchandise except jewelry"):
                        case ("All Merchandise"):
                        case ("Jewelry"):

                            labelReportStartDate.Visible = true;
                            labelAsterisk3.Visible = true;
                            dateCalendarStart.Visible = true;

                            labelReportEndDate.Visible = true;
                            labelAsterisk4.Visible = true;
                            dateCalendarEnd.Visible = true;
                            break;
                    }
                    break;

            }
        }

        private void AddATFOpenRecordItems()
        {
            SortTypeATFDetailCombobox();
        }

        //date validation
        private void dateCalendarEnd_Resize(object sender, EventArgs e)
        {
            const string DATERERROR = "Date Error";
            const string STARTDATEERROR = "The Start Date cannot be a future date.";
            const string ENDDATEERROR = "The End Date cannot be a future date.";

            if (this.dateCalendarEnd.Height == 32)
            {
                ReportTitle reportTitle = (ReportTitle)this.comboboxReportName.SelectedItem;
                _report.ReportTitle = reportTitle.Title;
                _report.ReportNumber = reportTitle.Number;

                switch (_report.ReportNumber)
                {
                    //PWNU00000582 4/1/2010 SMurphy added start date info to Gun Disposition report
                    case (int)ReportIDs.RifleDispositionReport://Multi Rifle Disposition report
                    case (int)ReportIDs.GunDispositionReport://Multiple sale or Gun Disposition report
                        dateCalendarStart.Refresh();
                        break;

                    case (int)ReportIDs.LoanAuditReport://Loan Audit report
                        if (Utilities.GetDateTimeValue(dateCalendarStart.SelectedDate) > DateTime.Now)
                        {
                            MessageBox.Show(STARTDATEERROR, DATERERROR);
                            dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
                        }
                        else if (Utilities.GetDateTimeValue(dateCalendarEnd.SelectedDate) > DateTime.Now)
                        {
                            MessageBox.Show(ENDDATEERROR, DATERERROR);
                            dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
                        }
                        else
                        {
                            //populate & enable the next 2 criteria controls
                            //ReportDetailCombobox();
                            //SortTypeDetailCombobox();
                        }
                        break;

                    case (int)ReportIDs.FullLocationsReport://Full Location report
                        if (Utilities.GetDateTimeValue(dateCalendarStart.SelectedDate) > DateTime.Now)
                        {
                            MessageBox.Show(STARTDATEERROR, DATERERROR);
                            dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
                        }
                        break;
                    // EDW - Removed per CR 14647
                    //case (int)ReportIDs.MAL:
                    //    if (Utilities.GetDateTimeValue(dateCalendarStart.SelectedDate) > DateTime.Now)
                    //    {
                    //        MessageBox.Show(STARTDATEERROR, DATERERROR);
                    //        dateCalendarStart.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
                    //    }
                    //    if (Utilities.GetDateTimeValue(dateCalendarEnd.SelectedDate) > DateTime.Now)
                    //    {
                    //        MessageBox.Show(ENDDATEERROR, DATERERROR);
                    //        dateCalendarEnd.SelectedDate = DateTime.Now.ToString("MM/dd/yyyy");
                    //    }
                    //    break;
                }
            }
        }

        private void SetDetailInventoryHeaders(ReportObject _report)
        {
            //comboboxReportDetail
            OtherComboItem otherCombo = (OtherComboItem)this.cmbOther.SelectedItem;
            ReportDetail reportDetail = (ReportDetail)this.comboboxReportDetail.SelectedItem;
            if (reportDetail.Display == "All Merchandise")
            {
                _report.MerchandiseLocation = "All";
                _report.ReportDetail = reportDetail.Display;
            }
            else
            {
                if (!string.IsNullOrEmpty(txtAisle.Text) && !string.IsNullOrEmpty(txtShelf.Text))
                {
                    _report.MerchandiseLocation = txtAisle.Text + "," + txtShelf.Text;
                    _report.ReportDetail = reportDetail.Display;
                }
                else if (string.IsNullOrEmpty(txtAisle.Text) && string.IsNullOrEmpty(txtShelf.Text) && otherCombo.Display == "Select")
                {
                    _report.MerchandiseLocation = "All";
                    _report.ReportDetail = reportDetail.Display;
                }
                else if (string.IsNullOrEmpty(txtAisle.Text) && string.IsNullOrEmpty(txtShelf.Text) && otherCombo.Display != "Select")
                {
                    _report.MerchandiseLocation = otherCombo.Display;
                    _report.ReportDetail = reportDetail.Display;
                }
                else if (!string.IsNullOrEmpty(txtAisle.Text) && string.IsNullOrEmpty(txtShelf.Text))
                {
                    _report.MerchandiseLocation = txtAisle.Text;
                    _report.ReportDetail = reportDetail.Display;
                }
                else if (string.IsNullOrEmpty(txtAisle.Text) && !string.IsNullOrEmpty(txtShelf.Text))
                {
                    _report.MerchandiseLocation = txtShelf.Text;
                    _report.ReportDetail = reportDetail.Display;
                }
            }
        }

        //run report
        private void custombuttonViewReport_Click(object sender, EventArgs e)
        {
            if (!ValidateDateRanges())
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            custombuttonView.Enabled = false;

            string validationMessage = string.Empty;

            //set the report object
            _report = GetReportObject(out validationMessage);
            _report.RunDate = ShopDateTime.Instance.ShopDate.ToShortDateString();
            //get report data
            if (validationMessage.Length == 0)
            {
                var confRef = SecurityAccessor.Instance.EncryptConfig;
                var clientConfigDB = confRef.GetOracleDBService();

                //Print end of day reports
                var credentials = new Credentials
                {
                    UserName =
                    confRef.DecryptValue(clientConfigDB.DbUser),
                    PassWord =
                    confRef.DecryptValue(
                        clientConfigDB.DbUserPwd),
                    DBHost =
                    confRef.DecryptValue(clientConfigDB.Server),
                    DBPort = confRef.DecryptValue(clientConfigDB.Port),
                    DBService =
                    confRef.DecryptValue(
                        clientConfigDB.AuxInfo),
                    DBSchema =
                    confRef.DecryptValue(clientConfigDB.Schema)
                };

                switch (_report.ReportNumber)
                {
                    case (int)ReportIDs.Dstr:
                        {
                            BalanceCash bc = new BalanceCash();
                            if (ReportProcessing.AdobeReaderOpen() != null)
                            {
                                MessageBox.Show("All open Adobe files will be closed.", "Report Message");
                                ReportProcessing.AdobeReaderOpen().Kill();
                            }

                            bc.ExecuteDSTR(credentials, GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, true);

                            this.custombuttonView.Enabled = true;
                            Cursor = Cursors.Default;
                            return;
                        }

                    case (int)ReportIDs.CheckDeposit:
                        var mcdForm = new ManualCheckDepositSlipPrint();
                        bool hasData = mcdForm.GenerateMCDSlipDocument();
                        if (!hasData)
                            MessageBox.Show("No Data Found.", "Report Message");

                        this.custombuttonView.Enabled = true;

                        return;
                    case (int)ReportIDs.LoanInquiry:
                        var inquiry = new Inquiry.LoanInquiry.LoanInquirySearch();
                        // this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
                        this.Hide();
                        inquiry.ShowDialog();

                        this.custombuttonView.Enabled = true;
                        Cursor = Cursors.Default;
                        return;

                    case (int)ReportIDs.ExtInquiry:
                        var extInquiry = new Inquiry.ExtInquiry.ExtensionInquirySearch();
                        // this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
                        this.Hide();
                        extInquiry.ShowDialog();

                        this.custombuttonView.Enabled = true;
                        Cursor = Cursors.Default;
                        return;


                    case (int)ReportIDs.InventoryInquiry:
                        var invInquiry = new Inquiry.InventoryInquiry.InventoryInquirySearch();
                        // this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
                        this.Hide();
                        invInquiry.ShowDialog();

                        this.custombuttonView.Enabled = true;
                        Cursor = Cursors.Default;

                        return;


                    case (int)ReportIDs.PartialPaymentInquiry:
                        var invPPInquiry = new Inquiry.PartialPaymentInquiry.PartialPaymentInquirySearch();
                        this.Hide();
                        invPPInquiry.ShowDialog();

                        this.custombuttonView.Enabled = true;
                        Cursor = Cursors.Default;
                        return;

                    case (int)ReportIDs.RetailInquiry:
                        var retailInquiry = new RetailInquirySearch();
                        // this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
                        this.Hide();
                        retailInquiry.ShowDialog();

                        this.custombuttonView.Enabled = true;
                        Cursor = Cursors.Default;

                        return;

                    case (int)ReportIDs.CashTransferInquiry:
                        var cashTransferInquiry = new Inquiry.CashTransferInquiry.CashTransferInquirySearch();
                        // this.NavControlBox.Action = NavBox.NavAction.HIDEANDSHOW;
                        this.Hide();
                        cashTransferInquiry.ShowDialog();

                        this.custombuttonView.Enabled = true;
                        Cursor = Cursors.Default;

                        return;

                    case (int)ReportIDs.MAL: //Management Action Log

                        var reportData = new Data(Convert.ToDateTime(this.dateCalendarStart.SelectedDate),
                                                  Convert.ToDateTime(this.dateCalendarEnd.SelectedDate),
                                                  GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, credentials);

                        if (reportData.ErrCode != "OK")
                        {
                            MessageBox.Show("An Error Occurred while retrieving the report data", reportData.ErrTxt, MessageBoxButtons.OK);
                            this.custombuttonView.Enabled = true;
                            Cursor = Cursors.Default;
                            return;
                        }

                        string filename;
                        bool retval = buildMAL(reportData, out filename, false);

                        if (retval)
                            DesktopSession.ShowPDFFile(filename, false);
                        else
                        {
                            MessageBox.Show("The Gun Dispostion report could not be generated because there were no records found.", "No Data Found", MessageBoxButtons.OK);

                        }


                        this.custombuttonView.Enabled = true;
                        Cursor = Cursors.Default;
                        return;

                    //------------------
                    /*

                    // Ensure validation code fires, and the user has a chance to fix it.
                    dateCalendarStart_Leave(sender, e);
                    dateCalendarEnd_Leave(sender, e);

                    var reportData = new Data(Convert.ToDateTime(this.dateCalendarStart.SelectedDate),
                                                Convert.ToDateTime(this.dateCalendarEnd.SelectedDate),
                                              CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber, credentials);

                    if (reportData.ErrCode != "OK")
                    {
                        MessageBox.Show("An Error Occurred while retrieving the report data", reportData.ErrTxt, MessageBoxButtons.OK);
                        this.custombuttonView.Enabled = true;
                        Cursor = Cursors.Default;
                        return;
                    }
                    else
                    {
                        var dataSets = reportData.BuildDataset();
                        if (dataSets != null && dataSets.IsValid)
                        {
                            DataTable dt;
                            dataSets.GetTable("DISP_DATA", out dt);

                            if (dt == null)
                            {
                                MessageBox.Show("The Gun Dispostion report could not be generated because there were no records found.", "No Data Found", MessageBoxButtons.OK);
                                this.custombuttonView.Enabled = true;
                                Cursor = Cursors.Default;
                                return;
                            }
                   
                            var fileName = @"mal_report_" + DateTime.Now.Ticks + ".pdf";

                            string rptDir = PawnSecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
                            //here group dataset by transaction date.

                            var pdfRpt = new PawnReports.Reports.MAL.BuildRpt(CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber,
                                                                              Convert.ToDateTime(this.dateCalendarEnd.SelectedDate),
                                                                              Convert.ToDateTime(this.dateCalendarStart.SelectedDate),
                                                                              dataSets, rptDir, fileName);

                            pdfRpt.CreateRpt();


                            CashlinxDesktopSession.ShowPDFFile(pdfRpt.ReportDir + @"\" + pdfRpt.RptFileName, false);
                        }

                        this.custombuttonView.Enabled = true;
                        Cursor = Cursors.Default;
                        
                    } 
                    return;
                         
                     */
                }

                DataSet outputDataSet = new DataSet();
                //PWNU00000582 4/1/2010 SMurphy added start date info to Gun Disposition report 
                outputDataSet = ReportsProcedures.GetCursors(_report);

                if (outputDataSet != null && outputDataSet.Tables.Count > 0 && _report.ReportErrorLevel == 0)
                {
                    switch (_report.ReportNumber)
                    {
                        case (int)ReportIDs.DetailInventory:
                            SetDetailInventoryHeaders(_report);
                            _report.DetailInventoryData = ReportsProcedures.GetDetailInventoryData(outputDataSet, _report);
                            break;

                        case (int)ReportIDs.RifleDispositionReport://Multi Rifle Disposition report
                        case (int)ReportIDs.GunDispositionReport://Multi Handgun Disposition report
                            //Gun Disposition report uses outputdataset
                            _report.GunDispositionData = outputDataSet;
                            if (outputDataSet != null && outputDataSet.Tables["OUTPUT"] != null && outputDataSet.Tables["OUTPUT"].Rows.Count > 0)
                                _report.ReportParms[0] = outputDataSet.Tables["OUTPUT"].Rows[0][1];
                            break;

                        case (int)ReportIDs.LoanAuditReport://Loan Audit report
                            _report.LoanAuditData = ReportsProcedures.GetLoanAuditData(outputDataSet.Tables["loan_audit"], _report);
                            break;

                        case (int)ReportIDs.FullLocationsReport://Full Locations report
                            _report.FullLocationsData = ReportsProcedures.GetFullLocationData(outputDataSet, _report);
                            break;

                        case (int)ReportIDs.Snapshot://Snapshot report
                            _report.SnapshotData = ReportsProcedures.GetSnapshotData(outputDataSet, _report);
                            break;

                        case (int)ReportIDs.InPawnJewelryLocationReport://In Pawn Jewelry Locations report
                            _report.InPawnJewelryData = ReportsProcedures.GetInPawnJewelryData(outputDataSet.Tables["inpawn_jewelry"], _report);
                            break;

                        case (int)ReportIDs.GunAuditReport://Gun Audit report
                            _report.GunAuditData = ReportsProcedures.GetGunAuditData(outputDataSet.Tables["gun_audit"], _report);
                            break;

                        case (int)ReportIDs.GunAuditReportATFOpenRecords://Gun Audit report ATF
                            _report.GunAuditDataATFOpenRecordsData = ReportsProcedures.GetGunAuditATFOpenRecordsData(outputDataSet.Tables["gun_audit"], _report);
                            break;

                        case (int)ReportIDs.CACCSales://CACC Sales Report
                            //_report.CACCSalesData = ReportsProcedures.GetCACCSalesData(outputDataSet.Tables["cacc_sales"], _report);
                            _report.CACCSalesData = outputDataSet.Tables["cacc_sales"];
                            break;

                        case (int)ReportIDs.JewelryCount://Jewelry Count Report
                            _report.JewleryCountDetailData = outputDataSet.Tables["jewelry_sales"];
                            _report.JewleryCountSummaryData = outputDataSet.Tables["jewelry_smry"];
                            // Enforce that this report detail must have a valid value.
                            _report.ReportDetail = this.comboboxReportDetail.Text;

                            break;

                        case (int)ReportIDs.DailySales:
                            if (_report.ReportDetail == "Detail")
                                _report.DailySalesData = outputDataSet.Tables["daily_sales"];
                            else
                                _report.DailySalesSummaryData = outputDataSet.Tables["daily_sales_summary"];
                            break;
                    }

                    if (_report.ReportErrorLevel == 0)
                    {
                        if (ReportProcessing.AdobeReaderOpen() != null)
                        {
                            MessageBox.Show("All open Adobe files will be closed.", "Report Message");
                        }
                        ReportProcessing.DoReport(_report, PdfLauncher.Instance);
                    }
                }

                if (_report.ReportErrorLevel != 0)
                {
                    if ((LogLevel)_report.ReportErrorLevel != LogLevel.DEBUG)
                    {
                        MessageBox.Show(_report.ReportError, "Report Message: " + Enum.GetName(typeof(LogLevel), _report.ReportErrorLevel));
                    }
                    FileLogger.Instance.logMessage((LogLevel)_report.ReportErrorLevel, this, _report.ReportError);
                }
            }//didn't pass input parm validation
            else
            {
                MessageBox.Show(validationMessage, "Report Selection");
            }

            this.custombuttonView.Enabled = true;
            Cursor = Cursors.Default;
        }

        public static bool buildMAL(Data reportData, out string fileName, bool emptyReport)
        {
            fileName = string.Empty;

            if (reportData != null && reportData.ErrCode != "OK")
                return false;
            else
            {
                var dataSets = reportData.BuildDataset();
                if (dataSets != null && dataSets.IsValid)
                {
                    DataTable dt;
                    dataSets.GetTable("DISP_DATA", out dt);

                    if (dt == null)//&& !emptyReport)  // may not want to skip if null
                    {
                        return false;
                    }

                    fileName = @"mal_report_" + DateTime.Now.Ticks + ".pdf";


                    string rptDir = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath;
                    //here group dataset by transaction date.

                    var pdfRpt = new global::Reports.MAL.BuildRpt(GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                        reportData.RunEndDate,
                        reportData.RunDate,
                        dataSets, rptDir, fileName);

                    pdfRpt.CreateRpt();

                    if (pdfRpt.ErrorCode != "OK")
                        return false;


                    fileName = pdfRpt.ReportDir + @"\" + pdfRpt.RptFileName;

                    if (System.IO.File.Exists(fileName) && dt == null && !emptyReport)
                        System.IO.File.Delete(fileName);
                }


            }
            return true;
        }

        public static void PrintGunDispositionReport() // need to update this to facilitate auto print of rifle report
        {
            var _report = new ReportObject()
            {
                ReportTitle = "Gun Disposition",
                ReportNumber = (int)ReportIDs.GunDispositionReport,
                ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\",
                ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber,
                ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreName,
                ReportError = string.Empty,
                ReportErrorLevel = 0
            };

            var startDate = ShopDateTime.Instance.ShopDate.AddDays(-1 * ReportConstants.GUNDISPNUMOFDAYS);
            _report.ReportParms.AddRange(new object[]
            {
                    startDate.ToShortDateString(), ShopDateTime.Instance.ShopDate.ToShortDateString()
            });
            _report.ReportParms.AddRange(new object[]
            {
                    ReportConstants.GUNDISPBACKWARDS, ReportConstants.GUNDISPNUMOFDAYS
            });

            for (int i = 0; i < 2; i++) // run for both handguns and rifles
            {
                _report.GunDispositionData = ReportsProcedures.GetCursors(_report);

                //ReportProcessing.DoReport(_report);

                RunReport runReport = new RunReport();
                runReport.reportObject = _report;

                if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsMultipleDispositionOfCertainRiflesReportPrintedForSite(GlobalDataAccessor.Instance.CurrentSiteId))
                {
                    if (runReport.CreateReport(PdfLauncher.Instance))
                    {
                        //TODO: Store in couch db
                        if (GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                        {
                            PrintingUtilities.printDocument(_report.ReportTempFileFullName,
                                                            GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                                            GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);
                        }
                    }

                    _report.ReportNumber = (int)ReportIDs.RifleDispositionReport;
                    _report.ReportTitle = "Multiple Gun Disposition Rifles";
                }
                else
                {
                    break;
                }
            }

            // Regular gun disposition report
            //===============================

            var confRef = SecurityAccessor.Instance.EncryptConfig;
            var clientConfigDB = confRef.GetOracleDBService();

            var credentials = new Credentials
            {
                UserName =
                confRef.DecryptValue(clientConfigDB.DbUser),
                PassWord =
                confRef.DecryptValue(
                    clientConfigDB.DbUserPwd),
                DBHost =
                confRef.DecryptValue(clientConfigDB.Server),
                DBPort = confRef.DecryptValue(clientConfigDB.Port),
                DBService =
                confRef.DecryptValue(
                    clientConfigDB.AuxInfo),
                DBSchema =
                confRef.DecryptValue(clientConfigDB.Schema)
            };
            // DONT CHECK THIS IN WITH HARDCODED DATE -- Convert.ToDateTime("2/1/2012"),//
            var reportData = new Data(System.DateTime.Now,
                                      System.DateTime.Now,
                                      GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, credentials);

            string filename;

            if (buildMAL(reportData, out filename, true))
            {
                if (GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                {
                    PrintingUtilities.printDocument(filename,
                                                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                                                    GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 1);
                }
            }

        }

        //
        //  custom methods
        //
        //parameter selection validation
        private bool ParmValidation(bool checkReportType, bool checkReportName, bool checkStartDate, bool checkEndDate,
                                    bool checkReportDetail, bool checkSortType, bool checkStartAndEnd,
                                    bool checkFutureDate, out string validationMessage)
        {
            bool isValidated = true;
            validationMessage = string.Empty;
            //SMurphy 3/11/2010 added vivsble validation checks
            if (checkReportType && (comboboxReportType.SelectedIndex <= 0 || !comboboxReportType.Visible))
            {
                validationMessage = "Please select a Report Type\n\n";
                isValidated = false;
            }

            if (checkReportName && (comboboxReportName.SelectedIndex <= 0 || !comboboxReportName.Visible))
            {
                validationMessage += "Please select a Report Name\n\n";
                isValidated = false;
            }

            if (checkStartDate && (dateCalendarStart.SelectedDate.Length == 0 || !dateCalendarStart.Visible))
            {
                validationMessage += "Please select a Start Date\n\n";
                isValidated = false;
            }

            if (checkEndDate && (dateCalendarEnd.SelectedDate.Length == 0 || !dateCalendarEnd.Visible))
            {
                validationMessage += "Please select an End Date\n\n";
                isValidated = false;
            }

            if (checkFutureDate && (dateCalendarEnd.SelectedDate.Length == 0 || !dateCalendarEnd.Visible))
            {
                if (Convert.ToDateTime(dateCalendarEnd.SelectedDate) > DateTime.Now)
                {
                    validationMessage += "You cannot have a future End Date\n\n";
                    isValidated = false;
                }
            }

            if (checkStartAndEnd && dateCalendarStart.SelectedDate.Length > 0 && dateCalendarEnd.SelectedDate.Length > 0)
            {
                if (Convert.ToDateTime(dateCalendarStart.SelectedDate) > Convert.ToDateTime(dateCalendarEnd.SelectedDate))
                {
                    validationMessage += "The End Date must be more recent than the Start Date\n\n";
                    isValidated = false;
                }
            }

            if (checkReportDetail && (comboboxReportDetail.SelectedIndex <= 0 || !comboboxReportDetail.Visible))
            {
                validationMessage += "Please select a Report Detail\n\n";
                isValidated = false;
            }

            if (checkSortType && (comboboxSortType.SelectedIndex <= 0 || !comboboxSortType.Visible))
            {
                validationMessage += "Please select a Sort Type";
                isValidated = false;
            }

            return isValidated;
        }

        //load up the report object
        private ReportObject GetReportObject(out string validationMessage)
        {
            _report = new ReportObject();

            //must have at least a Report Type & Report name selected
            if (ParmValidation(true, true, false, false, false, false, false, false, out validationMessage))
            {
                ReportTitle reportTitle = (ReportTitle)this.comboboxReportName.SelectedItem;
                switch (comboboxReportDetail.Text)
                {
                    // EDW - Removed per CR 14647
                    //case "Manager Action Log":
                    //    _report.ReportTitle = "Manager Action Log";
                    //    _report.ReportNumber = (int)ReportIDs.MAL;
                    //    break;

                    case "ATF Open Records":
                        _report.ReportTitle = "Firearm/ATF Open Records";
                        _report.ReportNumber = (int)ReportIDs.GunAuditReportATFOpenRecords;
                        break;

                    case "Gun Disposition": // previously this was the Sumary Report
                        _report.ReportTitle = "Gun Disposition";
                        _report.ReportNumber = (int)ReportIDs.MAL;
                        break;

                    case "Multiple Gun Disposition Handguns":
                        _report.ReportNumber = (int)ReportIDs.GunDispositionReport;
                        _report.ReportTitle = "Multiple Gun Disposition ReportIDs";
                        break;

                    case "Multiple Gun Disposition Rifles":
                        _report.ReportNumber = (int)ReportIDs.RifleDispositionReport;
                        _report.ReportTitle = "Multiple Gun Disposition Rifles";
                        break;

                    case "Gun Audit Summary":
                        _report.ReportNumber = (int)ReportIDs.GunAuditReport;
                        _report.ReportTitle = "Gun Audit Summary";
                        break;

                    default:
                        _report.ReportTitle = reportTitle.Title;
                        _report.ReportNumber = reportTitle.Number;
                        break;
                }

                _report.ReportTempFile = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\";
                if (comboboxShopNumber.Visible)
                {
                    _report.ReportStore = comboboxShopNumber.Text;
                }
                else
                {
                    _report.ReportStore = GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber;
                }

                _report.ReportStoreDesc = GlobalDataAccessor.Instance.CurrentSiteId.StoreName;//"CASH AMERICA PAWN OF DFW";
                _report.ReportError = string.Empty;
                _report.ReportErrorLevel = 0;

                ReportDetail reportDetail = new ReportDetail(string.Empty, string.Empty);
                SortType sortType = new SortType(string.Empty, string.Empty);

                switch (_report.ReportNumber)
                {
                    case (int)ReportIDs.DetailInventory://Detail Inventory
                        var otherItem = (OtherComboItem)cmbOther.SelectedItem;

                        _report.ReportParms.AddRange(new object[] { txtAisle.Text, txtShelf.Text });
                        _report.ReportParms.AddRange(new object[] { otherItem.Filter });
                        reportDetail = (ReportDetail)comboboxReportDetail.SelectedItem;
                        _report.ReportDetail = reportDetail.Display;
                        _report.ReportFilter = reportDetail.Filter;
                        break;

                    case (int)ReportIDs.GunDispositionReport://Gun Disposition report
                    case (int)ReportIDs.RifleDispositionReport://Multi Rifle Disposition report
                        //PWNU00000582 4/1/2010 SMurphy added start date info
                        _report.ReportParms.AddRange(new object[] { textboxReport.Text, dateCalendarEnd.SelectedDate });
                        _report.ReportParms.AddRange(new object[] { ReportConstants.GUNDISPBACKWARDS, ReportConstants.GUNDISPNUMOFDAYS });
                        break;

                    case (int)ReportIDs.LoanAuditReport://Loan Audit report
                        if (ParmValidation(true, true, true, true, true, true, true, true, out validationMessage))
                        {
                            _report.ReportParms.AddRange(new object[] { dateCalendarStart.SelectedDate, dateCalendarEnd.SelectedDate });

                            reportDetail = (ReportDetail)comboboxReportDetail.SelectedItem;
                            _report.ReportDetail = reportDetail.Display;
                            _report.ReportFilter = reportDetail.Filter;

                            sortType = (SortType)comboboxSortType.SelectedItem;
                            _report.ReportSort = sortType.Display;
                            _report.ReportSortSQL = sortType.Fields;
                        }
                        else
                        {
                            if (String.Compare("The End Date", validationMessage) == 1)
                            {
                                validationMessage = "After selecting a Start and End date: \n" + validationMessage;
                            }
                        }

                        break;

                    case (int)ReportIDs.GunAuditReport://Gun Audit report
                        _report.ReportParms.AddRange(new object[] { dateCalendarStart.SelectedDate, dateCalendarEnd.SelectedDate });
                        reportDetail = (ReportDetail)comboboxReportDetail.SelectedItem;
                        _report.ReportDetail = "Summary";
                        _report.ReportFilter = reportDetail.Filter;

                        //for Gun Audit report - Summary selection 
                        //if (_report.ReportDetail == "Summary" && _report.ReportNumber == (int)ReportIDs.GunAuditReport)
                        //{
                        if (ParmValidation(true, true, false, false, true, false, false, false, out validationMessage))
                        {
                            _report.ReportSortSQL = "status_cd ASC, gun_type ASC";
                        }
                        //}
                        //else
                        //{
                        //    if (ParmValidation(true, true, false, false, true, true, false, false, out validationMessage))
                        //    {
                        //        sortType = (SortType)comboboxSortType.SelectedItem;
                        //        _report.ReportSort = sortType.Display;
                        //        _report.ReportSortSQL = sortType.Fields;
                        //    }
                        //}
                        break;

                    case (int)ReportIDs.FullLocationsReport://Full locations report
                        if (ParmValidation(true, true, true, false, false, false, false, true, out validationMessage))
                        {
                            _report.ReportParms.AddRange(new object[] { dateCalendarStart.SelectedDate, dateCalendarStart.SelectedDate });

                            _report.ReportSort = string.Empty;
                            _report.ReportSortSQL = string.Empty;

                            _report.ReportDetail = string.Empty;
                            _report.ReportFilter = string.Empty;
                        }
                        break;

                    case (int)ReportIDs.InPawnJewelryLocationReport://In Pawn Jewelry Locations report
                        _report.ReportParms.AddRange(new object[] { DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.ToString("MM/dd/yyyy") });

                        _report.ReportSort = string.Empty;
                        _report.ReportSortSQL = "location";

                        _report.ReportDetail = string.Empty;
                        _report.ReportFilter = string.Empty;
                        break;

                    case (int)ReportIDs.CACCSales:
                    case (int)ReportIDs.Snapshot:
                        _report.ReportParms.AddRange(new object[] { ShopDateTime.Instance.ShopDate.FormatDate() });
                        break;

                    case (int)ReportIDs.DailySales:
                        _report.ReportParms.AddRange(new object[] { criteriaPanel.dateCalendarStart.SelectedDate, criteriaPanel.dateCalendarEnd.SelectedDate });
                        _report.ReportParms.AddRange(new object[] { criteriaPanel.LowSalesAmt_tb.Text, criteriaPanel.HighSalesAmt_tb.Text });
                        _report.ReportParms.AddRange(new object[] { criteriaPanel.LowVariance_tb.Text, criteriaPanel.HighVariance_tb.Text });
                        _report.ReportSort = criteriaPanel.SortBy;
                        _report.ReportParms.Add(criteriaPanel.Option);
                        _report.ReportDetail = criteriaPanel.ReportDetail;
                        break;

                    case (int)ReportIDs.JewelryCount:
                        _report.ReportParms.AddRange(new object[] { ShopDateTime.Instance.ShopDate.FormatDate() });
                        if (this.comboboxReportDetail.Text.Equals("Select"))
                        {
                            _report.ReportError = "Please select the level of report detail desired.";
                            _report.ReportErrorLevel = (int)LogLevel.ERROR;
                        }
                        break;

                    case (int)ReportIDs.GunAuditReportATFOpenRecords:
                        /* ATFOpenRecordsReport atm = new ATFOpenRecordsReport();
                         _report.ReportTempFileFullName = PawnSecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath + "\\ATFOpenRecords" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";
                         atm.ReportObject = _report;
                         atm.CreateReport();*/
                        _report.ReportParms.AddRange(new object[] { DateTime.Now.ToString("MM/dd/yyyy"), DateTime.Now.ToString("MM/dd/yyyy") });
                        reportDetail = (ReportDetail)comboboxReportDetail.SelectedItem;
                        _report.ReportDetail = reportDetail.Display;
                        _report.ReportFilter = reportDetail.Filter;
                        if (ParmValidation(true, true, false, false, true, true, false, false, out validationMessage))
                        {
                            sortType = (SortType)comboboxSortType.SelectedItem;
                            _report.ReportSort = sortType.Display;
                            _report.ReportSortSQL = sortType.Fields;
                        }
                        //InventoryChargeOffFields invFields = new InventoryChargeOffFields();
                        //CashlinxDesktop.DesktopForms.Report.CreateReportObject cro = new Report.CreateReportObject();
                        //cro.GetInventoryChargeOffReport(invFields);
                        break;

                    // EDW - Removed per CR 14647
                    //case (int)ReportIDs.MAL:
                    //    _report.ReportParms.AddRange(new object[] { dateCalendarStart.SelectedDate, dateCalendarEnd.SelectedDate });
                    //    break;
                }
            }

            FileLogger.Instance.logMessage(LogLevel.DEBUG, this, _report.ReportTitle + " Validation Message: " + validationMessage);

            return _report;
        }

        //manage comboboxes
        private void OtherComboBoxPopulate()
        {
            ArrayList arrayOtherComboItems = new ArrayList();
            arrayOtherComboItems.Add(new OtherComboItem("Select", "Select"));
            arrayOtherComboItems.Add(new OtherComboItem("Safe", "SAFE"));
            arrayOtherComboItems.Add(new OtherComboItem("Gun Vault", "GUN SAFE"));
            this.cmbOther.DataSource = arrayOtherComboItems;
            this.cmbOther.DisplayMember = "Display";
            this.cmbOther.ValueMember = "Filter";
            //Safe, Gun Vault
        }

        private void PopulateStoreNumberCombobox()
        {
            if (!comboboxShopNumber.Visible)
            {
                return;
            }

            comboboxShopNumber.Items.Clear();
            //DataTable dt = ReportsProcedures.GetStoresInMarketForUserId("15902");
            DataTable dt = ReportsProcedures.GetStoresInMarketForUserId(CashlinxDesktopSession.Instance.LoggedInUserSecurityProfile.UserID);
            if (dt == null)
            {
                this.labelReportShopNumber.Visible = false;
                this.labelAsterisk7.Visible = false;
                this.comboboxShopNumber.Visible = false;
                return;
            }

            foreach (DataRow dr in dt.Rows)
            {
                comboboxShopNumber.Items.Add(dr["storenumber"].ToString());
            }
        }

        private void ReportDetailCombobox()
        {
            ReportTitle reportTitle = (ReportTitle)this.comboboxReportName.SelectedItem;

            ArrayList reportDetail = new ArrayList();
            if (reportTitle.Number != (int)ReportIDs.DetailInventory)
            {
                reportDetail.Add(new ReportDetail("Select", "0"));
            }

            switch (reportTitle.Number)
            {
                case (int)ReportIDs.LoanAuditReport://Loan audit report
                    reportDetail.Add(new ReportDetail("All Merchandise", string.Empty));
                    reportDetail.Add(new ReportDetail("Jewelry", "cat_code <= 1999"));
                    reportDetail.Add(new ReportDetail("All merchandise except jewelry", "cat_code >=2000"));
                    break;

                case (int)ReportIDs.DailySales:
                case (int)ReportIDs.JewelryCount:
                    reportDetail.Add(new ReportDetail("Detailed", string.Empty));
                    reportDetail.Add(new ReportDetail("Summary", string.Empty));
                    break;

                case (int)ReportIDs.DetailInventory:
                    reportDetail.Add(new ReportDetail("All Merchandise", string.Empty));
                    reportDetail.Add(new ReportDetail("All merchandise except jewelry", "cat_code >=2000"));
                    reportDetail.Add(new ReportDetail("Guns Only", "cat_code >= 4000 and cat_code <= 4900"));
                    reportDetail.Add(new ReportDetail("Jewelry", "cat_code <= 1999"));
                    break;

                case (int)ReportIDs.FirearmsReport:
                    reportDetail.Add(new ReportDetail("ATF Open Records", string.Empty));
                    reportDetail.Add(new ReportDetail("Gun Disposition", string.Empty));
                    // EDW - CR 14647 - MAL Removed                            
                    //reportDetail.Add(new ReportDetail("Manager Action Log", string.Empty)); 
                    reportDetail.Add(new ReportDetail("Multiple Gun Disposition Handguns", string.Empty));
                    if (new BusinessRulesProcedures(GlobalDataAccessor.Instance.DesktopSession).IsMultipleDispositionOfCertainRiflesReportPrintedForSite(GlobalDataAccessor.Instance.CurrentSiteId))
                    {
                        reportDetail.Add(new ReportDetail("Multiple Gun Disposition Rifles", string.Empty));
                    }
                    reportDetail.Add(new ReportDetail("Gun Audit Summary", string.Empty));
                    break;
            }

            this.comboboxReportDetail.DataSource = reportDetail;
            this.comboboxReportDetail.DisplayMember = "Display";
            this.comboboxReportDetail.ValueMember = "Filter";
            this.comboboxReportDetail.SelectedIndex = 0;
            this.labelReportDetail.Visible = true;
            this.labelAsterisk5.Visible = true;
            this.comboboxReportDetail.Visible = true;
            this.comboboxReportDetail.Enabled = true;
        }

        private void SortTypeATFDetailCombobox()
        {
            ArrayList sortType = new ArrayList();
            sortType.Add(new SortType("Select", "0"));
            sortType.Add(new SortType("Gun Number", "gun_number ASC, status_cd  ASC, loc_aisle ASC, loc_shelf ASC, icn ASC"));
            sortType.Add(new SortType("Type", "gun_type ASC, status_cd ASC, loc_aisle ASC, loc_shelf ASC, icn ASC"));
            sortType.Add(new SortType("Location", "loc_aisle ASC, loc_shelf ASC, location ASC, gun_number ASC, status_cd  ASC, icn ASC"));

            this.comboboxSortType.DataSource = sortType;
            this.comboboxSortType.DisplayMember = "Display";
            this.comboboxSortType.ValueMember = "Fields";

            this.labelReportSortType.Visible = true;
            this.labelAsterisk6.Visible = true;
            this.comboboxSortType.Visible = true;
            this.comboboxSortType.Enabled = true;
        }

        private void SortTypeDetailCombobox()
        {
            ReportTitle reportTitle = (ReportTitle)this.comboboxReportName.SelectedItem;
            ArrayList sortType = new ArrayList();
            sortType.Add(new SortType("Select", "0"));
            switch (reportTitle.Number)
            {
                case (int)ReportIDs.LoanAuditReport://Loan audit report
                    sortType.Add(new SortType("Location", "loc_aisle ASC, loc_shelf ASC, location ASC"));
                    sortType.Add(new SortType("Loan Ticket Number", "org_ticket"));
                    sortType.Add(new SortType("Date", "date_made"));
                    break;

                case (int)ReportIDs.FirearmsReport:
                    ReportDetail reportDetail = (ReportDetail)comboboxReportDetail.SelectedItem;

                    switch (reportDetail.Display)
                    {
                        case "Multiple Gun Disposition Handguns":
                        case "Multiple Gun Disposition Rifles":
                            // EDW - CR 14647 - MAL Removed                            
                            //case "Manager Action Log":
                            break;

                        //case "Gun Audit Summary":
                        //    sortType.Add(new SortType("Status", "status_cd ASC, gun_type ASC, loc_aisle ASC, loc_shelf ASC, icn ASC"));
                        //    sortType.Add(new SortType("Gun Number", "gun_number ASC, status_cd  ASC, loc_aisle ASC, loc_shelf ASC, icn ASC"));
                        //    break;

                        //case "Gun Disposition": // (int)ReportIDs.GunAuditReport://Gun Audit report
                        //    sortType.Add(new SortType("Status", "status_cd ASC, gun_type ASC, loc_aisle ASC, loc_shelf ASC, icn ASC"));
                        //    sortType.Add(new SortType("Gun Number", "gun_number ASC, status_cd  ASC, loc_aisle ASC, loc_shelf ASC, icn ASC"));
                        //    break;

                        case "ATF Open Records": // (int)ReportIDs.GunAuditReportATFOpenRecords://Gun Audit report
                            sortType.Add(new SortType("Gun Number", "gun_number ASC, status_cd  ASC, loc_aisle ASC, loc_shelf ASC, icn ASC"));
                            sortType.Add(new SortType("Type", "gun_type ASC, status_cd ASC, loc_aisle ASC, loc_shelf ASC, icn ASC"));
                            sortType.Add(new SortType("Location", "loc_aisle ASC, loc_shelf ASC, gun_number ASC, status_cd  ASC, icn ASC"));
                            break;

                        default:
                            System.Diagnostics.Debug.Assert(false, "Unexpected case. Forget to add handler?");
                            break;
                    }
                    break;
                case (int)ReportIDs.DetailInventory:
                    break;
            }

            this.comboboxSortType.DataSource = sortType;
            this.comboboxSortType.DisplayMember = "Display";
            this.comboboxSortType.ValueMember = "Fields";

            this.labelReportSortType.Visible = true;
            this.labelAsterisk6.Visible = true;
            this.comboboxSortType.Visible = true;
            this.comboboxSortType.Enabled = true;
        }

        //control methods       
        private void HideControls()
        {
            labelReportStartDate.Visible = false;
            labelAsterisk3.Visible = false;
            dateCalendarStart.Visible = false;

            labelReportEndDate.Visible = false;
            labelAsterisk4.Visible = false;
            dateCalendarEnd.Visible = false;

            labelReportDetail.Visible = false;
            labelAsterisk5.Visible = false;
            comboboxReportDetail.Visible = false;

            labelReportSortType.Visible = false;
            labelAsterisk6.Visible = false;
            comboboxSortType.Visible = false;

            labelReportShopNumber.Visible = false;
            labelAsterisk7.Visible = false;
            comboboxShopNumber.Visible = false;

            lblAisle.Visible = false;
            txtAisle.Visible = false;

            lblShelf.Visible = false;
            txtShelf.Visible = false;

            lblOther.Visible = false;
            cmbOther.Visible = false;

            textboxReport.Visible = false;
        }

        private static void MoveControls(Control controlObject, object[] values)
        {
            Type type = controlObject.GetType();

            switch (type.Name.ToLower())
            {
                case "datecalendar":
                    DateCalendar dateCalendar = (DateCalendar)controlObject;
                    if (!string.IsNullOrEmpty(values[3].ToString()))
                        dateCalendar.SelectedDate = (string)values[3];
                    break;

                case "label":
                    Label label = (Label)controlObject;
                    if (!string.IsNullOrEmpty(values[3].ToString()))
                        label.Text = values[3].ToString();
                    break;

                case "textbox":
                    TextBox textbox = (TextBox)controlObject;
                    if (!string.IsNullOrEmpty(values[3].ToString()))
                        textbox.Text = values[3].ToString();
                    break;
            }

            controlObject.Location = (System.Drawing.Point)values[0];
            controlObject.Visible = (bool)values[1];
            controlObject.Enabled = (bool)values[2];
        }

        //PWNU00000582 4/2/2010 SMurphy added start date info to Gun Disposition report  
        private string GetDate(int numDays, int direction, DateTime inDate)
        {
            for (int i = 0; i < numDays; i++)
            {
                //if the ShopCalendar day of week = date and there is no store open time OR the date is a holiday for the store - increment
                //we want to move backwards 5 business days
                if ((GlobalDataAccessor.Instance.DesktopSession.ShopCalendar.FindIndex(delegate(ShopCalendarVO dateInfo)
                { return dateInfo.NameOfDay == inDate.DayOfWeek.ToString() && dateInfo.OpenTime == null; }) > 0) ||
                     (GlobalDataAccessor.Instance.DesktopSession.ShopCalendar.FindIndex(delegate(ShopCalendarVO dateInfo)
                     { return dateInfo.CalendarDate == inDate || dateInfo.IsHoliday; }) > 0))
                {
                    inDate = inDate.AddDays(direction);
                }
                inDate = inDate.AddDays(direction);
            }

            return inDate.ToString("MM/dd/yyyy");
        }

        private void comboboxReportName_MouseClick(object sender, MouseEventArgs e)
        {
            if (criteriaPanel != null)
            {
                criteriaPanel.Visible = false;
            }
        }

        private void dateCalendarEnd_Leave(object sender, EventArgs e)
        {
            ValidateDateRanges();
        }

        private Boolean ValidateDateRanges()
        {
            Boolean validData = true;

            if (_report.ReportNumber == (int)ReportIDs.FirearmsReport ||
                _report.ReportNumber == (int)ReportIDs.MAL)
            {
                if (this.comboboxReportDetail.Text == "Multiple Gun Disposition Handguns" ||
                this.comboboxReportDetail.Text == "Multiple Gun Disposition Rifles")
                {
                    try
                    {
                        textboxReport.Text = GetDate(ReportConstants.GUNDISPNUMOFDAYS, -1, Convert.ToDateTime(dateCalendarEnd.SelectedDate));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Invalid Date", MessageBoxButtons.OK);
                        validData = false;
                    }
                }
                else if (this.comboboxReportDetail.Text == "Gun Disposition")
                {
                    try
                    {
                        // Today
                        DateTime dtNow = DateTime.Now;
                        DateTime dtStart = DateTime.Parse(this.dateCalendarStart.DateText.DateTextBox.Text);
                        DateTime dtEnd = DateTime.Parse(this.dateCalendarEnd.DateText.DateTextBox.Text);
                        //Date dtSop = this.dateCalendarEnd.DateText;

                        if (dtStart < dtNow.AddYears(-1)) // 1 year max
                        {
                            MessageBox.Show("This report is not available more than one year prior to the current date.", "Error", MessageBoxButtons.OK);
                            dateCalendarStart.Focus();
                            validData = false;
                        }
                        else if ((dtEnd - dtStart).TotalDays > 90) // 90 max max
                        {
                            MessageBox.Show("Please restrict the date range to a period of 3 months.", "Error", MessageBoxButtons.OK);
                            dateCalendarEnd.Focus();
                            validData = false;
                        }

                        //textboxReport.Text = GetDate(ReportConstants.GUNDISPNUMOFDAYS, -1, Convert.ToDateTime(dateCalendarStart.SelectedDate));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Invalid Date", MessageBoxButtons.OK);
                        dateCalendarStart.Focus();
                        validData = false;
                    }
                }
            }

            return validData;
        }

        private void dateCalendarStart_Leave(object sender, EventArgs e)
        {
            if (_report.ReportNumber == (int)ReportIDs.FirearmsReport)
            {
                if (this.comboboxReportDetail.Text == "Gun Disposition")
                {
                    try
                    {
                        // Today
                        DateTime dtNow = DateTime.Now;
                        DateTime dtStart = DateTime.Parse(this.dateCalendarStart.DateText.DateTextBox.Text);
                        //Date dtSop = this.dateCalendarEnd.DateText;

                        if (dtStart < dtNow.AddYears(-1))
                        {
                            MessageBox.Show("This report is not available more than one year prior to the current date.", "Invalid Date", MessageBoxButtons.OK);
                            dateCalendarStart.Focus();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Invalid Date", MessageBoxButtons.OK);
                        dateCalendarStart.Focus();
                    }
                }
            }
        }

        private void dateCalendarStart_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dateCalendarStart_Leave(sender, e);
        }

        private void dateCalendarEnd_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            dateCalendarEnd_Leave(sender, e);
        }
    }

#if DEBUG
    //NOTE: Evaluate usefulness of commented code below and if it is necessary any longer
    //  used only to test DSTR report     
    //
    //private void customButton1_Click(object sender, EventArgs e)
    //{
    //    var confRef = PawnSecurityAccessor.Instance.EncryptConfig;
    //    var clientConfigDB = confRef.GetOracleDBService();

    //    //Print end of day reports
    //    var credentials = new PawnReports.DataAccessLayer.Credentials
    //    {
    //        UserName = confRef.DecryptValue(clientConfigDB.DbUser),
    //        PassWord = confRef.DecryptValue(clientConfigDB.DbUserPwd),
    //        DBHost = confRef.DecryptValue(clientConfigDB.Server),
    //        DBPort = confRef.DecryptValue(clientConfigDB.Port),
    //        DBService = confRef.DecryptValue(clientConfigDB.AuxInfo),
    //        DBSchema = confRef.DecryptValue(clientConfigDB.Schema)
    //    };
    //    //ExecuteDSTR(credentials, CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber);
    //    //this.handleEndFlow(null);

    //    //var dInitDate = Convert.ToDateTime(ShopDateTime.Instance.ShopDate.FormatDate());
    //    //var dInitDate = Convert.ToDateTime("03/26/2010");//for extensions, new, renewals & pickup
    //    //var dInitDate = Convert.ToDateTime("05/12/2010");//for paydowns, pulled for inventory, transfer out
    //    //var dInitDate = Convert.ToDateTime("03/22/2010");//for paydowns, pulled for inventory, transfer out
    //    //var dInitDate = Convert.ToDateTime("04/28/2010");//for new, pickup, pulled for inventory
    //    var dInitDate = Convert.ToDateTime("04/20/2010");//for release to claimant, pulled for inventory
    //    var stoNum = CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber;
    //    var oRptObj = new GetDstrData(dInitDate, stoNum, credentials);
    //    var dataSets = oRptObj.BuildDataset();
    //    var fileName = @"dstr_report_" + DateTime.Now.Ticks + ".pdf";
    //    const string rptDir = @"c:\tmp";
    //    var pdfRpt = new BuildDstrRpt(stoNum, dInitDate, dataSets, rptDir, fileName);
    //    pdfRpt.CreateRpt();
    //    CashlinxDesktopSession.ShowPDFFile(pdfRpt.ReportDir + @"\" + pdfRpt.RptFileName, false);

    //    dInitDate = Convert.ToDateTime("04/28/2010");//for new, pickup, pulled for inventory
    //    stoNum = CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber;
    //    oRptObj = new GetDstrData(dInitDate, stoNum, credentials);
    //    dataSets = oRptObj.BuildDataset();
    //    fileName = @"dstr_report_" + DateTime.Now.Ticks + ".pdf";
    //    pdfRpt = new BuildDstrRpt(stoNum, dInitDate, dataSets, rptDir, fileName);
    //    pdfRpt.CreateRpt();
    //    CashlinxDesktopSession.ShowPDFFile(pdfRpt.ReportDir + @"\" + pdfRpt.RptFileName, false);

    //    dInitDate = Convert.ToDateTime("03/22/2010");//for new, pickup, pulled for inventory
    //    stoNum = CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber;
    //    oRptObj = new GetDstrData(dInitDate, stoNum, credentials);
    //    dataSets = oRptObj.BuildDataset();
    //    fileName = @"dstr_report_" + DateTime.Now.Ticks + ".pdf";
    //    pdfRpt = new BuildDstrRpt(stoNum, dInitDate, dataSets, rptDir, fileName);
    //    pdfRpt.CreateRpt();
    //    CashlinxDesktopSession.ShowPDFFile(pdfRpt.ReportDir + @"\" + pdfRpt.RptFileName, false);

    //    dInitDate = Convert.ToDateTime("05/12/2010");//for new, pickup, pulled for inventory
    //    stoNum = CashlinxDesktopSession.Instance.CurrentSiteId.StoreNumber;
    //    oRptObj = new GetDstrData(dInitDate, stoNum, credentials);
    //    dataSets = oRptObj.BuildDataset();
    //    fileName = @"dstr_report_" + DateTime.Now.Ticks + ".pdf";
    //    pdfRpt = new BuildDstrRpt(stoNum, dInitDate, dataSets, rptDir, fileName);
    //    pdfRpt.CreateRpt();
    //    //if (pdfRpt.ErrorCode == OK && pdfRpt.ErrorText == OK)
    //    //{
    //        CashlinxDesktopSession.ShowPDFFile(pdfRpt.ReportDir + @"\" + pdfRpt.RptFileName, false);
    //    //}
    //    //else
    //    //{
    //    //    MessageBox.Show("Could not generate DSTR report: " + "Code: " +
    //    //        pdfRpt.ErrorCode + ", Reason: " + pdfRpt.ErrorText,
    //    //        "DSTR Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    //    //}
    //}
    //    }
#endif

    //used to populate the Report Name drop down box
    public class ReportTitle
    {
        public string Title { get; set; }

        public int Number { get; set; }

        public ReportTitle(int number, string title)
        {
            this.Title = title;
            this.Number = number;
        }
    }

    //used to populate the Sort Type drop down box
    public class SortType
    {
        public string Display { get; set; }

        public string Fields { get; set; }

        public SortType(string display, string fields)
        {
            this.Display = display;
            this.Fields = fields;
        }
    }

    //used to populate the Report Detail drop down box
    public class ReportDetail
    {
        public string Display { get; set; }

        public string Filter { get; set; }

        public ReportDetail(string display, string filter)
        {
            this.Display = display;
            this.Filter = filter;
        }
    }

    public class OtherComboItem
    {
        public string Display { get; set; }

        public string Filter { get; set; }

        public OtherComboItem(string display, string filter)
        {
            this.Display = display;
            this.Filter = filter;
        }
    }
}

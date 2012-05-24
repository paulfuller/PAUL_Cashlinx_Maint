using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Controllers.Rules.Interface.Impl;
using Common.Libraries.Objects;
using Common.Libraries.Objects.Business;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Pawn.Logic;

namespace PawnTests.TestEnvironment
{
    public class TestDesktopSession : DesktopSession
    {
        public override void GetCashDrawerAssignmentsForStore()
        {
            
        }

        public override void Setup(Form desktopForm)
        {
            GetPawnBusinessRules();
        }

        public override void UpdateDesktopCustomerInformation(Form form)
        {
            
        }

        public override void ShowDesktopCustomerInformation(Form form, bool b)
        {
            
        }

        public override void ClearCustomerList()
        {
            
        }

        public override void ClearPawnLoan()
        {
            
        }

        public override void ClearLoggedInUser()
        {
            
        }

        public override void ClearSessionData()
        {
            
        }

        public override void PerformAuthorization()
        {
            
        }

        public override void PerformAuthorization(bool chgUsrPasswd)
        {
            
        }

        public override bool IsButtonAvailable(string buttonTagName, SiteId currentSiteId)
        {
            return true;
        }

        public override bool IsButtonTellerOperation(string buttonTagName)
        {
            return true;
        }

        public override bool IsUserAssignedCashDrawer(string username, string cashdrawer, out string cashDrawerflag)
        {
            cashDrawerflag = string.Empty;
            return true;
        }

        public override void PrintTags(Item _Item, bool reprint)
        {
            
        }

        public override void PrintTags(Item _Item, CurrentContext context)
        {
            
        }

        public override void ScanParse(object sender, string sBarCodeData, TextBoxBase userNameControl, TextBoxBase passwordControl, ButtonBase submitBtn)
        {
            
        }

        public override bool WriteUsbData(string data)
        {
            return true;
        }

        public override void DeviceRemovedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e)
        {
            
        }

        public override void DeviceArrivedEventHandler(object sender, USBUtilities.DriveDetectorEventArgs e)
        {
            
        }

        public override void Dispose()
        {
            
        }

        public override void showProcessTender(ProcessTenderProcedures.ProcessTenderMode processTenderMode)
        {
            
        }

        public override void PerformCashDrawerChecks(out bool checkPassed)
        {
            checkPassed = true;
        }

        public override void CheckOpenCashDrawers(out bool openCD)
        {
            openCD = false;
        }

        public override void UpdateShopDate(Form fm)
        {
            
        }

        public override void GetPawnBusinessRules()
        {
            try
            {
                this.PawnRulesSys = new PawnRulesSystemImpl();
                var bRules = new List<String>
                {
                    "PWN_BR-000",
                    "PWN_BR-002",
                    "PWN_BR-003",
                    "PWN_BR-004", 
                    "PWN_BR-005",
                    "PWN_BR-007",
                    "PWN_BR-008",
                    "PWN_BR-010",
                    "PWN_BR-013",
                    "PWN_BR-014",
                    "PWN_BR-016",
                    "PWN_BR-017",
                    "PWN_BR-019",
                    "PWN_BR-020",
                    "PWN_BR-021",
                    "PWN_BR-022",
                    "PWN_BR-023",
                    "PWN_BR-024",
                    "PWN_BR-025",
                    "PWN_BR-026",
                    "PWN_BR-027",
                    "PWN_BR-028",
                    "PWN_BR-032",
                    "PWN_BR-033",
                    "PWN_BR-035",
                    "PWN_BR-038",
                    "PWN_BR-042",
                    "PWN_BR-043",
                    "PWN_BR-046",
                    "PWN_BR-047",
                    "PWN_BR-048",
                    "PWN_BR-051",
                    "PWN_BR-053", 
                    "PWN_BR-054",
                    "PWN_BR-057",
                    "PWN_BR-058",
                    "PWN_BR-059",
                    "PWN_BR-061",
                    "PWN_BR-064",
                    "PWN_BR-068",
                    "PWN_BR-069",
                    "PWN_BR-071",
                    "PWN_BR-074",
                    "PWN_BR-075",
                    "PWN_BR-077",
                    "PWN_BR-084", // TL 02-09-2010 Added for Wipe Drive Categories
                    "PWN_BR-089",
                    "PWN_BR-092",
                    "PWN_BR-094",
                    "PWN_BR-096",
                    "PWN_BR-097",
                    "PWN_BR-116",
                    "PWN_BR-117",
                    "PWN_BR-130",
                    "PWN_BR-133",
                    "PWN_BR-148",
                    "PWN_BR-141",
                    "PWN_BR-134",
                    "PWN_BR-142",
                    "PWN_BR-169",
                    "PWN_BR-171",
                    "PWN_BR-172",
                    "PWN_BR-175",
                    "PWN_BR-176",
                    "PWN_BR-179",
                    "PWN_BR_0002",
                    "PWN_BR_0003"
                    
                    

                };
                var pawnBusinessRuleVO = PawnBusinessRuleVO;
                PawnRulesSys.beginSite(this, this.CurrentSiteId, bRules, out pawnBusinessRuleVO);
                PawnRulesSys.getParameters(this, this.CurrentSiteId, ref pawnBusinessRuleVO);
                PawnRulesSys.endSite(this, this.CurrentSiteId);
                PawnBusinessRuleVO = pawnBusinessRuleVO;
                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "Finished loading business rules");
                }
            }
            catch (Exception exp)
            {
                //BasicExceptionHandler.Instance.AddException("GetPawnBusinessRulesFailed", new ApplicationException("Cannot execute the GetPawnBusinessRules during StartUp. [" + exp.Message + "]"));
                //throws exception, read it here so the stack doesnt go awry
                FileLogger.Instance.logMessage(LogLevel.FATAL, this, "Could not process business rules: " + exp);
                throw new ApplicationException("Business rules failure", exp);
            }
        }
    }
}

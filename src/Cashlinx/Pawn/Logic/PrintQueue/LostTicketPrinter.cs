using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Controllers.Application;
using Common.Controllers.Security;
using Common.Libraries.Objects.Pawn;
using Common.Libraries.Objects;
using Common.Libraries.Utility;
using Common.Libraries.Utility.ISharp;
using Common.Libraries.Utility.Logger;
using Common.Libraries.Utility.Shared;
using Reports;

namespace Pawn.Logic.PrintQueue
{
    public class LostTicketPrinter
    {
        public LostTicketPrinter(PawnLoan p, SiteId siteId)
        {
            PawnLoan = p;
            SiteId = siteId;
        }

        public SiteId SiteId { get; private set; }
        public PawnLoan PawnLoan { get; private set; }

        public void Print()
        {
            if (SiteId.State.Equals(States.Indiana))
            {
                var lostTicketAffidavitContext = new LostTicketAffidavitContext();
                var indianaLostTicketAffidavit = new IndianaLostTicketAffidavit(lostTicketAffidavitContext);
                var customer = GlobalDataAccessor.Instance.DesktopSession.ActiveCustomer;
                var address = customer.getHomeAddress();
                lostTicketAffidavitContext.CustomerName = customer.CustomerName;

                lostTicketAffidavitContext.StoreName = SiteId.StoreName;
                lostTicketAffidavitContext.StoreNumber = SiteId.StoreNumber;
                lostTicketAffidavitContext.LoanDateMade = PawnLoan.DateMade;
                lostTicketAffidavitContext.TicketNumber = PawnLoan.TicketNumber;
                lostTicketAffidavitContext.ReasonMissing = Commons.GetLostTicketType(PawnLoan.LostTicketInfo.LSDTicket);

                var descriptions = new StringBuilder();
                foreach (var item in PawnLoan.Items)
                {
                    descriptions.AppendLine(item.TicketDescription);
                }
                lostTicketAffidavitContext.MerchandiseDescription = descriptions.ToString();
                lostTicketAffidavitContext.OutputPath = SecurityAccessor.Instance.EncryptConfig.ClientConfig.GlobalConfiguration.BaseLogPath +
                                                                  "\\LostTicketAffidavit_" + DateTime.Now.ToString("MMddyyyyhhmmssFFFFFFF") + ".pdf";

                if (!indianaLostTicketAffidavit.Print())
                {
                    return;
                }

                if (!SecurityAccessor.Instance.EncryptConfig.ClientConfig.ClientConfiguration.PrintEnabled ||
                    !GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IsValid)
                {
                    return;
                }

                if (FileLogger.Instance.IsLogInfo)
                {
                    FileLogger.Instance.logMessage(LogLevel.INFO, this, "Printing Lost Ticket Affidavit on: {0}",
                                                   GlobalDataAccessor.Instance.DesktopSession.LaserPrinter);
                }

                var strReturnMessage =
                    PrintingUtilities.printDocument(
                        lostTicketAffidavitContext.OutputPath,
                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.IPAddress,
                        GlobalDataAccessor.Instance.DesktopSession.LaserPrinter.Port, 2);
                if (!strReturnMessage.Contains("SUCCESS"))
                    FileLogger.Instance.logMessage(LogLevel.ERROR, this, "Lost Ticket Affidavit : " + strReturnMessage);
            }
            else
            {
                var lstPrint = new LostTicketStatementPrint();
                lstPrint.Print(PawnLoan);
            }
        }
    }
}

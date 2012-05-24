using System;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Libraries.Utility.Exception;
using Common.Libraries.Utility.Logger;

namespace Common.Controllers.Application.ApplicationFlow.Impl.Common
{
    public abstract class CommonAppBlocksBase : MarshalByRefObject, IDisposable
    {
        public override object InitializeLifetimeService()
        {
            return null;
        }

        protected DesktopSession desktopSession;
        protected CommonAppBlocksBase(DesktopSession dSession)
        {
            desktopSession = dSession;
        }

        protected ShowForm createShowFormBlock(
            uint nm,
            Form pFm,
            Form fm)
        {
            if (fm == null)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CommonAppBlocksBase::ShowForm(nm,pFm,fm)", "Attempted to create ShowForm block with null form");
                }
                BasicExceptionHandler.Instance.AddException("CommonAppBlocksBase::ShowForm(nm,pFm,fm) failed", new Exception("CommonAppBlocksBase::ShowForm failed"));
                return (null);
            }
            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, "CommonAppBlocksBase::ShowForm(nm,pFm,fm)", "Creating ShowForm block for form {0}", fm.Name);
            }
            var sFm = new ShowForm(desktopSession, pFm, fm);
            return (sFm);
        }

        protected ShowForm createShowFormBlock(
            uint nm,
            Form pFm,
            Form fm,
            NavBox nvBox)
        {
            if (fm == null || nvBox == null)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CommonAppBlocksBase::ShowForm(nm,pFm,fm,nvBox)",
                                                   "Attempted to create ShowForm block with null form or null NavBox");
                }
                BasicExceptionHandler.Instance.AddException("CommonAppBlocksBase::ShowForm(nm,pFm,fm,nvBox) failed",
                                                            new Exception("CommonAppBlocksBase::ShowFormShowForm(nm,pFm,fm,nvBox) failed"));
                return (null);
            }

            if (FileLogger.Instance.IsLogDebug)
            {
                FileLogger.Instance.logMessage(LogLevel.DEBUG, "CommonAppBlocksBase::ShowForm(nm,pFm,fm)", "Creating ShowForm block for form {0}", fm.Name);
            }
            var sFm = new ShowForm(desktopSession, pFm, fm, nvBox);
            return (sFm);
        }

        protected ShowForm createShowFormBlock(
            uint nm,
            Form pFm,
            Form fm,
            NavBox nvBox,
            NavBox.NavBoxActionFired nvBoxDeleg)
        {
            if (fm == null || nvBox == null || nvBoxDeleg == null)
            {
                if (FileLogger.Instance.IsLogError)
                {
                    FileLogger.Instance.logMessage(LogLevel.ERROR, "CommonAppBlocksBase::ShowForm(nm,pFm,fm,nvBox,nvBoxDeleg)",
                                                   "Attempted to create ShowForm block with null form or null NavBox");
                }
                BasicExceptionHandler.Instance.AddException("CommonAppBlocksBase::ShowForm(nm,pFm,fm,nvBox,nvBoxDeleg) failed",
                                                            new Exception("CommonAppBlocksBase::ShowForm(nm,pFm,fm,nvBox,nvBoxDeleg) failed"));
            }

            var sFm = new ShowForm(desktopSession, pFm, fm, nvBox, nvBoxDeleg);
            return (sFm);
        }

        protected ShowForm createShowFormBlock(
             uint nm,
             Form pFm,
             Form fm,
             NavBox nvBox,
             NavBox.NavBoxActionFired nvBoxDeleg,
            bool showFormAsDialog)
        {
            if (fm == null)
            {
                return (null);
            }

            var sFm = new ShowForm(desktopSession, pFm, fm, nvBox, nvBoxDeleg, showFormAsDialog);
            return (sFm);
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            this.desktopSession = null;
        }

        #endregion
    }
}

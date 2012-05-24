using System;
using System.Windows.Forms;
using Common.Controllers.Application.ApplicationFlow.Blocks.Base;
using Common.Controllers.Application.ApplicationFlow.Navigation;
using Common.Controllers.Security;

namespace Common.Controllers.Application.ApplicationFlow.Impl.Common
{
    public class ShowForm : ActionBlock
    {
        public static readonly string NAME = "ShowForm";

        public DesktopSession DesktopSession { get; private set; }

        private Form form;
        private NavBox formNavBox;
        private bool hasValidNavBox;
        private bool showDialog;
        public bool Visible
        {
            get
            {
                return (this.form != null && this.form.Visible);
            }
        }

        public Form ClassForm
        {
            get
            {
                return (this.form);
            }
        }

        public bool HasValidNavBox
        {
            get
            {
                return (this.hasValidNavBox);
            }
        }

        private object displayForm(object data)
        {
            if (this.form == null)
                return (null);

            Form ownerForm = null;
            if (data != null)
            {
                ownerForm = (Form)data;
            }

            if (this.form.Visible == false)
            {
                DesktopSession.HistorySession.AddForm(this.form);
                if (ownerForm != null)
                {
                    if (showDialog)
                        form.ShowDialog(ownerForm);
                    else
                        form.Show(ownerForm);
                }
                else
                {
                    if (showDialog)
                        form.ShowDialog();
                    else
                        form.Show();
                }
            }
            return (true);
        }

        /// <summary>
        /// Show form with form's specified nav box and
        /// do not explicitly set the navbox onactionfired 
        /// delegate
        /// </summary>
        /// <param name="desktopSession"> </param>
        /// <param name="parentForm"> </param>
        /// <param name="fm"></param>
        /// <param name="fmNavBoxInst"></param>
        public ShowForm(
            DesktopSession desktopSession,
            Form parentForm,
            Form fm,
            NavBox fmNavBoxInst)
            : base(NAME)
        {
            if (fm == null)
            {
                throw new ApplicationException("Cannot create ShowForm block");
            }
            this.DesktopSession = desktopSession;
            this.form = fm;
            this.formNavBox = fmNavBoxInst;
            this.hasValidNavBox = true;
            this.Action = new FxnBlock();
            this.Action.InputParameter = parentForm;
            this.Action.Function = displayForm;
        }

        /// <summary>
        /// Show form with form's specified nav box and
        /// explicitly set the navbox onactionfired
        /// delegate
        /// </summary>
        /// <param name="parentForm"> </param>
        /// <param name="fm"></param>
        /// <param name="fmNavBoxInst"></param>
        /// <param name="navBoxDelegate"></param>
        /// <param name="desktopSession"> </param>
        public ShowForm(
            DesktopSession desktopSession,
            Form parentForm,
            Form fm,
            NavBox fmNavBoxInst,
            NavBox.NavBoxActionFired navBoxDelegate)
            : base(NAME)
        {
            if (fm == null)
            {
                throw new ApplicationException("Cannot create ShowForm block");
            }
            this.DesktopSession = desktopSession;
            this.form = fm;
            this.formNavBox = fmNavBoxInst;
            this.formNavBox.OnActionFire = navBoxDelegate;
            this.hasValidNavBox = true;
            this.Action = new FxnBlock();
            this.Action.InputParameter = parentForm;
            this.Action.Function = displayForm;
        }

        /// <summary>
        ///         Show form with form's specified nav box and
        /// explicitly set the navbox onactionfired
        /// delegate and whether to show the form as a dialog
        /// </summary>
        /// <param name="desktopSession"> </param>
        /// <param name="parentForm"></param>
        /// <param name="fm"></param>
        /// <param name="fmNavBoxInst"></param>
        /// <param name="navBoxDelegate"></param>
        /// <param name="showDialog"></param>
        public ShowForm(
            DesktopSession desktopSession,
            Form parentForm,
            Form fm,
            NavBox fmNavBoxInst,
            NavBox.NavBoxActionFired navBoxDelegate,
            bool showDialog)
            : base(NAME)
        {
            if (fm == null)
            {
                throw new ApplicationException("Cannot create ShowForm block");
            }
            this.DesktopSession = desktopSession;
            this.form = fm;
            this.formNavBox = fmNavBoxInst;
            this.formNavBox.OnActionFire = navBoxDelegate;
            this.hasValidNavBox = true;
            this.showDialog = showDialog;
            this.Action = new FxnBlock();
            this.Action.InputParameter = parentForm;
            this.Action.Function = displayForm;
        }

        /// <summary>
        /// Show form without regard to the
        /// potential existence of a nav block
        /// or its delegate
        /// </summary>
        /// <param name="parentForm"> </param>
        /// <param name="fm"></param>
        /// <param name="desktopSession"> </param>
        public ShowForm(
            DesktopSession desktopSession,
            Form parentForm,
            Form fm)
            : base(NAME)
        {
            if (fm == null)
            {
                throw new ApplicationException("Cannot create ShowForm block");
            }
            this.DesktopSession = desktopSession;
            this.form = fm;
            this.hasValidNavBox = false;
            this.Action = new FxnBlock();
            this.Action.InputParameter = parentForm;
            this.Action.Function = displayForm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentForm"></param>
        public void setParentForm(Form parentForm)
        {
            this.Action.InputParameter = parentForm;
        }
    }
}

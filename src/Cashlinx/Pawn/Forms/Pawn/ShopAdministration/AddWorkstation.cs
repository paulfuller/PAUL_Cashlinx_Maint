using System;
using System.Windows.Forms;
using Common.Controllers.Application;
using Common.Controllers.Database.Procedures;
using Common.Libraries.Forms.Components;

namespace Pawn.Forms.Pawn.ShopAdministration
{
    public partial class AddWorkstation : CustomBaseForm
    {
        public AddWorkstation()
        {
            InitializeComponent();
        }

        private void customButtonSubmit_Click(object sender, EventArgs e)
        {
            if (!(customTextBoxWorkstationName.isValid))
            {
                MessageBox.Show("Please enter a name for the workstation");
                return;
            }
            string errorCode;
            string errorMesg;
            bool retval = ShopCashProcedures.AddWorkstation(customTextBoxWorkstationName.Text,
                GlobalDataAccessor.Instance.CurrentSiteId.StoreNumber, GlobalDataAccessor.Instance.DesktopSession,
                out errorCode, out errorMesg);
            if (retval)
                MessageBox.Show("Workstation successfully added");
            else
                MessageBox.Show("Error adding workstation " + errorMesg);
            this.Close();

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Libraries.Utility;

namespace Pawn.Forms.UserControls
{
    public partial class GunOwner : UserControl
    {
        public GunOwner()
        {
            InitializeComponent();
        }

        public string StartDate { get { return dateStart.X<string>(); } }
        public string EndDate { get { return dateEnd.X<string>(); } }
        public string LastName { get { return txtLastName.X<string>(); } }
        public string FirstName { get { return txtFirstName.X<string>(); } }
        public string CustomerNumber { get { return txtCustomerNumber.X<string>(); } }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PawnStoreSetupTool
{
    public partial class DataRequestForm : Form
    {
        private bool isLoaded;
        private object dataObject;
        public object DataObject
        {
            set
            {
                this.refreshGridWithObject(value);
            }

            get
            {
                return (this.dataObject);
            }
        }
        private object replacedDataObject;
        public object ReplacedDataObject
        {
            get
            {
                return (this.replacedDataObject);
            }
        }


        public DataRequestForm()
        {
            InitializeComponent();
            this.replacedDataObject = null;
            this.dataObject = null;
            this.isLoaded = false;
        }

        private void DataRequestForm_Load(object sender, EventArgs e)
        {
            this.isLoaded = true;
            if (this.dataObject != null)
            {
                this.refreshGridWithObject(this.dataObject);
            }
        }

        private void refreshGridWithObject(object newObj)
        {
            if (newObj == null) return;
            this.replacedDataObject = this.dataObject;
            this.dataObject = newObj;
            if (!this.isLoaded)return;

            this.dataPropertyGrid.SelectedObject = this.dataObject;
            this.dataPropertyGrid.Update();            
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}

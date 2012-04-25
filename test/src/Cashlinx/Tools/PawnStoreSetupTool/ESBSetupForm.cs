using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Common.Libraries.Utility.Collection;
using Common.Libraries.Utility.Type;

namespace PawnStoreSetupTool
{
    public partial class ESBSetupForm : Form
    {
        private const string ADDSERVICE_BUTTONTEXT = "Add Service";

        public enum REQINFOFLAGS
        {
            TYPE = 0x01,
            HOST = 0x02,
            PORT = 0x04,
            EURI = 0x10,
            DOMA = 0x20,
            ENPO = 0x40
        }

        public enum ESBTYPE
        {
            ADDRESS = 0,
            PROKNOW = 1,
            MDSETRANS = 2
        };

        public static readonly string[] ESBTYPES = 
            new[]
            {
                "Address Service", "ProKnow Service", "MDSE Transfer Service"
            };

        private List<PairType<ulong, QuadType<string, string, string, TupleType<string, string, string>>>> esbServices;
        public List<PairType<ulong, QuadType<string, string, string, TupleType<string, string,string>>>> EsbServices
        { 
            get
            {
                return (this.esbServices);
            }
        }

        private string tempEsbType;
        private string tempEsbHost;
        private string tempEsbPort;
        private string tempEsbURI;
        private string tempEsbDomain;
        private string tempEsbEndPointName;
        private uint infoFlag;
        private bool editServiceMode;

        public ESBSetupForm(List<PairType<ulong, QuadType<string, string, string, TupleType<string, string, string>>>> exEsbServices)
        {
            this.esbServices = new List<PairType<ulong, QuadType<string, string, string, TupleType<string, string, string>>>>();
            this.editServiceMode = false;
            if (CollectionUtilities.isNotEmpty(exEsbServices))
            {
                foreach(var exEsb in exEsbServices)
                {
                    if (exEsb == null) continue;
                    this.esbServices.Add(exEsb);
                }
            }
            InitializeComponent();
        }      
  

        private void ESBSetupForm_Load(object sender, EventArgs e)
        {
            this.esbServiceComboBox.BeginUpdate();
            this.esbServiceComboBox.Items.Clear();
            foreach(var str in ESBTYPES)
            {
                this.esbServiceComboBox.Items.Add(str);
            }
            this.esbServiceComboBox.EndUpdate();
            this.syncExistingEsbServices();
        }

        private void esbServiceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sIdx = this.esbServiceComboBox.SelectedIndex;
            if (sIdx >= 0 && sIdx < ESBTYPES.Length)
            {
                this.tempEsbType = ESBTYPES[sIdx];
                this.infoFlag |= (uint)REQINFOFLAGS.TYPE;
                this.hostNameTextBox.Enabled = true;
            }
        }

        private void syncExistingEsbServices()
        {
            if (CollectionUtilities.isEmpty(this.esbServices)) return;

            //Create list and populate with esb services list
            this.existingEsbServicesListBox.BeginUpdate();
            this.existingEsbServicesListBox.Items.Clear();
            //Repopulate list
            foreach (var esbObj in this.esbServices)
            {
                if (esbObj == null) continue;
                var pTypeId = esbObj.Left;
                var esb = esbObj.Right;
                if (esb == null)
                    continue;
                var lItemRow = new ListViewItem(esb.X);
                //lItemRow.SubItems.Add(esb.X);
                lItemRow.SubItems.Add(esb.Y);
                lItemRow.SubItems.Add(esb.Z);
                lItemRow.SubItems.Add(esb.W.Left);
                lItemRow.SubItems.Add(esb.W.Mid);
                lItemRow.SubItems.Add(esb.W.Right);                
                this.existingEsbServicesListBox.Items.Add(lItemRow);
            }
            this.existingEsbServicesListBox.EndUpdate();
        }

        private void hostNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.hostNameTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.hostNameLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.HOST;
                return;
            }
            this.tempEsbHost = dat;
            infoFlag |= (uint)REQINFOFLAGS.HOST;
            PawnStoreSetupForm.CheckmarkLabel(true, this.hostNameLabel);
            this.portTextBox.Enabled = true;

        }

        private void portTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.portTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.portLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.PORT;
                return;
            }
            this.tempEsbPort = dat;
            infoFlag |= (uint)REQINFOFLAGS.PORT;
            PawnStoreSetupForm.CheckmarkLabel(true, this.portLabel);
            this.domainTextBox.Enabled = true;
        }

        private void domainTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.domainTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.domainLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.DOMA;
                return;
            }
            this.tempEsbDomain = dat;
            infoFlag |= (uint)REQINFOFLAGS.DOMA;
            PawnStoreSetupForm.CheckmarkLabel(true, this.domainLabel);
            this.endPointURITextBox.Enabled = true;
        }

        private void endPointURITextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.endPointURITextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.endPointURILabel);
                infoFlag &= ~(uint)REQINFOFLAGS.EURI;
                return;
            }
            this.tempEsbURI = dat;
            infoFlag |= (uint)REQINFOFLAGS.EURI;
            PawnStoreSetupForm.CheckmarkLabel(true, this.endPointURILabel);
            this.endPointNameTextBox.Enabled = true;
        }

        private void endPointNameTextBox_TextChanged(object sender, EventArgs e)
        {
            string dat;
            if (!PawnStoreSetupForm.GetTextBoxData(this.endPointNameTextBox, null, out dat))
            {
                PawnStoreSetupForm.CheckmarkLabel(false, this.endPointNameLabel);
                infoFlag &= ~(uint)REQINFOFLAGS.ENPO;
                return;
            }
            this.tempEsbEndPointName = dat;
            infoFlag |= (uint)REQINFOFLAGS.ENPO;
            PawnStoreSetupForm.CheckmarkLabel(true, this.endPointNameLabel);
            this.addServiceButton.Enabled = true;
        }

        private void addServiceButton_Click(object sender, EventArgs e)
        {
            if (this.editServiceMode)
            {
                //Edit entry list
                foreach(var esbObj in this.esbServices)
                {
                    if (esbObj == null)
                        continue;
                    var pTypeId = esbObj.Left;
                    var esb = esbObj.Right;
                    if (esb == null)
                        continue;
                    if (esb.X.Equals(this.tempEsbType, StringComparison.OrdinalIgnoreCase))
                    {
                        esb.Y = this.tempEsbHost;
                        esb.Z = this.tempEsbPort;
                        esb.W = new TupleType<string,string,string>(this.tempEsbDomain, this.tempEsbURI, this.tempEsbEndPointName);
                        break;
                    }
                }

                //Reset edit service mode
                this.enableEditMode(false);
            }
            else
            {
                //Add to list
                this.esbServices.Add(new PairType<ulong, QuadType<string, string, string, TupleType<string, string, string>>>(
                    (ulong)this.esbServices.Count, new QuadType<string,string,string,TupleType<string, string,string>>( 
                    this.tempEsbType,
                    this.tempEsbHost,
                    this.tempEsbPort,
                    new TupleType<string, string, string>(this.tempEsbDomain, this.tempEsbURI, this.tempEsbEndPointName))));
            }
            this.syncExistingEsbServices();
        }


        private void enableEditMode(bool enabled)
        {
            if (this.editServiceMode == enabled) return;
            var curValue = this.editServiceMode;
            //Enabling Edit Mode
            if (enabled && !curValue)
            {
                this.addServiceButton.Enabled = true;
                this.addServiceButton.Text = "Edit Done";                
                this.addServiceButton.Update();
                this.existingEsbServicesListBox.Enabled = false;
                this.existingEsbServicesListBox.Update();
                this.editServiceButton.Enabled = false;
                this.editServiceButton.Update();
            }
            //Disabling Edit Mode
            else if (!enabled && curValue)
            {
                this.addServiceButton.Text = ADDSERVICE_BUTTONTEXT;
                this.addServiceButton.Update();
                this.existingEsbServicesListBox.Enabled = true;
                this.existingEsbServicesListBox.Update();
                this.editServiceButton.Enabled = true;
                this.editServiceButton.Update();
            }
            this.editServiceMode = enabled;
            
        }

        private void editServiceButton_Click(object sender, EventArgs e)
        {
            this.enableEditMode(!this.editServiceMode);
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void existingEsbServicesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!this.editServiceMode &&
                this.existingEsbServicesListBox.SelectedItems != null &&
                this.existingEsbServicesListBox.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection sItems =
                        this.existingEsbServicesListBox.SelectedItems;
                ListViewItem sItem = sItems[0];
                this.editServiceButton.Enabled = true;
                this.editServiceButton.Update();

                //Extract the info
                this.tempEsbType   = sItem.SubItems[0].Text;
                this.tempEsbHost   = sItem.SubItems[1].Text;
                this.tempEsbPort   = sItem.SubItems[2].Text;
                this.tempEsbDomain = sItem.SubItems[3].Text;
                this.tempEsbEndPointName = sItem.SubItems[4].Text;
                this.tempEsbURI    = sItem.SubItems[5].Text;

                //Set the fields
                this.esbServiceComboBox.SelectedIndex =
                        this.esbServiceComboBox.Items.IndexOf(this.tempEsbType);
                this.hostNameTextBox.Text = tempEsbHost;
                this.portTextBox.Text = tempEsbPort;
                this.endPointURITextBox.Text = tempEsbURI;
                this.domainTextBox.Text = tempEsbDomain;
                this.endPointNameTextBox.Text = tempEsbEndPointName;

                //disable the add button
                this.addServiceButton.Enabled = false;
                this.addServiceButton.Update();
            }
            else
            {
                this.editServiceButton.Enabled = false;
                this.editServiceButton.Update();
                this.addServiceButton.Enabled = true;
                this.addServiceButton.Update();
            }
        }


    }
}

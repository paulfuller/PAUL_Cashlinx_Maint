using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Windows.Forms;
using ChangeStoreNumber.Connections;
using System.ComponentModel;
using ChangeStoreNumber.Properties;
using System.Linq;

namespace ChangeStoreNumber
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private List<AppVersion> Applications { get; set; }
        private List<ClientRegistry> ClientRegistries { get; set; }
        private Conversion Conversion
        {
            get { return Conversion.GetInstance(); }
        }
        private Dictionary<string, Store> Stores { get; set; }
        private List<StoreSiteInfo> StoreSites { get; set; }

        private bool PreventDatabaseSelectedProcessing { get; set; }

        Processing processing;

        private void btnChangeStore_Click(object sender, EventArgs e)
        {
            AppVersion appVersion = ddlApplication.SelectedItem as AppVersion;
            ClientRegistry cr = ddlClientRegistries.SelectedItem as ClientRegistry;
            StoreSiteInfo si = ddlStoreSites.SelectedItem as StoreSiteInfo;
            Store store = Stores.ContainsKey(si.StoreNumber) ? Stores[si.StoreNumber] : null;

            if (appVersion == null || cr == null || si == null || store == null)
            {
                MessageBox.Show("Cannot change the store.");
                return;
            }

            if (string.IsNullOrEmpty(cr.MachineName))
            {
                MessageBox.Show("Invalid machine name");
                return;
            }

            if (Db.ChangeStore(cr, si, store, appVersion))
            {
                MessageBox.Show("Store Changed", "Change Store", MessageBoxButtons.OK);
                UpdateDatabaseSelected();
            }
            else
            {
                MessageBox.Show("Error while changing store.");
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = LoadInformation();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ChangeDatabaseResult result = e.Result as ChangeDatabaseResult;
            if (result.HasError)
            {
                processing.Close();
                MessageBox.Show(result.ErrorMessage);

                int index = ConnectionFactory.GetInstance().Connections.FindIndex(c => c.GetType().Equals(typeof(BlankConnection)));

                if (index >= 0)
                {
                    ddlDatabase.SelectedIndex = index;
                }
                else
                {
                    Application.Exit();
                }

                return;
            }

            if (!ConnectionFactory.GetInstance().IsDatabaseSelected())
            {
                ddlApplication.DataSource = null;
                ddlClientRegistries.DataSource = null;
                ddlStoreSites.DataSource = null;
                txtClientId.Text = string.Empty;
                txtStoreId.Text = string.Empty;
                processing.Close();
                return;
            }

            ddlApplication.DataSource = Applications;

            processing.Close();
        }

        private void ddlClientRegistries_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClientRegistry cr = ddlClientRegistries.SelectedItem as ClientRegistry;

            if (cr == null)
            {
                return;
            }

            txtClientId.Text = cr.Id.ToString();

            StoreSiteInfo si = StoreSites.Find(s => s.Id.Equals(cr.CurrentStoreId));

            if (si != null)
            {
                ddlStoreSites.SelectedItem = si;
            }
        }

        private void ddlStoreSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            StoreSiteInfo si = ddlStoreSites.SelectedItem as StoreSiteInfo;

            if (si == null)
            {
                return;
            }

            if (Stores.ContainsKey(si.StoreNumber))
            {
                txtStoreId.Text = Stores[si.StoreNumber].StoreId;
            }
        }

        private void ddlDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            DatabaseConnection connection = ddlDatabase.SelectedItem as DatabaseConnection;

            ConnectionFactory.GetInstance().ActiveConnection = connection;

            btnChangeStore.Enabled = ConnectionFactory.GetInstance().IsDatabaseSelected();
            UpdateDatabaseSelected();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.ApplicationExitCall)
            {
                Settings.Default.LastDatabase = ConnectionFactory.GetInstance().ActiveConnection.DisplayName;
                Settings.Default.Save();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if (Settings.Default == null)
            {
                Settings.Default.LastDatabase = string.Empty;
                Settings.Default.Save();
            }

            PreventDatabaseSelectedProcessing = true;
            ddlDatabase.DisplayMember = "DisplayName";
            ddlDatabase.DataSource = ConnectionFactory.GetInstance().Connections;

            int index = ConnectionFactory.GetInstance().Connections.FindIndex(c => c.DisplayName == Settings.Default.LastDatabase);

            if (index >= 0)
            {
                ddlDatabase.SelectedIndex = index;
            }

            PreventDatabaseSelectedProcessing = false;

            UpdateDatabaseSelected();
        }

        # region Helper Methods

        private ChangeDatabaseResult LoadInformation()
        {
            ChangeDatabaseResult result = new ChangeDatabaseResult();
            if (!ConnectionFactory.GetInstance().IsDatabaseSelected())
            {
                return result;
            }

            try
            {
                Applications = Db.GetAppVersions();
                ClientRegistries = Db.GetClientRegistries();
                StoreSites = Db.GetStoreSites();
                Stores = Db.GetStores();
                result.HasError = false;
            }
            catch (Exception exc)
            {
                result.HasError = true;
                result.ErrorMessage = exc.Message;
            }

            return result;
        }

        private void UpdateDatabaseSelected()
        {
            bool databaseSelected = ConnectionFactory.GetInstance().IsDatabaseSelected();
            ddlApplication.Enabled = databaseSelected;
            ddlClientRegistries.Enabled = databaseSelected;
            ddlStoreSites.Enabled = databaseSelected;

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            processing = new Processing();
            bw.RunWorkerAsync(processing);
            processing.ShowDialog(this);
        }

        # endregion

        private void ddlApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppVersion app = ddlApplication.SelectedItem as AppVersion;

            if (app == null)
            {
                return;
            }

            txtClientId.Text = string.Empty;
            txtStoreId.Text = string.Empty;

            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            string fqdn = string.Format("{0}.{1}", ipProperties.HostName, ipProperties.DomainName);

            List<int> availableStoreSitesForApp = (from ssi in StoreSites
                                                             where ssi.AppVersionId == app.Id
                                                             select ssi.Id).ToList();
            List<ClientRegistry> clientRegistriesForApp = ClientRegistries.FindAll(cr1 => availableStoreSitesForApp.Contains(cr1.CurrentStoreId)).ToList();
            ddlClientRegistries.DataSource = clientRegistriesForApp;
            ddlStoreSites.DataSource = StoreSites.FindAll(s => availableStoreSitesForApp.Contains(s.Id));

            ClientRegistry cr = clientRegistriesForApp.Find(r => r.FullMachineName.Equals(fqdn, StringComparison.InvariantCultureIgnoreCase));
            if (cr != null)
            {
                ddlClientRegistries.SelectedItem = cr;
            }
        }
    }
}

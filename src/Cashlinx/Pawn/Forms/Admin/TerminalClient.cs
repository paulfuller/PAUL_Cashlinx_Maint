/*************************************************************************************
 * Namespace:       CashlinxDesktop.DesktopForms.Admin
 * Class:           Terminal Client
 * 
 * Description      Creates a Client Terminal Emulator
 * 
 * History
 * David D Wise, Initial Development
 * 
 *************************************************************************************/

using System;
using System.Windows.Forms;
using CashlinxDesktop.UserControls;
using Pawn.Forms.UserControls;

namespace Pawn.Forms.Admin
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class TerminalClient : System.Windows.Forms.Form
    {
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonTelnet;
		private System.Windows.Forms.RadioButton radioButtonSSH;
		private System.Windows.Forms.TextBox textBoxHostname;
		private System.Windows.Forms.TextBox textBoxUsername;
		private System.Windows.Forms.TextBox textBoxPassword;
		private System.Windows.Forms.Label labelUsername;
		private System.Windows.Forms.Label labelPassword;
		private System.Windows.Forms.Label labelHostname;
        private System.Windows.Forms.Button buttonConnect;
        private Label label3;
        private Button btnLogin;
        private Button btnClose;
        private TerminalEmulator terminalEmulator1;
        /// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public TerminalClient()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            btnLogin.Enabled = false;
            //
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TerminalClient));
            this.textBoxHostname = new System.Windows.Forms.TextBox();
            this.labelHostname = new System.Windows.Forms.Label();
            this.labelUsername = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.radioButtonSSH = new System.Windows.Forms.RadioButton();
            this.btnLogin = new System.Windows.Forms.Button();
            this.radioButtonTelnet = new System.Windows.Forms.RadioButton();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.terminalEmulator1 = new TerminalEmulator();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxHostname
            // 
            this.textBoxHostname.Location = new System.Drawing.Point(5, 41);
            this.textBoxHostname.Name = "textBoxHostname";
            this.textBoxHostname.Size = new System.Drawing.Size(93, 20);
            this.textBoxHostname.TabIndex = 1;
            this.textBoxHostname.Text = "172.21.16.5";
            // 
            // labelHostname
            // 
            this.labelHostname.Location = new System.Drawing.Point(6, 22);
            this.labelHostname.Name = "labelHostname";
            this.labelHostname.Size = new System.Drawing.Size(93, 16);
            this.labelHostname.TabIndex = 2;
            this.labelHostname.Text = "Hostname";
            // 
            // labelUsername
            // 
            this.labelUsername.Location = new System.Drawing.Point(6, 106);
            this.labelUsername.Name = "labelUsername";
            this.labelUsername.Size = new System.Drawing.Size(93, 13);
            this.labelUsername.TabIndex = 4;
            this.labelUsername.Text = "Username";
            this.labelUsername.Visible = false;
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(6, 122);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(93, 20);
            this.textBoxUsername.TabIndex = 3;
            this.textBoxUsername.Text = "root";
            this.textBoxUsername.Visible = false;
            // 
            // labelPassword
            // 
            this.labelPassword.Location = new System.Drawing.Point(7, 145);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(92, 13);
            this.labelPassword.TabIndex = 6;
            this.labelPassword.Text = "Password";
            this.labelPassword.Visible = false;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(7, 161);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(92, 20);
            this.textBoxPassword.TabIndex = 5;
            this.textBoxPassword.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.radioButtonSSH);
            this.groupBox1.Controls.Add(this.labelPassword);
            this.groupBox1.Controls.Add(this.textBoxPassword);
            this.groupBox1.Controls.Add(this.textBoxUsername);
            this.groupBox1.Controls.Add(this.buttonConnect);
            this.groupBox1.Controls.Add(this.radioButtonTelnet);
            this.groupBox1.Controls.Add(this.textBoxHostname);
            this.groupBox1.Controls.Add(this.labelUsername);
            this.groupBox1.Controls.Add(this.labelHostname);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Location = new System.Drawing.Point(603, 128);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(105, 274);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connection";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(15, 229);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 41);
            this.btnClose.TabIndex = 139;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // radioButtonSSH
            // 
            this.radioButtonSSH.Location = new System.Drawing.Point(15, 84);
            this.radioButtonSSH.Name = "radioButtonSSH";
            this.radioButtonSSH.Size = new System.Drawing.Size(60, 24);
            this.radioButtonSSH.TabIndex = 9;
            this.radioButtonSSH.Text = "SSH";
            this.radioButtonSSH.CheckedChanged += new System.EventHandler(this.radioButtonCheck);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogin.BackgroundImage")));
            this.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(618, 446);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 41);
            this.btnLogin.TabIndex = 138;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Visible = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // radioButtonTelnet
            // 
            this.radioButtonTelnet.Checked = true;
            this.radioButtonTelnet.Location = new System.Drawing.Point(15, 63);
            this.radioButtonTelnet.Name = "radioButtonTelnet";
            this.radioButtonTelnet.Size = new System.Drawing.Size(67, 24);
            this.radioButtonTelnet.TabIndex = 8;
            this.radioButtonTelnet.TabStop = true;
            this.radioButtonTelnet.Text = "Telnet";
            this.radioButtonTelnet.CheckedChanged += new System.EventHandler(this.radioButtonCheck);
            // 
            // buttonConnect
            // 
            this.buttonConnect.BackColor = System.Drawing.Color.Transparent;
            this.buttonConnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonConnect.BackgroundImage")));
            this.buttonConnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonConnect.FlatAppearance.BorderSize = 0;
            this.buttonConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.buttonConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.buttonConnect.ForeColor = System.Drawing.Color.White;
            this.buttonConnect.Location = new System.Drawing.Point(15, 185);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 41);
            this.buttonConnect.TabIndex = 8;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(280, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 16);
            this.label3.TabIndex = 137;
            this.label3.Text = "Terminal Client";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // terminalEmulator1
            // 
            this.terminalEmulator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160)))));
            this.terminalEmulator1.Columns = 80;
            this.terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.Telnet;
            this.terminalEmulator1.DataEventNotify = false;
            this.terminalEmulator1.Font = new System.Drawing.Font("Courier New", 8F);
            this.terminalEmulator1.Hostname = null;
            this.terminalEmulator1.Location = new System.Drawing.Point(13, 117);
            this.terminalEmulator1.Name = "terminalEmulator1";
            this.terminalEmulator1.Password = null;
            this.terminalEmulator1.Rows = 26;
            this.terminalEmulator1.Size = new System.Drawing.Size(587, 370);
            this.terminalEmulator1.TabIndex = 138;
            this.terminalEmulator1.Username = null;
            this.terminalEmulator1.RawDataEvent += new TerminalEmulator.RawDataEventHandler(this.terminalEmulator1_RawDataEvent);
            this.terminalEmulator1.Resize += new System.EventHandler(this.TerminalControlTest_Resize);
            this.terminalEmulator1.CleanDataEvent += new TerminalEmulator.RawDataEventHandler(this.terminalEmulator1_CleanDataEvent);
            // 
            // TerminalClient
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackgroundImage = Common.Properties.Resources.newDialog_320_BlueScale;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(722, 510);
            this.Controls.Add(this.terminalEmulator1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLogin);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TerminalClient";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.TerminalClient_Load);
            this.Resize += new System.EventHandler(this.TerminalControlTest_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void radioButtonCheck(object sender, System.EventArgs e)
		{
			if (this.radioButtonSSH.Checked)
			{
				this.labelPassword.Visible = true;
				this.textBoxPassword.Visible = true;

				this.labelUsername.Visible = true;
				this.textBoxUsername.Visible = true;				
			}

			if (this.radioButtonTelnet.Checked)
			{
				this.labelPassword.Visible = false;
				this.textBoxPassword.Visible = false;

				this.labelUsername.Visible = false;
				this.textBoxUsername.Visible = false;
			}
		}

		private void buttonConnect_Click(object sender, System.EventArgs e)
		{
            //string strAlpha = "";
            //for (int i = 0; i <= 90; i++) // Loop through the ASCII characters 65 to 90
            //{
            //    strAlpha += ((char)i).ToString() + " ";
            //}

		    this.terminalEmulator1.Focus();
            if (this.radioButtonTelnet.Checked)
            {
                this.terminalEmulator1.Hostname = this.textBoxHostname.Text;
                this.terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.Telnet;
                btnLogin.Enabled = this.terminalEmulator1.Connect();
            }
            else if (this.radioButtonSSH.Checked)
            {
                this.terminalEmulator1.Hostname = this.textBoxHostname.Text;
                this.terminalEmulator1.Username = this.textBoxUsername.Text;
                this.terminalEmulator1.Password = this.textBoxPassword.Text;

                this.terminalEmulator1.ConnectionType = TerminalEmulator.ConnectionTypes.SSH2;
                btnLogin.Enabled = this.terminalEmulator1.Connect();
            }
		}

		private void TerminalControlTest_Resize(object sender, System.EventArgs e)
		{
            //this.Text = Convert.ToString(this.terminalEmulator1.Rows) + " Rows. " + Convert.ToString(this.terminalEmulator1.Columns) + " Columns.";
		}

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //this.terminalEmulator1.Focus();
            SendKeys.Send("root\n");
            Application.DoEvents();
            SendKeys.Send("cash\n");
            Application.DoEvents();
            SendKeys.Send("\n");
            Application.DoEvents();
            SendKeys.Send("tops\n");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private delegate void UpdateRawDataField(string sData);
        void terminalEmulator1_RawDataEvent(string sRawData)
        {
            if (InvokeRequired)
            {
                UpdateRawDataField updateRawDataFile = new UpdateRawDataField(terminalEmulator1_RawDataEvent);
                Invoke(updateRawDataFile, sRawData);
            }
            else
            {
                //if (sRawData == "CLEAR")
                //    txtRawData.Text = "";
                //else
                //    txtRawData.Text += sRawData;
            }
        }
        private delegate void UpdateCleanDataField(string sData);
        void terminalEmulator1_CleanDataEvent(string sRawData)
        {
            if (InvokeRequired)
            {
                UpdateCleanDataField updateCleanDataFile = new UpdateCleanDataField(terminalEmulator1_CleanDataEvent);
                Invoke(updateCleanDataFile, sRawData);
            }
            else
            {
                //if (sRawData == "CLEAR")
                //    txtCleanData.Text = "";
                //else
                //    txtCleanData.Text += sRawData;
            }
        }

        private void TerminalClient_Load(object sender, EventArgs e)
        {
            //Hide();

            //string fullName = Properties.Settings.Default.TerminalEmulatorPath;

            //Process procHandle = new Process();
            //procHandle.StartInfo.FileName = fullName;
            //procHandle.StartInfo.CreateNoWindow = false;
            //try
            //{
            //    procHandle.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            //    procHandle.Start();
            //    procHandle.WaitForExit();
            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show("Error opening Power Term: " + exp.Message);
            //}
            //Close();
        }
	}
}
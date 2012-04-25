using System;
using System.Drawing;
using System.Windows.Forms;
using Common.Libraries.Utility.String;


namespace EncryptionMaster
{
    public partial class EncryptionMaster : Form
    {
        private const string HEX_CODE = "x";
        private readonly string _pKey;
        private string _publicKey;
        private string _publicKeyEncrypted;
        private string _masterKey;
        private readonly Font _wingdingsFont;
        private readonly Font _tahomaFont;
 
        public EncryptionMaster()
        {
            InitializeComponent();
            _pKey = Common.Properties.Resources.PrivateKey;
            _publicKey = String.Empty;
            _publicKeyEncrypted = String.Empty;

            _wingdingsFont = new Font("Wingdings", 8.25f);
            _tahomaFont = new Font("Tahoma", 8.25f);
        }

        /* ---------------------------------------
         * 
         * Private Methods
         * 
         * ---------------------------------------
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <param name="useHashing"></param>
        /// <returns></returns>
        private string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            return (StringUtilities.Encrypt(toEncrypt, key, useHashing));
/*            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            var cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);*/
        }

        public string Decrypt(string toDecrypt, string key, bool useHashing)
        {
            return (StringUtilities.Decrypt(toDecrypt, key, useHashing));
/*            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);*/
        }

        private string CreatePublicKey(string keySeed)
        {
            /*var hashmd5 = new MD5CryptoServiceProvider();
            var keyArray = hashmd5.ComputeHash(Encoding.ASCII.GetBytes(keySeed));
            var sb = new StringBuilder();

            foreach (byte b in keyArray)
            {
                var s = b.ToString(HEX_CODE);
                sb.Append(s);
            }

            return (sb.ToString());*/
            //Call md5 hash generator with encoding default
            return (StringUtilities.GenerateMD5Hash(keySeed));
        }

        /* ---------------------------------------
         * 
         * Event Handlers
         * 
         * ---------------------------------------
         */
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateKeyButton_Click(object sender, EventArgs e)
        {
            // check to make sure that all required fields are filled
            if ((publicSeedTextBox.Text.Length > 0) &&
                (schemaTextBox.Text.Length > 0) &&
                (serviceTextBox.Text.Length > 0) &&
                (hostTextBox.Text.Length > 0) &&
                (portTextBox.Text.Length > 0) &&
                (userIdTextBox.Text.Length > 0) &&
                (passwordTextBox.Text.Length > 0))
            {

                // Create public key and encrypted public key
                _publicKey = CreatePublicKey(publicSeedTextBox.Text);
                _publicKeyEncrypted = Encrypt(_publicKey, _pKey, true);

                // disable the text boxes
                publicSeedTextBox.Enabled = false;
                schemaTextBox.Enabled = false;
                serviceTextBox.Enabled = false;
                hostTextBox.Enabled = false;
                portTextBox.Enabled = false;
                userIdTextBox.Enabled = false;
                passwordTextBox.Enabled = false;

                // disable the generate key button
                generateKeyButton.Enabled = false;

                // enable the warning message
                saveWarningLabel.Visible = true;

                // set the keys into the text box
                publicKeyTextBox.Text = _publicKey;
                encryptedKeyTextBox.Text = _publicKeyEncrypted;

                // generate the master (public + private)
                _masterKey = _publicKey + _pKey;

                // enable the add button
                addButton.Enabled = true;

                // add pertinent data to nameValue textbox
                string publicSeed = publicSeedTextBox.Text;

                // encrypt PAWNSEC-related data and display it as well
                var schemaEnc = Encrypt(schemaTextBox.Text, _pKey, true);
                var serviceEnc = Encrypt(serviceTextBox.Text, _pKey, true);
                var hostEnc = Encrypt(hostTextBox.Text, _pKey, true);
                var portEnc = Encrypt(portTextBox.Text, _pKey, true);
                var userEnc = Encrypt(userIdTextBox.Text, _pKey, true);
                var passwordEnc = Encrypt(passwordTextBox.Text, _pKey, true);

                nameValueTextBox.AppendText("[PUBLIC_SEED]: " + publicSeed + "\n");
                nameValueTextBox.AppendText("[PUBLIC_KEY]: " + _publicKey + "\n");
                nameValueTextBox.AppendText("[ENCRYPTED_KEY]: " + _publicKeyEncrypted + "\n");
                nameValueTextBox.AppendText("-----------------------------------------------------------------\n");
                nameValueTextBox.AppendText("[PwnSec_Schema]: " + schemaEnc + "\n");
                nameValueTextBox.AppendText("[PwnSec_Service]: " + serviceEnc + "\n");
                nameValueTextBox.AppendText("[PwnSec_Host]: " + hostEnc + "\n");
                nameValueTextBox.AppendText("[PwnSec_Port]: " + portEnc + "\n");
                nameValueTextBox.AppendText("[PwnSec_User]: " + userEnc + "\n");
                nameValueTextBox.AppendText("[PwnSec_Pwd]: " + passwordEnc + "\n");
                nameValueTextBox.AppendText("-----------------------------------------------------------------\n");
            }
            else
            {
                MessageBox.Show("Please input values for all required fields", "Input Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(nameTextBox.Text) || String.IsNullOrEmpty(valueTextBox.Text))
            {
                // display error message for invalid input
                MessageBox.Show("You must input a name and a value.", "Invalid Input", MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
            else
            {
                // encrypt the value
                string value = Encrypt(valueTextBox.Text, _masterKey, true);
                string name = nameTextBox.Text;

                string validation = "(" + Decrypt(value, _masterKey, true) + ")";

                // add name and value pair to the table
                nameValueTextBox.AppendText("[" + name.ToUpper() + "]: " + value + "\n"); // + validation + "\n");

                // clear name and value text boxes
                nameTextBox.Text = String.Empty;
                valueTextBox.Text = String.Empty;

                // enable the save button
                saveButton.Enabled = true;
            }

            // set focus to name text box
            nameTextBox.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)
        {
            string path = "C:/data_" + DateTime.Now.Ticks + ".p2a";
            nameValueTextBox.SaveFile(path, RichTextBoxStreamType.PlainText);

            Application.Exit();
        }

        private void ModifyControl(Control control, bool valid)
        {
            if (valid)
            {
                control.Font = _wingdingsFont;
                control.ForeColor = Color.Green;
                control.Text = '\xFC'.ToString();
            }
            else
            {
                control.Font = _tahomaFont;
                control.ForeColor = Color.Red;
                control.Text = "*";
            }
        }

        private void publicSeedTextBox_TextChanged(object sender, EventArgs e)
        {
            if (publicSeedTextBox.Text.Length > 0)
            {
               ModifyControl(reqLabel1, true);
            }
            else
            {
                ModifyControl(reqLabel1, false);
            }
        }

        private void schemaTextBox_TextChanged(object sender, EventArgs e)
        {
            if (schemaTextBox.Text.Length > 0)
            {
                ModifyControl(reqLabel2, true);
            }
            else
            {
                ModifyControl(reqLabel2, false);
            }
        }

        private void serviceTextBox_TextChanged(object sender, EventArgs e)
        {
            if (serviceTextBox.Text.Length > 0)
            {
                ModifyControl(reqLabel3, true);
            }
            else
            {
                ModifyControl(reqLabel3, false);
            }
        }

        private void hostTextBox_TextChanged(object sender, EventArgs e)
        {
            if (hostTextBox.Text.Length > 0)
            {
                ModifyControl(reqLabel4, true);
            }
            else
            {
                ModifyControl(reqLabel4, false);
            }
        }

        private void portTextBox_TextChanged(object sender, EventArgs e)
        {
            if (portTextBox.Text.Length > 0)
            {
                ModifyControl(reqLabel5, true);
            }
            else
            {
                ModifyControl(reqLabel5, false);
            }
        }

        private void userIdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (userIdTextBox.Text.Length > 0)
            {
                ModifyControl(reqLabel6, true);
            }
            else
            {
                ModifyControl(reqLabel6, false);
            }
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e)
        {
            if (passwordTextBox.Text.Length > 0)
            {
                ModifyControl(reqLabel7, true);
            }
            else
            {
                ModifyControl(reqLabel7, false);
            }
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            DecrypterForm decrypterForm = new DecrypterForm(_masterKey);
            decrypterForm.ShowDialog(this);
            decrypterForm.Dispose();
        }
    }
}

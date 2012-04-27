using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Common.Libraries.Utility.String;

namespace EncryptionMaster
{
    public partial class DecrypterForm : Form
    {
        public StringBuilder DecryptedTextOutput;
        private string decryptionKey;

        public DecrypterForm(string decKey)
        {
            InitializeComponent();
            DecryptedTextOutput = new StringBuilder();
            this.decryptionKey = decKey;
        }

        private void addListEntry(string tag, string data)
        {
            if (!string.IsNullOrEmpty(tag) && !string.IsNullOrEmpty(data))
            {
                this.decryptList.BeginUpdate();
                string dataToAdd =
                    "{" + tag + ": " +
                    StringUtilities.Decrypt(data, this.decryptionKey, true) +
                    "  ( " + data + ")";                    
                this.decryptList.Items.Add(dataToAdd);
                this.decryptList.EndUpdate();
                DecryptedTextOutput.AppendLine(dataToAdd);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            //Add text / tag into list with decrypted text
            if (!string.IsNullOrEmpty(this.tagTextBox.Text) &&
                !string.IsNullOrEmpty(this.dataTextBox.Text))
            {
                this.addListEntry(this.tagTextBox.Text,
                    this.dataTextBox.Text);
            }            
        }

        private void doneButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Save data from string builder into a file
                var fStream = new FileStream(@"c:\dec_data_" + DateTime.Now.Ticks + ".txt", FileMode.Append, FileAccess.Write);
                if (fStream.CanWrite)
                {
                    var sBStr = DecryptedTextOutput.ToString();
                    var enc = new ASCIIEncoding();
                    var stringBytes = enc.GetBytes(sBStr);
                    fStream.Write(stringBytes, 0, stringBytes.Length);
                }
            }
            catch
            {
                MessageBox.Show("Could not save file");
            }
        }
    }
}

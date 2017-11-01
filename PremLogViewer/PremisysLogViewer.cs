using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Security.Principal;
using System.Media;

namespace PremLogViewer
{
    public partial class PremisysLogViewer : Form
    {

        
        public string file;
        public List<string> fileLoad;
        private const string DefaultDbName = "PremiSysLogViewer";
        public string techName;
        public string custName;
        public bool intSecurity = false;
        public string nameOfFile;
        public string typeOfFile;
        public string sqlconnectionBuilder;
        public List<string> Entries;
        public int CurrentIndex = 0;

        public PremisysLogViewer()
        {
            /* When the program is intialized, the DefaultDbName will be
               placed as the value for the txtDatabase textbox.

               Also, the SQL Login Radio Button will be checked by default.
            */
            InitializeComponent();

            txtSqlHost.Text = Properties.Settings.Default.SqlHost;
            txtDatabaseName.Text = Properties.Settings.Default.SqlDatabase;
            txtSqlUsername.Text = Properties.Settings.Default.SqlUser;
            txtSqlPassword.Text = Properties.Settings.Default.SqlPassword;
            tbWindowsLogin.Checked = Properties.Settings.Default.WindowsLogin;
            rbSqlLogin.Checked = Properties.Settings.Default.SqlLogin;

            //rbSqlLogin.Checked = true;

            
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            //Whenever the radio button selection is changed to SQL login 
            //The username and password fields are enabled
            lPassword.Enabled = rbSqlLogin.Checked; 
            lUsername.Enabled = rbSqlLogin.Checked;
            txtSqlUsername.Enabled = rbSqlLogin.Checked;
            txtSqlPassword.Enabled = rbSqlLogin.Checked;


            if (tbWindowsLogin.Checked)
            {
                intSecurity = true;
                txtSqlUsername.Enabled = false;
                txtSqlPassword.Enabled = false;
            }
            else
            {
                intSecurity = false;
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.SqlHost = txtSqlHost.Text;
            Properties.Settings.Default.SqlDatabase = txtDatabaseName.Text;
            Properties.Settings.Default.SqlUser = txtSqlUsername.Text;
            Properties.Settings.Default.SqlPassword = txtSqlPassword.Text;
            Properties.Settings.Default.WindowsLogin = tbWindowsLogin.Checked;
            Properties.Settings.Default.SqlLogin = rbSqlLogin.Checked;
            Properties.Settings.Default.Save();
            
        }

        private void ImportFile_Click(object sender, EventArgs e)
        {
        
        }

        private void LogFile_Click(object sender, EventArgs e)
        {
          
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void CustomerName_Click(object sender, EventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            file = openFileDialog1.FileName;

            nameOfFile = Path.GetFileName(file);

            if (result == DialogResult.OK)
            {
                if (!nameOfFile.ToLower().Contains("exception"))
                {
                    MessageBox.Show("Please Load An Exception File.");
                    return;
                }
                fileLoad = File.ReadAllLines(file).ToList();
                tBImport.Text = file;
            }
        }

        private void ImportFile_Click_1(object sender, EventArgs e)
        {
            if (rbSqlLogin.Checked)
            {
                if (string.IsNullOrWhiteSpace(Properties.Settings.Default.SqlHost) ||
                    string.IsNullOrWhiteSpace(Properties.Settings.Default.SqlDatabase) ||
                    string.IsNullOrWhiteSpace(Properties.Settings.Default.SqlUser) ||
                    string.IsNullOrWhiteSpace(Properties.Settings.Default.SqlPassword))
                {
                    MessageBox.Show("Please enter the database/user information.");
                    return;
                }
            }
            else if (string.IsNullOrWhiteSpace(tBImport.Text))
            {
                MessageBox.Show("Please enter select a file to import.");
                return;
            }
            else if (string.IsNullOrWhiteSpace(txtTechName.Text) || string.IsNullOrWhiteSpace(txtCusName.Text))
            {
                MessageBox.Show("Please add a Tech Name and a Customer Name");
                return;
            }

            var batchInsert = new Helper(txtTechName.Text, txtCusName.Text, txtSqlHost.Text, txtDatabaseName.Text, txtSqlUsername.Text,txtSqlPassword.Text, intSecurity);

            var loadAllFiles = new ImportExceptionFiles(file, batchInsert, fileLoad);

            ImportExceptionFiles.CheckFileHashAgainstExistingHashes(loadAllFiles, batchInsert);
                  
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var listBoxSender = sender as ListBox;
            if (listBoxSender == null) return;
            var selectedFile = listBoxSender.SelectedItem;

            var individualEntriesRetrieval = new Helper(txtTechName.Text, txtCusName.Text, txtSqlHost.Text, txtDatabaseName.Text, 
                txtSqlUsername.Text, txtSqlPassword.Text,intSecurity);

            Entries = individualEntriesRetrieval.GetIndividualEntries(selectedFile.ToString());

            CurrentIndex = 0;

            var entriesArray = new String[] {Entries[CurrentIndex]};

            entriesTextBox.Lines = entriesArray;
        }

        private void btFileNamePop_Click(object sender, EventArgs e)
        {
            var fileNamePop = new Helper(txtTechName.Text, txtCusName.Text, txtSqlHost.Text, txtDatabaseName.Text,
                txtSqlUsername.Text, txtSqlPassword.Text, intSecurity);
            var fileNames = fileNamePop.GetFileNames();
            filesList.DataSource = fileNames;
        }

        private void prevEntryBt_Click(object sender, EventArgs e)
        {
            int counter = Entries.Count - 1;

            if (CurrentIndex == 0)
            {
                SoundPlayer audio = new SoundPlayer(PremLogViewer.Properties.Resources.Stop);
                audio.Play();
                return;
            }
            else if (CurrentIndex <= counter)
            {
                CurrentIndex--;
                var entriesArray = new String[] { Entries[CurrentIndex] };
                entriesTextBox.Lines = entriesArray;
            }
        }

        private void nextEntryBt_Click(object sender, EventArgs e)
        {
            int counter = Entries.Count - 1;

            if (CurrentIndex == counter)
            {
                SoundPlayer audio = new SoundPlayer(PremLogViewer.Properties.Resources.Stop);
                audio.Play();
                return;
            }
            else if (CurrentIndex >= 0)
            {
                CurrentIndex++;
                var entriesArray = new String[] { Entries[CurrentIndex] };
                entriesTextBox.Lines = entriesArray;
            }
        }
    }
}

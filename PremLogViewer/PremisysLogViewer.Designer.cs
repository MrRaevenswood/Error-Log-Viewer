namespace PremLogViewer
{
    partial class PremisysLogViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PremisysLogViewer));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.importTab = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.lbTechName = new System.Windows.Forms.Label();
            this.txtTechName = new System.Windows.Forms.TextBox();
            this.CustomerName = new System.Windows.Forms.Label();
            this.txtCusName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Browse = new System.Windows.Forms.Button();
            this.tBImport = new System.Windows.Forms.TextBox();
            this.ImportFile = new System.Windows.Forms.Button();
            this.tabViewer = new System.Windows.Forms.TabPage();
            this.btFileNamePop = new System.Windows.Forms.Button();
            this.nextEntryBt = new System.Windows.Forms.Button();
            this.prevEntryBt = new System.Windows.Forms.Button();
            this.entriesTextBox = new System.Windows.Forms.RichTextBox();
            this.filesList = new System.Windows.Forms.ListBox();
            this.tabConfigure = new System.Windows.Forms.TabPage();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.txtSqlPassword = new System.Windows.Forms.TextBox();
            this.lPassword = new System.Windows.Forms.Label();
            this.txtSqlUsername = new System.Windows.Forms.TextBox();
            this.lUsername = new System.Windows.Forms.Label();
            this.tbWindowsLogin = new System.Windows.Forms.RadioButton();
            this.rbSqlLogin = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSqlHost = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.importTab.SuspendLayout();
            this.tabViewer.SuspendLayout();
            this.tabConfigure.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.importTab);
            this.tabControl1.Controls.Add(this.tabViewer);
            this.tabControl1.Controls.Add(this.tabConfigure);
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(851, 624);
            this.tabControl1.TabIndex = 0;
            // 
            // importTab
            // 
            this.importTab.Controls.Add(this.label6);
            this.importTab.Controls.Add(this.lbTechName);
            this.importTab.Controls.Add(this.txtTechName);
            this.importTab.Controls.Add(this.CustomerName);
            this.importTab.Controls.Add(this.txtCusName);
            this.importTab.Controls.Add(this.label5);
            this.importTab.Controls.Add(this.Browse);
            this.importTab.Controls.Add(this.tBImport);
            this.importTab.Controls.Add(this.ImportFile);
            this.importTab.ImageKey = "data_transfer_128.png";
            this.importTab.Location = new System.Drawing.Point(4, 39);
            this.importTab.Name = "importTab";
            this.importTab.Padding = new System.Windows.Forms.Padding(3);
            this.importTab.Size = new System.Drawing.Size(843, 581);
            this.importTab.TabIndex = 0;
            this.importTab.Text = "Import";
            this.importTab.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label6.Location = new System.Drawing.Point(135, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(609, 20);
            this.label6.TabIndex = 25;
            this.label6.Text = "Configure the Import By Adding Your Name, The Customer Name, and Files to Import";
            // 
            // lbTechName
            // 
            this.lbTechName.AutoSize = true;
            this.lbTechName.Location = new System.Drawing.Point(124, 102);
            this.lbTechName.Name = "lbTechName";
            this.lbTechName.Size = new System.Drawing.Size(63, 13);
            this.lbTechName.TabIndex = 24;
            this.lbTechName.Text = "Tech Name";
            // 
            // txtTechName
            // 
            this.txtTechName.Location = new System.Drawing.Point(72, 130);
            this.txtTechName.Name = "txtTechName";
            this.txtTechName.Size = new System.Drawing.Size(176, 20);
            this.txtTechName.TabIndex = 23;
            // 
            // CustomerName
            // 
            this.CustomerName.AutoSize = true;
            this.CustomerName.Location = new System.Drawing.Point(114, 193);
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Size = new System.Drawing.Size(82, 13);
            this.CustomerName.TabIndex = 22;
            this.CustomerName.Text = "Customer Name";
            this.CustomerName.Click += new System.EventHandler(this.CustomerName_Click);
            // 
            // txtCusName
            // 
            this.txtCusName.Location = new System.Drawing.Point(72, 222);
            this.txtCusName.Name = "txtCusName";
            this.txtCusName.Size = new System.Drawing.Size(176, 20);
            this.txtCusName.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(359, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(145, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Choose the Log File to Import";
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(298, 188);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(75, 23);
            this.Browse.TabIndex = 19;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // tBImport
            // 
            this.tBImport.Location = new System.Drawing.Point(298, 162);
            this.tBImport.Name = "tBImport";
            this.tBImport.Size = new System.Drawing.Size(265, 20);
            this.tBImport.TabIndex = 18;
            // 
            // ImportFile
            // 
            this.ImportFile.Location = new System.Drawing.Point(654, 109);
            this.ImportFile.Name = "ImportFile";
            this.ImportFile.Size = new System.Drawing.Size(115, 97);
            this.ImportFile.TabIndex = 17;
            this.ImportFile.Text = "Import";
            this.ImportFile.UseVisualStyleBackColor = true;
            this.ImportFile.Click += new System.EventHandler(this.ImportFile_Click_1);
            // 
            // tabViewer
            // 
            this.tabViewer.Controls.Add(this.btFileNamePop);
            this.tabViewer.Controls.Add(this.nextEntryBt);
            this.tabViewer.Controls.Add(this.prevEntryBt);
            this.tabViewer.Controls.Add(this.entriesTextBox);
            this.tabViewer.Controls.Add(this.filesList);
            this.tabViewer.ImageKey = "document_view.png";
            this.tabViewer.Location = new System.Drawing.Point(4, 39);
            this.tabViewer.Name = "tabViewer";
            this.tabViewer.Padding = new System.Windows.Forms.Padding(3);
            this.tabViewer.Size = new System.Drawing.Size(843, 581);
            this.tabViewer.TabIndex = 1;
            this.tabViewer.Text = "Viewer";
            this.tabViewer.UseVisualStyleBackColor = true;
            // 
            // btFileNamePop
            // 
            this.btFileNamePop.Location = new System.Drawing.Point(35, 541);
            this.btFileNamePop.Name = "btFileNamePop";
            this.btFileNamePop.Size = new System.Drawing.Size(75, 23);
            this.btFileNamePop.TabIndex = 4;
            this.btFileNamePop.Text = "Populate";
            this.btFileNamePop.UseVisualStyleBackColor = true;
            this.btFileNamePop.Click += new System.EventHandler(this.btFileNamePop_Click);
            // 
            // nextEntryBt
            // 
            this.nextEntryBt.Location = new System.Drawing.Point(749, 541);
            this.nextEntryBt.Name = "nextEntryBt";
            this.nextEntryBt.Size = new System.Drawing.Size(75, 23);
            this.nextEntryBt.TabIndex = 3;
            this.nextEntryBt.Text = "Next";
            this.nextEntryBt.UseVisualStyleBackColor = true;
            this.nextEntryBt.Click += new System.EventHandler(this.nextEntryBt_Click);
            // 
            // prevEntryBt
            // 
            this.prevEntryBt.Location = new System.Drawing.Point(295, 541);
            this.prevEntryBt.Name = "prevEntryBt";
            this.prevEntryBt.Size = new System.Drawing.Size(75, 23);
            this.prevEntryBt.TabIndex = 2;
            this.prevEntryBt.Text = "Previous";
            this.prevEntryBt.UseVisualStyleBackColor = true;
            this.prevEntryBt.Click += new System.EventHandler(this.prevEntryBt_Click);
            // 
            // entriesTextBox
            // 
            this.entriesTextBox.Location = new System.Drawing.Point(295, 33);
            this.entriesTextBox.Name = "entriesTextBox";
            this.entriesTextBox.Size = new System.Drawing.Size(529, 484);
            this.entriesTextBox.TabIndex = 1;
            this.entriesTextBox.Text = "";
            // 
            // filesList
            // 
            this.filesList.FormattingEnabled = true;
            this.filesList.Location = new System.Drawing.Point(35, 32);
            this.filesList.Name = "filesList";
            this.filesList.Size = new System.Drawing.Size(222, 485);
            this.filesList.TabIndex = 0;
            this.filesList.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tabConfigure
            // 
            this.tabConfigure.Controls.Add(this.btnSaveSettings);
            this.tabConfigure.Controls.Add(this.txtSqlPassword);
            this.tabConfigure.Controls.Add(this.lPassword);
            this.tabConfigure.Controls.Add(this.txtSqlUsername);
            this.tabConfigure.Controls.Add(this.lUsername);
            this.tabConfigure.Controls.Add(this.tbWindowsLogin);
            this.tabConfigure.Controls.Add(this.rbSqlLogin);
            this.tabConfigure.Controls.Add(this.label4);
            this.tabConfigure.Controls.Add(this.txtDatabaseName);
            this.tabConfigure.Controls.Add(this.label3);
            this.tabConfigure.Controls.Add(this.txtSqlHost);
            this.tabConfigure.Controls.Add(this.label2);
            this.tabConfigure.Controls.Add(this.label1);
            this.tabConfigure.ImageKey = "settings_wizard_128.png";
            this.tabConfigure.Location = new System.Drawing.Point(4, 39);
            this.tabConfigure.Margin = new System.Windows.Forms.Padding(10);
            this.tabConfigure.Name = "tabConfigure";
            this.tabConfigure.Size = new System.Drawing.Size(843, 581);
            this.tabConfigure.TabIndex = 2;
            this.tabConfigure.Text = "Configure";
            this.tabConfigure.UseVisualStyleBackColor = true;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSaveSettings.Image = global::PremLogViewer.Properties.Resources.tools_save_64;
            this.btnSaveSettings.Location = new System.Drawing.Point(716, 89);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(98, 100);
            this.btnSaveSettings.TabIndex = 12;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSaveSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // txtSqlPassword
            // 
            this.txtSqlPassword.Location = new System.Drawing.Point(123, 169);
            this.txtSqlPassword.Name = "txtSqlPassword";
            this.txtSqlPassword.PasswordChar = '*';
            this.txtSqlPassword.Size = new System.Drawing.Size(265, 20);
            this.txtSqlPassword.TabIndex = 11;
            // 
            // lPassword
            // 
            this.lPassword.AutoSize = true;
            this.lPassword.Location = new System.Drawing.Point(58, 172);
            this.lPassword.Name = "lPassword";
            this.lPassword.Size = new System.Drawing.Size(56, 13);
            this.lPassword.TabIndex = 10;
            this.lPassword.Text = "Password:";
            this.lPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSqlUsername
            // 
            this.txtSqlUsername.Location = new System.Drawing.Point(123, 143);
            this.txtSqlUsername.Name = "txtSqlUsername";
            this.txtSqlUsername.Size = new System.Drawing.Size(265, 20);
            this.txtSqlUsername.TabIndex = 9;
            // 
            // lUsername
            // 
            this.lUsername.AutoSize = true;
            this.lUsername.Location = new System.Drawing.Point(56, 146);
            this.lUsername.Name = "lUsername";
            this.lUsername.Size = new System.Drawing.Size(58, 13);
            this.lUsername.TabIndex = 8;
            this.lUsername.Text = "Username:";
            this.lUsername.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbWindowsLogin
            // 
            this.tbWindowsLogin.AutoSize = true;
            this.tbWindowsLogin.Location = new System.Drawing.Point(260, 120);
            this.tbWindowsLogin.Name = "tbWindowsLogin";
            this.tbWindowsLogin.Size = new System.Drawing.Size(69, 17);
            this.tbWindowsLogin.TabIndex = 7;
            this.tbWindowsLogin.TabStop = true;
            this.tbWindowsLogin.Text = "Windows";
            this.tbWindowsLogin.UseVisualStyleBackColor = true;
            this.tbWindowsLogin.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // rbSqlLogin
            // 
            this.rbSqlLogin.AutoSize = true;
            this.rbSqlLogin.Location = new System.Drawing.Point(123, 120);
            this.rbSqlLogin.Name = "rbSqlLogin";
            this.rbSqlLogin.Size = new System.Drawing.Size(75, 17);
            this.rbSqlLogin.TabIndex = 6;
            this.rbSqlLogin.TabStop = true;
            this.rbSqlLogin.Text = "SQL Login";
            this.rbSqlLogin.UseVisualStyleBackColor = true;
            this.rbSqlLogin.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Login Type:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(123, 89);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(265, 20);
            this.txtDatabaseName.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Database:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSqlHost
            // 
            this.txtSqlHost.Location = new System.Drawing.Point(123, 63);
            this.txtSqlHost.Name = "txtSqlHost";
            this.txtSqlHost.Size = new System.Drawing.Size(265, 20);
            this.txtSqlHost.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "SQL Server Host:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10);
            this.label1.Size = new System.Drawing.Size(843, 57);
            this.label1.TabIndex = 0;
            this.label1.Text = "Configure the system database to be used to store data from log files:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "data_transfer_128.png");
            this.imageList1.Images.SetKeyName(1, "document_view.png");
            this.imageList1.Images.SetKeyName(2, "settings_wizard_128.png");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // PremisysLogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 693);
            this.Controls.Add(this.tabControl1);
            this.Name = "PremisysLogViewer";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "PremiSys Log Viewer";
            this.tabControl1.ResumeLayout(false);
            this.importTab.ResumeLayout(false);
            this.importTab.PerformLayout();
            this.tabViewer.ResumeLayout(false);
            this.tabConfigure.ResumeLayout(false);
            this.tabConfigure.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage importTab;
        private System.Windows.Forms.TabPage tabViewer;
        private System.Windows.Forms.TabPage tabConfigure;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSqlPassword;
        private System.Windows.Forms.Label lPassword;
        private System.Windows.Forms.TextBox txtSqlUsername;
        private System.Windows.Forms.Label lUsername;
        private System.Windows.Forms.RadioButton tbWindowsLogin;
        private System.Windows.Forms.RadioButton rbSqlLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDatabaseName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSqlHost;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ImportFile;
        private System.Windows.Forms.TextBox tBImport;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCusName;
        private System.Windows.Forms.Label CustomerName;
        private System.Windows.Forms.TextBox txtTechName;
        private System.Windows.Forms.Label lbTechName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox entriesTextBox;
        private System.Windows.Forms.ListBox filesList;
        private System.Windows.Forms.Button nextEntryBt;
        private System.Windows.Forms.Button prevEntryBt;
        private System.Windows.Forms.Button btFileNamePop;
    }
}


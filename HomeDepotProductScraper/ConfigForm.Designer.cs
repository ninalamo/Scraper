using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
namespace HomeDepotProductScraper
{
    partial class ConfigForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdApply = new System.Windows.Forms.Button();
            this.grpFTP = new System.Windows.Forms.GroupBox();
            this.chkFtp = new System.Windows.Forms.CheckBox();
            this.chkLocal = new System.Windows.Forms.CheckBox();
            this.txtHTTP = new System.Windows.Forms.TextBox();
            this.lblHTTP = new System.Windows.Forms.Label();
            this.cmdFTPTest = new System.Windows.Forms.Button();
            this.lblNotice = new System.Windows.Forms.Label();
            this.lblFolder = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.lblHost = new System.Windows.Forms.Label();
            this.dlgOPEN = new System.Windows.Forms.OpenFileDialog();
            this.grpExcelFile = new System.Windows.Forms.GroupBox();
            this.cmdSKU = new System.Windows.Forms.Button();
            this.txtSKU = new System.Windows.Forms.TextBox();
            this.cmd1st = new System.Windows.Forms.Button();
            this.txt1st = new System.Windows.Forms.TextBox();
            this.cmd2nd = new System.Windows.Forms.Button();
            this.txt2nd = new System.Windows.Forms.TextBox();
            this.lblSKU = new System.Windows.Forms.Label();
            this.lbl2nd = new System.Windows.Forms.Label();
            this.lblPrimary = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtProxyIP = new System.Windows.Forms.TextBox();
            this.txtProxyPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRemoveCookies = new System.Windows.Forms.Button();
            this.pbDeleteFiles = new System.Windows.Forms.ProgressBar();
            this.grpFTP.SuspendLayout();
            this.grpExcelFile.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCancel.Location = new System.Drawing.Point(301, 431);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(131, 23);
            this.cmdCancel.TabIndex = 0;
            this.cmdCancel.Text = "Discard The Changes";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdApply
            // 
            this.cmdApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdApply.Location = new System.Drawing.Point(12, 431);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(113, 23);
            this.cmdApply.TabIndex = 1;
            this.cmdApply.Text = "Apply The Changes";
            this.cmdApply.UseVisualStyleBackColor = true;
            this.cmdApply.Click += new System.EventHandler(this.cmdApply_Click);
            // 
            // grpFTP
            // 
            this.grpFTP.Controls.Add(this.chkFtp);
            this.grpFTP.Controls.Add(this.chkLocal);
            this.grpFTP.Controls.Add(this.txtHTTP);
            this.grpFTP.Controls.Add(this.lblHTTP);
            this.grpFTP.Controls.Add(this.cmdFTPTest);
            this.grpFTP.Controls.Add(this.lblNotice);
            this.grpFTP.Controls.Add(this.lblFolder);
            this.grpFTP.Controls.Add(this.txtFolder);
            this.grpFTP.Controls.Add(this.txtPassword);
            this.grpFTP.Controls.Add(this.lblPass);
            this.grpFTP.Controls.Add(this.txtUser);
            this.grpFTP.Controls.Add(this.lblUser);
            this.grpFTP.Controls.Add(this.txtPort);
            this.grpFTP.Controls.Add(this.lblPort);
            this.grpFTP.Controls.Add(this.txtHost);
            this.grpFTP.Controls.Add(this.lblHost);
            this.grpFTP.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpFTP.Location = new System.Drawing.Point(12, 170);
            this.grpFTP.Name = "grpFTP";
            this.grpFTP.Size = new System.Drawing.Size(420, 255);
            this.grpFTP.TabIndex = 3;
            this.grpFTP.TabStop = false;
            this.grpFTP.Text = "FTP Configuration";
            // 
            // chkFtp
            // 
            this.chkFtp.AutoSize = true;
            this.chkFtp.Location = new System.Drawing.Point(7, 19);
            this.chkFtp.Name = "chkFtp";
            this.chkFtp.Size = new System.Drawing.Size(135, 17);
            this.chkFtp.TabIndex = 15;
            this.chkFtp.Text = "Disable FTP Uploading";
            this.chkFtp.UseVisualStyleBackColor = true;
            // 
            // chkLocal
            // 
            this.chkLocal.AutoSize = true;
            this.chkLocal.Location = new System.Drawing.Point(9, 233);
            this.chkLocal.Name = "chkLocal";
            this.chkLocal.Size = new System.Drawing.Size(236, 17);
            this.chkLocal.TabIndex = 14;
            this.chkLocal.Text = "Download Copy of Images to Local Directory";
            this.chkLocal.UseVisualStyleBackColor = true;
            // 
            // txtHTTP
            // 
            this.txtHTTP.Location = new System.Drawing.Point(109, 203);
            this.txtHTTP.Name = "txtHTTP";
            this.txtHTTP.Size = new System.Drawing.Size(294, 20);
            this.txtHTTP.TabIndex = 13;
            this.txtHTTP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblHTTP
            // 
            this.lblHTTP.AutoSize = true;
            this.lblHTTP.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblHTTP.Location = new System.Drawing.Point(6, 202);
            this.lblHTTP.Name = "lblHTTP";
            this.lblHTTP.Size = new System.Drawing.Size(91, 13);
            this.lblHTTP.TabIndex = 12;
            this.lblHTTP.Text = "HTTP Base Path:";
            // 
            // cmdFTPTest
            // 
            this.cmdFTPTest.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdFTPTest.Location = new System.Drawing.Point(216, 79);
            this.cmdFTPTest.Name = "cmdFTPTest";
            this.cmdFTPTest.Size = new System.Drawing.Size(187, 23);
            this.cmdFTPTest.TabIndex = 11;
            this.cmdFTPTest.Text = "Test FTP Connection";
            this.cmdFTPTest.UseVisualStyleBackColor = true;
            this.cmdFTPTest.Click += new System.EventHandler(this.cmdFTPTest_Click);
            // 
            // lblNotice
            // 
            this.lblNotice.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblNotice.Location = new System.Drawing.Point(106, 183);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(245, 16);
            this.lblNotice.TabIndex = 10;
            this.lblNotice.Text = "Leave It Blank For Root Folder";
            this.lblNotice.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblFolder
            // 
            this.lblFolder.AutoSize = true;
            this.lblFolder.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblFolder.Location = new System.Drawing.Point(4, 160);
            this.lblFolder.Name = "lblFolder";
            this.lblFolder.Size = new System.Drawing.Size(88, 13);
            this.lblFolder.TabIndex = 9;
            this.lblFolder.Text = "Upload to &Folder:";
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(109, 160);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(294, 20);
            this.txtFolder.TabIndex = 8;
            this.txtFolder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(109, 133);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(294, 20);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPass.Location = new System.Drawing.Point(7, 132);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(79, 13);
            this.lblPass.TabIndex = 6;
            this.lblPass.Text = "FTP &Password:";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(109, 106);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(294, 20);
            this.txtUser.TabIndex = 5;
            this.txtUser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblUser.Location = new System.Drawing.Point(7, 106);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(86, 13);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "FTP User &Name:";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(109, 79);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(100, 20);
            this.txtPort.TabIndex = 3;
            this.txtPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPort.Location = new System.Drawing.Point(7, 79);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(52, 13);
            this.lblPort.TabIndex = 2;
            this.lblPort.Text = "FTP P&ort:";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(109, 51);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(294, 20);
            this.txtHost.TabIndex = 1;
            this.txtHost.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblHost
            // 
            this.lblHost.AutoSize = true;
            this.lblHost.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblHost.Location = new System.Drawing.Point(7, 51);
            this.lblHost.Name = "lblHost";
            this.lblHost.Size = new System.Drawing.Size(95, 13);
            this.lblHost.TabIndex = 0;
            this.lblHost.Text = "FTP &Host (Server):";
            // 
            // grpExcelFile
            // 
            this.grpExcelFile.Controls.Add(this.cmdSKU);
            this.grpExcelFile.Controls.Add(this.txtSKU);
            this.grpExcelFile.Controls.Add(this.cmd1st);
            this.grpExcelFile.Controls.Add(this.txt1st);
            this.grpExcelFile.Controls.Add(this.cmd2nd);
            this.grpExcelFile.Controls.Add(this.txt2nd);
            this.grpExcelFile.Controls.Add(this.lblSKU);
            this.grpExcelFile.Controls.Add(this.lbl2nd);
            this.grpExcelFile.Controls.Add(this.lblPrimary);
            this.grpExcelFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.grpExcelFile.Location = new System.Drawing.Point(12, 59);
            this.grpExcelFile.Name = "grpExcelFile";
            this.grpExcelFile.Size = new System.Drawing.Size(420, 105);
            this.grpExcelFile.TabIndex = 4;
            this.grpExcelFile.TabStop = false;
            this.grpExcelFile.Text = "Output Excel File";
            // 
            // cmdSKU
            // 
            this.cmdSKU.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdSKU.Location = new System.Drawing.Point(340, 73);
            this.cmdSKU.Name = "cmdSKU";
            this.cmdSKU.Size = new System.Drawing.Size(75, 23);
            this.cmdSKU.TabIndex = 8;
            this.cmdSKU.Text = "Select";
            this.cmdSKU.UseVisualStyleBackColor = true;
            this.cmdSKU.Click += new System.EventHandler(this.cmdSKU_Click);
            // 
            // txtSKU
            // 
            this.txtSKU.BackColor = System.Drawing.SystemColors.Window;
            this.txtSKU.Location = new System.Drawing.Point(128, 73);
            this.txtSKU.Name = "txtSKU";
            this.txtSKU.ReadOnly = true;
            this.txtSKU.Size = new System.Drawing.Size(205, 20);
            this.txtSKU.TabIndex = 7;
            // 
            // cmd1st
            // 
            this.cmd1st.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmd1st.Location = new System.Drawing.Point(339, 17);
            this.cmd1st.Name = "cmd1st";
            this.cmd1st.Size = new System.Drawing.Size(75, 23);
            this.cmd1st.TabIndex = 6;
            this.cmd1st.Text = "Select";
            this.cmd1st.UseVisualStyleBackColor = true;
            this.cmd1st.Click += new System.EventHandler(this.cmd1st_Click);
            // 
            // txt1st
            // 
            this.txt1st.BackColor = System.Drawing.SystemColors.Window;
            this.txt1st.Location = new System.Drawing.Point(128, 17);
            this.txt1st.Name = "txt1st";
            this.txt1st.ReadOnly = true;
            this.txt1st.Size = new System.Drawing.Size(205, 20);
            this.txt1st.TabIndex = 5;
            // 
            // cmd2nd
            // 
            this.cmd2nd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmd2nd.Location = new System.Drawing.Point(339, 46);
            this.cmd2nd.Name = "cmd2nd";
            this.cmd2nd.Size = new System.Drawing.Size(75, 23);
            this.cmd2nd.TabIndex = 4;
            this.cmd2nd.Text = "Select";
            this.cmd2nd.UseVisualStyleBackColor = true;
            this.cmd2nd.Click += new System.EventHandler(this.cmd2nd_Click);
            // 
            // txt2nd
            // 
            this.txt2nd.BackColor = System.Drawing.SystemColors.Window;
            this.txt2nd.Location = new System.Drawing.Point(128, 46);
            this.txt2nd.Name = "txt2nd";
            this.txt2nd.ReadOnly = true;
            this.txt2nd.Size = new System.Drawing.Size(205, 20);
            this.txt2nd.TabIndex = 3;
            // 
            // lblSKU
            // 
            this.lblSKU.AutoSize = true;
            this.lblSKU.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblSKU.Location = new System.Drawing.Point(12, 73);
            this.lblSKU.Name = "lblSKU";
            this.lblSKU.Size = new System.Drawing.Size(90, 13);
            this.lblSKU.TabIndex = 2;
            this.lblSKU.Text = "SKU Lookup File:";
            // 
            // lbl2nd
            // 
            this.lbl2nd.AutoSize = true;
            this.lbl2nd.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbl2nd.Location = new System.Drawing.Point(12, 46);
            this.lbl2nd.Name = "lbl2nd";
            this.lbl2nd.Size = new System.Drawing.Size(109, 13);
            this.lbl2nd.TabIndex = 1;
            this.lbl2nd.Text = "Secondary Excel File:";
            // 
            // lblPrimary
            // 
            this.lblPrimary.AutoSize = true;
            this.lblPrimary.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPrimary.Location = new System.Drawing.Point(12, 20);
            this.lblPrimary.Name = "lblPrimary";
            this.lblPrimary.Size = new System.Drawing.Size(92, 13);
            this.lblPrimary.TabIndex = 0;
            this.lblPrimary.Text = "Primary Excel File:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtProxyIP);
            this.groupBox1.Controls.Add(this.txtProxyPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Location = new System.Drawing.Point(13, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 47);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Proxy Setting";
            // 
            // txtProxyIP
            // 
            this.txtProxyIP.BackColor = System.Drawing.SystemColors.Window;
            this.txtProxyIP.Location = new System.Drawing.Point(35, 17);
            this.txtProxyIP.Name = "txtProxyIP";
            this.txtProxyIP.Size = new System.Drawing.Size(173, 20);
            this.txtProxyIP.TabIndex = 5;
            // 
            // txtProxyPort
            // 
            this.txtProxyPort.BackColor = System.Drawing.SystemColors.Window;
            this.txtProxyPort.Location = new System.Drawing.Point(288, 17);
            this.txtProxyPort.Name = "txtProxyPort";
            this.txtProxyPort.Size = new System.Drawing.Size(104, 20);
            this.txtProxyPort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.Location = new System.Drawing.Point(251, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label3.Location = new System.Drawing.Point(12, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "IP:";
            // 
            // btnRemoveCookies
            // 
            this.btnRemoveCookies.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveCookies.Location = new System.Drawing.Point(131, 431);
            this.btnRemoveCookies.Name = "btnRemoveCookies";
            this.btnRemoveCookies.Size = new System.Drawing.Size(172, 23);
            this.btnRemoveCookies.TabIndex = 14;
            this.btnRemoveCookies.TabStop = false;
            this.btnRemoveCookies.Text = "Clear All Cookies & Temp File";
            this.btnRemoveCookies.UseMnemonic = false;
            this.btnRemoveCookies.UseVisualStyleBackColor = true;
            this.btnRemoveCookies.Click += new System.EventHandler(this.btnRemoveCookies_Click);
            // 
            // pbDeleteFiles
            // 
            this.pbDeleteFiles.Location = new System.Drawing.Point(13, 459);
            this.pbDeleteFiles.Name = "pbDeleteFiles";
            this.pbDeleteFiles.Size = new System.Drawing.Size(420, 23);
            this.pbDeleteFiles.TabIndex = 15;
            this.pbDeleteFiles.Visible = false;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 485);
            this.Controls.Add(this.pbDeleteFiles);
            this.Controls.Add(this.btnRemoveCookies);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.grpExcelFile);
            this.Controls.Add(this.grpFTP);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.cmdCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FTP Configuration";
            this.grpFTP.ResumeLayout(false);
            this.grpFTP.PerformLayout();
            this.grpExcelFile.ResumeLayout(false);
            this.grpExcelFile.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button cmdCancel;
        private Button cmdApply;
        private GroupBox grpFTP;
        private Button cmdFTPTest;
        private Label lblNotice;
        private Label lblFolder;
        private TextBox txtFolder;
        private TextBox txtPassword;
        private Label lblPass;
        private TextBox txtUser;
        private Label lblUser;
        private TextBox txtPort;
        private Label lblPort;
        private TextBox txtHost;
        private Label lblHost;
        private OpenFileDialog dlgOPEN;
        private TextBox txtHTTP;
        private Label lblHTTP;
        private GroupBox grpExcelFile;
        private Button cmdSKU;
        private TextBox txtSKU;
        private Button cmd1st;
        private TextBox txt1st;
        private Button cmd2nd;
        private TextBox txt2nd;
        private Label lblSKU;
        private Label lbl2nd;
        private Label lblPrimary;
        private CheckBox chkLocal;
        private CheckBox chkFtp;
        private GroupBox groupBox1;
        private TextBox txtProxyIP;
        private TextBox txtProxyPort;
        private Label label2;
        private Label label3;
        private Button btnRemoveCookies;
        private ProgressBar pbDeleteFiles;
    }
}
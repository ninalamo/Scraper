using Chilkat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeDepotProductScraper
{
    public partial class ConfigForm : Form
    {
        private Dictionary<string, string> config;

        public ConfigForm()
        {
            InitializeComponent();
            LoadConfig();
            TruncateText(config["Primary"], ref this.txt1st);
            TruncateText(config["Secondary"], ref this.txt2nd);
            TruncateText(config["SKU"], ref this.txtSKU);
            try
            {
                this.txtHost.Text = config["Host"];
                this.txtPort.Text = config["Port"];
                this.txtUser.Text = config["User"];
                this.txtPassword.Text = config["Password"];
                this.txtFolder.Text = config["Folder"];
                this.txtHTTP.Text = config["HTTP"];
                this.chkLocal.Checked = Convert.ToBoolean(config["Local"]);
                this.chkFtp.Checked = Convert.ToBoolean(config["FTP"]);
                this.txtProxyIP.Text = config["ProxyIP"];
                this.txtProxyPort.Text = config["ProxyPort"];
            }
            catch { }
        }

        private void LoadConfig()
        {
            var lines = File.ReadAllLines("Config.ini");
            config = new Dictionary<string, string>();

            foreach (var s in lines)
            {
                var split = s.Split(new char[] { '=' }, 2);
                if (split[0].CompareTo("Password") == 0)
                    split[1] = DecryptText(split[1], "Salma121015");
                config.Add(split[0], split[1]);
            }
        }

        private string EncryptText(string password, string salt)
        {
            Xml xml = new Xml();
            xml.NewChild2("key", password);
            xml.FirstChild2();
            xml.EncryptContent(salt);
            string password_encrypted = xml.Content;
            xml.Dispose();
            xml = (Xml)null;

            return password_encrypted;
        }

        private string DecryptText(string password_encrypted, string salt)
        {
            Xml xml = new Xml();
            xml.NewChild2("key", password_encrypted);
            xml.FirstChild2();
            xml.DecryptContent(salt);
            string password = xml.Content;
            xml.Dispose();
            xml = (Xml)null;

            return password;
        }

        private void TruncateText(string strFullText, ref TextBox objTextBox)
        {
            string text = string.Copy(strFullText);
            int width = objTextBox.Width;
            Font font = objTextBox.Font;
            TextFormatFlags textFormatFlags = TextFormatFlags.PathEllipsis;
            TextRenderer.MeasureText(text, font, new Size(width, 0), textFormatFlags | TextFormatFlags.ModifyString);
            objTextBox.Text = text;
            objTextBox.Tag = (object)strFullText;
        }

        private void cmd1st_Click(object sender, EventArgs e)
        {
            this.dlgOPEN.Title = "Please select the primary excel file:";
            this.dlgOPEN.DefaultExt = ".csv";
            this.dlgOPEN.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            this.dlgOPEN.FilterIndex = 1;
            this.dlgOPEN.CheckFileExists = true;
            this.dlgOPEN.CheckPathExists = true;
            this.dlgOPEN.FileName = "";
            if (this.dlgOPEN.ShowDialog() == DialogResult.Cancel)
                return;
            this.TruncateText(this.dlgOPEN.FileName, ref txt1st);
        }

        private void cmd2nd_Click(object sender, EventArgs e)
        {
            this.dlgOPEN.Title = "Please select the secondary excel file:";
            this.dlgOPEN.DefaultExt = ".csv";
            this.dlgOPEN.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            this.dlgOPEN.FilterIndex = 1;
            this.dlgOPEN.CheckFileExists = true;
            this.dlgOPEN.CheckPathExists = true;
            this.dlgOPEN.FileName = "";
            if (this.dlgOPEN.ShowDialog() == DialogResult.Cancel)
                return;
            this.TruncateText(this.dlgOPEN.FileName, ref this.txt2nd);
        }

        private void cmdSKU_Click(object sender, EventArgs e)
        {
            this.dlgOPEN.Title = "Please select the sku text file:";
            this.dlgOPEN.DefaultExt = ".txt";
            this.dlgOPEN.Filter = "Text File (*.txt)|*.txt";
            this.dlgOPEN.FilterIndex = 1;
            this.dlgOPEN.CheckFileExists = true;
            this.dlgOPEN.CheckPathExists = true;
            this.dlgOPEN.FileName = "";
            if (this.dlgOPEN.ShowDialog() == DialogResult.Cancel)
                return;
            this.TruncateText(this.dlgOPEN.FileName, ref this.txtSKU);
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void cmdApply_Click(object sender, EventArgs e)
        {
            if (this.txtHost.Text.Trim().CompareTo("") == 0 | this.txtPort.Text.Trim().CompareTo("") == 0 | this.txtUser.Text.Trim().CompareTo("") == 0 | this.txtPassword.Text.Trim().CompareTo("") == 0)
            {
                MessageBox.Show("Please fill all the fields of FTP Part!", "FTP Information Missing!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                StreamWriter streamWriter = new StreamWriter("Config.ini", false);

                streamWriter.Write("Primary" + "=" + this.txt1st.Tag.ToString() + Environment.NewLine);
                streamWriter.Write("Secondary" + "=" + this.txt2nd.Tag.ToString() + Environment.NewLine);
                streamWriter.Write("SKU" + "=" + this.txtSKU.Tag.ToString() + Environment.NewLine);
                streamWriter.Write("Host" + "=" + this.txtHost.Text + Environment.NewLine);
                streamWriter.Write("Port" + "=" + this.txtPort.Text + Environment.NewLine);
                streamWriter.Write("User" + "=" + this.txtUser.Text + Environment.NewLine);
                streamWriter.Write("Password" + "=" + EncryptText(this.txtPassword.Text, "Salma121015"));
                streamWriter.Write("Folder" + "=" + this.txtFolder.Text + Environment.NewLine);
                streamWriter.Write("HTTP" + "=" + this.txtHTTP.Text + Environment.NewLine);
                streamWriter.Write("Local" + "=" + this.chkLocal.Checked.ToString() + Environment.NewLine);
                streamWriter.Write("FTP" + "=" + this.chkFtp.Checked.ToString() + Environment.NewLine);
                streamWriter.Write("ProxyIP" + "=" + this.txtProxyIP.Text.ToString() + Environment.NewLine);
                streamWriter.Write("ProxyPort" + "=" + this.txtProxyPort.Text.ToString() + Environment.NewLine);
                streamWriter.Close();
                streamWriter.Dispose();
            }
            MessageBox.Show("All Configuration Information Has Been Successfully Saved.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
        }

        private void cmdFTPTest_Click(object sender, EventArgs e)
        {
            Ftp2 ftp2 = new Ftp2();
            if (!ftp2.UnlockComponent("FTP287654321_28EB48A8oH1T"))
            {
                MessageBox.Show("Sorry, FTP Library Registration Error! Please Contact Us!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                ftp2.HeartbeatMs = 100;
                ftp2.ConnectTimeout = 30;
                ftp2.Hostname = this.txtHost.Text.Trim();
                ftp2.Username = this.txtUser.Text;
                ftp2.Password = this.txtPassword.Text;
                int port = 0;
                Int32.TryParse(this.txtPort.Text, out port);
                ftp2.Port = port;
                this.cmdFTPTest.Text = "Connecting to server...";
                this.cmdFTPTest.Enabled = false;
                Application.DoEvents();
                if (!ftp2.Connect())
                {
                    int num2 = (int)MessageBox.Show("Failed to connect FTP server! Please verify that you have input correct information.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ftp2.Dispose();
                    this.cmdFTPTest.Text = "Test FTP Connection";
                    this.cmdFTPTest.Enabled = true;
                }
                else
                {
                    this.txtFolder.Text = this.txtFolder.Text.Trim();
                    this.txtFolder.Text = this.txtFolder.Text.Replace("\\", "/");
                    if (this.txtFolder.Text.CompareTo("") != 0)
                    {
                        string[] strArray = this.txtFolder.Text.Split(new string[1] { "/" }, StringSplitOptions.None);
                        int lowerBound = strArray.GetLowerBound(0);
                        int upperBound = strArray.GetUpperBound(0);
                        int index = lowerBound;
                        while (index <= upperBound)
                        {
                            this.cmdFTPTest.Text = "Accessing to folder: " + strArray[index];
                            Application.DoEvents();
                            if (!ftp2.ChangeRemoteDir(strArray[index]))
                            {
                                int num2 = (int)MessageBox.Show("Sorry, Failed to get access to following folder. Please make sure you have read and write access to that folder." + Environment.NewLine + Environment.NewLine + "Folder: /" + strArray[index] + "/", "FTP Folder Access Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                ftp2.Disconnect();
                                ftp2.Dispose();
                                this.cmdFTPTest.Text = "Test FTP Connection";
                                this.cmdFTPTest.Enabled = true;
                                return;
                            }
                            checked { ++index; }
                        }
                    }
                    MessageBox.Show("FTP Settings Looks Fine!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ftp2.Disconnect();
                    ftp2.Dispose();
                    this.cmdFTPTest.Text = "Test FTP Connection";
                    this.cmdFTPTest.Enabled = true;
                }
            }
        }

        #region Control Events

        /// <summary>
        /// Event is invoked when click on Remove cookies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveCookies_Click(object sender, EventArgs e)
        {
            ClearTempCookiesInfo();
        }

        #endregion

        #region Methods

        public void ClearTempCookiesInfo()
        {
            pbDeleteFiles.Visible = true;
            pbDeleteFiles.Maximum = 4;
            pbDeleteFiles.Step = 1;
            try
            {
                Process removeCookies = new Process();
                removeCookies.StartInfo = new ProcessStartInfo("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 2");
                removeCookies.StartInfo.CreateNoWindow = true;
                removeCookies.Start();
                removeCookies.WaitForExit();
                pbDeleteFiles.PerformStep();

                
                Process removeFormData = new Process();
                removeFormData.StartInfo = new ProcessStartInfo("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 16");
                removeFormData.StartInfo.CreateNoWindow = true;
                removeFormData.Start();                
                removeFormData.WaitForExit();
                pbDeleteFiles.PerformStep();
                
                Process removeHistory = new Process();
                removeHistory.StartInfo = new ProcessStartInfo("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 1");
                removeHistory.StartInfo.CreateNoWindow = true;
                removeHistory.Start();
                removeHistory.WaitForExit();
                pbDeleteFiles.PerformStep();
                
                Process removeTempFiles = new Process();
                removeTempFiles.StartInfo = new ProcessStartInfo("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 8");
                removeTempFiles.StartInfo.CreateNoWindow = true;
                removeTempFiles.Start();
                removeTempFiles.WaitForExit();
                pbDeleteFiles.PerformStep();
                
                MessageBox.Show("All Cookies & Temp files deleted successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while deleting cookies", "Delete Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                pbDeleteFiles.Visible = false;
            }
        }

        #endregion
    }
}



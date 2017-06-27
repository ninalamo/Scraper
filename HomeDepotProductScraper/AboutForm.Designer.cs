using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace HomeDepotProductScraper
{
    partial class AboutForm
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
            this.lblDevelop = new System.Windows.Forms.Label();
            this.lnkCompany = new System.Windows.Forms.LinkLabel();
            this.lnkWeb = new System.Windows.Forms.LinkLabel();
            this.lnkEmail = new System.Windows.Forms.LinkLabel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.picClose = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDevelop
            // 
            this.lblDevelop.AutoSize = true;
            this.lblDevelop.BackColor = System.Drawing.Color.Transparent;
            this.lblDevelop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevelop.Location = new System.Drawing.Point(375, 186);
            this.lblDevelop.Name = "lblDevelop";
            this.lblDevelop.Size = new System.Drawing.Size(113, 17);
            this.lblDevelop.TabIndex = 0;
            this.lblDevelop.Text = "Developed By:";
            // 
            // lnkCompany
            // 
            this.lnkCompany.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lnkCompany.AutoSize = true;
            this.lnkCompany.BackColor = System.Drawing.Color.Transparent;
            this.lnkCompany.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkCompany.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkCompany.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lnkCompany.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkCompany.LinkColor = System.Drawing.Color.SeaGreen;
            this.lnkCompany.Location = new System.Drawing.Point(263, 212);
            this.lnkCompany.Name = "lnkCompany";
            this.lnkCompany.Size = new System.Drawing.Size(225, 17);
            this.lnkCompany.TabIndex = 1;
            this.lnkCompany.TabStop = true;
            this.lnkCompany.Text = "CHOVAN CONSULTING CORP";
            this.lnkCompany.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lnkCompany.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCompany_LinkClicked);
            // 
            // lnkWeb
            // 
            this.lnkWeb.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lnkWeb.AutoSize = true;
            this.lnkWeb.BackColor = System.Drawing.Color.Transparent;
            this.lnkWeb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkWeb.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lnkWeb.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkWeb.LinkColor = System.Drawing.Color.SeaGreen;
            this.lnkWeb.Location = new System.Drawing.Point(353, 238);
            this.lnkWeb.Name = "lnkWeb";
            this.lnkWeb.Size = new System.Drawing.Size(135, 17);
            this.lnkWeb.TabIndex = 1;
            this.lnkWeb.TabStop = true;
            this.lnkWeb.Text = "patrikchovan.com";
            this.lnkWeb.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lnkWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkWeb_LinkClicked);
            // 
            // lnkEmail
            // 
            this.lnkEmail.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lnkEmail.AutoSize = true;
            this.lnkEmail.BackColor = System.Drawing.Color.Transparent;
            this.lnkEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lnkEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkEmail.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lnkEmail.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.lnkEmail.LinkColor = System.Drawing.Color.SeaGreen;
            this.lnkEmail.Location = new System.Drawing.Point(295, 264);
            this.lnkEmail.Name = "lnkEmail";
            this.lnkEmail.Size = new System.Drawing.Size(193, 17);
            this.lnkEmail.TabIndex = 1;
            this.lnkEmail.TabStop = true;
            this.lnkEmail.Text = "admin@patrikchovan.com";
            this.lnkEmail.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lnkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEmail_LinkClicked);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Blue;
            this.lblTitle.Location = new System.Drawing.Point(343, 80);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(145, 17);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Arbitrage Authority";
            // 
            // picClose
            // 
            this.picClose.BackColor = System.Drawing.Color.Transparent;
            this.picClose.Image = global::HomeDepotProductScraper.Properties.Resources.test;
            this.picClose.Location = new System.Drawing.Point(456, 12);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(32, 32);
            this.picClose.TabIndex = 5;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // AboutForm
            // 
            this.BackgroundImage = global::HomeDepotProductScraper.Properties.Resources.dreamstime2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(500, 300);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lnkEmail);
            this.Controls.Add(this.lnkWeb);
            this.Controls.Add(this.lnkCompany);
            this.Controls.Add(this.lblDevelop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AboutForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AboutForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private Label lblDevelop;
        private Label lblTitle;
        private LinkLabel lnkEmail;
        private LinkLabel lnkCompany;
        private LinkLabel lnkWeb;

        #endregion
        private PictureBox picClose;
    }
}
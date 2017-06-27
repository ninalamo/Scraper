namespace HomeDepotProductScraper
{
    partial class ConvertXlsxToCsvForm
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
            this.xlsxPathTextBox = new System.Windows.Forms.TextBox();
            this.openXlsButton = new System.Windows.Forms.Button();
            this.openCsvButton = new System.Windows.Forms.Button();
            this.csvPathTextBox = new System.Windows.Forms.TextBox();
            this.columnCLabel = new System.Windows.Forms.Label();
            this.columnLLabel = new System.Windows.Forms.Label();
            this.columnQLabel = new System.Windows.Forms.Label();
            this.columnBTextBox = new System.Windows.Forms.TextBox();
            this.columnCTextBox = new System.Windows.Forms.TextBox();
            this.columnLTextBox = new System.Windows.Forms.TextBox();
            this.columnQTextBox = new System.Windows.Forms.TextBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // xlsxPathTextBox
            // 
            this.xlsxPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xlsxPathTextBox.Location = new System.Drawing.Point(12, 12);
            this.xlsxPathTextBox.Name = "xlsxPathTextBox";
            this.xlsxPathTextBox.ReadOnly = true;
            this.xlsxPathTextBox.Size = new System.Drawing.Size(648, 20);
            this.xlsxPathTextBox.TabIndex = 0;
            // 
            // openXlsButton
            // 
            this.openXlsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openXlsButton.Location = new System.Drawing.Point(666, 10);
            this.openXlsButton.Name = "openXlsButton";
            this.openXlsButton.Size = new System.Drawing.Size(106, 23);
            this.openXlsButton.TabIndex = 1;
            this.openXlsButton.Text = "Open XLS";
            this.openXlsButton.UseVisualStyleBackColor = true;
            this.openXlsButton.Click += new System.EventHandler(this.openXlsButton_Click);
            // 
            // openCsvButton
            // 
            this.openCsvButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openCsvButton.Location = new System.Drawing.Point(666, 47);
            this.openCsvButton.Name = "openCsvButton";
            this.openCsvButton.Size = new System.Drawing.Size(106, 23);
            this.openCsvButton.TabIndex = 3;
            this.openCsvButton.Text = "Save as CSV";
            this.openCsvButton.UseVisualStyleBackColor = true;
            this.openCsvButton.Click += new System.EventHandler(this.openCsvButton_Click);
            // 
            // csvPathTextBox
            // 
            this.csvPathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.csvPathTextBox.Location = new System.Drawing.Point(12, 49);
            this.csvPathTextBox.Name = "csvPathTextBox";
            this.csvPathTextBox.ReadOnly = true;
            this.csvPathTextBox.Size = new System.Drawing.Size(648, 20);
            this.csvPathTextBox.TabIndex = 2;
            // 
            // columnCLabel
            // 
            this.columnCLabel.AutoSize = true;
            this.columnCLabel.Location = new System.Drawing.Point(51, 105);
            this.columnCLabel.Name = "columnCLabel";
            this.columnCLabel.Size = new System.Drawing.Size(117, 13);
            this.columnCLabel.TabIndex = 4;
            this.columnCLabel.Text = "Store Category Number";
            // 
            // columnLLabel
            // 
            this.columnLLabel.AutoSize = true;
            this.columnLLabel.Location = new System.Drawing.Point(51, 132);
            this.columnLLabel.Name = "columnLLabel";
            this.columnLLabel.Size = new System.Drawing.Size(50, 13);
            this.columnLLabel.TabIndex = 4;
            this.columnLLabel.Text = "Zip Code";
            // 
            // columnQLabel
            // 
            this.columnQLabel.AutoSize = true;
            this.columnQLabel.Location = new System.Drawing.Point(51, 160);
            this.columnQLabel.Name = "columnQLabel";
            this.columnQLabel.Size = new System.Drawing.Size(108, 13);
            this.columnQLabel.TabIndex = 4;
            this.columnQLabel.Text = "Paypal Email Address";
            // 
            // columnBTextBox
            // 
            this.columnBTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.columnBTextBox.Location = new System.Drawing.Point(173, 76);
            this.columnBTextBox.Name = "columnBTextBox";
            this.columnBTextBox.Size = new System.Drawing.Size(556, 20);
            this.columnBTextBox.TabIndex = 5;
            // 
            // columnCTextBox
            // 
            this.columnCTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.columnCTextBox.Location = new System.Drawing.Point(173, 102);
            this.columnCTextBox.Name = "columnCTextBox";
            this.columnCTextBox.Size = new System.Drawing.Size(556, 20);
            this.columnCTextBox.TabIndex = 5;
            // 
            // columnLTextBox
            // 
            this.columnLTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.columnLTextBox.Location = new System.Drawing.Point(172, 129);
            this.columnLTextBox.Name = "columnLTextBox";
            this.columnLTextBox.Size = new System.Drawing.Size(556, 20);
            this.columnLTextBox.TabIndex = 5;
            // 
            // columnQTextBox
            // 
            this.columnQTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.columnQTextBox.Location = new System.Drawing.Point(172, 157);
            this.columnQTextBox.Name = "columnQTextBox";
            this.columnQTextBox.Size = new System.Drawing.Size(556, 20);
            this.columnQTextBox.TabIndex = 5;
            // 
            // convertButton
            // 
            this.convertButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.convertButton.Location = new System.Drawing.Point(163, 189);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(475, 30);
            this.convertButton.TabIndex = 6;
            this.convertButton.Text = "Generate CSV";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(51, 79);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(116, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Ebay Category Number";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ConvertXlsxToCsvForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 231);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.columnQTextBox);
            this.Controls.Add(this.columnLTextBox);
            this.Controls.Add(this.columnCTextBox);
            this.Controls.Add(this.columnBTextBox);
            this.Controls.Add(this.columnQLabel);
            this.Controls.Add(this.columnLLabel);
            this.Controls.Add(this.columnCLabel);
            this.Controls.Add(this.openCsvButton);
            this.Controls.Add(this.csvPathTextBox);
            this.Controls.Add(this.openXlsButton);
            this.Controls.Add(this.xlsxPathTextBox);
            this.MaximumSize = new System.Drawing.Size(800, 270);
            this.MinimumSize = new System.Drawing.Size(600, 270);
            this.Name = "ConvertXlsxToCsvForm";
            this.Text = "Convert XLS To CSV";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox xlsxPathTextBox;
        private System.Windows.Forms.Button openXlsButton;
        private System.Windows.Forms.Button openCsvButton;
        private System.Windows.Forms.TextBox csvPathTextBox;
        private System.Windows.Forms.Label columnCLabel;
        private System.Windows.Forms.Label columnLLabel;
        private System.Windows.Forms.Label columnQLabel;
        private System.Windows.Forms.TextBox columnBTextBox;
        private System.Windows.Forms.TextBox columnCTextBox;
        private System.Windows.Forms.TextBox columnLTextBox;
        private System.Windows.Forms.TextBox columnQTextBox;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
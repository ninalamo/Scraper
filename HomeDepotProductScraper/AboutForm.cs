using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeDepotProductScraper
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void AboutForm_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath path = new GraphicsPath();
            int num = 25;
            if (num % 10 != 0)
                checked { num -= unchecked(num % 10); }
            Rectangle rect1 = new Rectangle(0, checked(this.Height - num), num, num);
            Rectangle rect2 = new Rectangle(checked(this.Width - num + 1), checked(this.Height - num), num, num);
            path.AddArc(0, 0, num, num, 180f, 90f);
            path.AddArc(checked(this.Width - num + 1), 0, num, num, 270f, 90f);
            path.AddRectangle(new Rectangle(0, checked((int)Math.Round(unchecked((double)num / 2.0))), this.Width, checked(this.Height - num)));
            path.AddArc(rect1, -270f, 90f);
            path.AddArc(rect2, 360f, 90f);
            this.Region = new Region(path);
        }

        private void lnkCompany_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://patrikchovan.com");
        }

        private void lnkWeb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://patrikchovan.com");
        }

        private void lnkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("mailto:admin@patrikchovan.com");
        }
    }
}

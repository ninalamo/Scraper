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
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace HomeDepotProductScraper
{
    public partial class ConvertXlsxToCsvForm : Form
    {
        public ConvertXlsxToCsvForm()
        {
            InitializeComponent();
        }

        private void openXlsButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.xlsx) | *.xlsx;";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (openFileDialog.ShowDialog() == DialogResult.OK) xlsxPathTextBox.Text = openFileDialog.FileName;
        }

        private void openCsvButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Csv files (*.csv) | *.csv;";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (saveFileDialog.ShowDialog() == DialogResult.OK) csvPathTextBox.Text = saveFileDialog.FileName;
        }

        private void convertButton_Click(object sender, EventArgs e)
        {
            string xlsxPath = xlsxPathTextBox.Text;
            string csvPath = csvPathTextBox.Text;
            if (xlsxPath.Length == 0 || csvPath.Length == 0) return;
            StreamWriter sw = new StreamWriter(csvPath);
            sw.Write(string.Format("*Action(SiteID=US|Country=US|Currency=USD|Version=745),*Category,StoreCategory,*Title,*Description,*ConditionID,PicURL,*Quantity,*Format,*StartPrice,*Duration,PostalCode,C:Brand,C:MPN,Product:UPC,PayPalAccepted,PayPalEmailAddress,WeightMajor,WeightUnit,GlobalShipping,DispatchTimeMax,CustomLabel,C:Material,C:Color,ReturnsAcceptedOption,RefundOption,ReturnsWithinOption,ShippingCostPaidByOption,RestockingFeeValueOption,ShippingCarrierUsed,ShippingType,ShippingService-1:Cost,ShippingService-1:FreeShipping,ShippingService-1:Option\r\n"));

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(xlsxPath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;
            int[] cols = { 1, 2, 3, 4, 7, 10, 11, 12, 15, 19, 20, 21 };
            int colNum = cols.Length;
            for (int i = 1; ; i++)
            {
                string[] record = {
                    "Add", "", "", "", "", "", "", "3", "FixedPrice", "",
                    "GTC", "", "", "", "", "1", "", "", "lb", "1",
                    "3", "", "", "", "ReturnsAccepted", "MoneyBack", "Days_30", "Buyer", "Percent_10", "GlobalShipping_MultiCarrier",
                    "Flat", "", "1", "UPSGround", ""
                };
                bool ok = false;
                for (int j = 0; j < colNum; j++)
                {
                    var cell = xlRange.Cells[i, cols[j]].Value2;
                    record[cols[j] + 2] = cell != null ? Convert.ToString(cell) : "";
                    if (cell != null) ok = true;
                }
                if (!ok) break;
                record[1] = columnBTextBox.Text;
                record[2] = columnCTextBox.Text;
                record[11] = columnLTextBox.Text;
                record[16] = columnQTextBox.Text;
                for (int j = 0; j < 35; j++)
                {
                    sw.Write(string.Format("{0}{1}", record[j], j == 34 ? "\r\n" : ","));
                }
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            sw.Close();
            MessageBox.Show("Success");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://pages.ebay.com/sellerinformation/news/categorychanges.html");
        }
    }
}

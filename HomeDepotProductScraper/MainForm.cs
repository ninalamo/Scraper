using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Chilkat;
using System.IO;
using GemBox.Spreadsheet;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace HomeDepotProductScraper
{
    public partial class MainForm : Form
    {
        bool isProxyAppied = false;
        private string title = "Arbitrage Authority";
        private Dictionary<string, string> config;
        public List<string> oldLinks = new List<string>();
        public List<string> historyLinks = new List<string>();
        public BackgroundWorker bw = new BackgroundWorker();
        public string[] strArray1b = new string[100];
        public int lowerBound1, upperBound1, index33;
        public Ftp2 ftp;
        public bool isRestarted = false;
        public string strPass11 = "Salma121015";
        public int port = 0;

        public string[] nameArray;
        public string[] priceArray;

        public ExcelFile excelFile1;
        public ExcelWorksheet excelWorksheet1;

        public ExcelFile excelFile2;
        public ExcelWorksheet excelWorksheet3;

        string str8 = "";
        string strRefurb = "1000";
        string str2 = "";
        string str11 = "";
        string str12 = "";
        string str13 = "";
        string str14 = "";
        string str15 = "";
        string str16 = "";
        string str17 = "";
        string str18 = "";
        string str19 = "";
        string strOverstock = "";
        string deneme = "";

        public MainForm()
        {
            InitializeComponent();
            bw.DoWork += new DoWorkEventHandler(Scrape);

            int BrowserVer, RegVal;

            // get the installed IE version
            using (WebBrowser Wb = new WebBrowser())
                BrowserVer = Wb.Version.Major;

            // set the appropriate IE version
            if (BrowserVer >= 11)
                RegVal = 11001;
            else if (BrowserVer == 10)
                RegVal = 10001;
            else if (BrowserVer == 9)
                RegVal = 9999;
            else if (BrowserVer == 8)
                RegVal = 8888;
            else
                RegVal = 7000;

            // set the actual key
            RegistryKey Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION", true);
            Key.SetValue(System.Diagnostics.Process.GetCurrentProcess().ProcessName + ".exe", RegVal, RegistryValueKind.DWord);
            Key.Close();

            this.webBrowser.AllowNavigation = true;
            this.webBrowser.ScriptErrorsSuppressed = true;

            string[] links = null;
            string tempLinks = "";
            TextWriter tw;
            TextReader tr;
            if (File.Exists("history.txt"))
            {
                tr = new StreamReader("history.txt");
                tempLinks = tr.ReadToEnd();

                if (tempLinks.Contains("[link]"))
                {
                    links = tempLinks.Split(new string[] { "[link]" }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < links.Count(); i++)
                    {
                        historyLinks.Add(links[i]);
                        Console.WriteLine(links[i]);
                    }

                    webBrowser.Navigate(historyLinks[historyLinks.Count - 1]);
                    tr.Close();
                    tr.Dispose();

                    tw = new StreamWriter("history.txt", false); //true = append
                    tw.Write("");
                    tw.Close();
                    tw.Dispose();
                }

                if (File.Exists("windowSize.txt"))
                {
                    tr = new StreamReader("windowSize.txt");
                    string wSize = "3";
                    wSize = tr.ReadLine();

                    if (wSize.Equals("0"))
                        WindowState = FormWindowState.Minimized;

                    if (wSize.Equals("1"))
                        WindowState = FormWindowState.Normal;

                    if (wSize.Equals("2"))
                        WindowState = FormWindowState.Maximized;

                    tr.Close();
                    tr.Dispose();

                    tw = new StreamWriter("windowSize.txt", false); //true = append
                    tw.Write("3");
                    tw.Close();
                    tw.Dispose();
                }
            }
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

        private void SetStatus(string status)
        {
            lblStatus.Invoke((MethodInvoker)delegate
            {
                this.lblStatus.Text = status;
            });
            // Application.DoEvents();
            // System.Threading.Thread.Sleep(50);
        }

        private void ResetStatus()
        {
            lblStatus.Invoke((MethodInvoker)delegate
            {
                this.lblStatus.Text = "Ready.";
            });
            MemoryManagement.FlushMemory();
            // Application.DoEvents();
            // System.Threading.Thread.Sleep(50);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = this.title;
            LoadConfig();
            UseProxy();
            this.txtAddress.Text = "http://www.arbitrageauthority.com/program-home/";
            this.webBrowser.Navigate(this.txtAddress.Text);
        }

        public string fHTML2XML(string strHTML)
        {
            HtmlToXml htmlToXml = new HtmlToXml();
            htmlToXml.UnlockComponent("HtmlToXml87654321_165FE0A5mRR0");
            htmlToXml.DropCustomTags = true;
            htmlToXml.DropTextFormattingTags();
            htmlToXml.Nbsp = 0;
            htmlToXml.XmlCharset = "utf-8";
            htmlToXml.Html = strHTML;
            return htmlToXml.ToXml();
        }

        public string fHTML2TXT(string strHTML)
        {
            HtmlToText htmlToText = new HtmlToText();
            htmlToText.UnlockComponent("HtmlToXml87654321_165FE0A5mRR0");
            htmlToText.DecodeHtmlEntities = true;
            htmlToText.RightMargin = 9999;
            htmlToText.SuppressLinks = true;
            return htmlToText.ToText(strHTML);
        }

        private void saveWithJPGEGQuality(Bitmap objSRCBitmap, string strTargetFile)
        {
            ImageCodecInfo encoder1 = this.GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder encoder2 = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters encoderParams = new EncoderParameters(1);
            EncoderParameter encoderParameter = new EncoderParameter(encoder2, 50L);
            encoderParams.Param[0] = encoderParameter;
            objSRCBitmap.Save(strTargetFile, encoder1, encoderParams);
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] imageDecoders = ImageCodecInfo.GetImageDecoders();
            int index = 0;
            while (index < imageDecoders.Length)
            {
                ImageCodecInfo imageCodecInfo = imageDecoders[index];
                if (imageCodecInfo.FormatID == format.Guid)
                    return imageCodecInfo;
                checked { ++index; }
            }
            return (ImageCodecInfo)null;
        }

        private void Scrape_kirklands()
        {
            int startIndex1 = 0;
            string documentText = this.webBrowser.DocumentText;
            int num2 = 0;

            String lastsource = ((mshtml.HTMLDocumentClass)(((webBrowser.Document.DomDocument)))).documentElement.innerHTML;
            if (documentText.IndexOf("<h2>Ship to Home</h2>", 0) == -1)
            {
                if (MessageBox.Show("Ship to Home is not available! Would you like to continue?", "Ship to Home Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }

            if (this.txtTitle.Text.CompareTo("") != 0)
            {
                str2 = this.txtTitle.Text;
            }
            else if (documentText.IndexOf("<h1>", 0) != -1)
            {
                int index2 = checked(this.webBrowser.DocumentText.IndexOf("<h1>", 0) + "<h1>".Length);
                int index3 = this.webBrowser.DocumentText.IndexOf("</h1>", index2);
                title = this.webBrowser.DocumentText.Substring(index2, checked(index3 - index2)).Trim();
                title = System.Net.WebUtility.HtmlDecode(title);
                this.txtTitle.Text = title;
            }
            else
                str2 = "Title Not Found";
            Console.WriteLine("Str2: " + str2);

            string str3;
            if (documentText.IndexOf("description\">", 0) != -1)
            {
                startIndex1 = documentText.IndexOf("description\">", 0);
                num2 = checked(documentText.IndexOf("</font>", startIndex1));
                str3 = documentText.Substring(startIndex1, checked(num2 - startIndex1)).Trim();
                string xmlData = this.fHTML2XML(str3);
                Xml xml1 = new Xml();
                xml1.LoadXml(xmlData);
                str3 = xml1.AccumulateTagContent("text", "script");
            }
            else
                str3 = "";
            Console.WriteLine("Str3: " + str3);

            string str4;
            if (documentText.IndexOf("description\">", 0) != -1)
            {
                //int startIndex2 = documentText.IndexOf("class=\"bulletList\"", 0);
                startIndex1 = documentText.IndexOf("<ul", startIndex1);
                num2 = checked(documentText.IndexOf("</ul>", startIndex1) + "</ul>".Length);
                string xmlData = this.fHTML2XML(documentText.Substring(startIndex1, checked(num2 - startIndex1)).Trim());
                Xml xml1 = new Xml();
                xml1.LoadXml(xmlData);
                Xml xml2 = xml1.SearchForTag((Xml)null, "ul");
                str4 = "";
                int num3 = 0;
                int num4 = checked(xml2.NumChildren - 1);
                int index = num3;
                while (index <= num4)
                {
                    str4 = str4 + "• " + xml2.GetChild(index).GetChild(0).Content;
                    if (index != checked(xml2.NumChildren - 1))
                        str4 += Environment.NewLine;
                    checked { ++index; }
                }
                xml2.Dispose();
                xml1.Dispose();
            }
            else
                str4 = "Bullet List Not Found";
            Console.WriteLine("Str4: " + str4);

            // Dimension Not Found!
            string str5 = "";
            Console.WriteLine("Str5: " + str5);

            // Details Not Found!
            string str6 = "";
            Console.WriteLine("Str6: " + str6);

            // 
            string str7 = "N/A";
            Console.WriteLine("Str7: " + str7);

            if (documentText.IndexOf("productSku=\"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("productSku=\"", 0) + ("productSku=\"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                str8 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str8 = "SKU Not Found";
            Console.WriteLine("Str8: " + str8);

            string str10 = config["HTTP"];
            str11 = "1000px Images Not Found";
            str12 = "1000px Images Not Found";
            if (documentText.IndexOf("productAltImages_wrap", 0) != -1)
            {
                int index11 = documentText.IndexOf("productAltImages_wrap js_productAltImages_wrap", 0);
                int index12 = checked(documentText.IndexOf("<ul", index11));
                int index13 = documentText.IndexOf("</ul>", index12) + "</ul>".Length;

                string xmlData = this.fHTML2XML(documentText.Substring(index12, checked(index13 - index12)).Trim());
                Xml xml = new Xml();
                xml.LoadXml(xmlData);
                xml = xml.SearchForTag((Xml)null, "ul");
                int num4 = checked(xml.NumChildren - 1);
                int index = 0;
                string strImages = "";
                while (index <= num4)
                {
                    strImages += xml.GetChild(index).GetChild(0).GetAttrValue("src").Split('?')[0] + "|";
                    if (index != checked(xml.NumChildren - 1))
                        str4 += Environment.NewLine;
                    checked { ++index; }
                }

                str11 = strImages.Substring(0, checked(strImages.Length - 1));
                string[] strArray = str11.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                index = lowerBound;
                while (index <= upperBound)
                {
                    strArray[index] = str10 + "/" + str8 + "/" + str8 + "_" + Convert.ToString(checked(index + 1)) + ".jpg";
                    checked { ++index; }
                }
                str12 = string.Join("|", strArray);
            }
            Console.WriteLine("Str11: " + str11);
            Console.WriteLine("Str12: " + str12);

            if (documentText.IndexOf("price\">", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("price\">", 0) + "price\">".Length;
                int num3 = documentText.IndexOf("<", startIndex2);
                str13 = (Convert.ToDouble(documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim().Replace("$", "").Replace(",", "").Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) * 2.0).ToString("0.##");
            }
            else
                str13 = "Price Not Found";
            Console.WriteLine("Str13: " + str13);

            // Brand
            str14 = "Kirklands";
            Console.WriteLine("Str14: " + str14);

            // UPC
            str15 = "Does Not Apply";
            Console.WriteLine("Str15: " + str15);

            str16 = "Weight Not Found";
            Console.WriteLine("Str16: " + str16);

            // Color/Finish
            str17 = "";
            Console.WriteLine("Str17: " + str17);

            // Material
            str18 = "";
            Console.WriteLine("Str18: " + str18);

            str19 = string.Join(Environment.NewLine, (str3 + Environment.NewLine + str4).Split(new string[1]
              {
                Environment.NewLine
              }, StringSplitOptions.RemoveEmptyEntries)).Replace("[varNewLine]", Environment.NewLine).Replace(Environment.NewLine, "<BR>");
        }

        private void Scrape_walmart()
        {
            int startIndex1 = 0;
            string documentText = this.webBrowser.DocumentText;
            int num2 = 0;

            String lastsource = ((mshtml.HTMLDocumentClass)(((webBrowser.Document.DomDocument)))).documentElement.innerHTML;


            if (this.txtTitle.Text.CompareTo("") != 0)
            {
                str2 = this.txtTitle.Text;
            }
            else if (documentText.IndexOf("query\":\"", 0) != -1)
            {
                int index2 = checked(this.webBrowser.DocumentText.IndexOf("query\":\"", 0) + "query\":\"".Length);
                int index3 = this.webBrowser.DocumentText.IndexOf(Convert.ToChar(34), index2);
                title = this.webBrowser.DocumentText.Substring(index2, checked(index3 - index2)).Trim();
                title = System.Net.WebUtility.HtmlDecode(title);
                this.txtTitle.Text = title;
            }
            else
                str2 = "Title Not Found";
            Console.WriteLine("Str2: " + str2);

            string str3;
            if (documentText.IndexOf("js-ellipsis module", 0) != -1)
            {
                startIndex1 = documentText.IndexOf("js-ellipsis module", 0);
                num2 = checked(documentText.IndexOf("</div>", startIndex1) + "</div>".Length);
                str3 = documentText.Substring(startIndex1, checked(num2 - startIndex1)).Trim();
                string xmlData = this.fHTML2XML(str3);
                Xml xml1 = new Xml();
                xml1.LoadXml(xmlData);
                str3 = xml1.AccumulateTagContent("text", "script");
            }
            else
                str3 = "";
            Console.WriteLine("Str3: " + str3);

            string str4 = "";
            if (documentText.IndexOf("<tbody", startIndex1) != 0)
            {
                int startIndex3 = documentText.IndexOf("<tbody", startIndex1);
                num2 = checked(documentText.IndexOf("</tbody>", startIndex3) + "</tbody>".Length);
                string[] strArray = this.fHTML2TXT(documentText.Substring(startIndex3, checked(num2 - startIndex3)).Trim()).Split(new string[1]
          {
            Environment.NewLine
          }, StringSplitOptions.RemoveEmptyEntries);
                str4 = "";
                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                int index = lowerBound;
                while (index <= upperBound)
                {
                    str4 = str4 + "• " + strArray[index] + " : " + strArray[checked(index + 1)];
                    if (index != strArray.GetUpperBound(0))
                        str4 += Environment.NewLine;
                    checked { index += 2; }
                }

                int newindex1 = documentText.IndexOf("<table", num2);
                newindex1 = documentText.IndexOf("<table", newindex1 + 1);
                int newindex2 = checked(documentText.IndexOf("</table>", newindex1) + "</table>".Length);
                strArray = this.fHTML2TXT(documentText.Substring(newindex1, checked(newindex2 - newindex1)).Trim()).Split(new string[1]
          {
            Environment.NewLine
          }, StringSplitOptions.RemoveEmptyEntries);
                lowerBound = strArray.GetLowerBound(0);
                upperBound = strArray.GetUpperBound(0);
                index = lowerBound;
                while (index <= upperBound - 1)
                {
                    str4 = str4 + "• " + strArray[index] + " : " + strArray[checked(index + 1)];
                    if (index != strArray.GetUpperBound(0))
                        str4 += Environment.NewLine;
                    checked { index += 2; }
                }
            }
            else
                str4 = "Bullet List Not Found";
            Console.WriteLine("Str4: " + str4);

            // Dimension Not Found!
            string str5 = "";
            Console.WriteLine("Str5: " + str5);

            // Details Not Found!
            string str6 = "";
            Console.WriteLine("Str6: " + str6);

            // 
            string str7 = "N/A";
            Console.WriteLine("Str7: " + str7);


            if (documentText.IndexOf("itemId\":\"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("itemId\":\"", 0) + ("itemId\":\"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                str8 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str8 = "SKU Not Found";
            Console.WriteLine("Str8: " + str8);

            string str9 = "";
            string str10 = config["HTTP"];

            str11 = "1000px Images Not Found";
            str12 = "1000px Images Not Found";
            /*
            if (lastsource.IndexOf("spotlight-carousel-hotspot", 0) != -1)
            {
                int index11 = lastsource.IndexOf("spotlight-carousel-hotspot", 0);
                int index12 = checked(lastsource.IndexOf("<ol", index11));
                int index13 = lastsource.IndexOf("</ol>", index12) + "</ol>".Length;

                string xmlData = this.fHTML2XML(lastsource.Substring(index12, checked(index13 - index12)).Trim());
                Xml xml1 = new Xml();
                xml1.LoadXml(xmlData);
                Xml xml2 = xml1.SearchForTag((Xml)null, "ol");
                xml2 = xml2.SearchForAttribute((Xml)null, "div", "class", "slick-track");
                int num4 = checked(xml2.NumChildren - 1);
                int index = 0;
                string strImages = "";
                while (index <= num4)
                {
                    strImages += xml2.GetChild(index).GetChild(0).GetChild(0).GetAttrValue("data-hero-image") + "|";
                    if (index != checked(xml2.NumChildren - 1))
                        str4 += Environment.NewLine;
                    checked { ++index; }
                }

                str11 = strImages.Substring(0, checked(strImages.Length - 1));
                string[] strArray = str11.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                index = lowerBound;
                while (index <= upperBound)
                {
                    strArray[index] = str10 + "/" + str8 + "/" + str8 + "_" + Convert.ToString(checked(index + 1)) + ".jpg";
                    checked { ++index; }
                }
                str12 = string.Join("|", strArray);
            }
            Console.WriteLine("Str11: "+str11);
            Console.WriteLine("Str12: "+str12);
            */


            //Walmart Image New
            if (documentText.Contains("https://i5.walmartimages.com/asr/"))
            {
                str11 = "";
                str12 = "";
                string tmp = this.webBrowser.DocumentText;

                int urlIndex1, urlIndex2, urlIndex3, count = 0;
                urlIndex3 = tmp.IndexOf("env-info container container-responsive ResponsiveContainer");
                tmp = tmp.Substring(0, urlIndex3);
                string tmp2;
                string[] dizi;
                while (tmp.Contains("https://i5.walmartimages.com/asr/"))
                {
                    urlIndex1 = tmp.IndexOf("https://i5.walmartimages.com/asr/");
                    tmp = tmp.Substring(urlIndex1);
                    urlIndex2 = tmp.IndexOf("\"");
                    tmp2 = tmp.Substring(0, urlIndex2);
                    if (urlIndex2 != null)
                        tmp = tmp.Substring(urlIndex2);
                    count++;

                    if (tmp2.Contains("https://i5.walmartimages.com/asr/"))
                    {
                        tmp2 = tmp2.Substring(0, tmp2.IndexOf(".jpeg") + 5);
                        tmp2 += "?odnHeight=1000&odnWidth=1000&odnBg=FFFFFF";
                        str11 += tmp2;
                        str11 += "|";
                    }

                }
                dizi = str11.Split('|');
                str11 = "";
                for (int diziIndex = 0; diziIndex < dizi.Length - 1; diziIndex++)
                {
                    for (int j = diziIndex + 1; j < dizi.Length; j++)
                    {
                        if (dizi[diziIndex].ToLower().Equals(dizi[j].ToLower())) // If same images
                            dizi[j] = "";

                    }
                }

                for (int k = 0; k < dizi.Length; k++)
                {
                    if (!dizi[k].Equals("") && !dizi[k].Equals(null))
                    {
                        str11 += dizi[k];
                        str11 += "|";
                    }


                }


                string[] strArray = str11.Split('|');

                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                int index = lowerBound;
                while (index <= upperBound)
                {
                    if (!strArray[index].Equals("") && !strArray[index].Equals(null))
                        strArray[index] = str10 + "/" + str8 + "/" + str8 + "_" + Convert.ToString(checked(index + 1)) + ".jpg";
                    checked { ++index; }
                }
                str12 = string.Join("|", strArray);
                if (str12[str12.Length - 1].Equals("|"))
                    str12 = str12.Substring(0, str12.Length - 1);



                Console.WriteLine("Str11: " + str11);
                Console.WriteLine("Str12: " + str12);
                if (documentText.IndexOf("price\":", 0) != -1)
                {
                    int startIndex2 = documentText.IndexOf("price\":", 0) + "price\":".Length;
                    int num3 = documentText.IndexOf(',', startIndex2);
                    str13 = (Convert.ToDouble(documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim().Replace("$", "").Replace(",", "").Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) * 2.0).ToString("0.##");
                }
                else
                    str13 = "Price Not Found";
                Console.WriteLine("Str13: " + str13);

                if (documentText.IndexOf("brand\":\"", 0) != -1)
                {
                    int startIndex2 = checked(documentText.IndexOf("brand\":\"", 0) + ("brand\":\"").Length);
                    int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                    str14 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
                }
                else
                    str14 = "Does Not Apply";
                Console.WriteLine("Str14: " + str14);


                if (documentText.IndexOf("upc\":\"", 0) != -1)
                {
                    int startIndex2 = checked(documentText.IndexOf("upc\":\"", 0) + ("upc\":\"").Length);
                    int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                    str14 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
                }
                else
                    str15 = "Does Not Apply";
                Console.WriteLine("Str15: " + str15);

                str16 = "Weight Not Found";
                Console.WriteLine("Str16: " + str16);

                // Color/Finish
                str17 = "";
                Console.WriteLine("Str17: " + str17);

                // Material
                str18 = "";
                Console.WriteLine("Str18: " + str18);

                str19 = string.Join(Environment.NewLine, (str3 + Environment.NewLine + str4).Split(new string[1]
              {
                Environment.NewLine
              }, StringSplitOptions.RemoveEmptyEntries)).Replace("[varNewLine]", Environment.NewLine).Replace(Environment.NewLine, "<BR>");
            }
        }

        private void Scrape_overstock()
        {
            int startIndex1 = 0;
            string documentText = this.webBrowser.DocumentText;
            int num2 = 0;
            strOverstock = "";
            string oTemp = "";
            string oTemp2 = "";

            if (documentText.Contains("<select class=\"options-dropdown\""))
            {
                oTemp = documentText.Substring(documentText.IndexOf("<select class=\"options-dropdown\"") + 15);
                oTemp = oTemp.Substring(0, oTemp.IndexOf("<div class=\"row quantity-atc-section\">"));

                while (oTemp.Contains("class=\"\""))
                {
                    oTemp = oTemp.Substring(oTemp.IndexOf("class=\"\"") + 9);
                    oTemp2 = oTemp.Substring(0, oTemp.IndexOf("</option>"));
                    //oTemp2 = oTemp2.Replace(((char)16).ToString(),"");
                    //oTemp2 = oTemp2.Replace(((char)19).ToString(), "");
                    oTemp2 = oTemp2.Replace(">", "");
                    oTemp2 = oTemp2.Replace("\n\r", "");
                    oTemp2 = oTemp2.Replace("\r\n", "");
                    oTemp2 = oTemp2.Replace("\n", "");
                    oTemp2 = oTemp2.Replace("\r", "");
                    oTemp2 = oTemp2.Replace("\"", "");
                    oTemp2 = oTemp2.Replace(Environment.NewLine, "");
                    deneme += oTemp2 + "fatih_kose";
                    oTemp2 = oTemp2.Trim();
                    oTemp2 = oTemp2.Replace("                                                ", " ");
                    oTemp2 = oTemp2.Replace("- $", "-$");

                    strOverstock += oTemp2 + "fatih_kose";
                    Console.WriteLine(oTemp2);
                    oTemp = oTemp.Substring(oTemp.IndexOf("</option>")); // 2 = sdfasf

                }

                string[] dizi = deneme.Split(new string[1] { " " }, StringSplitOptions.RemoveEmptyEntries);

            }

            String lastsource = ((mshtml.HTMLDocumentClass)(((webBrowser.Document.DomDocument)))).documentElement.innerHTML;

            str2 = "";
            if (this.txtTitle.Text.CompareTo("") != 0)
            {
                str2 = this.txtTitle.Text;
            }
            else if (documentText.IndexOf("class=\"product-title\"", 0) != -1)
            {
                int index01 = this.webBrowser.DocumentText.IndexOf("class=\"product-title\"", 0);
                int index2 = checked(this.webBrowser.DocumentText.IndexOf("<h1>", index01) + "<h1>".Length);
                int index3 = this.webBrowser.DocumentText.IndexOf("</", index2);
                str2 = this.webBrowser.DocumentText.Substring(index2, checked(index3 - index2)).Trim();
                str2 = System.Net.WebUtility.HtmlDecode(str2);
                str2 = str2.Substring(0, Math.Min(80, str2.Length));
            }
            else
                str2 = "Title Not Found";
            Console.WriteLine("Str2: " + str2);

            string str3;
            if (documentText.IndexOf("<span itemprop=\"description", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("<span itemprop=\"description", 0);
                num2 = checked(documentText.IndexOf("</span>", startIndex2) + "</span>".Length);
                str3 = documentText.Substring(startIndex2, checked(num2 - startIndex2)).Trim();
                string xmlData = this.fHTML2XML(str3);
                Xml xml1 = new Xml();
                xml1.LoadXml(xmlData);
                str3 = xml1.AccumulateTagContent("text", "script");
            }
            else
                str3 = "";
            Console.WriteLine("Str3: " + str3);

            string str4 = "";
            if (documentText.IndexOf("<tbody", startIndex1) != 0)
            {
                int startIndex3 = documentText.IndexOf("<tbody", startIndex1);
                num2 = checked(documentText.IndexOf("</tbody>", startIndex3) + "</tbody>".Length);
                string[] strArray = this.fHTML2TXT(documentText.Substring(startIndex3, checked(num2 - startIndex3)).Trim()).Split(new string[1]
          {
            Environment.NewLine
          }, StringSplitOptions.RemoveEmptyEntries);
                str4 = "";
                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                int index = lowerBound;
                while (index <= upperBound - 1)
                {
                    str4 = str4 + "• " + strArray[index] + " : " + strArray[checked(index + 1)];
                    if (index != strArray.GetUpperBound(0))
                        str4 += Environment.NewLine;
                    checked { index += 2; }
                }

                int newindex1 = documentText.IndexOf("<table", num2);
                newindex1 = documentText.IndexOf("<table", newindex1 + 1);
                int newindex2 = checked(documentText.IndexOf("</table>", newindex1) + "</table>".Length);
                strArray = this.fHTML2TXT(documentText.Substring(newindex1, checked(newindex2 - newindex1)).Trim()).Split(new string[1]
          {
            Environment.NewLine
          }, StringSplitOptions.RemoveEmptyEntries);
                lowerBound = strArray.GetLowerBound(0);
                upperBound = strArray.GetUpperBound(0);
                index = lowerBound;
                while (index <= upperBound - 1)
                {
                    str4 = str4 + "• " + strArray[index] + " : " + strArray[checked(index + 1)];
                    if (index != strArray.GetUpperBound(0))
                        str4 += Environment.NewLine;
                    checked { index += 2; }
                }
            }
            else
                str4 = "Bullet List Not Found";
            Console.WriteLine("Str4: " + str4);

            // Dimension Not Found!
            string str5 = "";
            Console.WriteLine("Str5: " + str5);

            // Details Not Found!
            string str6 = "";
            Console.WriteLine("Str6: " + str6);

            // 
            string str7 = "N/A";
            Console.WriteLine("Str7: " + str7);

            //productId: "9994515"

            if (documentText.IndexOf("productId: \"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("productId: \"", 0) + ("productId: \"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                str8 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str8 = "SKU Not Found";
            Console.WriteLine("Str8: " + str8);

            if (documentText.Contains("<i class=\"os-icon os-icon-transfer-3\"></i> Refurbished"))
            {
                strRefurb = "2000";
            }

            string str9 = "";
            string str10 = config["HTTP"];

            str11 = "1000px Images Not Found";
            str12 = "1000px Images Not Found";
            if (documentText.IndexOf("div class=\"thumbs featured cf\"", 0) != -1)
            {
                int index11 = documentText.IndexOf("div class=\"thumbs featured cf\"", 0);
                int index12 = checked(documentText.IndexOf("<ul", index11));
                int index13 = documentText.IndexOf("</ul>", index12) + "</ul>".Length;

                string xmlData = this.fHTML2XML(documentText.Substring(index12, checked(index13 - index12)).Trim());
                Xml xml1 = new Xml();
                xml1.LoadXml(xmlData);
                Xml xml2 = xml1.SearchForTag((Xml)null, "ul");
                int num4 = checked(xml2.NumChildren - 1);
                int index = 0;
                string strImages = "";
                while (index <= num4)
                {
                    strImages += xml2.GetChild(index).GetAttrValue("data-max-img") + "|";
                    if (index != checked(xml2.NumChildren - 1))
                        str4 += Environment.NewLine;
                    checked { ++index; }
                }

                str11 = strImages.Substring(0, checked(strImages.Length - 1));
                str11 = str11.Replace("_600.", "_1000.");
                string[] strArray = str11.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                index = lowerBound;
                while (index <= upperBound)
                {
                    strArray[index] = str10 + "/" + str8 + "/" + str8 + "_" + Convert.ToString(checked(index + 1)) + ".jpg";
                    checked { ++index; }
                }
                str12 = string.Join("|", strArray);
                str12 = str12.Replace("_600.j", "_1000.j");
            }
            Console.WriteLine("Str11: " + str11);
            Console.WriteLine("Str12: " + str12);

            //string str13;
            if (documentText.IndexOf("class=\"monetary-price-value\"", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("class=\"monetary-price-value\"", 0);
                int startIndex9 = checked(documentText.IndexOf(">", startIndex2) + 1);
                int num3 = documentText.IndexOf("</", startIndex9);
                str13 = (Convert.ToDouble(documentText.Substring(startIndex9, checked(num3 - startIndex9)).Trim().Replace("$", "").Replace(",", "").Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) * 2.0).ToString("0.##");
            }
            else
                str13 = "Price Not Found";
            Console.WriteLine("Str13: " + str13);

            //string str14;
            if (documentText.IndexOf("brand\":\"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("brand\":\"", 0) + ("brand\":\"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                str14 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str14 = "Does Not Apply";
            Console.WriteLine("Str14: " + str14);

            // UPC
            str15 = "Does Not Apply";
            Console.WriteLine("Str15: " + str15);

            str16 = "Weight Not Found";
            Console.WriteLine("Str16: " + str16);

            // Color/Finish
            str17 = "";
            Console.WriteLine("Str17: " + str17);

            // Material
            str18 = "";
            Console.WriteLine("Str18: " + str18);

            str19 = string.Join(Environment.NewLine, (str3 + Environment.NewLine + str4).Split(new string[1]
              {
                Environment.NewLine
              }, StringSplitOptions.RemoveEmptyEntries)).Replace("[varNewLine]", Environment.NewLine).Replace(Environment.NewLine, "<BR>");
        }

        private void Scrape_grainger()
        {
            int startIndex1 = 0;
            string documentText = this.webBrowser.DocumentText;
            int num2 = 0;

            String lastsource = ((mshtml.HTMLDocumentClass)(((webBrowser.Document.DomDocument)))).documentElement.innerHTML;

            if (this.txtTitle.Text.CompareTo("") != 0)
            {
                str2 = this.txtTitle.Text;
            }
            else if (documentText.IndexOf("class=\"productName\"", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("class=\"productName\"", 0);
                startIndex1 = checked(documentText.IndexOf(">", startIndex2) + 1);
                num2 = documentText.IndexOf("</", startIndex1);
                str2 = documentText.Substring(startIndex1, checked(num2 - startIndex1)).Trim();
                str2 = str2.Substring(0, Math.Min(80, str2.Length));
            }
            else
                str2 = "Title Not Found";
            Console.WriteLine("Str2: " + str2);

            string str3;
            if (documentText.IndexOf("class=\"copyTextSection\"", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("class=\"copyTextSection\"", 0);
                startIndex1 = checked(documentText.IndexOf(">", startIndex2) + 1);
                num2 = documentText.IndexOf("</", startIndex1);
                str3 = documentText.Substring(startIndex1, checked(num2 - startIndex1)).Trim();
            }
            else
                str3 = "";
            Console.WriteLine("Str3: " + str3);

            string str4;
            if (documentText.IndexOf("class=\"techSpecsTable\"", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("class=\"techSpecsTable\"", 0);
                startIndex1 = documentText.IndexOf("<ul ", startIndex2);
                num2 = checked(documentText.IndexOf("</ul>", startIndex1) + "</ul>".Length);
                string xmlData = this.fHTML2XML(documentText.Substring(startIndex1, checked(num2 - startIndex1)).Trim());
                Xml xml1 = new Xml();
                xml1.LoadXml(xmlData);
                Xml xml2 = xml1.SearchForAttribute((Xml)null, "ul", "class", "column firstCol");
                str4 = "";
                int num4 = checked(xml2.NumChildren - 1);
                int index = 0;
                while (index <= num4)
                {
                    str4 = str4 + "• " + xml2.GetChild(index).GetChild(0).GetChild(0).Content + " : " + xml2.GetChild(index).GetChild(2).GetChild(0).Content;
                    if (index != checked(xml2.NumChildren - 1))
                        str4 += Environment.NewLine;
                    checked { ++index; }
                }

                startIndex2 = num2;
                startIndex1 = documentText.IndexOf("<ul ", startIndex2);
                num2 = checked(documentText.IndexOf("</ul>", startIndex1) + "</ul>".Length);
                xmlData = this.fHTML2XML(documentText.Substring(startIndex1, checked(num2 - startIndex1)).Trim());
                xml1 = new Xml();
                xml1.LoadXml(xmlData);
                xml2 = xml1.SearchForAttribute((Xml)null, "ul", "class", "column");
                str4 = "";
                num4 = checked(xml2.NumChildren - 1);
                index = 0;
                while (index <= num4)
                {
                    str4 = str4 + "• " + xml2.GetChild(index).GetChild(0).GetChild(0).Content + " : " + xml2.GetChild(index).GetChild(2).GetChild(0).Content;
                    if (index != checked(xml2.NumChildren - 1))
                        str4 += Environment.NewLine;
                    checked { ++index; }
                }

                xml2.Dispose();
                xml1.Dispose();
            }
            else
                str4 = "Bullet List Not Found";
            Console.WriteLine("Str4: " + str4);

            // Dimension Not Found!
            string str5 = "";
            Console.WriteLine("Str5: " + str5);

            // Details Not Found!
            string str6 = "";
            Console.WriteLine("Str6: " + str6);

            // 
            string str7 = "N/A";
            Console.WriteLine("Str7: " + str7);


            if (documentText.IndexOf("ProductId\":\"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("ProductId\":\"", 0) + ("ProductId\":\"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                str8 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str8 = "SKU Not Found";
            Console.WriteLine("Str8: " + str8);

            string str9 = "";
            string str10 = config["HTTP"];
            str11 = "1000px Images Not Found";
            str12 = "1000px Images Not Found";
            if (documentText.IndexOf("class=\"carouselProductLists carouselItemList\"", 0) != -1)
            {
                int index11 = documentText.IndexOf("class=\"carouselContainer\"", 0);
                int index12 = checked(documentText.IndexOf("<ul", index11));
                int index13 = documentText.IndexOf("</ul>", index12) + "</ul>".Length;

                string xmlData = this.fHTML2XML(documentText.Substring(index12, checked(index13 - index12)).Trim());
                Xml xml1 = new Xml();
                xml1.LoadXml(xmlData);
                Xml xml2 = xml1.SearchForTag((Xml)null, "ul");
                int num4 = checked(xml2.NumChildren - 1);
                int index = 0;
                string strImages = "";
                while (index <= num4)
                {
                    if (xml2.GetChild(index).GetAttrValue("data-type") == "prodImage")
                    {
                        string tempImg = xml2.GetChild(index).GetChild(0).GetAttrValue("data-src");
                        strImages += tempImg.Substring(2, tempImg.IndexOf('?')) + "|";
                    }
                    if (index != checked(xml2.NumChildren - 1))
                        str4 += Environment.NewLine;
                    checked { ++index; }
                }

                str11 = strImages.Substring(0, checked(strImages.Length - 1));
                string[] strArray = str11.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);

                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                index = lowerBound;
                while (index <= upperBound)
                {
                    strArray[index] = str10 + "/" + str8 + "/" + str8 + "_" + Convert.ToString(checked(index + 1)) + ".jpg";
                    checked { ++index; }
                }
                str12 = string.Join("|", strArray);
            }
            else if (documentText.IndexOf("id=\"mainImageZoom\"", 0) != -1)
            {
                int index11 = documentText.IndexOf("id=\"mainImageZoom\"", 0);
                int index12 = checked(documentText.LastIndexOf("<img src=\"//", index11) + "<img src=\"//".Length);
                int index13 = documentText.IndexOf("?", index12);
                str11 = documentText.Substring(index12, checked(index13 - index12)).Trim();
                str12 = str10 + "/" + str8 + "/" + str8 + "_1.jpg";
            }
            Console.WriteLine("Str11: " + str11);
            Console.WriteLine("Str12: " + str12);


            if (documentText.IndexOf("class=\"gcprice-value\"", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("class=\"gcprice-value\"", 0);
                int startIndex9 = checked(documentText.IndexOf(">", startIndex2) + 1);
                int num3 = documentText.IndexOf("</", startIndex9);
                str13 = (Convert.ToDouble(documentText.Substring(startIndex9, checked(num3 - startIndex9)).Trim().Replace("$", "").Replace(",", "").Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) * 2.0).ToString("0.##");
            }
            else
                str13 = "Price Not Found";
            Console.WriteLine("Str13: " + str13);


            if (documentText.IndexOf("BrandName\":\"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("BrandName\":\"", 0) + ("BrandName\":\"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                str14 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str14 = "Does Not Apply";
            Console.WriteLine("Str14: " + str14);

            // UPC
            str15 = "Does Not Apply";
            Console.WriteLine("Str15: " + str15);

            str16 = "";
            if (documentText.IndexOf("class=\"shippingWeight\"", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("class=\"shippingWeight\"", 0);
                int startIndex9 = checked(documentText.IndexOf(">", startIndex2) + 1);
                int num3 = documentText.IndexOf("</", startIndex9);
                str16 = documentText.Substring(startIndex9, checked(num3 - startIndex9));
                str16 = str16.Replace("Shipping Weight <span class=\"productInfoValue\">", "");
                str16 = str16.Replace(" ", "");
                str16 = str16.Replace("\"", "");
                str16 = str16.Replace("\t", "");
                str16 = str16.Replace("\n\r", "");
                str16 = str16.Replace("\r\n", "");
                str16 = str16.Replace("\n", "");
                str16 = str16.Replace("\r", "");
                str16 = str16.Replace("lbs.", " lbs");
                /*
                int in1 = str13.IndexOf("class=\"productInfoValue\"", 0);
                int in2 = checked(str13.IndexOf(">", in1) + 1);
                str16 = str13.Substring(in2, checked(str13.Length - 1 - in2));
                str16 = Math.Ceiling(Convert.ToDouble(str13.Split(' ')[0])).ToString();
                 */
            }
            else
                str16 = "Weight Not Found";
            Console.WriteLine("Str16: " + str16);

            // Color/Finish
            str17 = "";
            Console.WriteLine("Str17: " + str17);

            // Material
            str18 = "";
            Console.WriteLine("Str18: " + str18);

            str19 = string.Join(Environment.NewLine, (str3 + Environment.NewLine + str4).Split(new string[1]
              {
                Environment.NewLine
              }, StringSplitOptions.RemoveEmptyEntries)).Replace("[varNewLine]", Environment.NewLine).Replace(Environment.NewLine, "<BR>");
        }

        private void Scrape_homedepot()
        {
            int startIndex1 = 0;
            string documentText = this.webBrowser.DocumentText;
            int num2 = 0;

            String lastsource = ((mshtml.HTMLDocumentClass)(((webBrowser.Document.DomDocument)))).documentElement.innerHTML;
            if (lastsource.IndexOf("We'll Ship It to You", 0) == -1)
            {
                if (MessageBox.Show("Ship to Home is not available! Would you like to continue?", "Ship to Home Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;
            }


            if (this.txtTitle.Text.CompareTo("") != 0)
            {
                str2 = this.txtTitle.Text;
            }
            else if (documentText.IndexOf("class=\"product_title\"", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("class=\"product_title\"", 0);
                startIndex1 = checked(documentText.IndexOf(">", startIndex2) + 1);
                num2 = documentText.IndexOf("</", startIndex1);
                str2 = documentText.Substring(startIndex1, checked(num2 - startIndex1)).Trim();
                str2 = str2.Substring(0, Math.Min(80, str2.Length));
            }
            else
                str2 = "Title Not Found";
            Console.WriteLine("Str2: " + str2);

            string str3;
            if (documentText.IndexOf("p itemprop=\"description\"", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("p itemprop=\"description\"", 0);
                startIndex1 = checked(documentText.IndexOf(">", startIndex2) + 1);
                num2 = documentText.IndexOf("</", startIndex1);
                str3 = documentText.Substring(startIndex1, checked(num2 - startIndex1)).Trim();
            }
            else
                str3 = "";
            Console.WriteLine("Str3: " + str3);

            string str4;
            if (documentText.IndexOf("class=\"bulletList\"", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("class=\"bulletList\"", 0);
                startIndex1 = documentText.LastIndexOf("<ul ", startIndex2);
                num2 = checked(documentText.IndexOf("</ul>", startIndex1) + "</ul>".Length);
                string xmlData = this.fHTML2XML(documentText.Substring(startIndex1, checked(num2 - startIndex1)).Trim());
                Xml xml1 = new Xml();
                xml1.LoadXml(xmlData);
                Xml xml2 = xml1.SearchForAttribute((Xml)null, "ul", "class", "bulletList");
                str4 = "";
                int num3 = 0;
                int num4 = checked(xml2.NumChildren - 1);
                int index = num3;
                while (index <= num4)
                {
                    str4 = str4 + "• " + xml2.GetChild(index).GetChild(0).Content;
                    if (index != checked(xml2.NumChildren - 1))
                        str4 += Environment.NewLine;
                    checked { ++index; }
                }
                xml2.Dispose();
                xml1.Dispose();
            }
            else if (documentText.IndexOf("class=\"main_description", 0) != -1)
            {
                str4 = "";
                HtmlElementCollection divs = this.webBrowser.Document.GetElementsByTagName("div");
                foreach (HtmlElement div in divs)
                {
                    if (div.GetAttribute("className").Contains("main_description"))
                    {
                        var ul = div.Children[1];
                        foreach (HtmlElement li in ul.Children)
                        {
                            str4 = str4 + "• " + li.InnerText;
                        }
                        str4 += Environment.NewLine;
                        continue;
                    }
                }
            }
            else
                str4 = "Bullet List Not Found";
            Console.WriteLine("Str4: " + str4);

            int startIndex3 = documentText.IndexOf("<table", startIndex1);
            string str5;
            if (startIndex3 != -1)
            {
                num2 = checked(documentText.IndexOf("</table>", startIndex3) + "</table>".Length);
                string[] strArray = this.fHTML2TXT(documentText.Substring(startIndex3, checked(num2 - startIndex3)).Trim()).Split(new string[1]
          {
            Environment.NewLine
          }, StringSplitOptions.RemoveEmptyEntries);
                str5 = "";
                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                int index = lowerBound;
                while (index <= upperBound)
                {
                    if (checked(index + 1) > upperBound)
                    {
                        str5 = str5 + "• " + strArray[index];
                    }
                    else
                    {
                        str5 = str5 + "• " + strArray[index] + " : " + strArray[checked(index + 1)];
                    }

                    if (index != strArray.GetUpperBound(0))
                        str5 += Environment.NewLine;
                    checked { index += 2; }
                }
            }
            else
                str5 = "Dimension Not Found!";
            Console.WriteLine("Str5: " + str5);

            int startIndex4 = checked(num2 + 1);
            int startIndex5 = documentText.IndexOf("<table", startIndex4);
            string str6;
            if (startIndex5 != -1)
            {
                num2 = checked(documentText.IndexOf("</table>", startIndex5) + "</table>".Length);
                string[] strArray = this.fHTML2TXT(documentText.Substring(startIndex5, checked(num2 - startIndex5)).Trim()).Split(new string[1]
          {
            Environment.NewLine
          }, StringSplitOptions.RemoveEmptyEntries);
                str6 = "";
                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                int index = lowerBound;
                while (index <= upperBound)
                {
                    if (strArray[index].IndexOf("Returnable") == -1)
                    {
                        str6 = str6 + "• " + strArray[index] + " : " + strArray[checked(index + 1)];
                        if (index != strArray.GetUpperBound(0))
                            str6 += Environment.NewLine;
                    }
                    else
                    {
                        string days = strArray[checked(index + 1)].Split(new string[1] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0];
                        int ndays = 0;
                        Int32.TryParse(days, out ndays);
                        string output = "";

                        if (ndays >= 30)
                        {
                            output = "30 Days";
                        }
                        else if (ndays >= 14 && ndays < 30)
                        {
                            output = "14 Days";
                        }
                        else if (ndays < 14)
                        {
                            output = "Not Returnable";
                        }

                        str6 = str6 + "• Returnable : " + output;
                        if (index != strArray.GetUpperBound(0))
                            str6 += Environment.NewLine;
                    }
                    checked { index += 2; }
                }
            }
            else
                str6 = "Details Not Found!";
            Console.WriteLine("Str6: " + str6);

            int startIndex6 = checked(num2 + 1);
            int startIndex7 = documentText.IndexOf("<table", startIndex6);
            string str7;
            if (startIndex7 != -1)
            {
                int num3 = checked(documentText.IndexOf("</table>", startIndex7) + "</table>".Length);
                string[] strArray = this.fHTML2TXT(documentText.Substring(startIndex7, checked(num3 - startIndex7)).Trim()).Split(new string[1]
          {
            Environment.NewLine
          }, StringSplitOptions.RemoveEmptyEntries);
                str7 = "";
                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                int index = lowerBound;
                while (index <= upperBound)
                {
                    str7 = str7 + "• " + strArray[index] + " : " + strArray[checked(index + 1)];
                    if (index != strArray.GetUpperBound(0))
                        str7 += Environment.NewLine;
                    checked { index += 2; }
                }
            }
            else
                str7 = "N/A";
            Console.WriteLine("Str7: " + str7);

            if (documentText.IndexOf("modelNumber\":\"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("modelNumber\":\"", 0) + ("modelNumber\":\"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                str8 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str8 = "SKU Not Found";
            Console.WriteLine("Str8: " + str8);

            string str9 = "";
            string str10 = config["HTTP"];

            int startIndex8 = 0;
            string Left1 = "";
            int num7;
            for (; documentText.IndexOf("_1000.jpg\",", startIndex8) != -1; startIndex8 = checked(num7 + 1))
            {
                int startIndex2 = documentText.IndexOf("_1000.jpg\",", startIndex8);
                num7 = checked(startIndex2 + "_1000.jpg".Length);
                int startIndex9 = documentText.LastIndexOf("http://", startIndex2);
                Left1 = Left1 + documentText.Substring(startIndex9, checked(num7 - startIndex9)).Trim() + "|";
            }

            if (Left1.CompareTo("") == 0)
            {
                str11 = "1000px Images Not Found";
                str12 = "1000px Images Not Found";
            }
            else
            {
                str11 = Left1.Substring(0, checked(Left1.Length - 1));
                string[] strArray = str11.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                int lowerBound = strArray.GetLowerBound(0);
                int upperBound = strArray.GetUpperBound(0);
                int index = lowerBound;
                while (index <= upperBound)
                {
                    strArray[index] = str10 + "/" + str8 + "/" + str8 + "_" + Convert.ToString(checked(index + 1)) + ".jpg";
                    checked { ++index; }
                }
                str12 = string.Join("|", strArray);
            }
            Console.WriteLine("Str11: " + str11);
            Console.WriteLine("Str12: " + str12);

            //string str13;
            if (documentText.IndexOf("itemprop=\"price\" content=", 0) != -1)
            {
                int startIndex2 = documentText.IndexOf("itemprop=\"price\" content=", 0);
                int startIndex9 = checked(documentText.IndexOf(">", startIndex2));
                int lengthForPrice = startIndex9 - startIndex2;
                string currentPrice = documentText.Substring(startIndex2, lengthForPrice);
                currentPrice = currentPrice.Replace("itemprop=\"price\" content=", "").Replace("\"","");                
                str13 = (Convert.ToDouble(currentPrice.Trim().Replace("$", "").Replace(",", "").Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)) * 2.0).ToString("0.##");
            }
            else
                str13 = "Price Not Found";
            Console.WriteLine("Str13: " + str13);

            if (documentText.IndexOf("brandName\":\"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("brandName\":\"", 0) + ("brandName\":\"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                str14 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str14 = "Does Not Apply";
            Console.WriteLine("Str14: " + str14);

            if (documentText.IndexOf("upc\":\"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("upc\":\"", 0) + ("upc\":\"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                str15 = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str15 = "Does Not Apply";
            Console.WriteLine("Str15: " + str15);

            if (documentText.IndexOf("Dotcom Shipping Carton Gross Weight (lb)\",\"value\":\"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("Dotcom Shipping Carton Gross Weight (lb)\",\"value\":\"", 0) + ("Dotcom Shipping Carton Gross Weight (lb)\",\"value\":\"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                string temp = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
                if (temp[0] == '.')
                    temp = temp.Replace(".", "0,");
                str16 = Math.Ceiling(Convert.ToDouble(temp)).ToString();
            }
            else
                str16 = "Weight Not Found";
            Console.WriteLine("Str16: " + str16);


            if (str6.IndexOf("Color/Finish  : ", 0) != -1)
            {
                int startIndex2 = checked(str6.IndexOf("Color/Finish  : ", 0) + "Color/Finish  : ".Length);
                int num3 = str6.IndexOf(Environment.NewLine, startIndex2);
                str17 = str6.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str17 = "";
            Console.WriteLine("Str17: " + str17);

            if (str6.IndexOf("Material  : ", 0) != -1)
            {
                int startIndex2 = checked(str6.IndexOf("Material  : ", 0) + "Material  : ".Length);
                int num3 = str6.IndexOf(Environment.NewLine, startIndex2);
                str18 = str6.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
            }
            else
                str18 = "";
            Console.WriteLine("Str18: " + str18);

            str19 = string.Join(Environment.NewLine, (str3 + Environment.NewLine + str4 + Environment.NewLine + "[varNewLine][varNewLine]DIMENSIONS" + Environment.NewLine + str5 + Environment.NewLine + "[varNewLine][varNewLine]DETAILS" + Environment.NewLine + str6 + Environment.NewLine + "[varNewLine][varNewLine]WARRANTY / CERTIFICATIONS" + Environment.NewLine + str7).Split(new string[1]
              {
                Environment.NewLine
              }, StringSplitOptions.RemoveEmptyEntries)).Replace("[varNewLine]", Environment.NewLine).Replace(Environment.NewLine, "<BR>");

            SetStatus("Checking excluded ship states...");
            if (documentText.IndexOf("excludedShipStates\":\"", 0) != -1)
            {
                int startIndex2 = checked(documentText.IndexOf("excludedShipStates\":\"", 0) + ("excludedShipStates\":\"").Length);
                int num3 = documentText.IndexOf(Convert.ToChar(34), startIndex2);
                string[] doc_states = documentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim().Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
                string[] all_states = File.ReadAllText("States.txt").Split(new string[1]
                {
                  Environment.NewLine
                }, StringSplitOptions.RemoveEmptyEntries);
                int index = 0;
                while (index < doc_states.Length)
                {
                    if (Array.IndexOf<string>(all_states, doc_states[index]) != -1)
                    {
                        if (MessageBox.Show("One or more states found in exclusion list! Would you like to continue?" + Environment.NewLine + Environment.NewLine + "Excluded States Are: " + string.Join(",", doc_states), "States Matched Exclution List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            ResetStatus();
                            return;
                        }
                        break;
                    }
                    checked { ++index; }
                }
            }
        }

        private void Scrape(object sender, EventArgs ev)
        {
            try
            {
                Console.WriteLine("test1");

                SpreadsheetInfo.SetLicense("ETZW-AT28-33Q6-1HAS");

                excelFile1 = ExcelFile.Load(config["Primary"]);
                excelWorksheet1 = excelFile1.Worksheets[0];

                excelFile2 = ExcelFile.Load(config["Secondary"]);
                excelWorksheet3 = excelFile2.Worksheets[0];
                Console.WriteLine("test2");

                SetStatus("Scraping...");

                webBrowser.Invoke((MethodInvoker)delegate
                {
                    if (this.webBrowser.DocumentText.CompareTo("") == 0)
                    {
                        MessageBox.Show("No webpage has been loaded!", "Empty Document Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ResetStatus();
                        return;
                    }
                });

                txtAddress.Invoke((MethodInvoker)delegate
                {
                    if (this.txtAddress.Text.Contains("grainger.com"))
                        Scrape_grainger();

                    if (this.txtAddress.Text.Contains("homedepot.com"))
                        Scrape_homedepot();

                    if (this.txtAddress.Text.Contains("overstock.com"))
                        Scrape_overstock();


                    if (this.txtAddress.Text.Contains("walmart.com"))
                        Scrape_walmart();

                    if (this.txtAddress.Text.Contains("kirklands.com"))
                        Scrape_kirklands();
                });

                SetStatus("Checking blacklisted brands...");
                string str1 = File.ReadAllText("Brands.txt");
                if (str1.ToLower().Contains(str14.ToLower())) // !=
                {
                    int num3 = (int)MessageBox.Show("This brand is in your blacklist!", "Black Listed Brand", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ResetStatus();
                    return;
                }

                SetStatus("Checking for SKU duplicates...");
                string[] arr_sku = File.ReadAllText(config["SKU"]).Split(new string[1] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                if (Array.IndexOf<string>(arr_sku, str8) != -1)
                {
                    MessageBox.Show("This SKU already exists!", "Duplicate SKU", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    ResetStatus();
                    return;
                }

                if (strOverstock.Equals(""))
                {
                    SetStatus("Updating SKU list...");
                    StreamWriter streamWriter = new StreamWriter(config["SKU"], false);
                    streamWriter.Write(string.Join(Environment.NewLine, arr_sku) + Environment.NewLine + str8);
                    streamWriter.Close();
                    streamWriter.Dispose();

                    SetStatus("Updating primary csv...");
                    /*
                        SpreadsheetInfo.SetLicense("ETZW-AT28-33Q6-1HAS");
                        ExcelFile excelFile1 = ExcelFile.Load(config["Primary"]);
                        ExcelWorksheet excelWorksheet1 = excelFile1.Worksheets[0];
                    */

                    int index1 = 0;

                    try
                    {
                        index1 = excelWorksheet1.GetUsedCellRange(true).LastRowIndex + 1;
                    }
                    catch (Exception e)
                    {
                        index1 = 0;
                    }

                    excelWorksheet1.Cells[index1, 3].SetValue(str2.Replace(",", " "));
                    excelWorksheet1.Cells[index1, 4].SetValue(str19.Replace(",", " "));
                    excelWorksheet1.Cells[index1, 5].SetValue(strRefurb.Replace(",", " "));

                    if (!Convert.ToBoolean(config["FTP"]))
                        excelWorksheet1.Cells[index1, 6].SetValue(str12.Replace(",", " "));
                    else
                        excelWorksheet1.Cells[index1, 6].SetValue(str11.Replace(",", " "));


                    excelWorksheet1.Cells[index1, 9].SetValue(str13.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Replace(" ", "").Replace("/", "").Replace("each", "").Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator));
                    excelWorksheet1.Cells[index1, 12].SetValue(str14.Replace(",", " "));
                    excelWorksheet1.Cells[index1, 13].SetValue(str8.Replace(",", " "));
                    excelWorksheet1.Cells[index1, 14].SetValue(str15.Replace(",", " "));
                    excelWorksheet1.Cells[index1, 17].SetValue(str16.Replace(",", " "));
                    excelWorksheet1.Cells[index1, 21].SetValue(str8.Replace(",", " "));
                    excelWorksheet1.Cells[index1, 22].SetValue(str18.Replace(",", " "));
                    excelWorksheet1.Cells[index1, 23].SetValue(str17.Replace(",", " "));

                    excelFile1.Save(config["Primary"], SaveOptions.XlsxDefault); //SaveOptions.CsvDefault

                    SetStatus("Updating secondary csv...");
                    /*
                        SpreadsheetInfo.SetLicense("ETZW-AT28-33Q6-1HAS");
                        ExcelFile excelFile2 = ExcelFile.Load(config["Secondary"]);
                        ExcelWorksheet excelWorksheet3 = excelFile2.Worksheets[0];
                    */
                    int index2 = 0;
                    try
                    {
                        index2 = excelWorksheet3.GetUsedCellRange(true).LastRowIndex + 1;
                    }
                    catch (Exception e)
                    {
                        index2 = 0;
                    }

                    excelWorksheet3.Cells[index2, 0].SetValue(this.webBrowser.Url.ToString().Replace(",", " "));
                    excelWorksheet3.Cells[index2, 1].SetValue(str8.Replace(",", " "));
                    excelFile2.Save(config["Secondary"], SaveOptions.XlsxDefault);//.CsvDefault


                    SetStatus("Downloading And Resizing Product Images...");
                    Directory.CreateDirectory(Path.GetTempPath() + "\\Images\\" + str8);
                    Chilkat.Http http = new Chilkat.Http();
                    http.UnlockComponent("SVERTLEADHttp_qcaVzzoP4J6Y");
                    strArray1b = str11.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    lowerBound1 = strArray1b.GetLowerBound(0);
                    upperBound1 = strArray1b.GetUpperBound(0);
                    index33 = lowerBound1;

                    string strTargetFile;
                    string tempFileName;

                    while (index33 <= upperBound1 && index33 <= 11)
                    {
                        strTargetFile = Path.GetTempPath() + "\\Images\\" + str8 + "\\" + str8 + "_" + Convert.ToString(checked(index33 + 1)) + ".jpg";
                        tempFileName = Path.GetTempFileName();
                        http.Download(strArray1b[index33], tempFileName);
                        File.Copy(tempFileName, strTargetFile, true);
                        /*
                            if (System.IO.File.Exists(tempFileName))
                            {
                                Bitmap objSRCBitmap = new Bitmap(1600, 1600);
                                Graphics graphics = Graphics.FromImage((Image)objSRCBitmap);
                                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                graphics.DrawImage(Image.FromFile(tempFileName), 0, 0, 1600, 1600);
                                this.saveWithJPGEGQuality(objSRCBitmap, strTargetFile);
                            }
                        */
                        checked { ++index33; }
                    }
                    http.Dispose();

                    if (config["Local"] == "True")
                    {
                        SetStatus("Copying Images To Local Directory...");
                        Directory.CreateDirectory("Images\\" + str8);
                        int lowerBound = strArray1b.GetLowerBound(0);
                        int upperBound = strArray1b.GetUpperBound(0);
                        int i = lowerBound;
                        string str23;
                        while (i <= upperBound)
                        {
                            try
                            {
                                str23 = "Images\\" + str8 + "\\" + str8 + "_" + Convert.ToString(checked(i + 1)) + ".jpg";
                                File.Copy(Path.GetTempPath() + "\\" + str23, str23, true);
                                checked { ++i; }
                            }
                            catch (Exception e) { }
                        }
                        ResetStatus();
                    }

                    if (!Convert.ToBoolean(config["FTP"]))
                    {
                        ftp = new Ftp2();
                        ftp.HeartbeatMs = 100;
                        ftp.ConnectTimeout = 30;
                        ftp.Username = config["User"];
                        ftp.Password = config["Password"];
                        ftp.Hostname = config["Host"];
                        Int32.TryParse(config["Port"], out port);
                        ftp.Port = port;

                        SetStatus("Uploading Images To FTP...");

                        if (!ftp.UnlockComponent("FTP287654321_28EB48A8oH1T"))
                        {
                            MessageBox.Show("Sorry, FTP Library Registration Error! Please Contact Us!", this.title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            ResetStatus();
                            return;
                        }

                        SetStatus("Connecting to server...");
                        if (!ftp.Connect())
                        {
                            MessageBox.Show("Failed to connect FTP server! Please verify that you have input correct information.", this.title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            ftp.Dispose();
                            ResetStatus();
                            return;
                        }

                        string Left2 = config["Folder"];
                        if (Left2.CompareTo("") != 0)
                        {
                            string[] strArray2 = Left2.Split(new string[1] { "/" }, StringSplitOptions.None);
                            int lowerBound2 = strArray2.GetLowerBound(0);
                            int upperBound2 = strArray2.GetUpperBound(0);
                            int index4 = lowerBound2;
                            while (index4 <= upperBound2)
                            {
                                SetStatus("Accessing to folder: " + strArray2[index4]);
                                if (!ftp.ChangeRemoteDir(strArray2[index4]))
                                {
                                    MessageBox.Show("Sorry, Failed to get access to following folder. Please make sure you have read and write access to that folder." + Environment.NewLine + Environment.NewLine + "Folder: /" + strArray2[index4] + "/", "FTP Folder Access Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    ftp.Disconnect();
                                    ftp.Dispose();
                                    ResetStatus();
                                    return;
                                }
                                checked { ++index4; }
                            }
                        }
                        SetStatus("Making SKU Folder...");
                        ftp.CreateRemoteDir(str8);
                        ftp.ChangeRemoteDir(str8);
                        SetStatus("Uploading Images...");
                        int lowerBound3 = strArray1b.GetLowerBound(0);
                        int upperBound3 = strArray1b.GetUpperBound(0);
                        int num30 = lowerBound3;
                        string str23;
                        while (num30 <= upperBound3)
                        {
                            str23 = "Images\\" + str8 + "\\" + str8 + "_" + Convert.ToString(checked(num30 + 1)) + ".jpg";
                            ftp.PutFile(Path.GetTempPath() + "\\" + str23, Path.GetFileName(str23));
                            checked { ++num30; }
                        }

                        http.Dispose();
                        ftp.Disconnect();
                        ftp.Dispose();
                    }
                    ResetStatus();
                    //MemoryManagement.FlushMemory();
                    MessageBox.Show("Complete");
                }
                else
                {
                    if (strOverstock.Contains("-$"))
                    {
                        ftp = new Ftp2();
                        ftp.HeartbeatMs = 100;
                        ftp.ConnectTimeout = 30;
                        ftp.Username = config["User"];
                        ftp.Password = config["Password"];
                        ftp.Hostname = config["Host"];
                        Int32.TryParse(config["Port"], out port);
                        ftp.Port = port;

                        string tmpP = "";
                        double tP;
                        nameArray = strOverstock.Split(new string[] { "fatih_kose" }, StringSplitOptions.RemoveEmptyEntries);
                        priceArray = strOverstock.Split(new string[] { "fatih_kose" }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < nameArray.Length; i++)
                        {
                            if (nameArray[i].Contains("-$"))
                            {
                                try
                                {
                                    tmpP = nameArray[i];
                                    tmpP = tmpP.Substring(tmpP.IndexOf("$") + 1);
                                    tP = Convert.ToDouble(tmpP.Replace(",", "").Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator).Replace(" ", "")) * 2.0;
                                    priceArray[i] = tP.ToString();

                                    nameArray[i] = nameArray[i].Substring(0, nameArray[i].IndexOf("-$"));
                                    Console.WriteLine(nameArray[i] + "*" + priceArray[i]);
                                }
                                catch (Exception e) { }
                            }
                        }

                        strOverstock = "";

                        SetStatus("Updating SKU list...");

                        string skuTemp = "";
                        for (int j = 0; j < nameArray.Length; j++)
                        {
                            if (!nameArray[j].Equals(""))
                            {
                                skuTemp += str8 + "-" + nameArray[j] + Environment.NewLine;
                            }
                        }

                        StreamWriter streamWriter = new StreamWriter(config["SKU"], false);
                        streamWriter.Write(string.Join(Environment.NewLine, arr_sku) + Environment.NewLine + str8 + Environment.NewLine + skuTemp);
                        streamWriter.Close();
                        streamWriter.Dispose();

                        SetStatus("Updating primary csv...");
                        /*
                            SpreadsheetInfo.SetLicense("ETZW-AT28-33Q6-1HAS");
                            ExcelFile excelFile1 = ExcelFile.Load(config["Primary"]);
                            ExcelWorksheet excelWorksheet1 = excelFile1.Worksheets[0];
                        */

                        int index1 = 0;
                        int index2 = 0;

                        for (int j = 0; j < nameArray.Length; j++)
                        {
                            if (!nameArray[j].Equals(""))
                            {
                                try
                                {
                                    index1 = excelWorksheet1.GetUsedCellRange(true).LastRowIndex + 1;
                                }
                                catch (Exception e)
                                {
                                    index1 = 0;
                                }

                                excelWorksheet1.Cells[index1, 3].SetValue(nameArray[j].Replace(",", " ") + " " + str2.Replace(",", " "));
                                excelWorksheet1.Cells[index1, 4].SetValue(str19.Replace(",", " "));
                                excelWorksheet1.Cells[index1, 5].SetValue(strRefurb.Replace(",", " "));
                                if (!Convert.ToBoolean(config["FTP"]))
                                    excelWorksheet1.Cells[index1, 6].SetValue(str12.Replace(",", " "));
                                else
                                    excelWorksheet1.Cells[index1, 6].SetValue(str11.Replace(",", " "));
                                excelWorksheet1.Cells[index1, 9].SetValue(priceArray[j].Replace(" ", "").Replace("/", "").Replace("each", ""));
                                excelWorksheet1.Cells[index1, 12].SetValue(str14.Replace(",", " "));
                                excelWorksheet1.Cells[index1, 13].SetValue(str8.Replace(",", " ") + "-" + nameArray[j].Replace(",", " "));
                                excelWorksheet1.Cells[index1, 14].SetValue(str15.Replace(",", " "));
                                excelWorksheet1.Cells[index1, 17].SetValue(str16.Replace(",", " "));
                                excelWorksheet1.Cells[index1, 21].SetValue(str8.Replace(",", " ") + "-" + nameArray[j].Replace(",", " "));
                                excelWorksheet1.Cells[index1, 22].SetValue(str18.Replace(",", " "));
                                excelWorksheet1.Cells[index1, 23].SetValue(str17.Replace(",", " "));
                                excelFile1.Save(config["Primary"], SaveOptions.XlsxDefault); //.CsvDefault

                                SetStatus("Updating secondary csv...");
                                /*  
                                    SpreadsheetInfo.SetLicense("ETZW-AT28-33Q6-1HAS");
                                    ExcelFile excelFile2 = ExcelFile.Load(config["Secondary"]);
                                    ExcelWorksheet excelWorksheet3 = excelFile2.Worksheets[0];
                                */

                                try
                                {
                                    index2 = excelWorksheet3.GetUsedCellRange(true).LastRowIndex + 1;
                                }
                                catch (Exception e)
                                {
                                    index2 = 0;
                                }

                                excelWorksheet3.Cells[index2, 0].SetValue(this.webBrowser.Url.ToString().Substring(0, this.webBrowser.Url.ToString().IndexOf(".html?") + 5));
                                excelWorksheet3.Cells[index2, 1].SetValue(str8 + "-" + nameArray[j]);
                                excelWorksheet3.Cells[index2, 4].SetValue(nameArray[j]);
                                excelFile2.Save(config["Secondary"], SaveOptions.XlsxDefault);

                                index1++;
                                index2++;
                            }
                        }

                        Directory.CreateDirectory("Images\\" + str8);
                        Directory.CreateDirectory(Path.GetTempPath() + "\\Images\\" + str8);

                        /*
                            Chilkat.Http http = new Chilkat.Http();
                            http.UnlockComponent("SVERTLEADHttp_qcaVzzoP4J6Y");
                            strArray1b = str11.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                            lowerBound1 = strArray1b.GetLowerBound(0);
                            upperBound1 = strArray1b.GetUpperBound(0);
                            index33 = lowerBound1;

                            while (index33 <= upperBound1 && index33 <= 11)
                            {
                        */

                        SetStatus("Downloading And Resizing Product Images...");
                        Chilkat.Http http = new Chilkat.Http();

                        strArray1b = str11.Split(new string[1] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                        lowerBound1 = strArray1b.GetLowerBound(0);
                        upperBound1 = strArray1b.GetUpperBound(0);
                        index33 = lowerBound1;
                        int bas = lowerBound1;

                        while (index33 <= upperBound1)
                        {
                            http.UnlockComponent("SVERTLEADHttp_qcaVzzoP4J6Y");
                            string strTargetFile = Path.GetTempPath() + "\\Images\\" + str8 + "\\" + str8 + "_" + Convert.ToString(checked(index33 + 1)) + ".jpg";
                            string tempFileName = Path.GetTempFileName();
                            http.Download(strArray1b[index33], tempFileName);

                            File.Copy(tempFileName, strTargetFile, true);

                            // Delete empty image file
                            /*
                            if (new FileInfo(strTargetFile).Length == 0)
                            {
                                File.Delete(strTargetFile);
                            }
                            */
                            checked { ++index33; }

                        }
                        /*if (System.IO.File.Exists(tempFileName))
                        {
                            Bitmap objSRCBitmap = new Bitmap(1600, 1600);
                            Graphics graphics = Graphics.FromImage((Image)objSRCBitmap);
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.DrawImage(Image.FromFile(tempFileName), 0, 0, 1600, 1600);
                            this.saveWithJPGEGQuality(objSRCBitmap, strTargetFile);
                        }*/

                        if (config["Local"] == "True")
                        {
                            SetStatus("Copying Images To Local Directory...");

                            int i = lowerBound1;
                            string str23;
                            while (i <= upperBound1)
                            {
                                try
                                {
                                    str23 = "Images\\" + str8 + "\\" + str8 + "_" + Convert.ToString(checked(i + 1)) + ".jpg";
                                    File.Copy(Path.GetTempPath() + "\\" + str23, str23, true);
                                    i++;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Images\\" + str8 + "\\" + str8 + "_" + Convert.ToString(checked(i + 1)) + ".jpg");
                                    i++;
                                }
                            }
                        }

                        if (!Convert.ToBoolean(config["FTP"]))
                        {
                            SetStatus("Uploading Images To FTP...");

                            if (!ftp.UnlockComponent("FTP287654321_28EB48A8oH1T"))
                            {
                                MessageBox.Show("Sorry, FTP Library Registration Error! Please Contact Us!", this.title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                ResetStatus();
                                return;
                            }

                            SetStatus("Connecting to server...");
                            if (!ftp.Connect())
                            {
                                MessageBox.Show("Failed to connect FTP server! Please verify that you have input correct information.", this.title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                ftp.Dispose();
                                ResetStatus();
                                return;
                            }

                            string Left2 = config["Folder"];
                            if (Left2.CompareTo("") != 0)
                            {
                                string[] strArray2 = Left2.Split(new string[1] { "/" }, StringSplitOptions.None);
                                int lowerBound2 = strArray2.GetLowerBound(0);
                                int upperBound2 = strArray2.GetUpperBound(0);
                                int index4 = lowerBound2;
                                while (index4 <= upperBound2)
                                {
                                    SetStatus("Accessing to folder: " + strArray2[index4]);
                                    if (!ftp.ChangeRemoteDir(strArray2[index4]))
                                    {

                                        MessageBox.Show("Sorry, Failed to get access to following folder. Please make sure you have read and write access to that folder." + Environment.NewLine + Environment.NewLine + "Folder: /" + strArray2[index4] + "/", "FTP Folder Access Failed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        ftp.Disconnect();
                                        ftp.Dispose();
                                        ResetStatus();
                                        return;
                                    }
                                    checked { ++index4; }
                                }
                            }
                            SetStatus("Making SKU Folder...");
                            ftp.CreateRemoteDir(str8);
                            ftp.ChangeRemoteDir(str8);
                            SetStatus("Uploading Images...");
                            int lowerBound3 = strArray1b.GetLowerBound(0);
                            int upperBound3 = strArray1b.GetUpperBound(0);
                            int num30 = lowerBound3;
                            string str23;
                            while (num30 <= upperBound3)
                            {
                                str23 = "Images\\" + str8 + "\\" + str8 + "_" + Convert.ToString(checked(num30 + 1)) + ".jpg";
                                ftp.PutFile(Path.GetTempPath() + "\\" + str23, Path.GetFileName(str23));
                                checked { ++num30; }
                            }

                            http.Dispose();
                            ftp.Disconnect();
                            ftp.Dispose();
                        }

                        ResetStatus();

                        MessageBox.Show("Complete");
                    }

                    //MemoryManagement.FlushMemory();
                }
            }
            catch (Exception)
            {
                //MemoryManagement.FlushMemory();
            }
        }

        private void btnScrape_Click(object sender, EventArgs e)
        {

            if (this.webBrowser.IsBusy)
                MessageBox.Show("Page is still loading!", this.title);
            else
                bw.RunWorkerAsync();
        }

        private void txtAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar != (int)Convert.ToChar(13))
                return;
            UseProxy();
            this.webBrowser.Navigate(this.txtAddress.Text);
            SetStatus("Loading: " + this.txtAddress.Text);
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.Equals(webBrowser.Url))
            {
                string title = "";
                if (this.webBrowser.DocumentText.IndexOf("productLabel\":\"", 0) != -1)
                {
                    int startIndex2 = checked(this.webBrowser.DocumentText.IndexOf("productLabel\":\"", 0) + ("productLabel\":\"").Length);
                    int num3 = this.webBrowser.DocumentText.IndexOf(Convert.ToChar(34), startIndex2);
                    title = this.webBrowser.DocumentText.Substring(startIndex2, checked(num3 - startIndex2)).Trim();
                    this.txtTitle.Text = title;
                }
                else if (this.txtAddress.Text.Contains("grainger.com") && this.webBrowser.DocumentText.IndexOf("class=\"productName\"", 0) != -1)
                {
                    int index1 = this.webBrowser.DocumentText.IndexOf("class=\"productName\"", 0);
                    int index2 = checked(this.webBrowser.DocumentText.IndexOf(">", index1) + 1);
                    int index3 = this.webBrowser.DocumentText.IndexOf("</", index2);
                    title = this.webBrowser.DocumentText.Substring(index2, checked(index3 - index2)).Trim();
                    title = System.Net.WebUtility.HtmlDecode(title);
                    this.txtTitle.Text = title;
                }
                else if (this.txtAddress.Text.Contains("overstock.com") && this.webBrowser.DocumentText.IndexOf("class=\"product-title\"", 0) != -1)
                {
                    int index1 = this.webBrowser.DocumentText.IndexOf("class=\"product-title\"", 0);
                    int index2 = checked(this.webBrowser.DocumentText.IndexOf("<h1>", index1) + "<h1>".Length);
                    int index3 = this.webBrowser.DocumentText.IndexOf("</", index2);
                    title = this.webBrowser.DocumentText.Substring(index2, checked(index3 - index2)).Trim();
                    title = System.Net.WebUtility.HtmlDecode(title);
                    this.txtTitle.Text = title;
                }
                else if (this.txtAddress.Text.Contains("walmart.com") && this.webBrowser.DocumentText.IndexOf("query\":\"", 0) != -1)
                {
                    int index2 = checked(this.webBrowser.DocumentText.IndexOf("query\":\"", 0) + "query\":\"".Length);
                    int index3 = this.webBrowser.DocumentText.IndexOf(Convert.ToChar(34), index2);
                    title = this.webBrowser.DocumentText.Substring(index2, checked(index3 - index2)).Trim();
                    title = System.Net.WebUtility.HtmlDecode(title);
                    this.txtTitle.Text = title;
                }
                else if (this.txtAddress.Text.Contains("kirklands.com") && this.webBrowser.DocumentText.IndexOf("<h1>", 0) != -1)
                {
                    int index2 = checked(this.webBrowser.DocumentText.IndexOf("<h1>", 0) + "<h1>".Length);
                    int index3 = this.webBrowser.DocumentText.IndexOf("</h1>", index2);
                    title = this.webBrowser.DocumentText.Substring(index2, checked(index3 - index2)).Trim();
                    title = System.Net.WebUtility.HtmlDecode(title);
                    this.txtTitle.Text = title;
                }
                else
                    this.txtTitle.Text = "Title Not Found";



                if (this.webBrowser.Url == null)
                    return;
                this.txtAddress.Text = this.webBrowser.Url.ToString();


                oldLinks.Add(webBrowser.Url.ToString());
                Console.Write("Added to history:" + webBrowser.Url.ToString());

                ResetStatus();                
            }
        }

        private void txtAddress_Click(object sender, EventArgs e)
        {
            this.txtAddress.SelectAll();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ConfigForm cf = new ConfigForm();
            cf.ShowDialog();
            LoadConfig();
        }

        private void picBack_Click(object sender, EventArgs e)
        {            
            if (!this.webBrowser.CanGoBack)
            {
                if (historyLinks.Contains(webBrowser.Url.ToString()))
                {
                    if (historyLinks.IndexOf(webBrowser.Url.ToString()) > 0)
                        webBrowser.Navigate(historyLinks[historyLinks.IndexOf(webBrowser.Url.ToString()) - 1]);
                }
                else
                {
                    webBrowser.Navigate(historyLinks[historyLinks.Count - 1]);
                }
                return;
            }
            this.webBrowser.GoBack();
            MemoryManagement.FlushMemory();
        }

        private void picRefresh_Click(object sender, EventArgs e)
        {
            MemoryManagement.FlushMemory();
            this.webBrowser.Refresh();
        }

        private void picForward_Click(object sender, EventArgs e)
        {
            MemoryManagement.FlushMemory();
            UseProxy();
            if (!this.webBrowser.CanGoForward)
            {
                if (historyLinks.Contains(webBrowser.Url.ToString()))
                {
                    if (historyLinks.IndexOf(webBrowser.Url.ToString()) < historyLinks.Count - 1)
                        webBrowser.Navigate(historyLinks[historyLinks.IndexOf(webBrowser.Url.ToString()) + 1]);
                }

                return;

            }
            this.webBrowser.GoForward();
        }

        private void picAbout_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.ShowDialog();
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            this.lblCharCnt.Text = this.txtTitle.TextLength.ToString();
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            ConvertXlsxToCsvForm convertForm = new ConvertXlsxToCsvForm();
            convertForm.ShowDialog();
            MemoryManagement.FlushMemory();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //MemoryManagement.FlushMemory();
            if (!bw.IsBusy && GC.GetTotalMemory(false) >= 12000000)
            {
                string path = "history.txt";
                string history = "";

                oldLinks = oldLinks.Distinct().ToList();

                for (int i = 0; i < oldLinks.Count; i++)
                    history = history + oldLinks[i] + "[link]";

                try
                {
                    if (!File.Exists(path))
                    {
                        File.Create(path);
                        TextWriter tw = new StreamWriter(path, true); //true = append
                        tw.Write(history);
                        tw.Close();
                        tw.Dispose();
                    }
                    else if (File.Exists(path))
                    {
                        TextWriter tw = new StreamWriter(path, true); //true = append
                        tw.Write(history);
                        tw.Close();
                        tw.Dispose();
                    }
                    else { }
                }
                catch (Exception ex)
                { }

                try
                {
                    if (!File.Exists("windowSize.txt"))
                    {
                        File.Create("windowSize.txt");
                        TextWriter tw = new StreamWriter("windowSize.txt", false); //true = append
                        if (WindowState == FormWindowState.Normal)
                            tw.Write("1");
                        if (WindowState == FormWindowState.Maximized)
                            tw.Write("2");
                        if (WindowState == FormWindowState.Minimized)
                            tw.Write("0");
                        tw.Close();
                        tw.Dispose();
                    }
                    else if (File.Exists("windowSize.txt"))
                    {
                        TextWriter tw = new StreamWriter("windowSize.txt", false); //true = append
                        if (WindowState == FormWindowState.Normal)
                            tw.Write("1");
                        if (WindowState == FormWindowState.Maximized)
                            tw.Write("2");
                        if (WindowState == FormWindowState.Minimized)
                            tw.Write("0");
                        tw.Close();
                        tw.Dispose();
                    }
                    else { }
                }
                catch (Exception ex) { }

                System.Diagnostics.Process.Start("ArbitrageAuthority.exe");
                Application.Exit();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //MemoryManagement.FlushMemory();
            if (WindowState == FormWindowState.Minimized)
            {
                string path = "history.txt";
                string history = "";

                oldLinks = oldLinks.Distinct().ToList();

                for (int i = 0; i < oldLinks.Count; i++)
                    history = history + oldLinks[i] + "[link]";



                try
                {
                    if (!File.Exists(path))
                    {
                        File.Create(path);
                        TextWriter tw = new StreamWriter(path, true); //true = append
                        tw.Write(history);
                        tw.Close();
                        tw.Dispose();
                    }
                    else if (File.Exists(path))
                    {
                        TextWriter tw = new StreamWriter(path, true); //true = append
                        tw.Write(history);
                        tw.Close();
                        tw.Dispose();
                    }

                    else { }
                }
                catch (Exception ex)
                { }

                try
                {
                    if (!File.Exists("windowSize.txt"))
                    {
                        File.Create("windowSize.txt");
                        TextWriter tw = new StreamWriter("windowSize.txt", false); //true = append
                        tw.Write("0");
                        tw.Close();
                        tw.Dispose();
                    }
                    else if (File.Exists("windowSize.txt"))
                    {
                        TextWriter tw = new StreamWriter("windowSize.txt", false); //true = append
                        tw.Write("0");
                        tw.Close();
                        tw.Dispose();
                    }

                    else { }
                }
                catch (Exception ex)
                { }




                System.Diagnostics.Process.Start("ArbitrageAuthority.exe");
                Application.Exit();


            }
        }

        #region Methods

        #region Manage Proxy

        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        bool settingsReturn, refreshReturn;

        /// <summary>
        /// Method is used to Manage Proxy
        /// </summary>
        private void UseProxy()
        {
            try
            {
                string proxyIP = config["ProxyIP"];
                string proxyPort = config["ProxyPort"];
                if (!string.IsNullOrWhiteSpace(proxyIP))
                {
                    string currentProxy = proxyIP + (!string.IsNullOrWhiteSpace(proxyPort) ? (":" + proxyPort) : "");
                    RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(@"Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
                    RegKey.SetValue("ProxyServer", currentProxy);
                    RegKey.SetValue("ProxyEnable", 1);

                    settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
                    refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
                    isProxyAppied = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #endregion

        #region Form Events

        /// <summary>
        /// Event is invoked when form will close. This will remove proxy if it has been set from application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isProxyAppied)
            {
                string currentProxy = ":" + "";
                RegistryKey RegKey = Registry.CurrentUser.OpenSubKey(@"Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
                RegKey.SetValue("ProxyServer", currentProxy);
                RegKey.SetValue("ProxyEnable", 0);
                settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
                refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
            }
        }

        #endregion

        #region Control Events

      

        #endregion
    }

}

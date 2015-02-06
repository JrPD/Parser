using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Text;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using AmazonBookInfo.Model;
using FluentSharp.CoreLib;
using FluentSharp.WinForms;
using HtmlAgilityPack;
using Microsoft.SqlServer.Server;

namespace AmazonBookInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
            
        }

        private async void BtnSelect_Click(object sender, EventArgs e)
        {
            List<string> contentList =  await LoadDataAsync();
            foreach (var content in contentList)
            {
                Parse(content);
            }
        }

        public IEnumerable<string>  SetUpList()
        {
            Stream myStream = null;
            List<string> URLlist = null;

            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            ListPathTb.Text = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string file = openFileDialog1.FileName;
                            ListPathTb.Text = file;
                            string text = File.ReadAllText(file);
                            URLlist = new List<string>();
                            string[] list = Regex.Split(text, "\r\n");

                            foreach (var item in list)
                            {
                                URLlist.Add(item);
                                URLLb.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            return URLlist;
        }

        private async Task<List<string>> LoadDataAsync()
        {
            // Make a list of web addresses.
            IEnumerable<string> urlList = SetUpList();
            List<string> contentList = new List<string>();

            //foreach (var url in urlList)
            //{
            //    string urlContents = await GetURLContentsAsync(url);

            //    RTB.Text = urlContents;
            //}
            string urlContents = await GetURLContentsAsync(urlList.First());
            contentList.Add(urlContents);
            return contentList;

        }

        private async Task<string> GetURLContentsAsync(string url)
        {
            var content = new MemoryStream();

            var webReq = (HttpWebRequest)WebRequest.Create(url);

            using (WebResponse response = await webReq.GetResponseAsync())
            {
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    string responseJson = sr.ReadToEnd();
                  return responseJson;
                }
            }
        }


        public  void Parse(string content)
        {
            try
            {

                HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

                htmlDoc.OptionFixNestedTags = true;

                htmlDoc.LoadHtml(content);
                //if (htmlDoc.ParseErrors != null && htmlDoc.ParseErrors.Count() > 0)
                //{
                //}

                if (htmlDoc.DocumentNode != null)
                {
                    HtmlAgilityPack.HtmlNode bodyNode = htmlDoc.DocumentNode.SelectSingleNode("//body");
                    if (bodyNode != null)
                    {
                        Book book = new Book();

                        RTB.Text = "";
                        HtmlNode mainNode = bodyNode.SelectSingleNode("//div[@class='singlecolumnminwidth']");
                        HtmlNode workNode=null;

                        //Image
                        try
                        {
                            workNode = mainNode.SelectSingleNode("//img[@id='main-image']");
                            book.Image = workNode.Attributes["src"].Value;
                        }
                        catch (Exception)
                        {
                            book.Image = null;
                        }

                        // Name
                        try
                        {
                            workNode = bodyNode.SelectSingleNode("//span[@id='btAsinTitle']");
                            book.Name = workNode.ChildNodes[0].InnerText;
                        }
                        catch (Exception)
                        {
                            book.Name = null;
                        }
                       

                        // Author
                        try
                        {
                            workNode = mainNode.SelectSingleNode("//div[@class='buying']/span");
                            book.Author = workNode.InnerHtml.ParseAuthor();
                        }
                        catch (Exception)
                        {
                            book.Author = null;
                        }
                       

                        // Comments
                        try
                        {
                            workNode = mainNode.SelectSingleNode("//div[@class='fl gl5 mt3 txtnormal acrCount']/a");
                            book.Comments = workNode.ChildNodes[0].InnerText.ParseCount();
                        }
                        catch (Exception)
                        {
                            book.Comments = null;
                        }

                        // Price
                        try
                        {
                            workNode = mainNode.SelectSingleNode("//b[@class='priceLarge']");
                            book.Price = workNode.InnerText.ParsePrice();
                        }
                        catch (Exception)
                        {
                            book.Price = null;
                        }

                        // Amazon Best Sellers Rank
                        try
                        {
                            workNode = mainNode.SelectSingleNode("//li[@id='SalesRank']");
                            book.BestSellersRank = workNode.InnerText.ParseRank();
                        }
                        catch (Exception)
                        {
                            book.BestSellersRank = null;
                        }

                        // Categories
                        try
                        {
                            workNode = mainNode.SelectSingleNode("//ul[@class='zg_hrsr']");
                            var cont = from li in workNode.Descendants("li")
                                from span in li.Descendants("span")
                                from b in span.Descendants("b")
                                from a in span.Descendants("a")

                                select new Category
                                {
                                    Name = a.InnerText
                                };

                            List<string> categories = new List<string>();
                            foreach (Category item in cont)
                            {
                                if (!categories.Contains(item.Name))
                                {
                                    categories.Add(item.Name);
                                }
                            }
                            book.Categories = categories;
                        }
                        catch (Exception)
                        {
                            book.Categories = null;
                        }

                        // Publication Data
                        try
                        {
                            workNode = mainNode.SelectSingleNode("//input[@id='pubdate']");
                            book.PublicationDate = workNode.OuterHtml.ParseDate();
                        }
                        catch (Exception)
                        {
                            book.PublicationDate = null;
                        }

                        // Print
                        Print(book);

                        try
                        {
                            if (book != null)
                            {
                                //db.Books.Add(book);
                                //db.SaveChanges();
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Network Problem or parse!");
            }
        }

        private void Print(Book book)
        {
            RTB.Text += book.Image + '\r';
            RTB.Text += book.Name + '\r';
            RTB.Text += book.Author + '\r';
            RTB.Text += book.Comments.ToString() + '\r';
            RTB.Text += book.Price.ToString() + '\r';
            RTB.Text += book.BestSellersRank.ToString() + '\r';
            RTB.Text +=  '\r'+"Categories" + '\r';
            foreach (var name in book.Categories)
            {
                RTB.Text += name + '\r';
            }
            RTB.Text +=  '\r';

            RTB.Text += book.PublicationDate.ToString() + '\r';
        }

    }

}

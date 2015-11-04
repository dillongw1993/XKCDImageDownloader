using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.IO;

namespace XKCDImageDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            bool IsActive = true;
            Console.WriteLine("Please type a number between 1 and 1600");
            Console.WriteLine("This program will go and download that picture number from the XKCD site");
            while (IsActive)
            {
                DoWork();
            }

        }
        public static string PictureTitle(int number)
        {
            string title = "";
            string url = "http://xkcd.com/###/";
            url = url.Replace("###", number.ToString());
            //Use HTML Agility Pack To Get Title ID
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            title = doc.GetElementbyId("ctitle").InnerText;

            return title;
        }

        public static void GetPicture(string title)
        {
            Console.WriteLine("Attempting to download picture....");
            string url = "http://imgs.xkcd.com/comics/###.png";
            url = url.Replace("###", title);

            if (!Directory.Exists("C:\\Users\\" + Environment.UserName + "\\Pictures\\XKCD\\"))
            {
                Directory.CreateDirectory("C:\\Users\\" + Environment.UserName + "\\Pictures\\XKCD\\");
            }
            

            using (var client = new WebClient())
            {
                url = url.ToLower();
                try
                {
                    client.DownloadFile(url, "C:\\Users\\" + Environment.UserName + "\\Pictures\\XKCD\\" + title + ".png");
                }
                catch (Exception e)
                {
                    try
                    {
                        url = url.Replace(".png", ".jpg");
                        url = url.Replace(",", "");
                        client.DownloadFile(url, "C:\\Users\\" + Environment.UserName + "\\Pictures\\XKCD\\" + title + ".jpg");
                        Console.WriteLine("DONE!!!");
                    }
                    catch (Exception finale)
                    {
                        Console.WriteLine("Could Not Download Image...Please try another");
                        Console.ReadLine();
                    }
                    
                }
                
            }

        }
        public static void DoWork()
        {
            
            int number = 0;
            bool IsNumber = false;
            while (!IsNumber)
            {
                try
                {
                    number = Convert.ToInt32(Console.ReadLine());
                    IsNumber = true;
                }
                catch (Exception IsNUmberException)
                {
                    Console.WriteLine("Please enter only a number, with no leading zeros....");
                }
            }


            Console.WriteLine("Getting title.....");
            string title = PictureTitle(number);
            Console.WriteLine("Downloading " + title + " comic.....");
            string titleFixed = title.Replace(" ", "_");
            GetPicture(titleFixed);
        }
    }
}

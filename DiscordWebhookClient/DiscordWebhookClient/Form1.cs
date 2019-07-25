using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using System.Xml.Linq;

namespace DiscordWebhookClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public static string ImageURl(string imageLocation)
        {
            string imageUrl = string.Empty;
            using (var w = new WebClient())
            {
                string clientID = "1f9bc62318c10fc";
                w.Headers.Add("Authorization", "Client-ID " + clientID);
                var values = new NameValueCollection
                {
                     { "image", Convert.ToBase64String(File.ReadAllBytes(imageLocation)) }
                 };

                byte[] response = w.UploadValues("https://api.imgur.com/3/upload.xml", values);

                var responseXml = XDocument.Load(new MemoryStream(response));
                return imageUrl = (string)responseXml.Root.Element("link");

            }
        }
         
    }
}

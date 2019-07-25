using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Xml.Linq;

namespace DiscordWebhookClient
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }
        OpenFileDialog openFile = new OpenFileDialog();
        private void Button3_Click(object sender, EventArgs e)
        {
            openFile.Filter = "Image File (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                label5.Text = openFile.SafeFileName;
            }
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

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (label5.Text == "Select Image")
            {
                MessageBox.Show("Please select image before uploading");

            }
            else
            {
                txtUpload.Text = ImageURl(openFile.FileName);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAvatar.Text) || string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtUrl.Text))
            {
                MessageBox.Show("Please fill out all fields");
                
            }
            else
            {
                Properties.Settings.Default.webUrl = txtUrl.Text;
                Properties.Settings.Default.name = txtName.Text;
                Properties.Settings.Default.avatar = txtAvatar.Text;
                Properties.Settings.Default.Changed = true;
                Properties.Settings.Default.Save();
                MessageBox.Show("updated");
            }
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            txtAvatar.Text = Properties.Settings.Default.avatar;
            txtName.Text = Properties.Settings.Default.name;
            txtUrl.Text = Properties.Settings.Default.webUrl;
        }
    }
}

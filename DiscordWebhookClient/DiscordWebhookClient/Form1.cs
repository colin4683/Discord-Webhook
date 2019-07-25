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
        #region Dll Refernces
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion



        #region Webhook References

        public static string webUrl = Properties.Settings.Default.webUrl;
        public static string name = Properties.Settings.Default.name;
        public static string avatarurl = Properties.Settings.Default.avatar;
        public static string embedColor = string.Empty;
        #endregion


        #region From Design and usage
        private void UpdateTmr_Tick(object sender, EventArgs e)
        {
            label1.Text = Properties.Settings.Default.name;
            webUrl = Properties.Settings.Default.webUrl;
            name = Properties.Settings.Default.name;
            avatarurl = Properties.Settings.Default.avatar;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var frm = new Settings();
            frm.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.Changed)
            {
                label1.Visible = false;
                MessageBox.Show("Looks like your settings have not yet been changed \n In order for this to work please fill out all fields in the settings form");
            }
            else
            {
                label1.Visible = true;
            }
            updateTmr.Start();
        }
        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion


        #region commands


        public static void SendMessage(string message)
        {
            
            WebRequest web = WebRequest.Create("https://chrissite.online/webhook/sendmessage.php?webUrl=" + webUrl + "&botName=" + name + "&avatar=" + avatarurl + "&regular=1&message=" + message);
            WebResponse response = web.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream()); string wResult = reader.ReadLine();
        }
        public static void SendEmbedMessage(string embeddescription, string embedtitle, string embedcolor, string message)
        {

            WebRequest web = WebRequest.Create("https://chrissite.online/webhook/sendmessage.php?webUrl=" + webUrl + "&botName=" + name + "&avatar=" + avatarurl + "&embed=1&message=" + message + "&embedDescription=" + embeddescription + "&embedTitle=" + embedtitle + "&embedColor=" + embedcolor); ;
            WebResponse response = web.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream()); string wResult = reader.ReadLine();
        }
        #endregion


        #region Command Triggers
        private void Button1_Click(object sender, EventArgs e)
        {
            SendMessage(richTextBox1.Text);
        }
        #endregion

        private void Button3_Click(object sender, EventArgs e)
        {
            SendEmbedMessage(embedDescrip.Text, embedTitle.Text, embedColor, embedMSG.Text);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                embedColor = (colorDialog1.Color.ToArgb() & 0x00FFFFFF).ToString("X6");
                label8.Text = "Color: " + embedColor;
            }
        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

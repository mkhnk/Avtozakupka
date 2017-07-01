using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FluentFTP;
using System.Net;
using System.IO;
using MetroFramework.Forms;

namespace Avtozakupka
{
    public partial class Form2 : MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            Form4 fs = new Form4();
            fs.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form5 fs = new Form5();
            fs.Show();
            this.Hide();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            Application.Exit();
            using (FtpClient conn = new FtpClient())
            {
                conn.Host = "ftp.vhost30709.cpsite.ru";
                conn.Credentials = new NetworkCredential("eskov@vhost30709.cpsite.ru", "admin123321");
                conn.Connect();
                string curDir = Directory.GetCurrentDirectory();
                string writepath = String.Format(@"{0}\base.mdb", curDir);
                conn.UploadFile(writepath, "base.mdb");
                conn.Disconnect();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form7 fs = new Form7();
            fs.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 fs = new Form6();
            fs.Show();
            this.Hide();
        }
    }
}

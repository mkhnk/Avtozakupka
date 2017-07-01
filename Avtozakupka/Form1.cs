using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using FluentFTP;
using MetroFramework.Forms;

namespace Avtozakupka
{
    public partial class Form1 : MetroForm
    {
        public static int klient = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "123")
            {
                Form2 fs = new Form2();
                fs.Show();
                this.Hide();
                using (FtpClient conn = new FtpClient())
                {
                    conn.Host = "ftp.vhost30709.cpsite.ru";
                    conn.Credentials = new NetworkCredential("eskov@vhost30709.cpsite.ru", "admin123321");
                    conn.Connect();
                    string curDir = Directory.GetCurrentDirectory();
                    string writepath = String.Format(@"{0}\base.mdb", curDir);
                    conn.DownloadFile(writepath, "base.mdb");
                    conn.Disconnect();
                }
            }
            else if (textBox1.Text == "user")
            {
                klient = 1;
                Form6 fs = new Form6();
                fs.Show();
                this.Hide();
                using (FtpClient conn = new FtpClient())
                {
                    conn.Host = "ftp.vhost30709.cpsite.ru";
                    conn.Credentials = new NetworkCredential("eskov@vhost30709.cpsite.ru", "admin123321");
                    conn.Connect();
                    string curDir = Directory.GetCurrentDirectory();
                    string writepath = String.Format(@"{0}\base.mdb", curDir);
                    conn.DownloadFile(writepath, "base.mdb");
                    conn.Disconnect();
                }
            }
            else
            {
                MessageBox.Show("Неверный пароль!");
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 fs = new Form3();
            fs.Show();
            this.Hide();
        }
    }
}

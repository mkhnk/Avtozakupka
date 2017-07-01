using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using FluentFTP;
using System.Net;
using System.IO;
using MetroFramework.Forms;

namespace Avtozakupka
{
    public partial class Form5 : MetroForm
    {
        public Form5()
        {
            InitializeComponent();
        }


        private void Form5_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'baseDataSet.Запчасти' table. You can move, or remove it, as needed.
            this.запчастиTableAdapter.Fill(this.baseDataSet.Запчасти);
            // TODO: This line of code loads data into the 'baseDataSet.Запчасти' table. You can move, or remove it, as needed.
            this.запчастиTableAdapter.Fill(this.baseDataSet.Запчасти);
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 fs = new Form2();
            fs.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < запчастиDataGridView.Rows.Count - 1; i++)
            {
                запчастиDataGridView.CurrentCell = null;
                запчастиDataGridView.Rows[i].Visible = false;
                for (int c = 0; c < запчастиDataGridView.Columns.Count; c++)
                {
                    if (запчастиDataGridView[c, i].Value.ToString() == textBox1.Text)
                    {
                        запчастиDataGridView.Rows[i].Visible = true;
                        break;
                    }
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int x = 0;
            int y = 20;
            int cell_height = 0;

            int colCount = запчастиDataGridView.ColumnCount;
            int rowCount = запчастиDataGridView.RowCount - 1;

            Font font = new Font("Tahoma", 9, FontStyle.Bold, GraphicsUnit.Point);

            int[] widthC = new int[colCount];

            int current_col = 0;
            int current_row = 0;

            while (current_col < colCount)
            {
                if (g.MeasureString(запчастиDataGridView.Columns[current_col].HeaderText.ToString(), font).Width > widthC[current_col])
                {
                    widthC[current_col] = (int)g.MeasureString(запчастиDataGridView.Columns[current_col].HeaderText.ToString(), font).Width;
                }
                current_col++;
            }

            while (current_row < rowCount)
            {
                while (current_col < colCount)
                {
                    if (g.MeasureString(запчастиDataGridView[current_col, current_row].Value.ToString(), font).Width > widthC[current_col])
                    {
                        widthC[current_col] = (int)g.MeasureString(запчастиDataGridView[current_col, current_row].Value.ToString(), font).Width;
                    }
                    current_col++;
                }
                current_col = 0;
                current_row++;
            }

            current_col = 0;
            current_row = 0;

            string value = "";

            int width = widthC[current_col] + 5;
            int height = запчастиDataGridView[current_col, current_row].Size.Height;

            Rectangle cell_border;
            SolidBrush brush = new SolidBrush(Color.Black);


            while (current_col < colCount)
            {
                width = widthC[current_col];
                cell_height = запчастиDataGridView[current_col, current_row].Size.Height;
                cell_border = new Rectangle(x, y, width, height);
                value = запчастиDataGridView.Columns[current_col].HeaderText.ToString();
                g.DrawRectangle(new Pen(Color.Black), cell_border);
                g.DrawString(value, font, brush, x, y);
                x += widthC[current_col];
                current_col++;
            }
            current_row = -1;
            while (current_row < rowCount)
            {
                while (current_col < colCount)
                {
                    width = widthC[current_col];
                    cell_height = запчастиDataGridView[current_col, current_row].Size.Height;
                    cell_border = new Rectangle(x, y, width, height);
                    value = запчастиDataGridView[current_col, current_row].Value.ToString();
                    g.DrawRectangle(new Pen(Color.Black), cell_border);
                    g.DrawString(value, font, brush, x, y);
                    x += widthC[current_col];
                    current_col++;
                }
                current_col = 0;
                current_row++;
                x = 0;
                y += cell_height;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDocument Document = new PrintDocument();
            Document.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            PrintPreviewDialog dlg = new PrintPreviewDialog();
            dlg.Document = Document;
            dlg.ShowDialog();
        }

        private void запчастиBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.запчастиBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.baseDataSet);
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

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            Form2 fs = new Form2();
            fs.Show();
            this.Hide();
        }

    }
}

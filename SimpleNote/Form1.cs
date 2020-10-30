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

namespace SimpleNote
{
    public partial class Form1 : Form
    {
        DataTable table;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            table = new DataTable();
            table.Columns.Add("Title", typeof(String));
            table.Columns.Add("Messages", typeof(String));

            dataGridView1.DataSource = table;

            dataGridView1.Columns["Messages"].Visible = false;
            dataGridView1.Columns["Title"].Width = 240;
        }



        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.TextLength == 0) txtTitle.Text = ".";
            if (txtMessage.TextLength == 0) txtMessage.Text = ".";
            table.Rows.Add(txtTitle.Text, txtMessage.Text);
            txtTitle.Clear();
            txtMessage.Clear();
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            if (index > -1)
            {
                txtTitle.Text = table.Rows[index].ItemArray[0].ToString();
                txtMessage.Text = table.Rows[index].ItemArray[1].ToString();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            table.Rows[index].Delete();
        }

        private void buttonSaveToFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string fileName = saveFileDialog1.FileName;

            int index = table.Rows.Count;

            List<String> output = new List<string>();

            

            for (int i = 0; i < index; i++)
            {
                output.Add(table.Rows[i].ItemArray[0].ToString());
                
                string messageMulti = table.Rows[i].ItemArray[1].ToString().Replace("\r\n", "\\r\\n");
                output.Add(messageMulti);
                messageMulti = String.Empty;
            }

            File.WriteAllLines(fileName, output);

        }

        private void buttonLoadFromFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string fileName = openFileDialog1.FileName;

                try
                {
                    string[] loadedFile = File.ReadAllLines(fileName);

                    table.Clear();

                    for (int i = 0; i < loadedFile.Length; i+=2)
                    {
                        var divideLine = loadedFile[i + 1].Replace("\\r\\n", Environment.NewLine);
                        table.Rows.Add(loadedFile[i], divideLine);
                    }
                }
                catch (IOException)
                {
                }
                
            }

           

        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            txtTitle.Clear();
            txtMessage.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataUploader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CSVHelper csvHelper = new CSVHelper();
            SPHelper helper = new SPHelper();

            helper.CopytoSharePoint(txtSiteURL.Text, txtUserName.Text, txtPassword.Text, txtListName.Text, csvHelper.GetEntitiesfromCSV(txtCSVpath.Text));
            MessageBox.Show("Process Completed!"); 

        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "CSV File|*.csv";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCSVpath.Text = openFileDialog1.FileName;
               
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }
    }
}

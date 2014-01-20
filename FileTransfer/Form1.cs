using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileTransfer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MySqlDb db = new MySqlDb(txtMyConnection.Text);

            db.IterationComplete += new EventHandler<MySqlDb.IterationEventArgs>(db_IterationComplete);
            db.MoveMedia();
            MessageBox.Show("The transfer is complete", "Media Transfer", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }

        void db_IterationComplete(object sender, MySqlDb.IterationEventArgs e)
        {
            string infoFromWorker = e.iterationNumber;
            txtMessage.Text += e.iterationNumber + " - " + e.name;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtMyConnection.Text = "server=192.168.1.179; user id='root'; password='root'; database='test_crm'; pooling=false; default command timeout=120";
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRental
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            carreg c = new carreg();
            c.Show();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            customer c = new customer();
            c.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            rental r = new rental();
            r.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            returncar re = new returncar();
            re.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

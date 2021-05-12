using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace CarRental
{
    public partial class rental : Form
    {
        public rental()
        {
            InitializeComponent();
            carload();
            rentalload();
        }
        SqlConnection con = new SqlConnection("Data Source=.; Initial Catalog= carrental; User ID=sa; Password=123.x.ts;");
        SqlCommand cmd;
        SqlCommand cmd1;
        SqlDataReader dr;
        string proid;
        string sql;
        string sql1;
        bool Mode = true;
        string id;

        public void carload()
        {

            cmd = new SqlCommand("select * from carreg", con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                txtcarid.Items.Add(dr["regno"].ToString());

            }
            con.Close();

        }
        public void rentalload()
        {

            sql = "select * from rental";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {

                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3],dr[4],dr[5],dr[6]);

            }
            con.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtcarid_SelectedIndexChanged(object sender, EventArgs e)
        {

            cmd = new SqlCommand("select * from carreg where regno = '" + txtcarid.Text + "'  ", con);
            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                string aval;


                aval = dr["available"].ToString();
                label9.Text = aval;
                if (aval == "No")
                {

                    txtcustid.Enabled = false;
                    txtcustname.Enabled = false;
                    txtfee.Enabled = false;
                    txtdate.Enabled = false;
                    txtdue.Enabled = false;

                }
                else
                {
                    txtcustid.Enabled = true;
                    txtcustname.Enabled = true;
                    txtfee.Enabled = true;
                    txtdate.Enabled = true;
                    txtdue.Enabled = true;

                }

            }
            else
            {
                label9.Text = "Car Not Avaliable";
            }
            con.Close();

        }

        private void txtcustid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                cmd = new SqlCommand("select * from customer where custid = '" + txtcustid.Text + "'  ", con);
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {

                    txtcustname.Text = dr["custname"].ToString();

                }
                else
                {
                    MessageBox.Show("Customer ID Not Found");


                }

                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            {
                string carid = txtcarid.SelectedItem.ToString();
                string custid = txtcustid.Text;
                string custname = txtcustname.Text;
                string fee = txtfee.Text;
                string date = txtdate.Value.Date.ToString("yyyy-MM-dd");
                string due = txtdue.Value.Date.ToString("yyyy-MM-dd");


                sql = "insert into rental(cat_id,cust_id,custname,fee,date,due)values(@cat_id,@cust_id,@custname,@fee,@date,@due)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@cat_id", carid);
                cmd.Parameters.AddWithValue("@cust_id", custid);
                cmd.Parameters.AddWithValue("@custname", custname);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@date", date);
                cmd.Parameters.AddWithValue("@due", due); 


                cmd.ExecuteNonQuery();

                sql1 = "update carreg set available = 'No' where regno = @regno";
                
                cmd1= new SqlCommand(sql1, con);
                cmd1.Parameters.AddWithValue("@regno", carid);
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Record Addedd");

                con.Close();








            }

        }
    }
}

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
    public partial class returncar : Form
    {
        public returncar()
        {
            InitializeComponent();
            returncarload();

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


        
        
        public void returncarload()
        {

            sql = "select * from returncar";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();
            dataGridView1.Rows.Clear();

            while (dr.Read())
            {

                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3],dr[4],dr[5]);

            }
            con.Close();

        }


        public void getid(String id)
        {

            sql = "select * from carreg where regno = '" + id + "' ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

                txtcarid.Text = dr[0].ToString();
                txtcustid.Text = dr[1].ToString();
                txtdate.Text = dr[2].ToString();
                txtelp.Text = dr[3].ToString();
                txtfine.Text = dr[4].ToString();

            }
            con.Close();

        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {

                cmd = new SqlCommand("select cat_id,cust_id,date,due,DATEDIFF(dd,due,GETDATE()) as elap from rental where cat_id = '" + txtcarid.Text + "' ", con);
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtcustid.Text = dr["cust_id"].ToString();
                    txtdate.Text = dr["date"].ToString();

                    string elap = dr["elap"].ToString();
                    int elapped = int.Parse(elap);
                    txtelp.Text = (elap);



                    if (elapped > 0)
                    {

                        int fine = elapped * 100;
                        txtfine.Text = fine.ToString();


                    }
                    else
                    {

                        txtfine.Text = "0";
                        txtfine.Text = "0";

                    }

                    con.Close();

                }




            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string carid = txtcarid.Text;
            string custid = txtcustid.Text;
            string date = txtdate.Text;
            string elp = txtelp.Text;
            string fine = txtfine.Text;






            sql = "insert into returncar(car_id,cust_id,date,elp,fine)values(@car_id,@cust_id,@date,@elp,@fine)";
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@car_id", carid);
            cmd.Parameters.AddWithValue("@cust_id", custid);
            cmd.Parameters.AddWithValue("@date", date);
            cmd.Parameters.AddWithValue("@elp", elp);
            cmd.Parameters.AddWithValue("@fine", fine);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Record Addedd");

            con.Close();




        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["edit"].Index && e.RowIndex >= 0)
            {

                Mode = false;
                txtcarid.Enabled = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();

                getid(id);

            }
            else if (e.ColumnIndex == dataGridView1.Columns["delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from returncar where regno = @id";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            returncarload();
            
            txtcarid.Clear();
            txtcustid.Clear();
            txtdate.Clear();
            txtelp.Clear();
            txtfine.Clear();

            txtcarid.Focus();
        }
    }
}

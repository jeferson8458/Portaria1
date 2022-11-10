using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portaria
{
    public partial class dashboard : Form
    {
        String connectionString = @"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True";
        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True");
        SqlConnection cn2 = new SqlConnection(@"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADSATRO;Integrated Security=True");
        string imgLocation="";
        SqlCommand cmd;
        SqlDataReader dr;

        private Bitmap bmp;

        public dashboard()
        {
            InitializeComponent();
        }

        public void label2_Click(object sender, EventArgs e)
        {
           
        }
        private void dashboard02()
        {///SELECT SOCIO COUNT(*) as Total FROM PORTARIA GROUP BY SOCIO=@socio
            SqlConnection con = new SqlConnection(connectionString);
            cmd = new SqlCommand("SELECT COUNT(ID) FROM PORTARIA", con);
                con.Open();
            {
                label5.Text = cmd.ExecuteScalar().ToString();
            }

            string sql = "SELECT COUNT(*) FROM PORTARIA where SOCIO=@Teste";
            cmd = new SqlCommand(sql, con);
            {
                 cmd.Parameters.AddWithValue("@Teste", "S");
                pika.Text = cmd.ExecuteScalar().ToString();
            }

            string sql2 = "SELECT COUNT(*) FROM PORTARIA where SOCIO=@Teste";
            cmd = new SqlCommand(sql2, con);
            {
                cmd.Parameters.AddWithValue("@Teste", "D");
                label8.Text = cmd.ExecuteScalar().ToString();
            }

            string sql3 = "SELECT COUNT(*) FROM PORTARIA where SOCIO=@Teste";
            cmd = new SqlCommand(sql3, con);
            {
                cmd.Parameters.AddWithValue("@Teste", "C");
                label10.Text = cmd.ExecuteScalar().ToString();
            }

            string sql4 = "SELECT COUNT(*) FROM PORTARIA where SOCIO=@Teste";
            cmd = new SqlCommand(sql4, con);
            {
                cmd.Parameters.AddWithValue("@Teste", "-");
                label4.Text = cmd.ExecuteScalar().ToString();
            }

            string sql5 = "SELECT COUNT(*) FROM PORTARIA where SOCIO=@Teste";
            cmd = new SqlCommand(sql5, con);
            {
                cmd.Parameters.AddWithValue("@Teste", "C/A");
                label11.Text = cmd.ExecuteScalar().ToString();
            }


        }
        private void dashboard_Load(object sender, EventArgs e)
        {
            dashboard02();

        }

            private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                byte[] images = null;
                FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
                BinaryReader brs = new BinaryReader(stream);
                images = brs.ReadBytes((int)stream.Length);

                cn.Open();
                string sqlQuery = "INSERT INTO login(FOTOS)"+"VALUES(@images)";
                cmd = new SqlCommand(sqlQuery, cn);
                cmd.Parameters.Add(new SqlParameter("@images", images));
                int n = cmd.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show(n.ToString() + "Data saved");
                

            }catch(Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
            

    

    



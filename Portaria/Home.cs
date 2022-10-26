using AForge.Video;
using AForge.Video.DirectShow;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portaria
{

    public partial class Home : Form
    {
        string connectionString = @"Data Source=DESKTOP-1BCGEJD;Initial Catalog=Login;Integrated Security=True";
        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-1BCGEJD;Initial Catalog=Login;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        bool novo21;
        bool novo;
        public Home(string usuarioLogado, string jef2, string id)
        {
            InitializeComponent();
            AbrirHome(new dashboard());
            label1.Text = "Usuário " + usuarioLogado;
            textBox1.Text = jef2;
            label2.Text = id;
        }

        public Home()
        {
        }

        private void AbrirHome(object Formhijo)
        {
            if (this.panel3.Controls.Count > 0)
                this.panel3.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.panel3.Controls.Add(fh);
            this.panel3.Tag = fh;
            fh.Show();
        }
        public string Propriedade {get;set;}
        private void Home_Load(object sender, EventArgs e)
        {

            
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirHome(new Cadastro());
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login jef = new Login();
            jef.ShowDialog();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirHome(new Presença());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirHome(new Editar());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AbrirHome(new dashboard());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            AbrirHome(new Lista());

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM Login WHERE id='" + textBox1.Text + "'";
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                    cmd = new SqlCommand(sql, cn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows)
                    {
                        byte[] img = (byte[])(reader[3]);

                        if (img == null)
                        {
                            fotocirculo1.Image = null;

                        }
                        try
                        {
                            MemoryStream ms = new MemoryStream(img);
                            fotocirculo1.Image = Image.FromStream(ms);
                        }
                        catch
                        {

                        }
                    }
                    try
                    {
                        cn.Close();
                    }
                    finally
                    {

                    }
                }
            }
            finally
            {

            }
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            
            }

        private void button6_Click_2(object sender, EventArgs e)
        {
            AbrirHome(new ListaPresença());
        }

        private void fotocirculo1_Click(object sender, EventArgs e)
        {
            editarfoto objLogin = new editarfoto(label2.Text);
            objLogin.ShowDialog();
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {

          }

        private void button3_Click_1(object sender, EventArgs e)
        {
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
        }

        private void button3_Click_3(object sender, EventArgs e)
        {
       
        }
    }
    }
    
    


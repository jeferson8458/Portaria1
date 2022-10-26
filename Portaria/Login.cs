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
    public partial class Login : Form
    {
        string connectionString = @"Data Source=DESKTOP-1BCGEJD;Initial Catalog=Login;Integrated Security=True";
        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-1BCGEJD;Initial Catalog=Login;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        Conexão con = new Conexão();
        bool novo;
        string imgLocation = "";
        public Login()
        {
            InitializeComponent();
            textnome.Focus();
        }
        bool VerificaLogin()
        {

            bool result = false;
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = connectionString;

                try
                {
                    SqlCommand cmd = new SqlCommand("select * from Login where usuario = '" + textnome.Text + "' and senha = '" + textsenha.Text + "';", cn);
                    cn.Open();
                    SqlDataReader dados = cmd.ExecuteReader();
                    result = dados.HasRows;

                }
                catch (SqlException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    cn.Close();
                }
            }
            return result;

        }
        private void Login_Load(object sender, EventArgs e)
        {
            textnome.Focus();
            this.AcceptButton = button1;
            textnome.Text = "Rodrigo";

            label9.Text = DateTime.Now.ToString("dd/MM/yyyy");
            toolStripLabel4.Text = DateTime.Now.ToString("HH:mm");
        }

        private bool Logado = false;
        public static string usuarioConectado;
        public static string jef2;
        private bool jef;
        private string g;

        private void button1_Click(object sender, EventArgs e)
        {
            ////Verificador de Usuario
            bool result = VerificaLogin();

            Logado = result;

            if (result)
            {
                novo = true;
                if (novo)
                {
                    string sql = "INSERT INTO ENTROU (NOME,DATAS,HORA) " + "VALUES (@Nome,@Datas,@Hora)";

                    SqlConnection con = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.Parameters.AddWithValue("@Nome", textnome.Text);
                    cmd.Parameters.AddWithValue("@Datas", label9.Text);
                    cmd.Parameters.AddWithValue("@Hora", toolStripLabel4.Text);
                    cmd.CommandType = CommandType.Text;
                    con.Open();

                    try
                    {
                        int i = cmd.ExecuteNonQuery();
                        if (i > 0);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro: " + ex.ToString());
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorreto!");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {

            if (Logado)
            {
                this.Hide();
                Home objLogin = new Home(textnome.Text,textBox1.Text,textBox1.Text);
                objLogin.ShowDialog();
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textsenha.UseSystemPasswordChar = false;
            }
            else
                textsenha.UseSystemPasswordChar = true;
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            label9.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)| *.* ";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
  
            string sql = "UPDATE login SET FOTOS=@Foto WHERE ID = @Id";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", textBox1.Text);
            cmd.Parameters.AddWithValue("@Foto", ConverterFotoParaByteArray());
            cmd.CommandType = CommandType.Text;
            con.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show("Registro incluido com sucesso!", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private byte[] ConverterFotoParaByteArray()
        {
            using (var stream = new System.IO.MemoryStream())
            {
                pictureBox1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                byte[] bArray = new byte[stream.Length];
                stream.Read(bArray, 0, System.Convert.ToInt32(stream.Length));
                return bArray;
            }
        }

        private void textnome_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (textnome.Text)

            {

                case "Carmi":

                    textBox1.Text = "14";
                    break;

                case "Rodrigo":

                    textBox1.Text = "10";
                    break;

                case "Admin":

                    textBox1.Text = "11";
                    break;
            }
           

        }
        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textnome_Click(object sender, EventArgs e)
        {

        }

        private void textnome_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textnome_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textnome_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_LocationChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_ModifiedChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        } 

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }
    }
    }


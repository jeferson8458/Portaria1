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
    public partial class editarfoto : Form
    {
        string connectionString = @"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True";
        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        string imgLocation = "";
        public editarfoto()
        {
            InitializeComponent();
        }

        public editarfoto(string buceta)
        {
            InitializeComponent();
            textBox3.Text = buceta;

            string sql = "SELECT * FROM login WHERE ID=@Id";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", textBox3.Text.ToString());
            SqlDataReader reader;
            con.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox3.Text = reader[0].ToString();
                    textBox1.Text = reader[1].ToString();
                    try
                    {
                        if (fotocirculo1.Image != null)
                        {
                            fotocirculo1.Image = ConverterFotoParaByteArray2((byte[])reader[3]);
                        }
                        else
                        {
                            fotocirculo1.Image = fotocirculo1.InitialImage;
                        }

                    }
                    catch
                    {
                        fotocirculo1.Image = fotocirculo1.InitialImage;
                    }

                }
                else
                    MessageBox.Show("Nenhum registro encontrado com o Id informado!");
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

        public Image ConverterFotoParaByteArray2(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)| *.* ";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                fotocirculo1.ImageLocation = imgLocation;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Preencha a senha", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.Focus();
                return;
            }
            string sql = "UPDATE login SET senha=@Senha, fotos=@Foto WHERE id = @Id";

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", textBox3.Text);
            cmd.Parameters.AddWithValue("@Senha", textBox2.Text);
            cmd.Parameters.AddWithValue("@Foto", ConverterFotoParaByteArray());
            cmd.CommandType = CommandType.Text;
            con.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show("Registro alterado com sucesso!", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
                this.Hide();
            }
        }
        private byte[] ConverterFotoParaByteArray()
        {
            using (var stream = new System.IO.MemoryStream())
            {
                fotocirculo1.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                byte[] bArray = new byte[stream.Length];
                stream.Read(bArray, 0, System.Convert.ToInt32(stream.Length));
                return bArray;
            }
        }

        private void editarfoto_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

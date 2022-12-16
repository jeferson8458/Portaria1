using AForge.Video;
using AForge.Video.DirectShow;
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
    public partial class User : Form
    {
        public byte[] FOTO { get; set; }
        string connectionString = @"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True";
        bool novo;
        string gender;
        string g;
        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        string imgLocation = "";
        private VideoCaptureDevice imagem_disp;

        public User()
        {
            InitializeComponent();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void VideoSource_Newframe(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void atualizartodalista()
        {
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "Select * FROM login";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            cn.Close();

            SqlConnection con = new SqlConnection(connectionString);
        }

        private void User_Load(object sender, EventArgs e)
        {
            atualizartodalista();
            txtId.Enabled = false;
            txtNome.Enabled = false;
            textsenha.Enabled = false;
            button1.Enabled = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            if (txtNome.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Campo de NOME preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }

            if (textsenha.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Campo de NOME preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textsenha.Focus();
                return;
            }

            if (novo)
            {
                string sql = "INSERT INTO login (usuario,senha,fotos) " + "VALUES (@usuario, @senha, @fotos)";

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@usuario", txtNome.Text);
                cmd.Parameters.AddWithValue("@senha", textsenha.Text);
                cmd.Parameters.AddWithValue("@fotos", ConverterFotoParaByteArray());
                cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show("Registro com sucesso!", "Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else
            {
                string sql = "UPDATE login SET usuario = @usuario, senha = @senha, " + " fotos = @fotos WHERE id = @id";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", txtId.Text);
                cmd.Parameters.AddWithValue("@usuario", txtNome.Text);
                cmd.Parameters.AddWithValue("@senha", textsenha.Text);
                cmd.Parameters.AddWithValue("@fotos", ConverterFotoParaByteArray());
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show("Registro atualizado com sucesso!", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            atualizartodalista();
            txtId.Enabled = false;
            txtNome.Enabled = false;
            textsenha.Enabled = false;
            button1.Enabled = false;
            txtId.Text = "";
            txtNome.Text = "";
            textsenha.Text = "";

            dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];

            pictureBox2.Image = pictureBox2.InitialImage;


        }
        private byte[] ConverterFotoParaByteArray()
        {
            using (var stream = new System.IO.MemoryStream())
            {
                pictureBox2.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                byte[] bArray = new byte[stream.Length];
                stream.Read(bArray, 0, System.Convert.ToInt32(stream.Length));
                return bArray;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sql = "SELECT * FROM login WHERE id=@Id";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            SqlDataReader reader;
            con.Open();
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {

                    txtId.Text = reader[0].ToString();
                    txtNome.Text = reader[1].ToString();
                    textsenha.Text = reader[2].ToString();
                    try
                    {
                        if (pictureBox2.Image != null)
                        {
                            pictureBox2.Image = ConverterFotoParaByteArray2((byte[])reader[3]);
                        }
                        else
                        {
                            pictureBox2.Image = pictureBox2.InitialImage;
                        }

                    }
                    catch
                    {
                        pictureBox2.Image = pictureBox2.InitialImage;
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

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM login WHERE id=@Id";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Id", txtId.Text);
            cmd.CommandType = CommandType.Text;
            con.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                    MessageBox.Show("Registro excluído com sucesso!", "Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.ToString());
            }
            finally
            {
                con.Close();
            }

            atualizartodalista();
            txtId.Enabled = false;
            txtNome.Enabled = false;
            textsenha.Enabled = false;
            button1.Enabled = false;
            txtId.Text = "";
            txtNome.Text = "";
            textsenha.Text = "";
            pictureBox2.Image = pictureBox2.InitialImage;
            dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (txtId.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Nenhum Cadastro Selecionado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void tsbNovo_Click(object sender, EventArgs e)
        {
            novo = true;
            txtId.Enabled = true;
            txtNome.Enabled = true;
            textsenha.Enabled = true;
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
  
            txtId.Enabled = false;
            txtNome.Enabled = false;
            textsenha.Enabled = false;
            button1.Enabled = false;
            txtId.Text = "";
            txtNome.Text = "";
            textsenha.Text = "";
            pictureBox2.Image = pictureBox2.InitialImage;
        }

        private void tsbEditar_Click_1(object sender, EventArgs e)
        {
            txtId.Enabled = true;
            txtNome.Enabled = true;
            textsenha.Enabled = true;
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)| *.* ";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pictureBox2.ImageLocation = imgLocation;
            }
        }
    }

    
    }


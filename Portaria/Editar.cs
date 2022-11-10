using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;
using System.IO;

namespace Portaria
{
    public partial class Editar : Form
    {
        public byte[] FOTO { get; set; }
        string connectionString = @"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True";
        bool novo2;
        string gender;
        string g;
        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        string imgLocation = "";

        public Editar()
        {
            InitializeComponent();
        }
        private FilterInfoCollection Dispositivo;
        private VideoCaptureDevice imagem_disp;
        private void Editar_Load(object sender, EventArgs e)
        {
            atualizartodalista();

            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tsbEditar.Enabled = true;
            txtId.Enabled = true;
            txtNome2.Enabled = false;
            txtCPF2.Enabled = false;
            comboFU.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            txtdata2.Enabled = false;
            txthora2.Enabled = false;
            comboFU.Enabled = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            button1.Enabled = false;
            button2.Enabled = false;

            Image img = null;

                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    dataGridView1.Rows[i].Cells["FOTO"].Value = img;

                }
   
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.GetLastRow(DataGridViewElementStates.Visible);
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];


            Dispositivo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in Dispositivo)
            {
                comboBox1.Items.Add(device.Name);
            }
            imagem_disp = new VideoCaptureDevice();
        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                g = "1";
            }
            else

                g = "0";


            if (txtNome2.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Campo de NOME preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome2.Focus();
                return;
            }
            if (txtCPF2.MaskCompleted)
            {

            }
            else
            {
                MessageBox.Show("Campo de CPF preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCPF2.Focus();
                return;
            }
            if (comboSetor2.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Campo de SETOR VISITADO preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboSetor2.Focus();
                return;
            }
            if (comboFU.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Campo de VISITANTE preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboFU.Focus();
                return;
            }
            if (comboSocio2.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Campo de TRABALHADOR preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboSocio2.Focus();
                return;
            }

            if (novo2)
            {
                string sql = "INSERT INTO PORTARIA (NOME,CPF,SETOR,SOCIO,DATAS,HORA,FUNCAO) " + "VALUES (@Nome, @Cpf, @Setor, @Socio, @Data, @Hora, @Funcao)";

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Nome", txtNome2.Text);
                cmd.Parameters.AddWithValue("@Cpf", txtCPF2.Text);
                cmd.Parameters.AddWithValue("@Setor", comboSetor2.Text);
                cmd.Parameters.AddWithValue("@Socio", comboSocio2.Text);
                cmd.Parameters.AddWithValue("@Hora", txthora2.Text);
                cmd.Parameters.AddWithValue("@Data", txtdata2.Text);
                cmd.Parameters.AddWithValue("@Funcao", comboFU.Text);
                cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show("Registro atualizado com sucesso!", "Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string sql = "UPDATE PORTARIA SET NOME = @Nome, CPF = @Cpf, SETOR = @Setor, SOCIO = @Socio, DATAS = @Data, " + " HORA = @Hora, FUNCAO = @Funcao, GENDER=@Gender, FOTO=@Foto WHERE ID = @Id";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.Parameters.AddWithValue("@Nome", txtNome2.Text);
                cmd.Parameters.AddWithValue("@Cpf", txtCPF2.Text);
                cmd.Parameters.AddWithValue("@Setor", comboSetor2.Text);
                cmd.Parameters.AddWithValue("@Socio", comboSocio2.Text);
                cmd.Parameters.AddWithValue("@Hora", txthora2.Text);
                cmd.Parameters.AddWithValue("@Data", txtdata2.Text);
                cmd.Parameters.AddWithValue("@Funcao", comboFU.Text);
                cmd.Parameters.AddWithValue("@Gender", g);
                cmd.Parameters.AddWithValue("@Foto", ConverterFotoParaByteArray());
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
            button1.Enabled = false;
            button2.Enabled = false;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tsbEditar.Enabled = false;
            txtNome2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            comboFU.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            txtId.Text = "";
            txtNome2.Text = "";
            txtCPF2.Text = "";
            comboSetor2.SelectedIndex = -1;
            comboSocio2.SelectedIndex = -1;
            comboFU.SelectedIndex = -1;
            radioButton1.Checked = false;
            radioButton2.Checked = false;


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
        public Image ConvertByte(byte[] data)
        {
            using(MemoryStream ms= new MemoryStream(data))
            {
                return Image.FromStream(ms);
            }
        }
        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM PORTARIA WHERE ID=@ID";
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
            button1.Enabled = false;
            button2.Enabled = false;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tsbEditar.Enabled = false;
            txtNome2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            comboFU.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            txtId.Text = "";
            txtNome2.Text = "";
            txtCPF2.Text = "";
            comboSetor2.SelectedIndex = -1;
            comboSocio2.SelectedIndex = -1;
            txtdata2.Text = "";
            txthora2.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboFU.SelectedIndex = -1;

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
            button1.Enabled = true;
            button2.Enabled = true;
            tsbCancelar.Enabled = true;
            tsbExcluir.Enabled = true;
            tsbSalvar.Enabled = true;
            txtNome2.Enabled = true;
            tsbEditar.Enabled = false;
            tsbCancelar.Enabled = true;
            txtCPF2.Enabled = true;
            comboSetor2.Enabled = true;
            comboSocio2.Enabled = true;
            txtCPF2.Enabled = true;
            comboSetor2.Enabled = true;
            comboSocio2.Enabled = true;
            comboFU.Enabled = true;
            txtdata2.Enabled = false;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            txtNome2.Focus();
        }
        private void atualizartodalista()
        {
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            txtId.Enabled = true;
            txtNome2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            txtdata2.Enabled = false;
            txthora2.Enabled = false;
            comboFU.Enabled = false;

            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "Select * FROM PORTARIA";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            cn.Close();

            SqlConnection con = new SqlConnection(connectionString);
            cmd.CommandType = CommandType.Text;
            dataGridView1.Columns[0].Width = 22;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 45;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "Select * FROM PORTARIA where Nome  like ('" + textBox1.Text + "%')";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            cn.Close();
        }

        private void textBox2_ValueChanged(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "Select * FROM PORTARIA where Datas  like ('" + textBox2.Text + "%')";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            cn.Close();

            Total.Text = dataGridView1.Rows.Count.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtCPF2.Mask = radioButton1.Checked ? @"000\.000\.000\-00" : @"00\.000\.000\-0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtCPF2.Mask = radioButton1.Checked ? @"00\.000\.000\-0" : @"00\.000\.000\-0";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sql = "SELECT * FROM PORTARIA WHERE ID=@Id";
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
                    tsbCancelar.Enabled = false;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    tsbExcluir.Enabled = false;
                    tsbSalvar.Enabled = false;
                    tsbEditar.Enabled = true;
                    txtNome2.Enabled = false;
                    tsbCancelar.Enabled = false;
                    txtCPF2.Enabled = false;
                    comboSetor2.Enabled = false;
                    comboSocio2.Enabled = false;
                    txtCPF2.Enabled = false;
                    comboSetor2.Enabled = false;
                    comboSocio2.Enabled = false;
                    comboFU.Enabled = false;
                    txtNome2.Focus();
                    string genero = reader["GENDER"].ToString();
                    switch (genero)
                    {
                        case "1":
                            radioButton1.Checked = true;//cpf
                            radioButton2.Checked = false;
                            txtCPF2.Mask = radioButton1.Checked ? @"000\.000\.000\-00" : @"00\.000\.000\-0";
                            break;
                        case "0":
                            radioButton2.Checked = true;//RG
                            radioButton1.Checked = false;
                            txtCPF2.Mask = radioButton1.Checked ? @"000\.000\.000\-00" : @"00\.000\.000\-0";
                            break;

                    }
                    txtId.Text = reader[0].ToString();
                    txtNome2.Text = reader[1].ToString();
                    txtCPF2.Text = reader[2].ToString();
                    comboSetor2.Text = reader[3].ToString();
                    comboSocio2.Text = reader[4].ToString();
                    txtdata2.Text = reader[5].ToString();
                    txthora2.Text = reader[6].ToString();
                    comboFU.Text = reader[7].ToString();
                    try
                    {
                        if (pictureBox2.Image != null)
                        {
                            pictureBox2.Image = ConverterFotoParaByteArray2((byte[])reader[9]);
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

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            if (imagem_disp.IsRunning)
            {
                imagem_disp.Stop();
                pictureBox2.Image = pictureBox2.InitialImage;
                pictureBox2.Invalidate();
            }
            button1.Enabled = false;
            button2.Enabled = false;
            tsbSalvar.Enabled = false;
            tsbEditar.Enabled = true;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            txtNome2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            comboFU.Enabled = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            txtId.Text = "";
            txtNome2.Text = "";
            txtCPF2.Text = "";
            txtdata2.Text = "";
            txthora2.Text = "";
            comboSetor2.SelectedIndex = -1;
            comboSocio2.SelectedIndex = -1;
            comboFU.SelectedIndex = -1;
            pictureBox2.Image = pictureBox2.InitialImage;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "Select * FROM PORTARIA where CPF like ('" + textBox3.Text + "%')";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            cn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (imagem_disp.IsRunning)
            {
                imagem_disp.Stop();
                pictureBox2.Image = pictureBox2.InitialImage;
                pictureBox2.Invalidate();
            }
            else
            {
                if (comboBox1.Text != string.Empty)
                {
                    imagem_disp = new VideoCaptureDevice(Dispositivo[comboBox1.SelectedIndex].MonikerString);
                    imagem_disp.NewFrame += new NewFrameEventHandler(imagem_disp_NewFrame);
                    imagem_disp.Start();
                }
                else
                {
                    MessageBox.Show("Webcam Desconectada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNome2.Focus();
                    return;
                }
            }
        }
        private void imagem_disp_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap imagem = (Bitmap)eventArgs.Frame.Clone();
            pictureBox2.Image = imagem;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (imagem_disp.IsRunning)
            {
                imagem_disp.Stop();
                pictureBox2.Image = (Bitmap)pictureBox2.Image.Clone();
                pictureBox2.Invalidate();
            }
        }

        private void Editar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (imagem_disp.IsRunning)
                imagem_disp.Stop();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
      
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }


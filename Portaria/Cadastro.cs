using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portaria
{
    public partial class Cadastro : Form
    {
        public byte[] FOTO { get; set; }
        string connectionString = @"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True";
        Conexão con = new Conexão();
        bool novo;
        string g;
        bool novo2;
        bool novo3;
        string imgLocation = "";
        SqlCommand cmd;

        public Cadastro()
        {
            InitializeComponent();
        }
        private FilterInfoCollection Dispositivo;
        private VideoCaptureDevice imagem_disp;
        private void Cadastro_Load(object sender, EventArgs e)
        {
            tsbNovo.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = false;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            txtNome.Enabled = false;
            txtCPF.Enabled = false;
            comboSetor.Enabled = false;
            comboSocio.Enabled = false;
            comboFU.Enabled = false;
            label9.Text = DateTime.Now.ToString("dd/MM/yyyy");
            radioButton1.Checked = true;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;

            Dispositivo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in Dispositivo)
            {
                comboBox1.Items.Add(device.Name);
            }
            imagem_disp = new VideoCaptureDevice();

            
        }

        private void tsbNovo_Click(object sender, EventArgs e)
        {
            tsbNovo.Enabled = false;
            button2.Enabled = true;
            button4.Enabled = true;
            tsbSalvar.Enabled = true;
            tsbCancelar.Enabled = true;
            txtNome.Enabled = true;
            txtCPF.Enabled = true;
            comboSetor.Enabled = true;
            comboSocio.Enabled = true;
            comboFU.Enabled = true;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            txtNome.Focus();
            novo = true;
        }
        private void tsbSalvar_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true)
            {
                g = "1";
            }
            else

                g = "0";


            if (txtNome.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Campo de NOME preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNome.Focus();
                return;
            }
            if (txtCPF.MaskCompleted)
            {

            }
            else
            {
                MessageBox.Show("Campo de CPF preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCPF.Focus();
                return;
            }
            if (comboSetor.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Campo de SETOR VISITADO preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboSetor.Focus();
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
            if (comboSocio.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Campo de TRABALHADOR preenchido incorretamento", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboSocio.Focus();
                return;
            }
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "Select COUNT(*) from PORTARIA WHERE CPF = @CPF";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@CPF", txtCPF.Text);

                int cant = Convert.ToInt32(cmd.ExecuteScalar());

                if (cant == 0)
                {

                }
                else
                {
                    MessageBox.Show("ESSE CPF/RG JÀ EXISTE NO BANCO", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                string query = "Select COUNT(*) from PORTARIA WHERE NOME = @NOME";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@NOME", txtNome.Text);

                int cant = Convert.ToInt32(cmd.ExecuteScalar());

                if (cant == 0)
                {

                }
                else
                {
                    MessageBox.Show("ESSE NOME JÀ EXISTE NO BANCO", "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            if (novo)
            {
                string sql = "INSERT INTO PORTARIA (NOME,CPF,SETOR,SOCIO,DATAS,HORA,FUNCAO,GENDER,FOTO) " + "VALUES (@Nome, @Cpf, @Setor, @Socio, @Data, @Hora, @Funcao, @Gender, @Foto)";

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@Cpf", txtCPF.Text);
                cmd.Parameters.AddWithValue("@Setor", comboSetor.Text);
                cmd.Parameters.AddWithValue("@Socio", comboSocio.Text);
                cmd.Parameters.AddWithValue("@Hora", label8.Text);
                cmd.Parameters.AddWithValue("@Data", label9.Text);
                cmd.Parameters.AddWithValue("@Funcao", comboFU.Text);
                cmd.Parameters.AddWithValue("@Gender", radioButton1.Checked);
                cmd.Parameters.AddWithValue("@Foto", ConverterFotoParaByteArray());
                cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    string jef = txtNome.Text;
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show("Registro " + jef + " incluido com sucesso!", "Salvo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string sql = "UPDATE PORTARIA SET NOME=@Nome, CPF=@Cpf, SETOR=@Setor " + " SOCIO=@Socio, DATAS=@Data, HORA=@Hora, FUNCAO = @Funcao WHERE ID=@Id";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@Cpf", txtCPF.Text);
                cmd.Parameters.AddWithValue("@Setor", comboSetor.Text);
                cmd.Parameters.AddWithValue("@Socio", comboSocio.Text);
                cmd.Parameters.AddWithValue("@Hora", label8.Text);
                cmd.Parameters.AddWithValue("@Data", label9.Text);
                cmd.Parameters.AddWithValue("@Funcao", comboFU.Text);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show("Registro atualizado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.ToString());
                }
                finally
                {

                }
            }

            if (novo)
            {
                string sql = "INSERT INTO PRESENCA (NOME,CPF,SETOR,SOCIO,DATAS,HORA,MES,FUNCAO) " + "VALUES (@Nome, @Cpf, @Setor, @Socio, @Data, @Hora, @Mes, @Funcao)";

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@Cpf", txtCPF.Text);
                cmd.Parameters.AddWithValue("@Setor", comboSetor.Text);
                cmd.Parameters.AddWithValue("@Socio", comboSocio.Text);
                cmd.Parameters.AddWithValue("@Hora", label8.Text);
                cmd.Parameters.AddWithValue("@Data", label9.Text);
                cmd.Parameters.AddWithValue("@Mes", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@Funcao", comboFU.Text);

                cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0) ;
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
                string sql = "UPDATE PRESENCA SET NOME = @Nome, CPF = @Cpf, SETOR = @Setor " + " SOCIO = @Socio, DATAS = @Data, HORA = @Hora, MES = @Mes, FUNCAO = @Funcao WHERE ID=@Id";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@Cpf", txtCPF.Text);
                cmd.Parameters.AddWithValue("@Setor", comboSetor.Text);
                cmd.Parameters.AddWithValue("@Socio", comboSocio.Text);
                cmd.Parameters.AddWithValue("@Hora", label8.Text);
                cmd.Parameters.AddWithValue("@Data", label9.Text);
                cmd.Parameters.AddWithValue("@Mes", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@Funcao", comboFU.Text);
                cmd.CommandType = CommandType.Text;
                con.Open();
                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                        MessageBox.Show("Registro atualizado com sucesso!");
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

            if (textBox1.Text == comboSetor.Text) 
            {
                string sql = "INSERT INTO RECIBOS (NOME,CPF) " + "VALUES (@Nome, @Cpf)";

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Nome", txtNome.Text);
                cmd.Parameters.AddWithValue("@Cpf", txtCPF.Text);

                cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0) ;
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
                MessageBox.Show("Tomou no Cu", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }



            tsbNovo.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = false;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            txtNome.Enabled = false;
            txtCPF.Enabled = false;
            comboSetor.Enabled = false;
            comboSocio.Enabled = false;
            comboFU.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            txtId.Text = "";
            txtNome.Text = "";
            txtCPF.Text = "";
            comboSetor.SelectedIndex = -1;
            comboSocio.SelectedIndex = -1;
            comboFU.SelectedIndex = -1;
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
        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            if (imagem_disp.IsRunning)
            {
                imagem_disp.Stop();
                pictureBox2.Image = pictureBox2.InitialImage;
                pictureBox2.Invalidate();
            }

            tsbNovo.Enabled = true;
            button2.Enabled = false;
            button4.Enabled = false;
            tsbSalvar.Enabled = false;
            tsbCancelar.Enabled = false;
            txtNome.Enabled = false;
            txtCPF.Enabled = false;
            comboSetor.Enabled = false;
            comboSocio.Enabled = false;
            txtCPF.Enabled = false;
            comboSetor.Enabled = false;
            comboSocio.Enabled = false;
            comboFU.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            txtId.Text = "";
            txtNome.Text = "";
            txtCPF.Text = "";
            comboSetor.SelectedIndex = -1;
            comboSocio.SelectedIndex = -1;
            comboFU.SelectedIndex = -1;
            pictureBox2.Image = pictureBox2.InitialImage;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            txtCPF.Mask = radioButton1.Checked ? @"000\.000\.000\-00" : @"00\.000\.000\-0";
            txtCPF.Text = "";
            txtCPF.Focus();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtCPF.Mask = radioButton1.Checked ? @"00\.000\.000\-0" : @"00\.000\.000\-0";
            txtCPF.Text = "";
            txtCPF.Focus();
        }

        private void txtCPF_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            int iPosicaoDoCursor = 0;
            txtCPF.SelectionStart = iPosicaoDoCursor;
            txtCPF.ScrollToCaret();

        }

        private void Horaportaria_Tick(object sender, EventArgs e)
        {
            label8.Text = DateTime.Now.ToString("HH:mm");
        }
        private void txtCPF_Click(object sender, EventArgs e)
        {
            int iPosicaoDoCursor = 0;
            txtCPF.SelectionStart = iPosicaoDoCursor;
            txtCPF.ScrollToCaret();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void txtCPF_Click_1(object sender, EventArgs e)
        {

            if (txtCPF.MaskCompleted)
            {

            }
            else
            {
                int iPosicaoDoCursor = 0;
                txtCPF.SelectionStart = iPosicaoDoCursor;
                txtCPF.ScrollToCaret();
            }



        }

        private void comboSocio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
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
                    comboBox1.Focus();
                    return;
                }
            }
        }
        private void imagem_disp_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap imagem = (Bitmap)eventArgs.Frame.Clone();
            pictureBox2.Image = imagem;
        }



        private void button4_Click(object sender, EventArgs e)
        {
            if (imagem_disp.IsRunning)
            {
                imagem_disp.Stop();
                pictureBox2.Image = (Bitmap)pictureBox2.Image.Clone();
                pictureBox2.Invalidate();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Cadastro_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void Cadastro_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Cadastro_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void Cadastro_Deactivate(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


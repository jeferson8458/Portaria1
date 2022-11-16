using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Media;

namespace Portaria
{
    public partial class Presença : Form
    {
        SqlDataAdapter pageAdapter;
        DataSet pageDS;
        int scollVal;
        public byte[] FOTO { get; set; }
        string connectionString = @"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True";
        bool novo4;
        bool jef = true;
        string g;
        bool novo2;

        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        string imgLocation = "";
        private VideoCaptureDevice videoSource;

        public Presença()
        {
            InitializeComponent();

            var videoSources = new FilterInfoCollection(FilterCategory.AudioInputDevice);
            if (videoSources != null && videoSources.Count > 0)
            {
                videoSource = new VideoCaptureDevice(videoSources[0].MonikerString);
                videoSource.NewFrame += VideoSource_Newframe;
            }

        }
        private FilterInfoCollection Dispositivo;
        private VideoCaptureDevice imagem_disp;

        private void VideoSource_Newframe(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Presença_Load(object sender, EventArgs e)
        {
            atualizartodalista();


            label14.Visible = false;
            label15.Visible = false;
            dateTimePicker3.Visible = false;
            dateTimePicker4.Visible = false;

            tsbSalvar.Enabled = true;
            tsbEditar.Enabled = false;
            button2.Enabled = false;
            button1.Enabled = false;
            button3.Enabled = true;
            tsbCancelar.Enabled = false;
            txtId.Enabled = true;
            txtNome2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            txtdata2.Enabled = false;
            txthora2.Enabled = false;
            comboFU.Enabled = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            tsbExcluir.Enabled = false;
            button3.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker4.Enabled = false;

            label9.Text = DateTime.Now.ToString("dd/MM/yyyy");
            toolStripLabel4.Text = DateTime.Now.ToString("HH:mm");

            Image img = null;

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells["FOTO"].Value = img;

            }

            //bunifuDataGridView1.FirstDisplayedScrollingRowIndex = bunifuDataGridView1.Rows.GetLastRow(DataGridViewElementStates.Visible);
            //bunifuDataGridView1.CurrentCell = bunifuDataGridView1.Rows[bunifuDataGridView1.Rows.Count - 1].Cells[0];


            Dispositivo = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in Dispositivo)
            {
                comboBox1.Items.Add(device.Name);
            }
            imagem_disp = new VideoCaptureDevice();


        }
        private void bosta(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void tsbSalvar_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                g = "1";
            }
            else

                g = "0";

            if (txtid02.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Nenhum cadastro selecionado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtId.Text != string.Empty)
            {

            }
            else
            {
                MessageBox.Show("Nenhum cadastro selecionado!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (comboSetor2.Text == "PILATES/DANCA")
            {
                if (Convert.ToDateTime(label9.Text) <= Convert.ToDateTime(dateTimePicker4.Text))
                {
                    var player = new SoundPlayer("C:/mario.wav");
                    player.Play();
                    Notificação1 n = new Notificação1("Pagamento Liberado", 1);
                    n.ShowDialog();
                }
                else
                {
                    var player = new SoundPlayer("C:/error.wav");
                    player.Play();
                    Notificação1 n = new Notificação1("Deverá efetuar o pagamento de Matricula", 2);
                    n.ShowDialog();
                    txtdata2.Focus();
                    return;
                }
            }
            else
            {
             
            }
          

            if (novo4)
            {
                string sql = "INSERT INTO PRESENCA (NOME,CPF,SETOR,SOCIO,DATAS,HORA,MES,FUNCAO) " + "VALUES (@Nome, @Cpf, @Setor, @Socio, @Data, @Hora, @Mes, @Funcao)";

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Nome", txtNome2.Text);
                cmd.Parameters.AddWithValue("@Cpf", txtCPF2.Text);
                cmd.Parameters.AddWithValue("@Setor", comboSetor2.Text);
                cmd.Parameters.AddWithValue("@Socio", comboSocio2.Text);
                cmd.Parameters.AddWithValue("@Hora", label22.Text);
                cmd.Parameters.AddWithValue("@Data", label9.Text);
                cmd.Parameters.AddWithValue("@Mes", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@Funcao", comboFU.Text);

                cmd.CommandType = CommandType.Text;
                con.Open();

                try
                {
                    string jef = txtNome2.Text;
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0);
                    var player = new SoundPlayer("C:/mario.wav");
                    player.Play();
                    Notificação1 n = new Notificação1("Registro " + jef + " incluido com sucesso!", 1);
                    n.ShowDialog();
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
                cmd.Parameters.AddWithValue("@Nome", txtNome2.Text);
                cmd.Parameters.AddWithValue("@Cpf", txtCPF2.Text);
                cmd.Parameters.AddWithValue("@Setor", comboSetor2.Text);
                cmd.Parameters.AddWithValue("@Socio", comboSocio2.Text);
                cmd.Parameters.AddWithValue("@Hora", label22.Text);
                cmd.Parameters.AddWithValue("@Data", txtdata2.Text);
                cmd.Parameters.AddWithValue("@Mes", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@Funcao", comboFU.Text);
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

            if (novo4)
            {
                string sql = "UPDATE PORTARIA SET " + " FOTO=@Foto WHERE ID = @Id";
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", txtId.Text);
                cmd.Parameters.AddWithValue("@Foto", ConverterFotoParaByteArray());
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

            atualizartodalista();

            tsbSalvar.Enabled = true;
            button2.Enabled = false;
            button1.Enabled = false;
            tsbCancelar.Enabled = true;
            txtNome2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            comboFU.Enabled = false;
            txtid02.Text = "";
            txtId.Text = "";
            txtNome2.Text = "";
            txtCPF2.Text = "";
            textBox1.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboSetor2.SelectedIndex = -1;
            comboSocio2.SelectedIndex = -1;
            comboFU.SelectedIndex = -1;


            dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            pictureBox1.Image = pictureBox1.InitialImage;


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
        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            if (imagem_disp.IsRunning)
            {
                imagem_disp.Stop();
                pictureBox1.Image = pictureBox1.InitialImage;
                pictureBox1.Invalidate();
            }
            tsbSalvar.Enabled = true;
            button2.Enabled = false;
            button1.Enabled = false;
            tsbCancelar.Enabled = false;
            txtNome2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            comboFU.Enabled = false;
            radioButton1.Enabled = true;
            radioButton2.Enabled = true;
            button3.Enabled = false;
            tsbExcluir.Enabled = false;
            tsbEditar.Enabled = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker4.Enabled = false;
            txtId.Text = "";
            txtNome2.Text = "";
            txtCPF2.Text = "";
            txtdata2.Text = "";
            txthora2.Text = "";
            textBox1.Text = "";
            comboSetor2.SelectedIndex = -1;
            comboSocio2.SelectedIndex = -1;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboFU.SelectedIndex = -1;
            pictureBox1.Image = pictureBox1.InitialImage;
            txtId.Focus();
            atualizartodalista();
            //dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
        }
        private void atualizartodalista()
        {
            
            tsbSalvar.Enabled = true;
            tsbCancelar.Enabled = true;
            txtId.Enabled = true;
            txtNome2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            txtdata2.Enabled = false;
            txthora2.Enabled = false;
            comboFU.Enabled = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;

            string constr = @"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True";
            SqlConnection con = new SqlConnection(constr);
            con.Open();
            pageAdapter = new SqlDataAdapter("Select * FROM PORTARIA", con);
            pageDS = new DataSet();
            pageAdapter.Fill(pageDS, scollVal, 10, "contomeres");
            con.Close();
            dataGridView1.DataSource = pageDS;
            dataGridView1.DataMember = "contomeres";

            //cn.Open();
            //SqlCommand cmd = cn.CreateCommand();
            //cmd.CommandText = "Select * FROM PORTARIA";
            //cmd.ExecuteNonQuery();

            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);

            //da.Fill(dt);

            //bunifuDataGridView1.DataSource = dt;

            //cn.Close();

            //SqlConnection con = new SqlConnection(connectionString);
            //cmd.CommandType = CommandType.Text;
            //dataGridView1.Columns[0].Width = 22;

            novo4 = true;

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
            cmd.CommandText = "Select * FROM PORTARIA where Datas  like ('" + dateTimePicker1.Text + "%')";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            cn.Close();


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "Select * FROM PORTARIA where Datas  like ('" + dateTimePicker1.Text + "%')";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            label9.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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
                    tsbCancelar.Enabled = true;
                    tsbSalvar.Enabled = true;
                    txtNome2.Enabled = false;
                    tsbCancelar.Enabled = true;
                    txtCPF2.Enabled = false;
                    comboSetor2.Enabled = false;
                    comboSocio2.Enabled = false;
                    txtCPF2.Enabled = false;
                    comboSetor2.Enabled = true;
                    comboSocio2.Enabled = false;
                    comboFU.Enabled = false;
                    txtId.Text = "";
                    txtNome2.Text = "";
                    txtCPF2.Text = "";
                    txtdata2.Text = "";
                    txthora2.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    comboSetor2.SelectedIndex = -1;
                    comboSocio2.SelectedIndex = -1;
                    comboFU.SelectedIndex = -1;
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {


        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
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
                    tsbEditar.Enabled = true;
                    tsbCancelar.Enabled = true;
                    button2.Enabled = true;
                    button1.Enabled = true;
                    tsbSalvar.Enabled = true;
                    txtNome2.Enabled = false;
                    tsbCancelar.Enabled = true;
                    txtCPF2.Enabled = false;
                    comboSetor2.Enabled = false;
                    comboSocio2.Enabled = false;
                    txtCPF2.Enabled = false;
                    comboSetor2.Enabled = true;
                    comboSocio2.Enabled = false;
                    comboFU.Enabled = false;
                    txtId.Text = "";
                    txtNome2.Text = "";
                    txtCPF2.Text = "";
                    txtdata2.Text = "";
                    txthora2.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    comboSetor2.SelectedIndex = -1;
                    comboSocio2.SelectedIndex = -1;
                    comboFU.SelectedIndex = -1;
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
                    txtid02.Text = reader[0].ToString();
                    txtNome2.Text = reader[1].ToString();
                    txtCPF2.Text = reader[2].ToString();
                    comboSetor2.Text = reader[3].ToString();
                    comboSocio2.Text = reader[4].ToString();
                    txtdata2.Text = reader[5].ToString();
                    txthora2.Text = reader[6].ToString();
                    comboFU.Text = reader[7].ToString();
                    dateTimePicker3.Text = reader[10].ToString();
                    dateTimePicker4.Text = reader[11].ToString();
                    try
                    {
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image = ConverterFotoParaByteArray2((byte[])reader[9]);
                        }
                        else
                        {
                            pictureBox1.Image = pictureBox1.InitialImage;
                        }

                    }
                    catch
                    {
                        pictureBox1.Image = pictureBox1.InitialImage;
                    }

                    if (comboSetor2.Text == "PILATES/DANCA")
                    {
                        label14.Visible = true;
                        label15.Visible = true;
                        dateTimePicker3.Visible = true;
                        dateTimePicker4.Visible = true;
                    }
                    else
                    {
                        label14.Visible = false;
                        label15.Visible = false;
                        dateTimePicker3.Visible = false;
                        dateTimePicker4.Visible = false;
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



        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "Select * FROM PORTARIA where Datas  like ('" + dateTimePicker1.Text + "%')";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            cn.Close();
        }

        private void Horaportaria_Tick(object sender, EventArgs e)
        {
            label22.Text = DateTime.Now.ToString("HH:mm");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "png files(*.png)|*.png|jpg files(*.jpg)|*.jpg|All files(*.*)| *.* ";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;
            }
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

        private void button4_Click(object sender, EventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.Stop();
                pictureBox1.Image = null;
                pictureBox1.Invalidate();
            }
            else
            {
                videoSource.Start();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (imagem_disp.IsRunning)
            {
                imagem_disp.Stop();
                pictureBox1.Image = pictureBox1.InitialImage;
                pictureBox1.Invalidate();
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
            pictureBox1.Image = imagem;
        }
        private void Editar_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (imagem_disp.IsRunning)
                imagem_disp.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (imagem_disp.IsRunning)
            {
                imagem_disp.Stop();
                pictureBox1.Image = (Bitmap)pictureBox1.Image.Clone();
                pictureBox1.Invalidate();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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
            button3.Enabled = true;
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
            tsbSalvar.Enabled = false;
            dateTimePicker3.Enabled = true;
            dateTimePicker4.Enabled = true;
            dateTimePicker3.Enabled = true;
            dateTimePicker4.Enabled = true;
            txtNome2.Focus();
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

            //atualizartodalista();
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            tsbSalvar.Enabled = true;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tsbEditar.Enabled = true;
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
            dateTimePicker3.Enabled = false;
            dateTimePicker4.Enabled = false;
            txtId.Text = "";
            txtNome2.Text = "";
            txtCPF2.Text = "";
            comboSetor2.SelectedIndex = -1;
            comboSocio2.SelectedIndex = -1;
            txtdata2.Text = "";
            txthora2.Text = "";
            comboFU.SelectedIndex = -1;

            dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
        }

        private void button3_Click_1(object sender, EventArgs e)
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
                string sql = "INSERT INTO PORTARIA (NOME,CPF,SETOR,SOCIO,DATAS,HORA,FUNCAO, PAGAMENTO, VENCIMENTO) " + "VALUES (@Nome, @Cpf, @Setor, @Socio, @Data, @Hora, @Funcao, @Pagamento, @Vencimento)";

                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Nome", txtNome2.Text);
                cmd.Parameters.AddWithValue("@Cpf", txtCPF2.Text);
                cmd.Parameters.AddWithValue("@Setor", comboSetor2.Text);
                cmd.Parameters.AddWithValue("@Socio", comboSocio2.Text);
                cmd.Parameters.AddWithValue("@Hora", txthora2.Text);
                cmd.Parameters.AddWithValue("@Data", txtdata2.Text);
                cmd.Parameters.AddWithValue("@Funcao", comboFU.Text);
                cmd.Parameters.AddWithValue("@Pagamento", dateTimePicker3.Text);
                cmd.Parameters.AddWithValue("@Vencimento", dateTimePicker4.Text);
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
                string sql = "UPDATE PORTARIA SET NOME = @Nome, CPF = @Cpf, SETOR = @Setor, SOCIO = @Socio, DATAS = @Data, " + " HORA = @Hora, FUNCAO = @Funcao, GENDER=@Gender, FOTO=@Foto, PAGAMENTO=@Pagamento, VENCIMENTO=@Vencimento WHERE ID = @Id";
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
                cmd.Parameters.AddWithValue("@Pagamento", dateTimePicker3.Text);
                cmd.Parameters.AddWithValue("@Vencimento", dateTimePicker4.Text);
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
            tsbSalvar.Enabled = true;
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            tsbEditar.Enabled = true;
            txtNome2.Enabled = false;
            txtCPF2.Enabled = false;
            comboSetor2.Enabled = false;
            comboSocio2.Enabled = false;
            comboFU.Enabled = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            button3.Enabled = false;
            txtId.Text = "";
            txtNome2.Text = "";
            txtCPF2.Text = "";
            textBox1.Text = "";
            comboSetor2.SelectedIndex = -1;
            comboSocio2.SelectedIndex = -1;
            comboFU.SelectedIndex = -1;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker3.Enabled = false;
            dateTimePicker4.Enabled = false;


            dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];

            pictureBox1.Image = pictureBox1.InitialImage;
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

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboSetor2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboSetor2.Text == "PILATES/DANCA")
            {
                label14.Visible = true;
                label15.Visible = true;
                dateTimePicker3.Visible = true;
                dateTimePicker4.Visible = true;
            }
            else
            {
                label14.Visible = false;
                label15.Visible = false;
                dateTimePicker3.Visible = false;
                dateTimePicker4.Visible = false;
            }
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
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
                    tsbEditar.Enabled = true;
                    tsbCancelar.Enabled = true;
                    button2.Enabled = true;
                    button1.Enabled = true;
                    tsbSalvar.Enabled = true;
                    txtNome2.Enabled = false;
                    tsbCancelar.Enabled = true;
                    txtCPF2.Enabled = false;
                    comboSetor2.Enabled = false;
                    comboSocio2.Enabled = false;
                    txtCPF2.Enabled = false;
                    comboSetor2.Enabled = true;
                    comboSocio2.Enabled = false;
                    comboFU.Enabled = false;
                    txtId.Text = "";
                    txtNome2.Text = "";
                    txtCPF2.Text = "";
                    txtdata2.Text = "";
                    txthora2.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    comboSetor2.SelectedIndex = -1;
                    comboSocio2.SelectedIndex = -1;
                    comboFU.SelectedIndex = -1;
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
                    txtid02.Text = reader[0].ToString();
                    txtNome2.Text = reader[1].ToString();
                    txtCPF2.Text = reader[2].ToString();
                    comboSetor2.Text = reader[3].ToString();
                    comboSocio2.Text = reader[4].ToString();
                    txtdata2.Text = reader[5].ToString();
                    txthora2.Text = reader[6].ToString();
                    comboFU.Text = reader[7].ToString();
                    dateTimePicker3.Text = reader[10].ToString();
                    dateTimePicker4.Text = reader[11].ToString();
                    try
                    {
                        if (pictureBox1.Image != null)
                        {
                            pictureBox1.Image = ConverterFotoParaByteArray2((byte[])reader[9]);
                        }
                        else
                        {
                            pictureBox1.Image = pictureBox1.InitialImage;
                        }

                    }
                    catch
                    {
                        pictureBox1.Image = pictureBox1.InitialImage;
                    }

                    if (comboSetor2.Text == "PILATES/DANCA")
                    {
                        label14.Visible = true;
                        label15.Visible = true;
                        dateTimePicker3.Visible = true;
                        dateTimePicker4.Visible = true;
                    }
                    else
                    {
                        label14.Visible = false;
                        label15.Visible = false;
                        dateTimePicker3.Visible = false;
                        dateTimePicker4.Visible = false;
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
    }
}

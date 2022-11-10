using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Portaria
{
    public partial class ListaPresença : Form
    {
        string connectionString = @"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True";
        bool novo4;
        bool jef = true;
        string g;

        SqlConnection cn = new SqlConnection(@"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public ListaPresença()
        {
            InitializeComponent();
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
        }

        private void ListaPresença_Load(object sender, EventArgs e)
        {
            string jefão;
            jefão = dateTimePicker2.Text;
            label1.Text = jefão;
            label5.Text = dateTimePicker1.Text;
            string text = label1.Text;
            dashboard02();
            atualizartodalista();
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.GetLastRow(DataGridViewElementStates.Visible);
            dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        private void dashboard02()
        {///SELECT SOCIO COUNT(*) as Total FROM PORTARIA GROUP BY SOCIO=@socio
            SqlConnection con = new SqlConnection(connectionString);
            cmd = new SqlCommand("SELECT COUNT(ID) FROM PORTARIA", con);
            con.Open();

            string sql = "SELECT COUNT(*) FROM PRESENCA where MES=@Teste";
            cmd = new SqlCommand(sql, con);
            {
                string jeferson;
                jeferson =dateTimePicker1.Text;
                cmd.Parameters.AddWithValue("@Teste", jeferson);
                label5.Text = cmd.ExecuteScalar().ToString();
            }
            
        }
        private void atualizartodalista()
        {

            cn.Open();
            SqlCommand cmd = cn.CreateCommand();
            cmd.CommandText = "Select * FROM PRESENCA";
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            dataGridView1.DataSource = dt;

            cn.Close();

            SqlConnection con = new SqlConnection(connectionString);
            cmd.CommandType = CommandType.Text;
            dataGridView1.Columns[0].Width = 22;


            novo4 = true;

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
   
        }

        private void tsbExcluir_Click(object sender, EventArgs e)
        {
            string sql = "DELETE FROM PRESENCA WHERE ID=@ID";
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
            txtId.Text = "";
            atualizartodalista();
            dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string sql = "SELECT * FROM PRESENCA WHERE ID=@Id";
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
            tsbCancelar.Enabled = true;
            tsbExcluir.Enabled = true;
        }

        private void tsbCancelar_Click(object sender, EventArgs e)
        {
            tsbCancelar.Enabled = false;
            tsbExcluir.Enabled = false;
            txtId.Text = "";
            txtId.Focus();
            atualizartodalista();
            dataGridView1.FirstDisplayedCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

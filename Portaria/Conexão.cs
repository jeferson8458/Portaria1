using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portaria
{
    class Conexão
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True");
        string connectionString = @"Data Source=DESKTOP-6TOVA5C\SQLEXPRESS;Initial Catalog=BDCADASTRO;Integrated Security=True";
        SqlCommand cmd;
        public string VERIFICAR(string CPF)
        {
            string emp = "";
            string sql = "SELECT ID FROM PORTARIA WHERE CPF=@CPF";
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.Add("@CPF", CPF);
                con.Open();

                var result = cmd.ExecuteScalar();

                if (result != null)
                {

                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            return emp;
        }
       
        }
    }
 

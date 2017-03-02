using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace DAL
{
    public class Contexto : IDisposable
    {
        private readonly SqlConnection MinhaConexao;

        public Contexto()
        {
            MinhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["TesteCliente"].ConnectionString);
            MinhaConexao.Open();
        }

        public void executaComando(string query)
        {
            var cmdComando = new SqlCommand
            {
                CommandText = query,
                CommandType = CommandType.Text,
                Connection = MinhaConexao
            };

            cmdComando.ExecuteNonQuery();
        }

        public SqlDataReader executaComandoComRetorno(string query)
        {
            var cmdComando = new SqlCommand(query, MinhaConexao);
            return cmdComando.ExecuteReader();

        }

        public void Dispose()
        {
            if (MinhaConexao.State == ConnectionState.Open)
                MinhaConexao.Close();
        }

    }
}






    

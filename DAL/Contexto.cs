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
            MinhaConexao = new SqlConnection(ConfigurationManager.ConnectionStrings["TarentoConn"].ConnectionString);
            MinhaConexao.Open();
        }


        public SqlConnection createConexao()
        {
            SqlConnection conexao = new SqlConnection();
            conexao.ConnectionString = ConfigurationManager.ConnectionStrings["TarentoConn"].ConnectionString;
            conexao.Open();

            return conexao;
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

        public SqlCommand createComando(String query, CommandType cmdType, SqlParameter[] parametros)
        {
            SqlCommand comando = new SqlCommand(query);
            comando.CommandType = cmdType;

            //Adiciona parametros
            if (parametros != null) comando.Parameters.AddRange(parametros);

            return comando;
        }

        public void Dispose()
        {
            if (MinhaConexao.State == ConnectionState.Open)
                MinhaConexao.Close();
        }

        public void closeConexao(SqlConnection conexao)
        {
            if (conexao != null)
            {
                conexao.Close();
                conexao.Dispose();
                conexao = null;
            }
        }

        public DataSet executeDataSet(String query, CommandType cmdType, Boolean makelog)
        {
            return this.executeDataSet(query, cmdType, null, null, null, makelog);
        }

        public DataSet executeDataSet(String query, CommandType cmdType, SqlConnection conexao, SqlTransaction transacao, SqlParameter[] parametros, Boolean makelog)
        {
            // Conectar, se necessário
            bool fecharConexao = false;
            if (conexao == null || conexao.State == ConnectionState.Closed)
            {
                conexao = this.createConexao();
                fecharConexao = true;
            }

            SqlDataAdapter da = new SqlDataAdapter();
            DataSet dados = new DataSet();
            SqlCommand comando;

            //preparação do comando
            comando = this.createComando(query, cmdType, parametros);
            comando.Connection = conexao;
            if (transacao != null)
                comando.Transaction = transacao;

            //execução do comando
            da.SelectCommand = comando;
            da.Fill(dados);

            //libera recursos
            if (fecharConexao)
                this.closeConexao(conexao);

            comando.Dispose();
            da.Dispose();

            // TO DO
            //if (makelog) this.fazerLog(query, parametros, conexao, transacao);

            return dados;
        }

        public SqlDataReader executeDataReader(String query, CommandType cmdType, Boolean makelog)
        {
            return this.executeDataReader(query, cmdType, null, null, null, makelog);

        }

        public SqlDataReader executeDataReader(String query, CommandType cmdType, SqlConnection conexao, SqlTransaction transacao, SqlParameter[] parametros, Boolean makelog)
        {
            //conexao da transacao
            if (conexao == null || conexao.State == ConnectionState.Closed)
                conexao = this.createConexao();

            SqlCommand comando = this.createComando(query, cmdType, parametros);
            comando.Connection = conexao;
            if (transacao != null)
                comando.Transaction = transacao;

            SqlDataReader dr = comando.ExecuteReader(CommandBehavior.CloseConnection);

            //libera recursos
            comando.Dispose();
            //this.closeConexao(conexao);

            //TO DO
            //if (makelog) this.fazerLog(query, parametros, conexao, transacao);

            return dr;
        }

        public SqlParameter createParametro(String nome, SqlDbType tipo, Object valor)
        {
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = nome;
            parametro.SqlDbType = tipo;

            if (valor == null)
            {
                parametro.Value = DBNull.Value;
            }
            else
            {
                if (tipo.Equals(SqlDbType.VarChar) && valor.ToString().Length.Equals(0))
                {
                    parametro.Value = DBNull.Value;
                }
                else
                {
                    parametro.Value = valor;
                }
            }

            return parametro;
        }

        /// <summary>
        /// Executa um comando SQL sem retorno de dados.
        /// </summary>
        /// <param name="query">Query a ser executada.</param>
        /// <param name="cmdType">Tipo da query.</param>
        /// <param name="makelog">Faz log da operação?</param>
        public void executeNonQuery(String query, CommandType cmdType, Boolean makelog)
        {
            this.executeNonQuery(query, cmdType, null, null, null, makelog);
        }



        /// <summary>
        /// Executa um comando SQL sem retorno de dados.
        /// </summary>
        /// <param name="query">Query a ser executada.</param>
        /// <param name="cmdType">Tipo da query.</param>
        /// <param name="parametros">Parâmetros para a query.</param>
        /// <param name="makelog">Faz log da operação?</param>
        public void executeNonQuery(String query, CommandType cmdType, SqlConnection conexao, SqlTransaction transacao, SqlParameter[] parametros, Boolean makelog)
        {
            // Conectar, se necessário
            bool fecharConexao = false;
            if (conexao == null || conexao.State == ConnectionState.Closed)
            {
                conexao = this.createConexao();
                fecharConexao = true;
            }

            SqlCommand comando = this.createComando(query, cmdType, parametros);
            comando.Connection = conexao;
            if (transacao != null)
                comando.Transaction = transacao;
            comando.ExecuteNonQuery();

            //libera recursos
            comando.Dispose();

            if (fecharConexao)
                this.closeConexao(conexao);

            //TO DO
            //if (makelog) this.fazerLog(query, parametros, conexao, transacao);

        }

    }
}






    

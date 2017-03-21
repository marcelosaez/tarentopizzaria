using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Funcionario;
using System.Data.SqlClient;

namespace DAL.Funcionario
{
    public class FuncionarioDAL : Contexto
    {
        public DataSet obterDataSet()
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT f.idtb_funcionario as idFuncionario, nome, login, email, ativo, tipo  ");
            query.Append(" FROM tb_funcionarios f  INNER JOIN tb_tipoFuncionario tf    ");
            query.Append(" ON f.tb_tipoFuncionario_idtb_tipoFuncionario = tf.idtb_tipoFuncionario  ");
            query.Append(" ORDER BY nome");
            return  executeDataSet(query.ToString(), CommandType.Text, false);
        }

        public List<TipoFuncionario_VO> obterTipos()
        {
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT  [idtb_tipoFuncionario] as id, [tipo] FROM tb_tipoFuncionario ");
            query.Append(" ORDER BY tipo ");

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            List<TipoFuncionario_VO> retorno = new List<TipoFuncionario_VO>();
            while (dr.Read())
            {
                TipoFuncionario_VO registro = new TipoFuncionario_VO();
                registro.tipoID = Convert.ToInt32(dr["id"]);
                registro.tipo = Convert.ToString(dr["tipo"]);
                retorno.Add(registro);
            }

            //libera recursos
            dr.Dispose();
            dr.Close();

            return retorno;
        }

       

        public bool checaLogin(string login)
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT  login FROM tb_Funcionarios ");
            query.Append(" WHERE login = '" + login +"'");

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            //List<TipoFuncionario_VO> retorno = new List<TipoFuncionario_VO>();
            while (dr.Read())
            {
                Funcionario_VO registro = new Funcionario_VO();
                registro.login = Convert.ToString(dr["login"]);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Salvar.
        /// </summary>
        /// <param name="parceiro"></param>
        public void salvar(Funcionario_VO funcionario)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO TB_FUNCIONARIOS (tb_tipoFuncionario_idtb_tipoFuncionario, nome, login, senha, email, ativo) ");
            query.Append("VALUES (@idTipo, @Nome, @Login, @Senha, @Email, @Ativo) ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@idTipo", SqlDbType.Int, funcionario.tipoID),
                createParametro("@Nome", SqlDbType.VarChar, funcionario.nome),
                createParametro("@Login", SqlDbType.VarChar, funcionario.login),
                createParametro("@Senha", SqlDbType.VarChar, funcionario.senha),
                createParametro("@Email", SqlDbType.VarChar, funcionario.email),
                createParametro("@Ativo", SqlDbType.Bit, funcionario.ativo)
            };

            //executa
            executeNonQuery( query.ToString(), CommandType.Text, null, null, parametros, false);
        }

        /// <summary>
        /// Alterar.
        /// </summary>
        /// <param name="parceiro"></param>
        public void alterar(Funcionario_VO funcionario)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE TB_FUNCIONARIOS SET Nome = @Nome,  login = @login, senha =@senha, ");
            query.Append(" email = @email, ativo=@ativo, tb_tipoFuncionario_idtb_tipoFuncionario=@idTipo  ");

           
            query.Append("WHERE idtb_funcionario = @idFuncionario ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@idTipo", SqlDbType.Int, funcionario.tipoID),
                createParametro("@Nome", SqlDbType.VarChar, funcionario.nome),
                createParametro("@Login", SqlDbType.VarChar, funcionario.login),
                createParametro("@Senha", SqlDbType.VarChar, funcionario.senha),
                createParametro("@Email", SqlDbType.VarChar, funcionario.email),
                createParametro("@Ativo", SqlDbType.Bit, funcionario.ativo),
                createParametro("@idFuncionario", SqlDbType.Int, funcionario.id)
            };

            //executa
            executeNonQuery(query.ToString(), CommandType.Text,null,null, parametros, false);
        }

        public Funcionario_VO obterRegistro(int codigo)
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT f.idtb_funcionario as idFuncionario, nome, login, email, ativo, senha, tipo,tf.idtb_tipoFuncionario  ");
            query.Append(" FROM tb_funcionarios f  INNER JOIN tb_tipoFuncionario tf    ");
            query.Append(" ON f.tb_tipoFuncionario_idtb_tipoFuncionario = tf.idtb_tipoFuncionario  ");
            query.Append(" WHERE idtb_funcionario = " + codigo);

            Funcionario_VO registro = null;

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro = new Funcionario_VO();
                registro.id = Convert.ToInt32(dr["idFuncionario"]);
                registro.nome = Convert.ToString(dr["nome"]);
                registro.login = Convert.ToString(dr["login"]);
                registro.senha = Convert.ToString(dr["senha"]);
                registro.email = Convert.ToString(dr["email"]);
                registro.ativo = Convert.ToBoolean(dr["ativo"]);
                registro.tipoID = Convert.ToInt32(dr["idtb_tipoFuncionario"]);
            }

            return registro;
        }

    }
}

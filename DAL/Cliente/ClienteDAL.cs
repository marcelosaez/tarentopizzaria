using MODEL.Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Cliente
{
    public class ClienteDAL : Contexto
    {
        public DataSet obterDataSet()
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT  [idtb_cliente] as id, [nome], [endereco] ,[numero],[email] ,[ativo], ");
            query.Append(" CASE WHEN [dddcel] <> 0 THEN [dddcel] END as dddcel, ");
            query.Append(" CASE WHEN[cel] <> 0 THEN[cel] END as cel, ");
            query.Append(" CASE WHEN[dddres] <> 0 THEN[dddres] END as dddres, ");
            query.Append(" CASE WHEN[telres] <> 0 THEN[telres] END as telres  ");
            query.Append(" FROM tb_cliente ");
            query.Append(" ORDER BY nome");
            return executeDataSet(query.ToString(), CommandType.Text, false);
        }

        public void alterar(Cliente_VO cliente)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE TB_CLIENTE SET Nome = @Nome,  endereco = @Endereco, numero =@Numero, ");
            query.Append(" email = @Email, ativo=@Ativo, DDDres = @DDDres, TelRes= @TelRes, DDDcel = @DDDcel, cel= @cel ");
            query.Append("WHERE idtb_cliente = @idCliente ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@Nome", SqlDbType.VarChar, cliente.nome),
                createParametro("@Endereco", SqlDbType.VarChar, cliente.endereco),
                createParametro("@Numero", SqlDbType.Int, cliente.numero),
                createParametro("@Email", SqlDbType.VarChar, cliente.email),
                createParametro("@Ativo", SqlDbType.Bit, cliente.ativo),
                createParametro("@DDDres", SqlDbType.Int, cliente.dddres),
                createParametro("@TelRes", SqlDbType.Int, cliente.telres),
                createParametro("@DDDcel", SqlDbType.Int, cliente.dddcel),
                createParametro("@cel", SqlDbType.Int, cliente.cel),
                createParametro("@idCliente", SqlDbType.Int, cliente.idCliente)



            };

            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);

        }

        public Cliente_VO checaCadastro(int telres, int cel)
        {
            Cliente_VO registro = new Cliente_VO();
            registro.temCadastro = false;


            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT [idtb_cliente] as idCliente, [nome] ");
            query.Append(" FROM tb_cliente ");
            query.Append(" WHERE 1=1 ");
            if (telres != 0)
            {
                if (cel != 0)
                    query.Append(" AND ( telres = " + telres + " OR cel = " + cel + " ) ");
                else
                    query.Append(" AND telres = " + telres);
            }
            else
            {
                if (cel != 0)
                    query.Append(" AND cel = " + cel);
            }
               

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro.idCliente = Convert.ToInt32(dr["idCliente"]);
                registro.nome = Convert.ToString(dr["nome"]);
                registro.temCadastro = true;


            }

            return registro;
        }

        /// <summary>
        /// Salvar.
        /// </summary>
        /// <param name="parceiro"></param>
        public void salvar(Cliente_VO cliente)
        {
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO TB_CLIENTE ( nome, endereco, numero, email, ativo,dddres, telres, dddcel,cel) ");
            query.Append("VALUES ( @Nome, @Endereco, @Numero, @Email, @Ativo,@DDDres, @TelRes, @DDDcel, @cel ) ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@Nome", SqlDbType.VarChar, cliente.nome),
                createParametro("@Endereco", SqlDbType.VarChar, cliente.endereco),
                createParametro("@Numero", SqlDbType.Int, cliente.numero),
                createParametro("@Email", SqlDbType.VarChar, cliente.email),
                createParametro("@Ativo", SqlDbType.Bit, cliente.ativo),
                createParametro("@DDDres", SqlDbType.Int, cliente.dddres),
                createParametro("@TelRes", SqlDbType.Int, cliente.telres),
                createParametro("@DDDcel", SqlDbType.Int, cliente.dddcel),
                createParametro("@cel", SqlDbType.Int, cliente.cel)


            };

            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);
        }

        public Cliente_VO obterRegistro(int codigo)
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT  [idtb_cliente] as idCliente, [nome], [endereco] ,[numero],[email] ,[ativo], ");
            query.Append(" CASE WHEN [dddcel] <> 0 THEN [dddcel] ELSE 0 END as dddcel, ");
            query.Append(" CASE WHEN[cel] <> 0 THEN[cel] ELSE 0 END as cel, ");
            query.Append(" CASE WHEN[dddres] <> 0 THEN[dddres] ELSE 0 END as dddres, ");
            query.Append(" CASE WHEN[telres] <> 0 THEN[telres] ELSE 0 END as telres  ");
            query.Append(" FROM tb_cliente ");
            query.Append(" WHERE idtb_cliente = " + codigo);

            Cliente_VO registro = null;

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro = new Cliente_VO();
                registro.idCliente = Convert.ToInt32(dr["idCliente"]);
                registro.nome = Convert.ToString(dr["nome"]);
                registro.endereco = Convert.ToString(dr["endereco"]);
                registro.numero = Convert.ToInt32(dr["numero"]);
                registro.email = Convert.ToString(dr["email"]);
                registro.ativo = Convert.ToBoolean(dr["ativo"]);
                registro.dddcel = Convert.ToInt32(dr["dddcel"]);
                registro.cel = Convert.ToInt32(dr["cel"]);
                registro.dddres = Convert.ToInt32(dr["dddres"]);
                registro.telres = Convert.ToInt32(dr["telres"]);


            }

            return registro;
        }

        public List<Cliente_VO> autoComplete(string busca)
        {
            List<Cliente_VO> listaClientes = new List<Cliente_VO>();
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT  [idtb_cliente] as id, [nome], [endereco] ,[numero],[email] ,[ativo], ");
            query.Append(" CASE WHEN [dddcel] <> 0 THEN [dddcel] END as dddcel, ");
            query.Append(" CASE WHEN[cel] <> 0 THEN[cel] END as cel, ");
            query.Append(" CASE WHEN[dddres] <> 0 THEN[dddres] END as dddres, ");
            query.Append(" CASE WHEN[telres] <> 0 THEN[telres] END as telres  ");
            query.Append(" FROM tb_cliente ");
            query.Append(" WHERE ((cel like '" + busca + "%')  ");
            query.Append(" or(telres like '" + busca + "%'))   ");
            query.Append(" and cel is not null ");
            query.Append(" and telres is not null ");
            query.Append(" ORDER BY nome");
            Cliente_VO registro = null;

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro = new Cliente_VO();
                registro.idCliente = Convert.ToInt32(dr["id"]);
                registro.nome = Convert.ToString(dr["nome"]);
                registro.endereco = Convert.ToString(dr["endereco"]);
                registro.numero = Convert.ToInt32(dr["numero"]);
                registro.email = Convert.ToString(dr["email"]);
                registro.ativo = Convert.ToBoolean(dr["ativo"]);
                if (!DBNull.Value.Equals(dr["dddcel"]))
                {
                    registro.dddcel = Convert.ToInt32(dr["dddcel"]);
                    registro.cel = Convert.ToInt32(dr["cel"]);
                }

                if (!DBNull.Value.Equals(dr["dddres"]))
                {
                    registro.dddres = Convert.ToInt32(dr["dddres"]);
                    registro.telres = Convert.ToInt32(dr["telres"]);
                }

                    
                listaClientes.Add(registro);


            }

            return listaClientes;
        }


    }
}

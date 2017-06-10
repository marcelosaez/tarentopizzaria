using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Produto
{
    public class ProdutoTipoDAL : Contexto
    {
        public DataSet obterDataSet()
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT idtb_tipo as id, tipo, ativo ");
            query.Append(" FROM tb_tipoProduto ");
            query.Append(" ORDER BY tipo");
            return executeDataSet(query.ToString(), CommandType.Text, false);
        }

        public TipoProduto_VO obterRegistro(int codigo)
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT  idtb_tipo as id, tipo, ativo ");
            query.Append(" FROM tb_tipoProduto ");
            query.Append(" WHERE idtb_tipo = " + codigo);

            TipoProduto_VO registro = null;

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro = new TipoProduto_VO();
                registro.idTipoProduto = Convert.ToInt32(dr["id"]);
                registro.tipo = Convert.ToString(dr["tipo"]);
                registro.ativo = Convert.ToBoolean(dr["ativo"]);

            }

            return registro;
        }

        public List<TipoProduto_VO> obterTipos()
        {
            List<TipoProduto_VO> lstTipos = new List<TipoProduto_VO> ();
           
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT idtb_tipo as id , tipo ");
            query.Append(" FROM tb_tipoProduto ");
            query.Append(" WHERE ativo  = 1");

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);
            TipoProduto_VO registro = null;

            while (dr.Read())
            {
                registro = new TipoProduto_VO();

                registro.idTipoProduto = Convert.ToInt32(dr["id"]);
                registro.tipo = Convert.ToString(dr["tipo"]);
                lstTipos.Add(registro);
            }

            return lstTipos;
        }

        public bool verificaTipoProduto(string tipo)
        {
            TipoProduto_VO registro = new TipoProduto_VO();
            registro.temCadastro = false;

            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT idtb_tipo as id ");
            query.Append(" FROM tb_tipoProduto ");
            query.Append(" WHERE tipo = '" +tipo+ "'" );

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro.idTipoProduto = Convert.ToInt32(dr["id"]);
                registro.temCadastro = true;
            }

            return registro.temCadastro;
        }

        /// <summary>
        /// Salvar.
        /// </summary>
        /// <param name="parceiro"></param>
        public void salvar(TipoProduto_VO tipoProduto)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" INSERT INTO TB_TIPOPRODUTO ( tipo, ativo ) ");
            query.Append(" VALUES ( @Tipo, @Ativo) ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@Tipo", SqlDbType.VarChar, tipoProduto.tipo),
                createParametro("@Ativo", SqlDbType.VarChar, tipoProduto.ativo)
            };

            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);
        }

        public void alterar(TipoProduto_VO tipoProduto)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE TB_TIPOPRODUTO SET Tipo = @Tipo, ativo=@Ativo ");
            query.Append("WHERE idtb_tipo = @idTipoProduto ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@Tipo", SqlDbType.VarChar, tipoProduto.tipo),
                createParametro("@Ativo", SqlDbType.Bit, tipoProduto.ativo),
                createParametro("@idTipoProduto", SqlDbType.Int, tipoProduto.idTipoProduto)
            };

            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);

        }
    }
}

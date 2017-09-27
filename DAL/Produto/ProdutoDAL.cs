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
    public class ProdutoDAL : Contexto
    {
        public DataSet obterDataSet()
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" select [idtb_produtos] as id, [nome],[ingredientes], ");
            query.Append(" [valor], p.[ativo],[tipo] ");
            query.Append(" from tb_produtos (nolock) p ");
            query.Append(" inner join tb_tipoProduto(nolock) tp ");
            query.Append(" on p.tb_tipo_idtb_tipo = tp.idtb_tipo ");
            query.Append(" ORDER BY nome");
            return executeDataSet(query.ToString(), CommandType.Text, false);
        }

        public DataSet obterDataSetOpcionais()
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" select [idtb_opcionais] as id, [nome],  ");
            query.Append(" [valor], p.[ativo]  ");
            query.Append(" from tb_opcionais (nolock) p ");
            query.Append(" ORDER BY nome");
            return executeDataSet(query.ToString(), CommandType.Text, false);
        }

        public Opcional_VO obterRegistroOpcional(int codigo)
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" select [idtb_opcionais] as id, [nome] ");
            query.Append(" ,[valor], [ativo] FROM[dbo].[tb_opcionais] ");
            query.Append(" WHERE idtb_opcionais = " + codigo);

            Opcional_VO registro = null;

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro = new Opcional_VO();
                registro.idOpcional = Convert.ToInt32(dr["id"]);
                registro.nome = Convert.ToString(dr["nome"]);
                registro.valor = Convert.ToDecimal(dr["valor"]);
                registro.ativo = Convert.ToBoolean(dr["ativo"]);
            }

            return registro;
        }

        public void salvar(Produto_VO Produto)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" INSERT INTO TB_PRODUTOS ( [tb_tipo_idtb_tipo] ,[nome] ,[ingredientes] ,[valor] ,[ativo]) ");
            query.Append(" VALUES ( @Tipo, @Nome, @Ingredientes, @Valor, @Ativo) ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@Tipo", SqlDbType.VarChar, Produto.idTipoProduto),
                createParametro("@Nome", SqlDbType.VarChar, Produto.nome),
                createParametro("@Ingredientes", SqlDbType.VarChar, Produto.ingredientes),
                createParametro("@Valor", SqlDbType.Decimal, Produto.valor),
                createParametro("@Ativo", SqlDbType.Bit, Produto.ativo)
            };

            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);
        }

        public List<BordaProduto_VO> obterBordas()
        {
            List<BordaProduto_VO> lstBordas = new List<BordaProduto_VO>();
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT [idtb_borda] , [borda] , [valor], [ativo] ");
            query.Append(" FROM[dbo].[tb_BordaProduto] p ");
            query.Append(" WHERE ativo =1 order by borda ");

            BordaProduto_VO registro = null;

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro = new BordaProduto_VO();
                registro.idBordaProduto = Convert.ToInt32(dr["idtb_borda"]);
                registro.borda = Convert.ToString(dr["borda"]);
                registro.valor = Convert.ToDecimal(dr["valor"]);
                registro.ativo = Convert.ToBoolean(dr["ativo"]);
                lstBordas.Add(registro);
            }

            return lstBordas;
        }

        public bool verificaOpcional(string opcional)
        {
            Opcional_VO registro = new Opcional_VO();
            registro.temCadastro = false;

            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT idtb_opcionais as id ");
            query.Append(" FROM tb_opcionais ");
            query.Append(" WHERE nome = '" + opcional + "'");

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro.idOpcional = Convert.ToInt32(dr["id"]);
                registro.temCadastro = true;
            }

            return registro.temCadastro;
        }

        public void alterarOpcional(Opcional_VO opcional)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE TB_OPCIONAIS SET nome=@Nome, valor =@valor, ativo=@ativo ");
            query.Append("WHERE idtb_opcionais = @idOpcional ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@Ativo", SqlDbType.Bit, opcional.ativo),
                createParametro("@idOpcional", SqlDbType.Int, opcional.idOpcional),
                createParametro("@Nome", SqlDbType.VarChar, opcional.nome),
                createParametro("@Valor", SqlDbType.Decimal, opcional.valor)
            };

            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);
        }

        public void salvarOpcional(Opcional_VO opcional)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" INSERT INTO TB_OPCIONAIS ( [nome] ,[valor] ,[ativo]) ");
            query.Append(" VALUES (@Nome, @Valor, @Ativo) ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@Nome", SqlDbType.VarChar, opcional.nome),
                createParametro("@Valor", SqlDbType.Decimal, opcional.valor),
                createParametro("@Ativo", SqlDbType.Bit, opcional.ativo)
            };

            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);
        }

        public decimal obterValorBorda(int idBorda)

        {
            decimal retorno = 0;
           
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT max([valor]) as valor ");
            query.Append(" FROM[dbo].[tb_BordaProduto]  ");
            query.Append(" WHERE idtb_borda in (" + idBorda + ")");

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                retorno = Convert.ToDecimal(dr["valor"]);
            }

            return retorno;
        }



        public void alterar(Produto_VO Produto)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE TB_PRODUTOS SET tb_tipo_idtb_tipo = @idTipoProduto, nome=@Nome, ingredientes=@ingredientes, valor =@valor, ativo=@ativo ");
            query.Append("WHERE idtb_produtos = @idProduto ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@idTipoProduto", SqlDbType.Int, Produto.idTipoProduto),
                createParametro("@Ativo", SqlDbType.Bit, Produto.ativo),
                createParametro("@idProduto", SqlDbType.Int, Produto.idProduto),
                createParametro("@Nome", SqlDbType.VarChar, Produto.nome),
                createParametro("@Ingredientes", SqlDbType.VarChar, Produto.ingredientes),
                createParametro("@Valor", SqlDbType.Decimal, Produto.valor)
            };

            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);

        }

        public Produto_VO obterRegistro(int codigo)
        {
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" select [idtb_produtos] ,[tb_tipo_idtb_tipo], [nome] ");
            query.Append(" ,[ingredientes],[valor], p.[ativo], tp.tipo FROM[dbo].[tb_produtos] p ");
            query.Append(" inner join tb_tipoProduto tp on p.idtb_produtos = tp.idtb_tipo ");
            query.Append(" WHERE p.idtb_produtos = " + codigo);

            Produto_VO registro = null;

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro = new Produto_VO();
                registro.idProduto = Convert.ToInt32(dr["idtb_produtos"]);
                registro.idTipoProduto = Convert.ToInt32(dr["tb_tipo_idtb_tipo"]);
                registro.nome = Convert.ToString(dr["nome"]);
                registro.ingredientes = Convert.ToString(dr["ingredientes"]);
                registro.valor = Convert.ToDecimal(dr["valor"]);
                registro.tipo = Convert.ToString(dr["tipo"]);
                registro.ativo = Convert.ToBoolean(dr["ativo"]);
            }

            return registro;
        }

        public bool verificaProduto(string produto, int idTipo)
        {
            Produto_VO registro = new Produto_VO();
            registro.temCadastro = false;

            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT idtb_produtos as id ");
            query.Append(" FROM tb_Produtos ");
            query.Append(" WHERE nome = '" + produto + "' and tb_tipo_idtb_tipo=" + idTipo);

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro.idProduto = Convert.ToInt32(dr["id"]);
                registro.temCadastro = true;
            }

            return registro.temCadastro;
        }

        public List<Produto_VO> obterSabores(int idTipo)
        {

            List<Produto_VO> lstProdutos = new List<Produto_VO>();
            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT [idtb_produtos] , [nome] , [valor] ");
            query.Append(" FROM[dbo].[tb_produtos] p ");
            query.Append(" WHERE ativo =1 and tb_tipo_idtb_tipo = " + idTipo);
            query.Append(" ORDER BY [nome] ");
            Produto_VO registro = null;

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                registro = new Produto_VO();
                registro.idProduto = Convert.ToInt32(dr["idtb_produtos"]);
                registro.nome = Convert.ToString(dr["nome"]);
                registro.valor = Convert.ToDecimal(dr["valor"]);
                lstProdutos.Add(registro);
            }

            return lstProdutos;
        }

        public decimal obterValor(string idSabor)
        {
            decimal retorno = 0;
            List<Produto_VO> lstProdutos = new List<Produto_VO>();
            /* string sql = "";
             sql += idSabor1;
             if (idSabor2 != 0)
                 sql += ","+idSabor2;
             if (idSabor3 != 0)
                 sql += "," + idSabor3;*/

            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT max([valor]) as valor ");
            query.Append(" FROM[dbo].[tb_produtos] p ");
            query.Append(" WHERE idtb_produtos in (" + idSabor + ")");


            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                retorno = Convert.ToDecimal(dr["valor"]);
            }

            return retorno;
        }

    }
}

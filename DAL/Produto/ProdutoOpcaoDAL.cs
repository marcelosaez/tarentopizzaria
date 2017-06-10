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
    public class ProdutoOpcaoDAL : Contexto
    {
        public List<OpcaoProduto_VO> obterTipos()
        {
            List<OpcaoProduto_VO> lstTipos = new List<OpcaoProduto_VO>();

            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT idOpcao, opcao ");
            query.Append(" FROM tb_opcao ");
            query.Append(" ORDER BY opcao");

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);
            OpcaoProduto_VO registro = null;

            while (dr.Read())
            {
                registro = new OpcaoProduto_VO();
                registro.idOpcao = Convert.ToInt32(dr["idOpcao"]);
                registro.Opcao = Convert.ToString(dr["opcao"]);
                lstTipos.Add(registro);
            }

            return lstTipos;
        }

    }
}

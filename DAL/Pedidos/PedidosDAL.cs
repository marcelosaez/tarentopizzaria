using MODEL.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Cliente;
using System.Data;
using MODEL.Pedido;
using System.Data.SqlClient;

namespace DAL.Pedidos
{
    public class PedidosDAL : Contexto
    {
        public DataSet obterDataSet()
        {
            List<Pedido_VO> lst = new List<Pedido_VO>();
            

            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT idDetalhaPedido as idDet, ctp.idPedido, tp.tipo, ");
            query.Append(" ISNULL(op.Opcao, '') as Opcao, ");
            query.Append(" CASE ");
            query.Append("  WHEN op.idOpcao = 2 THEN pr.nome + ' | ' + pr2.nome ");
            query.Append("  WHEN op.idOpcao = 3 THEN pr.nome + ' | ' + pr2.nome + ' | ' + pr3.nome ");
            query.Append("  ELSE pr.nome ");
            query.Append("  END as sabor, ");
            query.Append(" dt.quantidade, dt.valor, st.statusPedido ");
            query.Append(" FROM[dbo].[tb_cliente_tem_pedido] ctp ");
            query.Append(" INNER JOIN tb_detalhaPedido dt on dt.idPedido = ctp.idPedido ");
            query.Append(" LEFT JOIN tb_opcao op on dt.idOpcao = op.idOpcao ");
            query.Append(" INNER JOIN tb_statusPedido st on ctp.tb_status_idtb_statusPedido = st.idStatus ");
            query.Append(" INNER JOIN tb_produtos pr on dt.idSabor1_idtb_produtos = pr.idtb_produtos ");
            query.Append(" LEFT JOIN tb_produtos pr2 on dt.idSabor2_idtb_produtos = pr2.idtb_produtos ");
            query.Append(" LEFT JOIN tb_produtos pr3 on dt.idSabor3_idtb_produtos = pr3.idtb_produtos ");
            query.Append(" INNER JOIN tb_tipoProduto tp on tp.idtb_tipo = pr.tb_tipo_idtb_tipo  ");
            query.Append(" WHERE dt.idPedido = 0");

            return executeDataSet(query.ToString(), CommandType.Text, false);
        }

        public List<Pedido_VO> obterDadosPedidos(int idPedido)
        {
            List<Pedido_VO> lst = new List<Pedido_VO>();


            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT idDetalhaPedido as idDet, ctp.idPedido, tp.tipo, ");
            query.Append(" ISNULL(op.Opcao, '') as Opcao, ");
            query.Append(" CASE ");
            query.Append("  WHEN op.idOpcao = 2 THEN pr.nome + ' | ' + pr2.nome ");
            query.Append("  WHEN op.idOpcao = 3 THEN pr.nome + ' | ' + pr2.nome + ' | ' + pr3.nome ");
            query.Append("  ELSE pr.nome ");
            query.Append("  END as sabor, ");
            query.Append(" dt.quantidade, dt.valor, st.statusPedido ");
            query.Append(" FROM[dbo].[tb_cliente_tem_pedido] ctp ");
            query.Append(" INNER JOIN tb_detalhaPedido dt on dt.idPedido = ctp.idPedido ");
            query.Append(" LEFT JOIN tb_opcao op on dt.idOpcao = op.idOpcao ");
            query.Append(" INNER JOIN tb_statusPedido st on ctp.tb_status_idtb_statusPedido = st.idStatus ");
            query.Append(" INNER JOIN tb_produtos pr on dt.idSabor1_idtb_produtos = pr.idtb_produtos ");
            query.Append(" LEFT JOIN tb_produtos pr2 on dt.idSabor2_idtb_produtos = pr2.idtb_produtos ");
            query.Append(" LEFT JOIN tb_produtos pr3 on dt.idSabor3_idtb_produtos = pr3.idtb_produtos ");
            query.Append(" INNER JOIN tb_tipoProduto tp on tp.idtb_tipo = pr.tb_tipo_idtb_tipo  ");
            query.Append(" WHERE dt.idPedido = " + idPedido);

            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                Pedido_VO pedido = new Pedido_VO();
                pedido.idDetPed = Convert.ToInt32(dr["idDet"]);
                pedido.idPedido = Convert.ToInt32(dr["idPedido"]);
                pedido.tipo = Convert.ToString(dr["tipo"]);
                pedido.opcao = Convert.ToString(dr["opcao"]);
                pedido.sabor = Convert.ToString(dr["sabor"]);
                pedido.qtd = Convert.ToInt32(dr["quantidade"]);
                pedido.valor = Convert.ToDecimal(dr["valor"]);
                pedido.StatusPedido = Convert.ToString(dr["statusPedido"]);
                lst.Add(pedido);
            }

            return lst;
            //return executeDataSet(query.ToString(), CommandType.Text, false);
        }


        public Pedido_VO adicionaPedidosInicial(Pedido_VO Pedido)
        {

            StringBuilder query = new StringBuilder();
            query.Append(" INSERT INTO tb_cliente_tem_pedido ([tb_cliente_idtb_cliente] ,[tb_funcionario_idtb_funcionario] ,[tb_status_idtb_statusPedido]) ");
            query.Append(" VALUES ( @IdCliente, @idFuncionario, @IdStatus);");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@IdCliente", SqlDbType.Int, Pedido.idCliente),
                createParametro("@idFuncionario", SqlDbType.Int, Pedido.idFuncionario),
                createParametro("@IdStatus", SqlDbType.Int, 1) // Aberto

            };

            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);

            //SqlParameter IDParameter = new SqlParameter("@ID", SqlDbType.Int);
            //IDParameter.Direction = ParameterDirection.Output;
            //cmd.Parameters.Add(IDParameter);
            int id = 0;
            StringBuilder queryID = new StringBuilder();
            queryID.Append("select top 1 idPedido as ID from tb_cliente_tem_pedido order by idPedido desc");

            SqlDataReader dr = executeDataReader(queryID.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                id = Convert.ToInt32(dr["id"]);
            }

  
            Pedido.idPedido = id;
            Pedido =  adicionaPedidos(Pedido);
            return Pedido;
        }

        public Pedido_VO adicionaPedidos(Pedido_VO Pedido)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" INSERT INTO tb_detalhaPedido ([idPedido],[idOpcao],[idSabor1_idtb_produtos],[idSabor2_idtb_produtos],[idSabor3_idtb_produtos],[quantidade],[valor]) ");
            query.Append(" VALUES ( @IdPedido, @idOpcao, @IdSabor1, @IdSabor2, @IdSabor3, @Qtd, @Valor) ");

            //Pedido.idOpcao = Pedido.idOpcao == 0 ? null : Pedido.idOpcao;

            //parametros
            SqlParameter[] parametros = {
                createParametro("@IdPedido", SqlDbType.Int, Pedido.idPedido),
                createParametro("@idOpcao", SqlDbType.Int, Pedido.idOpcao),
                createParametro("@IdSabor1", SqlDbType.Int, Pedido.idSabor1),
                createParametro("@IdSabor2", SqlDbType.Int, Pedido.idSabor2),
                createParametro("@IdSabor3", SqlDbType.Int, Pedido.idSabor3),
                createParametro("@Qtd", SqlDbType.Int, Pedido.qtd),
                createParametro("@Valor", SqlDbType.Decimal, Pedido.valor)
            };

           
            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);
            return Pedido;
        }
    }
}

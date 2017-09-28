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
using MODEL.Pagamento;
using MODEL.Produto;

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
            query.Append(" dt.quantidade, dt.valor, st.statusPedido, dt.obs ");
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

        public DataSet obterDataSet(DateTime dataIni, DateTime dataFim)
        {
            List<Pedido_VO> lst = new List<Pedido_VO>();

            //Verifico se a hora esta entre a 00:00 e as 04:00
            if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour <= 4)
                //Caso sim, retiro um dia
                dataIni = dataIni.AddDays(-1);

            dataIni = new DateTime(dataIni.Year, dataIni.Month, dataIni.Day, 05, 00, 00);

            //Se esta no mesmo dia, seto a data dos pedidos para as 23:59 do mesmo dia
            if (dataIni.Day == dataFim.Day)
                dataFim = new DateTime(dataFim.Year, dataFim.Month, dataFim.Day, 23, 59, 59);
            else
                // senao, seto para as 4:59 da manha como horario limite
                dataFim = new DateTime(dataFim.Year, dataFim.Month, dataFim.Day, 4, 59, 59);

            // query
            StringBuilder query = new StringBuilder();
            query.Append(" set dateformat dmy;");
            query.Append(" SELECT ctp.idPedido, c.nome,st.statusPedido,dtPedido,f.nome as atendente, tipoEntrega as [Tipo Pedido]  ");
            query.Append(" FROM[dbo].[tb_cliente_tem_pedido] ctp ");
            query.Append(" INNER JOIN tb_statusPedido st on ctp.tb_status_idtb_statusPedido = st.idStatus  ");
            query.Append(" INNER JOIN tb_cliente c on ctp.tb_cliente_idtb_cliente = c.idtb_cliente ");
            query.Append(" INNER JOIN tb_funcionarios f on ctp.tb_funcionario_idtb_funcionario = f.idtb_funcionario ");
            query.Append(" INNER JOIN tb_entrega e on ctp.idtb_entrega = e.idtb_entrega ");
            query.Append(" WHERE dtPedido between '" + dataIni + "' and '" + dataFim + "'  ");
            query.Append(" ORDER BY ctp.idPedido desc   ");

            return executeDataSet(query.ToString(), CommandType.Text, false);
        }


        public Cupom_VO obterDadosCupomFiscal(int idPedido)
        {
            Cupom_VO cupom = new Cupom_VO();

            // query
            StringBuilder query = new StringBuilder();
            query.Append(" select c.nome as cliente, c.endereco, c.numero,ISNULL(c.dddres,0) as dddres,ISNULL(c.telres,0) as telres, ");
            query.Append(" ISNULL(c.dddcel, 0) as dddcel, ISNULL(c.cel, 0) as cel, ");
            query.Append(" f.nome as funcionario, ");
            query.Append(" tp.TipoPagamento, ");
            query.Append(" te.TipoEntrega ");
            query.Append(" from tb_cliente c  ");
            query.Append(" inner join tb_cliente_tem_pedido ctp on c.idtb_cliente = ctp.tb_cliente_idtb_cliente  ");
            query.Append(" inner join tb_funcionarios f on f.idtb_funcionario = ctp.tb_funcionario_idtb_funcionario  ");
            query.Append(" inner join tb_tipoPagamento tp on tp.idTipoPagamento = ctp.tb_tipoPagamento  ");
            query.Append(" inner join tb_entrega te on te.idtb_Entrega = ctp.idtb_Entrega  ");
            query.Append(" where ctp.idPedido = " + idPedido);

            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                cupom.cliente.nome = Convert.ToString(dr["cliente"]);
                cupom.cliente.endereco = Convert.ToString(dr["endereco"]);
                cupom.cliente.numero = Convert.ToInt32(dr["numero"]);
                cupom.cliente.dddres = Convert.ToInt32(dr["dddres"]);
                cupom.cliente.telres = Convert.ToInt32(dr["telres"]);
                cupom.cliente.dddcel = Convert.ToInt32(dr["dddcel"]);
                cupom.cliente.cel = Convert.ToInt32(dr["cel"]);
                cupom.funcionario.nome = Convert.ToString(dr["funcionario"]);
                cupom.pagamento.TipoPagamento = Convert.ToString(dr["TipoPagamento"]);
                cupom.entrega = Convert.ToString(dr["TipoEntrega"]);
            }

            dr.Close();
            return cupom;
        }

        public void atualizaPagamento(Pagamento_VO pagamento)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE [tb_cliente_tem_pedido] SET [tb_TipoPagamento]=@idPagamento, tb_status_idtb_statusPedido=@statusPedido, idtb_entrega=@idEntrega");
            query.Append(" WHERE idPedido = @idPedido ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@idPagamento", SqlDbType.Int, pagamento.idTipoPagamento),
                createParametro("@idPedido", SqlDbType.Int, pagamento.idPedido),
                createParametro("@statusPedido", SqlDbType.Int, pagamento.idStatusPedido),
                createParametro("@idEntrega", SqlDbType.Int, pagamento.idEntrega)
            };

            //executa
            try
            {
                executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);
            }
            catch (Exception e)
            {

            }
        }

        public List<Pagamento_VO> obterPagamento()
        {
            List<Pagamento_VO> lst = new List<Pagamento_VO>();

            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT idtipoPagamento, tipoPagamento ");
            query.Append(" FROM[dbo].[tb_tipoPagamento] order by tipoPagamento ");

            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                Pagamento_VO pagamento = new Pagamento_VO();
                pagamento.idTipoPagamento = Convert.ToInt32(dr["idtipoPagamento"]);
                pagamento.TipoPagamento = Convert.ToString(dr["tipoPagamento"]);
                lst.Add(pagamento);
            }

            dr.Close();
            return lst;
        }

        public bool apagarDadosDetPedido(int idDetPed)
        {
            bool retorno = false;
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append(" DELETE FROM tb_detalhaPedido WHERE idDetalhaPedido= @idDetPed");

                //parametros
                SqlParameter[] parametros = {
                    createParametro("@idDetPed", SqlDbType.Int, idDetPed)
                };

                //executa
                executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);
                retorno = true;

            }
            catch (Exception e)
            {

            }
            return retorno;
        }

        public decimal totalPedido(int idPedido)
        {
            decimal valor = 0;
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append(" SELECT SUM(VALOR) as total FROM tb_detalhaPedido WHERE idPedido=" + idPedido);

                SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

                while (dr.Read())
                {
                    valor = Convert.ToDecimal(dr["total"]);
                }
            }
            catch (Exception e)
            {

            }
            return valor;
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
            query.Append(" dt.quantidade, dt.valor, st.statusPedido, dt.obs ");
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
                //pedido.obs = Convert.ToString(dr["obs"]);

                if (dr["obs"] == DBNull.Value)
                    pedido.obs = "";
                else
                    pedido.obs = Convert.ToString(dr["obs"]);


                lst.Add(pedido);
            }

            dr.Close();
            return lst;
        }

        public Pedido_VO adicionaPedidosInicial(Pedido_VO Pedido)
        {

            StringBuilder query = new StringBuilder();
            query.Append(" INSERT INTO tb_cliente_tem_pedido ([tb_cliente_idtb_cliente] ,[tb_funcionario_idtb_funcionario] ,[tb_status_idtb_statusPedido],[dtPedido]) ");
            query.Append(" VALUES ( @IdCliente, @idFuncionario, @IdStatus, @dtPedido);");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@IdCliente", SqlDbType.Int, Pedido.idCliente),
                createParametro("@idFuncionario", SqlDbType.Int, Pedido.idFuncionario),
                createParametro("@dtPedido", SqlDbType.DateTime, DateTime.Now),
                createParametro("@IdStatus", SqlDbType.Int, 1) // Aberto

            };

            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);

            int id = 0;
            StringBuilder queryID = new StringBuilder();
            queryID.Append("select top 1 idPedido as ID from tb_cliente_tem_pedido order by idPedido desc");

            SqlDataReader dr = executeDataReader(queryID.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                id = Convert.ToInt32(dr["id"]);
            }

            Pedido.idPedido = id;

            Pedido = adicionaPedidos(Pedido);
            dr.Close();
            return Pedido;
        }

        /*public Pedido_VO adicionaOpcionais(Pedido_VO Pedido)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" INSERT INTO tb_detalhaPedido ([idPedido],[idOpcao],[idSabor1_idtb_produtos],[idSabor2_idtb_produtos],[idSabor3_idtb_produtos],[quantidade],[valor],[idBorda],[Obs]) ");
            query.Append(" VALUES ( @IdPedido, @idOpcao, @IdSabor1, @IdSabor2, @IdSabor3, @Qtd, @Valor,@IdBorda,@Obs) ");

            //Pedido.idOpcao = Pedido.idOpcao == 0 ? null : Pedido.idOpcao;

            //parametros
            SqlParameter[] parametros = {
                createParametro("@IdPedido", SqlDbType.Int, Pedido.idPedido),
                createParametro("@idOpcao", SqlDbType.Int, Pedido.idOpcao),
                createParametro("@IdSabor1", SqlDbType.Int, Pedido.idSabor1),
                createParametro("@IdSabor2", SqlDbType.Int, Pedido.idSabor2),
                createParametro("@IdSabor3", SqlDbType.Int, Pedido.idSabor3),
                createParametro("@Qtd", SqlDbType.Int, Pedido.qtd),
                createParametro("@Valor", SqlDbType.Decimal, Pedido.valor),
                createParametro("@IdBorda", SqlDbType.Int, Pedido.idBorda),
                createParametro("@Obs", SqlDbType.VarChar, Pedido.obs)
            };


            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);


            

            return Pedido;
        }*/

        public Pedido_VO adicionaPedidos(Pedido_VO Pedido)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" INSERT INTO tb_detalhaPedido ([idPedido],[idOpcao],[idSabor1_idtb_produtos],[idSabor2_idtb_produtos],[idSabor3_idtb_produtos],[quantidade],[valor],[idBorda],[Obs]) ");
            query.Append(" VALUES ( @IdPedido, @idOpcao, @IdSabor1, @IdSabor2, @IdSabor3, @Qtd, @Valor,@IdBorda,@Obs) ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@IdPedido", SqlDbType.Int, Pedido.idPedido),
                createParametro("@idOpcao", SqlDbType.Int, Pedido.idOpcao),
                createParametro("@IdSabor1", SqlDbType.Int, Pedido.idSabor1),
                createParametro("@IdSabor2", SqlDbType.Int, Pedido.idSabor2),
                createParametro("@IdSabor3", SqlDbType.Int, Pedido.idSabor3),
                createParametro("@Qtd", SqlDbType.Int, Pedido.qtd),
                createParametro("@Valor", SqlDbType.Decimal, Pedido.valor),
                createParametro("@IdBorda", SqlDbType.Int, Pedido.idBorda),
                createParametro("@Obs", SqlDbType.VarChar, Pedido.obs)
            };


            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);

            if (Pedido.opcionais != null)
            {

                int idDetPedido = 0;
                StringBuilder queryIdDetalha = new StringBuilder();
                queryIdDetalha.Append("select top 1 idDetalhaPedido from tb_detalhaPedido where idPedido = " + Pedido.idPedido + " order by idDetalhaPedido desc");

                SqlDataReader drd = executeDataReader(queryIdDetalha.ToString(), CommandType.Text, false);

                while (drd.Read())
                {
                    idDetPedido = Convert.ToInt32(drd["idDetalhaPedido"]);
                }

                Pedido.idDetPed = idDetPedido;


                foreach (Opcional_VO opc in Pedido.opcionais)
                {

                    StringBuilder queryOpc = new StringBuilder();


                    queryOpc.Append(" INSERT INTO tb_pedido_tem_opcionais ([idDetalhaPedido],[idOpcional],[Data])");
                    queryOpc.Append(" VALUES ( @IdDetalhaPedido, @IdOpcional, @Data) ");
                    //parametros
                    SqlParameter[] parametrosOpc = {
                    createParametro("@IdDetalhaPedido", SqlDbType.Int, Pedido.idDetPed),
                    createParametro("@IdOpcional", SqlDbType.Int, opc.idOpcional),
                    createParametro("@Data", SqlDbType.DateTime, DateTime.Now)

                    };

                    //executa
                    executeNonQuery(queryOpc.ToString(), CommandType.Text, null, null, parametrosOpc, false);

                }
            }

            return Pedido;
        }

        public Pedido_VO atualizaPedidos(Pedido_VO Pedido)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" UPDATE tb_detalhaPedido SET [idPedido]=@IdPedido,[idOpcao]=@idOpcao,[idSabor1_idtb_produtos]=@IdSabor1,[idSabor2_idtb_produtos]=@IdSabor2,[idSabor3_idtb_produtos]=@IdSabor3,[quantidade]=@Qtd,[valor]=@Valor,[idBorda]=@idBorda,[Obs]=@Obs ");
            query.Append(" WHERE idDetalhaPedido = @idDetPedido ");

            //parametros
            SqlParameter[] parametros = {
                createParametro("@IdPedido", SqlDbType.Int, Pedido.idPedido),
                createParametro("@idOpcao", SqlDbType.Int, Pedido.idOpcao),
                createParametro("@IdSabor1", SqlDbType.Int, Pedido.idSabor1),
                createParametro("@IdSabor2", SqlDbType.Int, Pedido.idSabor2),
                createParametro("@IdSabor3", SqlDbType.Int, Pedido.idSabor3),
                createParametro("@Qtd", SqlDbType.Int, Pedido.qtd),
                createParametro("@Valor", SqlDbType.Decimal, Pedido.valor),
                createParametro("@idDetPedido", SqlDbType.Decimal, Pedido.idDetPed),
                createParametro("@IdBorda", SqlDbType.Int, Pedido.idBorda),
                createParametro("@Obs", SqlDbType.VarChar, Pedido.obs)


            };


            //executa
            executeNonQuery(query.ToString(), CommandType.Text, null, null, parametros, false);
            return Pedido;
        }


        public List<Pedido_VO> obterDadosDetPedido(int idDetPedido)
        {
            List<Pedido_VO> lst = new List<Pedido_VO>();


            // query
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT idDetalhaPedido as idDet, ctp.idPedido, tp.tipo, ");
            query.Append(" ISNULL(op.Opcao, '') as Opcao, tp.idtb_tipo, ");
            query.Append(" pr.idtb_produtos as idSabor1, pr2.idtb_produtos as idSabor2, pr3.idtb_produtos as idSabor3, ");
            query.Append(" CASE ");
            query.Append("  WHEN op.idOpcao = 2 THEN pr.nome + ' | ' + pr2.nome ");
            query.Append("  WHEN op.idOpcao = 3 THEN pr.nome + ' | ' + pr2.nome + ' | ' + pr3.nome ");
            query.Append("  ELSE pr.nome ");
            query.Append("  END as sabor, ");
            query.Append(" dt.quantidade, dt.valor, st.statusPedido, dt.idOpcao, dt.idBorda, dt.Obs ");
            query.Append(" FROM[dbo].[tb_cliente_tem_pedido] ctp ");
            query.Append(" INNER JOIN tb_detalhaPedido dt on dt.idPedido = ctp.idPedido ");
            query.Append(" LEFT JOIN tb_opcao op on dt.idOpcao = op.idOpcao ");
            query.Append(" INNER JOIN tb_statusPedido st on ctp.tb_status_idtb_statusPedido = st.idStatus ");
            query.Append(" INNER JOIN tb_produtos pr on dt.idSabor1_idtb_produtos = pr.idtb_produtos ");
            query.Append(" LEFT JOIN tb_produtos pr2 on dt.idSabor2_idtb_produtos = pr2.idtb_produtos ");
            query.Append(" LEFT JOIN tb_produtos pr3 on dt.idSabor3_idtb_produtos = pr3.idtb_produtos ");
            query.Append(" INNER JOIN tb_tipoProduto tp on tp.idtb_tipo = pr.tb_tipo_idtb_tipo  ");
            query.Append(" WHERE dt.idDetalhaPedido = " + idDetPedido);

            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            while (dr.Read())
            {
                Pedido_VO pedido = new Pedido_VO();
                pedido.idDetPed = Convert.ToInt32(dr["idDet"]);
                pedido.idPedido = Convert.ToInt32(dr["idPedido"]);
                pedido.idOpcao = Convert.ToInt32(dr["idOpcao"]);
                pedido.idBorda = Convert.ToInt32(dr["idBorda"]);
                pedido.tipo = Convert.ToString(dr["tipo"]);
                pedido.opcao = Convert.ToString(dr["opcao"]);
                pedido.idTipo = Convert.ToInt32(dr["idtb_tipo"]);

                if (dr["idSabor1"] != DBNull.Value) pedido.idSabor1 = Convert.ToInt32(dr["idSabor1"]);
                if (dr["idSabor2"] != DBNull.Value) pedido.idSabor2 = Convert.ToInt32(dr["idSabor2"]);
                if (dr["idSabor3"] != DBNull.Value) pedido.idSabor3 = Convert.ToInt32(dr["idSabor3"]);


                pedido.sabor = Convert.ToString(dr["sabor"]);
                pedido.qtd = Convert.ToInt32(dr["quantidade"]);
                pedido.obs = Convert.ToString(dr["Obs"]);
                pedido.valor = Convert.ToDecimal(dr["valor"]);
                pedido.StatusPedido = Convert.ToString(dr["statusPedido"]);
                lst.Add(pedido);
            }

            fecharTodasConexoes(dr);
            return lst;
        }

    }
}

using DAL.Cliente;
using DAL.Pedidos;
using MODEL.Cliente;
using MODEL.Pagamento;
using MODEL.Pedido;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Financeiro;

namespace BLL.Pedidos
{
    public class PedidosBLL
    {
        public List<Pedido_VO> obterDadosPedidos(int idPedido)
        {
            return new PedidosDAL().obterDadosPedidos(idPedido);
        }

        public List<Pedido_VO> obterDadosDetPedido(int idDetPedido)
        {
            return new PedidosDAL().obterDadosDetPedido(idDetPedido);
        }

        public object obterDadosFechamentoResumo(DateTime dataIni, DateTime dataFim, string fechamento)
        {
            return new PedidosDAL().obterDadosFechamentoResumo(dataIni, dataFim, fechamento);
        }

        public object obterDadosFechamentoPagtoResumo(DateTime dataIni, DateTime dataFim, string fechamento)
        {
            return new PedidosDAL().obterDadosFechamentoPagtoResumo(dataIni, dataFim, fechamento);
        }

        public Financeiro_VO obterDadosFechamentoResumoTotal(DateTime dataIni, DateTime dataFim, string fechamento)
        {
            return new PedidosDAL().obterDadosFechamentoResumoTotal(dataIni, dataFim, fechamento);
        }

        
        public Financeiro_VO obterDadosFechamentoResumoTotalEntregas(DateTime dataIni, DateTime dataFim, string fechamento)
        {
            return new PedidosDAL().obterDadosFechamentoResumoTotalEntregas(dataIni, dataFim, fechamento);
        }

        public DataSet obterDataSet()
        {
            return new PedidosDAL().obterDataSet();
        }

        public DataSet obterDataSetData(DateTime dataIni, DateTime dataFim)
        {
            return new PedidosDAL().obterDataSet(dataIni, dataFim);
        }


        public List<Cliente_VO> autoComplete(string text)
        {
            return new ClienteDAL().autoComplete(text);
        }

        public Pedido_VO adicionaPedido(Pedido_VO pedido)
        {
            if (pedido.idPedido==0)
                return new PedidosDAL().adicionaPedidosInicial(pedido);
            else
                return new PedidosDAL().adicionaPedidos(pedido);
        }

        public Pedido_VO atualizaPedido(Pedido_VO pedidoAtualizado)
        {
            return new PedidosDAL().atualizaPedidos(pedidoAtualizado);
        }

        public bool apagarDadosDetPedido(int idDetPed)
        {
            return new PedidosDAL().apagarDadosDetPedido(idDetPed);
        }

        public decimal totalPedido(int idDetPed)
        {
            return new PedidosDAL().totalPedido(idDetPed);
        }

        public List<Pagamento_VO> obterPagamento()
        {
            return new PedidosDAL().obterPagamento();
        }

        public void atualizaPagamento(Pagamento_VO pagamento)
        {
            new PedidosDAL().atualizaPagamento(pagamento);
        }

        public Cupom_VO obterDadosCupomFiscal(int idPedido)
        {
            return new PedidosDAL().obterDadosCupomFiscal(idPedido);
        }

    }
}

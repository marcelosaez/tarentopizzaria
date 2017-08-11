using DAL.Cliente;
using DAL.Pedidos;
using MODEL.Cliente;
using MODEL.Pedido;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DataSet obterDataSet()
        {
            return new PedidosDAL().obterDataSet();
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
    }
}

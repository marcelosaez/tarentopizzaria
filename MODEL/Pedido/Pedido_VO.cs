using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Pedido
{
    public class Pedido_VO
    {
        public int idCliente;
        public int qtd { get; set; }
        public int idDetPed { get; set; }
        public int idTipoProdutos { get; set; }
        public int idOpcao  { get; set; }
        public int idSabor1 { get; set; }
        public int idSabor2 { get; set; }
        public int idSabor3 { get; set; }

        public int idPedido { get; set; }
        public int idFuncionario { get; set; }
        public bool TemPedido { get; set; }
        public decimal valor { get; set; }
        public string tipo { get; set; }
        public string opcao { get; set; }
        public string sabor { get; set; }
        public string StatusPedido { get; set; }
    }
}

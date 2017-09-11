using MODEL.Pedido;
using BLL.Pedidos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.IO;
using MODEL.Empresa;
using BLL.Empresa;
using MODEL.Cliente;

namespace Framework.Impressao
{
    public class PrintCupom : PrintDocument
    {
        private int idPedido;
        private Empresa_VO empresa;
        private Cupom_VO cupom;
        private List<Pedido_VO> venda;
        private Font bold = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
        private Font regular = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular);
        private Font regularItens = new Font(FontFamily.GenericSansSerif, 6, FontStyle.Regular);
        Pedido_VO Pedido = new Pedido_VO();
        List<Pedido_VO> pedidos = new List<Pedido_VO>();
        

        public void ImprimeVendaVista(List<Pedido_VO> venda, int idEmp, int idPedido)
        {
            this.idPedido = idPedido;
            this.empresa = new EmpresaBLL().obterDadosEmpresa(idEmp);
            this.pedidos = new PedidosBLL().obterDadosPedidos(idPedido);
            this.cupom   = new PedidosBLL().obterDadosCupomFiscal(idPedido);

            PrinterSettings settings = new PrinterSettings();

            try
            {
                this.PrinterSettings.PrinterName = settings.PrinterName;
                //this.PrinterSettings.Copies = 2;
                this.OriginAtMargins = false;
                this.PrintPage += new PrintPageEventHandler(printPage);
                this.Print();

            }
            catch (Exception ex)
            {

                throw;
            }

            

        }

        private void printPage(object send, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            int offset = 135;

            //print header
            //graphics.DrawString(empresa.razaoSocial, bold, Brushes.Black, 20, 0);
            //graphics.DrawString(empresa.endereco.endereco + " Nº " + empresa.endereco.numero, regular, Brushes.Black, 100, 15);
            graphics.DrawString("***********************************************************", regular, Brushes.Black, 0, 0);
            graphics.DrawString("" + empresa.NomeFantasia.ToUpper() + "", regular, Brushes.Black, 65, 15);
            graphics.DrawString("" + empresa.Endereco.ToUpper() + " - " +empresa.Numero+" - " + empresa.Bairro.ToUpper() + " " + empresa.Cidade.ToUpper() + " ", regular, Brushes.Black, 0, 30); //+ empresa.Estado.ToUpper() + ""
            graphics.DrawString("***********************************************************", regular, Brushes.Black, 0, 45);
            //graphics.DrawLine(Pens.Black, 20, 30, 310, 50);
            graphics.DrawString("******** CUPOM NÃO FISCAL ************", bold, Brushes.Black, 0, 55);
            //graphics.DrawLine(Pens.Black, 20, 50, 310, 80);
            graphics.DrawString("PEDIDO:" + this.idPedido , bold, Brushes.Black, 0, 80);

            //graphics.DrawLine(Pens.Black, 20, 75, 310, 105);

            //itens header
            graphics.DrawString("QTD", regular, Brushes.Black, 0, 110);
            graphics.DrawString("PRODUTO", regular, Brushes.Black, 60, 110);
            //graphics.DrawString("OBS.", regular, Brushes.Black, 240, 130);
            graphics.DrawString("TOTAL", regular, Brushes.Black, 200, 110);

            //graphics.DrawLine(Pens.Black, 20, 95, 310, 155);

            
            //itens de venda
            foreach (Pedido_VO pedido in pedidos)
            {
                string tipo = pedido.tipo;
                string produto = pedido.sabor;
                string obs = pedido.obs;


                graphics.DrawString(Convert.ToString(pedido.qtd), regular, Brushes.Black, 0, offset);
                graphics.DrawString(tipo, regular, Brushes.Black, 60, offset);
                graphics.DrawString("R$" + Convert.ToString(pedido.valor), regular, Brushes.Black, 200, offset);


                offset += 20;
                graphics.DrawString(produto.Length > 40 ? produto.Substring(0, 40) + "..." : produto, regular, Brushes.Black, 60, offset);

                if (obs != "")
                {
                    offset += 15;
                    if (obs.Length > 40)
                    {
                        graphics.DrawString("*" + obs.Substring(0, 40), regular, Brushes.Black, 60, offset);
                        offset += 15;
                        graphics.DrawString(" " + obs.Substring(40, obs.Length-40), regular, Brushes.Black, 60, offset);
                    }
                    else
                        graphics.DrawString("*" + obs, regular, Brushes.Black, 60, offset);


                }

                offset += 20;
            }
            
            //total
            //graphics.DrawLine(Pens.Black, 20, offset, 350, offset);
            offset += 10;
            
            decimal total = 0;
            total = new PedidosBLL().totalPedido(this.idPedido);

            graphics.DrawString("TOTAL R$:"+total, bold, Brushes.Black, 0, offset);
            offset += 15;

            //graphics.DrawLine(Pens.Black, 20, offset, 310, offset);
            offset += 15;
            graphics.DrawString("***********************************************************",  regular, Brushes.Black, 0, offset);

            offset += 15;
            graphics.DrawString("******************** PAGAMENTO *********************", regular, Brushes.Black, 0, offset);

            offset += 15;
            graphics.DrawString("***********************************************************", regular, Brushes.Black, 0, offset);

            offset += 15;
            graphics.DrawString("" + cupom.pagamento.TipoPagamento.ToUpper() + "", bold, Brushes.Black, 0, offset);


            offset += 25;
            graphics.DrawString("=======================================", regular, Brushes.Black, 0, offset);
            offset += 15;
            graphics.DrawString("=========== DADOS ENTREGA ============", regular, Brushes.Black, 0, offset);
            offset += 25;
            graphics.DrawString("NOME: " + cupom.cliente.nome.ToUpper() , regular, Brushes.Black, 0, offset);
            offset += 15;
            graphics.DrawString("END: "  + cupom.cliente.endereco.ToUpper(), regular, Brushes.Black, 0, offset);
            offset += 15;
            graphics.DrawString("N: "   + cupom.cliente.numero, regular, Brushes.Black, 0, offset);
            offset += 15;
            graphics.DrawString("TEL: "  + formataTelefone(cupom.cliente ), regular, Brushes.Black, 0, offset);
            offset += 15;
            graphics.DrawString("TIPO: " +  cupom.entrega.ToUpper() + "       " , bold, Brushes.Black, 0, offset);
            offset += 15;
            graphics.DrawString("=======================================", regular, Brushes.Black, 0, offset);


            //bottom
            offset += 15;

            string nomeFunc = cupom.funcionario.nome.Length > 10 ? cupom.funcionario.nome.Substring(0,10) + "" : cupom.funcionario.nome;
            graphics.DrawString("ATEND: " + nomeFunc, regularItens, Brushes.Black, 0, offset);

            offset += 15;
            graphics.DrawString("DATA: " + DateTime.Now.ToString("dd/MM/yyyy"), regularItens, Brushes.Black, 0, offset);
            graphics.DrawString("HORA: " + DateTime.Now.ToString("HH:mm:ss"), regularItens, Brushes.Black, 200, offset);
            
            e.HasMorePages = false;

        }


        private string formataTelefone(Cliente_VO cliente)
        {
            string tel = "";
            if (cliente.dddcel != 0)
                tel = cliente.dddcel + " " + cliente.cel;
            if (cliente.dddres != 0)
            {
                if (cliente.dddcel != 0)
                    tel += " | ";
                tel += cliente.dddres + " " + cliente.telres;
            }
            return tel;
        }


    }
}

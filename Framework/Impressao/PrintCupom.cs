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
using System.Security.Principal;
using MODEL.Produto;

namespace Framework.Impressao
{
    public class PrintCupom : PrintDocument
    {
        private int idPedido;
        private Empresa_VO empresa;
        private Cupom_VO cupom;
        //private List<Pedido_VO> venda;
        private Font bold = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold);
        private Font regular = new Font(FontFamily.GenericSansSerif, 8, FontStyle.Regular); 
        private Font regularItens = new Font(FontFamily.GenericSansSerif, 6, FontStyle.Regular);
        Pedido_VO Pedido = new Pedido_VO();
        List<Pedido_VO> pedidos = new List<Pedido_VO>(); 
        

        public void ImprimeVendaVista(List<Pedido_VO> venda, int idEmp, int idPedido, decimal valorEntrega)
        {
            this.idPedido = idPedido;
            this.empresa = new EmpresaBLL().obterDadosEmpresa(idEmp);
            this.pedidos = new PedidosBLL().obterDadosPedidos(idPedido);
            this.cupom   = new PedidosBLL().obterDadosCupomFiscal(idPedido);

            this.cupom.valorEntrega = valorEntrega;



            PrinterSettings settings = new PrinterSettings();

            try
            {
                //this.PrinterSettings.PrinterName = "\\\\CAIXA\\Diebold Procomp IM453HP_B";  
                this.PrinterSettings.PrinterName = settings.PrinterName;
                
                if (PrinterSettings.IsValid)
                {
                    using (WindowsIdentity.GetCurrent().Impersonate())
                    {
                        //this.PrinterSettings.Copies = 2;
                        this.OriginAtMargins = false;
                        this.PrintPage += new PrintPageEventHandler(printPage);
                        this.Print();
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter("C:\\inetpub\\wwwroot\\tarento\\logs\\erro" +  DateTime.Now.ToString("_ddMMyyyy_HHmmss") + ".txt", true))

                    {
                        writer.WriteLine("sistema: Impressora");
                        writer.WriteLine("erro: Impressora invalida!");

                    }

                }
            }
            catch (InvalidPrinterException exc)
            {
                // handle your errors here.
                using (StreamWriter writer = new StreamWriter("C:\\inetpub\\wwwroot\\tarento\\logs\\erro" + DateTime.Now.ToString("_ddMMyyyy_HHmmss") + ".txt", true))

                {
                    writer.WriteLine("sistema: Impressão Exceção");
                    writer.WriteLine("erro: " + exc.Message);

                }
            }
            catch (Exception ex)
            {
                using (StreamWriter writer = new StreamWriter("C:\\inetpub\\wwwroot\\tarento\\logs\\erro"+DateTime.Now.ToString("_ddMMyyyy_HHmmss") +".txt", true))
                   
                {
                    writer.WriteLine("sistema: Impressora");
                    writer.WriteLine("erro: "+ex.Message);
                    
                }

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
            graphics.DrawString("PRODUTO", regular, Brushes.Black, 40, 110);
            //graphics.DrawString("OBS.", regular, Brushes.Black, 240, 130);
            graphics.DrawString("TOTAL", regular, Brushes.Black, 210, 110);

            //graphics.DrawLine(Pens.Black, 20, 95, 310, 155);

            int i = 0;

            //itens de venda
            foreach (Pedido_VO pedido in pedidos)
            {
                string tipo = pedido.tipo;
                string produto = pedido.sabor;
                string obs = pedido.obs;
                string borda = "";
                string opcao = pedido.opcao;

                if (pedido.borda != null && pedido.borda.Trim() !="")
                    borda = " Borda: " + pedido.borda;


                graphics.DrawString(Convert.ToString(pedido.qtd), regular, Brushes.Black, 0, offset);
                //graphics.DrawString(tipo +  borda, regular, Brushes.Black, 60, offset);

                if (opcao.Trim() != "")
                    opcao = " - " + opcao.ToUpper();

                    /*if(opcao.Trim() != "")
                    {
                        if (pedido.opcao.ToUpper() == "INTEIRA")
                            opcao = "";
                        if (pedido.opcao.ToUpper() == "MEIO A MEIO")
                            opcao = " - 1/2 - ";
                        if (pedido.opcao.ToUpper() == "TRÊS PEDAÇOS")
                            opcao = " - 1/3 - ";
                    }*/


                graphics.DrawString(tipo.ToUpper() +  opcao + "", regular, Brushes.Black, 40, offset);


                graphics.DrawString("R$ " + Convert.ToString(pedido.valor), regular, Brushes.Black, 210, offset);


                offset += 20;
                //graphics.DrawString(produto.Length > 40 ? produto.Substring(0, 40).ToUpper() + "..." : produto.ToUpper(), regular, Brushes.Black, 40, offset);


                if (produto.Length > 40)
                {
                    graphics.DrawString( produto.Substring(0, 40).ToUpper(), regular, Brushes.Black, 40, offset);
                    offset += 15;
                    graphics.DrawString( produto.Substring(40,  produto.Length-40).ToUpper(), regular, Brushes.Black, 40, offset);

                    //produto.Length > 40 ? produto.Substring(0, 40).ToUpper() + "..."

                } else
                    graphics.DrawString(produto.ToUpper(), regular, Brushes.Black, 40, offset);



                if (obs != "")
                {
                    obs = obs.Replace('\n', ' ');
                    offset += 15;
                    if (obs.Length > 30)
                    {

                        int div = obs.Length / 30;

                        int pos = 0;

                        for (int j = 0; j < div;)
                        {
                            graphics.DrawString("*" + obs.Substring(j, 30).ToUpper(), regular, Brushes.Black, 40, offset);
                            offset += 15;
                            j += 30;
                            pos = j;
                        }

                        int resto = obs.Length % 30;

                        if (resto > 0)
                        {
                            graphics.DrawString(" " + obs.Substring(pos, resto).ToUpper(), regular, Brushes.Black, 40, offset);
                            //offset += 15;
                        }
                    }
                    else
                        graphics.DrawString("*" + obs.ToUpper(), regular, Brushes.Black, 50, offset);

                }
                if (pedidos[i].opcionais != null)
                {
                    foreach (Opcional_VO opc in pedidos[i].opcionais)
                    {
                        offset += 15;
                        graphics.DrawString(" + " + opc.nome.ToUpper(), regular, Brushes.Black, 40, offset);
                    }
                }

                if (borda != null)
                {
                    offset += 15;
                    graphics.DrawString(" * " + borda.ToUpper(), bold, Brushes.Black, 40, offset);
                }
                


               i++;
                offset += 25;
            }
            
            //total
            //graphics.DrawLine(Pens.Black, 20, offset, 350, offset);
            offset += 10;
            
            decimal total = 0;
            total = new PedidosBLL().totalPedido(this.idPedido);

            if (cupom.entrega == "Entrega")
                total = total + this.cupom.valorEntrega;

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

            if (cupom.pagamento.obs.Trim() != "")
            {
                offset += 15;
                graphics.DrawString("OBS: " + cupom.pagamento.obs.ToUpper() + "", bold, Brushes.Black, 0, offset);
            }


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

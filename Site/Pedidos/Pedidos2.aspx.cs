using BLL;
using BLL.Pedidos;
using BLL.Produto;
using MODEL.Funcionario;
using MODEL.Pedido;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Pedidos
{
    public partial class Pedidos2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                carregarDados();
            }
        }

        private void carregarDados()
        {
            //Carrego o pedido
            Pedido_VO Pedido = new Pedido_VO();
            List<Pedido_VO> lst = new List<Pedido_VO>();
            

            if (Session["NovoPedido"] != null)
            {
               
                lst = (List<Pedido_VO>)Session["NovoPedido"];
                if (lst[0].TemPedido)
                    obterPedido();
            }

            //Carrego os tipos dos produtos
            carregarTiposProdutos();
            

        }

        private void carregarQuantidade()
        {
            this.ddlQtd.Visible = true;
            this.ddlQtd.Items.Clear();
            this.ddlQtd.Items.Add(new ListItem("Qtd", ""));
            for (int i = 1; i <= 20; i++)
            {
                ddlQtd.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            ddlQtd.SelectedIndex = 0;
        }

        private void carregarBorda()
        {
            List<BordaProduto_VO> bordas = new ProdutoBLL().obterBordas();

            //this.ddlBorda.Visible = true;
            this.ddlBorda.Items.Clear();
            this.ddlBorda.Items.Add(new ListItem("Selecione a borda", ""));
            if (bordas != null)
            {
                foreach (BordaProduto_VO tpBorda in bordas)
                {
                    this.ddlBorda.Items.Add(new ListItem(tpBorda.borda.ToString(), tpBorda.idBordaProduto.ToString()));

                    //if (tpBorda.idBordaProduto == tipoID)
                    //    ddlTipoProdutos.SelectedIndex = tipoID;
                }
            }
            ddlQtd.SelectedIndex = 0;
        }

        protected void gvListaPedidos_RowCommand(Object src, GridViewCommandEventArgs e)
        {
            int index = 0;
            Int32.TryParse(e.CommandArgument.ToString(), out index);

            if (e.CommandName == "Edit")
            {
                int id_ = 0;
                Int32.TryParse(gvListaPedidos.DataKeys[index].Value.ToString(), out id_);

                Response.Redirect("Editar.aspx?idPedido=" + id_.ToString());
            }

        }
        protected void gvListaPedidos_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
        }
        private void carregarTiposProdutos(int tipoID)
        {
            List<TipoProduto_VO> produtos = new ProdutoTipoBLL().obterTipos();

            this.ddlTipoProdutos.Items.Clear();
            this.ddlTipoProdutos.Items.Add(new ListItem("Selecione o tipo", ""));

            if (produtos != null)
            {
                foreach (TipoProduto_VO tpProd in produtos)
                {
                    this.ddlTipoProdutos.Items.Add(new ListItem(tpProd.tipo, tpProd.idTipoProduto.ToString()));

                    if (tpProd.idTipoProduto == tipoID)
                        ddlTipoProdutos.SelectedIndex = tipoID;
                }
            }
        }
        private void carregarTiposProdutos()
        {
            List<TipoProduto_VO> produtos = new ProdutoTipoBLL().obterTipos();

            this.ddlTipoProdutos.Items.Clear();
            this.ddlTipoProdutos.Items.Add(new ListItem("Selecione o tipo", ""));

            if (produtos != null)
            {
                foreach (TipoProduto_VO tpProd in produtos)
                {
                    this.ddlTipoProdutos.Items.Add(new ListItem(tpProd.tipo, tpProd.idTipoProduto.ToString()));
                }
            }
        }
        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            Pedido_VO NovoPedido = new Pedido_VO();
            Pedido_VO Pedido = new Pedido_VO();
            List<Pedido_VO> lstPedido = new List<Pedido_VO>();
            Funcionario_VO usuario = AutenticacaoBLL.obterUsuarioAutenticado();
            Pedido_VO clientePedido = new Pedido_VO();


            if (usuario != null)
                NovoPedido.idFuncionario = usuario.id;

            if (Session["pedido"] != null)
            {
                if(Session["NovoPedido"] != null)
                {
                    lstPedido = (List<Pedido_VO>)Session["NovoPedido"];
                    clientePedido.idPedido = lstPedido[0].idPedido;
                }

                //    clientePedido = (Pedido_VO)Session["NovoPedido"];
                else
                    clientePedido = (Pedido_VO)Session["pedido"];

                NovoPedido.idCliente = clientePedido.idCliente;
            }

            if (!validaForm())
            {
                ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newAlert('Preencha todos os campos!');", true);
                return;
            }

            if (Session["NovoPedido"] != null)
            {
   
                lstPedido = (List<Pedido_VO>)Session["NovoPedido"];
                NovoPedido.idPedido = lstPedido[0].idPedido;
                NovoPedido.idCliente = lstPedido[0].idCliente;

            }

            //if (Session["Pedido"] != null)
            //{
            //    Pedido = (Pedido_VO)Session["Pedido"];
            //}

            NovoPedido.idTipoProdutos = Convert.ToInt32(ddlTipoProdutos.SelectedValue);

            if (ddlTipoProdutos.SelectedValue == "1")
            {
                NovoPedido.idOpcao = Convert.ToInt32(ddlOpcao.SelectedValue);
            }

    
            if (ddlSabor1.SelectedValue !=null)
                NovoPedido.idSabor1 = Convert.ToInt32(ddlSabor1.SelectedValue);

            if (ddlSabor2.SelectedValue != "")
                NovoPedido.idSabor2 = Convert.ToInt32(ddlSabor2.SelectedValue);

            if (ddlSabor3.SelectedValue != "")
                NovoPedido.idSabor3 = Convert.ToInt32(ddlSabor3.SelectedValue);

            NovoPedido.qtd = Convert.ToInt32(ddlQtd.SelectedValue);
            NovoPedido.TemPedido = true;

            decimal bordaValor = 0;
            //ViewState["Preco"] = (decimal)ViewState["Preco"] ;
            if (ViewState["BordaValor"] != null)
            {
                bordaValor = (decimal)ViewState["BordaValor"];
                ViewState["Preco"] = (decimal)ViewState["Preco"] + (bordaValor * Convert.ToInt32(ddlQtd.SelectedValue));

            }

            NovoPedido.valor = (decimal)ViewState["Preco"];


            PedidosBLL pedido = new PedidosBLL();
            NovoPedido = pedido.adicionaPedido(NovoPedido);

            lstPedido.Add(NovoPedido);

            Session["Pedido"] = NovoPedido.idPedido;
            Session["NovoPedido"] = lstPedido;

            Response.Redirect("Pedidos2.aspx");


        }

        private bool validaForm()
        {
            bool retorno = true;
            if (ddlTipoProdutos.SelectedValue == "0")
                retorno = false;
            if (ddlSabor1.SelectedValue == "0")
                retorno = false;
            if ((ddlQtd.SelectedValue == "0") || (ddlQtd.SelectedValue == ""))
                retorno = false;

            return retorno;
        }

        protected void cmdVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void cmdFinalizar_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Default.aspx");
        }
        

        protected void ddlTipoProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlOpcao.Visible = false;
            ddlQtd.Visible = false;
            limpaTudo();
            carregarQuantidade();

            if (ddlTipoProdutos.SelectedValue == "1")
            {

                carregaOpcao();
                carregarBorda();
                ddlOpcao.Visible = true;
                divBorda.Visible = true;
            }
            else
            {
                divBorda.Visible = false;
                if(ddlTipoProdutos.SelectedValue !="")
                    carregaSabor(Convert.ToInt32(ddlTipoProdutos.SelectedValue));
            }


        }

        private void limpaTudo()
        {
            this.ddlSabor1.Items.Clear();
            this.ddlSabor1.Visible = false;
            if (ddlSabor2.Visible)
            {
                this.ddlSabor2.Items.Clear();
                this.ddlSabor2.Visible = false;
            }
            if (ddlSabor3.Visible)
            {
                this.ddlSabor3.Items.Clear();
                this.ddlSabor3.Visible = false;
            }


            this.ddlQtd.Items.Clear();
            this.ddlQtd.Visible = false;
            ViewState["Sabores"] = null;

        }

        private void carregaOpcao()
        {
            List<OpcaoProduto_VO> opcao = new OpcaoBLL().obterTipos();

            this.ddlOpcao.Items.Clear();
            this.ddlOpcao.Items.Add(new ListItem("Selecione a opção", ""));

            if (opcao != null)
            {
                foreach (OpcaoProduto_VO opc in opcao)
                {
                    this.ddlOpcao.Items.Add(new ListItem(opc.Opcao, opc.idOpcao.ToString()));
                }
            }
        }

        protected void ddlOpcao_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSabor1.Visible = false;
            ddlSabor2.Visible = false;
            ddlSabor3.Visible = false;
            ViewState["Sabores"] = null;
            if (ddlOpcao.SelectedValue == "1")
            {
                carregaSabor(ddlSabor1);
                ViewState["Sabores"] = 1;
            }
            if (ddlOpcao.SelectedValue == "2")
            {
                carregaSabor(ddlSabor1);
                carregaSabor(ddlSabor2);
                ViewState["Sabores"] = 2;

            }
            if (ddlOpcao.SelectedValue == "3")
            {
                carregaSabor(ddlSabor1);
                carregaSabor(ddlSabor2);
                carregaSabor(ddlSabor3);
                ViewState["Sabores"] = 3;

            }


        }

        private void carregaSabor(DropDownList ddl)
        {
            ddl.Visible = true;


            List<Produto_VO> lstSabores = new ProdutoBLL().obterSabores(Convert.ToInt32(ddlTipoProdutos.SelectedValue));

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("Selecione o sabor", ""));

            if (lstSabores != null)
            {
                foreach (Produto_VO sabor in lstSabores)
                {
                    ddl.Items.Add(new ListItem(sabor.nome + " - R$ "  +sabor.valor , sabor.idProduto.ToString()));

                }
            }

            carregarQuantidade();

        }

        private void carregaSabor(int IdSabores)
        {
            ddlSabor1.Visible = true;

            List<Produto_VO> lstSabores = new ProdutoBLL().obterSabores(Convert.ToInt32(IdSabores));

            ddlSabor1.Items.Clear();
            ddlSabor1.Items.Add(new ListItem("Selecione o sabor", ""));

            if (lstSabores != null)
            {
                foreach (Produto_VO sabor in lstSabores)
                {
                    ddlSabor1.Items.Add(new ListItem(sabor.nome + " - R$ " + sabor.valor, sabor.idProduto.ToString()));

                }
            }
        }


        protected void ddlQtd_SelectedIndexChanged(object sender, EventArgs e)
        {

                
            int qtdSabores = 0;
            int qtd = 0;
            if (ViewState["Sabores"] != null)
                qtdSabores = (int)ViewState["Sabores"];

            decimal valor = ObterValor(qtdSabores);
            decimal total = 0;
            decimal valorBorda = 0;
            if (ddlQtd.SelectedValue != "")
                qtd = Convert.ToInt32(ddlQtd.SelectedValue);
            else
                return;
            total = (qtd * valor) ;

            if (ViewState["BordaValor"] != null)
                valorBorda = qtd * (decimal)ViewState["BordaValor"];

            ViewState["Preco"] = total;

            total += valorBorda;


            ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newSucess('R$ "+total+"');", true);
        }

        protected void ddlSabor_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregarQuantidade();
            if ((ddlSabor1.SelectedValue != "") && (ddlTipoProdutos.SelectedValue != "1"))
                ViewState["Sabores"] = 1;

        }

        private decimal ObterValor(int qtdSabores)
        {
            if(qtdSabores != 0)
            {
                if (qtdSabores == 1) return new ProdutoBLL().obterValor(Convert.ToInt32(ddlSabor1.SelectedValue), 0, 0);
                if ((qtdSabores == 2) && (ddlSabor1.SelectedValue!="") && (ddlSabor2.SelectedValue != ""))
                    return new ProdutoBLL().obterValor(Convert.ToInt32(ddlSabor1.SelectedValue), Convert.ToInt32(ddlSabor2.SelectedValue), 0);
                if ((qtdSabores == 3) && (ddlSabor1.SelectedValue != "") && (ddlSabor2.SelectedValue != "") && (ddlSabor3.SelectedValue != ""))
                    return new ProdutoBLL().obterValor(Convert.ToInt32(ddlSabor1.SelectedValue), Convert.ToInt32(ddlSabor2.SelectedValue), Convert.ToInt32(ddlSabor3.SelectedValue));


            }

            return 0;
        }

        private decimal ObterValorBorda(int idBorda)
        {
            if (idBorda != 0)
            {
             return new ProdutoBLL().obterValorBorda(idBorda);
            }

            return 0;
        }



        private void obterPedido()
        {
            Pedido_VO Pedido = new Pedido_VO();
            List<Pedido_VO> lst = new List<Pedido_VO>();
            if (Session["NovoPedido"] != null)
            {
                lst = (List<Pedido_VO>)Session["NovoPedido"];
                gvListaPedidos.DataSourceID = null;
                //Pedido = (Pedido_VO)Session["NovoPedido"];

                lst = new PedidosBLL().obterDadosPedidos(lst[0].idPedido);
                gvListaPedidos.DataSource = lst;
                //gvListaPedidos.DataBind();
            }
           
        }

        protected void MostraBorda_Click(object sender, EventArgs e)
        {
            if (fancyCheckBox.Checked)
                ddlBorda.Visible = true;
            else
            {
                ddlBorda.Visible = false;
                ddlBorda.SelectedIndex = 0;
            }
        }

        protected void ddlBorda_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idBorda = Convert.ToInt32(ddlBorda.SelectedValue);
            decimal valor = new ProdutoBLL().obterValorBorda(idBorda);
            ViewState["BordaValor"] = valor;

            decimal qtd = 0;
            decimal total = 0;
            //decimal bordaValor = 0;

            if (ddlQtd.SelectedValue != "")
                qtd = Convert.ToInt32(ddlQtd.SelectedValue);
            else
                return;
            if(qtd > 0)
                total = (qtd * valor);

            decimal pedido = (decimal)ViewState["Preco"];
            decimal pedidoTotal = pedido + total;

            //ViewState["Preco"] = pedidoTotal;

            /*if (ViewState["BordaValor"] != null)
            {
                bordaValor = (decimal)ViewState["BordaValor"];
                ViewState["Preco"] = (decimal)ViewState["Preco"] + (bordaValor * Convert.ToInt32(ddlQtd.SelectedValue));

            }*/


            ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newSucess('R$ " + pedidoTotal + "');", true);

        }
    }
}
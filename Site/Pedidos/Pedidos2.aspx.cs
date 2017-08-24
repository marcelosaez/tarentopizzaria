﻿using BLL;
using BLL.Pedidos;
using BLL.Produto;
using MODEL.Funcionario;
using MODEL.Pedido;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
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
            if (Request["idDetPed"] != null)
            {
                int idDetPed = 0;
                int.TryParse(Request["idDetPed"].ToString(), out idDetPed);

                if (idDetPed > 0)
                {
                    this.editarPedido(idDetPed);

                }

            }
            else
            {
                if (Session["NovoPedido"] != null)
                {

                    lst = (List<Pedido_VO>)Session["NovoPedido"];
                    if (lst[0].TemPedido)
                        obterPedido();
                }
            }

            //Carrego os tipos dos produtos
            carregarTiposProdutos();
            

        }

        private void editarPedido(int idDetPed)
        {
            Session["DetPedido"] = idDetPed;
            var pedido = new PedidosBLL().obterDadosDetPedido(idDetPed);
            if (pedido.Count > 0)
                PreenchePedido(pedido);
        }

        

        private void PreenchePedido(List<Pedido_VO> pedido)
        {
            limpaTudo();
            cmdAtualizar.Visible = true;
            ddlTipoProdutos.SelectedIndex = pedido[0].idTipo;

            if (pedido[0].idTipo == 1)
            {
                carregaOpcao(pedido[0].idOpcao);
                ddlOpcao.Visible = true;
            }



            //if (pedido[0].idOpcao == 1)
            //{
                carregaSabor(ddlSabor1, pedido[0].idSabor1);
                ViewState["Sabores"] = 1;
            //}
            if (pedido[0].idOpcao == 2)
            {
                carregaSabor(ddlSabor1, pedido[0].idSabor1);
                carregaSabor(ddlSabor2, pedido[0].idSabor2);
                ViewState["Sabores"] = 2;

            }
            if (pedido[0].idOpcao == 3)
            {
                carregaSabor(ddlSabor1, pedido[0].idSabor1);
                carregaSabor(ddlSabor2, pedido[0].idSabor2);
                carregaSabor(ddlSabor3, pedido[0].idSabor3);
                ViewState["Sabores"] = 3;

            }

            //ddlOpcao.SelectedIndex = pedido[0].idOpcao;

            decimal valorBorda = 0;

            if (pedido[0].idBorda != 0)
            {
                int idBorda = Convert.ToInt32(pedido[0].idBorda);
                decimal valor = new ProdutoBLL().obterValorBorda(idBorda);
                valorBorda = valor * pedido[0].qtd;
                ViewState["BordaValor"] = valorBorda;
                divBorda.Visible = true;
                fancyCheckBox.Checked = true;
                ddlBorda.Visible = true;
                carregarBorda(pedido[0].idBorda);

            }


            carregarQuantidade(pedido[0].qtd);

            ViewState["Preco"] = Convert.ToDecimal(pedido[0].valor) - (valorBorda);

            if (! String.IsNullOrEmpty(pedido[0].obs))
            {
                txtObs.Text = pedido[0].obs;
            }


            updPainel1.Update();
            //throw new NotImplementedException();
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

        private void carregarQuantidade(int qtd)
        {
            this.ddlQtd.Visible = true;
            this.ddlQtd.Items.Clear();
            this.ddlQtd.Items.Add(new ListItem("Qtd", ""));
            for (int i = 1; i <= 20; i++)
            {
                ddlQtd.Items.Add(new ListItem(i.ToString(), i.ToString()));
                if (i== qtd)
                {
                    ddlQtd.SelectedIndex = i;
                }
                //i++;
            }
            //ddlQtd.SelectedIndex = 0;
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
            //ddlQtd.SelectedIndex = 0;
        }

        private void carregarBorda(int idBorda)
        {
            List<BordaProduto_VO> bordas = new ProdutoBLL().obterBordas();

            //this.ddlBorda.Visible = true;
            this.ddlBorda.Items.Clear();
            this.ddlBorda.Items.Add(new ListItem("Selecione a borda", ""));
            int i = 0;
            if (bordas != null)
            {
                
                foreach (BordaProduto_VO tpBorda in bordas)
                {
                    i++;
                    this.ddlBorda.Items.Add(new ListItem(tpBorda.borda.ToString(), tpBorda.idBordaProduto.ToString()));

                    if (tpBorda.idBordaProduto == idBorda)
                        ddlBorda.SelectedIndex = i;
                }
            }
            //ddlQtd.SelectedIndex = 0;
        }

        protected void gvListaPedidos_RowCommand(Object src, GridViewCommandEventArgs e)
        {
            int index = 0;
            Int32.TryParse(e.CommandArgument.ToString(), out index);
            
            if (e.CommandName == "Edit")
            {
                int detPedido = Convert.ToInt32(e.CommandArgument.ToString());
                editarPedido(detPedido);
            }

            if (e.CommandName == "Delete")
            {
                int detPedido = Convert.ToInt32(e.CommandArgument.ToString());
                //excluirPedido(detPedido);
                bool excluido = new PedidosBLL().apagarDadosDetPedido(detPedido);
                if (excluido == true)
                     Response.Redirect("Pedidos2.aspx");
                else
                    ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newAlert('Nao foi possivel excluir o pedido!');", true);

            }


        }
        
        decimal totalPedido = 0;

        protected void gvListaPedidos_RowDataBound(Object sender, GridViewRowEventArgs e)
        {
            
            //decimal total;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string valor = ((Label)e.Row.FindControl("lblValor")).Text; //(Label)e.Row.FindControl("lblValor");
                if (valor != null)
                    totalPedido += Convert.ToDecimal(valor.ToString());
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //decimal valorTotal = Convert.ToDecimal(ViewState["PrecoTotal"].ToString());
                Label lbl = (Label)e.Row.FindControl("lblTotal");
                lbl.Text = totalPedido.ToString();
                e.Row.CssClass = "rodape";
            }
        }

        private void carregarTiposProdutos(int tipoID)
        {
            List<TipoProduto_VO> produtos = new ProdutoTipoBLL().obterTipos();

            this.ddlTipoProdutos.Items.Clear();
            this.ddlTipoProdutos.Items.Add(new ListItem("Selecione o produto", ""));

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
            this.ddlTipoProdutos.Items.Add(new ListItem("Selecione o produto", ""));

            if (produtos != null)
            {
                foreach (TipoProduto_VO tpProd in produtos)
                {
                    this.ddlTipoProdutos.Items.Add(new ListItem(tpProd.tipo, tpProd.idTipoProduto.ToString()));
                }
            }
        }

        protected void cmdAtualizar_Click(object sender, EventArgs e)
        {
            int idDetPed = 0;

            if (Session["DetPedido"] != null)
            {
                int.TryParse(Session["DetPedido"].ToString(), out idDetPed);

            }

            if (!validaForm())
            {
                ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newAlert('Preencha todos os campos!');", true);
                return;
            }


            PedidosBLL pedido = new PedidosBLL();
            Pedido_VO  PedidoAtualizado = new Pedido_VO();

            PedidoAtualizado.idTipoProdutos = Convert.ToInt32(ddlTipoProdutos.SelectedValue);

            if (ddlTipoProdutos.SelectedValue == "1")
            {
                PedidoAtualizado.idOpcao = Convert.ToInt32(ddlOpcao.SelectedValue);
            }


            if (ddlSabor1.SelectedValue != null)
                PedidoAtualizado.idSabor1 = Convert.ToInt32(ddlSabor1.SelectedValue);

            if (ddlSabor2.SelectedValue != "")
                PedidoAtualizado.idSabor2 = Convert.ToInt32(ddlSabor2.SelectedValue);

            if (ddlSabor3.SelectedValue != "")
                PedidoAtualizado.idSabor3 = Convert.ToInt32(ddlSabor3.SelectedValue);

            PedidoAtualizado.qtd = Convert.ToInt32(ddlQtd.SelectedValue);
            PedidoAtualizado.TemPedido = true;

            decimal bordaValor = 0;
            //ViewState["Preco"] = (decimal)ViewState["Preco"] ;
            if (ViewState["BordaValor"] != null)
            {
                bordaValor = (decimal)ViewState["BordaValor"];
                ViewState["Preco"] = (decimal)ViewState["Preco"] + (bordaValor * Convert.ToInt32(ddlQtd.SelectedValue));
                PedidoAtualizado.idBorda = Convert.ToInt32(ddlBorda.SelectedValue);

            }

            if (txtObs.Text.Trim() != null)
                PedidoAtualizado.obs = txtObs.Text;

            PedidoAtualizado.valor = (decimal)ViewState["Preco"];

            PedidoAtualizado.idPedido =  (int)Session["Pedido"];

            PedidoAtualizado.idTipoProdutos = Convert.ToInt32(ddlTipoProdutos.SelectedValue);

            if (ddlTipoProdutos.SelectedValue == "1")
            {
                PedidoAtualizado.idOpcao = Convert.ToInt32(ddlOpcao.SelectedValue);
            }

            PedidoAtualizado.idDetPed = idDetPed;
            if(idDetPed!=0)
                PedidoAtualizado = pedido.atualizaPedido(PedidoAtualizado);

            cmdAtualizar.Visible = false;
            Response.Redirect("Pedidos2.aspx");


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
                NovoPedido.idBorda = Convert.ToInt32(ddlBorda.SelectedValue);

            }

            if (txtObs.Text.Trim() != null)
                NovoPedido.obs = txtObs.Text;

            NovoPedido.valor = (decimal)ViewState["Preco"];


            PedidosBLL pedido = new PedidosBLL();
            NovoPedido = pedido.adicionaPedido(NovoPedido);

            lstPedido.Add(NovoPedido);

            Session["Pedido"] = NovoPedido.idPedido;
            Session["NovoPedido"] = lstPedido;

            cmdAtualizar.Visible = false;
            Response.Redirect("Pedidos2.aspx");


        }

        private bool validaForm()
        {
            bool retorno = true;
            if (ddlTipoProdutos.SelectedValue == "")
                retorno = false;
            if (ddlSabor1.SelectedValue == "")
                retorno = false;
            if ((ddlQtd.SelectedValue == "0") || (ddlQtd.SelectedValue == ""))
                retorno = false;
            if (fancyCheckBox.Checked  == true)
                if (ddlBorda.SelectedValue == "")
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
                if(ddlQtd.SelectedValue  != "0")
                    carregarBorda();

                ddlOpcao.Visible = true;
                //divBorda.Visible = true;
            }
            else
            {
                divBorda.Visible = false;
                if(ddlTipoProdutos.SelectedValue !="")
                    carregaSabor(Convert.ToInt32(ddlTipoProdutos.SelectedValue),0);
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

            if (ddlOpcao.Visible)
            {
                this.ddlOpcao.Items.Clear();
                this.ddlOpcao.Visible = false;
            }


            this.ddlQtd.Items.Clear();
            this.ddlQtd.Visible = false;
            ViewState["Sabores"] = null;
            escondeBordas();
            txtObs.Text = "";

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

        private void carregaOpcao(int idOpcao)
        {
            List<OpcaoProduto_VO> opcao = new OpcaoBLL().obterTipos();

            this.ddlOpcao.Items.Clear();
            this.ddlOpcao.Items.Add(new ListItem("Selecione a opção", ""));
            int i = 1;

            if (opcao != null)
            {
                foreach (OpcaoProduto_VO opc in opcao)
                {
                    this.ddlOpcao.Items.Add(new ListItem(opc.Opcao, opc.idOpcao.ToString()));
                    if (opc.idOpcao == idOpcao)
                        ddlOpcao.SelectedIndex = i;

                    i++;
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
                carregaSabor(ddlSabor1,0);
                ViewState["Sabores"] = 1;
            }
            if (ddlOpcao.SelectedValue == "2")
            {
                carregaSabor(ddlSabor1,0);
                carregaSabor(ddlSabor2,0);
                ViewState["Sabores"] = 2;

            }
            if (ddlOpcao.SelectedValue == "3")
            {
                carregaSabor(ddlSabor1,0);
                carregaSabor(ddlSabor2,0);
                carregaSabor(ddlSabor3,0);
                ViewState["Sabores"] = 3;

            }
            fancyCheckBox.Checked = false;
            //ddlBorda.SelectedIndex = 0;
            if (ddlTipoProdutos.SelectedValue == "1")
            {
                //carregarBorda();
                //ddlBorda.Visible = true;
            }
            ViewState["BordaValor"] = null;

            if(ddlQtd.SelectedIndex > 0)
                ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newSucess('R$ 0,00');", true);


        }

        private void carregaSabor(DropDownList ddl, int idSabor)
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
                    if (sabor.idProduto == idSabor)
                        ddl.SelectedValue = sabor.idProduto.ToString();
                }
            }

            carregarQuantidade();

        }

        private void carregaSabor(int IdSabores,int idSabor)
        {
            ddlSabor1.Visible = true;

            List<Produto_VO> lstSabores = new ProdutoBLL().obterSabores(Convert.ToInt32(IdSabores));

            ddlSabor1.Items.Clear();
            ddlSabor1.Items.Add(new ListItem("Selecione o sabor", ""));
            int i = 1;

            if (lstSabores != null)
            {
                foreach (Produto_VO sabor in lstSabores)
                {
                    ddlSabor1.Items.Add(new ListItem(sabor.nome + " - R$ " + sabor.valor, sabor.idProduto.ToString()));
                    if (sabor.idProduto== idSabor)
                        ddlSabor1.SelectedIndex = i;
                    i++;
                }
            }
        }

        protected void ddlQtd_SelectedIndexChanged(object sender, EventArgs e)
        {

            // if((ddlQtd.SelectedValue != "0" ) && (ddlTipoProdutos.SelectedValue == "1"))
            // {
            //fancyCheckBox.Checked = false;
            //divBorda.Visible = true;
            //ddlBorda.SelectedIndex = 0;
            //carregarBorda();

            int qtdSabores = 0;
            int qtd = 0;

            if ((ddlSabor1.SelectedValue != "") && (ddlTipoProdutos.SelectedValue != "1"))
                ViewState["Sabores"] = 1;

            if (ViewState["Sabores"] != null)
                qtdSabores = (int)ViewState["Sabores"];




            decimal valor = ObterValor(qtdSabores);
            decimal total = 0;
            decimal valorBorda = 0;
            if (ddlQtd.SelectedValue != "")
                qtd = Convert.ToInt32(ddlQtd.SelectedValue);
            else
                return;
            total = (qtd * valor);

            if (ViewState["BordaValor"] != null)
                valorBorda = qtd * (decimal)ViewState["BordaValor"];

            ViewState["Preco"] = total;

            total += valorBorda;


            ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newSucess('R$ " + total + "');", true);

            //}

            if ((ddlQtd.SelectedValue != "0") && (ddlTipoProdutos.SelectedValue == "1"))
            {
                carregarBorda();

                fancyCheckBox.Checked = false;
                divBorda.Visible = true;
                ddlBorda.SelectedIndex = 0;
            }



        }

        protected void ddlSabor_SelectedIndexChanged(object sender, EventArgs e)
        {
            carregarQuantidade();
            if ((ddlSabor1.SelectedValue != "") && (ddlTipoProdutos.SelectedValue != "1"))
                ViewState["Sabores"] = 1;

        }

        private decimal ObterValor(int qtdSabores)
        {
            if ((qtdSabores != 0))
            {
                if (qtdSabores == 1)
                {
                    if (ddlSabor1.SelectedValue != "")
                        return new ProdutoBLL().obterValor(Convert.ToInt32(ddlSabor1.SelectedValue), 0, 0);
                    else
                        ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newAlert('Preencha todos os sabores!');", true);
                }

                if (qtdSabores == 2)
                {
                 if ((ddlSabor1.SelectedValue != "") && (ddlSabor2.SelectedValue != ""))
                    return new ProdutoBLL().obterValor(Convert.ToInt32(ddlSabor1.SelectedValue), Convert.ToInt32(ddlSabor2.SelectedValue), 0);
                 else
                    ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newAlert('Preencha todos os sabores!');", true);
                }

                if (qtdSabores == 3)
                {
                 if ((ddlSabor1.SelectedValue != "") && (ddlSabor2.SelectedValue != "") && (ddlSabor3.SelectedValue != ""))
                    return new ProdutoBLL().obterValor(Convert.ToInt32(ddlSabor1.SelectedValue), Convert.ToInt32(ddlSabor2.SelectedValue), Convert.ToInt32(ddlSabor3.SelectedValue));
                 else
                    ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newAlert('Preencha todos os sabores!');", true);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newAlert('Preencha todos os sabores!');", true);
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
                decimal valorBorda = 0;

                    //if (ViewState["BordaValor"] != null)
                        //valorBorda = (decimal)ViewState["BordaValor"] * Convert.ToInt32(ddlQtd.SelectedValue);
                    //else
                        ViewState["BordaValor"] = null;


                //ViewState["Preco"] = total;
                //total += valorBorda;

                //decimal valorBorda = (decimal)ViewState["BordaValor"];
                decimal pedido = (decimal)ViewState["Preco"];
                decimal pedidoTotal = pedido; //- valorBorda;

                ViewState["Preco"] = pedidoTotal;

                ddlBorda.Visible = false;
                ddlBorda.SelectedIndex = 0;
                ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "newSucess('R$ " + pedidoTotal + "');", true);


            }
        }

        protected void ddlBorda_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlBorda.SelectedIndex > 0)
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
                if (qtd > 0)
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

        protected void gvListaPedidos_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void escondeBordas()
        {
            this.ddlBorda.Items.Clear();
            ddlBorda.Visible = false;
            fancyCheckBox.Checked = false;
            divBorda.Visible = false;
            ViewState["BordaValor"] = null;
        }

        protected void escondeQtd()
        {
            this.ddlQtd.Items.Clear();
            ddlQtd.Visible = false;
        }

        protected void escondeSabor(DropDownList ddlSabor)
        {
            ddlSabor.Items.Clear();
            ddlSabor.Visible = false;
        }



        protected void lnkEditar_Click(object sender, CommandEventArgs e)
        {
            //int detPedido = 
            //ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message1", "return confirmEdit(this);", true);

            


        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(updPainel1, updPainel1.GetType(), "message", "confirmDelete("+sender+");", true);

        }
    }
}
using BLL;
using BLL.Produto;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Produtos
{
    public partial class EditarProdutos : BaseWebFormAutenticado
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            //configura o titulo da pagina
            this.Title = this.Title + " - Editar Produtos";

            if (!this.IsPostBack)
            {
                //Verifico a permissao
                AutenticacaoBLL.checaPermissao();

                //Carrego os tipos dos produtos
                carregarTiposProdutos();

                // edicao de dados?
                if (Request["idProduto"] != null)
                {
                    int codigo = 0;
                    int.TryParse(Request["idProduto"].ToString(), out codigo);

                    if (codigo > 0)
                    {
                        this.obterProduto(codigo);

                    }

                }
            }
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


        private void obterProduto(int codigo)
        {
            Produto_VO Produto = new ProdutoBLL().obterRegistro(codigo);
            if (Produto != null)
            {
                ViewState.Add("idProduto", codigo);

                txtNome.Text = Produto.nome;
                txtValor.Text = Produto.valor.ToString();
                txtIngredientes.Text = Produto.ingredientes;
                cbxAtivo.Visible = true;
                cbxAtivo.Checked = Produto.ativo == true ? true : false;

                // carregar os tipos
                this.carregarTiposProdutos(Produto.idTipoProduto);

            }
        }

        /// <summary>
        /// Ao clicar em salvar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdSalvar_Click(object sender, EventArgs e)
        {
            if (validarFormulario())
            {
                this.salvar();
            }
        }

        private bool validarFormulario()
        {

            if (ViewState["idProduto"] == null)
            {
                Produto_VO produto = new Produto_VO();
                string tipo = txtNome.Text;
                int idTipo = Convert.ToInt32(ddlTipoProdutos.SelectedValue);

                bool verifica = new ProdutoBLL().verificaProduto(tipo, idTipo);

                //caso exista registro, mostro a mensagem
                if (verifica)
                {
                    ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Produto já cadastrado!');", true);
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Salvar o registro.
        /// </summary>
        private void salvar()
        {
            Produto_VO produto = new Produto_VO();
            produto.nome = txtNome.Text.Trim();
            produto.idTipoProduto = Convert.ToInt32(ddlTipoProdutos.SelectedValue.ToString());
            produto.valor = Convert.ToDecimal(txtValor.Text);
            produto.ingredientes = txtIngredientes.Text;

            try
            {

                if (ViewState["idProduto"] != null)
                {
                    produto.ativo = cbxAtivo.Checked;
                    produto.idProduto = Convert.ToInt32(ViewState["idProduto"].ToString());
                    new ProdutoBLL().alterar(produto);
                }
                else
                {
                    produto.ativo = true;
                    new ProdutoBLL().salvar(produto);
                }
            }
            catch (Framework.Erros.Excecao ex)
            {
                this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                return;
            }

            Response.Redirect("Produtos.aspx");

        }

        /// <summary>
        /// Ao clicar em voltar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Produtos.aspx");
        }

    }
}
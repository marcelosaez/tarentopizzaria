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
    public partial class EditarProdutoTipos : BaseWebFormAutenticado
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            //configura o titulo da pagina
            this.Title = this.Title + " - Editar Tipo Produtos";

            if (!this.IsPostBack)
            {
                //Verifico a permissao
                AutenticacaoBLL.checaPermissao();

                // edicao de dados?
                if (Request["idTipoProduto"] != null)
                {
                    int codigo = 0;
                    int.TryParse(Request["idTipoProduto"].ToString(), out codigo);

                    if (codigo > 0)
                    {
                        this.obterTipoProduto(codigo);

                    }

                }
            }
        }

        private void obterTipoProduto(int codigo)
        {
            TipoProduto_VO tipoProduto = new ProdutoTipoBLL().obterRegistro(codigo);
            if (tipoProduto != null)
            {
                ViewState.Add("idTipoProduto", codigo);
                txtTipo.Text = tipoProduto.tipo;
                cbxAtivo.Visible = true;
                cbxAtivo.Checked = tipoProduto.ativo == true ? true : false;
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

            if (ViewState["idTipoProduto"] == null)
            {
                TipoProduto_VO tipoProduto = new TipoProduto_VO();
                string tipo = txtTipo.Text;

               bool verifica = new ProdutoTipoBLL().verificaTipoProduto(tipo);
                
                //caso exista registro, mostro a mensagem
                if (verifica)
                {
                    ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Tipo de Produto já cadastrado!');", true);
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
            TipoProduto_VO tipoProduto = new TipoProduto_VO();
            tipoProduto.tipo = txtTipo.Text.Trim();

            try
            {

                if (ViewState["idTipoProduto"] != null)
                {
                    tipoProduto.ativo = cbxAtivo.Checked;
                    tipoProduto.idTipoProduto = Convert.ToInt32(ViewState["idTipoProduto"].ToString());
                    new ProdutoTipoBLL().alterar(tipoProduto);
                }
                else
                {
                    tipoProduto.ativo = true;
                    new ProdutoTipoBLL().salvar(tipoProduto);
                }
            }
            catch (Framework.Erros.Excecao ex)
            {
                this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                return;
            }

            Response.Redirect("Tipos.aspx");

        }

        /// <summary>
        /// Ao clicar em voltar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tipos.aspx");
        }

    }
}
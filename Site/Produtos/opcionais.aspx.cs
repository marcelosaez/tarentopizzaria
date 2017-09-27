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
    public partial class opcionais : BaseWebFormAutenticado
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            //configura o titulo da pagina
            this.Title = this.Title + " - Editar Opcionais";

            if (!this.IsPostBack)
            {
                //Verifico a permissao
                AutenticacaoBLL.checaPermissao();

                // edicao de dados?
                if (Request["idOpcional"] != null)
                {
                    int codigo = 0;
                    int.TryParse(Request["idOpcional"].ToString(), out codigo);

                    if (codigo > 0)
                    {
                        this.obterOpcional(codigo);

                    }

                }
            }
        }

        private void obterOpcional(int codigo)
        {
            Opcional_VO Produto = new ProdutoBLL().obterRegistroOpcional(codigo);
            if (Produto != null)
            {
                ViewState.Add("idOpcional", codigo);

                txtNome.Text = Produto.nome;
                txtValor.Text = Produto.valor.ToString();
                cbxAtivo.Visible = true;
                cbxAtivo.Checked = Produto.ativo == true ? true : false;
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

            if (ViewState["idOpcional"] == null)
            {
                Produto_VO produto = new Produto_VO();
                string tipo = txtNome.Text;
                bool verifica = new ProdutoBLL().verificaOpcional(tipo);

                //caso exista registro, mostro a mensagem
                if (verifica)
                {
                    ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Opcional já cadastrado!');", true);
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
            Opcional_VO opcional = new Opcional_VO();
            opcional.nome = txtNome.Text.Trim();
            opcional.valor = Convert.ToDecimal(txtValor.Text);

            try
            {

                if (ViewState["idOpcional"] != null)
                {
                    opcional.ativo = cbxAtivo.Checked;
                    opcional.idOpcional = Convert.ToInt32(ViewState["idOpcional"].ToString());
                    new ProdutoBLL().alterarOpcional(opcional);
                }
                else
                {
                    opcional.ativo = true;
                    new ProdutoBLL().salvarOpcional(opcional);
                }
            }
            catch (Framework.Erros.Excecao ex)
            {
                this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                return;
            }

            Response.Redirect("OpcionaisMain.aspx");

        }

        /// <summary>
        /// Ao clicar em voltar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("OpcionaisMain.aspx");
        }
    }
}
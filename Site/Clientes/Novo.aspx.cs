using BLL;
using BLL.Cliente;
using MODEL.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Site.Clientes
{
    public partial class Novo : BaseWebFormAutenticado
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            //configura o titulo da pagina
            this.Title = this.Title + " - Clientes";

            if (!this.IsPostBack)
            {
                
                //Verifico a permissao
                //AutenticacaoBLL.checaPermissao();

                // edicao de dados?
                if (Request["idCliente"] != null)
                {
                    int codigo = 0;
                    int.TryParse(Request["idCliente"].ToString(), out codigo);

                    if (codigo > 0)
                    {
                        this.obterCliente(codigo);

                    }

                }
            }
        }

        private void obterCliente(int codigo)
        {
            Cliente_VO cliente = new ClienteBLL().obterRegistro(codigo);
            if (cliente != null)
            {
                ViewState.Add("idCliente", codigo);


                txtNome.Text = cliente.nome;
                txtEndereco.Text = cliente.endereco;
                txtNumero.Text = cliente.numero.ToString();
                txtEmail.Text = cliente.email;
                cbxAtivo.Visible = true;
                cbxAtivo.Checked = cliente.ativo == true ? true : false;

                //Verifico o telefone
                if (cliente.telres != 0)
                {
                    txtDDDRES.Text = cliente.dddres.ToString();
                    txtTELRES.Text = cliente.telres.ToString();
                }

                if (cliente.cel != 0)
                {
                    txtDDDCEL.Text = cliente.dddcel.ToString();
                    txtCEL.Text = cliente.cel.ToString();
                }

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

            if (ViewState["idCliente"] == null)
            {
                Cliente_VO cliente = new Cliente_VO();

                int telres = 0;
                int.TryParse(txtTELRES.Text.Trim(), out telres);

                int cel = 0;
                int.TryParse(txtCEL.Text.Trim(), out cel);

                if ((telres == 0) && (cel == 0))
                {
                    ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Por favor digite ao menos um telefone!');", true);
                    return false;
                }

                if ((telres != 0) || (cel != 0))
                    cliente = new ClienteBLL().checaCadastro(telres, cel);

                if (cliente.temCadastro)
                {
                    ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Cadastro já realizado para o cliente " + cliente.nome + "');", true);
                    return false;
                }
            }
            else
            {
                if ((txtTELRES.Text.Trim() == "") && (txtCEL.Text.Trim() == ""))
                {
                    ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Por favor digite ao menos um telefone!');", true);
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
            Cliente_VO cliente = new Cliente_VO();
            cliente.nome = txtNome.Text.Trim();
            cliente.endereco = txtEndereco.Text.Trim();
            cliente.numero = Convert.ToInt32(txtNumero.Text.Trim().ToString());
            cliente.email = txtEmail.Text.Trim();
            cliente.ativo = true;
            var telr = "";
            var telc = "";
            //Verifico o telefone
            if (txtTELRES.Text.Trim() != "")
            {
                cliente.dddres = Convert.ToInt32(txtDDDRES.Text.Trim());
                telr = txtTELRES.Text.Trim().Replace("-", "").Trim();
                cliente.telres = Convert.ToInt32(telr.Trim());
            }

            if (txtCEL.Text.Trim() != "")
            {
                cliente.dddcel = Convert.ToInt32(txtDDDCEL.Text.Trim());
                telc = txtCEL.Text.Trim().Replace("-", "").Trim();
                cliente.cel = Convert.ToInt32(telc);
            }

            try
            {

                if (ViewState["idCliente"] != null)
                {
                    cliente.ativo = cbxAtivo.Checked;
                    cliente.idCliente = Convert.ToInt32(ViewState["idCliente"].ToString());
                    new ClienteBLL().alterar(cliente);
                }
                else
                {
                    new ClienteBLL().salvar(cliente);
                    ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Cadastro incluído com sucesso!');", true);
                    limparCampos();
                }
            }
            catch (Framework.Erros.Excecao ex)
            {
                this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                return;
            }

           // ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('success', 'Cadastro incluído com sucesso!');", true);
            //Response.Redirect("Default.aspx");

        }

        /// <summary>
        /// Ao clicar em voltar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdLimpar_Click(object sender, EventArgs e)
        {
            limparCampos();
        }

        private void limparCampos()
        {
            txtNome.Text = "";
            txtEndereco.Text = "";
            txtEmail.Text = "";
            //txtDDDCEL.Text = "";
            txtCEL.Text = "";
            //txtDDDRES.Text = "";
            txtTELRES.Text = "";
            txtNumero.Text = "";
        }
    }
}
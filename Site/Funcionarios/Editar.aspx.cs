using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MODEL.Funcionario;
using BLL.Funcionario;
using BLL;

namespace Site.Funcionarios
{
    public partial class Editar : BaseWebFormAutenticado
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            //configura o titulo da pagina
            this.Title = this.Title + " - Funcionários";

            if (!this.IsPostBack)
            {
                //Verifico a permissao
                AutenticacaoBLL.checaPermissao();
                
                // edicao de dados?
                if (Request["idFuncionario"] != null)
                {
                    int codigo = 0;
                    int.TryParse(Request["idFuncionario"].ToString(), out codigo);

                    if (codigo > 0)
                    {
                        this.obterRegistro(codigo);
                        
                    }
                        
                }
                else
                  // carregar os tipos
                  this.obterTipos();
            }
        }

        private void obterRegistro(int codigo)
        {
            Funcionario_VO registro = new FuncionarioBLL().obterRegistro(codigo);
            if (registro != null)
            {
                ViewState.Add("idFuncionario", codigo);

                txtNome.Text = registro.nome;
                txtLogin.Text = registro.login;
                //txtSenha.Text = registro.senha;
                txtSenha.Attributes.Add("value", registro.senha);
                txtEmail.Text = registro.email;
                cbxAtivo.Visible = true;
                cbxAtivo.Checked = registro.ativo == true ? true : false;

                // carregar os tipos
                this.obterTipos(registro.tipoID);

            }
        }

        private void obterTipos(int tipoID)
        {
            List<TipoFuncionario_VO> funcionarios = new FuncionarioBLL().obterTipos();

            this.ddlTipos.Items.Clear();
            this.ddlTipos.Items.Add(new ListItem("Selecione o tipo", ""));

            if (funcionarios != null)
            {
                foreach (TipoFuncionario_VO tpFun in funcionarios)
                {
                    this.ddlTipos.Items.Add(new ListItem(tpFun.tipo, tpFun.tipoID.ToString()));

                    if (tpFun.tipoID == tipoID)
                        ddlTipos.SelectedIndex = tipoID;
                }
            }
        }

        private void obterTipos()
        {
            List<TipoFuncionario_VO> funcionarios = new FuncionarioBLL().obterTipos();

            this.ddlTipos.Items.Clear();
            this.ddlTipos.Items.Add(new ListItem("Selecione o tipo", ""));

            if (funcionarios != null)
            {
                foreach (TipoFuncionario_VO tpFun in funcionarios)
                {
                    this.ddlTipos.Items.Add(new ListItem(tpFun.tipo, tpFun.tipoID.ToString()));
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
            //this.validarFormulario();

            if (validarFormulario())
            {
                this.salvar();
            }
        }

        private bool validarFormulario()
        {

            //var idFuncionario = (int)ViewState["idFuncionario"];

            if (ViewState["idFuncionario"] == null)
            {
                var login = txtLogin.Text.Trim();
                bool existeLogin = new FuncionarioBLL().checaLogin(login);
                if (existeLogin)
                {
                    ClientScript.RegisterStartupScript(GetType(), "message", "newAlert('alert alert-danger', 'Login já utilizado!');", true);
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
            Funcionario_VO registro = new Funcionario_VO();
            registro.tipoID =  Convert.ToInt32(this.ddlTipos.SelectedValue);
            registro.nome = txtNome.Text.Trim();
            registro.login = txtLogin.Text.Trim();
            registro.senha = txtSenha.Text.Trim();
            registro.email = txtEmail.Text.Trim();

            registro.ativo = true;

            try
            {
                
                if (ViewState["idFuncionario"] != null)
                {
                    registro.ativo = cbxAtivo.Checked;
                    registro.id = Convert.ToInt32(ViewState["idFuncionario"].ToString());
                    new FuncionarioBLL().alterar(registro);
                }
                else
                {
                    new FuncionarioBLL().salvar(registro);
                }
            }
            catch (Framework.Erros.Excecao ex)
            {
                this.Page.Validators.Add(new Framework.Validacao.CustomError(ex.Message));
                return;
            }

            Response.Redirect("Default.aspx");
            
        }

        /// <summary>
        /// Ao clicar em voltar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }




    }
}
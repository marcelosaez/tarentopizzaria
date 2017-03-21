using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using MODEL;
using System.Web;
using MODEL.Funcionario;

namespace BLL
{
    public class AutenticacaoBLL
    {
        public static Funcionario_VO autenticar(string login, string senha)
        {
            Funcionario_VO usuario = new Funcionario_VO();

            usuario = new AutenticacaoDAL().autenticarUsuario(login,senha);

            if (usuario != null)
            {
                if (usuario.autenticado)
                {
                    HttpContext.Current.Session["Usuario"] = usuario;
                }
                else
                {
                    //throw new Framework.Erros.Excecao("O seu usuário não está habilitado para utilizar o sistema!");
                    usuario.autenticado = false;
                }
            }

            return usuario;

        }

        public static bool obterAcessoAdministrador(Funcionario_VO usuario)
        {
            if (usuario.tipo.ToLower() == "administrador")
                return true;
            else
                return false;
        }

        public static void finalizarSessao()
        {
            HttpContext.Current.Session.Abandon();
        }

        public static bool estaAutenticado()
        {
            if (HttpContext.Current.Session["Usuario"] == null)
                return false;
            else
                return true;
        }

        public static Funcionario_VO obterUsuarioAutenticado()
        {
           
            if (HttpContext.Current.Session["Usuario"] != null)
            {
                return (Funcionario_VO)HttpContext.Current.Session["Usuario"];
            }
            else
            {
                return null;
            }
        
        }

        public static void checaPermissao()
        {
            Funcionario_VO usuario = AutenticacaoBLL.obterUsuarioAutenticado();
            //Verifico a permissao
            if (usuario != null)
            {
                bool obterAcesso = AutenticacaoBLL.obterAcessoAdministrador(usuario);
                //Redireciono
                if (!obterAcesso)
                    HttpContext.Current.Response.Redirect("~/Main.aspx?permissao=N");
            }
        }

}
}

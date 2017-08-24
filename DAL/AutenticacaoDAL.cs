using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL;
using MODEL.Funcionario;

namespace DAL
{
    public class AutenticacaoDAL : Contexto
    {
        private Contexto contexto;

        public Funcionario_VO autenticarUsuario(string login, string senha)
        {
            Funcionario_VO usuario = null;
            var strQuery = "SELECT f.idtb_funcionario as id, nome, login, email, ativo, tipo from tb_funcionarios f ";
            strQuery += " inner join tb_tipoFuncionario tu ";
            strQuery += " on f.tb_tipoFuncionario_idtb_tipoFuncionario = tu.idtb_tipoFuncionario ";
            strQuery += " WHERE login = '" + login +"'"; 
            strQuery += " and senha = '" + senha + "'";
            strQuery += " and f.ativo = 1 ";
            //usuario.autenticado = false;


            using (contexto = new Contexto())
            {
                var retornoDataReader = contexto.executaComandoComRetorno(strQuery);

                while (retornoDataReader.Read())
                {
                    usuario = new Funcionario_VO();
                    var user = new Funcionario_VO()
                    {
                        id = int.Parse(retornoDataReader["id"].ToString()),
                        nome = retornoDataReader["nome"].ToString(),
                        login = retornoDataReader["login"].ToString(),
                        email = retornoDataReader["email"].ToString(),
                        tipo = retornoDataReader["tipo"].ToString(),
                        autenticado = true

                    };
                    usuario= user;

                }
                retornoDataReader.Close();
            }

            return usuario;
        }
    }
}

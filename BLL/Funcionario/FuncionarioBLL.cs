using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Funcionario;
using MODEL;
using MODEL.Funcionario;

namespace BLL.Funcionario
{
    public class FuncionarioBLL
    {
        public DataSet obterDataSet()
        {
            return new FuncionarioDAL().obterDataSet();
        }

        public List<TipoFuncionario_VO> obterTipos()
        {
            return new FuncionarioDAL().obterTipos();
        }

        public void alterar(Funcionario_VO registro)
        {
            new FuncionarioDAL().alterar(registro);
        }

        public void salvar(Funcionario_VO registro)
        {
            new FuncionarioDAL().salvar(registro);
        }

        public bool checaLogin(string login)
        {
            return new FuncionarioDAL().checaLogin(login);
        }

        public Funcionario_VO obterRegistro(int codigo)
        {
            return new FuncionarioDAL().obterRegistro(codigo);
        }
    }
}

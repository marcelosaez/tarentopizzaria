using DAL.Empresa;
using MODEL.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Empresa
{
    public class EmpresaBLL
    {
        public Empresa_VO obterDadosEmpresa(int idEmpresa)
        {
            return new EmpresaDAL().obterDadosEmpresa(idEmpresa);
        }

        
    }
}

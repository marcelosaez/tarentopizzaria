using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Funcionario
{
    public class TipoFuncionario_VO
    {
 
            public int tipoID { get; set; }
            public string tipo { get; set; }
            public List<TipoFuncionario_VO> lstTipoFuncionario { get; set; }

     }
}

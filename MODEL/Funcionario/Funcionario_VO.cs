using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Funcionario
{
    public class Funcionario_VO
    {
       
            public int id { get; set; }
            public int tipoID { get; set; }
            public string nome { get; set; }
            public string login { get; set; }
            public string senha { get; set; }
            public string email { get; set; }
            public string tipo { get; set; }
            public bool ativo { get; set; }
            public List<Funcionario_VO> lstFuncionario { get; set; }
            public bool autenticado { get; set; }

    }
}

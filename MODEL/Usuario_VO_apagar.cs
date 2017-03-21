using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class Usuario_VO_apagar
    {
        public int id { get; set; }
        public int tipoID { get; set; }
        public string nome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public string email { get; set; }
        public string tipo { get; set; }
        public bool ativo { get; set; }
        public List<Usuario_VO_apagar> lstUsuario { get; set; }
        public bool autenticado { get; set; }

    }
}

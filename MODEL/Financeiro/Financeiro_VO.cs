using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Financeiro
{
    public class Financeiro_VO
    {
        public string tipo { get; set; }
        public decimal valorTotal { get; set; }
        public decimal qtdTotal { get; set; }
        public DateTime DataIni { get; set; }
    }
}

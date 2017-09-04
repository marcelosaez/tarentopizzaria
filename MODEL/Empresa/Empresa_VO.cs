using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Empresa
{
    public class Empresa_VO
    {
        public int idEmpresa { get; set; }
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string Endereco { get; set; }
        public int Numero { get; set; }
        public string Telefone { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}

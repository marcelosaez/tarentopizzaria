﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Produto
{
    public class Opcional_VO
    {
        public string label { get; set; }
        public bool temCadastro { get; set; }
        public int idOpcional { get; set; }
        public string nome { get; set; }
        public decimal valor { get; set; }
        public bool ativo { get; set; }
    }
}

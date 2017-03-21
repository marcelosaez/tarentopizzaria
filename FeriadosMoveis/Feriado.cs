using System;
using System.Collections.Generic;
using System.Text;

namespace ClassFeriados
{
    public class Feriado
    {
        public DateTime Data;
        public string Descricao;
        public string Dt { get; set; }
        public string Desc { get; set; }
        public List<Feriado> lstFeriado { get; set; }
        public string DiaSemana { get; set; }

        public Feriado(DateTime DataFeriado, string Descricao)
        {
            this.Data = DataFeriado;
            this.Descricao = Descricao;
        }

       
           
        
    }
}

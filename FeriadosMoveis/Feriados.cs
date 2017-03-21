using System;
using System.Collections.Generic;
using System.Text;

namespace ClassFeriados
{
    public class Feriados
    {
        public List<Feriado> _feriados = new List<Feriado>();

        /// <summary>
        /// INICIALIZA A CLASSE FERIADOS COM O ANO CORRENTE
        /// </summary>
        public Feriados()
        {
            int Ano = DateTime.Now.Year;
            GeraListaFeriados(Ano);
        }

        /// <summary>
        /// INICIALIZA A CLASSE FERIADOS COM O ANO DADO
        /// </summary>
        public Feriados(int Ano)
        {
            GeraListaFeriados(Ano);
        }

        private void GeraListaFeriados(int ano)
        {            
            FeriadosMoveis fm = new FeriadosMoveis(ano);

            //ADICIONE AQUI OS FERIADOS PARA SUA CIDADE OU ESTADO SE NECESSÁRIO

            _feriados.Add(new Feriado(fm.DiaPascoa, "Domingo de Páscoa"));
            _feriados.Add(new Feriado(DateTime.Parse("01/01/" + ano), "Confraternização Universal"));
            _feriados.Add(new Feriado(fm.SegundaCarnaval, "Segunda Carnaval"));
            _feriados.Add(new Feriado(fm.TercaCarnaval, "Terça Carnaval"));
            _feriados.Add(new Feriado(fm.SextaPaixao, "Sexta feira da paixão"));
            _feriados.Add(new Feriado(DateTime.Parse("21/04/" + ano), "Tiradentes"));
            _feriados.Add(new Feriado(DateTime.Parse("01/05/" + ano), "Dia do trabalho")); 
            _feriados.Add(new Feriado(fm.CorpusChristi, "Corpus Christi"));
            _feriados.Add(new Feriado(DateTime.Parse("07/09/" + ano), "Independência do Brasil"));
            _feriados.Add(new Feriado(DateTime.Parse("12/10/" + ano), "Padroeira do Brasil"));
            _feriados.Add(new Feriado(DateTime.Parse("02/11/" + ano), "Finados"));
            _feriados.Add(new Feriado(DateTime.Parse("15/11/" + ano), "Proclamação da República"));
            _feriados.Add(new Feriado(DateTime.Parse("25/12/" + ano), "Natal"));
        }

        public bool IsDiaUtil(DateTime data)
        {
            if (IsFimDeSemana(data) || IsFeriado(data))
                return false;
            else
                return true;
        }

        public bool IsFeriado(DateTime data)
        {
            return _feriados.Find(delegate(Feriado f1) { return f1.Data == data; }) != null ? true : false;            
        }

        public bool IsFimDeSemana(DateTime data)
        {
            if (data.DayOfWeek == DayOfWeek.Sunday || data.DayOfWeek == DayOfWeek.Saturday)
                return true;
            else
                return false;
        }

        public DateTime ProximoDiaUtil(DateTime data)
        {
            DateTime auxData = data;

            if (IsFeriado(auxData) || IsFimDeSemana(auxData))
            {
                auxData = data.AddDays(1);
                auxData = ProximoDiaUtil(auxData);
            }

            return auxData;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ClassFeriados
{
    class FeriadosMoveis
    {
        #region  Private Fields
        private DateTime _DomingoPascoa;
        private DateTime _DomingoCarnaval;
        private DateTime _SegundaCarnaval;
        private DateTime _TercaCarnaval;
        private DateTime _SextaPaixao;
        private DateTime _CorpusChristi;
        #endregion

        #region Properties
        public DateTime DiaPascoa { get { return _DomingoPascoa; } }
        public DateTime DomingoCarnaval { get { return _DomingoCarnaval; } }
        public DateTime SegundaCarnaval { get { return _SegundaCarnaval; } }
        public DateTime TercaCarnaval { get { return _TercaCarnaval; } }
        public DateTime SextaPaixao { get { return _SextaPaixao; } }
        public DateTime CorpusChristi { get { return _CorpusChristi; } }
        #endregion

        public FeriadosMoveis(int Ano)
        {
            _DomingoPascoa = CalculaDiaPascoa(Ano);
            _DomingoCarnaval = CalculaDomingoCarnaval(_DomingoPascoa);
            _SegundaCarnaval = CalculaSegundaCarnaval(_DomingoPascoa);
            _TercaCarnaval = CalculaTercaCarnaval(_DomingoPascoa);
            _SextaPaixao = CalculaSextaPaixao(_DomingoPascoa);
            _CorpusChristi = CalculaCorpusChristi(_DomingoPascoa);
        }
         /// <summary>
         ///  FUNÇÃO PARA CALCULAR A DATA DO DOMINGO DE PASCOA
         ///  DADO UM ANO QUALQUER
         /// </summary>
         /// <param name="AnoCalcular"></param>
         /// <returns></returns>
        private DateTime CalculaDiaPascoa(int AnoCalcular)
        {
            int x = 24;
            int y = 5;

            int a = AnoCalcular % 19;
            int b = AnoCalcular % 4;
            int c = AnoCalcular % 7;

            int d = (19 * a + x) % 30;
            int e = (2 * b + 4 * c + 6 * d + y) % 7;

            int dia = 0;
            int mes = 0;

            if (d + e > 9)
            {
                dia = (d + e - 9);
                mes = 4;
            }
            else
            {
                dia = (d + e + 22);
                mes = 3;
            }
            return DateTime.Parse(string.Format("{0},{1},{2}", AnoCalcular.ToString(), mes.ToString(), dia.ToString()));
        }


        private DateTime CalculaDomingoCarnaval(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-49);
        }

        private DateTime CalculaSegundaCarnaval(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-48);
        }

        private DateTime CalculaTercaCarnaval(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-47);
        }

        private DateTime CalculaSextaPaixao(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-2);
        }

        private DateTime CalculaCorpusChristi(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(+60);
        }
    }
}


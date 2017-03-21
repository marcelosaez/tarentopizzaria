using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassFeriados;
using static ClassFeriados.Feriado;

namespace Site.FeriadosM
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                montaCalendario();
        }

        private void montaCalendario()
        {
            int ano = Convert.ToInt32(DateTime.Now.Year.ToString());
            //ano = 2018;

            lblAno.Text = ano.ToString();

            Feriados fm = new Feriados(ano);
            List<Feriado> lista = fm._feriados;

            List<Feriado> lstFeriado = new List<Feriado>();

            foreach (Feriado f in lista)
            {
                Feriado registro = new Feriado(f.Data, f.Descricao);
                registro.Dt = f.Data.ToString("dd/MM/yyyy"); 
                registro.Desc = f.Descricao;
                registro.DiaSemana = f.Data.ToString("ddd");
                lstFeriado.Add(registro);
            }

            rptFeriados.DataSource = lstFeriado;
            rptFeriados.DataBind();
        }

        static DataTable GetTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Dia", typeof(string));
            table.Columns.Add("Feriado", typeof(string));
            table.Columns.Add("Dia da Semana", typeof(string));
            return table;
        }
    }
}
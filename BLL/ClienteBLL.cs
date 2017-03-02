using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using MODEL;


namespace ClienteBLL
{
    public class Cliente
    {
        public class TipoClienteBLL
        {
            public List<TipoCliente_VO> ListarClienteTipo()
            {
                
                List<TipoCliente_VO> lista = new List<TipoCliente_VO>();
                DAL.Cliente_DAL clDAL = new Cliente_DAL();

                lista = clDAL.ListarCliente();
                return lista;

            }

            public List<Cliente_VO> ListarCliente()
            {
                List<Cliente_VO> lista = new List<Cliente_VO>();
                DAL.Cliente_DAL clDAL = new Cliente_DAL();

                lista = clDAL.listarCliente();
                return lista;
            }

            public List<Cliente_VO> ListarTipoCliente(string tipo)
            {
                List<Cliente_VO> lista = new List<Cliente_VO>();
                DAL.Cliente_DAL clDAL = new Cliente_DAL();

                lista = clDAL.listarTipoCliente(tipo);
                return lista;
            }
        }
    }
}

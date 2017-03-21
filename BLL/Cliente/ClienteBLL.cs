using DAL.Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Cliente;

namespace BLL.Cliente
{
    public class ClienteBLL
    {
        public DataSet obterDataSet()
        {
            return new ClienteDAL().obterDataSet();
        }

        public bool checaLogin(string login)
        {
            throw new NotImplementedException();
        }

        public void alterar(Cliente_VO cliente)
        {
            new ClienteDAL().alterar(cliente);
        }

        public void salvar(Cliente_VO cliente)
        {
            new ClienteDAL().salvar(cliente);
        }

        public Cliente_VO checaCadastro(int telres, int cel)
        {
            return new ClienteDAL().checaCadastro(telres, cel);
           
        }

        public Cliente_VO obterRegistro(int codigo)
        {
            return new ClienteDAL().obterRegistro(codigo);
        }
    }
}

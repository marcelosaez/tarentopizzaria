using MODEL;
using MODEL.Funcionario;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Cliente_DAL : Contexto
    {

        private Contexto contexto;

        //Carrego o tipo de cliente
        public List<TipoCliente_VO> ListarCliente()
        {
            List<TipoCliente_VO> lista = new List<TipoCliente_VO>();
            var strQuery = "SELECT IdTipo, Descricao FROM tb_Tipo";
            using (contexto = new Contexto())
            {
                var retornoDataReader = contexto.executaComandoComRetorno(strQuery);

                while (retornoDataReader.Read())
                {
                    var tc = new TipoCliente_VO()
                    {
                        idTipo = int.Parse(retornoDataReader["IdTipo"].ToString()),
                        descricao = retornoDataReader["Descricao"].ToString()
                    };
                    lista.Add(tc);

                }
            }

            return lista;
        }
        /*
        public List<Funcionario_VO> listarTipoCliente(string tipo)
        {
            List<Cliente_VO> lista = new List<Cliente_VO>();
            var strQuery = "SELECT c.IdCliente, c.NomeCliente, c.Ativo, t.Descricao  FROM tb_Cliente c INNER JOIN tb_Tipo t ON c.TipoCliente=t.IdTipo WHERE c.TipoCliente = "+ int.Parse(tipo);
            using (contexto = new Contexto())
            {
                var retornoDataReader = contexto.executaComandoComRetorno(strQuery);

                while (retornoDataReader.Read())
                {
                    var cliente = new Cliente_VO()
                    {
                        idCliente = int.Parse(retornoDataReader["IdCliente"].ToString()),
                        NomeCliente = retornoDataReader["NomeCliente"].ToString(),
                        Descricao = retornoDataReader["Descricao"].ToString(),
                        Ativo = bool.Parse(retornoDataReader["Ativo"].ToString())
                    };
                    lista.Add(cliente);

                }
            }

            return lista;
        }

        //Listar clientes
        public List<Cliente_VO> listarCliente()
        {
            List<Cliente_VO> lista = new List<Cliente_VO>();
            var strQuery = "SELECT c.IdCliente, c.NomeCliente, c.Ativo, t.Descricao  FROM tb_Cliente c INNER JOIN tb_Tipo t ON c.TipoCliente=t.IdTipo ";
            using (contexto = new Contexto())
            {
                var retornoDataReader = contexto.executaComandoComRetorno(strQuery);

                while (retornoDataReader.Read())
                {
                    var cliente = new Cliente_VO()
                    {
                        idCliente = int.Parse(retornoDataReader["IdCliente"].ToString()),
                        NomeCliente = retornoDataReader["NomeCliente"].ToString(),
                        Descricao = retornoDataReader["Descricao"].ToString(),
                        Ativo = bool.Parse(retornoDataReader["Ativo"].ToString())
                    };
                    lista.Add(cliente);

                }
            }

            return lista;
        }*/
    }
}

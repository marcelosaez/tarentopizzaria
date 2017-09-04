using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Empresa;
using System.Data.SqlClient;
using System.Data;

namespace DAL.Empresa
{
    public class EmpresaDAL : Contexto
    {
        public Empresa_VO obterDadosEmpresa(int idEmpresa)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT  [idEmpresa] as id, [Nome],[NomeFantasia],[Endereco],[Numero],[Telefone],[Bairro],[CEP],[Cidade],[Estado] FROM tb_empresa  ");
            query.Append(" WHERE idEmpresa = " + idEmpresa);

            //executa
            SqlDataReader dr = executeDataReader(query.ToString(), CommandType.Text, false);

            Empresa_VO registro = new Empresa_VO();
            while (dr.Read())
            {
                
                registro.idEmpresa = Convert.ToInt32(dr["id"]);
                registro.Nome = Convert.ToString(dr["Nome"]);
                registro.NomeFantasia = Convert.ToString(dr["NomeFantasia"]);
                registro.Endereco = Convert.ToString(dr["Endereco"]);
                registro.Numero = Convert.ToInt32(dr["Numero"]);
                registro.Telefone = Convert.ToString(dr["Telefone"]);
                registro.Bairro = Convert.ToString(dr["Bairro"]);
                registro.CEP = Convert.ToString(dr["CEP"]);
                registro.Cidade = Convert.ToString(dr["Cidade"]);
                registro.Estado = Convert.ToString(dr["Estado"]);
                
            }

            //libera recursos
            dr.Dispose();
            dr.Close();

            return registro;
        }
    }
}

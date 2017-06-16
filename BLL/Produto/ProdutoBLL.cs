using DAL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Produto;

namespace BLL.Produto
{
    public class ProdutoBLL
    {
        public DataSet obterDataSet()
        {
            return new ProdutoDAL().obterDataSet();
        }

        public void alterar(Produto_VO Produto)
        {
            new ProdutoDAL().alterar(Produto);
        }

        public void salvar(Produto_VO Produto)
        {
            new ProdutoDAL().salvar(Produto);
        }

        public bool verificaProduto(string tipo, int idTipo)
        {
            return new ProdutoDAL().verificaProduto(tipo, idTipo);
        }

        public Produto_VO obterRegistro(int codigo)
        {
            return new ProdutoDAL().obterRegistro(codigo);
        }

        public List<Produto_VO> obterSabores(int idTipo)
        {
            return new ProdutoDAL().obterSabores(idTipo);

        }

        public decimal obterValor(int idSabor1, int idSabor2, int idSabor3)
        {
            string idSabor = "";

            idSabor += idSabor1;

            //verifico qual o maior valor 
            if (idSabor2 != 0)
                idSabor += "," + idSabor2;
            if (idSabor3 != 0)
                idSabor += "," + idSabor3;

            return new ProdutoDAL().obterValor(idSabor);
        }

        
        public List<BordaProduto_VO> obterTipos()
        {
            return new ProdutoDAL().obterBordas();
        }

        public List<BordaProduto_VO> obterBordas()
        {
            return new ProdutoDAL().obterBordas();
        }

        public decimal obterValorBorda(int idBorda)
        {
            return new ProdutoDAL().obterValorBorda(idBorda);
        }
    }
}

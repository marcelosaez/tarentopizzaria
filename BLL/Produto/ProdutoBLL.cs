using DAL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.Produto;
using MODEL.Pagamento;

namespace BLL.Produto
{
    public class ProdutoBLL
    {
        public DataSet obterDataSet()
        {
            return new ProdutoDAL().obterDataSet();
        }

        public DataSet obterDataSetOpcionais()
        {
            return new ProdutoDAL().obterDataSetOpcionais();
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

        public Opcional_VO obterRegistroOpcional(int codigo)
        {
            return new ProdutoDAL().obterRegistroOpcional(codigo);
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

        public bool verificaOpcional(string opcional)
        {
            return new ProdutoDAL().verificaOpcional(opcional);
        }

        public void alterarOpcional(Opcional_VO opcional)
        {
            new ProdutoDAL().alterarOpcional(opcional);
        }

        public void salvarOpcional(Opcional_VO opcional)
        {
            new ProdutoDAL().salvarOpcional(opcional);
        }
    }
}

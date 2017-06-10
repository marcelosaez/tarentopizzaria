using DAL.Produto;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Produto
{
    public class ProdutoTipoBLL
    {
        public DataSet obterDataSet()
        {
            return new ProdutoTipoDAL().obterDataSet();
        }

        public TipoProduto_VO obterRegistro(int codigo)
        {
            return new ProdutoTipoDAL().obterRegistro(codigo);
        }

        public void alterar(TipoProduto_VO tipoProduto)
        {
             new ProdutoTipoDAL().alterar(tipoProduto);
        }

        public void salvar(TipoProduto_VO tipoProduto)
        {
            new ProdutoTipoDAL().salvar(tipoProduto);
        }

        public bool verificaTipoProduto(string tipo)
        {
            return new ProdutoTipoDAL().verificaTipoProduto(tipo);
            
        }

        public List<TipoProduto_VO> obterTipos()
        {
            return new ProdutoTipoDAL().obterTipos();
        }
    }
}

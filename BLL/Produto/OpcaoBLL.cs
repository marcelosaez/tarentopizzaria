using DAL.Produto;
using MODEL.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Produto
{
    public class OpcaoBLL
    {
        public List<OpcaoProduto_VO> obterTipos()
        {
            return new ProdutoOpcaoDAL().obterTipos();
        }
    }
}

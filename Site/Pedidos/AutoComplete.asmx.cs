using BLL.Pedidos;
using MODEL.Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Site.Pedidos
{
    // <summary>
    /// Summary description for AutoComplete
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public class AutoComplete : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] AutoCompleteAjaxRequest(string prefixText, int count)
        {
            List<string> ajaxDataCollection = new List<string>();
            DataTable _objdt = new DataTable();
            List<Cliente_VO> clientes = new List<Cliente_VO>();

            clientes = new PedidosBLL().autoComplete(prefixText);
            
            var tel = "";

            foreach (var cliente in clientes)
            {
                tel = (!string.IsNullOrEmpty(cliente.dddcel.ToString()) && cliente.dddcel.ToString() != "0") ? cliente.dddcel.ToString() + ' ' + cliente.cel.ToString() : cliente.dddres.ToString() + ' ' + cliente.telres.ToString();

                //if((!string.IsNullOrEmpty(cliente.dddcel.ToString())) 
                //    &&((!string.IsNullOrEmpty(cliente.dddres.ToString()) 
                    if((cliente.dddres.ToString() != "0")
                    && ((cliente.dddcel.ToString()) != "0"))
                        tel = cliente.dddcel.ToString() + " "  + cliente.cel.ToString() + " / "+ cliente.dddres.ToString() + ' ' + cliente.telres.ToString();

                ajaxDataCollection.Add( cliente.idCliente + "  -  " + tel + "  -  " +  cliente.nome.ToString());
                //string item = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem( cliente.idCliente.ToString(), tel.ToString());
                //ajaxDataCollection.Add(item);
            }

            return ajaxDataCollection.ToArray();
        }

    }
}


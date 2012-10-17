using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kShop;


using umbraco.NodeFactory;
using umbraco.presentation.umbracobase.library;
using System.Web;

namespace kShopUmbraco
{
    class PaymentControllerManagerUmbraco : PaymentControllerManager
    {
        Node _node;
        public PaymentControllerManagerUmbraco(int id)
        {
            _node = new Node(id);
        }

        public void fill(PaymentController paymentHandlerController)
        {

            if (_node.GetProperty("dll") != null)
            {
                paymentHandlerController.dll = _node.GetProperty("dll").Value.ToString();
            }
            if (_node.GetProperty("namespace") != null)
            {
                paymentHandlerController.pluginNamespace = _node.GetProperty("namespace").Value.ToString();
            }
            if (_node.GetProperty("class") != null)
            {
                paymentHandlerController.pluginClass = _node.GetProperty("class").Value.ToString();
            }
            paymentHandlerController.id = "not_umb_id_" + _node.Id;

            string baseUrl = string.Format("http://{0}", HttpContext.Current.Request.Url.Host);
            //baseUrl = baseUrl.Substring(0, baseUrl.LastIndexOf("/"));
            paymentHandlerController.respondUrl = baseUrl + _node.NiceUrl;
        }
    }
}

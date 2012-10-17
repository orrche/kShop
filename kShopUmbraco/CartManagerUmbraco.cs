using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kShop;

using umbraco.businesslogic;
using umbraco.cms.businesslogic.web;

using System.Web;
using umbraco.BusinessLogic;
using umbraco.NodeFactory;


namespace kShopUmbraco
{
    public class CartManagerUmbraco : CartManager
    {
        Node _node = null;
        Cart _cart = null;

        public CartManagerUmbraco()
        {
            int docId = 0;

            if (HttpContext.Current.Items.Contains("kShopCart"))
            {
                if (HttpContext.Current.Items["kShopCart"] != null)
                {
                    _node = (Node)HttpContext.Current.Items["kShopCart"];
                }
            }
            else
            {
                if (HttpContext.Current.Request.Cookies["shopCartId"] != null && Int32.TryParse(System.Web.HttpContext.Current.Request.Cookies["shopCartId"].Value.ToString(), out docId))
                {
                    _node = new Node(docId, true);
                    HttpContext.Current.Items["kShopCart"] = _node;
                }
            }
        }

        public void HandleResponse()
        {
            if (_cart.paymentController.paymentProvider.responseHandler(HttpContext.Current, _cart))
            {
                _cart.status = Cart.Status.paid;


                save();
            }
        }

        public void UpdateCart()
        {
            if (HttpContext.Current.Request.Form["updateCart"] != null)
            {
                foreach (string key in HttpContext.Current.Request.Form.Keys)
                {
                    if (key.StartsWith("quantity_"))
                    {
                        _cart.setQuantity(key.Substring(9), Convert.ToInt32(HttpContext.Current.Request.Form[key]));
                    }
                }
                CartManagerUmbraco.getFrom(_cart).save();
            }

        }


        public static void Clear()
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("shopCartId", ""));
            HttpContext.Current.Items["kShopCart"] = null;
        }

        public void save()
        {
            
            User user = User.GetUser(0);
            Document _doc;
            if (_node == null || _node.Id == 0)
            {
                // A new cart.

                _doc = Document.MakeNew("Cart", DocumentType.GetByAlias("kShopCart"), user, -1);


                //umbraco.library.UpdateDocumentCache(_doc.Id);

                HttpContext.Current.Response.Cookies.Add(new HttpCookie("shopCartId", _doc.Id.ToString()));
            }
            else
            {
                _doc = new Document(_node.Id);
            }

            string productIds = "";
            bool first = true;
            foreach (CartedProduct cProduct in _cart.products)
            {
                if (!first)
                {
                    productIds += ",";
                }
                first = false;
                productIds += ProductManagerUmbraco.getFrom(cProduct.product).getNode().Id + ":" + cProduct.quantity;
            }

            _doc.getProperty("storedProducts").Value = productIds;
            if (_cart.paymentController != null && _doc.getProperty("paymentControllerId") != null )
            {
                _doc.getProperty("paymentControllerId").Value = _cart.paymentController.id;
            }
            if (_doc.getProperty("status") != null)
            {
                _doc.getProperty("status").Value = _cart.status.ToString();
            }
            _doc.SendToPublication(user);
            _doc.Publish(user);
           
            // This doesn't seam to work right ... for some reason..
            umbraco.library.UpdateDocumentCache(_doc.Id);

            _node = new Node(_doc.Id);
            HttpContext.Current.Items["kShopCart"] = _node;
             
        }

        public void fill(Cart cart)
        {
            _cart = cart;
            if (_node == null)
            {
                // We got nothing to fill from...
                return;
            }

            if (_node.GetProperty("storedProducts") != null)
            {
                foreach (string cartProduct in _node.GetProperty("storedProducts").Value.ToString().Split(','))
                {
                    int productId = 0;
                    int quantity = 1;
                    string id = cartProduct.Split(':')[0];
                    string strQuantity = null;
                    if (cartProduct.Split(':').Count() > 1)
                    {
                        strQuantity = cartProduct.Split(':')[1];
                    }

                    if (strQuantity != null)
                    {
                        Int32.TryParse(strQuantity, out quantity);
                    }


                    if (Int32.TryParse(id, out productId))
                    {
                        cart.addProduct(new Product(new ProductManagerUmbraco(productId)), quantity);
                    }
                }
            }

            if (_node.GetProperty("paymentControllerId") != null && _node.GetProperty("paymentControllerId").Value != "")
            {
                foreach (PaymentController paymentController in Helper.getCatalog().getEnabledPaymentControllers())
                {
                    if (paymentController.id == _node.GetProperty("paymentControllerId").Value)
                    {
                        cart.paymentController = paymentController;
                        break;
                    }
                }
            }


            /// Setting the status should be done last
            if (_node.GetProperty("status") != null)
            {
                foreach (Cart.Status status in System.Enum.GetValues(typeof(Cart.Status)))
                {
                    if (status.ToString() == _node.GetProperty("status").Value)
                    {
                        _cart.status = status;
                    }
                }
            }
        }


        /// <summary>
        /// Gets the manager from an service that has one already connected.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static CartManagerUmbraco getFrom(Cart cart)
        {
            return (CartManagerUmbraco)cart.getManager(typeof(CartManagerUmbraco));
        }

        /// <summary>
        /// Gets the document that is connected to this manager
        /// </summary>
        /// <returns></returns>
        public Node getNode()
        {
            return _node;
        }
    }
}

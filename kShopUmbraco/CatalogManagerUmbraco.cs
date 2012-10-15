using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kShop;

using umbraco.businesslogic;
using umbraco.cms.businesslogic.web;
using umbraco.NodeFactory;
using System.Web;


namespace kShopUmbraco
{
    public class CatalogManagerUmbraco : CatalogManager
    {
        Node _doc = null;
        public CatalogManagerUmbraco(int id)
        {
            _doc = new Node(id);
        }


        public void fill(Catalog catalog)
        {
            if (_doc.GetProperty("title") != null)
            {
                catalog.title = _doc.GetProperty("title").Value.ToString();
                if (catalog.title == "")
                {
                    catalog.title = _doc.Name;
                }
            }

            foreach (Node child in _doc.Children)
            {
                if (child.NodeTypeAlias == "kShopCategory")
                {
                    catalog.categories.Add(new Category(new CategoryManagerUmbraco(child.Id)));
                }
            }

            catalog.pathToPaymentProviders = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), "paymentProviders");
        }

        /// <summary>
        /// Gets the manager from an service that has one already connected.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static CategoryManagerUmbraco getFrom(Category category)
        {
            return (CategoryManagerUmbraco)category.getManager(typeof(CategoryManagerUmbraco));
        }

        /// <summary>
        /// Gets the document that is connected to this manager
        /// </summary>
        /// <returns></returns>
        public Node getNode()
        {
            return _doc;
        }
    }
}

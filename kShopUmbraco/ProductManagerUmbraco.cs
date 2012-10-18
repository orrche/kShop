using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kShop;

using System.Web;

using umbraco.NodeFactory;
using umbraco.businesslogic;
using umbraco.cms.businesslogic.web;

namespace kShopUmbraco
{
    public class ProductManagerUmbraco : ProductManager
    {
        Node _doc = null;

        public ProductManagerUmbraco(int id)
        {
            _doc = new Node(id);
        }

        public void fill(Product product)
        {
            product.id = _doc.Id.ToString();

            if (_doc.GetProperty("title") != null)
            {
                product.title = _doc.GetProperty("title").Value.ToString();
                if (product.title == "")
                {
                    product.title = _doc.Name;
                }
            }

            if (_doc.GetProperty("price") != null)
            {
                Decimal price;

                if (Decimal.TryParse(_doc.GetProperty("price").Value.ToString(), out price))
                {
                    product.price = price;
                }
            }

            foreach (Node child in _doc.Children)
            {
                if (child.NodeTypeAlias == "kShopProduct")
                {
                    product.products.Add(new Product(new ProductManagerUmbraco(child.Id)));
                }
            }
        }

        /// <summary>
        /// Gets the manager from an service that has one already connected.
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static ProductManagerUmbraco getFrom(Product product)
        {
            return (ProductManagerUmbraco)product.getManager(typeof(ProductManagerUmbraco));
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

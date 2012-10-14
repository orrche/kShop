using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using kShop;

using umbraco.businesslogic;
using umbraco.cms.businesslogic.web;
using umbraco.NodeFactory;

namespace kShopUmbraco
{
    public class CategoryManagerUmbraco : CategoryManager
    {
        Node _doc = null;
        public CategoryManagerUmbraco(int id)
        {
            _doc = new Node(id);
        }


        public void fill(Category category)
        {
            if (_doc.GetProperty("title") != null)
            {
                category.title = _doc.GetProperty("title").Value.ToString();
                if (category.title == "")
                {
                    category.title = _doc.Name;
                }
            }

            foreach (Node child in _doc.Children)
            {
                if (child.NodeTypeAlias == "kShopCategory")
                {
                    category.categories.Add(new Category(new CategoryManagerUmbraco(child.Id)));
                }
                else if (child.NodeTypeAlias == "kShopProduct")
                {
                    category.products.Add(new Product(new ProductManagerUmbraco(child.Id)));
                }
            }
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

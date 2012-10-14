using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kShop
{
    public class Catalog
    {
        string _title;
        List<CatalogManager> _managers = new List<CatalogManager>();
        bool filled = false;
        List<Category> _categories = new List<Category>();



        public Catalog(CatalogManager manager)
        {
            _managers.Add(manager);
        }

        public void fill()
        {
            if (!filled)
            {
                filled = true;
                foreach (CatalogManager manager in _managers)
                {
                    manager.fill(this);
                }
            }
        }

        public List<Category> categories
        {
            get
            {
                fill();
                return _categories;
            }
        }

        public string title
        {
            get
            {
                fill();
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public CatalogManager getManager(Type type)
        {
            foreach (CatalogManager manager in _managers)
            {
                if (manager.GetType() == type)
                {
                    return manager;
                }
            }

            return null;
        }
    }
}

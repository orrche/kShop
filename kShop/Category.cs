using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kShop
{
    public class Category
    {
        string _title;
        List<CategoryManager> _managers = new List<CategoryManager>();
        bool filled =false;
        List<Category> _categories = new List<Category>();
        List<Product> _products = new List<Product>();


        public Category(CategoryManager manager)
        {
            _managers.Add(manager);
        }

        public void fill()
        {
            if (!filled)
            {
                filled = true;
                foreach (CategoryManager manager in _managers)
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

        public List<Product> products
        {
            get
            {
                fill();
                return _products;
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

        public CategoryManager getManager(Type type)
        {
            foreach (CategoryManager manager in _managers)
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

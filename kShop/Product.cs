using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kShop
{
    public class Product
    {
        string _title;
        string _id = "";
        bool filled = false;
        decimal _price = 0;
        List<ProductManager> _managers = new List<ProductManager>();
        List<Product> _products = new List<Product>();


        public Product(ProductManager manager)
        {
            _managers.Add(manager);
        }

        public void fill()
        {
            if (!filled)
            {
                filled = true;

                foreach (ProductManager manager in _managers)
                {
                    manager.fill(this);
                }
            }
        }

        public decimal getFromPrice()
        {
            decimal ret = price;

            foreach (Product product in _products)
            {
                decimal subFromPrice = product.getFromPrice();
                if (subFromPrice < ret || ret == 0)
                {
                    ret = subFromPrice;
                }
            }

            return ret;
        }

        public List<Product> products
        {
            get
            {
                return _products;
            }
        }

        public string id
        {
            get
            {
                fill();
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public decimal price
        {
            get
            {
                fill();
                return _price;
            }
            set
            {
                _price = value;
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

        public ProductManager getManager(Type type)
        {
            foreach (ProductManager manager in _managers)
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

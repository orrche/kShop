using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kShop
{
    public class Cart
    {
        List<CartedProduct> _products = new List<CartedProduct>();
        List<CartManager> _managers = new List<CartManager>();

        bool filled = false;

        public Cart(CartManager manager)
        {
            _managers.Add(manager);

        }

        public void addProduct(Product product, int quantity)
        {
            bool found = false;
            foreach (CartedProduct cProduct in _products)
            {
                if (cProduct.product.id == product.id)
                {
                    found = true;
                    cProduct.quantity+= quantity;
                    break;
                }
            }

            if (!found)
            {
                CartedProduct cProduct = new CartedProduct(product, quantity);
                _products.Add(cProduct);
            }

        }

        public Decimal getSum()
        {
            fill();

            Decimal ret = 0;

            foreach (CartedProduct cProduct in _products)
            {
                ret += cProduct.product.price * cProduct.quantity;
            }

            return ret;

        }

        public void setQuantity(string productId, int quantity)
        {
            fill();
            CartedProduct foundProduct = null;
            foreach (CartedProduct cProduct in _products)
            {
                if (cProduct.product.id == productId)
                {
                    foundProduct = cProduct;
                    break;
                }
            }
            if (foundProduct == null)
            {
               // addProduct(product, quantity);
            }
            else
            {
                if (quantity == 0)
                {
                    _products.Remove(foundProduct);
                }
                else
                {
                    foundProduct.quantity = quantity;
                }
            }
        }

        public void addProduct(Product product)
        {
            addProduct(product, 1);            
        }

        public List<CartedProduct> products
        {
            get
            {
                fill();
                return _products;
            }
        }

        public void fill()
        {
            if (!filled)
            {
                filled = true;

                foreach (CartManager cartManager in _managers)
                {
                    cartManager.fill(this);
                }
            }
        }

        public CartManager getManager(Type type)
        {
            foreach (CartManager manager in _managers)
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

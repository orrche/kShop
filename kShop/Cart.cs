using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kShop
{
    public class Cart
    {
        public enum Status
        {
            incompleat,
            pending,
            paid,
            compleated
        };

        Cart.Status _status = Status.incompleat;

        List<CartedProduct> _products = new List<CartedProduct>();
        List<CartManager> _managers = new List<CartManager>();
        PaymentController _paymentController = null;

        bool filled = false;

        public Cart(CartManager manager)
        {
            _managers.Add(manager);
            fill();

        }

        public void addProduct(Product product, int quantity)
        {
            if (status != Status.incompleat)
            {
                throw new Exception("Can't add products to cart thats not in incompleat state");
            }

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

        /// <summary>
        /// The cart needs to be in incompleat state or this will not work
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        public void setQuantity(string productId, int quantity)
        {
            if (status != Status.incompleat)
            {
                throw new Exception("Can't add products to cart thats not in incompleat state");
            }
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

        public Status status
        {
            get
            {
                fill();
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        /// <summary>
        /// Setting this to something other then null will change the status from incompleat to pending
        /// </summary>
        public PaymentController paymentController
        {
            get
            {
                fill();
                return _paymentController;
            }
            set
            {
                if (value != null && status == Status.incompleat)
                {
                    status = Status.pending;
                }
                _paymentController = value; 
            }
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

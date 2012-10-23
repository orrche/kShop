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
            /// <summary>
            /// Cart is in an incompleate state, i.e. the user hasn't confirmed it yet
            /// </summary>
            incompleat,
            /// <summary>
            /// The cart is locked for futher additions to the cart, this means that the user has pressed the order button.
            /// </summary>
            pending,
            /// <summary>
            /// The cart is in a confirmed but not yet paid state, this might happen if payment in reality is selected or in any other way that can't be automaticly confirmed
            /// </summary>
            confirmed,
            /// <summary>
            /// The order is paid 
            /// </summary>
            paid,
            /// <summary>
            /// The order is acknowledged by the shop owner.
            /// </summary>
            compleated
        };

        Cart.Status _status = Status.incompleat;

        List<CartedProduct> _products = new List<CartedProduct>();
        List<CartManager> _managers = new List<CartManager>();
        PaymentController _paymentController = null;

        string _firstname;
        string _lastname;
        string _address;
        string _street;
        string _zipCode;
        string _city;
        string _email;
        string _phone;

        string _receipt;

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
        public void paid()
        {
            _status = Status.paid;
            createReceipt();

        }
        public void createReceipt()
        {
            decimal sum = 0;
            foreach (CartedProduct cProduct in products)
            {
                _receipt += cProduct.product.title + "\t" + cProduct.product.price + "\t" + cProduct.quantity + "\t" + (cProduct.product.price * cProduct.quantity) + "\n";
                sum += cProduct.product.price * cProduct.quantity;
            }

            _receipt += " Sum: " + sum;
        }

        /// <summary>
        /// The cart needs to be in incompleat state or this will not work
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        public void setQuantity(string productId, int quantity)
        {
            fill();
            if (status != Status.incompleat && false)
            {
                throw new Exception("Can't add products to cart thats not in incompleat state");
            }
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

        public string receipt
        {
            get
            {
                fill();
                return _receipt;
            }
            set
            {
                _receipt = value;
            }
        }
       
        public string firstname
        {
            get
            {
                fill();
                return _firstname;
            }
            set
            {
                _firstname = value;
            }
        }

        public string lastname
        {
            get
            {
                fill();
                return _lastname;
            }
            set
            {
                _lastname = value;
            }
        }

        public string address
        {
            get
            {
                fill();
                return _address;
            }
            set
            {
                _address = value;
            }
        }

        public string street
        {
            get
            {
                fill();
                return _street;
            }
            set
            {
                _street = value;
            }
        }

        public string zipCode
        {
            get
            {
                fill();
                return _zipCode;
            }
            set
            {
                _zipCode = value;
            }
        }

        public string city
        {
            get
            {
                fill();
                return _city;
            }
            set
            {
                _city = value;
            }
        }

        public string email
        {
            get
            {
                fill();
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        
        public string phone
        {
            get
            {
                fill();
                return _phone;
            }
            set
            {
                _phone = value;
            }
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
                    //status = Status.pending;
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

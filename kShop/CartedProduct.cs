using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kShop
{
    public class CartedProduct
    {
        Product _product;
        int _quantity;

        public CartedProduct(Product product, int quantity)
        {
            _product = product;
            _quantity = quantity;
        }

        public Product product
        {
            get
            {
                return _product;
            }
        }

        public int quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = value;
            }
        }
    }
}

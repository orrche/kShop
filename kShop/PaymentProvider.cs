using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

namespace kShop
{
    public interface PaymentProvider
    {
        string getName();
        PaymentRequest createPaymentRequest(Cart cart);
        bool responseHandler(HttpContext context, Cart cart);
        bool list();
    }
}

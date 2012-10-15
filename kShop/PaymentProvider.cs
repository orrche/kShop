using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kShop
{
    public interface PaymentProvider
    {
        string getName();
        string createPaymentRequest(Cart cart);
        bool list();
    }
}

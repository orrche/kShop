using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kShop;

namespace dummyPaymentHandler
{
    public class Class1 : kShop.PaymentProvider
    {
        public virtual string getName()
        {
            return "DUMMY";
        }

        public virtual bool list()
        {
            return true;
        }

        public string createPaymentRequest(Cart cart)
        {
            return "Payment Request";
        }
    }
}

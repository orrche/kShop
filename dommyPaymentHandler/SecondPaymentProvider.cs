using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kShop;

namespace dummyPaymentHandler
{
    /// <summary>
    /// Testing with inharitance, using new doesn't work for some reason
    /// </summary>
    class SecondPaymentProvider : Class1
    {
        public SecondPaymentProvider(PaymentController controller) : base(controller)
        {
            
        }

        public override string getName()
        {
            return "DUMMY3";
        }

        public override bool list()
        {
            return true;
        }

    }
}

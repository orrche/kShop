using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;

using kShop;

namespace dummyPaymentHandler
{
    public class Class1 : kShop.PaymentProvider
    {
        PaymentController _controller;

        public Class1(PaymentController controller)
        {
            _controller = controller;
        }


        public virtual string getName()
        {
            return "DUMMY";
        }

        public virtual bool list()
        {
            return true;
        }

        public PaymentRequest createPaymentRequest(Cart cart)
        {
            PaymentRequest paymentRequest = new PaymentRequest();


            paymentRequest.destination = "http://minoris.se/www/paytest.php";

            paymentRequest.setParameter("accepturl", _controller.respondUrl + "?resp=accept");
            paymentRequest.setParameter("cancelurl", _controller.respondUrl + "?resp=cancel");

            paymentRequest.renderMethod = PaymentRequestRenderMethods.Post;

            return paymentRequest;
        }

        public bool responseHandler(HttpContext context, Cart cart)
        {
            if (context.Request.Params["resp"] == "accept")
            {
                cart.paid();
                return true;
            }
            return false;
        }
    }
}

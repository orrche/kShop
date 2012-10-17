using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kShop
{
    public class PaymentController
    {
        string _dll;
        string _namespace;
        string _class;
        string _id;
        string _respondUrl;

        PaymentProvider _paymentProvider;

        List<PaymentControllerManager> _managers = new List<PaymentControllerManager>();
        bool filled = false;

        public PaymentController(PaymentControllerManager manager)
        {
            _managers.Add(manager);
        }

        public void fill() 
        {
            if (!filled)
            {
                filled = true;
                foreach (PaymentControllerManager manager in _managers)
                {
                    manager.fill(this);
                }
            }
        }

        public PaymentProvider paymentProvider
        {
            get
            {
                return _paymentProvider;
            }
            set
            {
                _paymentProvider = value;
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

        public string dll
        {
            get
            {
                fill();
                return _dll;
            }
            set
            {
                _dll = value;
            }
        }

        public string pluginNamespace
        {
            get
            {
                fill();
                return _namespace;
            }
            set
            {
                _namespace = value;
            }
        }

        public string pluginClass
        {
            get
            {
                fill();
                return _class;
            }
            set
            {
                _class = value;
            }
        }

        public string respondUrl
        {
            get
            {
                fill();
                return _respondUrl;
            }
            set
            {
                _respondUrl = value;
            }
        }
        
    }
}

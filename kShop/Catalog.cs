using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace kShop
{
    public class Catalog
    {
        string _title;
        List<CatalogManager> _managers = new List<CatalogManager>();
        bool filled = false;
        List<Category> _categories = new List<Category>();
        List<PaymentController> _paymentHandlerControllers = new List<PaymentController>();


        public Catalog(CatalogManager manager)
        {
            _managers.Add(manager);
        }

        public void fill()
        {
            if (!filled)
            {
                filled = true;
                foreach (CatalogManager manager in _managers)
                {
                    manager.fill(this);
                }
            }
        }

        public List<Category> categories
        {
            get
            {
                fill();
                return _categories;
            }
        }

        public string title
        {
            get
            {
                fill();
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public CatalogManager getManager(Type type)
        {
            foreach (CatalogManager manager in _managers)
            {
                if (manager.GetType() == type)
                {
                    return manager;
                }
            }

            return null;
        }

        public List<PaymentController> paymentHandlerControllers
        {
            get
            {
                fill();
                return _paymentHandlerControllers;
            }
        }

        public List<PaymentController> getEnabledPaymentControllers()
        {
            fill();
            List<PaymentController> ret = new List<PaymentController>();

            foreach (PaymentController controller in paymentHandlerControllers)
            {
                if (controller.paymentProvider != null)
                {
                    if (controller.paymentProvider.list())
                    {
                        ret.Add(controller);
                    }
                    continue;
                }
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {

                    //if (assembly.IsDynamic == false && controller.dll != null && assembly.Location.EndsWith(controller.dll))
                    {
                        foreach (Type type in assembly.GetTypes())
                        {
                            if ((type.Namespace == controller.pluginNamespace || true )  && type.Name == controller.pluginClass)
                            {
                                foreach (Type inter in type.GetInterfaces())
                                {
                                    if (inter == (typeof(PaymentProvider)))
                                    {
                                        PaymentProvider paymentProvider = (PaymentProvider)Activator.CreateInstance(type, new object[] { controller });
                                        if (paymentProvider.list())
                                        {
                                            controller.paymentProvider = paymentProvider;
                                            ret.Add(controller);
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return ret;

        }

        /// <summary>
        /// Returns a list of all available providers, not only the enabled ones.
        /// </summary>
        /// <returns></returns>
        public List<PaymentProvider> getAvailablePaymentProviders()
        {
            List<PaymentProvider> ret = new List<PaymentProvider>();
            
            foreach ( Assembly assembly in AppDomain.CurrentDomain.GetAssemblies() )
            {
                
                foreach( Type type in assembly.GetTypes() )
                {
                    foreach( Type inter in type.GetInterfaces())
                    {
                        if (inter == (typeof(PaymentProvider)))
                        {

                            PaymentProvider paymentProvider = (PaymentProvider)Activator.CreateInstance(type, new object[] { paymentHandlerControllers[0] });
                            if (paymentProvider.list())
                            {
                                ret.Add(paymentProvider);
                            }
                        }
                    }

                    
                   // ((PaymentProvider)Activator.CreateInstance(Type.GetType("dummyPaymentHandler.Class1"))).getName();
                }
            }

            return ret;
        }
    }
}

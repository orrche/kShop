using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kShop
{
    public enum PaymentRequestRenderMethods
    {
        Get,
        Post       
    }
    public class PaymentRequest
    {
        Dictionary<string, string> _parameters = new Dictionary<string, string>();
        PaymentRequestRenderMethods _renderMethod = PaymentRequestRenderMethods.Post;
        string _destination;


        /// <summary>
        /// Adds parameter, set value to null to remove it
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public void setParameter(string key, string value)
        {
            if (value == null)
            {
                _parameters.Remove(key);
            }
            else
            {
                _parameters[key] = value;
            }
        }

        /// <summary>
        /// Use as readonly please :)
        /// </summary>
        public Dictionary<string, string> parameters
        {
            get
            {
                return _parameters;
            }
        }

        public PaymentRequestRenderMethods renderMethod
        {
            get
            {
                return _renderMethod;
            }
            set
            {
                _renderMethod = value;
            }
        }

        public string destination
        {
            get
            {
                return _destination;
            }
            set
            {
                _destination = value;
            }
        }
    }
}

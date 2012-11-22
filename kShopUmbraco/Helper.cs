using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using kShop;
using umbraco.MacroEngines;
using System.Web;

namespace kShopUmbraco
{
    /// <summary>
    ///  These probably should be possible to overload somehow, just not sure how....
    /// </summary>
    public class Helper
    {
        static Catalog catalog = null;
        public static Catalog getCatalog()
        {
            catalog = (Catalog)HttpContext.Current.Cache.Get("kShopCatalog");
            if (catalog == null)
            {
                catalog = new Catalog(new CatalogManagerUmbraco((new DynamicNode(-1)).DescendantsOrSelf("kShopCatalog").Items.First().Id));
                HttpContext.Current.Cache.Add("kShopCatalog", catalog, null, DateTime.MaxValue, TimeSpan.FromMinutes(30), System.Web.Caching.CacheItemPriority.High, null);
            }

            return catalog;
        }
    }
}

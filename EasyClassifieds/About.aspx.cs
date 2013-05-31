using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Web.Caching;

namespace EasyClassifieds
{
    public partial class About : System.Web.UI.Page
    {
        protected string AboutText
        {
            get
            {
                if (Cache["AboutText"] == null)
                {
                    Cache.Add("AboutText",
                        File.OpenText(Server.MapPath("~/CustomContent/" + ConfigurationManager.AppSettings["About"])).ReadToEnd(),
                        null,
                        Cache.NoAbsoluteExpiration,
                        TimeSpan.FromMinutes(5),
                        CacheItemPriority.Default,
                        null);
                }
                return Cache["AboutText"].ToString();

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}

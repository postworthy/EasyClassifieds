using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;
using EasyClassifieds.Core;

namespace EasyClassifieds
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public string SiteStyleSheet 
        {
            get
            {

                return "<link href=\"" + ResolveUrl(ConfigurationManager.AppSettings["SiteStyleSheet"]) + "\" rel=\"stylesheet\" type=\"text/css\" />";
                
            }
        }
        protected string ApplicationTitle 
        {
            get
            {
                return ConfigurationManager.AppSettings["ApplicationTitle"];
            }
        }
        public string Logo 
        {
            get
            {
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Logo"]))
                {
                    if (ConfigurationManager.AppSettings["Logo"].StartsWith("~"))
                        return ResolveUrl(ResolveUrl(ConfigurationManager.AppSettings["Logo"]));
                    else
                        return ConfigurationManager.AppSettings["Logo"];
                }
                else
                    return "";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Membership.GetUser() != null)
            {
                NavigationMenu.Items.Remove(NavigationMenu.Items.Cast<MenuItem>().Where(i => i.Text == "Register").FirstOrDefault());
                if(!Roles.IsUserInRole(Constants.ROLE_ADMIN) || true /*SHORTED OUT FOR NOW*/)
                    NavigationMenu.Items.Remove(NavigationMenu.Items.Cast<MenuItem>().Where(i => i.Text == "Admin").FirstOrDefault());
            }
            else
            {
                NavigationMenu.Items.Remove(NavigationMenu.Items.Cast<MenuItem>().Where(i => i.Text == "My Shop").FirstOrDefault());
                NavigationMenu.Items.Remove(NavigationMenu.Items.Cast<MenuItem>().Where(i => i.Text == "Admin").FirstOrDefault());
            }
        }
    }
}

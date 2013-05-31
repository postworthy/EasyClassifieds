using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using EasyClassifieds.Model;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Web.Caching;

namespace EasyClassifieds.Account
{
    public partial class Register : System.Web.UI.Page
    {
        protected string EmailBody
        {
            get
            {
                if (Cache["AboutText"] == null)
                {
                    try
                    {
                        Cache.Add("AboutText",
                            File.OpenText(Server.MapPath("~/CustomContent/" + ConfigurationManager.AppSettings["SignupEmail"])).ReadToEnd(),
                            null,
                            Cache.NoAbsoluteExpiration,
                            TimeSpan.FromMinutes(5),
                            CacheItemPriority.Default,
                            null);
                    }
                    catch { return ""; }
                }
                return Cache["AboutText"].ToString();

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            var user = Membership.GetUser(RegisterUser.UserName);
            using(var ec = new EasyContext())
            {
                ec.Shops.Add(new Shop() { AccountID = (Guid)user.ProviderUserKey, Name = user.UserName });
                ec.SaveChanges();
            }

            try { SendRegistrationEmail(user); }catch{ }

            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        private void SendRegistrationEmail(MembershipUser user)
        {
            var smtp = new SmtpClient();
            smtp.Send(
                new MailMessage(ConfigurationManager.AppSettings["CompanyEmail"], user.Email)
                {
                    Subject = "Welcome to " + ConfigurationManager.AppSettings["ApplicationTitle"] + "!",
                    Body = "<p>" + EmailBody + "</p>",
                    IsBodyHtml = true
                });
        }
    }
}

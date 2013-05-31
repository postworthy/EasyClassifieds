using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Net.Mail;

namespace EasyClassifieds.Account
{
    public partial class PasswordRecovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void prRecoverPassword_VerifyingUser1(object sender, LoginCancelEventArgs e)
        {
            if (prRecoverPassword.UserName.Contains("@"))
            {
                string username = Membership.GetUserNameByEmail(prRecoverPassword.UserName);
                if (!string.IsNullOrEmpty(username))
                    prRecoverPassword.UserName = username;
            }
        }

        protected void prRecoverPassword_SendingMail(object sender, MailMessageEventArgs e)
        {
            e.Message.Subject = "Password Reset";
            e.Message.From = new MailAddress(ConfigurationManager.AppSettings["CompanyEmail"]);
        }
    }
}
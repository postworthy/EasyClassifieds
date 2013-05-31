using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using EasyClassifieds.Model;

namespace EasyClassifieds
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            //DO NOT CHANGE THIS CODE UNLESS YOU KNOW WHAT YOU ARE DOING AND YOU KNOW 100% THAT
            //YOU ARE NOT POINTING TO A PRODUCTION DATABASE BECAUSE YOU WILL DROP ALL DATA IF YOU 
            //ARE NOT CAREFUL.
            
            //System.Data.Entity.Database.SetInitializer<EasyContext>(null);
            System.Data.Entity.Database.SetInitializer<EasyContext>(new DatabaseBetaTestInitializer());
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }

    }
}

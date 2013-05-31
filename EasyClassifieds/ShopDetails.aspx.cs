using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EasyClassifieds.Model;
using EasyClassifieds.Core;
using System.Web.Security;

namespace EasyClassifieds
{
    public partial class ShopDetails : PageBase<Shop,EasyContext>
    {
        public string ShopImageID 
        {
            get
            {
                return (Entity.ShopImages != null && Entity.ShopImages.FirstOrDefault() != null) ? Entity.ShopImages.FirstOrDefault().ID.ToString() : "0";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack && Entity.ID > 0)
            {
                Entity.Views += 1;
                Update();
            }

            litAddress.Text = Entity.Address + " " + Entity.CityStateZip;
            litPhone.Text = Entity.PhoneNumber;

            listings.DataSource = EasyContext.ListingItems
                .Where(i => i.IsSold == false)
                .Where(i => i.Shop.ID == Entity.ID)
                .OrderByDescending(i => i.CreatedOn)
                .ToList();
            listings.DataBind();
        }
    }
}
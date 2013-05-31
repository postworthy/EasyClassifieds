using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EasyClassifieds.Core;
using EasyClassifieds.Model;
using System.Web.Security;

namespace EasyClassifieds
{
    public partial class ItemDetails : PageBase<ListingItem, EasyContext>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity.ID > 0)
            {
                if (!IsPostBack)
                {
                    Entity.Views += 1;
                    Update();
                }

                AddFieldMappings(() => chkIsFeatured.Checked == Entity.IsFeatured);
                litTitle.Text = Entity.Title;
                litDescription.Text = Entity.Description;
                litPrice.Text = "<b>" + Entity.Price.ToString() + "</b>";
                litCategory.Text = "<a href=\"" + ResolveUrl("~/?c=" + Entity.Category.ID) + "\">" + Entity.Category.Name + "</a>";
                litShop.Text = "<a href=\"" + ResolveUrl("~/shopdetails.aspx?id=" + Entity.Shop.ID) + "\">" + Entity.Shop.Name + "</a>";
                litPhone.Text = "<b>" + ((!string.IsNullOrEmpty(Entity.Shop.PhoneNumber)) ? Entity.Shop.PhoneNumber : "No contact information for this shop") + "</b>";
                litViews.Text = Entity.Views.ToString();

                rptImages.DataSource = Entity.Images;
                rptImages.DataBind();
                fsImages.Visible = Entity.Images.Count > 0;

                if (Roles.IsUserInRole(Constants.ROLE_ADMIN))
                    isFeatured.Visible = true;

                if (Entity.Images.Count == 0)
                {
                    chkIsFeatured.Enabled = false;
                    litNoImages.Visible = true;
                }
            }
        }

        protected void chkIsFeatured_CheckedChanged(object sender, EventArgs e)
        {
            Update();
        }
    }
}
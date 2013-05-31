using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EasyClassifieds.Model;

namespace EasyClassifieds
{
    public partial class Listing : System.Web.UI.MasterPage
    {
        public string ListingQuery { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            using(var ec = new EasyContext())
            {
                categoryListing.DataSource = ec.Categories
                    .Where(c => c.ListingItems.Where(i=>!i.IsSold).Count() > 0)
                    .OrderBy(c => c.Name)
                    .Select(c => new { ID = c.ID, Name = c.Name, Count = c.ListingItems.Where(i => !i.IsSold).Count() }).ToList();
                categoryListing.DataBind();

                featuredListing.DataSource = ec.ListingItems
                    .Where(i => i.IsFeatured && i.IsSold == false && i.Images.Count > 0)
                    .OrderByDescending(i => i.CreatedOn)
                    .Take(4)
                    .Select(i => new { ID = i.ID, Title = i.Title, Price = i.Price, ImageID = i.Images.FirstOrDefault().ID })
                    .ToList();
                featuredListing.DataBind();
                featuredContainer.Visible = featuredListing.Items.Count > 0;
            }
        }
    }
}
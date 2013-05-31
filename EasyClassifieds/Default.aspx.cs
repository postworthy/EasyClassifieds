using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EasyClassifieds.Model;
using System.Configuration;
using EasyClassifieds.Core;

namespace EasyClassifieds
{
    public partial class _Default : PageBase<ListingItem, EasyContext>
    {
        public int CategoryID 
        {
            get
            {
                int ret = 0;
                int.TryParse(Request.QueryString["c"], out ret);
                return ret;
            }
        }

        public string SearchQuery 
        {
            get
            {
                return Request.QueryString["s"];
            } 
        }

        public int PageSize 
        {
            get
            {
                int pagesize = 100;
                int.TryParse(ConfigurationManager.AppSettings["PageSize"], out pagesize);
                return pagesize;
            }
        }

        public int PageCount 
        {
            get
            {
                return (int)Math.Ceiling(Query().Count() / (PageSize * 1M));
            }
        }

        public int CurrentPageIndex
        {
            get
            {
                int page = 0;
                if(int.TryParse(Request.QueryString["page"], out page))
                    return page-1;
                else
                    return 0;
            }
        }

        public string QueryUrl 
        {
            get
            {
                return ((CategoryID > 0) ? "&c=" + CategoryID : "") + ((!string.IsNullOrEmpty(SearchQuery)) ? "&s=" + SearchQuery : "");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            listings.DataSource = Query().Skip(PageSize * CurrentPageIndex).Take(PageSize).ToList();
            listings.DataBind();

            BuildListingQuery();
        }

        private IQueryable<ListingItem> Query()
        {
            return EasyContext.ListingItems
                   .Where(i => i.IsSold == false)
                   .Where(i => CategoryID == 0 || i.Category.ID == CategoryID)
                   .Where(i => string.IsNullOrEmpty(SearchQuery) || i.Title.Contains(SearchQuery) || i.Description.Contains(SearchQuery) || i.Category.Name.Contains(SearchQuery))
                   .OrderByDescending(i => i.ID);
        }

        private void BuildListingQuery()
        {
            using (var ec = new EasyContext())
            {
                string category = "";
                string search = "";
                if (CategoryID > 0)
                {
                    category = "Category: " + ec.Categories.Find(CategoryID).Name;
                    (Master as Listing).ListingQuery = "(" + category + ")";
                }
                if (!string.IsNullOrEmpty(SearchQuery))
                {
                    search = "Search: " + Server.HtmlEncode(SearchQuery);
                    (Master as Listing).ListingQuery = "(" + search + ")";
                }
            }
        }
    }
}

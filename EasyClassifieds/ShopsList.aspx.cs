using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EasyClassifieds.Model;
using System.Web.UI.HtmlControls;

namespace EasyClassifieds
{
    public partial class ShopsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (var ec = new EasyContext())
            {
                List<char> alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();
                int columnIndex = 0;
                HtmlTableCell[] column = new HtmlTableCell[] { col0, col1, col2, col3 };
                for (int i = 0; i < alpha.Count; i++)
                {
                    string a = alpha[i].ToString();
                    if (ec.Shops.Where(s => s.Name.StartsWith(a)).Count() > 0)
                    {
                        column[columnIndex].InnerHtml += alpha[i] + "<br/>";
                        ec.Shops
                            .Where(s => s.Name.StartsWith(a))
                            .ToList()
                            .ForEach(s => column[columnIndex].InnerHtml += "<a href=\"shopdetails.aspx?id=" + s.ID + "\">" + s.Name + "</a><br/>");
                        columnIndex++;
                        if (columnIndex > 3) columnIndex = 0;
                    }
                }

            }
        }

        //protected void rptShops_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    Shop s = e.Item.DataItem as Shop;
        //    Repeater rpt = (e.Item.FindControl("shopListings") as Repeater);
        //    using (var ec = new EasyContext())
        //    {
        //        rpt.DataSource = ec.ListingItems
        //            .Where(i => i.IsSold == false && i.Shop.ID == s.ID)
        //            .OrderByDescending(i => i.CreatedOn)
        //            .Take(8)
        //            .Select(i => new { ID = i.ID, Title = i.Title, Price = i.Price, ImageID = (i.Images.FirstOrDefault() == null)? 0 : i.Images.FirstOrDefault().ID })
        //            .ToList();
        //        rpt.DataBind();
        //    }
        //}
    }
}
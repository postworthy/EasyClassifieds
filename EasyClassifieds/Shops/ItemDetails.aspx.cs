using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EasyClassifieds.Model;
using EasyClassifieds.Core;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Configuration;

namespace EasyClassifieds.Shops
{
    public partial class ItemDetails : PageBase<ListingItem, EasyContext>
    {
        public int MaxImages 
        {
            get
            {
                int max = 5;
                int.TryParse(ConfigurationManager.AppSettings["MaxListingImages"], out max);
                return max;
            }
        }
        public Shop MyShop 
        {
            get 
            {
                Guid accountID = (Guid)LoggedInUser.ProviderUserKey;
                return EasyContext.Shops.Where(s => s.AccountID == accountID).FirstOrDefault();
            }
            set { }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Entity != null && Entity.Shop != null && Entity.Shop.AccountID != (Guid)LoggedInUser.ProviderUserKey) Response.Redirect("~/Shops/Manage.aspx");

            AddFieldMappings(
                () => txtTitle.Text == Entity.Title,
                x => CleanHtml.StripTags(x.ToString(), new string[] { "" }));
           AddFieldMappings(
               () => txtDescription.Text == Entity.Description,
               x => CleanHtml.StripTags(x.ToString(), new string[] { "i", "b", "p", "br" }).Replace(Environment.NewLine, "<br/>"),
               x=> x.ToString().Replace("<br/>", Environment.NewLine));
           AddFieldMappings(
              () => txtSerial.Text == Entity.SerialNumber,
               x => CleanHtml.StripTags(x.ToString(), new string[] { "" }));
           AddFieldMappings(() => txtPrice.Text == Entity.Price.ToString());
           AddFieldMappings(
               () => ddlCategory.SelectedValue == Entity.Category.ToString(), 
               s => EasyContext.Categories.Find(int.Parse(s.ToString())));
           
            Entity.Shop = MyShop;

           if (Entity.ID == 0) Entity.Category = new Category() { Name = "" };
           else
           {
               rptImages.DataSource = Entity.Images;
               rptImages.DataBind();
               fsImages.Visible = Entity.Images.Count > 0;
           }

            if (!IsPostBack)
            {
                ddlCategory.DataSource = EasyContext.Categories.OrderBy(c=>c.Name).ToList();
                ddlCategory.DataTextField = "Name";
                ddlCategory.DataValueField = "ID";
                ddlCategory.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price)) txtPrice.Text = Entity.Price.ToString();

            if (!string.IsNullOrEmpty(fuImage.FileName) && IsImage(fuImage.FileName) && (Entity.Images == null || Entity.Images.Count < MaxImages))
            {
                if (Entity.Images == null) Entity.Images = new List<ListingImage>();
                var img = Bitmap.FromStream(new MemoryStream(fuImage.FileBytes));
                img = img.ResizeImage(300, 300, false);
                var ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                Entity.Images.Add(new ListingImage() { Name = fuImage.FileName.Split('.').FirstOrDefault(), ImageData = ms.GetBuffer() });
            }

            Update();

            RefreshImageList();
        }

        private void RefreshImageList()
        {
            rptImages.DataSource = Entity.Images;
            rptImages.DataBind();
            fsImages.Visible = Entity.Images.Count > 0;
        }

        protected void rptImages_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DeleteImage")
            {
                Entity.Images.Remove(Entity.Images.First(i => i.ID.ToString() == e.CommandArgument.ToString()));
                EasyContext.SaveChanges();
                RefreshImageList();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EasyClassifieds.Model;
using System.Web.Security;
using EasyClassifieds.Core;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace EasyClassifieds.Shops
{
    public partial class Manage : PageBase<Shop, EasyContext>
    {
        protected override long ID 
        {
            get
            {
                    Guid accountID = (Guid)Membership.GetUser().ProviderUserKey;
                    var shop = EasyContext.Shops.Where(s => s.AccountID == accountID).FirstOrDefault();
                    if (shop != null)
                        return shop.ID;
                    else 
                        return 0;
            }
        }
        public string ShopImageID
        {
            get
            {
                return (Entity.ShopImages != null && Entity.ShopImages.FirstOrDefault() != null) ? Entity.ShopImages.FirstOrDefault().ID.ToString() : "0";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            AddFieldMappings(() => txtName.Text == Entity.Name);
            AddFieldMappings(() => txtAddress.Text == Entity.Address);
            AddFieldMappings(() => txtZip.Text == Entity.Zip);
            AddFieldMappings(() => txtPhone.Text == Entity.PhoneNumber);

            imgShop.ImageUrl = "../image.ashx?shopimageid=" + ShopImageID;
            imgShop.AlternateText = Entity.Name;

            LoadListings();
        }

        private void LoadListings()
        {
            listings.DataSource = EasyContext.ListingItems
                .Where(i => i.Shop.ID == Entity.ID && (i.IsSold == false || chkShowSold.Checked))
                .OrderBy(i => i.IsSold)
                .ThenByDescending(i => i.CreatedOn)
                .ToList();
            listings.DataBind();
        }

        protected void btnSaveDetails_Click(object sender, EventArgs e)
        {
            if (txtZip.Text != Entity.Zip)
                Entity.CityStateZip = GeoCoder.TryGeocodeZip(txtZip.Text);

            if (!string.IsNullOrEmpty(fuImage.FileName) && IsImage(fuImage.FileName))
            {
                if (Entity.ShopImages == null) Entity.ShopImages = new List<ShopImage>();
                var img = Bitmap.FromStream(new MemoryStream(fuImage.FileBytes));
                img = img.ResizeImage(300, 300, false);
                var ms = new MemoryStream();
                img.Save(ms, ImageFormat.Jpeg);
                for (int i = Entity.ShopImages.Count - 1; i >= 0; i--)
                {
                    EasyContext.ShopImages.Remove(Entity.ShopImages.ElementAt(i));
                }
                Entity.ShopImages.Add(new ShopImage() { ImageData = ms.GetBuffer() });
            }

            Update();
        }

        protected void listings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton sold = e.Item.FindControl("btnSold") as LinkButton;
            if (sold != null)
            {
                sold.Text = ((ListingItem)e.Item.DataItem).IsSold ? "undo" : "sold";
                sold.CommandArgument = ((ListingItem)e.Item.DataItem).ID.ToString();
            }
        }

        protected void ToggleIsSold(object sender, EventArgs e)
        {
            LinkButton sold = sender as LinkButton;
            if (sold != null)
            {
                long soldID = long.Parse(sold.CommandArgument);
                if (soldID > 0)
                {
                    var item = EasyContext.ListingItems.Where(i => i.Shop.ID == Entity.ID && i.ID == soldID).FirstOrDefault();
                    if (item != null)
                    {
                        item.IsSold = !item.IsSold;
                        EasyContext.SaveChanges();
                    }
                }
            }
            LoadListings();
        }

        protected void DeleteShopImage(object sender, EventArgs e)
        {
            for (int i = Entity.ShopImages.Count - 1; i >= 0; i--)
            {
                EasyContext.ShopImages.Remove(Entity.ShopImages.ElementAt(i));
            }
            EasyContext.SaveChanges();
        }
    }
}
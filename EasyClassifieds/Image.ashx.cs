using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.IO;
using EasyClassifieds.Model;
using System.Drawing.Imaging;

namespace EasyClassifieds
{
    /// <summary>
    /// Summary description for Image
    /// </summary>
    public class Image : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            long id = 0;
            long.TryParse(context.Request.QueryString["id"], out id);
            if (id > 0)
            {
                using (var ec = new EasyContext())
                {
                    context.Response.ContentType = "image/jpeg";
                    var img = Bitmap.FromStream(new MemoryStream(ec.ListingImages.Where(i => i.ID == id).FirstOrDefault().ImageData));
                    img.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                }
            }
            else
            {
                long.TryParse(context.Request.QueryString["shopimageid"], out id);
                if (id > 0)
                {
                    using (var ec = new EasyContext())
                    {
                        context.Response.ContentType = "image/jpeg";
                        var img = Bitmap.FromStream(new MemoryStream(ec.ShopImages.Where(i => i.ID == id).FirstOrDefault().ImageData));
                        img.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EasyClassifieds.Model
{
    public class EasyContext : DbContext
    {
        public DbSet<ListingItem> ListingItems { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ListingImage> ListingImages { get; set; }
        public DbSet<ShopImage> ShopImages { get; set; }

        public EasyContext()
            : base("name=EasyClassifieds")
        {
        }
    }
}
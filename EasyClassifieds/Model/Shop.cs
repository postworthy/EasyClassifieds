using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyClassifieds.Model.Base;

namespace EasyClassifieds.Model
{
    public class Shop : EntityBase
    {
        public Guid? AccountID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string CityStateZip { get; set; }
        public string Zip { get; set; }
        public string PhoneNumber { get; set; }
        public long Views { get; set; }

        public virtual ICollection<ShopImage> ShopImages { get; set; }
        public ICollection<ListingItem> ListingItems { get; set; }
    }
}
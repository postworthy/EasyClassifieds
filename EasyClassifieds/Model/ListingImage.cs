using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyClassifieds.Model.Base;

namespace EasyClassifieds.Model
{
    public class ListingImage : EntityBase
    {
        public string Name { get; set; }
        public byte[] ImageData { get; set; }

        public virtual ListingItem ListingItem { get; set; }
    }
}
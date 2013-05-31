using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyClassifieds.Model.Base;

namespace EasyClassifieds.Model
{
    public class ShopImage : EntityBase
    {
        public string Name { get; set; }
        public byte[] ImageData { get; set; }

        public virtual Shop Shop { get; set; }
    }
}
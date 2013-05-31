using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyClassifieds.Model.Base;

namespace EasyClassifieds.Model
{
    public class ListingItem : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsSold { get; set; }
        public long Views { get; set; }
        public string SerialNumber { get; set; }

        public virtual Shop Shop { get; set; }
        /*private Category category = null;
        public virtual Category Category 
        { 
            get 
            {
                if (ID == 0) return new Category() { ID = 0, Name = "" }; 
                return category;
            } 
            set { category = value; }
        }*/
        public virtual Category Category { get; set; }
        public virtual ICollection<ListingImage> Images { get; set; }

        public ListingItem()
        {
            CreatedOn = DateTime.Now;
        }
    }
}
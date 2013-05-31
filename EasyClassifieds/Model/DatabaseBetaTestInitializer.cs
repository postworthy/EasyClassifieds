using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Management;
using System.Configuration;

namespace EasyClassifieds.Model
{
    public class DatabaseBetaTestInitializer : DropCreateDatabaseAlways<EasyContext>
    {
        protected override void Seed(EasyContext context)
        {
            SqlServices.Install("easy", SqlFeatures.Membership | SqlFeatures.RoleManager, ConfigurationManager.ConnectionStrings["EasyClassifieds"].ConnectionString);

            context.ListingItems.Add(new ListingItem()
            {
                Title = "Dishwasher",
                Description = "Washes dishes",
                Price = 1.0M,
                Category = new Category() { Name = "Appliances" },
                Shop = new Shop()
                {
                    Name = "Landon's Shop",
                    AccountID = Guid.Parse("0f2dff77-894d-47d7-9b1d-4c944061b3a7")
                }
            });

            context.ListingItems.Add(new ListingItem()
            {
                Title = "Amplifier",
                Description = "Makes music loud",
                Price = 1.0M,
                Category = new Category() { Name = "Audio" },
                Shop = new Shop()
                {
                    Name = "Brandon's Pawn Shop",
                    AccountID = Guid.Parse("32A417CF-9BDE-4BC6-9DBD-CEEE90AC6F70")
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "BMW",
                Description = "a nice car",
                Price = 1.0M,
                Category = new Category() { Name = "Autos" },
                IsFeatured = true,
                Shop = new Shop()
                {
                    Name = "Bill's Pawn Shop",
                    AccountID = new Guid("C086BCFD-3BE2-41D6-8013-9257327E03DF"),
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "Kodak",
                Description = "a camera for sale",
                Price = 1.0M,
                Category = new Category() { Name = "Cameras" },
                IsFeatured = true,
                Shop = new Shop()
                {
                    Name = "Kodak Camera Inc",
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "Painting",
                Description = "it is a pretty picture",
                Price = 1.0M,
                Category = new Category() { Name = "Collectibles" },
                Shop = new Shop()
                {
                    Name = "Nick Nacks",
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "Dell Laptop",
                Description = "15'' Dell XPS",
                Price = 1.0M,
                Category = new Category() { Name = "Computers" },
                IsFeatured = true,
                Shop = new Shop()
                {
                    Name = "Computers Direct",
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "Halo 3",
                Description = "a fun video game",
                Price = 1.0M,
                Category = new Category() { Name = "Movies/Games" },
                IsFeatured = true,
                Shop = new Shop()
                {
                    Name = "Game Stop",
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "Drums",
                Description = "new drum set",
                Price = 1.0M,
                Category = new Category() { Name = "Music Equipment" },
                Shop = new Shop()
                {
                    Name = "little drummer boy",
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "LED LCD Flat Screen",
                Description = "54'' flat screen with great picture quality",
                Price = 1.0M,
                Category = new Category() { Name = "Electronics" },
                Shop = new Shop()
                {
                    Name = "Electronics R Us",
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "Sig 40mm",
                Description = "Great gun and great price",
                Price = 1.0M,
                Category = new Category() { Name = "Firearms" },
                Shop = new Shop()
                {
                    Name = "The End Is Here Inc",
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "Big Diamond Ring",
                Description = "Title says it all",
                Price = 1.0M,
                Category = new Category() { Name = "Jewelry" },
                Shop = new Shop()
                {
                    Name = "Zales",
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "Weight Set",
                Description = "Get big with out premium weight set",
                Price = 1.0M,
                Category = new Category() { Name = "Sporting Goods" },
                Shop = new Shop()
                {
                    Name = "Gold's Gym",
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "Hammer",
                Description = "The must have tool",
                Price = 1.0M,
                Category = new Category() { Name = "Tools" },
                Shop = new Shop()
                {
                    Name = "Hammer Time",
                    Address = ""
                }
            });
            context.ListingItems.Add(new ListingItem()
            {
                Title = "Doll",
                Description = "just your basic doll",
                Price = 1.0M,
                Category = new Category() { Name = "Toys" },
                Shop = new Shop()
                {
                    Name = "Toy Box Inc",
                    Address = ""
                }
            });

            base.Seed(context);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class MusicStoreContext : DbContext
    {

        public MusicStoreContext() : base("MusicStoreDB") { }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<MusicItem> MusicItems { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public DbSet<ViewMusic> ViewMusics { get; set; }
        public DbSet<MusicCategory> MusicCategories { get; set; } // Optional
        public DbSet<Feedback> Feedbacks { get; set; }






    }
}
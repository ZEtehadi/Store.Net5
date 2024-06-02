using Clean_Architecture.Application.Interface.Contexts;
using Clean_Architecture.Common.Roles;
using Clean_Architecture.Domain.Entities.Carts;
using Clean_Architecture.Domain.Entities.DisCounts;
using Clean_Architecture.Domain.Entities.Finances;
using Clean_Architecture.Domain.Entities.HomePage;
using Clean_Architecture.Domain.Entities.Orders;
using Clean_Architecture.Domain.Entities.Products;
using Clean_Architecture.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Persistance.Contexts
{
    public class DataBaseContext:DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options):base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInRole> UserInRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<HomePageImages> HomePageImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<RequestPay> RequestPays { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
      public DbSet<DisCount> DisCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasOne(p => p.User)
                                       .WithMany(p => p.Orders)
                                       .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>().HasOne(p => p.RequestPay)
                                          .WithMany(p => p.Orders)
                                          .OnDelete(DeleteBehavior.NoAction);

            //Send data
            SendData(modelBuilder);

            //Set Email in User Entity to Index and Unique
            //Index=Search by Email vary Quickly
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();


            //so Default return All User's that thay have IsRemoved=0
            ApplyQueryFilter(modelBuilder);

        }

        private void SendData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = nameof(UserRoles.Admin) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 2, Name = nameof(UserRoles.Customer) });
            modelBuilder.Entity<Role>().HasData(new Role { Id = 3, Name = nameof(UserRoles.Operator) });

            modelBuilder.Entity<DisCount>().HasData(new DisCount { Id = 1, DisCountCode = "NULL DisCount", Percent = 0 });
        }
        

        private void ApplyQueryFilter(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Role>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<UserInRole>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductFeatures>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductImage>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Slider>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<HomePageImages>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Cart>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<CartItems>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<RequestPay>().HasQueryFilter(p => !p.IsRemoved);
          //  modelBuilder.Entity<DisCount>().HasQueryFilter(p => !p.IsRemoved);

        }

    }
}

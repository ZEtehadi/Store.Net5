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
using System.Threading;
using System.Threading.Tasks;

namespace Clean_Architecture.Application.Interface.Contexts
{
    public interface IDataBaseContext
    {
        //Set Entities
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<UserInRole> UserInRoles { get; set; }
        DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<HomePageImages> HomePageImages{ get; set; }
        public DbSet<Cart> Carts{ get; set; }
        public DbSet<CartItems> CartItems{ get; set; }
        public DbSet<RequestPay> RequestPays { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
      public DbSet<DisCount> DisCounts { get; set; }

        //Set Method

        int SaveChanges(bool acceptAllChangesOnSuccess);
        int SaveChanges();

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken());
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());





    }
}

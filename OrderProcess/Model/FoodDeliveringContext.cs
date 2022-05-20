using FoodDeleveryApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcess.Model
{
    public class FoodDeliveringContext : DbContext
    {
        private readonly string _conString;

        public FoodDeliveringContext()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                 .AddJsonFile("appsettings.json", true, true)
                 .Build();

            _conString = configuration.GetConnectionString("MyDatabase");
        }

        public FoodDeliveringContext(DbContextOptions<FoodDeliveringContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conString);
            /* if (!optionsBuilder.IsConfigured)
             {
 #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                 optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FoodDelivery;uid=tester;pwd=123;");
             }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           /* OnModelCreatingPartial(modelBuilder);*/
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

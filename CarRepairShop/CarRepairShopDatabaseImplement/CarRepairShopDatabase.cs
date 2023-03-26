using CarRepairShopDatabaseImplement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CarRepairShopDatabaseImplement
{
    internal class CarRepairShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-SINQU55\SQLEXPRESS;Initial Catalog=FoodOrdersDatabase;Integrated Security=True;MultipleActiveResultSets=True;;TrustServerCertificate=True");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Car> Cars { set; get; }

        public virtual DbSet<Detail> Details { set; get; }

        public virtual DbSet<DetailComponent> DetailComponents { set; get; }

        public virtual DbSet<Order> Orders { set; get; }
    }
}

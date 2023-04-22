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
                optionsBuilder.UseNpgsql(@"Host = localhost; Port = 5432; Database = CarRepairShop; Username = postgres; Password = 1");
            }
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Car> Cars { set; get; }

        public virtual DbSet<Detail> Details { set; get; }

        public virtual DbSet<CarDetail> DetailComponents { set; get; }

        public virtual DbSet<Order> Orders { set; get; }
    }
}

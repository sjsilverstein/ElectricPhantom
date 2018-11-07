using Microsoft.EntityFrameworkCore;
using ElectricPhantom.Models;

namespace ElectricPhantom.Context
{
    public class ElectricPhantomContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        
        public ElectricPhantomContext(DbContextOptions<ElectricPhantomContext> options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Order> Orders {get; set;}
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<OrderUnitList> OrderUnitLists { get; set; }

        public DbSet<Catagory> Catagories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Unit> Units { get; set; }

        
    }
}
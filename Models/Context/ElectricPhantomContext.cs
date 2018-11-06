using Microsoft.EntityFrameworkCore;
using ElectricPhantom.Models;

namespace ElectricPhantom.Context
{
    public class ElectricPhantomContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        
        public ElectricPhantomContext(DbContextOptions<ElectricPhantomContext> options) : base(options) { }
        
        public DbSet<User> Users {get;set;}
    }
}
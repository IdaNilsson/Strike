using Microsoft.EntityFrameworkCore;
using Strike.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strike.Data
{
    public class StrikeContext : DbContext
    {
        public StrikeContext(DbContextOptions<StrikeContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; } 
    }
}

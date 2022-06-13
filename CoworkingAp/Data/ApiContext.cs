using Microsoft.EntityFrameworkCore;
using CoworkingAp.Models;

namespace CoworkingAp.Data

{
    public class ApiContext : DbContext
    {
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
        {

        }
    }
}

  

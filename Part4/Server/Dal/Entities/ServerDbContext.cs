using Dal.Entities;
using Microsoft.EntityFrameworkCore;


namespace Dal.Entity
{
    public class ServerDbContext:DbContext
    {
 
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<SupplierEntity> Suppliers { get; set; }
        public DbSet<ItemEntity> Items { get; set; }
        public DbSet<StatusEntity> Status { get; set; }
        public ServerDbContext(DbContextOptions<ServerDbContext> options) : base(options)
        { }
        

    }
}

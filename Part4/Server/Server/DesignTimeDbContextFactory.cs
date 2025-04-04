
using Dal.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ServerDbContext>
{
    public ServerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ServerDbContext>();
        optionsBuilder.UseSqlServer("server=SHIRA; database=storeDB; Trusted_Connection=True;TrustServerCertificate=True;");

        return new ServerDbContext(optionsBuilder.Options);
    }
}

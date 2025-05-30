using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Program3
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TransportSystemContext>
    {
        public TransportSystemContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TransportSystemContext>();
            optionsBuilder.UseSqlite("Data Source=transport.db");

            return new TransportSystemContext(optionsBuilder.Options);
        }
    }
}
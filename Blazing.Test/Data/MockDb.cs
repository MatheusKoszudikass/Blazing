using Blazing.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blazing.Test.Data
{
    public class MockDb : IDbContextFactory<BlazingDbContext>
    {
        public BlazingDbContext CreateDbContext()
        {

            //var options = new DbContextOptionsBuilder<BlazingDbContext>()
            //    .UseInMemoryDatabase($"InMemoryTest{DateTime.Now.ToFileTime()}").Options;


            var options = new DbContextOptionsBuilder<BlazingDbContext>()
                .UseSqlServer($"Server=DESKTOP-QRUO464\\SQLEXPRESS;Database=Blazing;User Id=sa;Password=root;TrustServerCertificate=True").Options;


            return new BlazingDbContext(options);
        }
    }
}

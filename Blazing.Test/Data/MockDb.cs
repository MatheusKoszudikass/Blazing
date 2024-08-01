using Blazing.infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Blazing.Test.Data
{
    public class MockDb : IDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext()
        {

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase($"InMemoryTest{DateTime.Now.ToFileTime()}").Options;


            return new AppDbContext(options);
        }
    }
}

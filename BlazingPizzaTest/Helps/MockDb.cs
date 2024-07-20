using BlazingPizza.Api.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazingPizzaTest.Helps
{
    public class MockDb : IDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase($"InMemoryTest{DateTime.Now.ToFileTimeUtc()}").Options;

            return new AppDbContext(options);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMyShop
{
    public class DatabaseFixture:IDisposable
    {
        public MyShop328264650Context Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<MyShop328264650Context>()
                .UseSqlServer("Server=srv2\\pupils;Database=Test328264650;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;
            Context = new MyShop328264650Context(options);
           Context.Database.EnsureCreated();

        }
        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}

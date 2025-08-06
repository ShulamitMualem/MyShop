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
        public MyShopContext Context { get; private set; }

        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<MyShopContext>()
                .UseSqlServer("Server=srv2\\pupils;Database=Test328264650;Trusted_Connection=True;TrustServerCertificate=True")
                .Options;
            Context = new MyShopContext(options);
           Context.Database.EnsureCreated();

        }
        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }
}

using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace test_sqlite
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var optsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            optsBuilder.UseSqlite(DesignTimeDatabaseContextFactory.CONNECTION_STRING, x => x.UseNetTopologySuite());

            using var db = new DatabaseContext(optsBuilder.Options);

            await db.Database.MigrateAsync();

            var geoFactory = NtsGeometryServices.Instance.CreateGeometryFactory(4326);

            var marker = await db.Markers.AddAsync(new Marker()
            {
                Id = Guid.NewGuid().ToString("D"),
                Location = geoFactory.CreatePoint(new Coordinate(10, -1))
            });

            await db.SaveChangesAsync();
        }
    }

    public class Marker
    {
        public string Id { get; set; }
        public Point Location { get; set; }
    }

    public class DatabaseContext : DbContext
    {
        public DbSet<Marker> Markers { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(DatabaseContext)));
        }
    }

    public class DesignTimeDatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public static readonly string CONNECTION_STRING = "Data Source=testing.db";

        public DatabaseContext CreateDbContext(string[] args)
        {
            var optsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            optsBuilder.UseSqlite(CONNECTION_STRING, x => x.UseNetTopologySuite());

            return new DatabaseContext(optsBuilder.Options);
        }
    }
}

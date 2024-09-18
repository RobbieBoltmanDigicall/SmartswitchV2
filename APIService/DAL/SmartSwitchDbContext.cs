using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ClaimsService.DAL
{
    public class SmartSwitchDbContext(DbContextOptions<SmartSwitchDbContext> options) : DbContext(options)
    {
        public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }
        public virtual DbSet<BodyType> BodyTypes { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<DataType> DataTypes { get; set; }
        public virtual DbSet<MethodType> MethodTypes { get; set; }
        public virtual DbSet<ResponseBody> ResponseBodies { get; set; }
        public virtual DbSet<ResponseBodyParameter> ResponseBodyParameters { get; set; }
        public virtual DbSet<ResponseHeader> ResponseHeaders { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        public virtual DbSet<RouteBody> RouteBodies { get; set; }
        public virtual DbSet<RouteBodyParameter> RouteBodyParameters { get; set; }
        public virtual DbSet<RouteHeader> RouteHeaders { get; set; }
        public virtual DbSet<RouteParameter> RouteParameters { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<RouteType> RouteTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>()
                .HasOne(r => r.RouteBody)
                .WithOne(rb => rb.Route)
                .HasForeignKey<RouteBody>(rb => rb.Id);

            modelBuilder.Entity<Route>()
                .HasOne(r => r.MethodType)
                .WithOne(m => m.Route)
                .HasForeignKey<MethodType>(m => m.Id);

            modelBuilder.Entity<Route>()
                .HasOne(r => r.RouteType)
                .WithOne(rt => rt.Route)
                .HasForeignKey<RouteType>(rt => rt.Id);

            modelBuilder.Entity<RouteHeader>()
                .HasOne(rh => rh.Route)
                .WithMany(r => r.RouteHeaders)
                .HasForeignKey(rh => rh.Id);

            modelBuilder.Entity<RouteHeader>()
                .HasOne(rh => rh.DataType)
                .WithOne()
                .HasForeignKey<DataType>(d => d.Id);

            modelBuilder.Entity<RouteParameter>()
                .HasOne(rp => rp.Route)
                .WithMany(rp => rp.RouteParameters)
                .HasForeignKey(rp => rp.Id);

            modelBuilder.Entity<RouteParameter>()
                .HasOne(rh => rh.DataType)
                .WithOne()
                .HasForeignKey<DataType>(d => d.Id);

            modelBuilder.Entity<Route>()
                .HasOne(r => r.Response)
                .WithOne(r => r.Route)
                .HasForeignKey<Response>(r => r.Id);

            modelBuilder.Entity<RouteBodyParameter>()
                .HasOne(rp => rp.RouteBody)
                .WithMany(rp => rp.RouteBodyParameters)
                .HasForeignKey(rp => rp.Id);

            modelBuilder.Entity<RouteBodyParameter>()
                .HasOne(r => r.DataType)
                .WithOne()
                .HasForeignKey<DataType>(d => d.Id);

            modelBuilder.Entity<ResponseHeader>()
                .HasOne(r => r.DataType)
                .WithOne()
                .HasForeignKey<DataType>(d => d.Id);

            modelBuilder.Entity<ResponseBodyParameter>()
                .HasOne(r => r.DataType)
                .WithOne()
                .HasForeignKey<DataType>(d => d.Id);

            modelBuilder.Entity<ResponseHeader>()
                .HasOne(rh => rh.Response)
                .WithMany(r => r.ResponseHeaders)
                .HasForeignKey(rh => rh.Id);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Routes)
                .WithMany(e => e.Clients)
                .UsingEntity(
                    "ClientRoute",
                    l => l.HasOne(typeof(Route)).WithMany().HasForeignKey("RouteId").HasPrincipalKey(nameof(Route.Id)),
                    r => r.HasOne(typeof(Client)).WithMany().HasForeignKey("ClientId").HasPrincipalKey(nameof(Client.Id)),
                    j => j.HasKey("ClientId", "RouteId"));

            //TODO: Finish the relationships
        }
    }
}

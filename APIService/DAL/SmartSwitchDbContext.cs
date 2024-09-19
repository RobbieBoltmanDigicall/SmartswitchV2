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
                .HasForeignKey<RouteBody>(rb => rb.RouteId);

            modelBuilder.Entity<MethodType>()
                .HasOne(m => m.Route)
                .WithOne(r => r.MethodType)
                .HasForeignKey<Route>(r => r.MethodTypeId);

            modelBuilder.Entity<RouteType>()
                .HasOne(r => r.Route)
                .WithOne(rt => rt.RouteType)
                .HasForeignKey<Route>(rt => rt.RouteTypeId);

            modelBuilder.Entity<RouteHeader>()
                .HasOne(rh => rh.Route)
                .WithMany(r => r.RouteHeaders)
                .HasForeignKey(rh => rh.RouteId);

            modelBuilder.Entity<RouteHeader>()
                .HasOne(rh => rh.DataType)
                .WithOne()
                .HasForeignKey<DataType>(d => d.DataTypeId);

            modelBuilder.Entity<RouteParameter>()
                .HasOne(rp => rp.Route)
                .WithMany(rp => rp.RouteParameters)
                .HasForeignKey(rp => rp.RouteId);

            modelBuilder.Entity<RouteParameter>()
                .HasOne(rp => rp.DataType)
                .WithOne()
                .HasForeignKey<RouteParameter>(rp => rp.DataTypeId);

            modelBuilder.Entity<Route>()
                .HasOne(r => r.Response)
                .WithOne(r => r.Route)
                .HasForeignKey<Response>(r => r.ResponseId);

            modelBuilder.Entity<RouteBodyParameter>()
                .HasOne(rp => rp.RouteBody)
                .WithMany(rp => rp.RouteBodyParameters)
                .HasForeignKey(rp => rp.RouteBodyId);

            modelBuilder.Entity<RouteBodyParameter>()
                .HasOne(r => r.DataType)
                .WithOne()
                .HasForeignKey<DataType>(d => d.DataTypeId);

            modelBuilder.Entity<ResponseHeader>()
                .HasOne(r => r.DataType)
                .WithOne()
                .HasForeignKey<DataType>(d => d.DataTypeId);

            modelBuilder.Entity<ResponseBodyParameter>()
                .HasOne(r => r.DataType)
                .WithOne()
                .HasForeignKey<DataType>(d => d.DataTypeId);

            modelBuilder.Entity<ResponseHeader>()
                .HasOne(rh => rh.Response)
                .WithMany(r => r.ResponseHeaders)
                .HasForeignKey(rh => rh.ResponseHeaderId);

            modelBuilder.Entity<ResponseBody>()
                .HasOne(r => r.Response)
                .WithOne(rb => rb.ResponseBody)
                .HasForeignKey<ResponseBody>(r => r.ResponseId);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Routes)
                .WithMany(e => e.Clients)
                .UsingEntity(
                    "ClientRoute",
                    l => l.HasOne(typeof(Route)).WithMany().HasForeignKey("RouteId").HasPrincipalKey(nameof(Route.RouteId)),
                    r => r.HasOne(typeof(Client)).WithMany().HasForeignKey("ClientId").HasPrincipalKey(nameof(Client.ClientId)),
                    j => j.HasKey("ClientId", "RouteId"));

            //TODO: Finish the relationships
        }
    }
}

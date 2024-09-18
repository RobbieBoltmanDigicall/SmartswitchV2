using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ClaimsService.DAL
{
    public class SmartSwitchContext(DbContextOptions<SmartSwitchContext> options) : DbContext(options)
    {
        public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }
        public virtual DbSet<BodyType> BodyTypes { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<DataType> DataTypes { get; set; }
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
            modelBuilder.Entity<Client>()
                .HasMany(e => e.Routes)
                .WithMany(e => e.Clients)
                .UsingEntity(
                    "ClientRoute",
                    l => l.HasOne(typeof(Route)).WithMany().HasForeignKey("RouteId").HasPrincipalKey(nameof(Route.Id)),
                    r => r.HasOne(typeof(Client)).WithMany().HasForeignKey("ClientId").HasPrincipalKey(nameof(Client.Id)),
                    j => j.HasKey("ClientId", "RouteId"));
        }
    }
}

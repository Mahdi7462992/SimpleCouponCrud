using Microsoft.EntityFrameworkCore;
using SimpleCouponCrud.Models.BaseEntities;
using SimpleCouponCrud.Models.Entities;
using System.Reflection;


namespace SimpleCouponCrud.Models
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }

        public DbSet<Coupon> Coupon { get; set; }

        #region [- OnModelCreating(ModelBuilder modelBuilder) -]
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region [- ApplyConfigurationsFromAssembly() -]
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            #endregion

            #region [- RegisterAllEntities() -]
            modelBuilder.RegisterAllEntities<IDbSetEntity>(typeof(IDbSetEntity).Assembly);
            #endregion

            base.OnModelCreating(modelBuilder);

        }
        #endregion
    }
}

using Application.Interface.DbContext;
using Domain.Attributes;
using Domain.Catalogs;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityConfigurations;

namespace Persistence.Contexts
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // modelBuilder.Entity<User>().Property<DateTime>("");
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetCustomAttributes(typeof(AuditableAttribute), true).Length > 0)
                {
                    modelBuilder.Entity(entityType.Name).Property<DateTime>("InsertTime");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("UpdateTime");
                    modelBuilder.Entity(entityType.Name).Property<DateTime?>("RemoveTime");
                    modelBuilder.Entity(entityType.Name).Property<bool>("IsRemoved");

                }

            }
            modelBuilder.ApplyConfiguration(new CatalogBrandEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogTypeEntityConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {

            var modifiedEntities = ChangeTracker.Entries()
                .Where(p => p.State == EntityState.Modified ||
                p.State == EntityState.Added ||
                p.State == EntityState.Deleted);

            foreach (var item in modifiedEntities)
            {

                var entityType = item.Context.Model.FindEntityType(item.Entity.GetType());

                var inserted = entityType.FindProperty("InsertTime");
                var update = entityType.FindProperty("UpdateTime");
                var removedTime = entityType.FindProperty("RemoveTime");
                var isRemoved = entityType.FindProperty("IsRemoved");


                if (item.State == EntityState.Added && inserted != null)
                {
                    item.Property("InsertTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Modified && update != null)
                {
                    item.Property("UpdateTime").CurrentValue = DateTime.Now;
                }
                if (item.State == EntityState.Deleted && removedTime != null || isRemoved != null)
                {
                    item.Property("RemoveTime").CurrentValue = DateTime.Now;
                    item.Property("IsRemoved").CurrentValue = true;
                }
                //if (item.State == EntityState.Added && removedTime != null || isRemoved)
                //{
                //    item.Property("IsRemoved").CurrentValue = DateTime.Now;
                //}

            }
            return base.SaveChanges();
        }

    }
}

using Configuration.DataTransfer;
using Configuration.DataTransfer.ClientInfoSetup;
using Configuration.DataTransfer.VerticalInfoSetup;
using Configuration.DomainModel.ClientInfoSetup.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.Objects;

namespace Configuration.RepositoryHandler.MsSql.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Config_tblVerticalMaster> ConfigtblVerticalMaster { get; set; }

        public DbSet<Config_tblSBUMasterDto> Config_tblSBUMaster { get; set; }

        public DbSet<Config_tblClientSBUMapDto> Config_tblClientSBUMap { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GetBEClientInfoResponse>().HasNoKey();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.Now;
                        entry.Entity.ModifiedOn = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedOn = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}

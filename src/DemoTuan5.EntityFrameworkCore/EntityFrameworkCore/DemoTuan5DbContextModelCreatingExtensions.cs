using DemoTuan5.Countries;
using DemoTuan5.WarehouseLocations;
using DemoTuan5.Warehouses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace DemoTuan5.EntityFrameworkCore
{
    public static class DemoTuan5DbContextModelCreatingExtensions
    {
        public static void ConfigureDemoTuan5(
            this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(DemoTuan5DbProperties.DbTablePrefix + "Questions", DemoTuan5DbProperties.DbSchema);

                b.ConfigureByConvention();

                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
            if (builder.IsHostDatabase())
            {
                builder.Entity<Country>(b =>
                {
                    b.ToTable(DemoTuan5DbProperties.DbTablePrefix + "Countries", DemoTuan5DbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.HasIndex(x => x.Code).IsUnique();
                    b.Property(x => x.Code).HasColumnName(nameof(Country.Code)).IsRequired();
                    b.Property(x => x.Description).HasColumnName(nameof(Country.Description));
                });

            }
            if (builder.IsHostDatabase())
            {
                builder.Entity<Warehouse>(b =>
                {
                    b.ToTable(DemoTuan5DbProperties.DbTablePrefix + "Warehouses", DemoTuan5DbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.HasIndex(x => x.Code).IsUnique();
                    b.Property(x => x.Code).HasColumnName(nameof(Warehouse.Code)).IsRequired();
                    b.Property(x => x.Description).HasColumnName(nameof(Warehouse.Description));
                    b.Property(x => x.Active).HasColumnName(nameof(Warehouse.Active));
                });

            }
            if (builder.IsHostDatabase())
            {

            }
            if (builder.IsHostDatabase())
            {

            }
            if (builder.IsHostDatabase())
            {
                builder.Entity<WarehouseLocation>(b =>
                {
                    b.ToTable(DemoTuan5DbProperties.DbTablePrefix + "WarehouseLocations", DemoTuan5DbProperties.DbSchema);
                    b.ConfigureByConvention();
                    b.Property(x => x.Code).HasColumnName(nameof(WarehouseLocation.Code));
                    b.Property(x => x.Description).HasColumnName(nameof(WarehouseLocation.Description));
                    b.Property(x => x.Active).HasColumnName(nameof(WarehouseLocation.Active));
                    b.Property(x => x.Idx).HasColumnName(nameof(WarehouseLocation.Idx));
                    b.HasOne<Country>().WithMany().HasForeignKey(x => x.CountryId).OnDelete(DeleteBehavior.Restrict);
                    b.HasOne<Warehouse>().WithMany().HasForeignKey(x => x.WarehouseId).OnDelete(DeleteBehavior.Cascade);
                });

            }
        }
    }
}

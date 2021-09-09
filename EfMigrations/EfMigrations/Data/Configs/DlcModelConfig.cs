using EfMigrations.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfMigrations.Data
{
    class DlcModelConfig : IEntityTypeConfiguration<DLC>
    {
        public void Configure(EntityTypeBuilder<DLC> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(74).HasColumnName("DLCName");
        }
    }
}

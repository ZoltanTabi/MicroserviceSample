using MicroserviceSample.CommandService.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceSample.CommandService.Persistance.Configurations;

public class PlatformConfiguration : IEntityTypeConfiguration<Platform>
{
    public void Configure(EntityTypeBuilder<Platform> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ExternalId)
            .IsRequired();

        builder.Property(p => p.Name)
            .IsRequired();
    }
}

using MicroserviceSample.CommandService.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceSample.CommandService.Persistance.Configurations;

public class CommandConfiguration : IEntityTypeConfiguration<Command>
{
    public void Configure(EntityTypeBuilder<Command> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.HowTo)
            .IsRequired();

        builder.Property(c => c.CommandLine)
            .IsRequired();

        builder.HasOne(c => c.Platform)
            .WithMany(p => p.Commands)
            .HasForeignKey(c => c.PlatformId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

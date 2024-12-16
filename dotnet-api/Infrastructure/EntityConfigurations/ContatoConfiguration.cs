using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ContatoConfiguration : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.HasOne(c => c.Area)
                .WithMany()
                .HasForeignKey(c => c.CodigoArea)
                .HasPrincipalKey(a=>a.Codigo)
                .IsRequired();
        }
    }
}

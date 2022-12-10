using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal.Domain.Entities;

namespace Minimal.Infrastructure.Contexts.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.OwnsOne(x => x.Name)
                .Ignore(x => x.Notifications)
                .Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .IsRequired(true);

            builder.OwnsOne(x => x.Name)
                .Ignore(x => x.Notifications)
                .Property(x => x.LastName)
                .HasColumnName("LastName")
                .IsRequired(true);

            builder.Ignore(x => x.Notifications);
        }
    }
}

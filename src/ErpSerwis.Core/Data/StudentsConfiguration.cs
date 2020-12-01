using ErpSerwis.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSerwis.Core.Data
{
    internal class StudentsConfiguration : IEntityTypeConfiguration<Students>
    {
        public void Configure(EntityTypeBuilder<Students> builder)
        {
            builder.HasIndex(e => e.Id)
                .HasDatabaseName("IX_StudentsId")
                .IsUnique(true);

            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            builder.HasIndex(e => e.Name)
                .HasDatabaseName("IX_StudentsName")
                .IsUnique(false);

            builder.HasIndex(e => e.Surname)
                .HasDatabaseName("IX_StudentsSurname")
                .IsUnique(false);

            builder.HasIndex(e => e.IndexNumber)
                .HasDatabaseName("IX_StudentsIndexNumber")
                .IsUnique(false);

            builder.HasIndex(e => e.DateOfBirth)
                .HasDatabaseName("IX_StudentsDateOfBirth")
                .IsUnique(false);
        }
    }
}

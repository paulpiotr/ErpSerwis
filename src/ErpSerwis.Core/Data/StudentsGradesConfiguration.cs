using ErpSerwis.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ErpSerwis.Core.Data
{
    internal class StudentsGradesConfiguration : IEntityTypeConfiguration<StudentsGrades>
    {
        public void Configure(EntityTypeBuilder<StudentsGrades> builder)
        {
            builder.HasIndex(e => e.Id)
                .HasDatabaseName("IX_StudentsGradesId")
                .IsUnique(true);

            builder.Property(e => e.Id).HasDefaultValueSql("(newsequentialid())");

            builder.HasIndex(e => e.StudentId)
                .HasDatabaseName("IX_StudentsGradesStudentId")
                .IsUnique(false);
        }
    }
}

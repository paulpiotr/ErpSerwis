﻿// <auto-generated />
using System;
using ErpSerwis.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ErpSerwis.Core.Migrations
{
    [DbContext(typeof(ErpSerwisDbContext))]
    partial class ErpSerwisDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("ErpSerwis.Core.Models.Students", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime")
                        .HasColumnName("DateOfBirth");

                    b.Property<string>("IndexNumber")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("IndexNumber");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("Name");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("varchar(32)")
                        .HasColumnName("Surname");

                    b.HasKey("Id");

                    b.HasIndex("DateOfBirth")
                        .HasDatabaseName("IX_StudentsDateOfBirth");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("IX_StudentsId");

                    b.HasIndex("IndexNumber")
                        .HasDatabaseName("IX_StudentsIndexNumber");

                    b.HasIndex("Name")
                        .HasDatabaseName("IX_StudentsName");

                    b.HasIndex("Surname")
                        .HasDatabaseName("IX_StudentsSurname");

                    b.ToTable("Students", "esc");
                });

            modelBuilder.Entity("ErpSerwis.Core.Models.StudentsGrades", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(newsequentialid())");

                    b.Property<decimal>("Grade")
                        .HasColumnType("money")
                        .HasColumnName("Grade");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("IX_StudentsGradesId");

                    b.HasIndex("StudentId")
                        .HasDatabaseName("IX_StudentsGradesStudentId");

                    b.ToTable("StudentsGrades", "esc");
                });

            modelBuilder.Entity("ErpSerwis.Core.Models.StudentsGrades", b =>
                {
                    b.HasOne("ErpSerwis.Core.Models.Students", "Students")
                        .WithMany("StudentsGrades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Students");
                });

            modelBuilder.Entity("ErpSerwis.Core.Models.Students", b =>
                {
                    b.Navigation("StudentsGrades");
                });
#pragma warning restore 612, 618
        }
    }
}

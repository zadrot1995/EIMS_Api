// <auto-generated />
using System;
using API.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CuratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("InstituteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CuratorId");

                    b.HasIndex("InstituteId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Domain.Models.Institute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UniversityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.ToTable("Institutes");
                });

            modelBuilder.Entity("Domain.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Domain.Models.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LecturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PractitionerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LecturerId");

                    b.HasIndex("PractitionerId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Domain.Models.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Degree")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Education")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("InstituteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("InstituteId");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("Domain.Models.University", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("About")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Universities");
                });

            modelBuilder.Entity("Domain.Models.Group", b =>
                {
                    b.HasOne("Domain.Models.Teacher", "Curator")
                        .WithMany()
                        .HasForeignKey("CuratorId");

                    b.HasOne("Domain.Models.Institute", null)
                        .WithMany("Groups")
                        .HasForeignKey("InstituteId");

                    b.HasOne("Domain.Models.Subject", null)
                        .WithMany("Groups")
                        .HasForeignKey("SubjectId");

                    b.Navigation("Curator");
                });

            modelBuilder.Entity("Domain.Models.Institute", b =>
                {
                    b.HasOne("Domain.Models.University", null)
                        .WithMany("Institutes")
                        .HasForeignKey("UniversityId");
                });

            modelBuilder.Entity("Domain.Models.Student", b =>
                {
                    b.HasOne("Domain.Models.Group", null)
                        .WithMany("Students")
                        .HasForeignKey("GroupId");
                });

            modelBuilder.Entity("Domain.Models.Subject", b =>
                {
                    b.HasOne("Domain.Models.Teacher", "Lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerId");

                    b.HasOne("Domain.Models.Teacher", "Practitioner")
                        .WithMany()
                        .HasForeignKey("PractitionerId");

                    b.Navigation("Lecturer");

                    b.Navigation("Practitioner");
                });

            modelBuilder.Entity("Domain.Models.Teacher", b =>
                {
                    b.HasOne("Domain.Models.Institute", null)
                        .WithMany("Teachers")
                        .HasForeignKey("InstituteId");
                });

            modelBuilder.Entity("Domain.Models.Group", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Domain.Models.Institute", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("Domain.Models.Subject", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("Domain.Models.University", b =>
                {
                    b.Navigation("Institutes");
                });
#pragma warning restore 612, 618
        }
    }
}

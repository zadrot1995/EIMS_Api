﻿// <auto-generated />
using System;
using API.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220508125655_AddInstituteIDImageContent2")]
    partial class AddInstituteIDImageContent2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("CuratorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("InstituteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CuratorId");

                    b.HasIndex("InstituteId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Domain.Models.ImageContent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("InstituteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UniversityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InstituteId");

                    b.HasIndex("UniversityId");

                    b.ToTable("ImageContents");
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

                    b.Property<Guid>("UniversityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.ToTable("Institutes");
                });

            modelBuilder.Entity("Domain.Models.Mark", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MarkType")
                        .HasColumnType("int");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("Domain.Models.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImageLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UniversityId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UniversityId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Domain.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GroupId")
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

                    b.Property<Guid>("InstituteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LecturerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PractitionerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InstituteId");

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

                    b.Property<Guid>("InstituteId")
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

            modelBuilder.Entity("GroupSubject", b =>
                {
                    b.Property<Guid>("GroupsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubjectsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GroupsId", "SubjectsId");

                    b.HasIndex("SubjectsId");

                    b.ToTable("GroupSubject");
                });

            modelBuilder.Entity("Domain.Models.Group", b =>
                {
                    b.HasOne("Domain.Models.Teacher", "Curator")
                        .WithMany()
                        .HasForeignKey("CuratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Institute", "Institute")
                        .WithMany("Groups")
                        .HasForeignKey("InstituteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curator");

                    b.Navigation("Institute");
                });

            modelBuilder.Entity("Domain.Models.ImageContent", b =>
                {
                    b.HasOne("Domain.Models.Institute", null)
                        .WithMany("ImageContents")
                        .HasForeignKey("InstituteId");

                    b.HasOne("Domain.Models.University", null)
                        .WithMany("ImageContents")
                        .HasForeignKey("UniversityId");
                });

            modelBuilder.Entity("Domain.Models.Institute", b =>
                {
                    b.HasOne("Domain.Models.University", "University")
                        .WithMany("Institutes")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("Domain.Models.Mark", b =>
                {
                    b.HasOne("Domain.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Domain.Models.Post", b =>
                {
                    b.HasOne("Domain.Models.University", "University")
                        .WithMany("Posts")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("Domain.Models.Student", b =>
                {
                    b.HasOne("Domain.Models.Group", null)
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Subject", b =>
                {
                    b.HasOne("Domain.Models.Institute", "Institute")
                        .WithMany("Subjects")
                        .HasForeignKey("InstituteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Teacher", "Lecturer")
                        .WithMany()
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Teacher", "Practitioner")
                        .WithMany()
                        .HasForeignKey("PractitionerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institute");

                    b.Navigation("Lecturer");

                    b.Navigation("Practitioner");
                });

            modelBuilder.Entity("Domain.Models.Teacher", b =>
                {
                    b.HasOne("Domain.Models.Institute", "Institute")
                        .WithMany("Teachers")
                        .HasForeignKey("InstituteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Institute");
                });

            modelBuilder.Entity("GroupSubject", b =>
                {
                    b.HasOne("Domain.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.Group", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Domain.Models.Institute", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("ImageContents");

                    b.Navigation("Subjects");

                    b.Navigation("Teachers");
                });

            modelBuilder.Entity("Domain.Models.University", b =>
                {
                    b.Navigation("ImageContents");

                    b.Navigation("Institutes");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryLayer.Services;

namespace RepositoryLayer.Migrations
{
    [DbContext(typeof(FunDoContext))]
    [Migration("20220906130353_Label")]
    partial class Label
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RepositoryLayer.Services.Entities.Label", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("NoteID")
                        .HasColumnType("int");

                    b.Property<string>("LabelName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "NoteID");

                    b.HasIndex("NoteID");

                    b.ToTable("Labels");
                });

            modelBuilder.Entity("RepositoryLayer.Services.Entities.Note", b =>
                {
                    b.Property<int>("NoteID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Colour")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Reminder")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("createdDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isArchive")
                        .HasColumnType("bit");

                    b.Property<bool>("isPin")
                        .HasColumnType("bit");

                    b.Property<bool>("isReminder")
                        .HasColumnType("bit");

                    b.Property<bool>("isTrash")
                        .HasColumnType("bit");

                    b.Property<DateTime>("modifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("NoteID");

                    b.HasIndex("UserId");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("RepositoryLayer.Services.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("modifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("email")
                        .IsUnique()
                        .HasFilter("[email] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RepositoryLayer.Services.Entities.Label", b =>
                {
                    b.HasOne("RepositoryLayer.Services.Entities.Note", "Note")
                        .WithMany()
                        .HasForeignKey("NoteID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RepositoryLayer.Services.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Note");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepositoryLayer.Services.Entities.Note", b =>
                {
                    b.HasOne("RepositoryLayer.Services.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}

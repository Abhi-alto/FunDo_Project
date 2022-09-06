using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FunDoContext : DbContext
    {
        public FunDoContext(DbContextOptions option) : base(option)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Label> Labels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasIndex(u => u.email)
             .IsUnique();

            modelBuilder.Entity<Label>()
               .HasKey(p => new { p.UserId, p.NoteID });

            modelBuilder.Entity<Label>()
              .HasOne(u => u.User)
              .WithMany()
              .HasForeignKey(u => u.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Label>()
            .HasOne(n => n.Note)
                .WithMany()
                .HasForeignKey(n => n.NoteID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
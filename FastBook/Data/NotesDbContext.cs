using FastBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Intrinsics.Arm;

namespace FastBook.Data
{
    public class NotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tag>().ToTable("Tags");

            modelBuilder.Entity<Note>()
                .HasMany(n => n.Tags)
                .WithMany(t => t.Notes)
                .UsingEntity(j => j.ToTable("NoteTags"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SAVE");
         
            string dbPath = Path.Combine(folderPath, "notes.db");
           
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}

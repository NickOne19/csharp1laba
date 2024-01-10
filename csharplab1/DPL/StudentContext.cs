using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp3.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp3.DPL
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null;
        public DbSet<Group> Groups { get; set; } = null;
        public DbSet<Direction> Directions { get; set; } = null;
        public DbSet<Course> Courses { get; set; } = null;

        public StudentContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(x => x.StudentName).HasMaxLength(50);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;
                                         Database = StudentDb;
                                         Trusted_Connection = true");
        }
    }
}

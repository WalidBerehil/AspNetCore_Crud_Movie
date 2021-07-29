using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryMovie>()
                .HasKey(cm => new { cm.CategoryId, cm.MovieId });
            modelBuilder.Entity<CategoryMovie>()
                .HasOne(cm => cm.category)
                .WithMany(c => c.movies)
                .HasForeignKey(cm => cm.CategoryId);
            modelBuilder.Entity<CategoryMovie>()
                .HasOne(cm => cm.movie)
                .WithMany(m => m.categories)
                .HasForeignKey(cm => cm.MovieId);

        }

        public DbSet<MvcMovie.Models.Movie> Movie { get; set; }

        public DbSet<MvcMovie.Models.Actor> Actor { get; set; }

        public DbSet<MvcMovie.Models.Category> Category { get; set; }
        public DbSet<MvcMovie.Models.CategoryMovie> CategoryMovie { get; set; }
    }
}

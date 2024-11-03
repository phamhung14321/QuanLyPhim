using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Entities
{
    public partial class MovieContext : DbContext
    {
        public MovieContext()
            : base("name=MovieContext")
        {
        }

        public virtual DbSet<Actors> Actors { get; set; }
        public virtual DbSet<Directors> Directors { get; set; }
        public virtual DbSet<Genres> Genres { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }
        public virtual DbSet<Studios> Studios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actors>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Actors)
                .Map(m => m.ToTable("MovieActors").MapLeftKey("ActorId").MapRightKey("MovieId"));

            modelBuilder.Entity<Directors>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Directors)
                .Map(m => m.ToTable("MovieDirectors").MapLeftKey("DirectorId").MapRightKey("MovieId"));

            modelBuilder.Entity<Genres>()
                .HasMany(e => e.Movies)
                .WithMany(e => e.Genres)
                .Map(m => m.ToTable("MovieGenres").MapLeftKey("GenreId").MapRightKey("MovieId"));

            modelBuilder.Entity<Movies>()
                .HasMany(e => e.Studios)
                .WithMany(e => e.Movies)
                .Map(m => m.ToTable("MovieStudios").MapLeftKey("MovieId").MapRightKey("StudioId"));
        }
    }
}

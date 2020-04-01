using Microsoft.EntityFrameworkCore;

namespace FilmAdatbazis
{
    public partial class MovieCatalogContext : DbContext
    {
        public MovieCatalogContext()
        {
        }

        public MovieCatalogContext(DbContextOptions<MovieCatalogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DirectorsModel> Directors { get; set; }
        public virtual DbSet<GenreModel> Genre { get; set; }
        public virtual DbSet<MovieModel> Movie { get; set; }
        public virtual DbSet<UsersModel> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MovieCatalog");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DirectorsModel>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("C_UNIQUE_DIRID")
                    .IsUnique();

                entity.Property(e => e.Director)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GenreModel>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("C_UNIQUE_GENREID")
                    .IsUnique();

                entity.Property(e => e.Genre)
                    .IsRequired()
                    .HasColumnName("Genre")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MovieModel>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("C_UNIQUE_MOVIEID")
                    .IsUnique();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.HasOne(d => d.Director)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.DirectorId)
                    .HasConstraintName("FK_Movie_Director");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_Movie_Genre");
            });

            modelBuilder.Entity<UsersModel>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("C_UNIQUE_USERID")
                    .IsUnique();

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

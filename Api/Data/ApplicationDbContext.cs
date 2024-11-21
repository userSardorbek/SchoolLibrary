using Microsoft.EntityFrameworkCore;
using WebApplication1.Model;

namespace WebApplication1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<User> User { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<BorrowRecord> BorrowRecords { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<TransactionHistory> TransactionActions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<BookGenre>(x => x.HasKey(p => new { p.BookId, p.GenreId }));

        modelBuilder.Entity<BookGenre>()
            .HasOne(u => u.Book)
            .WithMany(u => u.BookGenres)
            .HasForeignKey(p => p.BookId);

        modelBuilder.Entity<BookGenre>()
            .HasOne(u => u.Genre)
            .WithMany(u => u.BookGenres)
            .HasForeignKey(p => p.GenreId);
        
        
        modelBuilder.Entity<User>()
            .HasMany(u => u.UserHistories)
            .WithOne(t => t.User)
            .HasForeignKey(t => t.UserId);
    }
}
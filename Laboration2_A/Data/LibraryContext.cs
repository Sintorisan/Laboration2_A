using Laboration2_A.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Laboration2_A.Data;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<Loan> Loans { get; set; }

    public LibraryContext(DbContextOptions options) : base(options) { }

}

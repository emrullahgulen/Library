using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Concrete.EntityFramework
{
    public class LibraryDbContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookTransaction> BorrowedBooks { get; set; }

        private readonly IConfiguration Configuration;

        public LibraryDbContext() : base()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            object value = options.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnection"));
        }
    }
}
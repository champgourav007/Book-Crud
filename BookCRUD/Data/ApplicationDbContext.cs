using Microsoft.EntityFrameworkCore;
using BookCRUD.Models;

namespace BookCRUD.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BookViewModel> Books { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using dbEF.BBL.model;

namespace dbEF.dbconfig
{
    public class AppContext : DbContext
    {
        // Объекты таблицы Users
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
       
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS03;Database=homework;TrustServerCertificate=True;Trusted_Connection=True;");
        }
    }
}

using dbEF.dbconfig;
using dbEF.BBL.model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace dbEF.DAL.Repositories
{
    public class BookRepository
    {
        // 25.3.5 выбор объекта из БД по его идентификатору
        public static Book GetBookById(DbSet<Book> Books, int id)
        {
            return Books.FirstOrDefault(x => x.Id == id);
        }
        // 25.3.5 выбор всех объектов
        public static List<Book> GetBookById(DbSet<Book> Books)
        {
            return Books.Include(n=>n.Authors).Include(n=>n.Genres).Include(n=>n.Users).ToList();
        }
        // 25.3.5 добавление объекта в БД 
        public static void AddBook(dbconfig.AppContext db, Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }
        // 25.3.5 Удаление объекта из БД 
        public static void DeleteBookById(dbconfig.AppContext db, int id)
        {
            db.Books.Remove(GetBookById(db.Books, id));
            db.SaveChanges();
        }
        // 25.3.5 обновление года выпуска книги(по Id)
        public static void UpdateBookYearById(dbconfig.AppContext db, int id, int year)
        {
            Book book = GetBookById(db.Books, id);
            book.Year = year;
            db.Books.Update(book);
            db.SaveChanges();
        }


        // 25.5.4 Получать количество книг определенного автора в библиотеке.
        public static int QuantityBooksAutor (dbconfig.AppContext db, Author author)
        {
            return db.Books.Include(u => u.Authors).Where(n => n.Authors.Name == author.Name).Count();
        }
        // перегрузка метода выше
        public static int QuantityBooksAutor(dbconfig.AppContext db, string authorName)
        {
            return db.Books.Include(u => u.Authors).Where(n => n.Authors.Name.ToLower() == authorName.ToLower()).Count();
        }
        // 25.5.4 Получать список книг определенного жанра и вышедших между определенными годами
        public static List<Book> SearchByTimePeriodAndGenre (dbconfig.AppContext db, int Year1, int Year2, string genrename)
        {

            return db.Genres.Include(u => u.Books).FirstOrDefault(n => n.NameGenre == genrename).Books.Where(u => (u.Year >= Year1) & (u.Year <= Year2)).ToList();
        }
        // 25.5.4 Получать количество книг определенного жанра в библиотеке.
        public static int QuantityBookByGenre (dbconfig.AppContext db, string genrename)
        {

           return db.Genres.Include(u => u.Books).FirstOrDefault(n => n.NameGenre == genrename).Books.Count();
          
        }
        // 25.5.4 Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.
        public static bool IsBookByAuthorAndNameinLibrary (dbconfig.AppContext db, string author, string name)
        {

            return db.Books.Any(x => x.Authors.Name == author.ToUpper() & x.Title.ToUpper() == name.ToUpper()& x.UserId == null);
        }
        // 25.5.4 Получать булевый флаг о том, есть ли определенная книга на руках у пользователя.
        public static bool IsBookOnHeadsinUser (dbconfig.AppContext db, string name)
        {
            return db.Books.Any(x => x.Title.ToUpper() == name.ToUpper()& x.UserId != null);
        }
        // 25.5.4 Получать количество книг на руках у пользователя.
        public static int BooksTaskenByUser(dbconfig.AppContext db, string userName)
        {
            return db.Books.Include(n => n.Authors).Include(n => n.Genres).Include(n => n.Users).Where(book => book.Users.Name == userName).Count();
        }
        // 25.5.4 Получение последней вышедшей книги.
        public static Book LastBookByYear(dbconfig.AppContext db)
        {
            return db.Books.OrderBy(book => book.Year).LastOrDefault();
        }
        // 25.5.4 Получение списка всех книг, отсортированного в алфавитном порядке по названию.
        public static List <Book> SortBookByName (DbSet<Book> Books)
        {
            return Books.Include(b => b.Genres).Include(b => b.Authors).Include(b => b.Users).OrderBy (b =>b.Title).ToList();
        }
        // 25.5.4 Получение списка всех книг, отсортированного в порядке убывания года их выхода.
        public static List<Book> SortBookByYear(DbSet<Book> Books)
        {
            return Books.Include(b => b.Genres).Include(b => b.Authors).Include(b => b.Users).OrderByDescending(b => b.Year).ToList();
        }
    }
}

using dbEF.BBL.model;
using dbEF.DAL.Repositories;

public class Programm
{
    /*25.4.3 реализовал связь 
     * книга  - жанр: Многие ко многим
     * Автор - книга: один ко многим (1 автор много книг)
     * Пользователь - Книга: один ко многим (1 пользователь, несколько книг) UserID может быть Null т. к. книга может быть в библиотеке
     * исключения в данном задании не ловил, из за нехватки времени
     */
    static void Main(string[] args)
    {
        using (var db = new dbEF.dbconfig.AppContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Book Book1 = new Book { Title = "Белый клык", Year = 1906 };
            Book Book2 = new Book { Title = "Морской волк", Year = 1904 };
            Book Book3 = new Book { Title = "Смок Беллью", Year = 1912 };
            Book Book4 = new Book { Title = "Три мушкитера", Year = 1844 };
            Book Book5 = new Book { Title = "Граф Монте-Кристо", Year = 1845 };
            Book Book6 = new Book { Title = "Провинциалка", Year = 1851 };
            Book Book7 = new Book { Title = "Нахлебник", Year = 1856 };

            Author Author1 = new Author { Name = "Джек Лондон" };
            Author Author2 = new Author { Name = "Дюма" };
            Author Author3 = new Author { Name = "Тургенев" };

            Genre Genre1 = new Genre { NameGenre = "novel" };
            Genre Genre2 = new Genre { NameGenre = "history" };
            Genre Genre3 = new Genre { NameGenre = "fantasy" };

            User User1 = new User { Name = "User1", Email = "gmail@gmail.com" };
            User User2 = new User { Name = "User2", Email = "gmail2@gmail.com" };
            User User3 = new User { Name = "User2", Email = "gmail3@gmail.com" };

            Author1.Books.AddRange(new[] { Book1, Book2, Book3 });
            Author2.Books.AddRange(new[] { Book4, Book5 });
            Author3.Books.AddRange(new[] { Book6, Book7 });

            User1.Books.AddRange(new[] { Book1, Book3 });
            User2.Books.AddRange(new[] { Book4,  Book5 });
            User3.Books.AddRange(new[] { Book6, Book7 });

            Genre1.Books.AddRange(new[] { Book1, Book2, Book3 });
            Genre2.Books.AddRange(new[] { Book4, Book5 });
            Genre3.Books.AddRange(new[] { Book1, Book2, Book3 });
            Book6.Genres.Add(Genre2);
            Book7.Genres.Add(Genre3);

            db.Books.AddRange(Book1, Book2, Book3, Book4, Book5, Book6, Book7);
            db.Authors.AddRange(Author1, Author2, Author3);
            db.Genres.AddRange(Genre1, Genre2, Genre3);
            db.Users.AddRange(User1, User2, User3);
            db.SaveChanges();

        }

        using (var db = new dbEF.dbconfig.AppContext())
        {
            UserRepository.UpdateUserNameById(db, 1, "Anton");
            Book askBook = BookRepository.GetBookById(db.Books, 1);
            Console.WriteLine(askBook.Title);
            Console.WriteLine(BookRepository.QuantityBooksAutor(db, "ДЮмА"));
           List <Book> Genres = BookRepository.SearchByTimePeriodAndGenre(db, 1845, 1915, "history");// в даты выхода не попала книга Три мужкитера, жанра история
            foreach (Book book in Genres)
            {
                Console.WriteLine(book.Title+book.Year);
            }
            Console.WriteLine(BookRepository.QuantityBookByGenre (db, "history"));
            // т. к. книга 2 не присвоена ни одному пользователю UserId = Null, вывод на консоль true
            Console.WriteLine(BookRepository.IsBookByAuthorAndNameinLibrary(db, "Джек Лондон", "Морской волк"));
            //false
            Console.WriteLine(BookRepository.IsBookByAuthorAndNameinLibrary(db, "Джек Лондон", "Смок Беллью"));
            Console.WriteLine(BookRepository.IsBookOnHeadsinUser(db,  "Смок Беллью"));//на руках у у Антона (пользователь1)
            Console.WriteLine(BookRepository.IsBookOnHeadsinUser(db, "Морской волк"));// в библиотеке
            Console.WriteLine(BookRepository.BooksTaskenByUser(db, "Anton"));
            Console.WriteLine(BookRepository.LastBookByYear(db).Year);//вывожу год выхода последней вышедшей книги внесенной в бд
            List<Book> SortByTitle = BookRepository.SortBookByName(db.Books);//сортировка по наименованию книг вывожу название и год
            foreach (Book book in SortByTitle)
            {
                Console.WriteLine(book.Title + book.Year);
            }
            Console.WriteLine();
            List<Book> SortByYear = BookRepository.SortBookByYear(db.Books);//сортировка по Date книг вывожу название и год
            foreach (Book book in SortByYear)
            {
                Console.WriteLine(book.Title + book.Year);
            }
        }   
    }

}

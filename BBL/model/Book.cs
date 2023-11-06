using System;


namespace dbEF.BBL.model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
 
        public int? UserId { get; set; }

        public User Users { get; set; }

        public int AuthorId { get; set; }

        public Author Authors { get; set; }

        public List<Genre> Genres { get; set; } = new List<Genre>();

    }
}

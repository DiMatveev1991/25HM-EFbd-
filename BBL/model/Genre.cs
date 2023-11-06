using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbEF.BBL.model
{
    public class Genre
    {
       public int id { get; set;}
        public string NameGenre { get; set; }

        public List<Book> Books { get; set; } = new List<Book>();

    }
}

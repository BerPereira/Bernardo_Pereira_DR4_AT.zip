using System;
using System.Collections.Generic;
using System.Text;

namespace BernardoATJohn.Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Isbn { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BernardoATJohn.Domain.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public DateTime Birth { get; set; }
    }
}

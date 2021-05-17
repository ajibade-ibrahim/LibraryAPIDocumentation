using System;

namespace Library.API.Models
{
    public class Author
    {
        public string FirstName { get; set; }
        public Guid Id { get; set; }

        public string LastName { get; set; }
    }
}
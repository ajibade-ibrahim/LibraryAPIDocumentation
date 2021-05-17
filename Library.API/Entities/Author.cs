using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.Entities
{
    [Table("Authors")]
    public class Author
    {
        public ICollection<Book> Books { get; set; } = new List<Book>();

        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }
    }
}
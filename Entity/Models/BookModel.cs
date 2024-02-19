using Entity.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? Year { get; set; }
        public int? PageCount { get; set; }
        public string? AuthorName { get; set; }
        public string? PictureUrl { get; set; }
        public BookStatusType? StatusType { get; set; }
        public BookType? BookType { get; set; }

    }
}

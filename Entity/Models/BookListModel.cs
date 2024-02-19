using Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class BookListModel
    {

        public int Id { get; set; }

        public string? Title { get; set; }

        public int? Year { get; set; }

        public int? PageCount { get; set; }

        public string? AuthorName { get; set; }

        public string? PictureUrl { get; set; }
        public BookStatusType? StatusType { get; set; }
        public BookType? BookType { get; set; }
        public string? BorrowedMemberName { get; set; }

        public DateTime? ReturnDate { get; set; }  
        public DateTime? ReservedDate { get; set; }

    }
}

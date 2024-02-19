using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entity.Entities
{
    [Table("BookTransactions")]
    public class BookTransaction: BaseEntity, IEntity
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("BookId")]
        public int BookId { get; set; }
        [Column("MemberId")]
        public int MemberId { get; set; }
        [Column("BorrowedDate")]
        public DateTime? BorrowedDate { get; set; }        
        [Column("ReturnDate")]
        public DateTime? ReturnDate { get; set; }
    }
}

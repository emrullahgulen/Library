using Core.Entities;
using Entity.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities
{
    [Table("Books")]
    public class Book:BaseEntity,IEntity
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("Title")]
        public string? Title { get; set; }
        [Column("ISBN")]
        public string? ISBN { get; set; }
        [Column("Year")]
        public int? Year { get; set; }
        [Column("PageCount")]
        public int? PageCount { get; set; }
        [Column("AuthorId")]
        public int? AuthorId { get; set; }
        [Column("PictureUrl")]
        public string? PictureUrl { get; set; }
        [Column("StatusType")]
        public BookStatusType? StatusType { get; set; }     
        [Column("BookedMemberId")]
        public int? BookedMemberId { get; set; }    
        [Column("ReservedDate")]
        public DateTime? ReservedDate { get; set; }
        [Column("BookType")]
        public BookType? BookType { get; set; }
        [Column("IsActive")]
        public bool? IsActive { get; set; } = true;
    }
}

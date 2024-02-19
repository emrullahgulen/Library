using Entity.Enums;
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
    [Table("Members")]
    public class Member : BaseEntity, IEntity
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("Firstname")]
        public string? Firstname { get; set; }
        [Column("Surname")]
        public string? Surname { get; set; }
        [Column("IdentityNumber")]
        public string? IdentityNumber { get; set; }
        [Column("Address")]
        public string? Address { get; set; }
        [Column("PhoneNumber")]
        public string? PhoneNumber { get; set; }
        [Column("IsActive")]
        public bool? IsActive { get; set; } = true;

        [NotMapped]
        public string? FullName
        {
            get
            {
                return Firstname+" "+Surname;
            }
        }


    }
}

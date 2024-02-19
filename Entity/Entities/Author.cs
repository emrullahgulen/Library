using Entity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Core.Entities;

namespace Entity.Entities
{
    [Table("Authors")]
    public class Author : BaseEntity, IEntity
    {
        [Key, Column("Id")]
        public int Id { get; set; }
        [Column("Name")]
        public string? Name { get; set; }
        [Column("IsActive")]
        public bool? IsActive { get; set; } = true;

    }
}

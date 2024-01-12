using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace BookingWebApplication.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("user_name")]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [Column("email")]
        [StringLength(35)]
        public string Email { get; set; }

        [Required]
        [Column("password")]
        [StringLength(45)]
        public string Password { get; set; }

        [Column("create_time")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreateTime { get; set; }

        [Column("salt")]
        [StringLength(45)]
        public string Salt { get; set; }

        [Column("role")]
        [StringLength(45)]
        public string Role { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public virtual Admin? Admin { get; set; }
        [NotMapped]
        [ScaffoldColumn(false)]
        public virtual ContentAdmin? ContentAdmin { get; set; }
        [NotMapped]
        [ScaffoldColumn(false)]
        public virtual Customer? Customer { get; set; }
    }
}

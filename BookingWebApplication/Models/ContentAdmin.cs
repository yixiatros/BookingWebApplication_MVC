using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingWebApplication.Models
{
    [Table("content_admins")]
    public class ContentAdmin
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(45)]
        public string? Name { get; set; }

        public string UserName { get; set; }
        [ForeignKey("UserName")]
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Movie>? Movies { get; set; }
        public virtual ICollection<Provoli>? Provoles { get; set; }
    }
}

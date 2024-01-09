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
        public string Name { get; set; }

        [ForeignKey("user_username")]
        public int UserName { get; set; }

        public virtual User User { get; set; }
    }
}

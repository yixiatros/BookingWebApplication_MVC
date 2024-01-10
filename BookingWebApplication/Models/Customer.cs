using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingWebApplication.Models
{
    [Table("customers")]
    public class Customer
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(45)]
        public string Name { get; set; }

        public int UserName { get; set; }
        [ForeignKey("UserName")]
        public virtual User User { get; set; } = null!;

        public virtual ICollection<Reservation> Reservations {  get; set; }
    }
}

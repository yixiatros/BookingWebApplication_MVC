using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingWebApplication.Models
{
    [Table("cinemas")]
    public class Cinema
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [StringLength(45)]
        public string Name { get; set; }

        [Column("seats")]
        public int Seats { get; set; }

        [Column("3D")]
        [StringLength(45)]
        public string I3D { get; set; }

        public virtual ICollection<Provoli>? Provoles { get; set; } = null!;
    }
}

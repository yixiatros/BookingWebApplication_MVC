using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingWebApplication.Models
{
    [Table("provoles")]
    public class Provoli
    {
        [Key]
        [ForeignKey("MoviesId")]
        [Column("MOVIES_ID")]
        [InverseProperty("Provoles")]
        public int MoviesId { get; set; }
        [Key]
        [ForeignKey("MoviesName")]
        [Column("MOVIES_NAME")]
        [InverseProperty("Provoles")]
        [StringLength(45)]
        public string MoviesName { get; set; } = null!;
        [InverseProperty("Provoles")]
        public virtual Movie? Movie { get; set; } = null!;


        [Key]
        public int? CinemasID { get; set; }
        [ForeignKey("CinemasID")]
        [Column("CINEMAS_ID")]
        [InverseProperty("Provoles")]
        public virtual Cinema? Cinema { get; set; } = null!;

        [Column("ID")]
        public int Id { get; set; }

        [Key]
        [Column("CONTENT_ADMIN_ID")]
        public int ContentAdminId { get; set; }
        [ForeignKey("ContentAdminId")]
        [InverseProperty("Provoles")]
        public virtual ContentAdmin? ContentAdmin { get; set; } = null!;

        public virtual ICollection<Reservation>? Reservations { get; set; }
    }
}

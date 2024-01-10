using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingWebApplication.Models
{
    [Table("provoles")]
    public class Provoli
    {
        [ForeignKey("MoviesId")]
        [InverseProperty("Provoles")]
        public int MoviesId { get; set; }
        [ForeignKey("MoviesName")]
        [InverseProperty("Provoles")]
        [StringLength(45)]
        public string MoviesName { get; set; } = null!;
        [InverseProperty("Provoles")]
        public virtual Movie? Movie { get; set; } = null!;



        public int? CinemasID { get; set; }
        [ForeignKey("CinemasID")]
        [InverseProperty("Provoles")]
        public virtual Cinema? Cinema { get; set; } = null!;

        [Key][Column("id")]
        public int Id { get; set; }

        public int ContentAdminId { get; set; }
        [ForeignKey("ContentAdminId")]
        [InverseProperty("Provoles")]
        public virtual ContentAdmin? ContentAdmin { get; set; } = null!;

        //public virtual ICollection<Reservation> Reservations { get; set; }
    }
}

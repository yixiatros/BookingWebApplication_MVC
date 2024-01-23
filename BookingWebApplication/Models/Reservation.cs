using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingWebApplication.Models
{
    [Table("reservations")]
    public class Reservation
    {
        [Key]
        [ForeignKey("ProvolesMoviesId")]
        [Column("PROVOLES_MOVIES_ID")]
        public int ProvolesMoviesId { get; set; }
        [Key]
        [ForeignKey("ProvolesMoviesName")]
        [Column("PROVOLES_MOVIES_NAME")]
        [StringLength(45)]
        public string ProvolesMoviesName { get; set; }
        [Key]
        [ForeignKey("ProvolesCinemasId")]
        [Column("PROVOLES_CINEMAS_ID")]
        public int ProvolesCinemasId { get; set; }
        [Key]
        [ForeignKey("ProvolesId")]
        [Column("PROVOLES_ID")]
        public int ProvolesId { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        [ForeignKey("ProvolesContentAdminId")]
        public int ProvolesContentAdminId { get; set; }

        [InverseProperty("Reservations")]
        public virtual Provoli? Provoli { get; set; } = null!;

        [Key]
        [ForeignKey("CustomersId")]
        [Column("CUSTOMERS_ID")]
        public int CustomersId { get; set; }
        [InverseProperty("Reservations")]
        public virtual Customer? Customer { get; set; } = null!;

        [Column("NUMBER_OF_SEATS")]
        public int NumberOfSeats { get; set; }

        [Column("Seats")]
        public string Seats { get; set; }
    }
}

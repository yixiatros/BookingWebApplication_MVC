using System.ComponentModel.DataAnnotations.Schema;

namespace BookingWebApplication.Models
{
    [Table("reservations")]
    public class Reservation
    {
        [ForeignKey("provoles_movies_id")]
        public int ProvolesMoviesId { get; set; }

        [ForeignKey("provoles_movies_name")]
        [StringLength(45)]
        public string ProvolesMoviesName { get; set; }

        [ForeignKey("provoles_cinemas_id")]
        public int ProvolesCinemaId { get; set; }

        public virtual ICollection<Provoli> Provoles { get; set; }

        [ForeignKey("customers_id")]
        public int CustomersId { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
    }
}

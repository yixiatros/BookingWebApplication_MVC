using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingWebApplication.Models
{
    [Table("provoles")]
    public class Provoli
    {
        [ForeignKey("movies_id")]
        public int MoviesID { get; set; }

        [ForeignKey("movies_name")]
        [StringLength(45)]
        public string MovieName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }

        [ForeignKey("cinemas_id")]
        public int CinemasID { get; set; }

        public virtual ICollection<Cinema> Cinemas { get; set; }

        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("content_admin_id")]
        public int ContentAdminId { get; set; }

        public virtual ICollection<ContentAdmin> ContentAdmins { get; set; }
    }
}

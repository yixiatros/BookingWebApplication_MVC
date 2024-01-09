using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingWebApplication.Models
{
    [Table("movies")]
    public class Movie
    {
        [Key]
        [Column("movie_id")]
        public int MovieId { get; set; }

        [Key]
        [Column("movie_name")]
        [StringLength(45)]
        public required string MovieName { get; set; }

        [Column("movie_description")]
        [StringLength(45)]
        public string MovieContent { get; set; }

        [Column("movie_length")]
        public int MovieLength { get; set; }

        [Column("movie_type")]
        [StringLength(45)]
        public string MovieType { get; set; }


        [Column("movie_summary")]
        [StringLength(45)]
        public string MovieSummary { get; set; }

        [Column("movie_director")]
        [StringLength(45)]
        public string MovieDirector { get; set; }

        [ForeignKey("content_admin_id")]
        public int ContentAdminId { get; set; }

        public virtual ContentAdmin ContentAdmin { get; set; }
    }
}

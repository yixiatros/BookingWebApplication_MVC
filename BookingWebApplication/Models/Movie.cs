using Microsoft.AspNetCore.Html;
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

        [Column("movie_content")]
        [StringLength(45)]
        public string? MovieContent { get; set; }

        [Column("movie_length")]
        public int MovieLength { get; set; }

        [Column("movie_type")]
        [StringLength(45)]
        public string? MovieType { get; set; }

        [Column("movie_summary")]
        public string? MovieSummary { get; set; }

        [Column("movie_director")]
        [StringLength(45)]
        public string? MovieDirector { get; set; }

        public int ContentAdminId { get; set; }
        [ForeignKey("ContentAdminId")]
        [InverseProperty("Movies")]
        public virtual ContentAdmin ContentAdmin { get; set; } = null!;

        public virtual ICollection<Provoli>? Provoles { get; set; } = new List<Provoli>();
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingWebApplication.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("category_name")]
        [StringLength(50)]
        public string CategoryName { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrenchBulldogs.Data.Models
{
    public class FrenchBulldog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0, 20)]
        public int Age { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public ICollection<UserFrenchBulldog> UserFrenchBulldog { get; set; } = new List<UserFrenchBulldog>();
    }
}
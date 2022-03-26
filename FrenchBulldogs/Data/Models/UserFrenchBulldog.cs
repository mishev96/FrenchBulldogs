using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrenchBulldogs.Data.Models
{
    public class UserFrenchBulldog
    {
        [Key]
        [StringLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(36)]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        [Required]
        public int FrenchBulldogId { get; set; }

        [ForeignKey(nameof(FrenchBulldogId))]
        public FrenchBulldog FrenchBulldog { get; set; }

    }
}
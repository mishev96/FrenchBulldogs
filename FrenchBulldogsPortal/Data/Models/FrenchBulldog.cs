namespace FrenchBulldogsPortal.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.FrenchBulldog;

    public class FrenchBulldog
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [Range(1, 20)]
        public int Age { get; set; }

        [Required]
        public string Color { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public int DealerId { get; init; }

        public Dealer Dealer { get; init; }
    }
}

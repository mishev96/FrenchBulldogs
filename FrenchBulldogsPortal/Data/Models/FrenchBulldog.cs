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

        public int ColorId { get; set; }

        public Color Color { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; init; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Description { get; set; }

        public int BreederId { get; init; }

        public Breeder Breeder { get; init; }
    }
}

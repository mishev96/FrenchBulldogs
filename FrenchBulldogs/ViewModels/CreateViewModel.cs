using System.ComponentModel.DataAnnotations;

namespace FrenchBulldogs.ViewModels
{
    public class CreateViewModel
    {
        [Required]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "{0} must be between {2} and {1} characters")]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0, 20, ErrorMessage = "{0} must be between {2} and {1}")]
        public int Age { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "{0} must be {1} characters max")]
        public string Description { get; set; }
    }
}

namespace FrenchBulldogsPortal.Models.FrenchBulldogs
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FrenchBulldogsPortal.Services.FrenchBulldogs.Models;

    using static Data.DataConstants.FrenchBulldog;

    public class FrenchBulldogFormModel : IFrenchBulldogModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; init; }

        [Required]
        public string Color { get; init; }

        [Required]
        [StringLength(
            int.MaxValue, 
            ErrorMessage = "The field Description must be a string with a minimum length of {2}.")]
        public string Description { get; init; }

        [Required]
        [Url]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; init; }

        [Range(AgeMinValue, AgeMaxValue)]
        public int Age { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public IEnumerable<FrenchBulldogCategoryServiceModel> Categories { get; set; }
    }
}

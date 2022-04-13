namespace FrenchBulldogsPortal.Models.Breeders
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Breeder;

    public class BecomeBreederFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        [Display(Name = "Phone Number")] 
        public string PhoneNumber { get; set; }
    }
}

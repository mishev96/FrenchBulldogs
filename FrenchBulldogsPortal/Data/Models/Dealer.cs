﻿namespace FrenchBulldogsPortal.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Dealer;

    public class Dealer
    {
        public int Id { get; init; }
         
        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<FrenchBulldog> FrenchBulldogs { get; init; } = new List<FrenchBulldog>();
    }
}

﻿namespace FrenchBulldogsPortal.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<FrenchBulldog> FrenchBulldogs { get; init; } = new List<FrenchBulldog>();
    }
}

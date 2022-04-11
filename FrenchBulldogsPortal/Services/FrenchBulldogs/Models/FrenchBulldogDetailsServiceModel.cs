﻿namespace FrenchBulldogsPortal.Services.FrenchBulldogs.Models
{
    public class FrenchBulldogDetailsServiceModel : FrenchBulldogServiceModel
    {
        public string Description { get; init; }

        public int CategoryId { get; init; }

        public int DealerId { get; init; }

        public string DealerName { get; init; }

        public string UserId { get; init; }
    }
}

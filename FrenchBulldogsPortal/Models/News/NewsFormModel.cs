namespace FrenchBulldogsPortal.Models.News
{
    using System.ComponentModel.DataAnnotations;
	using FrenchBulldogsPortal.Services.News.Models;
	using static Data.DataConstants.News;

    public class NewsFormModel : INewsModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; init; }

        [Required]
        [StringLength(
            int.MaxValue, 
            ErrorMessage = "The field Content must be a string with a minimum length of {2}.")]
        public string Content { get; init; }     
	}
}

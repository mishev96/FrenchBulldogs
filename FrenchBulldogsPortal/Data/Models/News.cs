namespace FrenchBulldogsPortal.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants.News;

    public class News
	{
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

    }
}

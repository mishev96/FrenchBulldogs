using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrenchBulldogs.Data.Models
{
	public class Comment
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[StringLength(500, MinimumLength = 3)]
		public string Content { get; set; }

		[Required]
		public DateTime DateCreated { get; set; }

		[Required]
		public string Username { get; set; }

		[Required]
		public int BlogPostId { get; set; }

		[ForeignKey(nameof(BlogPostId))]
		public BlogPost BlogPost { get; set; }
	}
}

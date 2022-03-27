using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrenchBulldogs.Data.Models
{
	public class BlogPost
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Title { get; set; }

		[Required]
		[StringLength(500, MinimumLength = 3)]
		[MaxLength]
		public string Content { get; set; }

		[Required]
		public DateTime DateCreated { get; set; }

		[Required]
		[StringLength(36)]
		public string UserId { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }

		public ICollection<Comment> Comments { get; set; } = new List<Comment>();
	}
}

namespace FrenchBulldogsPortal.Data.Models
{
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;

	public class Color
	{
		public int Id { get; init; }

		[Required]
		public string Name { get; set; }

		public IEnumerable<FrenchBulldog> FrenchBulldogs { get; init; } = new List<FrenchBulldog>();
	}
}

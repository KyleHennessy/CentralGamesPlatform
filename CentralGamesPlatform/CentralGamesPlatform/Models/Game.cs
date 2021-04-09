using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CentralGamesPlatform.Models
{
	public class Game
	{
		[BindNever]
		public int GameId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		[Range(0.00, 100.00)]
		public decimal Price { get; set; }
		[Required]
		public string ImageUrl { get; set; }
		[Required]
		public string ImageThumbnailUrl { get; set; }
		[Required]
		public bool IsOnSale { get; set; }
		[Required]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}

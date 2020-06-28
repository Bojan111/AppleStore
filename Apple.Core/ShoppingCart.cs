using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading;

namespace Apple.Core
{
	public class ShoppingCart
	{
		public ShoppingCart()
		{
			Count = 1;
		}
		public int Id { get; set; }
		public int CatalogId { get; set; }
		[NotMapped]
		[ForeignKey("CatalogId")]
		public virtual Catalog Catalog { get; set; }

		public string ApplicationUserId { get; set; }

		[NotMapped]
		[ForeignKey("ApplcationUserId")]
		public virtual ApplicationUser ApplicationUser { get; set; }
		[Range(1,100, ErrorMessage = "Please select a count betweem 1 and 100")]
		public int Count { get; set; }
	}
}

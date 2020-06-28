using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Apple.Core
{
	public class ProductType
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Display(Name = "Product Type Name")]
		public string Name { get; set; }
	}
}

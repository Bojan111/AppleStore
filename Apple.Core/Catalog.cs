using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Apple.Core
{
	public class Catalog
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		public string Specifications { get; set; }
		public string Image { get; set; }
		[Range(1,int.MaxValue, ErrorMessage ="Price should be greater than 1")]
		public double Price { get; set; }
		[Display(Name = "Category Type")]
		public int CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public virtual Category Category { get; set; }
		[Display(Name ="Product Type")]
		public int ProductTypeId { get; set; }
		[ForeignKey("ProductTypeId")]
		public virtual ProductType ProductType { get; set; }
	}
}

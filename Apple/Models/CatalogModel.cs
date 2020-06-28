using Apple.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apple.Models
{
	public class CatalogModel
	{
		public Catalog Catalogs { get; set; }
		public IEnumerable<SelectListItem> CategoryList { get; set; }
		public IEnumerable<SelectListItem> ProductTypeList { get; set; }
	}
}

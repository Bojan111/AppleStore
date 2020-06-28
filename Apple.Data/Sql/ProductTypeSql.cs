using Apple.Core;
using Apple.Data.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apple.Data.Sql
{
	public class ProductTypeSql : Sql<ProductType>, IProductType
	{
		private readonly AppleDbContext db;

		public ProductTypeSql(AppleDbContext db) : base(db)
		{
			this.db = db;
		}

		public IEnumerable<SelectListItem> ProductTypes()
		{
			return db.ProductType.Select(i => new SelectListItem()
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
		}

		public void Update(ProductType productType)
		{
			var productTypeObj = db.ProductType.FirstOrDefault(s => s.Id == productType.Id);
			productTypeObj.Name = productType.Name;

			db.SaveChanges();
		}
	}
}

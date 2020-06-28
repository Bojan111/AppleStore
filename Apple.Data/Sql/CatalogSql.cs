using Apple.Core;
using Apple.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apple.Data.Sql
{
	public class CatalogSql : Sql<Catalog>, ICatalog
	{
		private readonly AppleDbContext db;

		public CatalogSql(AppleDbContext db) : base(db)
		{
			this.db = db;
		}

		public void Update(Catalog catalog)
		{
			var catalogObj = db.Catalog.FirstOrDefault(m => m.Id == catalog.Id);
			catalogObj.Name = catalog.Name;
			catalogObj.CategoryId = catalog.CategoryId;
			catalogObj.Specifications = catalog.Specifications;
			catalogObj.ProductTypeId = catalog.ProductTypeId;
			catalogObj.Price = catalog.Price;
			if(catalog.Image != null)
			{
				catalogObj.Image = catalog.Image;
			}
			db.SaveChanges();
		}
	}
}

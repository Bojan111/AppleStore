using Apple.Core;
using Apple.Data.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apple.Data.Sql
{
	public class CategorySql : Sql<Category>, ICategory
	{
		private readonly AppleDbContext db;

		public CategorySql(AppleDbContext db) : base(db)
		{
			this.db = db;
		}

		public IEnumerable<SelectListItem> Categories()
		{
			return db.Category.Select(i => new SelectListItem()
			{
				Text = i.Name,
				Value = i.Id.ToString()
			});
		}

		public void Update(Category category)
		{
			var categoryObj = db.Category.FirstOrDefault(s => s.Id == category.Id);

			categoryObj.Name = category.Name;
			categoryObj.DisplayOrder = category.DisplayOrder;

			db.SaveChanges();

		}
	}
}

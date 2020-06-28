using Apple.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apple.Data.Interface
{
	public interface ICategory : ISql<Category>
	{
		IEnumerable<SelectListItem> Categories();
		void Update(Category category);
	}
}

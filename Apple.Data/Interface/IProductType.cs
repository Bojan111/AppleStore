using Apple.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apple.Data.Interface
{
	public interface IProductType : ISql<ProductType>
	{
		IEnumerable<SelectListItem> ProductTypes();
		void Update(ProductType productType);
	}
}

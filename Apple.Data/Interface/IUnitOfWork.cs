using System;
using System.Collections.Generic;
using System.Text;

namespace Apple.Data.Interface
{
	public interface IUnitOfWork : IDisposable
	{
		ICategory Category { get; }
		IProductType ProductType { get; }
		ICatalog Catalog { get; }
		IApplicationUser ApplicationUser { get; }
		IShoppingCart ShoppingCart { get; }
		IOrderHeader OrderHeader { get; }
		IOrderDetails OrderDetails { get; }
		void Save();
	}
}

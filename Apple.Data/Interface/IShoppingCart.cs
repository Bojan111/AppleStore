using Apple.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apple.Data.Interface
{
	public interface IShoppingCart : ISql<ShoppingCart>
	{
		int IncrementCount(ShoppingCart shoppingCart, int count);
		int DecrementCount(ShoppingCart shoppingCart, int count);
	}
}

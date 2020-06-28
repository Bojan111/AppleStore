using Apple.Core;
using Apple.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apple.Data.Sql
{
	public class ShoppingCartSql : Sql<ShoppingCart>, IShoppingCart
	{
		private readonly AppleDbContext db;

		public ShoppingCartSql(AppleDbContext db) : base(db)
		{
			this.db = db;
		}

		public int DecrementCount(ShoppingCart shoppingCart, int count)
		{
			shoppingCart.Count -= count;
			return shoppingCart.Count;
		}

		public int IncrementCount(ShoppingCart shoppingCart, int count)
		{
			shoppingCart.Count += count;
			return shoppingCart.Count;
		}
	}
}

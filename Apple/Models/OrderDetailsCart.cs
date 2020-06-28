using Apple.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Apple.Models
{
	public class OrderDetailsCart
	{
		public List<ShoppingCart> ShoppingCarts { get; set; }
		public OrderHeader OrderHeaders { get; set; }
	}
}

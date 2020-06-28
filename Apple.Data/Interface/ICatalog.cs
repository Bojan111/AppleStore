using Apple.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apple.Data.Interface
{
	public interface ICatalog : ISql<Catalog>
	{
		void Update(Catalog catalog);
	}
}

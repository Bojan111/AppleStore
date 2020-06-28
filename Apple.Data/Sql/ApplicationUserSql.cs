using Apple.Core;
using Apple.Data.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apple.Data.Sql
{
	public class ApplicationUserSql : Sql<ApplicationUser>, IApplicationUser
	{
		private readonly AppleDbContext db;

		public ApplicationUserSql(AppleDbContext db) : base(db)
		{
			this.db = db;
		}
	}
}

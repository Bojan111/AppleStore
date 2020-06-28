using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apple.Data.DbContextCreate
{
	public class DbCreate : IDbCreate
	{
		private readonly AppleDbContext db;
		private readonly UserManager<IdentityUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public DbCreate(AppleDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this.db = db;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}


		public void Initialize()
		{
			try
			{
				if(db.Database.GetPendingMigrations().Count() > 0)
				{
					db.Database.Migrate();
				}
			}
			catch(Exception e)
			{
			}
			if (db.Roles.Any(r => r.Name == SD.ManagerRole)) return;

			roleManager.CreateAsync(new IdentityRole(SD.ManagerRole)).GetAwaiter().GetResult();
			roleManager.CreateAsync(new IdentityRole(SD.CustomerRole)).GetAwaiter().GetResult();
		}
	}
}

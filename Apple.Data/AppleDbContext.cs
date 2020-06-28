using Apple.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apple.Data
{
	public class AppleDbContext : IdentityDbContext
	{
		public AppleDbContext(DbContextOptions<AppleDbContext> options) : base(options)
		{

		}
		public DbSet<Category> Category { get; set; }
		public DbSet<ProductType> ProductType { get; set; }
		public DbSet<Catalog> Catalog { get; set; }
		public DbSet<ApplicationUser> ApplicationUser { get; set; }
		public DbSet<ShoppingCart> ShoppingCart { get; set; }
		public DbSet<OrderHeader> OrderHeaders { get; set; }
		public DbSet<OrderDetails> OrderDetails { get; set; }
		
	}
}

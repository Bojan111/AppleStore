using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text;

namespace Apple.Core
{
	public class ApplicationUser : IdentityUser
	{
		[Display(Name = "Full Name")]
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[NotMapped]
		public string FullName { get { return FirstName + " " + LastName; } }
	}
}

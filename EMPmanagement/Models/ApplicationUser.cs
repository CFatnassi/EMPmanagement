using Microsoft.AspNetCore.Identity;

namespace EMPmanagement.Models
{
	public class ApplicationUser: IdentityUser
	{
		public string? FullName { get; set; }
        public int UserKind { get; set; }

		public int Status { get; set; }
	}
}

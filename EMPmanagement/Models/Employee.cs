using System.ComponentModel.DataAnnotations;

namespace EMPmanagement.Models
{
    public class Employee 
    {
        public int Id { get; set; }

		[MaxLength(100)]
		[Display(Name = "EmpNo", ResourceType = typeof(Resources.Resource))]
		public string EmpNo { get; set; }

		[MaxLength(100)]
		[Display(Name = "FirstName", ResourceType = typeof(Resources.Resource))]
		public string FirstName { get; set; }

		[MaxLength(100)]
		[Display(Name = "MiddleName", ResourceType = typeof(Resources.Resource))]
		public string MiddleName { get; set; }

		[MaxLength(100)]
		[Display(Name = "LastName", ResourceType = typeof(Resources.Resource))]
		public string LastName { get; set; }

		[MaxLength(100)]
		[Display(Name = "FullName", ResourceType = typeof(Resources.Resource))]
		public string FullName => $"{FirstName} {MiddleName} {LastName}";

		[MaxLength(100)]
		[Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Resource))]
		public int PhoneNumber { get; set; }

		[MaxLength(100)]
		[Display(Name = "EmailAddress", ResourceType = typeof(Resources.Resource))]
		public string EmailAddress { get; set; }

		[MaxLength(100)]
		[Display(Name = "Country", ResourceType = typeof(Resources.Resource))]
		public string Country { get; set; }

		[MaxLength(100)]
		[Display(Name = "DateOfBirth", ResourceType = typeof(Resources.Resource))]
		public DateTime DateOfBirth { get; set; }

		[MaxLength(100)]
		[Display(Name = "Address", ResourceType = typeof(Resources.Resource))]
		public string Address { get; set; }


        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
    }
}

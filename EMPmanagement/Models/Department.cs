using System.ComponentModel.DataAnnotations;

namespace EMPmanagement.Models
{
    public class Department
    {
		[Key]
        public int DepId { get; set; }

		[MaxLength(100)]
		[Display(Name = "DepArName", ResourceType = typeof(Resources.Resource))]
		public string DepArName { get; set; }

		[MaxLength(100)]
		[Display(Name = "DepEngName", ResourceType = typeof(Resources.Resource))]
		public string DepEngName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EMPmanagement.Models
{
    public class Designation
    {
        [Key]
        public int DesigId { get; set; }

		[MaxLength(100)]
		[Display(Name = "DesigArName", ResourceType = typeof(Resources.Resource))]
		public string DesigArName { get; set; }

		[MaxLength(100)]
		[Display(Name = "DesigEngName", ResourceType = typeof(Resources.Resource))]
		public string DesigEngName { get; set; }
    }
}
